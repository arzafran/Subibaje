using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Modelos;
using System.Data;

namespace accesoDatos
{
    public class DBEstablecimientos : IcapaDato<Establecimiento>
    {
        private DBConnector _conexion = new DBConnector();
        private DBCiudades _ciudades = new DBCiudades();
        private DBNivelesEducativos _niveles = new DBNivelesEducativos();

        /// <summary>
        /// Guarda un establecimiento en la DB
        /// </summary>
        /// <param name="oEstablecimiento">Objeto establecimiento a guardar</param>

        public void Agregar(Establecimiento oEstablecimiento)
        {
            string query = "INSERT INTO establecimientos (nombre, ciudad_id) VALUES ('" + oEstablecimiento.Nombre + "', " + oEstablecimiento.Ciudad.Id + ")";
            int id = _conexion.EjecutarEscalar(query);
            InsertarNiveles(id, oEstablecimiento.ListaNiveles);
        }

        /// <summary>
        /// Marca un registro de la DB como borrado aplicando un timestamp
        /// </summary>
        /// <param name="id">ID del registro a desactivar</param>

        public void Desactivar(int id)
        {
            string query = "UPDATE establecimientos SET borrado = getdate() WHERE id=" + id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        /// <summary>
        /// Marca un registro de la DB como borrado aplicando un timestamp
        /// </summary>
        /// <param name="oEstablecimiento">Objeto a desactivar</param>

        public void Desactivar(Establecimiento oEstablecimiento)
        {
            this.Desactivar(oEstablecimiento.Id);
        }

        /// <summary>
        /// Reactiva un registro de la DB
        /// </summary>
        /// <param name="id">ID del registro a activar</param>

        public void Reactivar(int id)
        {
            string query = "UPDATE establecimientos SET borrado = NULL WHERE id=" + id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        /// <summary>
        /// Edita un objeto y lo persiste en la DB
        /// </summary>
        /// <param name="oEstablecimiento">Establecimiento a ser editado y persistido</param>

        public void Editar(Establecimiento oEstablecimiento)
        {
            int id = oEstablecimiento.Id;
            string query = "UPDATE establecimientos SET nombre = '" + oEstablecimiento.Nombre + "', ciudad_id=" + oEstablecimiento.Ciudad.Id + " WHERE id=" + id.ToString();
            _conexion.EjecutarNonSql(query);
            BorrarNiveles(id);
            InsertarNiveles(id, oEstablecimiento.ListaNiveles);
        }

        /// <summary>
        /// Busca un establecimiento en la DB con el id especificado
        /// </summary>
        /// <param name="id">ID del establecimiento a buscar</param>
        /// <returns>Devuelve un objeto establecimiento o null cuando no encuentra registro.</returns>

        public Establecimiento BuscarPorId(int id)
        {
            Establecimiento devolver = null;
            string query = "SELECT TOP 1 * FROM establecimientos WHERE id = " + id.ToString();

            DataTable dt = _conexion.TraerDatos(query);
            if (dt.Rows.Count > 0)
            {
                devolver = ArmarObjeto(dt.Rows[0]);
            }

            return devolver;
        }

        /// <summary>
        /// Busca todos los establecimientos de la DB
        /// </summary>
        /// <returns>Devuelve una lista de establecimientos</returns>

        public List<Establecimiento> TraerTodos()
        {
            List<Establecimiento> devolver = new List<Establecimiento>();
            string query = "SELECT * FROM establecimientos ORDER BY borrado ASC, nombre ASC";
            DataTable dt = _conexion.TraerDatos(query);

            foreach (DataRow dr in dt.Rows)
            {
                devolver.Add(ArmarObjeto(dr));
            }

            return devolver;
        }

        /// <summary>
        /// Busca todos los establecimientos activos de la DB
        /// </summary>
        /// <returns>Devuelve una lista de establecimientos</returns>

        public List<Establecimiento> TraerActivos()
        {
            List<Establecimiento> devolver = new List<Establecimiento>();
            string query = "SELECT * FROM establecimientos WHERE borrado IS NULL ORDER BY nombre ASC";
            DataTable dt = _conexion.TraerDatos(query);

            foreach (DataRow dr in dt.Rows)
            {
                devolver.Add(ArmarObjeto(dr));
            }

            return devolver;
        }

        /// <summary>
        /// Busca todos los establecimientos con el nombre especificado
        /// </summary>
        /// <param name="nombre">Nombre a buscar en la DB</param>
        /// <returns>Devuelve una lista de objetos establecimiento</returns>

        public List<Establecimiento> BuscarPorNombre(string nombre)
        {
            List<Establecimiento> devolver = new List<Establecimiento>();
            string query = "SELECT * FROM establecimientos WHERE nombre = '" + nombre + "'";

            DataTable dt = _conexion.TraerDatos(query);
            
            foreach (DataRow dr in dt.Rows)
            {
                devolver.Add(ArmarObjeto(dr));
            }

            return devolver;
        }

        /// <summary>
        /// Genera un objeto establecimiento en base a un registro de la DB
        /// </summary>
        /// <param name="dr">Datarow de un datatable</param>
        /// <returns>Devuelve un objeto establecimiento</returns>

        private Establecimiento ArmarObjeto(DataRow dr)
        {
            DateTime dt;
            Ciudad oCiudad;
            List<NivelEducativo> niveles;
            int id;

            if (dr["borrado"] is DBNull)
                dr["borrado"] = "9/9/9999";

            id = (int)dr["id"];
            DateTime.TryParse(dr["borrado"].ToString(), out dt);
            oCiudad = _ciudades.BuscarPorId((int)dr["ciudad_id"]);
            niveles = NivelesAsociados(id);

            return new Establecimiento((string)dr["nombre"], oCiudad, niveles, id, dt);
        }

        /// <summary>
        /// Busca todos los niveles educativos asociados con el establecimiento especificado
        /// </summary>
        /// <param name="id">ID del establecimiento</param>
        /// <returns>Devuelve una lista de niveles educativos</returns>

        public List<NivelEducativo> NivelesAsociados(int id)
        {
            List<NivelEducativo> devolver = new List<NivelEducativo>();
            string query = "SELECT * FROM establecimiento_nivel WHERE borrado IS NULL AND establecimiento_id = " + id.ToString();
            DataTable dt = _conexion.TraerDatos(query);
            foreach (DataRow dr in dt.Rows)
            {
                devolver.Add(_niveles.BuscarPorId((int)dr["nivel_id"]));
            }

            return devolver;
        }

        /// <summary>
        /// Inserta relaciones asociando niveles educativos y establecimientos
        /// </summary>
        /// <param name="id">ID del establecimiento</param>
        /// <param name="niveles">Niveles del establecimiento</param>

        private void InsertarNiveles(int id, List<NivelEducativo> niveles)
        {
            if (niveles.Count > 0) 
            { 
                List<string> inserts = new List<string>();
                string insertarNivel;

                insertarNivel = "INSERT INTO establecimiento_nivel (establecimiento_id, nivel_id) VALUES ";

                foreach (NivelEducativo oNivel in niveles)
                {
                    inserts.Add("(" + id.ToString() + ", " + oNivel.Id.ToString() + ")");
                }

                insertarNivel += string.Join(",", inserts);
                _conexion.EjecutarNonSql(insertarNivel);
            }
        }

        /// <summary>
        /// Borra todas las relaciones con los niveles educativos asociados a un establecimiento
        /// </summary>
        /// <param name="id">ID del establecimiento</param>

        private void BorrarNiveles(int id)
        {
            string query = "DELETE FROM establecimiento_nivel WHERE borrado IS NULL AND establecimiento_id = " + id.ToString();
            _conexion.EjecutarNonSql(query);
        }
    }
}

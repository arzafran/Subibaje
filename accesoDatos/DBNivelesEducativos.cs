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
    public class DBNivelesEducativos : IcapaDato<NivelEducativo>
    {
        private DBConnector _conexion = new DBConnector();

        /// <summary>
        /// Guarda un nivel educativo en la DB
        /// </summary>
        /// <param name="oProvincia">Objeto nivel educativo a guardar</param>
        
        public void Agregar(NivelEducativo oNivel)
        {
            string query = "INSERT INTO niveles (nombre) VALUES ('" + oNivel.Nombre + "')";
            _conexion.EjecutarNonSql(query);
        }

        /// <summary>
        /// Marca un registro de la DB como borrado aplicando un timestamp
        /// </summary>
        /// <param name="id">ID del registro a desactivar</param>
        
        public void Desactivar(int id)
        {
            string query = "UPDATE niveles SET borrado = getdate() WHERE id=" + id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        /// <summary>
        /// Marca un registro de la DB como borrado aplicando un timestamp
        /// </summary>
        /// <param name="oNivel">Objeto a desactivar</param>

        public void Desactivar(NivelEducativo oNivel)
        {
            this.Desactivar(oNivel.Id);
        }

        /// <summary>
        /// Reactiva un registro de la DB
        /// </summary>
        /// <param name="id">ID del registro a activar</param>

        public void Reactivar(int id)
        {
            string query = "UPDATE niveles SET borrado = NULL WHERE id=" + id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        /// <summary>
        /// Edita un objeto y lo persiste en la DB
        /// </summary>
        /// <param name="oNivel">Nivel educativo a ser editado y persistido</param>

        public void Editar(NivelEducativo oNivel)
        {
            string query = "UPDATE niveles SET nombre = '" + oNivel.Nombre + "' WHERE id=" + oNivel.Id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        /// <summary>
        /// Busca un nivel educativo en la DB con el id especificado
        /// </summary>
        /// <param name="id">ID del nivel educativo a buscar</param>
        /// <returns>Devuelve un objeto nivel educativo o null cuando no encuentra registro.</returns>

        public NivelEducativo BuscarPorId(int id)
        {
            NivelEducativo devolver = null;
            string query = "SELECT TOP 1 * FROM niveles WHERE id = " + id.ToString();

            DataTable dt = _conexion.TraerDatos(query);
            if (dt.Rows.Count > 0)
            {
                devolver = ArmarObjeto(dt.Rows[0]);
            }

            return devolver;
        }

        /// <summary>
        /// Busca los niveles educativos con id incluido en la lista especificada
        /// </summary>
        /// <param name="ids">Lista de ids a buscar</param>
        /// <returns>Devuelve un listado de objetos nivel educativo</returns>

        public List<NivelEducativo> Traer(List<int> ids)
        {
            List<NivelEducativo> devolver = new List<NivelEducativo>();
            if (ids.Count > 0)
            {
                string query = "SELECT * from niveles WHERE id IN (" + string.Join(",", ids) + ") AND borrado IS NULL";
                DataTable dt = _conexion.TraerDatos(query);

                foreach (DataRow dr in dt.Rows)
                {
                    devolver.Add(ArmarObjeto(dr));
                }
            }
            return devolver;
        }

        /// <summary>
        /// Busca todos los niveles educativos de la DB
        /// </summary>
        /// <returns>Devuelve una lista de niveles educativos</returns>

        public List<NivelEducativo> TraerTodos()
        {
            List<NivelEducativo> devolver = new List<NivelEducativo>();
            string query = "SELECT * FROM niveles ORDER BY borrado ASC, nombre ASC";
            DataTable dt = _conexion.TraerDatos(query);

            foreach (DataRow dr in dt.Rows)
            {
                devolver.Add(ArmarObjeto(dr));
            }

            return devolver;
        }

        /// <summary>
        /// Busca todos los niveles educativos activos de la DB
        /// </summary>
        /// <returns>Devuelve una lista de niveles educativos</returns>

        public List<NivelEducativo> TraerActivos()
        {
            List<NivelEducativo> devolver = new List<NivelEducativo>();
            string query = "SELECT * FROM niveles WHERE borrado IS NULL ORDER BY nombre ASC";
            DataTable dt = _conexion.TraerDatos(query);

            foreach (DataRow dr in dt.Rows)
            {
                devolver.Add(ArmarObjeto(dr));
            }

            return devolver;
        }

        /// <summary>
        /// Busca todos los niveles educativos con el nombre especificado
        /// </summary>
        /// <param name="nombre">Nombre a buscar en la DB</param>
        /// <returns>Devuelve una lista de objetos nivel educativo.</returns>

        public NivelEducativo BuscarPorNombre(string nombre)
        {
            NivelEducativo devolver = null;
            string query = "SELECT TOP 1 * FROM niveles WHERE nombre = '" + nombre + "'";

            DataTable dt = _conexion.TraerDatos(query);
            if (dt.Rows.Count > 0)
            {
                devolver = ArmarObjeto(dt.Rows[0]);
            }

            return devolver;
        }

        /// <summary>
        /// Genera un objeto nivel educativo en base a un registro de la DB
        /// </summary>
        /// <param name="dr">Datarow de un datatable</param>
        /// <returns>Devuelve un objeto nivel educativo</returns>

        private NivelEducativo ArmarObjeto(DataRow dr)
        {
            DateTime dt;

            if (dr["borrado"] is DBNull)
                dr["borrado"] = "9/9/9999";

            DateTime.TryParse(dr["borrado"].ToString(), out dt);

            return new NivelEducativo((string)dr["nombre"], (int)dr["id"], dt);
        }
    }
}

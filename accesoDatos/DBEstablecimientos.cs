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
    public class DBEstablecimientos : CapaDatosAbstractaSingleton<DBEstablecimientos>, IcapaDato<Establecimiento>
    {
        private DBConnector _conexion = new DBConnector();
        public DBCiudades ciudades = new DBCiudades();
        public DBNivelesEducativos niveles = new DBNivelesEducativos();

        public void Agregar(Establecimiento oEstablecimiento)
        {
            string insertarNivel;
            string query = "INSERT INTO establecimientos (nombre, ciudad_id) VALUES ('" + oEstablecimiento.Nombre + "', " + oEstablecimiento.Ciudad.Id + ")";
            int id;

            id = _conexion.EjecutarEscalar(query);

            foreach (NivelEducativo oNivel in oEstablecimiento.ListaNiveles)
            {
                insertarNivel = "INSERT INTO establecimiento_nivel (establecimiento_id, nivel_id) VALUES (" + id.ToString() + ", " + oNivel.Id.ToString() + ")";
                _conexion.EjecutarNonSql(insertarNivel);
            }
        }

        public void Borrar(int id)
        {
            string query = "UPDATE establecimientos SET borrado = getdate() WHERE id=" + id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        public void Borrar(Establecimiento oEstablecimiento)
        {
            this.Borrar(oEstablecimiento.Id);
        }

        public void Restituir(int id)
        {
            string query = "UPDATE establecimientos SET borrado = NULL WHERE id=" + id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        public void Editar(Establecimiento oEstablecimiento)
        {
            string query = "UPDATE establecimientos SET nombre = '" + oEstablecimiento.Nombre + "', ciudad_id=" + oEstablecimiento.Ciudad.Id + " WHERE id=" + oEstablecimiento.Id.ToString();
            _conexion.EjecutarNonSql(query);
        }

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

        private Establecimiento ArmarObjeto(DataRow dr)
        {
            DateTime dt;
            Ciudad oCiudad;
            List<NivelEducativo> niveles;
            int id;

            if (dr["borrado"] is DBNull)
                dr["borrado"] = "9/9/9999";

            id = (int) dr["id"];
            DateTime.TryParse(dr["borrado"].ToString(), out dt);
            oCiudad = ciudades.BuscarPorId((int)dr["ciudad_id"]);
            niveles = NivelesAsociados(id);

            return new Establecimiento((string)dr["nombre"], oCiudad, niveles, id, dt);
        }

        public List<NivelEducativo> NivelesAsociados(int id)
        {
            List<NivelEducativo> devolver = new List<NivelEducativo>();
            foreach (int nivel_id in _niveles(id))
            {
                devolver.Add(niveles.BuscarPorId(nivel_id));
            }

            return devolver;
        }

        private List<int> _niveles(int id)
        {
            List<int> devolver = new List<int>();

            string query = "SELECT * FROM establecimiento_nivel WHERE establecimiento_id = " + id.ToString();
            DataTable dt = _conexion.TraerDatos(query);
            foreach (DataRow dr in dt.Rows)
            {
                devolver.Add((int)dr["nivel_id"]);
            }

            return devolver;
        }

        public Establecimiento BuscarPorNombre(string nombre)
        {
            Establecimiento devolver = null;
            string query = "SELECT TOP 1 * FROM establecimientos WHERE nombre = '" + nombre + "'";

            DataTable dt = _conexion.TraerDatos(query);
            if (dt.Rows.Count > 0)
            {
                devolver = ArmarObjeto(dt.Rows[0]);
            }

            return devolver;
        }
    }
}

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
    public class DBNivelesEducativos : CapaDatosAbstractaSingleton<DBNivelesEducativos>, IcapaDato<NivelEducativo>
    {
        private DBConnector _conexion = new DBConnector();

        public void Agregar(NivelEducativo oNivel)
        {
            string query = "INSERT INTO niveles (nombre) VALUES ('" + oNivel.Nombre + "')";
            _conexion.EjecutarNonSql(query);
        }

        public void Borrar(int id)
        {
            //string query = "DELETE FROM provincias WHERE id=" + id.ToString();
            string query = "UPDATE niveles SET borrado = getdate() WHERE id=" + id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        public void Borrar(NivelEducativo oNivel)
        {
            this.Borrar(oNivel.Id);
        }

        public void Restituir(int id)
        {
            string query = "UPDATE niveles SET borrado = NULL WHERE id=" + id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        public void Editar(NivelEducativo oNivel)
        {
            string query = "UPDATE niveles SET nombre = '" + oNivel.Nombre + "' WHERE id=" + oNivel.Id.ToString();
            _conexion.EjecutarNonSql(query);
        }

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

        private NivelEducativo ArmarObjeto(DataRow dr)
        {
            DateTime dt;

            if (dr["borrado"] is DBNull)
                dr["borrado"] = "9/9/9999";

            DateTime.TryParse(dr["borrado"].ToString(), out dt);

            return new NivelEducativo((string)dr["nombre"], (int)dr["id"], dt);
        }

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
    }
}

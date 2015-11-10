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
    public class DBProvincias : CapaDatosAbstractaSingleton<DBProvincias>, IcapaDato<Provincia>
    {
        private DBConnector _conexion = new DBConnector();

        public void Agregar(Provincia oProvincia)
        {
            if (this.BuscarPorNombre(oProvincia.Nombre) != null)
                throw new Exception("La provincia ya está registrada");

            string query = "INSERT INTO provincias (nombre) VALUES ('" + oProvincia.Nombre + "')";
            _conexion.EjecutarNonSql(query);

        }

        public void Borrar(int id)
        {
            //string query = "DELETE FROM provincias WHERE id=" + id.ToString();
            string query = "UPDATE provincias SET borrado = getdate() WHERE id=" + id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        public void Borrar(Provincia oProvincia)
        { }

        public void Restituir(int id)
        {
            //string query = "DELETE FROM provincias WHERE id=" + id.ToString();
            string query = "UPDATE provincias SET borrado = NULL WHERE id=" + id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        public void Editar(Provincia oProvincia)
        {
            if (this.BuscarPorNombre(oProvincia.Nombre) != null)
                throw new Exception("Ese nombre ya está tomado");

            string query = "UPDATE provincias SET nombre = '" + oProvincia.Nombre + "' WHERE id=" + oProvincia.Id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        public Provincia BuscarPorId(int id) 
        {
            Provincia devolver = null;
            string query = "SELECT TOP 1 * FROM provincias WHERE id = " + id.ToString();

            DataTable dt = _conexion.TraerDatos(query);
            if (dt.Rows.Count > 0)
            {
                devolver = ArmarObjeto(dt.Rows[0]);
            }

            return devolver;
        }

        public List<Provincia> TraerTodos()
        {
            List<Provincia> devolver = new List<Provincia>();
            string query = "SELECT * FROM provincias ORDER BY borrado ASC, nombre ASC";
            DataTable dt = _conexion.TraerDatos(query);

            foreach (DataRow dr in dt.Rows)
            {
                devolver.Add(ArmarObjeto(dr));
            }

            return devolver;
        }

        private Provincia ArmarObjeto(DataRow dr)
        {
            DateTime dt;

            if (dr["borrado"] is DBNull)
                dr["borrado"] = "9/9/9999";

            DateTime.TryParse(dr["borrado"].ToString(), out dt);

            return new Provincia((int)dr["id"], (string) dr["nombre"], dt);
        }

        private Provincia BuscarPorNombre(string nombre)
        {
            Provincia devolver = null;
            string query = "SELECT TOP 1 * FROM provincias WHERE nombre = '" + nombre + "'";

            DataTable dt = _conexion.TraerDatos(query);
            if (dt.Rows.Count > 0)
            {
                devolver = ArmarObjeto(dt.Rows[0]);
            }

            return devolver;
        }
    }
}

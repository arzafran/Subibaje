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
    public class DBCiudades : CapaDatosAbstractaSingleton<DBCiudades>, IcapaDato<Ciudad>
    {
        private DBConnector _conexion = new DBConnector();
        public DBProvincias provincias = new DBProvincias();

        public void Agregar(Ciudad oCiudad)
        {
            string query = "INSERT INTO ciudades (nombre, provincia_id) VALUES ('" + oCiudad.Nombre + "', " + oCiudad.Provincia.Id + ")";
            _conexion.EjecutarNonSql(query);
        }

        public void Borrar(int id)
        {
            string query = "UPDATE ciudades SET borrado = getdate() WHERE id=" + id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        public void Borrar(Ciudad oCiudad)
        {
            this.Borrar(oCiudad.Id);
        }

        public void Restituir(int id)
        {
            string query = "UPDATE ciudades SET borrado = NULL WHERE id=" + id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        public void Editar(Ciudad oCiudad)
        {
            string query = "UPDATE ciudades SET nombre = '" + oCiudad.Nombre + "', provincia_id=" + oCiudad.Provincia.Id + " WHERE id=" + oCiudad.Id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        public Ciudad BuscarPorId(int id)
        {
            Ciudad devolver = null;
            string query = "SELECT TOP 1 * FROM ciudades WHERE id = " + id.ToString();

            DataTable dt = _conexion.TraerDatos(query);
            if (dt.Rows.Count > 0)
            {
                devolver = ArmarObjeto(dt.Rows[0]);
            }

            return devolver;
        }

        public List<Ciudad> TraerTodos()
        {
            List<Ciudad> devolver = new List<Ciudad>();
            string query = "SELECT * FROM ciudades ORDER BY borrado ASC, nombre ASC";
            DataTable dt = _conexion.TraerDatos(query);

            foreach (DataRow dr in dt.Rows)
            {
                devolver.Add(ArmarObjeto(dr));
            }

            return devolver;
        }

        private Ciudad ArmarObjeto(DataRow dr)
        {
            DateTime dt;
            Provincia oProvincia;

            if (dr["borrado"] is DBNull)
                dr["borrado"] = "9/9/9999";

            DateTime.TryParse(dr["borrado"].ToString(), out dt);
            oProvincia = provincias.BuscarPorId((int) dr["provincia_id"]);
            
            return new Ciudad((string)dr["nombre"], oProvincia, (int)dr["id"], dt);
        }

        public Ciudad BuscarPorNombre(string nombre)
        {
            Ciudad devolver = null;
            string query = "SELECT TOP 1 * FROM ciudades WHERE nombre = '" + nombre + "'";

            DataTable dt = _conexion.TraerDatos(query);
            if (dt.Rows.Count > 0)
            {
                devolver = ArmarObjeto(dt.Rows[0]);
            }

            return devolver;
        }
    }
}

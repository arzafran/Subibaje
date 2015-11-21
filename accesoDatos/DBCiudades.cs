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
    public class DBCiudades : IcapaDato<Ciudad>
    {
        private DBProvincias _provincias = new DBProvincias();
        private DBConnector _conexion = new DBConnector();

        /// <summary>
        /// Guarda una ciudad en la DB
        /// </summary>
        /// <param name="oCiudad">Objeto ciudad a guardar</param>
        
        public void Agregar(Ciudad oCiudad)
        {
            string query = "INSERT INTO ciudades (nombre, provincia_id) VALUES ('" + oCiudad.Nombre + "', " + oCiudad.Provincia.Id + ")";
            _conexion.EjecutarNonSql(query);
        }

        /// <summary>
        /// Marca un registro de la DB como borrado aplicando un timestamp
        /// </summary>
        /// <param name="id">ID del registro a desactivar</param>
        
        public void Desactivar(int id)
        {
            string query = "UPDATE ciudades SET borrado = getdate() WHERE id=" + id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        /// <summary>
        /// Marca un registro de la DB como borrado aplicando un timestamp
        /// </summary>
        /// <param name="oCiudad">Objeto a desactivar</param>

        public void Desactivar(Ciudad oCiudad)
        {
            this.Desactivar(oCiudad.Id);
        }

        /// <summary>
        /// Reactiva un registro de la DB
        /// </summary>
        /// <param name="id">ID del registro a activar</param>

        public void Reactivar(int id)
        {
            string query = "UPDATE ciudades SET borrado = NULL WHERE id=" + id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        /// <summary>
        /// Edita un objeto y lo persiste en la DB
        /// </summary>
        /// <param name="oCiudad">Ciudad a ser editada y persistida</param>

        public void Editar(Ciudad oCiudad)
        {
            string query = "UPDATE ciudades SET nombre = '" + oCiudad.Nombre + "', provincia_id=" + oCiudad.Provincia.Id + " WHERE id=" + oCiudad.Id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        /// <summary>
        /// Busca una ciudad en la DB con el id especificado
        /// </summary>
        /// <param name="id">ID de la ciudad a buscar</param>
        /// <returns>Devuelve un objeto ciudad o null cuando no encuentra registro.</returns>

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

        /// <summary>
        /// Busca todas las ciudades de la DB
        /// </summary>
        /// <returns>Devuelve una lista de ciudades</returns>

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

        /// <summary>
        /// Busca todas las ciudades actvivas de la DB
        /// </summary>
        /// <returns>Devuelve una lista de ciudades</returns>

        public List<Ciudad> TraerActivos()
        {
            List<Ciudad> devolver = new List<Ciudad>();
            string query = "SELECT * FROM ciudades WHERE borrado IS NULL ORDER BY nombre ASC";
            DataTable dt = _conexion.TraerDatos(query);

            foreach (DataRow dr in dt.Rows)
            {
                devolver.Add(ArmarObjeto(dr));
            }

            return devolver;
        }

        /// <summary>
        /// Busca todas las ciudades con el nombre especificado
        /// </summary>
        /// <param name="nombre">Nombre a buscar en la DB</param>
        /// <returns>Devuelve una lista de objetos ciudad.</returns>

        public List<Ciudad> BuscarPorNombre(string nombre)
        {
            List<Ciudad> devolver = new List<Ciudad>();
            string query = "SELECT * FROM ciudades WHERE nombre = '" + nombre + "'";

            DataTable dt = _conexion.TraerDatos(query);
            foreach (DataRow dr in dt.Rows)
            {
                devolver.Add(ArmarObjeto(dr));
            }

            return devolver;
        }

        /// <summary>
        /// Genera un objeto ciudad en base a un registro de la DB
        /// </summary>
        /// <param name="dr">Datarow de un datatable</param>
        /// <returns>Devuelve un objeto ciudad</returns>

        private Ciudad ArmarObjeto(DataRow dr)
        {
            DateTime dt;
            Provincia oProvincia;

            if (dr["borrado"] is DBNull)
                dr["borrado"] = "9/9/9999";

            DateTime.TryParse(dr["borrado"].ToString(), out dt);
            oProvincia = _provincias.BuscarPorId((int)dr["provincia_id"]);

            return new Ciudad((string)dr["nombre"], oProvincia, (int)dr["id"], dt);
        }
    }
}

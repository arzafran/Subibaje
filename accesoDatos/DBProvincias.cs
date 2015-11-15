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
    public class DBProvincias : IcapaDato<Provincia>
    {
        private DBConnector _conexion = new DBConnector();

        /// <summary>
        /// Guarda una provincia en la DB
        /// </summary>
        /// <param name="oProvincia">Objeto provincia a guardar</param>
        
        public void Agregar(Provincia oProvincia)
        {
            string query = "INSERT INTO provincias (nombre) VALUES ('" + oProvincia.Nombre + "')";
            _conexion.EjecutarNonSql(query);
        }

        /// <summary>
        /// Marca un registro de la DB como borrado aplicando un timestamp
        /// </summary>
        /// <param name="id">ID del registro a desactivar</param>
        
        public void Desactivar(int id)
        {
            string query = "UPDATE provincias SET borrado = getdate() WHERE id=" + id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        /// <summary>
        /// Marca un registro de la DB como borrado aplicando un timestamp
        /// </summary>
        /// <param name="oProvincia">Objeto a desactivar</param>

        public void Desactivar(Provincia oProvincia)
        {
            this.Desactivar(oProvincia.Id);
        }

        /// <summary>
        /// Reactiva un registro de la DB
        /// </summary>
        /// <param name="id">ID del registro a activar</param>

        public void Reactivar(int id)
        {
            string query = "UPDATE provincias SET borrado = NULL WHERE id=" + id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        /// <summary>
        /// Edita un objeto y lo persiste en la DB
        /// </summary>
        /// <param name="oProvincia">Provincia a ser editada y persistida</param>

        public void Editar(Provincia oProvincia)
        {
            string query = "UPDATE provincias SET nombre = '" + oProvincia.Nombre + "' WHERE id=" + oProvincia.Id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        /// <summary>
        /// Busca una provincia en la DB con el id especificado
        /// </summary>
        /// <param name="id">ID de la provincia a buscar</param>
        /// <returns>Devuelve un objeto provincia o null cuando no encuentra registro.</returns>

        public Provincia BuscarPorId(int id) 
        {
            Provincia devolver = null;
            string query = "SELECT TOP 1 * FROM provincias WHERE id = " + id.ToString();

            DataTable dt = _conexion.TraerDatos(query);
            if (dt.Rows.Count > 0)
            {
                devolver = this.ArmarObjeto(dt.Rows[0]);
            }

            return devolver;
        }

        /// <summary>
        /// Busca todas las provincias de la DB
        /// </summary>
        /// <returns>Devuelve una lista de provincias</returns>

        public List<Provincia> TraerTodos()
        {
            List<Provincia> devolver = new List<Provincia>();
            string query = "SELECT * FROM provincias ORDER BY borrado ASC, nombre ASC";
            DataTable dt = _conexion.TraerDatos(query);

            foreach (DataRow dr in dt.Rows)
            {
                devolver.Add(this.ArmarObjeto(dr));
            }

            return devolver;
        }

        /// <summary>
        /// Busca todas las provincias activas de la DB
        /// </summary>
        /// <returns>Devuelve una lista de provincias</returns>

        public List<Provincia> TraerActivos()
        {
            List<Provincia> devolver = new List<Provincia>();
            string query = "SELECT * FROM provincias WHERE borrado IS NULL ORDER BY nombre ASC";
            DataTable dt = _conexion.TraerDatos(query);

            foreach (DataRow dr in dt.Rows)
            {
                devolver.Add(this.ArmarObjeto(dr));
            }

            return devolver;
        }

        /// <summary>
        /// Busca todas las provincias con el nombre especificado
        /// </summary>
        /// <param name="nombre">Nombre a buscar en la DB</param>
        /// <returns>Devuelve una lista de objetos provincia.</returns>

        public Provincia BuscarPorNombre(string nombre)
        {
            Provincia devolver = null;
            string query = "SELECT TOP 1 * FROM provincias WHERE nombre = '" + nombre + "'";

            DataTable dt = _conexion.TraerDatos(query);
            if (dt.Rows.Count > 0)
            {
                devolver = this.ArmarObjeto(dt.Rows[0]);
            }

            return devolver;
        }

        /// <summary>
        /// Genera un objeto provincia en base a un registro de la DB
        /// </summary>
        /// <param name="dr">Datarow de un datatable</param>
        /// <returns>Devuelve un objeto provincia</returns>

        private Provincia ArmarObjeto(DataRow dr)
        {
            DateTime dt;

            if (dr["borrado"] is DBNull)
                dr["borrado"] = "9/9/9999";

            DateTime.TryParse(dr["borrado"].ToString(), out dt);

            return new Provincia((string)dr["nombre"], (int)dr["id"], dt);
        }
    }
}

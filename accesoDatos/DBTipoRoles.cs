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
    public class DBTipoRoles : IcapaDato<TipoRol>
    {
        private DBConnector _conexion = new DBConnector();

        /// <summary>
        /// Guarda un rol en la DB
        /// </summary>
        /// <param name="oRol">Objeto rol a guardar</param>

        public void Agregar(TipoRol oRol)
        {
            string query = "INSERT INTO tipos (nombre) VALUES ('" + oRol.Nombre + "')";
            _conexion.EjecutarNonSql(query);
        }

        /// <summary>
        /// Marca un registro de la DB como borrado aplicando un timestamp
        /// </summary>
        /// <param name="id">ID del registro a desactivar</param>

        public void Desactivar(int id)
        {
            string query = "UPDATE tipos SET borrado = getdate() WHERE id=" + id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        /// <summary>
        /// Marca un registro de la DB como borrado aplicando un timestamp
        /// </summary>
        /// <param name="oRol">Objeto a desactivar</param>

        public void Desactivar(TipoRol oRol)
        {
            this.Desactivar(oRol.Id);
        }

        /// <summary>
        /// Reactiva un registro de la DB
        /// </summary>
        /// <param name="id">ID del registro a activar</param>

        public void Reactivar(int id)
        {
            string query = "UPDATE tipos SET borrado = NULL WHERE id=" + id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        /// <summary>
        /// Edita un objeto y lo persiste en la DB
        /// </summary>
        /// <param name="oRol">Rol a ser editado y persistido</param>

        public void Editar(TipoRol oRol)
        {
            string query = "UPDATE tipos SET nombre = '" + oRol.Nombre + "' WHERE id=" + oRol.Id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        /// <summary>
        /// Busca un rol en la DB con el id especificado
        /// </summary>
        /// <param name="id">ID del rol a buscar</param>
        /// <returns>Devuelve un objeto rol o null cuando no encuentra registro.</returns>

        public TipoRol BuscarPorId(int id)
        {
            TipoRol devolver = null;
            string query = "SELECT TOP 1 * FROM tipos WHERE id = " + id.ToString();

            DataTable dt = _conexion.TraerDatos(query);
            if (dt.Rows.Count > 0)
            {
                devolver = this.ArmarObjeto(dt.Rows[0]);
            }

            return devolver;
        }

        /// <summary>
        /// Busca todos los roles de la DB
        /// </summary>
        /// <returns>Devuelve una lista de roles</returns>

        public List<TipoRol> TraerTodos()
        {
            List<TipoRol> devolver = new List<TipoRol>();
            string query = "SELECT * FROM tipos ORDER BY borrado ASC, nombre ASC";
            DataTable dt = _conexion.TraerDatos(query);

            foreach (DataRow dr in dt.Rows)
            {
                devolver.Add(this.ArmarObjeto(dr));
            }

            return devolver;
        }

        /// <summary>
        /// Busca todos los roles activos de la DB
        /// </summary>
        /// <returns>Devuelve una lista de roles</returns>

        public List<TipoRol> TraerActivos()
        {
            List<TipoRol> devolver = new List<TipoRol>();
            string query = "SELECT * FROM tipos WHERE borrado IS NULL ORDER BY nombre ASC";
            DataTable dt = _conexion.TraerDatos(query);

            foreach (DataRow dr in dt.Rows)
            {
                devolver.Add(this.ArmarObjeto(dr));
            }

            return devolver;
        }

        /// <summary>
        /// Busca un rol con el nombre especificado
        /// </summary>
        /// <param name="nombre">Nombre a buscar en la DB</param>
        /// <returns>Devuelve un objeto rol.</returns>

        public TipoRol BuscarPorNombre(string nombre)
        {
            TipoRol devolver = null;
            string query = "SELECT TOP 1 * FROM tipos WHERE nombre = '" + nombre + "'";

            DataTable dt = _conexion.TraerDatos(query);
            if (dt.Rows.Count > 0)
            {
                devolver = this.ArmarObjeto(dt.Rows[0]);
            }

            return devolver;
        }

        /// <summary>
        /// Genera un objeto rol en base a un registro de la DB
        /// </summary>
        /// <param name="dr">Datarow de un datatable</param>
        /// <returns>Devuelve un objeto rol</returns>

        private TipoRol ArmarObjeto(DataRow dr)
        {
            DateTime dt;

            if (dr["borrado"] is DBNull)
                dr["borrado"] = "9/9/9999";

            DateTime.TryParse(dr["borrado"].ToString(), out dt);

            return new TipoRol((string)dr["nombre"], (int)dr["id"], dt);
        }
    }
}

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
    public class DBUsuarios : IcapaDato<Usuario>
    {
        private DBConnector _conexion = new DBConnector();

        /// <summary>
        /// Guarda un usuario en la DB
        /// </summary>
        /// <param name="oUsuario">Objeto usuario a guardar</param>

        public void Agregar(Usuario oUsuario)
        {
            string query = "INSERT INTO usuarios (nombre, dni, email, password)" +
                           "VALUES ('" + oUsuario.Nombre + "', '" + oUsuario.Dni + "', '" + oUsuario.Email + "', '" + oUsuario.Password +"')";
            _conexion.EjecutarNonSql(query);
        }

        /// <summary>
        /// Marca un registro de la DB como borrado aplicando un timestamp
        /// </summary>
        /// <param name="id">ID del registro a desactivar</param>

        public void Desactivar(int id)
        {
            string query = "UPDATE usuarios SET borrado = getdate() WHERE id=" + id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        /// <summary>
        /// Marca un registro de la DB como borrado aplicando un timestamp
        /// </summary>
        /// <param name="oUsuario">Objeto a desactivar</param>

        public void Desactivar(Usuario oUsuario)
        {
            this.Desactivar(oUsuario.Id);
        }

        /// <summary>
        /// Reactiva un registro de la DB
        /// </summary>
        /// <param name="id">ID del registro a activar</param>

        public void Reactivar(int id)
        {
            string query = "UPDATE usuarios SET borrado = NULL WHERE id=" + id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        /// <summary>
        /// Edita un objeto y lo persiste en la DB
        /// </summary>
        /// <param name="oUsuario">Usuario a ser editado y persistido</param>

        public void Editar(Usuario oUsuario)
        {
            string query = "UPDATE usuarios SET nombre = '" + oUsuario.Nombre + "', email = '" + oUsuario.Email + "', dni=" + oUsuario.Dni + " WHERE id=" + oUsuario.Id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        /// <summary>
        /// Busca un usuario en la DB con el id especificado
        /// </summary>
        /// <param name="id">ID del usuario a buscar</param>
        /// <returns>Devuelve un objeto usuario o null cuando no encuentra registro.</returns>

        public Usuario BuscarPorId(int id)
        {
            Usuario devolver = null;
            string query = "SELECT TOP 1 * FROM usuarios WHERE id = " + id.ToString();

            DataTable dt = _conexion.TraerDatos(query);
            if (dt.Rows.Count > 0)
            {
                devolver = ArmarObjeto(dt.Rows[0]);
            }

            return devolver;
        }

        /// <summary>
        /// Busca un usuario en la DB con el dni especificado
        /// </summary>
        /// <param name="dni">DNI del usuario a buscar</param>
        /// <returns>Devuelve un objeto usuario o null cuando no encuentra registro.</returns>

        public Usuario BuscarPorDni(int dni)
        {
            Usuario devolver = null;
            string query = "SELECT TOP 1 * FROM usuarios WHERE dni = " + dni.ToString();

            DataTable dt = _conexion.TraerDatos(query);
            if (dt.Rows.Count > 0)
            {
                devolver = ArmarObjeto(dt.Rows[0]);
            }

            return devolver;
        }

        /// <summary>
        /// Busca un usuario en la DB con el dni especificado
        /// </summary>
        /// <param name="dni">DNI del usuario a buscar</param>
        /// <returns>Devuelve un objeto usuario o null cuando no encuentra registro.</returns>

        public Usuario BuscarPorEmail(string email)
        {
            Usuario devolver = null;
            string query = "SELECT TOP 1 * FROM usuarios WHERE email = '" + email + "'";

            DataTable dt = _conexion.TraerDatos(query);
            if (dt.Rows.Count > 0)
            {
                devolver = ArmarObjeto(dt.Rows[0]);
            }

            return devolver;
        }

        /// <summary>
        /// Busca todos los usuarios de la DB
        /// </summary>
        /// <returns>Devuelve una lista de usuarios</returns>

        public List<Usuario> TraerTodos()
        {
            List<Usuario> devolver = new List<Usuario>();
            string query = "SELECT * FROM usuarios ORDER BY borrado ASC, nombre ASC";
            DataTable dt = _conexion.TraerDatos(query);

            foreach (DataRow dr in dt.Rows)
            {
                devolver.Add(ArmarObjeto(dr));
            }

            return devolver;
        }

        /// <summary>
        /// Busca todos los usuarios actvivos de la DB
        /// </summary>
        /// <returns>Devuelve una lista de usuarios</returns>

        public List<Usuario> TraerActivos()
        {
            List<Usuario> devolver = new List<Usuario>();
            string query = "SELECT * FROM usuarios WHERE borrado IS NULL ORDER BY nombre ASC";
            DataTable dt = _conexion.TraerDatos(query);

            foreach (DataRow dr in dt.Rows)
            {
                devolver.Add(ArmarObjeto(dr));
            }

            return devolver;
        }

        /// <summary>
        /// Busca todos los usuarios con el nombre especificado
        /// </summary>
        /// <param name="nombre">Nombre a buscar en la DB</param>
        /// <returns>Devuelve una lista de objetos usuario.</returns>

        public List<Usuario> BuscarPorNombre(string nombre)
        {
            List<Usuario> devolver = new List<Usuario>();
            string query = "SELECT * FROM usuarios WHERE nombre = '" + nombre + "'";

            DataTable dt = _conexion.TraerDatos(query);
            foreach (DataRow dr in dt.Rows)
            {
                devolver.Add(ArmarObjeto(dr));
            }

            return devolver;
        }

        /// <summary>
        /// Busca un usuario con el email y dni especificados
        /// </summary>
        /// <param name="dni">DNI a buscar en la DB</param>
        /// <param name="email">email a buscar en la DB</param>
        /// <returns>Devuelve un objeto usuario o null si no encuentra registro</returns>

        public List<Usuario> BuscarAnteriores(string dni, string email)
        {
            List<Usuario> devolver = new List<Usuario>();
            string query = "SELECT TOP 1 * FROM usuarios WHERE dni = " + Convert.ToInt32(dni) + " OR email = '" + email + "'";

            DataTable dt = _conexion.TraerDatos(query);

            foreach (DataRow dr in dt.Rows)
            {
                devolver.Add(ArmarObjeto(dr));
            }

            return devolver;
        }

        /// <summary>
        /// Genera un objeto usuario en base a un registro de la DB
        /// </summary>
        /// <param name="dr">Datarow de un datatable</param>
        /// <returns>Devuelve un objeto usuario</returns>

        private Usuario ArmarObjeto(DataRow dr)
        {
            DateTime dt;

            if (dr["borrado"] is DBNull)
                dr["borrado"] = "9/9/9999";

            DateTime.TryParse(dr["borrado"].ToString(), out dt);

            return new Usuario((string)dr["nombre"], (int)dr["dni"], (string)dr["email"], (int)dr["id"], dt);
        }
    }
}

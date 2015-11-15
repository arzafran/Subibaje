using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using accesoDatos;
using Modelos;

namespace Controladoras
{
    public class ControlAltaUsuarios
    {
        private DBUsuarios _usuarios = new DBUsuarios();

        /// <summary>
        /// Crea un objeto usuario y lo guarda en la DB
        /// </summary>
        /// <param name="nombre">Nombre del usuario</param>
        /// <param name="dni">DNI del usuario</param>
        /// <param name="email">email del usuario</param>

        public void Nuevo(string nombre, int dni, string email)
        {
            if (_usuarios.BuscarPorDni(dni) != null)
                throw new Exception("Ya existe un usuario con ese dni");

            if (_usuarios.BuscarPorEmail(email) != null)
                throw new Exception("Ya existe usuario con ese email");

            Usuario oUsuario = new Usuario(nombre, Convert.ToInt32(dni), email);
            _usuarios.Agregar(oUsuario);
        }

        /// <summary>
        /// Edita el registro del usuario especificado
        /// </summary>
        /// <param name="nombre">Nombre del usuario</param>
        /// <param name="id">ID del usuario a editar</param>
        /// <param name="dni">DNI del usuario</param>
        /// <param name="email">Email del usuario</param>

        public void Editar(string nombre, int dni, string email, int id)
        {
            Usuario previo,
                    oUsuario = _usuarios.BuscarPorId(id);

            if (oUsuario == null)
                throw new Exception("No existe usuario con ese id");

            previo = _usuarios.BuscarPorEmail(email);

            if (previo != null && previo.Id != id)
                throw new Exception("Ya existe usuario con ese email");

            previo = _usuarios.BuscarPorDni(dni);

            if (previo != null && previo.Id != id)
                throw new Exception("Ya existe usuario con ese DNI");

            oUsuario.Nombre = nombre;
            oUsuario.Email = email;
            oUsuario.Dni = dni;
            _usuarios.Editar(oUsuario);
        }

        /// <summary>
        /// Busca todos los usuarios
        /// </summary>
        /// <returns>Devuelve un lista de objetos usuario</returns>

        public List<Usuario> TraerTodos()
        {
            return _usuarios.TraerTodos();
        }

        /// <summary>
        /// Busca todos los usuarios activos
        /// </summary>
        /// <returns>Devuelve un lista de objetos usuario</returns>

        public List<Usuario> TraerActivos()
        {
            return _usuarios.TraerActivos();
        }

        /// <summary>
        /// Marca como borrado el usuario especificado
        /// </summary>
        /// <param name="id">ID del usuario a desactivar</param>

        public void Desactivar(int id)
        {
            Usuario oUsuario = _usuarios.BuscarPorId(id);

            if (oUsuario == null)
                throw new Exception("No existe usuario con ese ID");

            _usuarios.Desactivar(id);
        }

        /// <summary>
        /// Marca como activo el usuario especificado
        /// </summary>
        /// <param name="id">ID del usuario a activar</param>

        public void Reactivar(int id)
        {
            Usuario oUsuario = _usuarios.BuscarPorId(id);

            if (oUsuario == null)
                throw new Exception("No existe usuario con ese ID");

            _usuarios.Reactivar(id);
        }

        /// <summary>
        /// Busca el usuario con el ID especificado
        /// </summary>
        /// <param name="id">ID del usuario a buscar</param>
        /// <returns>Devuelve un objeto rol</returns>

        public Usuario BuscarPorId(int id)
        {
            Usuario oUsuario = _usuarios.BuscarPorId(id);

            if (oUsuario == null)
                throw new Exception("No existe usuario con ese ID");

            return oUsuario;
        }
    }
}

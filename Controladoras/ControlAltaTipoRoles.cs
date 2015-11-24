using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using accesoDatos;
using Modelos;

namespace Controladoras
{
    public class ControlAltaTipoRoles
    {
        private DBTipoRoles _roles = new DBTipoRoles();
        public ControlPermisos permisos = new ControlPermisos();

        /// <summary>
        /// Crea un objeto rol y lo guarda en la DB
        /// </summary>
        /// <param name="nombre">Nombre del rol</param>

        public void Nuevo(string nombre)
        {
            if (_roles.BuscarPorNombre(nombre) != null)
                throw new Exception("Ya existe un rol con ese nombre");

            TipoRol oRol = new TipoRol(nombre);
            _roles.Agregar(oRol);
        }

        /// <summary>
        /// Edita el registro del rol especificada
        /// </summary>
        /// <param name="nombre">Nombre del rol</param>
        /// <param name="id">ID del rol a editar</param>

        public void Editar(string nombre, int id)
        {
            TipoRol oRol = _roles.BuscarPorId(id),
                      previo = _roles.BuscarPorNombre(nombre);

            if (previo != null)
                throw new Exception("Otro rol ya uso ese nombre");

            if (oRol == null)
                throw new Exception("No existe rol con ese ID");

            oRol.Nombre = nombre;
            _roles.Editar(oRol);
        }

        /// <summary>
        /// Busca todos los roles
        /// </summary>
        /// <returns>Devuelve un lista de objetos rol</returns>

        public List<TipoRol> TraerTodos()
        {
            return _roles.TraerTodos();
        }

        /// <summary>
        /// Busca todos los roles activos
        /// </summary>
        /// <returns>Devuelve un lista de objetos rol</returns>

        public List<TipoRol> TraerActivos()
        {
            return _roles.TraerActivos();
        }

        /// <summary>
        /// Marca como borrado el rol especificado
        /// </summary>
        /// <param name="id">ID del rol a desactivar</param>

        public void Desactivar(int id)
        {
            TipoRol oRol = _roles.BuscarPorId(id);

            if (oRol == null)
                throw new Exception("No existe rol con ese ID");

            _roles.Desactivar(id);
        }

        /// <summary>
        /// Marca como activo el rol especificado
        /// </summary>
        /// <param name="id">ID del rol a activar</param>

        public void Reactivar(int id)
        {
            TipoRol oRol = _roles.BuscarPorId(id);

            if (oRol == null)
                throw new Exception("No existe rol con ese ID");

            _roles.Reactivar(id);
        }

        /// <summary>
        /// Busca el rol con el ID especificado
        /// </summary>
        /// <param name="id">ID del rol a buscar</param>
        /// <returns>Devuelve un objeto rol</returns>

        public TipoRol BuscarPorId(int id)
        {
            TipoRol oRol = _roles.BuscarPorId(id);

            if (oRol == null)
                throw new Exception("No existe rol con ese ID");

            return oRol;
        }

        /// <summary>
        /// Busca el rol con el nombre especificado
        /// </summary>
        /// <param name="nombre">Nombre del rol a buscar</param>
        /// <returns>Devuelve un objeto rol</returns>

        public TipoRol BuscarPorNombre(string nombre)
        {
            return _roles.BuscarPorNombre(nombre);
        }
    }
}

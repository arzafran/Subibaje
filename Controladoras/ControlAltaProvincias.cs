using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using accesoDatos;
using Modelos;

namespace Controladoras
{
    public class ControlAltaProvincias
    {
        private DBProvincias _provincias = new DBProvincias();
        public ControlPermisos permisos = new ControlPermisos();
        
        /// <summary>
        /// Crea un objeto provincia y lo guarda en la DB
        /// </summary>
        /// <param name="nombre">Nombre de la provincia</param>

        public void Nuevo(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
                throw new Exception("El nombre no puede estar vacío");
            if (_provincias.BuscarPorNombre(nombre) != null)
                throw new Exception("Ya existe una provincia con ese nombre");
            
            Provincia oProvincia = new Provincia(nombre);
            _provincias.Agregar(oProvincia);
        }

        /// <summary>
        /// Edita el registro de la provincia especificada
        /// </summary>
        /// <param name="nombre">Nombre de la provincia</param>
        /// <param name="id">ID de la provincia a editar</param>

        public void Editar(string nombre, int id)
        {
            Provincia oProvincia = _provincias.BuscarPorId(id),
                      previa = _provincias.BuscarPorNombre(nombre);

            if (previa != null)
                throw new Exception("Otra provincia ya uso ese nombre");

            if (oProvincia == null)
                throw new Exception("No existe provincia con ese ID");

            oProvincia.Nombre = nombre;
            _provincias.Editar(oProvincia);
        }

        /// <summary>
        /// Busca todas las provincias
        /// </summary>
        /// <returns>Devuelve un lista de objetos provincia</returns>

        public List<Provincia> TraerTodos()
        {
            return _provincias.TraerTodos();
        }

        /// <summary>
        /// Busca todas las provincias activas
        /// </summary>
        /// <returns>Devuelve un lista de objetos provincia</returns>

        public List<Provincia> TraerActivos()
        {
            return _provincias.TraerActivos();
        }

        /// <summary>
        /// Marca como borrada la provincia especificada
        /// </summary>
        /// <param name="id">ID de la provincia a desactivar</param>

        public void Desactivar(int id)
        {
            Provincia oProvincia = _provincias.BuscarPorId(id);

            if (oProvincia == null)
                throw new Exception("No existe provincia con ese ID");

            _provincias.Desactivar(id);
        }

        /// <summary>
        /// Marca como activa la provincia especificada
        /// </summary>
        /// <param name="id">ID de la provincia a activar</param>

        public void Reactivar(int id)
        {
            Provincia oProvincia = _provincias.BuscarPorId(id);

            if (oProvincia == null)
                throw new Exception("No existe provincia con ese ID");

            _provincias.Reactivar(id);
        }

        /// <summary>
        /// Busca la provincia con el ID especificado
        /// </summary>
        /// <param name="id">ID de la provincia a buscar</param>
        /// <returns>Devuelve un objeto provincia</returns>

        public Provincia BuscarPorId(int id)
        {
            Provincia oProvincia = _provincias.BuscarPorId(id);

            if (oProvincia == null)
                throw new Exception("No existe provincia con ese ID");

            return oProvincia;
        }
    }
}

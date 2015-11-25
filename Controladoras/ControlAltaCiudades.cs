using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using accesoDatos;
using Modelos;

namespace Controladoras
{
    public class ControlAltaCiudades
    {
        private DBCiudades _ciudades = new DBCiudades();
        public ControlAltaProvincias provincias = new ControlAltaProvincias();
        public ControlPermisos permisos = new ControlPermisos();

        /// <summary>
        /// Crea un nuevo objeto ciudad y lo guarda en la DB.
        /// </summary>
        /// <param name="nombre">Nombre de la ciudad</param>
        /// <param name="provincia_id">ID de la provincia asociada</param>
        
        public void Nuevo(string nombre, int provincia_id)
        {
            if(String.IsNullOrEmpty(nombre))
                throw new Exception("El nombre no puede estar vacío");

            Provincia oProvincia = provincias.BuscarPorId(provincia_id);
            Ciudad oCiudad,
                   previa = _ciudades.BuscarPorNombre(nombre).Find(p => p.Provincia.Id == provincia_id);

            if (previa != null)
                throw new Exception("Otra ciudad en esa provincia ya uso ese nombre");

            if (oProvincia == null)
                throw new Exception("No existe provincia con ese ID");

            if (DateTime.Compare(oProvincia.Borrado, DateTime.Now) < 0)
                throw new Exception("La provincia está desactivada");

            oCiudad = new Ciudad(nombre, oProvincia);
            _ciudades.Agregar(oCiudad);
        }

        /// <summary>
        /// Edita el registro de la ciudad especificada
        /// </summary>
        /// <param name="nombre">Nombre de la ciudad</param>
        /// <param name="id">ID de la ciudad a editar</param>
        /// <param name="provincia_id">ID de la provincia asociada</param>

        public void Editar(string nombre, int id, int provincia_id)
        {
            if (String.IsNullOrEmpty(nombre))
                throw new Exception("El nombre no puede estar vacío");

            Provincia oProvincia = provincias.BuscarPorId(provincia_id);
            Ciudad oCiudad = _ciudades.BuscarPorId(id),
                    previa = _ciudades.BuscarPorNombre(nombre).Find(p => p.Provincia.Id == provincia_id);

            if (previa != null)
                throw new Exception("Otra ciudad en esa provincia ya uso ese nombre");

            if (oCiudad == null)
                throw new Exception("No existe ciudad con ese ID");

            if (oProvincia == null)
                throw new Exception("No existe provincia con ese ID");

            oCiudad.Nombre = nombre;
            oCiudad.Provincia = oProvincia;
            _ciudades.Editar(oCiudad);
        }

        /// <summary>
        /// Busca todas las ciudades
        /// </summary>
        /// <returns>Devuelve un lista de objetos ciudad</returns>

        public List<Ciudad> TraerTodos()
        {
            return _ciudades.TraerTodos();
        }

        /// <summary>
        /// Busca todas las ciudades activas
        /// </summary>
        /// <returns>Devuelve un lista de objetos ciudad</returns>

        public List<Ciudad> TraerActivos()
        {
            return _ciudades.TraerActivos();
        }

        /// <summary>
        /// Marca como borrada la ciudad especificada
        /// </summary>
        /// <param name="id">ID de la ciudad a desactivar</param>

        public void Desactivar(int id)
        {
            Ciudad oCiudad = _ciudades.BuscarPorId(id);

            if (oCiudad == null)
                throw new Exception("No existe ciudad con ese ID");

            _ciudades.Desactivar(id);
        }

        /// <summary>
        /// Marca como activa la ciudad especificada
        /// </summary>
        /// <param name="id">ID de la ciudad a activar</param>

        public void Reactivar(int id)
        {
            Ciudad oCiudad = _ciudades.BuscarPorId(id);
            
            if (oCiudad == null)
                throw new Exception("No existe ciudad con ese ID");

            if (DateTime.Compare(oCiudad.Provincia.Borrado, DateTime.Now) < 0)
                throw new Exception("No se puede activar una ciudad cuya provincia está desactivada");

            _ciudades.Reactivar(id);
        }

        /// <summary>
        /// Busca la ciudad con el ID especificado
        /// </summary>
        /// <param name="id">ID de la ciudad a buscar</param>
        /// <returns>Devuelve un objeto ciudad</returns>

        public Ciudad BuscarPorId(int id)
        {
            Ciudad oCiudad = _ciudades.BuscarPorId(id);

            if (oCiudad == null)
                throw new Exception("No existe ciudad con ese ID");
            
            return oCiudad;
        }
    }
}

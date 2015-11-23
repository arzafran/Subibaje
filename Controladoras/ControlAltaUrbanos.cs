using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using accesoDatos;
using Modelos;

namespace Controladoras
{
    public class ControlAltaUrbanos
    {
        private DBUrbanos _urbanos = new DBUrbanos();
        public ControlAltaCiudades ciudades = new ControlAltaCiudades();
        public ControlPermisos permisos = new ControlPermisos();

        /// <summary>
        /// Crea un nuevo objeto urbano y lo persiste
        /// </summary>
        /// <param name="linea">Nombre de la linea</param>
        /// <param name="ciudad_id">ID de la ciudad a la que pertenece el colectivo urbano</param>

        public void Nuevo(string linea, int ciudad_id)
        {
            Ciudad oCiudad = ciudades.BuscarPorId(ciudad_id);
            Urbano previo = _urbanos.BuscarPorNombre(linea).Find(p => p.Ciudad.Id == ciudad_id),
                   oUrbano;

            if (oCiudad == null)
                throw new Exception("No existe ciudad con ese ID");

            if (previo != null)
                throw new Exception("Otra linea en esa ciudad ya uso esa identificación");

            oUrbano = new Urbano(linea, oCiudad);
            _urbanos.Agregar(oUrbano);
        }

        /// <summary>
        /// Edita el registro del colectivo urbano especificado
        /// </summary>
        /// <param name="linea">Nombre de la linea</param>
        /// <param name="id">ID del registro a editar</param>
        /// <param name="ciudad_id">ID de la ciudad a la que pertenece el colectivo urbano</param>

        public void Editar(string linea, int id, int ciudad_id)
        {
            Ciudad oCiudad = ciudades.BuscarPorId(ciudad_id);
            Urbano oUrbano = _urbanos.BuscarPorId(id),
                   previo = _urbanos.BuscarPorNombre(linea).Find(p => p.Ciudad.Id == ciudad_id);

            if (previo != null)
                throw new Exception("Otra linea en esa ciudad ya uso esa identificación");

            if (oUrbano == null)
                throw new Exception("No existe linea con ese ID");

            if (oCiudad == null)
                throw new Exception("No existe ciudad con ese ID");

            oUrbano.Linea = linea;
            oUrbano.Ciudad = oCiudad;
            _urbanos.Editar(oUrbano);

        }

        /// <summary>
        /// Busca todos los colectivos urbanos
        /// </summary>
        /// <returns>Devuelve una lista de urbanos</returns>

        public List<Urbano> TraerTodos()
        {
            return _urbanos.TraerTodos();
        }

        /// <summary>
        /// Marca como borrado el colectivo urbano especificado
        /// </summary>
        /// <param name="id">ID del colectivo urbano a borrar</param>

        public void Desactivar(int id)
        {
            Urbano oUrbano = _urbanos.BuscarPorId(id);

            if (oUrbano == null)
                throw new Exception("No existe linea con ese ID");

            _urbanos.Desactivar(id);
        }

        /// <summary>
        /// Marca como activo el colectivo urbano especificado
        /// </summary>
        /// <param name="id">ID del establecimiento a activar</param>

        public void Reactivar(int id)
        {
            Urbano oUrbano = _urbanos.BuscarPorId(id);

            if (oUrbano == null)
                throw new Exception("No existe linea con ese ID");

            if (DateTime.Compare(oUrbano.Ciudad.Borrado, DateTime.Now) < 0)
                throw new Exception("No se puede activar un colectivo urbano cuya ciudad está desactivada");

            _urbanos.Reactivar(id);
        }

        /// <summary>
        /// Busca el colectivo urbano con el ID especificado
        /// </summary>
        /// <param name="id">ID del colectivo urbano a buscar</param>
        /// <returns>Devuelve un objeto urbano</returns>

        public Urbano BuscarPorId(int id)
        {
            Urbano oUrbano = _urbanos.BuscarPorId(id);

            if (oUrbano == null)
                throw new Exception("No existe linea con ese ID");

            return oUrbano;
        }

        /// <summary>
        /// Busca el colectivo urbano activo con el ID especificado
        /// </summary>
        /// <param name="id">ID del colectivo urbano a buscar</param>
        /// <returns>Devuelve un objeto urbano</returns>

        public Urbano BuscarPorIdActivo(int id)
        {
            Urbano oUrbano = _urbanos.BuscarPorIdActivo(id);

            if (oUrbano == null)
                throw new Exception("No existe linea activa con ese ID");

            return oUrbano;
        }
    }
}

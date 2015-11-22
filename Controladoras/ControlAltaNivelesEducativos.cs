using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using accesoDatos;
using Modelos;

namespace Controladoras
{
    public class ControlAltaNivelesEducativos
    {
        private DBNivelesEducativos _niveles = new DBNivelesEducativos();
        public ControlPermisos permisos = new ControlPermisos();

        /// <summary>
        /// Crea un objeto nivel educativo y lo guarda en la DB
        /// </summary>
        /// <param name="nombre">Nombre del nivel educativo</param>

        public void Nuevo(string nombre)
        {
            if (_niveles.BuscarPorNombre(nombre) != null)
                throw new Exception("Ya existe un nivel educativo con ese nombre");

            NivelEducativo oNivel = new NivelEducativo(nombre);
            _niveles.Agregar(oNivel);
        }

        /// <summary>
        /// Edita el registro del nivel educativo especificado
        /// </summary>
        /// <param name="nombre">Nombre del nivel educativo</param>
        /// <param name="id">ID del nivel educativo a editar</param>

        public void Editar(string nombre, int id)
        {
            NivelEducativo oNivel = _niveles.BuscarPorId(id),
                           previo = _niveles.BuscarPorNombre(nombre);

            if (previo != null)
                throw new Exception("Ya existe un nivel educativo con ese nombre");

            if (oNivel == null)
                throw new Exception("No existe nivel educativo con ese ID");

            oNivel.Nombre = nombre;
            _niveles.Editar(oNivel);
        }

        /// <summary>
        /// Busca todos los niveles educativos
        /// </summary>
        /// <returns>Devuelve un lista de objetos nivel educativo</returns>

        public List<NivelEducativo> TraerTodos()
        {
            return _niveles.TraerTodos();
        }

        /// <summary>
        /// Busca todos los niveles educativos
        /// </summary>
        /// <returns>Devuelve un lista de objetos nivel educativo</returns>

        public List<NivelEducativo> TraerActivos()
        {
            return _niveles.TraerActivos();
        }

        /// <summary>
        /// Busca los niveles educativos con id incluido en la lista especificada
        /// </summary>
        /// <param name="ids">Lista de ids a buscar</param>
        /// <returns>Devuelve un listado de objetos nivel educativo</returns>

        public List<NivelEducativo> Traer(List<int> ids)
        {
            return _niveles.Traer(ids);
        }

        /// <summary>
        /// Marca como borrado el nivel educativo especificado
        /// </summary>
        /// <param name="id">ID del nivel educativo a desactivar</param>

        public void Desactivar(int id)
        {
            NivelEducativo oNivel = _niveles.BuscarPorId(id);

            if (oNivel == null)
                throw new Exception("No existe nivel educativo con ese ID");

            _niveles.Desactivar(id);
        }

        /// <summary>
        /// Marca como activo el nivel educativo especificado
        /// </summary>
        /// <param name="id">ID del nivel educativo a activar</param>

        public void Reactivar(int id)
        {
            NivelEducativo oNivel = _niveles.BuscarPorId(id);

            if (oNivel == null)
                throw new Exception("No existe nivel educativo con ese ID");

            _niveles.Reactivar(id);
        }

        /// <summary>
        /// Buscael nivel educativo con el ID especificado
        /// </summary>
        /// <param name="id">ID del nivel educativo a buscar</param>
        /// <returns>Devuelve un objeto nivel educativo</returns>

        public NivelEducativo BuscarPorId(int id)
        {
            NivelEducativo oNivel = _niveles.BuscarPorId(id);

            if (oNivel == null)
                throw new Exception("No existe nivel educativo con ese ID");

            return oNivel;
        }
    }
}

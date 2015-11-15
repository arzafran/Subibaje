using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using accesoDatos;
using Modelos;

namespace Controladoras
{
    public class ControlAltaEstablecimientos
    {
        private DBEstablecimientos _establecimientos = new DBEstablecimientos();
        public DBCiudades ciudades = new DBCiudades();
        public DBNivelesEducativos niveles = new DBNivelesEducativos();

        /// <summary>
        /// Crea un nuevo objeto establecimiento y lo guarda en la DB.
        /// </summary>
        /// <param name="nombre">Nombre del establecimiento</param>
        /// <param name="ciudad_id">ID de la ciudad a la que pertenece el establecimiento</param>
        /// <param name="listaNiveles">Lista de IDs de nivees asociados al establecimiento</param>

        public void Nuevo(string nombre, int ciudad_id, List<int> listaNiveles)
        {
            NivelEducativo oNivel;
            Establecimiento previo = _establecimientos.BuscarPorNombre(nombre).Find(p => p.Ciudad.Id == ciudad_id);
            Ciudad oCiudad = ciudades.BuscarPorId(ciudad_id);
            List<NivelEducativo> nivelesDb = new List<NivelEducativo>();

            if (previo != null)
                throw new Exception("Ya existe establecimiento en esa ciudad con ese nombre");

            if (oCiudad == null)
                throw new Exception("No existe ciudad con ese ID");

            foreach (int id in listaNiveles)
            {
                oNivel = niveles.BuscarPorId(id);
                if (oNivel == null)
                    throw new Exception("No existe nivel con ese ID");

                if (DateTime.Compare(oNivel.Borrado, DateTime.Now) < 0)
                    throw new Exception("El nivel está desactivado, no se puede asociar");

                nivelesDb.Add(niveles.BuscarPorId(id));
            }
            
            Establecimiento oEstablecimiento = new Establecimiento(nombre, oCiudad, nivelesDb);
            _establecimientos.Agregar(oEstablecimiento);
        }

        /// <summary>
        /// Edita el registro del establecimiento especificado
        /// </summary>
        /// <param name="nombre">Nombre del establecimiento</param>
        /// /// <param name="id">ID del establecimiento a editar</param>
        /// <param name="ciudad_id">ID de la ciudad a la que pertenece el establecimiento</param>
        /// <param name="listaNiveles">Lista de IDs de nivees asociados al establecimiento</param>

        public void Editar(string nombre, int id, int ciudad_id, List<int> listaNiveles)
        {
            Ciudad oCiudad = ciudades.BuscarPorId(ciudad_id);
            Establecimiento oEstablecimiento = _establecimientos.BuscarPorId(id),
                    previo = _establecimientos.BuscarPorNombre(nombre).Find(p => p.Ciudad.Id == ciudad_id);

            if (previo != null && id != previo.Id)
                throw new Exception("Otro establecimiento en esa ciudad ya uso ese nombre");

            if (oEstablecimiento == null)
                throw new Exception("No existe ciudad con ese ID");

            if (oCiudad == null)
                throw new Exception("No existe provincia con ese ID");

            oEstablecimiento.Nombre = nombre;
            oEstablecimiento.Ciudad = oCiudad;
            oEstablecimiento.ListaNiveles = niveles.Traer(listaNiveles);
            _establecimientos.Editar(oEstablecimiento);
        }

        /// <summary>
        /// Busca todos los establecimientos
        /// </summary>
        /// <returns>Devuelve una lista de establecimientos</returns>

        public List<Establecimiento> TraerTodos()
        {
            return _establecimientos.TraerTodos();
        }

        /// <summary>
        /// Busca todos los establecimientos activos
        /// </summary>
        /// <returns>Devuelve una lista de establecimientos</returns>

        public List<Establecimiento> TraerActivos()
        {
            return _establecimientos.TraerActivos();
        }

        /// <summary>
        /// Marca como borrado el establecimiento especificado
        /// </summary>
        /// <param name="id">ID del establecimiento a borrar</param>

        public void Borrar(int id)
        {
            Establecimiento oEstablecimiento = _establecimientos.BuscarPorId(id);

            if (oEstablecimiento == null)
                throw new Exception("No existe establecimiento con ese ID");

            _establecimientos.Desactivar(id);
        }

        /// <summary>
        /// Marca como activo el establecimiento especificado
        /// </summary>
        /// <param name="id">ID del establecimiento a activar</param>

        public void Restituir(int id)
        {
            Establecimiento oEstablecimiento = _establecimientos.BuscarPorId(id);

            if (oEstablecimiento == null)
                throw new Exception("No existe establecimiento con ese ID");

            if (DateTime.Compare(oEstablecimiento.Ciudad.Borrado, DateTime.Now) < 0)
                throw new Exception("No se puede activar un establecimiento cuya ciudad está desactivada");

            _establecimientos.Reactivar(id);
        }

        /// <summary>
        /// Busca el establecimiento con el ID especificado
        /// </summary>
        /// <param name="id">ID del establecimiento a buscar</param>
        /// <returns>Devuelve un objeto establecimiento</returns>

        public Establecimiento BuscarPorId(int id)
        {
            return _establecimientos.BuscarPorId(id);
        }

    }
}

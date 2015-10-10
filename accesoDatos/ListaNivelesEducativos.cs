using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modelos;

namespace accesoDatos
{
    public class ListaNivelesEducativos : CapaDatosAbstractaSingleton<ListaNivelesEducativos>, IcapaDato<NivelEducativo>
    {
        static private List<NivelEducativo> lista = new List<NivelEducativo>();

        private int buscarUltimoId()
        {
            int devolver = 0;

            if (lista.Count > 0)
            {
                devolver = lista.Last().Id;
            }
            return devolver;
        }

        public void Agregar(NivelEducativo oNivel)
        {
            if (BuscarPorDescripcion(oNivel.Descripcion) is NivelEducativo)
            {
                throw new Exception("El nivel educativo ya se encuentra registrado.");
            }
            oNivel.Id = 1 + buscarUltimoId();
            lista.Add(oNivel);
        }

        public void Borrar(NivelEducativo oNivel)
        {
            throw new NotImplementedException();
        }

        public void Editar(NivelEducativo oNivel)
        {
            throw new NotImplementedException();
        }

        public NivelEducativo BuscarPorId(int id)
        {
            return (NivelEducativo)lista.Find(u => u.Id == id);
        }

        public List<NivelEducativo> TraerTodos()
        {
            return lista;
        }

        /**
         * Fuera de la interface
         **/

        public NivelEducativo BuscarPorDescripcion(string descripcion)
        {
            return (NivelEducativo)lista.Find(u => u.Descripcion == descripcion);
        }
    }
}
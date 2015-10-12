using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modelos;

namespace accesoDatos
{
    public class ListaUrbanos : CapaDatosAbstractaSingleton<ListaUrbanos>, IcapaDato<Urbano>
    {
        static private List<Urbano> lista = new List<Urbano>();

        private int buscarUltimoId()
        {
            int devolver = 0;

            if (lista.Count > 0)
            {
                devolver = lista.Last().Id;
            }
            return devolver;
        }

        public void Agregar(Urbano oUrbano)
        {
            if (BuscarPorNombre(oUrbano.Linea) is Urbano)
            {
                throw new Exception("La linea ya se encuentra registrada.");
            }
            oUrbano.Id = 1 + buscarUltimoId();
            lista.Add(oUrbano);
        }

        public void Borrar(Urbano oUrbano)
        {
            throw new NotImplementedException();
        }

        public void Editar(Urbano oUrbano)
        {
            throw new NotImplementedException();
        }

        public Urbano BuscarPorId(int id)
        {
            return (Urbano)lista.Find(u => u.Id == id);
        }

        public List<Urbano> TraerTodos()
        {
            return lista;
        }

        /**
         * Fuera de la interface
         **/

        public Urbano BuscarPorNombre(string linea)
        {
            return (Urbano)lista.Find(u => u.Linea == linea);
        }
    }
}
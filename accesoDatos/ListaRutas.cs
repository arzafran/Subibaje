using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modelos;

namespace accesoDatos
{
    public class ListaRutas : CapaDatosAbstractaSingleton<ListaRutas>, IcapaDato<Ruta>
    {
        static private List<Ruta> lista = new List<Ruta>();

        private int buscarUltimoId()
        {
            int devolver = 0;

            if (lista.Count > 0)
            {
                devolver = lista.Last().Id;
            }
            return devolver;
        }

        public void Agregar(Ruta oRuta)
        {
            if (BuscarPorNombre(oRuta.Nombre) is Ruta)
            {
                throw new Exception("La ruta ya se encuentra registrada.");
            }
            oRuta.Id = 1 + buscarUltimoId();
            lista.Add(oRuta);
        }

        public void Borrar(Ruta oRuta)
        {
            throw new NotImplementedException();
        }

        public void Editar(Ruta oRuta)
        {
            throw new NotImplementedException();
        }

        public Ruta BuscarPorId(int id)
        {
            return (Ruta)lista.Find(u => u.Id == id);
        }

        public List<Ruta> TraerTodos()
        {
            return lista;
        }

        /**
         * Fuera de la interface
         **/

        public Ruta BuscarPorNombre(string nombre)
        {
            return (Ruta)lista.Find(u => u.Nombre == nombre);
        }
    }
}
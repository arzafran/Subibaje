using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modelos;

namespace accesoDatos
{
    public class ListaCiudades :CapaDatosAbstractaSingleton<ListaCiudades>, IcapaDato<Ciudad>
    {
        static private List<Ciudad> lista = new List<Ciudad>();

        private int buscarUltimoId()
        {
            int devolver = 0;

            if (lista.Count > 0)
            {
                devolver = lista.Last().Id;
            }
            return devolver;
        }

        public void Agregar(Ciudad oCiudad)
        {
            if (BuscarPorNombre(oCiudad.Nombre) is Ciudad)
            {
                throw new Exception("La ciudad ya se encuentra registrada.");
            }
            oCiudad.Id = 1 + buscarUltimoId();
            lista.Add(oCiudad);
        }

        public void Borrar(Ciudad oCiudad)
        {
            throw new NotImplementedException();
        }

        public void Editar(Ciudad oCiudad)
        {
            throw new NotImplementedException();
        }

        public Ciudad BuscarPorId(int id)
        {
            return (Ciudad)lista.Find(u => u.Id == id);
        }

        public List<Ciudad> TraerTodos()
        {
            return lista;
        }

        /**
         * Fuera de la interface
         **/

        public Ciudad BuscarPorNombre(string nombre)
        {
            return (Ciudad)lista.Find(c => c.Nombre == nombre);
        }
    }
}

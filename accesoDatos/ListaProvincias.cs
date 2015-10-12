using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modelos;

namespace accesoDatos
{
    public class ListaProvincias : CapaDatosAbstractaSingleton<ListaProvincias>, IcapaDato<Provincia>
    {
        static private List<Provincia> lista = new List<Provincia>();

        private int buscarUltimoId()
        {
            int devolver = 0;

            if (lista.Count > 0)
            {
                devolver = lista.Last().Id;
            }
            return devolver;
        }

        public void Agregar(Provincia oProvincia)
        {
            if (BuscarPorNombre(oProvincia.Nombre) is Provincia)
            {
                throw new Exception("La provincia  ya se encuentra registrada.");
            }
            oProvincia.Id = 1 + buscarUltimoId();
            lista.Add(oProvincia);
        }

        public void Borrar(Provincia oProvincia)
        {
            throw new NotImplementedException();
        }

        public void Editar(Provincia oProvincia)
        {
            throw new NotImplementedException();
        }

        public Provincia BuscarPorId(int id)
        {
            return (Provincia)lista.Find(u => u.Id == id);
        }

        public List<Provincia> TraerTodos()
        {
            return lista;
        }

        /**
         * Fuera de la interface
         **/

        public Provincia BuscarPorNombre(string nombre)
        {
            return (Provincia)lista.Find(c => c.Nombre == nombre);
        }
    }
}

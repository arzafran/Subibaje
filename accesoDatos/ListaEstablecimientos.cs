using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modelos;

namespace accesoDatos
{
    public class ListaEstablecimientos : CapaDatosAbstractaSingleton<ListaEstablecimientos>, IcapaDato<Establecimiento>
    {
        static private List<Establecimiento> lista = new List<Establecimiento>();

        private int buscarUltimoId()
        {
            int devolver = 0;

            if (lista.Count > 0)
            {
                devolver = lista.Last().Id;
            }
            return devolver;
        }

        public void Agregar(Establecimiento oEstablecimiento)
        {
            if (BuscarPorNombre(oEstablecimiento.Nombre) is Establecimiento)
            {
                throw new Exception("El establecimiento ya se encuentra registrado.");
            }
            oEstablecimiento.Id = 1 + buscarUltimoId();
            lista.Add(oEstablecimiento);
        }

        public void Borrar(Establecimiento oNivel)
        {
            throw new NotImplementedException();
        }

        public void Editar(Establecimiento oNivel)
        {
            throw new NotImplementedException();
        }

        public Establecimiento BuscarPorId(int id)
        {
            return (Establecimiento)lista.Find(u => u.Id == id);
        }

        public List<Establecimiento> TraerTodos()
        {
            return lista;
        }

        /**
         * Fuera de la interface
         **/

        public Establecimiento BuscarPorNombre(string nombre)
        {
            return (Establecimiento)lista.Find(u => u.Nombre == nombre);
        }
    }
}
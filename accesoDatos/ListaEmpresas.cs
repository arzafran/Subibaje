using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modelos;

namespace accesoDatos
{
    public class ListaEmpresas : CapaDatosAbstractaSingleton<ListaEmpresas>, IcapaDato<Empresa>
    {
        static private List<Empresa> lista = new List<Empresa>();

        private int buscarUltimoId()
        {
            int devolver = 0;

            if (lista.Count > 0)
            {
                devolver = lista.Last().Id;
            }
            return devolver;
        }

        public void Agregar(Empresa oEmpresa)
        {
            if (BuscarPorNombre(oEmpresa.Nombre) is Empresa)
            {
                throw new Exception("La empresa ya se encuentra registrada.");
            }
            oEmpresa.Id = 1 + buscarUltimoId();
            lista.Add(oEmpresa);
        }

        public void Borrar(Empresa oEmpresa)
        {
            throw new NotImplementedException();
        }

        public void Editar(Empresa oEmpresa)
        {
            throw new NotImplementedException();
        }

        public Empresa BuscarPorId(int id)
        {
            return (Empresa)lista.Find(u => u.Id == id);
        }

        public List<Empresa> TraerTodos()
        {
            return lista;
        }

        /**
         * Fuera de la interface
         **/

        public Empresa BuscarPorNombre(string nombre)
        {
            return (Empresa)lista.Find(u => u.Nombre == nombre);
        }
    }
}
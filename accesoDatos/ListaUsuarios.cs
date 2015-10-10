using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modelos;

namespace accesoDatos
{
    public class ListaUsuarios : CapaDatosAbstractaSingleton<ListaUsuarios>, IcapaDato<Usuario>
    {
        static private List<Usuario> lista = new List<Usuario>();

        private int buscarUltimoId()
        {
            int devolver = 0;

            if (lista.Count > 0)
            {
                devolver = lista.Last().Id;
            }
            return devolver;
        }

        public void Agregar(Usuario oUser)
        {
            if (BuscarPorId(oUser.Dni) is Usuario)
            {
                throw new Exception("El DNI " + oUser.Dni.ToString() + " ya se encuentra registrado.");   
            }
            oUser.Id = 1 + buscarUltimoId();
            lista.Add(oUser);
        }

        public void Borrar(Usuario oUser)
        {
            throw new NotImplementedException();
        }

        public void Editar(Usuario oUser)
        {
            throw new NotImplementedException();
        }

        public Usuario BuscarPorId(int id)
        {
            return (Usuario)lista.Find(u => u.Dni == id);
        }

        public List<Usuario> TraerTodos()
        {
            return lista;
        }
    }
}
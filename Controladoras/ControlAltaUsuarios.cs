using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using accesoDatos;
using Modelos;

namespace Controladoras
{
    public class ControlAltaUsuarios
    {
        private ListaUsuarios DatosUsuario = ListaUsuarios.Instance();

        public void Nuevo(string nombre, string password, string mail, int dni)
        {

            Usuario oUsuario = new Usuario(nombre, password, mail, dni);
            DatosUsuario.Agregar(oUsuario);

        }

        public List<Usuario> TraerTodos()
        {
            //return Lista_Usuarios.Mostrar_Todo();
            return DatosUsuario.TraerTodos();
        }
    }
}

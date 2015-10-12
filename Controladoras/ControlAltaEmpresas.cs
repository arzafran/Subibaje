using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using accesoDatos;
using Modelos;

namespace Controladoras
{
    public class ControlAltaEmpresas
    {
        private ListaEmpresas DatosEmpresa = ListaEmpresas.Instance();

        public void Nuevo(string nombre)
        {
            Empresa oEmpresa = new Empresa(nombre);
            DatosEmpresa.Agregar(oEmpresa);
        }

        public List<Empresa> TraerTodos()
        {
            //return Lista_Usuarios.Mostrar_Todo();
            return DatosEmpresa.TraerTodos();
        }
    }
}

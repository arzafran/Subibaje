using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using accesoDatos;
using Modelos;

namespace Controladoras
{
    public class ControlAltaProvincias
    {
        private ListaProvincias DatosProvincia = ListaProvincias.Instance();

        public void Nuevo(string nombre)
        {
            Provincia oProvincia = new Provincia(nombre);
            DatosProvincia.Agregar(oProvincia);
        }

        public List<Provincia> TraerTodos()
        {
            //return Lista_Usuarios.Mostrar_Todo();
            return DatosProvincia.TraerTodos();
        }
    }
}

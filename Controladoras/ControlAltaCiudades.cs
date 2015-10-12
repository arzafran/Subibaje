using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using accesoDatos;
using Modelos;

namespace Controladoras
{
    public class ControlAltaCiudades
    {
        private ListaCiudades DatosCiudades = ListaCiudades.Instance();

        public void Nuevo(string nombre)
        {
            Ciudad oCiudad = new Ciudad(nombre);
            DatosCiudades.Agregar(oCiudad);
        }

        public List<Ciudad> TraerTodos()
        {
            //return Lista_Usuarios.Mostrar_Todo();
            return DatosCiudades.TraerTodos();
        }
    }
}

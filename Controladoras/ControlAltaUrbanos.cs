using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using accesoDatos;
using Modelos;

namespace Controladoras
{
    public class ControlAltaUrbanos
    {
        private ListaUrbanos DatosUrbano = ListaUrbanos.Instance();
        public ListaCiudades ciudades = ListaCiudades.Instance();

        public void Nuevo(string linea, int idCiudad)
        {
            Ciudad oCiudad = ciudades.BuscarPorId(idCiudad);
            Urbano oUrbano = new Urbano(linea, oCiudad);
            DatosUrbano.Agregar(oUrbano);
        }

        public List<Urbano> TraerTodos()
        {
            //return Lista_Usuarios.Mostrar_Todo();
            return DatosUrbano.TraerTodos();
        }
    }
}

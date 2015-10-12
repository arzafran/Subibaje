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

        public void Nuevo(string linea)
        {
            Urbano oUrbano = new Urbano(linea);
            DatosUrbano.Agregar(oUrbano);
        }

        public List<Urbano> TraerTodos()
        {
            //return Lista_Usuarios.Mostrar_Todo();
            return DatosUrbano.TraerTodos();
        }
    }
}

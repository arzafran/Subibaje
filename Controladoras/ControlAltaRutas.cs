using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using accesoDatos;
using Modelos;

namespace Controladoras
{
    public class ControlAltaRutas
    {
        private ListaRutas DatosRuta = ListaRutas.Instance();
        public ListaCiudades ciudades = ListaCiudades.Instance();

        public void Nuevo(string nombre, int idOrigen, int idDestino)
        {
            Ciudad origen = ciudades.BuscarPorId(idOrigen);
            Ciudad destino = ciudades.BuscarPorId(idDestino);
            Ruta oRuta = new Ruta(nombre, origen, destino);
            DatosRuta.Agregar(oRuta);
        }

        public List<Ruta> TraerTodos()
        {
            //return Lista_Usuarios.Mostrar_Todo();
            return DatosRuta.TraerTodos();
        }
    }
}

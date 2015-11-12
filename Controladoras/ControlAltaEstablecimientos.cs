using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using accesoDatos;
using Modelos;

namespace Controladoras
{
    public class ControlAltaEstablecimientos
    {
        public DBEstablecimientos DatosEstablecimientos = new DBEstablecimientos();
        /*private ListaEstablecimientos DatosEstablecimiento = ListaEstablecimientos.Instance();
        public ListaNivelesEducativos niveles = ListaNivelesEducativos.Instance();
        public ListaCiudades ciudades = ListaCiudades.Instance();*/

        public void Nuevo(string nombre, int ciudad_id, List<int> listaNiveles)
        {
            List<NivelEducativo> _niveles = new List<NivelEducativo>();
            foreach (int id in listaNiveles)
            { 
                _niveles.Add(DatosEstablecimientos.niveles.BuscarPorId(id));
            }
            Ciudad _ciudad = DatosEstablecimientos.ciudades.BuscarPorId(ciudad_id);
            Establecimiento oEstablecimiento = new Establecimiento(nombre, _ciudad, _niveles);
            DatosEstablecimientos.Agregar(oEstablecimiento);
        }

        public List<Establecimiento> TraerTodos()
        {
            return DatosEstablecimientos.TraerTodos();
        }

        public void Borrar(int id)
        {
            DatosEstablecimientos.Borrar(id);
        }

        public void Restituir(int id)
        {
            DatosEstablecimientos.Restituir(id);
        }

        public Establecimiento BuscarPorId(int id)
        {
            return DatosEstablecimientos.BuscarPorId(id);
        }

    }
}

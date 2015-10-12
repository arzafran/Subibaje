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
        private ListaEstablecimientos DatosEstablecimiento = ListaEstablecimientos.Instance();
        public ListaNivelesEducativos niveles = ListaNivelesEducativos.Instance();
        public ListaCiudades ciudades = ListaCiudades.Instance();

        public void Nuevo(string descripcion, int idCiudad, List<int> listaIds)
        {
            List<NivelEducativo> _niveles = new List<NivelEducativo>();
            foreach (int id in listaIds)
            { 
                _niveles.Add(niveles.BuscarPorId(id));
            }
            Ciudad _ciudad = ciudades.BuscarPorId(idCiudad);
            Establecimiento oEstablecimiento = new Establecimiento(descripcion, _ciudad, _niveles);
            DatosEstablecimiento.Agregar(oEstablecimiento);
        }

        public List<Establecimiento> TraerTodos()
        {
            //return Lista_Usuarios.Mostrar_Todo();
            return DatosEstablecimiento.TraerTodos();
        }
    }
}

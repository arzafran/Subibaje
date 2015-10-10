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

        public void Nuevo(string descripcion, List<int> levels)
        {
            List<NivelEducativo> _niveles = null;
            levels.ForEach(delegate(int c)
                {
                    _niveles.Add(niveles.BuscarPorId(c));
                }
            );
            Establecimiento oNivel = new Establecimiento(descripcion);
            oNivel.Niveles = _niveles;
            DatosEstablecimiento.Agregar(oNivel);
        }

        public List<Establecimiento> TraerTodos()
        {
            //return Lista_Usuarios.Mostrar_Todo();
            return DatosEstablecimiento.TraerTodos();
        }
    }
}

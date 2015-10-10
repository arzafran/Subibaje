using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using accesoDatos;
using Modelos;

namespace Controladoras
{
    public class ControlAltaNivelesEducativos
    {
        private ListaNivelesEducativos DatosNivelEducativo = ListaNivelesEducativos.Instance();

        public void Nuevo(string descripcion)
        {
            NivelEducativo oNivel = new NivelEducativo(descripcion);
            DatosNivelEducativo.Agregar(oNivel);
        }

        public List<NivelEducativo> TraerTodos()
        {
            //return Lista_Usuarios.Mostrar_Todo();
            return DatosNivelEducativo.TraerTodos();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelos
{
    public class Establecimiento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Niveles 
        {
            get 
            {
                string devolver = "";
                foreach (NivelEducativo oNivel in ListaNiveles)
                {
                    devolver += oNivel.Descripcion + " ";
                }
                return devolver;
            }
        }
        public List<NivelEducativo> ListaNiveles { get; set; }

        public Establecimiento(string nombre, List<NivelEducativo> niveles)
        {
            Nombre = nombre;
            ListaNiveles = niveles;
        }
    }
}

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
        public List<NivelEducativo> Niveles { get; set; }

        public Establecimiento(string nombre)
        {
            Nombre = nombre;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelos
{
    public class Urbano
    {
        public int Id { get; set; }
        public string Linea { get; set; }
        public Ciudad Ciudad { get; set; }
        
        public Urbano(string linea, Ciudad ciudad)
        {
            Linea = linea;
            Ciudad = ciudad;
        }
    }
}

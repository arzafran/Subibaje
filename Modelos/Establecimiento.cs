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
        public Ciudad Ciudad { get; set; }
        public DateTime Borrado { get; set; }
        public string Niveles 
        {
            get 
            {
                string devolver = "";
                foreach (NivelEducativo oNivel in ListaNiveles)
                {
                    devolver += oNivel.Nombre + " ";
                }
                return devolver;
            }
        }

        public string NombreCompleto
        {
            get
            {
                return this.ToString();
            }
            
        }

        public override string ToString()
        {
            return this.Nombre + " (" + this.Ciudad.Nombre + ")";
        }
        
        public List<NivelEducativo> ListaNiveles { get; set; }
        
        public Establecimiento(string nombre, Ciudad ciudad, List<NivelEducativo> niveles)
        {
            Nombre = nombre;
            ListaNiveles = niveles;
            Ciudad = ciudad;
        }

        public Establecimiento(string nombre, Ciudad ciudad, List<NivelEducativo> niveles, int id, DateTime borrado)
        {
            Nombre = nombre;
            Id = id;
            ListaNiveles = niveles;
            Ciudad = ciudad;
            Borrado = borrado;
        }

    }
}

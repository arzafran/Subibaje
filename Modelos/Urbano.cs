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
        public DateTime Borrado { get; set; }
        
        public override string ToString()
        {
            return this.Linea;
        }

        public Urbano(string nombre, Ciudad ciudad)
        {
            Linea = nombre;
            Ciudad = ciudad;
        }

        public Urbano(string nombre, int id, DateTime borrado)
        {
            Id = id;
            Linea = nombre;
            Borrado = borrado;
        }

        public Urbano(string nombre, Ciudad ciudad, int id, DateTime borrado)
        {
            Id = id;
            Linea = nombre;
            Ciudad = ciudad;
            Borrado = borrado;
        }
    }
}

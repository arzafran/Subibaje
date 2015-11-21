using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelos
{
    public class Provincia
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime Borrado { get; set; }

        public override string ToString()
        {
            return this.Nombre;
        }
        
        public Provincia(string nombre)
        {
            Nombre = nombre;
        }
        
        public Provincia(string nombre, int id, DateTime borrado)
        {
            Nombre = nombre;
            Id = id;
            Borrado = borrado;
        }
    }
}

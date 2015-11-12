using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelos
{
    public class Ciudad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Provincia Provincia { get; set; }
        public DateTime Borrado { get; set; }

        public override string ToString()
        {
            return this.Nombre;
        }

        public Ciudad(string nombre, Provincia provincia)
        {
            Nombre = nombre;
            Provincia = provincia;
        }

        public Ciudad(string nombre, Provincia provincia, int id)
        {
            Id = id;
            Nombre = nombre;
            Provincia = provincia;
        }

        public Ciudad(string nombre, Provincia provincia, int id, DateTime borrado)
        {
            Id = id;
            Nombre = nombre;
            Provincia = provincia;
            Borrado = borrado;
        }
    }
}

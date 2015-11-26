using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelos
{
    public class TipoRol
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime Borrado { get; set; }
        public int Peso { get; set; }

        public override string ToString()
        {
            return this.Nombre;
        }

        public TipoRol(string nombre)
        {
            Nombre = nombre;
        }

        public TipoRol(string nombre, int id, DateTime borrado, int peso)
        {
            Nombre = nombre;
            Id = id;
            Borrado = borrado;
            Peso = peso;
        }
    }
}

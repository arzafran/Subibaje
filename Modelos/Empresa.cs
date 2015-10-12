using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelos
{
    public class Empresa
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public Empresa(string nombre)
        {
            Nombre = nombre;
        }
    }
}

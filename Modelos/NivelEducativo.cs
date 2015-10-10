using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelos
{
    public class NivelEducativo
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public NivelEducativo(string descripcion)
        {
            Descripcion = descripcion;
        }
    }
}

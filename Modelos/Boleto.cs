using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelos
{
    public class Boleto
    {
        public int Id { get; set; }
        public Urbano Linea { get; set; }
        public Rol Rol { get; set; }
        public DateTime Fecha { get; set; }

        public Boleto(Urbano oUrbano, Rol oRol)
        {
            Linea = oUrbano;
            Rol = oRol;
        }

        public Boleto(Urbano oUrbano, Rol oRol, DateTime dt, int id)
        {
            Linea = oUrbano;
            Rol = oRol;
            Fecha = dt;
            Id = id;
        }
    }
}

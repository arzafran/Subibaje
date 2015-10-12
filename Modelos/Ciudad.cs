﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelos
{
    public class Ciudad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public override string ToString()
        {
            return this.Nombre;
        }

        public Ciudad(string nombre)
        {
            Nombre = nombre;
        }
    }
}

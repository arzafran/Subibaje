using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelos
{
    public class Estudiante
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Dni { get; set; }
        public DateTime Borrado { get; set; }
        public Establecimiento Establecimiento { get; set; }
        public NivelEducativo Nivel { get; set; }


    }
}

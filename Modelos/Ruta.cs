using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelos
{
    public class Ruta
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Ciudad Origen { get; set; }
        public Ciudad Destino { get; set; }

        public Ruta(string nombre, Ciudad origen, Ciudad destino)
        {
            Nombre = nombre;
            Origen = origen;
            Destino = destino;
        }
    }
}

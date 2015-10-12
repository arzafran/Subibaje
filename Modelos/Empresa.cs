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
        public List<Ruta> ListaRutas { get; set; }
        public string Rutas
        {
            get
            {
                string devolver = "";
                foreach (Ruta oRuta in ListaRutas)
                {
                    devolver += oRuta.Nombre + " ";
                }
                return devolver;
            }
        }

        public Empresa(string nombre, List<Ruta> rutas)
        {
            Nombre = nombre;
            ListaRutas = rutas;
        }
    }
}

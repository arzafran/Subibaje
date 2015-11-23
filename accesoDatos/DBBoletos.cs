using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modelos;

namespace accesoDatos
{
    public class DBBoletos
    {
        private DBConnector _conexion = new DBConnector();

        public int Agregar(Rol oRol, Urbano oUrbano)
        {
            string query = "INSERT INTO boletos (rol_id, urbano_id) VALUES (" + oRol.Id.ToString() + ", " + oUrbano.Id.ToString() + ")";
            return _conexion.EjecutarEscalar(query);
        }
    }
}

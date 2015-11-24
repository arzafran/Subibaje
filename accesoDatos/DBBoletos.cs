using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modelos;
using System.Data;

namespace accesoDatos
{
    public class DBBoletos
    {
        private DBConnector _conexion = new DBConnector();
        private DBRoles _roles = new DBRoles();
        private DBUrbanos _urbanos = new DBUrbanos();

        /// <summary>
        /// Agrega un nuevo boleto a la base de datos
        /// </summary>
        /// <param name="oBoleto">Objeto a persistir</param>
        /// <returns>Devuelve el ID del boleto creado</returns>

        public int Agregar(Boleto oBoleto)
        {
            string query = "INSERT INTO boletos (rol_id, urbano_id) VALUES (" + oBoleto.Rol.Id.ToString() + ", " + oBoleto.Linea.Id.ToString() + ")";
            return _conexion.EjecutarEscalar(query);
        }
        /*
        public int Agregar(Rol oRol, Urbano oUrbano)
        {
            string query = "INSERT INTO boletos (rol_id, urbano_id) VALUES (" + oRol.Id.ToString() + ", " + oUrbano.Id.ToString() + ")";
            return _conexion.EjecutarEscalar(query);
        }*/

        /// <summary>
        /// Trae todos los boletos del rol (Estudiante) especificado
        /// </summary>
        /// <param name="rol_id">ID del rol a buscar</param>
        /// <returns>Devuelve una lista de objetos boleto</returns>

        public List<Boleto> TraerBoletosEstudiante(int rol_id)
        {
            List<Boleto> devolver = new List<Boleto>();
            string query = "SELECT * FROM boletos WHERE rol_id = " + rol_id.ToString();
            DataTable dt = _conexion.TraerDatos(query);

            foreach (DataRow dr in dt.Rows)
            {
                devolver.Add(ArmarObjeto(dr));
            }
            
            return devolver;
        }

        /// <summary>
        /// Genera un objeto boleto en base a un registro de la DB
        /// </summary>
        /// <param name="dr">Datarow de un datatable</param>
        /// <returns>Devuelve un objeto boleto</returns>

        private Boleto ArmarObjeto(DataRow dr)
        {
            DateTime dt;
            Rol oRol;
            Urbano oUrbano;

            DateTime.TryParse(dr["fecha"].ToString(), out dt);
            oRol = _roles.BuscarPorId((int)dr["rol_id"]);
            oUrbano = _urbanos.BuscarPorId((int)dr["urbano_id"]);

            return new Boleto(oUrbano, oRol, dt, (int)dr["id"]);
        }
    }
}

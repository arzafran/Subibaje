using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Modelos;
using System.Data;

namespace accesoDatos
{
    public class DBEstablecimientoNivel
    {
        private DBConnector _conexion = new DBConnector();

        /// <summary>
        /// Busca un registro de establecimiento/nivel con el ID especificado
        /// </summary>
        /// <param name="id">ID a buscar</param>
        /// <returns>Devuelve una lista de enteros con los ID de establecimiento y nivel correspondientes</returns>

        public List<int> Buscar(int id)
        {
            List<int> devolver = new List<int>();
            string query = "SELECT TOP 1 * FROM establecimiento_nivel WHERE id = " + id.ToString();
            DataTable dt = _conexion.TraerDatos(query);

            if (dt.Rows.Count > 0)
            {
                devolver.Add((int)dt.Rows[0]["establecimiento_id"]);
                devolver.Add((int)dt.Rows[0]["nivel_id"]);
            }

            return devolver;
        }

        /// <summary>
        /// Busca el ID del registro correspondiente a un nivel/establecimiento
        /// </summary>
        /// <param name="establecimiento_id">ID del establecimiento</param>
        /// <param name="nivel_id">ID del nivel</param>
        /// <returns>Devuelve el ID del registro o 0 si no encuentra valor.</returns>

        public int BuscarPorParametros(int establecimiento_id, int nivel_id)
        {
            int devolver = 0;
            string query = "SELECT TOP 1 * FROM establecimiento_nivel WHERE establecimiento_id = " + establecimiento_id.ToString() + " AND nivel_id = " + nivel_id.ToString();
            DataTable dt = _conexion.TraerDatos(query);

            if (dt.Rows.Count > 0)
            {
                devolver = (int)dt.Rows[0]["id"];
            }

            return devolver;
        }
    }
}

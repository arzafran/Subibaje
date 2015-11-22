using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace accesoDatos
{
    public class DBConnector
    {
        private SqlConnection _connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=micros;Integrated Security=True");
        //private SqlConnection _connection = new SqlConnection("Data Source=TE205804\\SQLEXPRESS;Initial Catalog=micros;Integrated Security=True");
        
        /// <summary>
        /// Abre una conexion con la base de datos
        /// </summary>
        /// <exception cref="System.Exception">Lanza una excepcion en caso de que la conexion ya se encuentre abierta.</exception>

        public void Abrir()
        {
            if (this._connection.State == ConnectionState.Open)
                throw new Exception("La conexión ya se encuentra abierta");
            this._connection.Open();
        }

        /// <summary>
        /// Cierra una conexion con la base de datos
        /// </summary>
        /// <exception cref="System.Exception">Lanza una excepcion en caso de que la conexion ya se encuentre cerrada.</exception>

        public void Cerrar()
        {
            if (this._connection.State == ConnectionState.Closed)
                throw new Exception("La conexión ya se encuentra cerrada");
            this._connection.Close();
        }

        /// <summary>
        /// Ejecuta un ExecuteReader sobre la consulta especificada.
        /// </summary>
        /// <param name="query">Consulta a ejecutar</param>
        /// <returns>Devuelve un DataTable con el resultado de la consulta.</returns>

        public DataTable TraerDatos(string query)
        {
            DataTable dataTable = new DataTable();
            SqlCommand command = new SqlCommand(query, this._connection);

            this.Abrir();

            SqlDataReader reader = command.ExecuteReader();
            dataTable.Load(reader);

            this.Cerrar();

            return dataTable;
        }

        /// <summary>
        /// Ejecuta un ExecuteNonQuery sobre la consulta especificada.
        /// </summary>
        /// <param name="query">Consulta a ejecutar</param>

        public void EjecutarNonSql(string query)
        {
            SqlCommand command = new SqlCommand(query, this._connection);
            
            this.Abrir();
            command.ExecuteNonQuery();
            this.Cerrar();
        }

        /// <summary>
        /// Ejecuta un ExecuteScalar sobre la consulta especificada.
        /// </summary>
        /// <param name="query">Consulta a ejecutar.</param>
        /// <returns>Devuelve el ID de la ultima fila ingresada.</returns>

        public int EjecutarEscalar(string query)
        {
            query = query + "SELECT CAST(scope_identity() AS int)";

            SqlCommand command = new SqlCommand(query, this._connection);
            this.Abrir();
            int resultado = (int) command.ExecuteScalar();
            this.Cerrar();

            return resultado;
        }

    }
}

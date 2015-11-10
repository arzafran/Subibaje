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
        private SqlConnection _connection = new SqlConnection("Data Source=COLOVM\\SQLEXPRESS;Initial Catalog=subibaje;Integrated Security=True");

        public void Abrir()
        {
            if (this._connection.State == ConnectionState.Open)
                throw new Exception("La conexión ya se encuentra abierta");
            this._connection.Open();
        }

        public void Cerrar()
        {
            if (this._connection.State == ConnectionState.Closed)
                throw new Exception("La conexión ya se encuentra cerrada");
            this._connection.Close();
        }

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

        public void EjecutarNonSql(string query)
        {
            SqlCommand command = new SqlCommand(query, this._connection);
            
            this.Abrir();
            command.ExecuteNonQuery();
            this.Cerrar();
        }

        public object EjecutarEscalar(string query)
        {
            SqlCommand command = new SqlCommand(query, this._connection);
            this.Abrir();
            Object resultado = command.ExecuteScalar();
            this.Cerrar();
            return resultado;
        }

    }
}

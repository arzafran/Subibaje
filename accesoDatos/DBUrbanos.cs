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
    public class DBUrbanos : IcapaDato<Urbano>
    {
        private DBConnector _conexion = new DBConnector();
        public DBCiudades _ciudades = new DBCiudades();

        /// <summary>
        /// Guarda un colectivo urbano en la DB
        /// </summary>
        /// <param name="oUrbano">Objeto urbano a guardar</param>

        public void Agregar(Urbano oUrbano)
        {
            string query = "INSERT INTO urbanos (linea, ciudad_id) VALUES ('" + oUrbano.Linea + "', " + oUrbano.Ciudad.Id + ")";
            _conexion.EjecutarNonSql(query);
        }

        /// <summary>
        /// Marca un registro de la DB como borrado aplicando un timestamp
        /// </summary>
        /// <param name="id">ID del registro a desactivar</param>

        public void Desactivar(int id)
        {
            string query = "UPDATE urbanos SET borrado = getdate() WHERE id=" + id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        /// <summary>
        /// Marca un registro de la DB como borrado aplicando un timestamp
        /// </summary>
        /// <param name="oEstablecimiento">Objeto a desactivar</param>

        public void Desactivar(Urbano oUrbano)
        {
            this.Desactivar(oUrbano.Id);
        }

        /// <summary>
        /// Reactiva un registro de la DB
        /// </summary>
        /// <param name="id">ID del registro a activar</param>

        public void Reactivar(int id)
        {
            string query = "UPDATE urbanos SET borrado = NULL WHERE id=" + id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        /// <summary>
        /// Edita un objeto y lo persiste en la DB
        /// </summary>
        /// <param name="oUrbano">Colectivo urbano a ser editado y persistido</param>

        public void Editar(Urbano oUrbano)
        {
            string query = "UPDATE urbanos SET linea = '" + oUrbano.Linea + "', ciudad_id=" + oUrbano.Ciudad.Id + " WHERE id=" + oUrbano.Id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        /// <summary>
        /// Busca un colectivo urbano en la DB con el id especificado
        /// </summary>
        /// <param name="id">ID del colectivo urbano a buscar</param>
        /// <returns>Devuelve un objeto urbano o null cuando no encuentra registro.</returns>

        public Urbano BuscarPorId(int id)
        {
            Urbano oUrbano = null;
            string query = "SELECT TOP 1 * FROM urbanos WHERE id = " + id.ToString();

            DataTable dt = _conexion.TraerDatos(query);
            if (dt.Rows.Count > 0)
            {
                oUrbano = ArmarObjeto(dt.Rows[0]);
            }

            return oUrbano;
        }

        /// <summary>
        /// Busca todos los colectivos urbanos de la DB
        /// </summary>
        /// <returns>Devuelve una lista de colectivos urbanos</returns>

        public List<Urbano> TraerTodos()
        {
            List<Urbano> devolver = new List<Urbano>();
            string query = "SELECT * FROM urbanos ORDER BY borrado ASC, linea ASC";
            DataTable dt = _conexion.TraerDatos(query);

            foreach (DataRow dr in dt.Rows)
            {
                devolver.Add(ArmarObjeto(dr));
            }

            return devolver;
        }

        /// <summary>
        /// Busca todos los colectivos urbanos con el nombre de linea especificado
        /// </summary>
        /// <param name="linea">Nombre de linea a buscar en la DB</param>
        /// <returns>Devuelve una lista de objetos urbanos</returns>

        public List<Urbano> BuscarPorNombre(string linea)
        {
            List<Urbano> devolver = new List<Urbano>();
            string query = "SELECT * FROM urbanos WHERE linea = '" + linea + "'";

            DataTable dt = _conexion.TraerDatos(query);

            foreach (DataRow dr in dt.Rows)
            {
                devolver.Add(ArmarObjeto(dr));
            }

            return devolver;
        }

        /// <summary>
        /// Genera un objeto urbano en base a un registro de la DB
        /// </summary>
        /// <param name="dr">Datarow de un datatable</param>
        /// <returns>Devuelve un objeto urbano</returns>

        private Urbano ArmarObjeto(DataRow dr)
        {
            DateTime dt;
            Ciudad oCiudad;

            if (dr["borrado"] is DBNull)
                dr["borrado"] = "9/9/9999";

            DateTime.TryParse(dr["borrado"].ToString(), out dt);
            oCiudad = _ciudades.BuscarPorId((int)dr["ciudad_id"]);

            return new Urbano((string)dr["linea"], oCiudad, (int)dr["id"], dt);
        }
    }
}

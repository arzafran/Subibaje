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
    class DBEstudiantes : IcapaDato<Ciudad>
    {
        private DBConnector _conexion = new DBConnector();
        private DBEstablecimientos _establecimientos = new DBEstablecimientos();
        private DBNivelesEducativos _niveles = new DBNivelesEducativos();

        /// <summary>
        /// Guarda un estudiante en la DB
        /// </summary>
        /// <param name="oEstudiante">Objeto estudiante a guardar</param>

        public void Agregar(Estudiante oEstudiante)
        {
            string query = "INSERT INTO estudiantes (nombre, dni, email, establecimiento_id, nivel_id)" +
                           "VALUES ('" + oEstudiante.Nombre + "', '" + oEstudiante.Dni + "', '" + oEstudiante.Email + "'" +
                           ", " + oEstudiante.Establecimiento.Id + ", " + oEstudiante.Nivel.Id + ")";
            _conexion.EjecutarNonSql(query);
        }

        /// <summary>
        /// Marca un registro de la DB como borrado aplicando un timestamp
        /// </summary>
        /// <param name="id">ID del registro a desactivar</param>

        public void Desactivar(int id)
        {
            string query = "UPDATE estudiantes SET borrado = getdate() WHERE id=" + id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        /// <summary>
        /// Marca un registro de la DB como borrado aplicando un timestamp
        /// </summary>
        /// <param name="oCiudad">Objeto a desactivar</param>

        public void Desactivar(Estudiante oEstudiante)
        {
            this.Desactivar(oEstudiante.Id);
        }

        /// <summary>
        /// Reactiva un registro de la DB
        /// </summary>
        /// <param name="id">ID del registro a activar</param>

        public void Reactivar(int id)
        {
            string query = "UPDATE estudiantes SET borrado = NULL WHERE id=" + id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        /// <summary>
        /// Edita un objeto y lo persiste en la DB
        /// </summary>
        /// <param name="oEstudiante">Estudiante a ser editado y persistido</param>

        public void Editar(Estudiante oEstudiante)
        {
            //string query = "UPDATE ciudades SET nombre = '" + oCiudad.Nombre + "', provincia_id=" + oCiudad.Provincia.Id + " WHERE id=" + oCiudad.Id.ToString();
            //_conexion.EjecutarNonSql(query);
        }

        /// <summary>
        /// Busca un estudiante en la DB con el id especificado
        /// </summary>
        /// <param name="id">ID del estudiante a buscar</param>
        /// <returns>Devuelve un objeto estudiante o null cuando no encuentra registro.</returns>

        public Estudiante BuscarPorId(int id)
        {
            Estudiante devolver = null;
            string query = "SELECT TOP 1 * FROM estudiantes WHERE id = " + id.ToString();

            DataTable dt = _conexion.TraerDatos(query);
            if (dt.Rows.Count > 0)
            {
                devolver = ArmarObjeto(dt.Rows[0]);
            }

            return devolver;
        }

        /// <summary>
        /// Busca todos los estudiantes de la DB
        /// </summary>
        /// <returns>Devuelve una lista de estudiantes</returns>

        public List<Estudiante> TraerTodos()
        {
            List<Estudiante> devolver = new List<Estudiante>();
            string query = "SELECT * FROM estudiantes ORDER BY borrado ASC, nombre ASC";
            DataTable dt = _conexion.TraerDatos(query);

            foreach (DataRow dr in dt.Rows)
            {
                devolver.Add(ArmarObjeto(dr));
            }

            return devolver;
        }

        /// <summary>
        /// Busca todos los estudiantes actvivos de la DB
        /// </summary>
        /// <returns>Devuelve una lista de estudiantes</returns>

        public List<Estudiante> TraerActivos()
        {
            List<Estudiante> devolver = new List<Estudiante>();
            string query = "SELECT * FROM estudiantes WHERE borrado IS NULL ORDER BY nombre ASC";
            DataTable dt = _conexion.TraerDatos(query);

            foreach (DataRow dr in dt.Rows)
            {
                devolver.Add(ArmarObjeto(dr));
            }

            return devolver;
        }

        /// <summary>
        /// Busca todos los estudiantes con el nombre especificado
        /// </summary>
        /// <param name="nombre">Nombre a buscar en la DB</param>
        /// <returns>Devuelve una lista de objetos ciudad.</returns>

        public List<Estudiante> BuscarPorNombre(string nombre)
        {
            List<Estudiante> devolver = new List<Estudiante>();
            string query = "SELECT * FROM estudiantes WHERE nombre = '" + nombre + "'";

            DataTable dt = _conexion.TraerDatos(query);
            foreach (DataRow dr in dt.Rows)
            {
                devolver.Add(ArmarObjeto(dr));
            }

            return devolver;
        }

        /// <summary>
        /// Genera un objeto estudiante en base a un registro de la DB
        /// </summary>
        /// <param name="dr">Datarow de un datatable</param>
        /// <returns>Devuelve un objeto estudiante</returns>

        private Estudiante ArmarObjeto(DataRow dr)
        {
            /*DateTime dt;
            Provincia oProvincia;

            if (dr["borrado"] is DBNull)
                dr["borrado"] = "9/9/9999";

            DateTime.TryParse(dr["borrado"].ToString(), out dt);
            oProvincia = _provincias.BuscarPorId((int)dr["provincia_id"]);

            return new Ciudad((string)dr["nombre"], oProvincia, (int)dr["id"], dt);*/
            return new Estudiante();
        }
    }
}

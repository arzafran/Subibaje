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
    public class DBRoles
    {
        private DBConnector _conexion = new DBConnector();
        private DBUsuarios _usuarios = new DBUsuarios();
        private DBNivelesEducativos _niveles = new DBNivelesEducativos();
        private DBEstablecimientos _establecimientos = new DBEstablecimientos();
        private DBEstablecimientoNivel _establecimientos_niveles = new DBEstablecimientoNivel();
        private DBTipoRoles _tipo_roles = new DBTipoRoles();

        /// <summary>
        /// Guarda un rol en la DB
        /// </summary>
        /// <param name="oRol">Objeto rol a guardar</param>

        public void Agregar(Rol oRol)
        {
            int establecimiento_id = oRol.Establecimiento == null ? 0 : oRol.Establecimiento.Id;
            int nivel_id = oRol.Nivel == null ? 0 : oRol.Nivel.Id;
            int idCombinado = 0;

            if (establecimiento_id != 0 && nivel_id != 0)
            {
                idCombinado = _establecimientos_niveles.BuscarPorParametros(oRol.Establecimiento.Id, oRol.Nivel.Id);
                if (idCombinado == 0)
                    throw new Exception("El Establecimiento que eligió no tiene el nivel asociado");
            }

            string query = "INSERT INTO roles (tipoRol_id, usuario_id, establecimiento_nivel_id) VALUES (" + oRol.Tipo.Id.ToString() + ", " + oRol.Usuario.Id.ToString() + ", ";

            if (idCombinado == 0)
                query += "NULL)";
            else
                query += idCombinado.ToString() + ")";

            _conexion.EjecutarNonSql(query);
        }

        /// <summary>
        /// Marca un registro de la DB como borrado aplicando un timestamp
        /// </summary>
        /// <param name="id">ID del registro a desactivar</param>

        public void Desactivar(int id)
        {
            string query = "UPDATE roles SET borrado = getdate() WHERE id=" + id.ToString();
            _conexion.EjecutarNonSql(query);
        }

        /// <summary>
        /// Marca un registro de la DB como borrado aplicando un timestamp
        /// </summary>
        /// <param name="oEstablecimiento">Objeto a desactivar</param>

        public void Desactivar(Rol oRol)
        {
            this.Desactivar(oRol.Id);
        }

        /// <summary>
        /// Reactiva un registro de la DB
        /// </summary>
        /// <param name="id">ID del registro a activar</param>

        public void Reactivar(int id)
        {
            string query = "UPDATE roles SET borrado = NULL WHERE id=" + id.ToString();
            _conexion.EjecutarNonSql(query);
        }


        /// <summary>
        /// Edita un objeto y lo persiste en la DB
        /// </summary>
        /// <param name="oRol">Rol a ser editado y persistido</param>

        public void Editar(Rol oRol)
        {
            int id = oRol.Id;
            int idCombinado = _establecimientos_niveles.BuscarPorParametros(oRol.Establecimiento.Id, oRol.Nivel.Id);
            if (idCombinado == 0)
                throw new Exception("El Establecimiento que eligió no tiene el nivel asociado");
            
            string query = "UPDATE roles SET tipoRol_id = " + oRol.Tipo.Id.ToString() + ", establecimiento_id = " + idCombinado.ToString();
            _conexion.EjecutarNonSql(query);

            /*
            int id = oEstablecimiento.Id;
            string query = "UPDATE establecimientos SET nombre = '" + oEstablecimiento.Nombre + "', ciudad_id=" + oEstablecimiento.Ciudad.Id + " WHERE id=" + id.ToString();
            _conexion.EjecutarNonSql(query);
            BorrarNiveles(id);
            InsertarNiveles(id, oEstablecimiento.ListaNiveles);*/
        }

        /// <summary>
        /// Busca un rol en la DB con el id especificado
        /// </summary>
        /// <param name="id">ID del rol a buscar</param>
        /// <returns>Devuelve un objeto rol o null cuando no encuentra registro.</returns>

        public Rol BuscarPorId(int id)
        {
            Rol devolver = null;
            string query = "SELECT TOP 1 * FROM roles WHERE id = " + id.ToString();

            DataTable dt = _conexion.TraerDatos(query);
            if (dt.Rows.Count > 0)
            {
                devolver = ArmarObjeto(dt.Rows[0]);
            }

            return devolver;
        }

        /// <summary>
        /// Busca todos los roles de un usuario de la DB
        /// </summary>
        /// <param name="id">ID del usuario</param>
        /// <returns>Devuelve una lista de roles</returns>

        public List<Rol> TraerTodos(int id)
        {
            List<Rol> devolver = new List<Rol>();
            string query = "SELECT * FROM roles WHERE usuario_id = " + id.ToString() + " ORDER BY borrado ASC";
            DataTable dt = _conexion.TraerDatos(query);

            foreach (DataRow dr in dt.Rows)
            {
                devolver.Add(ArmarObjeto(dr));
            }

            return devolver;
        }

        /// <summary>
        /// Busca todos los roles activos de un usuario de la DB
        /// </summary>
        /// <param name="id">ID del usuario</param>
        /// <returns>Devuelve una lista de roles</returns>

        public List<Rol> TraerActivos(int id)
        {
            List<Rol> devolver = new List<Rol>();
            string query = "SELECT * FROM roles WHERE borrado AND usuario_id = " + id.ToString() + " IS NULL";
            DataTable dt = _conexion.TraerDatos(query);

            foreach (DataRow dr in dt.Rows)
            {
                devolver.Add(ArmarObjeto(dr));
            }

            return devolver;
        }

        /// <summary>
        /// Genera un objeto rol en base a un registro de la DB
        /// </summary>
        /// <param name="dr">Datarow de un datatable</param>
        /// <returns>Devuelve un objeto establecimiento</returns>

        private Rol ArmarObjeto(DataRow dr)
        {
            DateTime dt;
            int id = (int)dr["id"];
            Establecimiento oEstablecimiento = null;
            NivelEducativo oNivel = null;
            if (!(dr["establecimiento_nivel_id"] is DBNull))
            {
                int idCombinado = (int)dr["establecimiento_nivel_id"];
                List<int> ids = _establecimientos_niveles.Buscar(idCombinado);
                oEstablecimiento = _establecimientos.BuscarPorId(ids[0]);
                oNivel = _niveles.BuscarPorId(ids[1]);
            }
                
            
            Usuario oUsuario = _usuarios.BuscarPorId((int)dr["usuario_id"]);
            TipoRol oRol = _tipo_roles.BuscarPorId((int)dr["tipoRol_id"]);

            DateTime.TryParse(dr["borrado"].ToString(), out dt);

            if (dr["borrado"] is DBNull)
                dr["borrado"] = "9/9/9999";

            return new Rol(id, oRol, oUsuario, oEstablecimiento, oNivel, dt);
        }

    }
}

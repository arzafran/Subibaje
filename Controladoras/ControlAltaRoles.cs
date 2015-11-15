using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using accesoDatos;
using Modelos;

namespace Controladoras
{
    public class ControlAltaRoles
    {
        private DBRoles _roles = new DBRoles();
        public DBEstablecimientos establecimientos = new DBEstablecimientos();
        public DBUsuarios usuarios = new DBUsuarios();
        public DBNivelesEducativos niveles = new DBNivelesEducativos();
        public DBTipoRoles tipo_roles = new DBTipoRoles();

        /// <summary>
        /// Crea un nuevo objeto rol y lo guarda en la DB.
        /// </summary>
        /// <param name="usuario_id">ID del usuario asociado</param>
        /// <param name="tipoRol_id">ID del tipo de rol asociado</param>
        /// <param name="establecimiento_id">ID del establecimiento asociado</param>
        /// <param name="nivel_id">ID del nivel educativo asociado</param>

        public void Nuevo(int usuario_id, int tipoRol_id, int establecimiento_id, int nivel_id)
        {
            Rol oRol;
            NivelEducativo oNivel = null;
            Establecimiento oEstablecimiento = null;
            Usuario oUsuario = usuarios.BuscarPorId(usuario_id);
            TipoRol oTipo = tipo_roles.BuscarPorId(tipoRol_id);

            if (oUsuario == null)
                throw new Exception("No existe usuario con ese ID");

            if (oTipo == null)
                throw new Exception("No existe un tipo de rol con ese ID");
            /*
            if (oEstablecimiento == null)
                throw new Exception("No existe establecimiento con ese id");

            NivelEducativo oNivel = oEstablecimiento.ListaNiveles.Find(n => n.Id == nivel_id);

            if (oNivel == null)
                throw new Exception("El establecimiento no tiene asociado ese nivel educativo");
            */
            oRol = new Rol(oTipo, oUsuario, oEstablecimiento, oNivel);
            _roles.Agregar(oRol);
        }

        /// <summary>
        /// Edita el registro de la rol especificado
        /// </summary>
        /// <param name="nombre">Nombre de la ciudad</param>
        /// <param name="id">ID de la ciudad a editar</param>
        /// <param name="provincia_id">ID de la provincia asociada</param>

        public void Editar()
        {
            /*
            Provincia oProvincia = provincias.BuscarPorId(provincia_id);
            Ciudad oCiudad = _ciudades.BuscarPorId(id),
                    previa = _ciudades.BuscarPorNombre(nombre).Find(p => p.Provincia.Id == provincia_id);

            if (previa != null)
                throw new Exception("Otra ciudad en esa provincia ya uso ese nombre");

            if (oCiudad == null)
                throw new Exception("No existe ciudad con ese ID");

            if (oProvincia == null)
                throw new Exception("No existe provincia con ese ID");

            oCiudad.Nombre = nombre;
            oCiudad.Provincia = oProvincia;
            _ciudades.Editar(oCiudad);*/
        }

        /// <summary>
        /// Busca todos los roles
        /// </summary>
        /// <returns>Devuelve un lista de objetos rol</returns>

        public List<Rol> TraerTodos(int id)
        {
            return _roles.TraerTodos(id);
        }

        /// <summary>
        /// Marca como borrado el rol especificado
        /// </summary>
        /// <param name="id">ID del rol a desactivar</param>

        public void Desactivar(int id)
        {
            Rol oRol = _roles.BuscarPorId(id);

            if (oRol == null)
                throw new Exception("No existe rol con ese ID");

            _roles.Desactivar(id);
        }

        /// <summary>
        /// Marca como activo el rol especificado
        /// </summary>
        /// <param name="id">ID del rol a activar</param>

        public void Reactivar(int id)
        {
            Rol oRol = _roles.BuscarPorId(id);

            if (oRol == null)
                throw new Exception("No existe rol con ese ID");

            if (DateTime.Compare(oRol.Tipo.Borrado, DateTime.Now) < 0)
                throw new Exception("No se puede activar un rol cuyo tipo está desactivado");

            if (DateTime.Compare(oRol.Usuario.Borrado, DateTime.Now) < 0)
                throw new Exception("No se puede activar un rol cuyo usuario está desactivado");

            if (oRol.Establecimiento != null)
                if (DateTime.Compare(oRol.Establecimiento.Borrado, DateTime.Now) < 0)
                    throw new Exception("No se puede activar un rol cuyo establecimiento está desactivado");

            if (oRol.Nivel != null)
                if (DateTime.Compare(oRol.Nivel.Borrado, DateTime.Now) < 0)
                    throw new Exception("No se puede activar un rol cuyo nivel está desactivado");
            
            _roles.Reactivar(id);
        }

        /// <summary>
        /// Busca el rol con el ID especificado
        /// </summary>
        /// <param name="id">ID de la rol a buscar</param>
        /// <returns>Devuelve un objeto rol</returns>

        public Rol BuscarPorId(int id)
        {
            Rol oRol = _roles.BuscarPorId(id);

            if (oRol == null)
                throw new Exception("No existe rol con ese ID");

            return oRol;
        }
    }
}

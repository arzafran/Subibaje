using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using accesoDatos;
using Modelos;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using System.Drawing;
using System.Drawing.Imaging;

namespace Controladoras
{
    public class ControlAltaRoles
    {
        private DBRoles _roles = new DBRoles();
        public DBEstablecimientoNivel establecimientos_niveles = new DBEstablecimientoNivel();
        public ControlAltaEstablecimientos establecimientos = new ControlAltaEstablecimientos();
        public ControlAltaNivelesEducativos niveles = new ControlAltaNivelesEducativos();
        public ControlAltaUsuarios usuarios = new ControlAltaUsuarios();
        public ControlAltaTipoRoles tipo_roles = new ControlAltaTipoRoles();

        /// <summary>
        /// Guarda un objeto rol en la DB
        /// </summary>
        /// <param name="oRol">Objeto a persistir</param>

        public void Nuevo(Rol oRol)
        {
            int _id = _roles.Nuevo(oRol);
            this.GenerarQr(_id);
        }

        /// <summary>
        /// Crea un nuevo objeto rol y lo guarda en la DB.
        /// </summary>
        /// <param name="usuario_id">ID del usuario asociado</param>
        /// <param name="tipoRol_id">ID del tipo de rol asociado</param>
        /// <param name="establecimiento_id">ID del establecimiento asociado</param>
        /// <param name="nivel_id">ID del nivel educativo asociado</param>

        public void Nuevo(int usuario_id, int tipo_id, int establecimiento_id, int nivel_id)
        {
            Rol oRol;
            NivelEducativo oNivel = niveles.BuscarPorId(nivel_id);
            Establecimiento oEstablecimiento = establecimientos.BuscarPorId(establecimiento_id);
            Usuario oUsuario = usuarios.BuscarPorId(usuario_id);
            TipoRol oTipo = tipo_roles.BuscarPorId(tipo_id);

            if (oUsuario == null)
                throw new Exception("No existe usuario con ese ID");

            if (oTipo == null)
                throw new Exception("No existe un tipo de rol con ese ID");
           
            if(oNivel == null)
                throw new Exception("El establecimiento no tiene asociado ese nivel educativo");

            if (oEstablecimiento == null)
                throw new Exception("No existe establecimiento con ese id");

            if (establecimientos_niveles.BuscarPorParametros(oEstablecimiento.Id, oNivel.Id) == 0)
                throw new Exception("No existe dupla Establecimiento/Nivel como la que elegiste");

            oRol = new Rol(oTipo, oUsuario, oEstablecimiento, oNivel);
            int _id = _roles.Nuevo(oRol);
            this.GenerarQr(_id);
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

            if (_roles.TieneRolActivo(oRol.Usuario.Id, oRol.Tipo.Id))
                throw new Exception("El usuario ya tiene ese rol activo");
            
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

        /// <summary>
        /// Busca el rol activo con el ID especificado
        /// </summary>
        /// <param name="id">ID de la rol a buscar</param>
        /// <returns>Devuelve un objeto rol</returns>

        public Rol BuscarPorIdActivo(int id)
        {
            Rol oRol = _roles.BuscarPorIdActivo(id);

            if (oRol == null)
                throw new Exception("No existe rol activo con ese ID");

            return oRol;
        }

        /// <summary>
        /// Verifica si el usuario loggueado tiene los permisos para acceder a la pagina solicitada
        /// </summary>
        /// <param name="usuario_id">ID del usuario a verificar</param>
        /// <param name="tipo_id">ID del rol que deberia tener</param>
        /// <returns>True si tiene los permisos o false en caso contrario.</returns>

        public int TieneRol(int usuario_id, int tipo_id)
        {
            return _roles.TieneRol(usuario_id, tipo_id);
        }

        /// <summary>
        /// Verifica si el usuario especificado ya es estudiante en el establecimiento especificado.
        /// </summary>
        /// <param name="usuario_id">ID del usuario a verificar</param>
        /// <param name="establecimiento_nivel_id"></param>
        /// <returns>True si tiene el rol asignado, false si no lo tiene.</returns>

        public bool EsEstudiante(int usuario_id, int establecimiento_nivel_id)
        {
            return _roles.EsEstudiante(usuario_id, establecimiento_nivel_id);
        }

        /// <summary>
        /// Busca todos los roles activos de un usuario
        /// </summary>
        /// <param name="id">ID del usuario</param>
        /// <returns>Devuelve una lista de roles</returns>

        public List<Rol> TraerActivos(int id)
        {
            return _roles.TraerActivos(id);
        }

        /// <summary>
        /// Busca todos los roles estudiante de un establecimiento/nivel
        /// </summary>
        /// <param name="id">ID establecimiento_nivel</param>
        /// <returns>Devuelve una lista de roles</returns>

        public List<Rol> TraerDirigidos(int establecimiento_nivel_id)
        {
            return _roles.TraerDirigidos(establecimiento_nivel_id);
        }

        /// <summary>
        /// Busca todos los roles "estudiante" inactivos
        /// </summary>
        /// <returns>Devuelve una lista de roles</returns>

        public List<Rol> TraerInactivos()
        {
            return _roles.TraerInactivos();
        }

        private void GenerarQr(int rol_id)
        {
            QRCodeEncoder encoder = new QRCodeEncoder();
            Bitmap img = encoder.Encode(rol_id.ToString());
            img.Save(AppDomain.CurrentDomain.BaseDirectory.ToString() + "img\\qr\\" + rol_id.ToString() + ".jpeg", ImageFormat.Jpeg);
        }
    }
}

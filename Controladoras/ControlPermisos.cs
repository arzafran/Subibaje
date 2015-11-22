using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using accesoDatos;

namespace Controladoras
{
    public class ControlPermisos
    {
        private DBRoles _roles = new DBRoles();

        /// <summary>
        /// Verifica si el usuario loggueado tiene los permisos para acceder a la pagina solicitada
        /// </summary>
        /// <param name="usuario_id">ID del usuario a verificar</param>
        /// <param name="tipo_id">ID del rol que deberia tener</param>
        /// <returns>True si tiene los permisos o false en caso contrario.</returns>

        public bool TieneRol(int usuario_id, int tipo_id)
        {
            return _roles.TieneRol(usuario_id, tipo_id);
        }
    }
}

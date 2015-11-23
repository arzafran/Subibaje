using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modelos;
using accesoDatos;

namespace Controladoras
{
    public class ControlHome
    {
        private DBUsuarios _usuarios = new DBUsuarios();
        public ControlAltaRoles roles = new ControlAltaRoles();

        public void CambiarPassword(int usuario_id, string passVieja, string passNueva1, string passNueva2)
        {
            if (passNueva1 != passNueva2)
                throw new Exception("El password ingresado y su confirmación no concuerdan");

            Usuario oUsuario = _usuarios.BuscarPorId(usuario_id);

            if (oUsuario == null)
                throw new Exception("No existe Usuario activo con ese id");

            if (oUsuario.Password != passVieja)
                throw new Exception("El password original no es correcto");

            _usuarios.ActualizarPassword(usuario_id, passNueva1);
        }
    }
}

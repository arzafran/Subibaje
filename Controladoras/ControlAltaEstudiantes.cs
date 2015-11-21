using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using accesoDatos;
using Modelos;

namespace Controladoras
{
    public class ControlAltaEstudiantes
    {
        private DBUsuarios _usuarios = new DBUsuarios();
        private DBRoles _roles = new DBRoles();
        private DBTipoRoles _tipos = new DBTipoRoles();
        private DBEstablecimientos _establecimientos = new DBEstablecimientos();
        private DBNivelesEducativos _niveles = new DBNivelesEducativos();

        public void Nuevo(int dni, string nombre, string email, int director_id)
        {
            TipoRol oTipo = _tipos.BuscarPorNombre("Estudiante");
            Usuario director = _usuarios.BuscarPorId(director_id),
                    oUser = _usuarios.BuscarPorDni(dni);
            Establecimiento oEstablecimiento = director.ListaRoles.First(p => p.Tipo.Nombre == "Director").Establecimiento;
            NivelEducativo oNivel = director.ListaRoles.First(p => p.Tipo.Nombre == "Director").Nivel;

            if (oUser == null)
            {
                oUser = new Usuario(nombre, dni, email);
                oUser.Id = _usuarios.Nuevo(oUser);
            }

            Rol oRol = new Rol(oTipo, oUser, oEstablecimiento, oNivel);
            _roles.Agregar(oRol);
        }
        
    }
}

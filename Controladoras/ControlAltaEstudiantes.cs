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
        public DBRoles roles = new DBRoles();
        private DBTipoRoles _tipos = new DBTipoRoles();
        private DBEstablecimientos _establecimientos = new DBEstablecimientos();
        private DBNivelesEducativos _niveles = new DBNivelesEducativos();
        private DBEstablecimientoNivel _establecimientos_niveles = new DBEstablecimientoNivel();

        public void Nuevo(int dni, string nombre, string email, int director_id)
        {
            TipoRol oTipo = _tipos.BuscarPorNombre("Estudiante");
            Usuario director = _usuarios.BuscarPorId(director_id),
                    oUser = _usuarios.BuscarPorDni(dni);

            director.ListaRoles = roles.TraerActivos(director_id);

            Establecimiento oEstablecimiento = director.ListaRoles.First(p => p.Tipo.Nombre == "Director").Establecimiento;
            NivelEducativo oNivel = director.ListaRoles.First(p => p.Tipo.Nombre == "Director").Nivel;

            if (oUser == null)
            {
                oUser = new Usuario(nombre, dni, email);
                oUser.Id = _usuarios.Nuevo(oUser);
            }

            Rol oRol = new Rol(oTipo, oUser, oEstablecimiento, oNivel);
            roles.Agregar(oRol);
        }

        public List<Rol> TraerDirigidos(int director_id)
        {
            Usuario director = _usuarios.BuscarPorId(director_id);
            director.ListaRoles = roles.TraerActivos(director_id);
            Establecimiento oEstablecimiento = director.ListaRoles.First(p => p.Tipo.Nombre == "Director").Establecimiento;
            NivelEducativo oNivel = director.ListaRoles.First(p => p.Tipo.Nombre == "Director").Nivel;
            int establecimiento_nivel_id = _establecimientos_niveles.BuscarPorParametros(oEstablecimiento.Id, oNivel.Id);
            
            return roles.TraerDirigidos(establecimiento_nivel_id);
        }
        
    }
}

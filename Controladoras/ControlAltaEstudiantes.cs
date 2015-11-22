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

        /// <summary>
        /// Agrega un rol estudiante en la DB. Si el usuario no existe, tambien lo crea.
        /// </summary>
        /// <param name="dni">DNI del estudiante</param>
        /// <param name="nombre">Nombre del estudiante</param>
        /// <param name="email">Email del estudiante</param>
        /// <param name="director_id">ID del director asociado que crea al estudiante.</param>

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
                if (_usuarios.BuscarPorEmail(email) != null)
                    throw new Exception("Ya existe un estudiante con ese email");

                oUser = new Usuario(nombre, dni, email);
                oUser.Id = _usuarios.Nuevo(oUser);
            }

            if(roles.EsEstudiante(oUser.Id, _establecimientos_niveles.BuscarPorParametros(oEstablecimiento.Id, oNivel.Id)))
                throw new Exception("Este usuario ya tiene el rol estudiante en su institucion.");

            Rol oRol = new Rol(oTipo, oUser, oEstablecimiento, oNivel);
            roles.Agregar(oRol);
        }

        /// <summary>
        /// Busca todos los estudiantes bajo la tutela de un director especificado.
        /// </summary>
        /// <param name="director_id">ID del rol Director del cual buscar estudiantes.</param>
        /// <returns>Devuelve una lista de roles.</returns>

        public List<Rol> TraerDirigidos(int director_id)
        {
            Usuario director = _usuarios.BuscarPorId(director_id);
            director.ListaRoles = roles.TraerActivos(director_id);
            Establecimiento oEstablecimiento = director.ListaRoles.First(p => p.Tipo.Nombre == "Director").Establecimiento;
            NivelEducativo oNivel = director.ListaRoles.First(p => p.Tipo.Nombre == "Director").Nivel;
            int establecimiento_nivel_id = _establecimientos_niveles.BuscarPorParametros(oEstablecimiento.Id, oNivel.Id);
            
            return roles.TraerDirigidos(establecimiento_nivel_id);
        }

        /// <summary>
        /// Marca como desactivado al estudiante especificado
        /// </summary>
        /// <param name="id">ID del estudiante a desactivar</param>

        public void Desactivar(int id)
        {
            Rol oRol = roles.BuscarPorId(id);

            if (oRol == null)
                throw new Exception("No existe usuario con el rol con ese ID");

            roles.Desactivar(id);
        }
    }
}

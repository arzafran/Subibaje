using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using accesoDatos;
using Modelos;

namespace Controladoras
{
    public class ControlAltaNivelesEducativos
    {
        private DBNivelesEducativos DatosNivelEducativo = new DBNivelesEducativos();

        public void Nuevo(string nombre)
        {
            if (DatosNivelEducativo.BuscarPorNombre(nombre) != null)
                throw new Exception("Ya existe un nivel educativo con ese nombre");

            NivelEducativo oNivel = new NivelEducativo(nombre);
            DatosNivelEducativo.Agregar(oNivel);
        }

        public void Editar(string nombre, int id)
        {
            if (DatosNivelEducativo.BuscarPorNombre(nombre) != null)
                throw new Exception("El nombre ya está usado");

            NivelEducativo aux = DatosNivelEducativo.BuscarPorId(id);
            if (aux != null)
            {
                aux.Nombre = nombre;
                DatosNivelEducativo.Editar(aux);
            }
        }

        public List<NivelEducativo> TraerTodos()
        {
            //return Lista_Usuarios.Mostrar_Todo();
            return DatosNivelEducativo.TraerTodos();
        }

        public void Borrar(int id)
        {
            DatosNivelEducativo.Borrar(id);
        }

        public void Restituir(int id)
        {
            DatosNivelEducativo.Restituir(id);
        }

        public string BuscarPorId(int id)
        {
            NivelEducativo aux = DatosNivelEducativo.BuscarPorId(id);
            string devolver = "";

            if (aux != null)
                devolver = aux.Nombre;

            return devolver;
        }
    }
}

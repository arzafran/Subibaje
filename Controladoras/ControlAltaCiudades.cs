using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using accesoDatos;
using Modelos;

namespace Controladoras
{
    public class ControlAltaCiudades
    {
        private DBCiudades DatosCiudades = new DBCiudades();
        //private DBProvincias provincias = new DBProvincias();

        //private ListaCiudades DatosCiudades = ListaCiudades.Instance();
        //public ListaProvincias provincias = ListaProvincias.Instance();

        public void Nuevo(string nombre, int idProvincia)
        {
            Provincia oProvincia = DatosCiudades.provincias.BuscarPorId(idProvincia);
            Ciudad oCiudad = new Ciudad(nombre, oProvincia);
            DatosCiudades.Agregar(oCiudad);
        }

        public void Editar(string nombre, int id, int provincia_id)
        {
            Ciudad oCiudad = DatosCiudades.BuscarPorId(id);
            Ciudad previa = DatosCiudades.BuscarPorNombre(nombre);

            if (previa != null)
                if (previa.Provincia.Id == provincia_id)
                    throw new Exception("Otra ciudad en esa provincia ya uso ese nombre");

            if (oCiudad != null)
            {
                oCiudad.Nombre = nombre;
                oCiudad.Provincia = DatosCiudades.provincias.BuscarPorId(provincia_id);
                DatosCiudades.Editar(oCiudad);
            }
        }

        public List<Ciudad> TraerTodos()
        {
            //return Lista_Usuarios.Mostrar_Todo();
            return DatosCiudades.TraerTodos();
        }

        public void Borrar(int id)
        {
            DatosCiudades.Borrar(id);
        }

        public void Restituir(int id)
        {
            DatosCiudades.Restituir(id);
        }

        public Dictionary<string, string> BuscarPorId(int id)
        {
            Ciudad aux = DatosCiudades.BuscarPorId(id);
            Dictionary<string, string> devolver = new Dictionary<string,string>();

            if (aux != null)
            {
                devolver.Add("id", aux.Id.ToString());
                devolver.Add("nombre", aux.Nombre);
                devolver.Add("provincia_id", aux.Provincia.Id.ToString());
            }
                

            return devolver;
        }
    }
}

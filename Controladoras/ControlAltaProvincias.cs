using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using accesoDatos;
using Modelos;

namespace Controladoras
{
    public class ControlAltaProvincias
    {
        private DBProvincias DatosProvincia = new DBProvincias(); //DBProvincias.Instance();

        public void Nuevo(string nombre)
        {
            Provincia oProvincia = new Provincia(nombre);
            DatosProvincia.Agregar(oProvincia);
        }

        public void Editar(string nombre, int id)
        {
            Provincia aux = DatosProvincia.BuscarPorId(id);
            if (aux != null)
            {
                aux.Nombre = nombre;
                DatosProvincia.Editar(aux);
            }
        }

        public List<Provincia> TraerTodos()
        {
            return DatosProvincia.TraerTodos();
        }

        public void Borrar(int id)
        {
            DatosProvincia.Borrar(id);
        }

        public void Restituir(int id)
        {
            DatosProvincia.Restituir(id);
        }

        public string BuscarPorId(int id)
        {
            Provincia aux = DatosProvincia.BuscarPorId(id);
            string devolver = "";

            if (aux != null)
                devolver = aux.Nombre;

            return devolver;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using accesoDatos;
using Modelos;

namespace Controladoras
{
    public class ControlAltaEmpresas
    {
        private ListaEmpresas DatosEmpresa = ListaEmpresas.Instance();
        public ListaRutas rutas = ListaRutas.Instance();

        public void Nuevo(string nombre, List<int> listaIds)
        {
            List<Ruta> _rutas = new List<Ruta>();
            foreach (int id in listaIds)
            {
                _rutas.Add(rutas.BuscarPorId(id));
            }
            Empresa oEmpresa = new Empresa(nombre, _rutas);
            DatosEmpresa.Agregar(oEmpresa);
        }

        public List<Empresa> TraerTodos()
        {
            //return Lista_Usuarios.Mostrar_Todo();
            return DatosEmpresa.TraerTodos();
        }
    }
}

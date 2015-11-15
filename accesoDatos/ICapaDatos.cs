using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace accesoDatos
{
    public interface IcapaDato<t>
    {
       void Agregar(t dato);
       void Desactivar(t dato);
       void Desactivar(int dato);
       void Reactivar(int dato);
       void Editar(t dato);
       t BuscarPorId(int dato);
       List<t> TraerTodos();
    }
}

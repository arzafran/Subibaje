using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modelos;
using accesoDatos;

namespace Controladoras
{
    public class ControlAltaBoletos
    {
        private DBBoletos _boletos = new DBBoletos();
        public ControlAltaRoles roles = new ControlAltaRoles();
        public ControlAltaUrbanos urbanos = new ControlAltaUrbanos();

        /// <summary>
        /// Emite un boleto y lo persiste en la DB
        /// </summary>
        /// <param name="rol_id">ID del rol que quiere sacar el boleto</param>
        /// <param name="urbano_id">ID de la linea </param>
        /// <returns>Devuelve el ID del boleto emitido</returns>

        public int EmitirBoleto(int rol_id, int urbano_id)
        {
            Rol oRol = roles.BuscarPorIdActivo(rol_id);
            Urbano oUrbano = urbanos.BuscarPorIdActivo(urbano_id);

            if (oRol == null || oRol.Tipo.Id != 1 )
                throw new Exception("No existe rol estudiante activo con el id especificado");

            if (oUrbano == null)
                throw new Exception("No existe linea urbana activa con el id especificado");

            Boleto oBoleto = new Boleto(oUrbano, oRol);

            return _boletos.Agregar(oBoleto);
        }

        /// <summary>
        /// Busca todos los boletos del rol (estudiante) especificado
        /// </summary>
        /// <param name="rol_id">ID del rol a buscar</param>
        /// <returns>Devuelve una lista de objetos rol</returns>

        public List<Boleto> TraerBoletosEstudiante(int rol_id)
        {
            return _boletos.TraerBoletosEstudiante(rol_id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Controladoras;

namespace SVC
{
    [WebService(Namespace = "http://localhost")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
 
    public class EmitirBoleto : System.Web.Services.WebService
    {
        /// <summary>
        /// Emite un boleto para el rol especificado en la linea urbana especificada
        /// </summary>
        /// <param name="rol_id">ID del rol que desea emitir el boleto</param>
        /// <param name="urbano_id">ID de la linea donde se va a emitir el boleto</param>
        /// <returns>Devuelve el ID del boleto emitido.</returns>

        [WebMethod]
        public string Emitir(int rol_id, int urbano_id)
        {
            string devolver = "";
            try
            {
                ControlAltaBoletos _controladora = new ControlAltaBoletos();
                devolver = _controladora.EmitirBoleto(rol_id, urbano_id).ToString();
            }
            catch (Exception ex)
            {
                devolver = ex.Message;
            }

            return devolver;
        }
    }
}

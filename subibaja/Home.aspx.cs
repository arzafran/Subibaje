using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using subibaja.ClasesBase;
using Controladoras;

namespace subibaja
{
    public partial class Password : Pagina
    {
        private ControlHome _controladora = new ControlHome();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.VerificarLogin();
            int rol_id = _controladora.roles.TieneRol(_usuario.Id, 1);
            _wrapperError = (Panel)Master.FindControl("wrapperExcepcion");
            _error = (Label)Master.FindControl("lblExcepcion");

            if (rol_id != 0)
            {
                imagenQr.Visible = true;
                imagenQr.ImageUrl = "img/qr/" + rol_id.ToString() + ".jpeg";
            }
                
        }

        protected void btnCambiar_Click(object sender, EventArgs e)
        {
            try
            {
                _controladora.CambiarPassword(_usuario.Id, txtPassViejo.Text, txtPassNuevo.Text, txtPassNuevo2.Text);
            }
            catch (Exception ex)
            {
                this.MostrarError(ex.Message);
            }
        }
    }
}
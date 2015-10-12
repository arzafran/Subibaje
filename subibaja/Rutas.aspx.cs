using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controladoras;
using subibaja.ClasesBase;

namespace subibaja
{
    public partial class Rutas : Pagina
    {
        private ControlAltaRutas controladora = new ControlAltaRutas();

        protected void Page_Load(object sender, EventArgs e)
        {
            grdUrbanos.DataSource = controladora.TraerTodos();
            ddlOrigen.DataSource = controladora.ciudades.TraerTodos();
            ddlDestino.DataSource = controladora.ciudades.TraerTodos();
            ddlOrigen.DataValueField = "Id";
            ddlDestino.DataValueField = "Id";
            ddlOrigen.DataTextField = "Nombre";
            ddlDestino.DataTextField = "Nombre";
            GenerarColumnas(grdUrbanos);
            if (!IsPostBack)
            {
                grdUrbanos.DataBind();
                ddlOrigen.DataBind();
                ddlDestino.DataBind();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {          
            controladora.Nuevo(txtNombre.Text, Convert.ToInt32(ddlOrigen.SelectedValue), Convert.ToInt32(ddlDestino.SelectedValue));
            grdUrbanos.DataBind();
            this.LimpiarControles(Page.Controls);
        }
    }
}
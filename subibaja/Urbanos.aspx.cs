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
    public partial class Urbanos : Pagina
    {
        private ControlAltaUrbanos controladora = new ControlAltaUrbanos();

        protected void Page_Load(object sender, EventArgs e)
        {
            grdUrbanos.DataSource = controladora.TraerTodos();
            ddlLinea.DataSource = controladora.ciudades.TraerTodos();
            ddlLinea.DataValueField = "Id";
            ddlLinea.DataTextField = "Nombre";
            GenerarColumnas(grdUrbanos);
            if (!IsPostBack)
            {
                grdUrbanos.DataBind();
                ddlLinea.DataBind();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            controladora.Nuevo(txtLinea.Text, Convert.ToInt32(ddlLinea.SelectedValue));
            grdUrbanos.DataBind();
            this.LimpiarControles(Page.Controls);
        }
    }
}
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
    public partial class Provincias : Pagina
    {
        private ControlAltaProvincias controladora = new ControlAltaProvincias();

        protected void Page_Load(object sender, EventArgs e)
        {
            grdProvincias.DataSource = controladora.TraerTodos();
            GenerarColumnas(grdProvincias);
            if (!IsPostBack)
            {
                grdProvincias.DataBind();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            controladora.Nuevo(txtNombre.Text);
            grdProvincias.DataBind();
            this.LimpiarControles(Page.Controls);
        }
    }
}
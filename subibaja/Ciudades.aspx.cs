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
    public partial class Ciudades : Pagina
    {
        private ControlAltaCiudades controladora = new ControlAltaCiudades();

        protected void Page_Load(object sender, EventArgs e)
        {
            grdCiudades.DataSource = controladora.TraerTodos();
            GenerarColumnas(grdCiudades);
            if (!IsPostBack)
            {
                grdCiudades.DataBind();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            controladora.Nuevo(txtNombre.Text);
            grdCiudades.DataBind();
            this.LimpiarControles(Page.Controls);
        }
    }
}
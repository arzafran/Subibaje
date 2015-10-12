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
    public partial class NivelesEducativos : Pagina
    {
        private ControlAltaNivelesEducativos controladora = new ControlAltaNivelesEducativos();

        protected void Page_Load(object sender, EventArgs e)
        {
            grdNiveles.DataSource = controladora.TraerTodos();
            GenerarColumnas(grdNiveles);
            if (!IsPostBack)
                grdNiveles.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            controladora.Nuevo(txtDescripcion.Text);
            grdNiveles.DataBind();
            this.LimpiarControles(Page.Controls);
        }
    }
}
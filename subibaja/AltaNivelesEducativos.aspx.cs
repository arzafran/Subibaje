using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controladoras;

namespace subibaja
{
    public partial class AltaNivelEducativo : System.Web.UI.Page
    {
        private ControlAltaNivelesEducativos controladora = new ControlAltaNivelesEducativos();


        protected void Page_Load(object sender, EventArgs e)
        {
            grdNiveles.DataSource = controladora.TraerTodos();
            grdNiveles.DataBind();
        }

        protected void Agregar_Click(object sender, EventArgs e)
        {
            controladora.Nuevo(txtDescripcion.Text);
            grdNiveles.DataBind();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controladoras;

namespace subibaja
{
    public partial class AltaUsuarios : System.Web.UI.Page
    {
        private ControlAltaUsuarios controladora = new ControlAltaUsuarios();

        protected void Page_Load(object sender, EventArgs e)
        {
            grdUsuarios.DataSource = controladora.TraerTodos();
            grdUsuarios.DataBind();
        }

        protected void Agregar_Click(object sender, EventArgs e)
        {
            controladora.Nuevo(txtNombre.Text, txtPassword.Text, txtEmail.Text, Convert.ToInt32(txtDni.Text));
            grdUsuarios.DataBind();
        }
    }
}
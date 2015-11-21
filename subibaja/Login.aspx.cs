using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelos;
using Controladoras;

namespace subibaja
{
    public partial class Login : System.Web.UI.Page
    {
        private Usuario _usuario;
        private ControlAltaUsuarios _controladora = new ControlAltaUsuarios();

        protected void Page_Load(object sender, EventArgs e)
        {
            _usuario = (Usuario)Session["usuario"];
            if (_usuario != null)
                Response.Redirect("Index.aspx");
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Usuario oUser = _controladora.Validar(txtEmail.Text, txtPassword.Text);
            if (oUser != null)
            {
                Session["usuario"] = oUser;
                Response.Redirect("Index.aspx");
            }

            lblError.Text = "INCORRECTO GATO";    
        }
    }
}
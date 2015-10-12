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
    public partial class Usuarios : Pagina
    {
        private ControlAltaUsuarios controladora = new ControlAltaUsuarios();

        protected void Page_Load(object sender, EventArgs e)
        {
            grdUsuarios.DataSource = controladora.TraerTodos();
            GenerarColumnas(grdUsuarios);
            if (!IsPostBack)
            {
                grdUsuarios.DataBind();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            controladora.Nuevo(txtNombre.Text, txtPassword.Text, txtEmail.Text, Convert.ToInt32(txtDni.Text));
            grdUsuarios.DataBind();
            this.LimpiarControles(Page.Controls);
        }
    }
}
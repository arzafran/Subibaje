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
        public ControlAltaProvincias provincias = new ControlAltaProvincias();

        protected void Page_Load(object sender, EventArgs e)
        {
            grdCiudades.DataSource = controladora.TraerTodos();
            ddlProvincia.DataSource = provincias.TraerTodos();
            ddlProvincia.DataValueField = "Id";
            ddlProvincia.DataTextField = "Nombre";
            GenerarColumnas(grdCiudades);
            if (!IsPostBack)
            {
                grdCiudades.DataBind();
                ddlProvincia.DataBind();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            controladora.Nuevo(txtNombre.Text, Convert.ToInt32(ddlProvincia.SelectedValue));
            grdCiudades.DataBind();
            this.LimpiarControles(Page.Controls);
        }
    }
}
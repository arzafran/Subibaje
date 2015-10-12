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
    public partial class Establecimientos : Pagina
    {
        private ControlAltaEstablecimientos controladora = new ControlAltaEstablecimientos();

        private List<int> listaIdsSeleccionados()
        {
            List<int> _niveles = new List<int>();

            foreach (ListItem _item in ckNiveles.Items)
            {
                if (_item.Selected)
                {
                    _niveles.Add(Convert.ToInt32(_item.Value));
                }
            }

            return _niveles;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            grdEstablecimientos.DataSource = controladora.TraerTodos();
            ckNiveles.DataSource = controladora.niveles.TraerTodos();
            ddlCiudad.DataSource = controladora.ciudades.TraerTodos();
            ddlCiudad.DataValueField = "Id";
            ddlCiudad.DataTextField = "Nombre";
            GenerarColumnas(grdEstablecimientos);
            if (!IsPostBack)
            {
                grdEstablecimientos.DataBind();
                ckNiveles.DataBind();
                ddlCiudad.DataBind();
            }
        }

        protected void btnAgregar_Click1(object sender, EventArgs e)
        {
            controladora.Nuevo(txtNombre.Text, Convert.ToInt32(ddlCiudad.SelectedValue), listaIdsSeleccionados());
            grdEstablecimientos.DataBind();
            this.LimpiarControles(Page.Controls);
        }
    }
}
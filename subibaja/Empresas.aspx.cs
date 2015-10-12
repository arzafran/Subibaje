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
    public partial class Empresas : Pagina
    {
        private ControlAltaEmpresas controladora = new ControlAltaEmpresas();

        private List<int> listaIdsSeleccionados()
        {
            List<int> _rutas = new List<int>();

            foreach (ListItem _item in ckRutas.Items)
            {
                if (_item.Selected)
                {
                    _rutas.Add(Convert.ToInt32(_item.Value));
                }
            }

            return _rutas;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            grdEmpresas.DataSource = controladora.TraerTodos();
            ckRutas.DataSource = controladora.rutas.TraerTodos();
            GenerarColumnas(grdEmpresas);
            if (!IsPostBack)
            {
                grdEmpresas.DataBind();
                ckRutas.DataBind();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            controladora.Nuevo(txtNombre.Text, listaIdsSeleccionados());
            grdEmpresas.DataBind();
            this.LimpiarControles(Page.Controls);
        }
    }
}
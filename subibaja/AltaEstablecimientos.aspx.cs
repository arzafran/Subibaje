using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controladoras;

namespace subibaja
{
    public partial class AltaEstablecimientos : System.Web.UI.Page
    {
        ControlAltaEstablecimientos controladora = new ControlAltaEstablecimientos();

        private List<int> idsNiveles()
        {
            List<int> _niveles = null;
            IEnumerable<Control> _controles = from Control c in this.Controls where c is CheckBox select c;

            if (_controles.Count() > 0)
            {
                foreach (CheckBox _c in _controles)
                {
                    _niveles.Add(Convert.ToInt32(_c.InputAttributes["value"]));
                }
            }

            return _niveles;
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            grdEstablecimientos.DataSource = controladora.TraerTodos();
            grdEstablecimientos.DataBind();
            CheckList1.DataSource = controladora.niveles.TraerTodos();
            CheckList1.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            
            controladora.Nuevo(txtNombre.Text, idsNiveles());
            grdEstablecimientos.DataBind();
        }
    }
}
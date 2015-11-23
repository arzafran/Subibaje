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
    public partial class Pendientes : Pagina
    {
        private ControlAltaEstudiantes _controladora = new ControlAltaEstudiantes();

        private void Bind()
        {
            try
            {
                grdPendientes.DataSource = _controladora.TraerInactivos();
                grdPendientes.DataBind();
            }
            catch (Exception ex)
            {
                this.MostrarError(ex.Message);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.VerificarLogin();
            this._permiso_id = 2;

            if (_controladora.roles.TieneRol(_usuario.Id, _permiso_id) == 0)
                Response.Redirect("Permisos.aspx");

            _wrapperError = (Panel)Master.FindControl("wrapperExcepcion");
            _error = (Label)Master.FindControl("lblExcepcion");

            if (!IsPostBack)
                this.Bind();
        }

        protected void grdPendientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            _wrapperError.Style.Add("display", "none");

            try
            {
                int id = Convert.ToInt32(grdPendientes.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value.ToString());

                switch (e.CommandName.ToString())
                {
                    case "comandoRestitucion":
                        _controladora.Reactivar(id);
                        this.Bind();
                        break;
                }

            }
            catch (Exception ex)
            {
                this.MostrarError(ex.Message);
            }
        }

        protected void grdPendientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DateTime dt;
                DateTime.TryParse(e.Row.Cells[4].Text, out dt);

                LinkButton lb = (LinkButton)e.Row.FindControl("linkBorrado");
                if (lb != null && dt.CompareTo(DateTime.Now) < 0)
                {
                    e.Row.CssClass = "danger";
                }
            }
        }
    }
}
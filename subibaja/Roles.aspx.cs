using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controladoras;
using subibaja.ClasesBase;
using Modelos;

namespace subibaja
{
    public partial class Roles : Pagina
    {
        private ControlAltaRoles _controladora = new ControlAltaRoles();
        private int usuario_id;

        private void Bind()
        {
            try
            {
                grdRoles.DataSource = _controladora.TraerTodos(usuario_id);
                grdRoles.DataBind();
                   
                ddlEstablecimiento.DataSource = _controladora.establecimientos.TraerActivos();
                ddlEstablecimiento.DataValueField = "Id";
                ddlEstablecimiento.DataTextField = "NombreCompleto";
                ddlEstablecimiento.DataBind();
                ddlEstablecimiento.Items.Insert(0, new ListItem(String.Empty, "0"));
                ddlEstablecimiento.SelectedIndex = 0;

                ddlNivel.DataSource = _controladora.niveles.TraerActivos();
                ddlNivel.DataValueField = "Id";
                ddlNivel.DataTextField = "Nombre";
                ddlNivel.DataBind();
                ddlNivel.Items.Insert(0, new ListItem(String.Empty, "0"));
                ddlNivel.SelectedIndex = 0;

                ddlTipo.DataSource = _controladora.tipo_roles.TraerActivos();
                ddlTipo.DataValueField = "Id";
                ddlTipo.DataTextField = "Nombre";
                ddlTipo.DataBind();
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

            if (_controladora.TieneRol(_usuario.Id, _permiso_id) == 0)
                Response.Redirect("Permisos.aspx");

            _wrapperError = (Panel)Master.FindControl("wrapperExcepcion");
            _error = (Label)Master.FindControl("lblExcepcion");

            try
            {
                usuario_id = Convert.ToInt32(Request.QueryString["id"]);
                if (usuario_id == 0)
                    throw new Exception("Que estás tratando de hacer?");
            }
            catch (Exception ex)
            {
                Response.Redirect("Usuarios.aspx");
            }

            if (!IsPostBack)
                this.Bind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            _wrapperError.Style.Add("display", "none");

            try
            {
                _controladora.Nuevo(usuario_id, Convert.ToInt32(ddlTipo.SelectedValue), Convert.ToInt32(ddlEstablecimiento.SelectedValue), Convert.ToInt32(ddlNivel.SelectedValue));
            }
            catch (Exception ex)
            {
                this.MostrarError(ex.Message);
            }

            this.Bind();
            this.LimpiarControles(Page.Controls);
        }

        protected void grdRoles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            _wrapperError.Style.Add("display", "none");

            try
            {
                int id = Convert.ToInt32(grdRoles.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value.ToString());

                switch (e.CommandName.ToString())
                {
                    case "comandoBorrado":
                        _controladora.Desactivar(id);
                        this.Bind();
                        break;

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

        protected void grdRoles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DateTime dt;
                DateTime.TryParse(e.Row.Cells[5].Text, out dt);

                LinkButton lb = (LinkButton)e.Row.FindControl("linkBorrado");
                if (lb != null && dt.CompareTo(DateTime.Now) < 0)
                {
                    e.Row.CssClass = "danger";
                    lb.CommandName = "comandoRestitucion";
                    lb.Text = "<span class='glyphicon glyphicon-repeat'></span>";
                }
                else
                {
                    e.Row.CssClass = "success";
                    e.Row.Cells[5].Text = "-";
                }
            }
        }
    }
}
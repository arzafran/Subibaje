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
    public partial class Usuarios : Pagina
    {
        private ControlAltaUsuarios _controladora = new ControlAltaUsuarios();

        private void Bind()
        {
            try
            {
                grdUsuarios.DataSource = _controladora.TraerTodos();
                grdUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                this.MostrarError(ex.Message);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.VerificarLogin();

            _wrapperError = (Panel)Master.FindControl("wrapperExcepcion");
            _error = (Label)Master.FindControl("lblExcepcion");

            if (!IsPostBack)
                this.Bind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            _wrapperError.Style.Add("display", "none");

            try
            {
                _controladora.Nuevo(txtNombre.Text, Convert.ToInt32(txtDni.Text), txtEmail.Text);
            }
            catch (Exception ex)
            {
                this.MostrarError(ex.Message);
            }

            this.Bind();
            this.LimpiarControles(Page.Controls);
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            _wrapperError.Style.Add("display", "none");

            try
            {
                _controladora.Editar(txtNombre.Text, Convert.ToInt32(txtDni.Text), txtEmail.Text, Convert.ToInt32(idEdicion.Value));
                this.Bind();
            }
            catch (Exception ex)
            {
                this.MostrarError(ex.Message);
            }

            this.LimpiarControles(Page.Controls);
        }

        protected void grdUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            _wrapperError.Style.Add("display", "none");

            try
            {
                int id = Convert.ToInt32(grdUsuarios.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value.ToString());

                switch (e.CommandName.ToString())
                {
                    case "comandoEdicion":
                        Usuario oUsuario = _controladora.BuscarPorId(id);
                        txtNombre.Text = oUsuario.Nombre;
                        txtDni.Text = oUsuario.Dni.ToString();
                        txtEmail.Text = oUsuario.Email;
                        idEdicion.Value = id.ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "none", "<script>$('#carga').modal('show');</script>", false);
                        break;

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

        protected void grdUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DateTime dt;
                DateTime.TryParse(e.Row.Cells[4].Text, out dt);

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
                    e.Row.Cells[4].Text = "-";
                }
            }
        }
    }
}
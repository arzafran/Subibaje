using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controladoras;
using subibaja.ClasesBase;
using System.Collections;

namespace subibaja
{
    public partial class Provincias : Pagina
    {
        private ControlAltaProvincias _controladora = new ControlAltaProvincias();

        private void Bind()
        {
            grdProvincias.DataSource = _controladora.TraerTodos();
            grdProvincias.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _wrapperError = (Panel)Master.FindControl("wrapperExcepcion");
            _error = (Label)Master.FindControl("lblExcepcion");

            if (!IsPostBack)
                this.Bind();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            _wrapperError.Style.Add("display", "none");

            try
            {
                _controladora.Editar(txtNombre.Text, Convert.ToInt32(idEdicion.Value));
                this.Bind();
            }
            catch (Exception ex)
            {
                this.MostrarError(ex.Message);
            }

            this.LimpiarControles(Page.Controls);

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {   
            _wrapperError.Style.Add("display", "none");

            try
            {
                _controladora.Nuevo(txtNombre.Text);
            }
            catch (Exception ex)
            {
                this.MostrarError(ex.Message);
            }

            this.Bind();
            this.LimpiarControles(Page.Controls);
        }

        protected void grdProvincias_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            _wrapperError.Style.Add("display", "none");
            
            try
            {
                int id = Convert.ToInt32(grdProvincias.DataKeys[e.RowIndex].Value.ToString());
                _controladora.Desactivar(id);
                this.Bind();
            }
            catch(Exception ex)
            {
                this.MostrarError(ex.Message);
            }
        }

        protected void grdProvincias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            _wrapperError.Style.Add("display", "none");

            try
            {
                int id = Convert.ToInt32(grdProvincias.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value.ToString());

                switch (e.CommandName.ToString())
                {
                    case "comandoEdicion":
                        txtNombre.Text = _controladora.BuscarPorId(id).Nombre;
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

        protected void grdProvincias_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DateTime dt;

                DateTime.TryParse(e.Row.Cells[2].Text, out dt);
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
                    e.Row.Cells[2].Text = "-";
                }
            }
        }

    }
}
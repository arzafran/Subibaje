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
        private ControlAltaProvincias provincias = new ControlAltaProvincias();
        private Panel wrapperError;
        private Label error;

        private void Bind()
        {
            grdCiudades.DataSource = controladora.TraerTodos();
            grdCiudades.DataBind();
            ddlProvincia.DataSource = provincias.TraerTodos();
            ddlProvincia.DataValueField = "Id";
            ddlProvincia.DataTextField = "Nombre";
            ddlProvincia.DataBind();
        }

        private void MostrarError(string mensaje)
        {
            error.Text = mensaje;
            wrapperError.Style.Add("display", "block !important");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            wrapperError = (Panel)Master.FindControl("wrapperExcepcion");
            error = (Label)Master.FindControl("lblExcepcion");

            if (!IsPostBack)
                this.Bind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            /*controladora.Nuevo(txtNombre.Text, Convert.ToInt32(ddlProvincia.SelectedValue));
            grdCiudades.DataBind();
            this.LimpiarControles(Page.Controls);*/

            wrapperError.Style.Add("display", "none");

            try
            {
                controladora.Nuevo(txtNombre.Text, Convert.ToInt32(ddlProvincia.SelectedValue));
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
            wrapperError.Style.Add("display", "none");

            try
            {
                controladora.Editar(txtNombre.Text, Convert.ToInt32(idEdicion.Value), Convert.ToInt32(ddlProvincia.SelectedValue));
                this.Bind();
            }
            catch (Exception ex)
            {
                this.MostrarError(ex.Message);
            }

            this.LimpiarControles(Page.Controls);
        }

        protected void grdCiudades_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            wrapperError.Style.Add("display", "none");

            try
            {
                int id = Convert.ToInt32(grdCiudades.DataKeys[e.RowIndex].Value.ToString());
                controladora.Borrar(id);
                this.Bind();
            }
            catch (Exception ex)
            {
                this.MostrarError(ex.Message);
            }
        }

        protected void grdCiudades_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            wrapperError.Style.Add("display", "none");

            try
            {
                int id = Convert.ToInt32(grdCiudades.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value.ToString());

                switch (e.CommandName.ToString())
                {
                    case "comandoEdicion":
                        Dictionary<string, string> _ciudad = controladora.BuscarPorId(id);
                        string _nombre;
                        string _provincia_id;

                        _ciudad.TryGetValue("nombre", out _nombre);
                        _ciudad.TryGetValue("provincia_id", out _provincia_id);

                        txtNombre.Text = _nombre;
                        ddlProvincia.SelectedValue = _provincia_id;

                        idEdicion.Value = id.ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "none", "<script>$('#carga').modal('show');</script>", false);
                        break;

                    case "comandoBorrado":
                        //id = Convert.ToInt32(grdProvincias.DataKeys[e.RowIndex].Value.ToString());
                        controladora.Borrar(id);
                        this.Bind();
                        break;

                    case "comandoRestitucion":
                        //id = Convert.ToInt32(grdProvincias.DataKeys[e.RowIndex].Value.ToString());
                        controladora.Restituir(id);
                        this.Bind();
                        break;
                }

            }
            catch (Exception ex)
            {
                this.MostrarError(ex.Message);
            }
        }

        protected void grdCiudades_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string fechaBorrado = e.Row.Cells[3].Text;
                DateTime dt;
                DateTime.TryParse(fechaBorrado, out dt);

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
                    e.Row.Cells[3].Text = "-";
                }
            }
        }
    }
}
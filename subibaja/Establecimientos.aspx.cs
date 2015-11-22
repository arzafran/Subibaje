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
    public partial class Establecimientos : Pagina
    {
        private ControlAltaEstablecimientos _controladora = new ControlAltaEstablecimientos();

        private void Bind()
        {
            try
            {
                ckNiveles.DataSource = _controladora.niveles.TraerActivos();
                ddlCiudades.DataSource = _controladora.ciudades.TraerActivos();
                grdEstablecimientos.DataSource = _controladora.TraerTodos();
                grdEstablecimientos.DataBind();
                ckNiveles.DataBind();
                ddlCiudades.DataValueField = "Id";
                ddlCiudades.DataTextField = "Nombre";
                ddlCiudades.DataBind();
            }
            catch (Exception ex)
            { 
                this.MostrarError(ex.Message);
            }
        }

        private void marcarCheckBoxes(List<NivelEducativo> niveles)
        {
            foreach (ListItem _item in ckNiveles.Items)
            {
                if(niveles.Find(n => n.Id.ToString() == _item.Value) != null)
                {
                    _item.Selected = true;
                }
            }
        }

        private void DesmarcarCheckBoxes() 
        {
            foreach (ListItem _item in ckNiveles.Items)
            {
                _item.Selected = false;   
            }
        }

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
            this.VerificarLogin();
            this._permiso_id = 2;

            if (!_controladora.permisos.TieneRol(_usuario.Id, _permiso_id))
                Response.Redirect("Permisos.aspx");

            _wrapperError = (Panel)Master.FindControl("wrapperExcepcion");
            _error = (Label)Master.FindControl("lblExcepcion");

            if (!IsPostBack)
                this.Bind();
        }

        protected void btnAgregar_Click1(object sender, EventArgs e)
        {
            try
            {
                _controladora.Nuevo(txtNombre.Text, Convert.ToInt32(ddlCiudades.SelectedValue), listaIdsSeleccionados());
            }
            catch (Exception ex)
            {
                this.MostrarError(ex.Message);
            }

            this.LimpiarControles(Page.Controls);
            this.Bind();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            _wrapperError.Style.Add("display", "none");
            
            try
            {
                _controladora.Editar(txtNombre.Text, Convert.ToInt32(idEdicion.Value), Convert.ToInt32(ddlCiudades.SelectedValue), listaIdsSeleccionados());
                this.Bind();
            }
            catch (Exception ex)
            {
                this.MostrarError(ex.Message);
            }

            DesmarcarCheckBoxes();
            this.LimpiarControles(Page.Controls);
        }

        protected void grdEstablecimientos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            _wrapperError.Style.Add("display", "none");

            try
            {
                int id = Convert.ToInt32(grdEstablecimientos.DataKeys[e.RowIndex].Value.ToString());
                _controladora.Borrar(id);
                this.Bind();
            }
            catch (Exception ex)
            {
                this.MostrarError(ex.Message);
            }
        }

        protected void grdEstablecimientos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            _wrapperError.Style.Add("display", "none");

            try
            {
                int id = Convert.ToInt32(grdEstablecimientos.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value.ToString());

                switch (e.CommandName.ToString())
                {
                    case "comandoEdicion":
                        Establecimiento oEstablecimiento = _controladora.BuscarPorId(id);
                        txtNombre.Text = oEstablecimiento.Nombre;
                        ddlCiudades.SelectedValue = oEstablecimiento.Ciudad.Id.ToString();
                        idEdicion.Value = id.ToString();
                        marcarCheckBoxes(oEstablecimiento.ListaNiveles);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "none", "<script>$('#carga').modal('show');</script>", false);
                        break;

                    case "comandoBorrado":
                        _controladora.Borrar(id);
                        this.Bind();
                        break;

                    case "comandoRestitucion":
                        _controladora.Restituir(id);
                        this.Bind();
                        break;
                }

            }
            catch (Exception ex)
            {
                this.MostrarError(ex.Message);
            }
        }

        protected void grdEstablecimientos_RowDataBound(object sender, GridViewRowEventArgs e)
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
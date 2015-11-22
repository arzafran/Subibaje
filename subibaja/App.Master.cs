using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controladoras;
using Modelos;

namespace subibaja
{
    public partial class App : System.Web.UI.MasterPage
    {
        Usuario _usuario;
        ControlPermisos _controladora = new ControlPermisos();

        protected void Page_Load(object sender, EventArgs e)
        {
            _usuario = (Usuario)Session["usuario"];
            ListItem li;
            
            if (_controladora.TieneRol(_usuario.Id, 1))
            {
                
            }

            if (_controladora.TieneRol(_usuario.Id, 2))
            {
                li = new ListItem();
                li.Enabled = true;
                li.Value = "Ciudades.aspx";
                li.Text = "Ciudades";
                listaLinks.Items.Add(li);

                li = new ListItem();
                li.Enabled = true;
                li.Value = "Establecimientos.aspx";
                li.Text = "Establecimientos";
                listaLinks.Items.Add(li);

                li = new ListItem();
                li.Enabled = true;
                li.Value = "Urbanos.aspx";
                li.Text = "Lineas Urbanas";
                listaLinks.Items.Add(li);

                li = new ListItem();
                li.Enabled = true;
                li.Value = "NivelesEducativos.aspx";
                li.Text = "Niveles";
                listaLinks.Items.Add(li);

                li = new ListItem();
                li.Enabled = true;
                li.Value = "Provincias.aspx";
                li.Text = "Provincias";
                listaLinks.Items.Add(li);

                li = new ListItem();
                li.Enabled = true;
                li.Value = "Roles.aspx";
                li.Text = "Roles";
                listaLinks.Items.Add(li);

                li = new ListItem();
                li.Enabled = true;
                li.Value = "TipoRoles.aspx";
                li.Text = "Tipos de roles";
                listaLinks.Items.Add(li);

                li = new ListItem();
                li.Enabled = true;
                li.Value = "Usuarios.aspx";
                li.Text = "Usuarios";
                listaLinks.Items.Add(li);
            }

            if (_controladora.TieneRol(_usuario.Id, 3))
            {
                li = new ListItem();
                li.Enabled = true;
                li.Value = "Estudiantes.aspx";
                li.Text = "Estudiantes";
                listaLinks.Items.Add(li);
            }

            li = new ListItem();
            li.Enabled = true;
            li.Value = "Password.aspx";
            li.Text = "Cambiar Password";
            listaLinks.Items.Add(li);

            li = new ListItem();
            li.Enabled = true;
            li.Value = "Logout.aspx";
            li.Text = "Logout";
            listaLinks.Items.Add(li);
        }
    }
}
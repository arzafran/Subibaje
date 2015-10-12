using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;

namespace subibaja.ClasesBase
{
    public class Pagina : System.Web.UI.Page
    {
        /**
         * Por si quiero iterar solo sobre el panel
         * ContentPlaceHolder cp = (ContentPlaceHolder)this.Master.FindControl("modalCarga");
         **/

        protected void GenerarColumnas(GridView gv)
        {
            if (gv.Columns.Count == 0)
            {
                Type tipoCompleto = gv.DataSource.GetType().GetGenericArguments()[0];
                BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
                PropertyInfo[] properties = tipoCompleto.GetProperties(flags);
                string[] palabras = tipoCompleto.ToString().Split('.');
                string tipo = palabras.Last();
                
                foreach (PropertyInfo property in properties)
                {
                    var oTipo = property.PropertyType;
                    if (!(oTipo.IsGenericType && (oTipo.GetGenericTypeDefinition() == typeof(List<>))))
                    {
                        BoundField bf = new BoundField();
                        bf.HeaderText = property.Name;
                        bf.DataField = property.Name;
                        gv.Columns.Add(bf);
                    }
                }
                HyperLinkField enlaceEdicion = new HyperLinkField();
                enlaceEdicion.Text = "<span class='glyphicon glyphicon-edit'></span>";
                enlaceEdicion.DataNavigateUrlFields = new string[] { "Id" };
                enlaceEdicion.DataNavigateUrlFormatString = "Edit/" + tipo + "?Id={0}";
                gv.Columns.Add(enlaceEdicion);
                HyperLinkField enlaceBorrado = new HyperLinkField();
                enlaceBorrado.Text = "<span class='glyphicon glyphicon-remove'></span>";
                enlaceBorrado.DataNavigateUrlFields = new string[] { "Id" };
                enlaceBorrado.DataNavigateUrlFormatString = "Remove/" + tipo + "?Id={0}";
                gv.Columns.Add(enlaceBorrado);
            }
        }

        protected void LimpiarControles(ControlCollection cc)
        {
            foreach (Control c in cc)
            {
                if (c is System.Web.UI.WebControls.TextBox)
                    (c as TextBox).Text = String.Empty;
                else if (c.Controls.Count > 0)
                    LimpiarControles(c.Controls);
            }
        }
    }
}
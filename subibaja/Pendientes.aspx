<%@ Page Title="" Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true" CodeBehind="Pendientes.aspx.cs" Inherits="subibaja.Pendientes" %>

<asp:Content ID="muestraPendientes" ContentPlaceHolderID="contenido" runat="server">
    <h2>Estudiantes pendientes</h2>
    <hr/>
    <asp:GridView EmptyDataText="No hay estudiantes pendientes de activación"
        CssClass="table table-condensed table-hover sortable" ID="grdPendientes" runat="server" 
        GridLines="None" AutoGenerateColumns="false" DataKeyNames="id" 
        onrowcommand="grdPendientes_RowCommand" OnRowDataBound="grdPendientes_RowDataBound">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="Id" />
            <asp:BoundField DataField="Usuario" HeaderText="Estudiante" />
            <asp:TemplateField HeaderText="DNI">
                <itemtemplate>
                    <%#DataBinder.Eval(Container.DataItem, "Usuario.Dni")%>
                </itemtemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="EMAIL">
                <itemtemplate>
                    <%#DataBinder.Eval(Container.DataItem, "Usuario.Email")%>
                </itemtemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="borrado" HeaderText="Ingresado" />

            <asp:TemplateField ItemStyle-Width="40px">
                <ItemTemplate>
                    <asp:LinkButton CssClass="mentira" OnClientClick="return confirm('Estás seguro?');" ID="linkBorrado" runat="server" Text="<span class='glyphicon glyphicon-repeat'></span>" CommandName="comandoRestitucion" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
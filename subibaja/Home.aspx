<%@ Page Title="Subibaje :: Password" Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="subibaja.Password" %>
<%@ Register TagPrefix="mc" Namespace="subibaja.Controles" Assembly="subibaja" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">
    <h2>Cambiar Password</h2>
    <hr/>
    <asp:Image runat="server" ID="imagenQr" Visible="false"/>
    <div class="col-md-6 col-md-offset-3">
        <div class="form-group">
            <label class="col-sm-4 control-label" for="txtPassViejo">Password Anterior:</label>
            <div class="col-sm-8">
                <asp:TextBox TextMode="Password" autocomplete="off" CssClass="form-control" ID="txtPassViejo" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-4 control-label" for="txtPassNuevo">Password nueva:</label>
            <div class="col-sm-8">
                <asp:TextBox TextMode="Password" autocomplete="off" CssClass="form-control" ID="txtPassNuevo" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-4 control-label" for="txtPassNuevo2">Repetir nueva:</label>
            <div class="col-sm-8">
                <asp:TextBox TextMode="Password" autocomplete="off" CssClass="form-control" ID="txtPassNuevo2" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-12 text-right">
                <mc:ModernButton runat="server" ID="btnCambiar" CssClass="btn btn-success" 
                    onclick="btnCambiar_Click" >
                    <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                </mc:ModernButton>
            </div>
        </div>
    </div>
    <asp:Panel ID="panelGrid" CssClass="col-sm-6 col-sm-offset-3" runat="server">
        <asp:GridView Visible="false" EmptyDataText="No hay boletos para este estudiante"
            CssClass="table table-condensed table-hover sortable" ID="grdBoletos" runat="server" 
            GridLines="None" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="Id" />
                <asp:TemplateField HeaderText="Linea">
                    <itemtemplate>
                        <%#DataBinder.Eval(Container.DataItem, "Linea.Linea")%>
                    </itemtemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="fecha" HeaderText="Fecha" />
            </Columns>
        </asp:GridView>
    </asp:Panel>
</asp:Content>

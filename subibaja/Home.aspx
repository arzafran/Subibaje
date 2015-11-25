<%@ Page Title="Subibaje :: Password" Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="subibaja.Password" %>
<%@ Register TagPrefix="mc" Namespace="subibaja.Controles" Assembly="subibaja" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="server">
    <asp:ValidationSummary runat="server" ID="summary"
        DisplayMode="BulletList"
        ShowMessageBox="False" ShowSummary="True" CssClass="alert alert-danger" />
    <h2>Home</h2>
    <hr/>
    <div class="col-sm-5">
        <h4>Cambiar Password<asp:CompareValidator
                        ID="comparaPass" runat="server" 
                        ErrorMessage="Password nueva y su repetición deben coincidir" 
                        ControlToCompare="txtPassNuevo2" ControlToValidate="txtPassNuevo">*</asp:CompareValidator></h4>
        <hr />
        <div class="form-group">
            <label class="col-sm-5 control-label" for="txtPassViejo">Password Anterior:</label>
            <div class="col-sm-6">
                <asp:TextBox TextMode="Password" autocomplete="off" CssClass="form-control" ID="txtPassViejo" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-1">
                <asp:RequiredFieldValidator ID="requiredPassViejo" runat="server" CssClass="validadores"
                    ErrorMessage="El password anterior es obligatorio" SetFocusOnError="True"
                    ControlToValidate="txtPassViejo">*</asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-5 control-label" for="txtPassNuevo">Password nueva:</label>
            <div class="col-sm-6">
                <asp:TextBox TextMode="Password" autocomplete="off" CssClass="form-control" ID="txtPassNuevo" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-1">
                <asp:RequiredFieldValidator ID="requiredPassNuevo" runat="server" CssClass="validadores"
                    ErrorMessage="El password nuevo es obligatorio" SetFocusOnError="True"
                    ControlToValidate="txtPassNuevo">*</asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-5 control-label" for="txtPassNuevo2">Repetir nueva:</label>
            <div class="col-sm-6">
                <asp:TextBox TextMode="Password" autocomplete="off" CssClass="form-control" ID="txtPassNuevo2" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-1">
                <asp:RequiredFieldValidator ID="requiredRepetirPass" runat="server" CssClass="validadores"
                    ErrorMessage="La confirmación del nuevo password es obligatoria" SetFocusOnError="True"
                    ControlToValidate="txtPassNuevo2">*</asp:RequiredFieldValidator>
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
        <asp:Panel runat="server" cssclass="col-sm-6 col-sm-offset-3" ID="panelQr" Visible="false">
            <h4>Descarga tu QR</h4>
            <hr />
            <asp:Image runat="server" ID="imagenQr"/>
        </asp:Panel>
    </div>
    <asp:Panel Visible="false" ID="panelGrid" CssClass="col-sm-5 col-sm-offset-2" runat="server">
        <h4>Listado de boletos</h4>
        <hr />
        <asp:GridView EmptyDataText="No hay boletos para este estudiante"
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

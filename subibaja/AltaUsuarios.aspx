<%@ Page Title="Subibaje :: Usuarios" Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true" CodeBehind="AltaUsuarios.aspx.cs" Inherits="subibaja.AltaUsuarios" %>
<%@ Register TagPrefix="mc" Namespace="subibaja.Controles" Assembly="subibaja" %>

<asp:Content ID="altaUsuarios" ContentPlaceHolderID="contenido" Runat="Server">
    <h2>Alta de Usuarios</h2>
    <hr/>    
    <div class="form-group">
        <label class="control-label col-sm-2" for="txtNombre">Nombre:</label>
        <div class="col-sm-10">
            <asp:TextBox CssClass="form-control" ID="txtNombre" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <label class="control-label col-sm-2" for="txtDni">DNI:</label>
        <div class="col-sm-10">
            <asp:TextBox CssClass="form-control" ID="txtDni" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <label class="control-label col-sm-2" for="txtEmail">Email:</label>
        <div class="col-sm-10">
            <asp:TextBox CssClass="form-control" ID="txtEmail" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <label class="control-label col-sm-2" for="txtPassword">Password:</label>
        <div class="col-sm-10">
            <asp:TextBox CssClass="form-control" ID="txtPassword" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <label class="control-label col-sm-2" for="txtPassord2">Confirmar password</label>
        <div class="col-sm-10">
            <asp:TextBox CssClass="form-control" ID="txtPassword2" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <div class="col-sm-offset-2 col-sm-10">
            <mc:ModernButton runat="server" ID="btnAgregar" CssClass="btn btn-info" >
                Search
            </mc:ModernButton>
        </div>
    </div>
    <div>
        <asp:GridView ID="grdUsuarios" runat="server">
        </asp:GridView>
    </div>
</asp:Content>
<%@ Page Title="Subibaje :: Establecimientos" Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true" CodeBehind="AltaEstablecimientos.aspx.cs" Inherits="subibaja.AltaEstablecimientos" %>
<%@ Register TagPrefix="mc" Namespace="subibaja.Controles" Assembly="subibaja" %>

<asp:Content ID="altaEstablecimientos" ContentPlaceHolderID="contenido" Runat="Server">
    <h2>Alta de establecimientos</h2>
    <hr/>
    <div class="form-group">
        <label class="col-sm-2 control-label" for="txtNombre">Nombre:</label>
        <div class="col-sm-10">
            <asp:TextBox CssClass="form-control" ID="txtNombre" runat="server"></asp:TextBox>
        </div>
    </div>
    <asp:Panel ID="divCheckbox" runat="server">
        <asp:CheckBoxList ID="CheckList1" DataTextField="Descripcion" DataValueField="Id" runat="server">
        </asp:CheckBoxList>
    </asp:Panel>
    <div class="form-group">
        <div class="col-sm-offset-2 col-sm-10">
            <mc:ModernButton runat="server" ID="btnAgregar" CssClass="btn btn-info" >
                Agregar
            </mc:ModernButton>
        </div>
    </div>
    <div>
        <asp:GridView ID="grdEstablecimientos" runat="server">
        </asp:GridView>
    </div>
</asp:Content>
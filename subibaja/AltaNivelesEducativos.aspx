<%@ Page Title="Subibaje :: Niveles" Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true" CodeBehind="AltaNivelesEducativos.aspx.cs" Inherits="subibaja.AltaNivelEducativo" %>
<%@ Register TagPrefix="mc" Namespace="subibaja.Controles" Assembly="subibaja" %>

<asp:Content ID="altaNiveles" ContentPlaceHolderID="contenido" Runat="Server">
    <div class="form-group">
        <label class="col-sm-2 control-label" for="txtDescripcion">Descripcion:</label>
        <div class="col-sm-10">
            <asp:TextBox ID="txtDescripcion" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <div class="col-sm-offset-2 col-sm-10">
            <mc:ModernButton runat="server" ID="btnAgregar" CssClass="btn btn-info" >
                Agregar
            </mc:ModernButton>
        </div>
    </div>
    <div>
        <asp:GridView ID="grdNiveles" runat="server">
        </asp:GridView>
    </div>
</asp:Content>
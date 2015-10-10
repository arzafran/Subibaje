<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AltaNivelesEducativos.aspx.cs" Inherits="subibaja.AltaNivelEducativo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/app.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            Descripción: 
            <asp:TextBox ID="txtDescripcion" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" 
                onclick="Agregar_Click" />
        </div>
        <div>
            <asp:GridView ID="grdNiveles" runat="server">
            </asp:GridView>
        </div>
    </div>
    <asp:Label ID="lblPrueba" runat="server" Text="Label"></asp:Label>
    </form>
</body>
</html>

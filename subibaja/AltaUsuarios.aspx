<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AltaUsuarios.aspx.cs" Inherits="subibaja.AltaUsuarios" %>

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
            Nombre: 
            <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
        </div>
        <div>
            DNI: 
            <asp:TextBox ID="txtDni" runat="server"></asp:TextBox>
        </div>
        <div>
            Email:
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
        </div>
        <div>
            Password: 
            <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
        </div>
        <div>
            Confirmar password:
            <asp:TextBox ID="txtPassword2" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" 
                onclick="Agregar_Click" />
        </div>
    </div>
    <div>
        <asp:GridView ID="grdUsuarios" runat="server">
        </asp:GridView>
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AltaEstablecimientos.aspx.cs" Inherits="subibaja.AltaEstablecimientos" %>

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
        <asp:Panel ID="divCheckbox" runat="server">
            <asp:CheckBoxList ID="CheckList1" DataTextField="Descripcion" DataValueField="Id" runat="server">
            </asp:CheckBoxList>
        </asp:Panel>
        <div>
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" 
                onclick="btnAgregar_Click" />
        </div>
        <div>
            <asp:GridView ID="grdEstablecimientos" runat="server">
            </asp:GridView>
        </div>
    </div>
    
    <asp:Label ID="lblPrueba" runat="server" Text="Label"></asp:Label>
    
    </form>
</body>
</html>

<%@ Page Title="Subibaje :: Tipo de Roles" Language="C#" AutoEventWireup="true" MasterPageFile="~/App.Master" CodeBehind="TipoRoles.aspx.cs" Inherits="subibaja.TipoRoles" %>
<%@ Register TagPrefix="mc" Namespace="subibaja.Controles" Assembly="subibaja" %>

<asp:Content ID="muestraRoles" ContentPlaceHolderID="contenido" Runat="Server">
    <h2>Roles</h2>
    <hr/>
    <button id="btnMas" type="button" class="btn btn-fab btn-material-pink" data-toggle="modal" data-target="#carga">
        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
    </button>
    <asp:GridView EmptyDataText="No hay roles cargados"
        CssClass="table table-condensed table-hover sortable" ID="grdRoles" runat="server" 
        GridLines="None" AutoGenerateColumns="false" DataKeyNames="id" 
        onrowcommand="grdRoles_RowCommand" OnRowDataBound="grdRoles_RowDataBound">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="Id" />
            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="borrado" HeaderText="Borrado" />

            <asp:TemplateField ItemStyle-Width="40px">
                <ItemTemplate>
                    <asp:LinkButton ID="linkEdicion" runat="server" CssClass="edicion" Text="<span class='glyphicon glyphicon-pencil'></span>" CommandName="comandoEdicion" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField ItemStyle-Width="40px">
                <ItemTemplate>
                    <asp:LinkButton OnClientClick="return confirm('Estás seguro?');" ID="linkBorrado" runat="server" Text="<span class='glyphicon glyphicon-remove'></span>" CommandName="comandoBorrado" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>

<asp:Content ID="cargaRoles" ContentPlaceHolderID="modalCarga" Runat="Server">
    <asp:HiddenField ID="idEdicion" runat="server" />
    <div class="modal fade" id="carga" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">Agregar Rol</h4>
                </div>
                <div class="modal-body">
                    <asp:ValidationSummary runat="server" ID="summary"
                        DisplayMode="BulletList"
                        ShowMessageBox="False" ShowSummary="True" CssClass="alert alert-danger" />
                    <div class="form-group">
                        <label class="col-sm-2 control-label" for="txtNombre">Nombre:</label>
                        <div class="col-sm-9">
                            <asp:TextBox autocomplete="off" CssClass="form-control" ID="txtNombre" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-sm-1">
                            <asp:RequiredFieldValidator ID="requiredNombre" runat="server" CssClass="validadores"
                                ErrorMessage="El nombre es obligatorio" SetFocusOnError="True"
                                ControlToValidate="txtNombre">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-12 text-right">
                            <mc:ModernButton runat="server" ID="btnEditar" CssClass="btn btn-warning" 
                                onclick="btnEditar_Click" >
                                <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                            </mc:ModernButton>
                            <mc:ModernButton runat="server" ID="btnAgregar" CssClass="btn btn-success" 
                                onclick="btnAgregar_Click" >
                                <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                            </mc:ModernButton>
                            <button type="button" class="btn btn-danger" data-dismiss="modal">
                                <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
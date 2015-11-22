<%@ Page Title="Subibaje :: Roles" Language="C#" AutoEventWireup="true" MasterPageFile="~/App.Master" CodeBehind="Roles.aspx.cs" Inherits="subibaja.Roles" %>
<%@ Register TagPrefix="mc" Namespace="subibaja.Controles" Assembly="subibaja" %>

<asp:Content ID="muestraRoles" ContentPlaceHolderID="contenido" Runat="Server">
    <h2>Roles</h2>
    <hr/>
    <button id="btnMas" type="button" class="btn btn-fab btn-material-pink" data-toggle="modal" data-target="#carga">
        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
    </button>
    <asp:GridView EmptyDataText="No hay roles cargados para este usuario"
        CssClass="table table-condensed table-hover sortable" ID="grdRoles" runat="server" 
        GridLines="None" AutoGenerateColumns="false" DataKeyNames="id" 
        onrowcommand="grdRoles_RowCommand" OnRowDataBound="grdRoles_RowDataBound">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="Id" />
            <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
            <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
            <asp:BoundField DataField="Establecimiento" HeaderText="Establecimiento" />
            <asp:BoundField DataField="Nivel" HeaderText="Nivel educativo" />
            <asp:BoundField DataField="borrado" HeaderText="Borrado" />

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
                    <div class="form-group">
                        <label class="col-sm-2 control-label" for="ddlTipo">Tipo:</label>
                        <div class="col-sm-10">
                            <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label" for="ddlEstablecimiento">Establecimiento:</label>
                        <div class="col-sm-10">
                            <asp:DropDownList ID="ddlEstablecimiento" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label" for="ddlNivel">Nivel:</label>
                        <div class="col-sm-10">
                            <asp:DropDownList ID="ddlNivel" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    
                    <div class="form-group">
                        <div class="col-sm-12 text-right">
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

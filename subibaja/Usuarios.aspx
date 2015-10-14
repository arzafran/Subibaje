<%@ Page Title="Subibaje :: Usuarios" Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="subibaja.Usuarios" %>
<%@ Register TagPrefix="mc" Namespace="subibaja.Controles" Assembly="subibaja" %>

<asp:Content ID="muestraUsuarios" ContentPlaceHolderID="contenido" Runat="Server">
    <h2>Usuarios</h2>
    <hr/>
    <button type="button" class="btn btn-info btn-add" data-toggle="modal" data-target="#carga">
        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
    </button>
    <asp:GridView EmptyDataText="No hay usuarios cargados" 
        CssClass="table table-condensed table-hover" ID="grdUsuarios" runat="server" 
        GridLines="None" AutoGenerateColumns="false">
    </asp:GridView>
</asp:Content>

<asp:Content ID="cargaUsuarios" ContentPlaceHolderID="modalCarga" Runat="Server">
    <div class="modal fade" id="carga" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">Agregar Ciudad</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label class="control-label col-sm-2" for="txtNombre">Nombre:</label>
                        <div class="col-sm-10">
                            <asp:TextBox autocomplete="off" CssClass="form-control" ID="txtNombre" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-2" for="txtDni">DNI:</label>
                        <div class="col-sm-10">
                            <asp:TextBox autocomplete="off" CssClass="form-control" ID="txtDni" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-2" for="txtEmail">Email:</label>
                        <div class="col-sm-10">
                            <asp:TextBox autocomplete="off" CssClass="form-control" ID="txtEmail" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-2" for="txtPassword">Password:</label>
                        <div class="col-sm-10">
                            <asp:TextBox autocomplete="off" CssClass="form-control" ID="txtPassword" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-2" for="txtPassord2">Confirmar password</label>
                        <div class="col-sm-10">
                            <asp:TextBox autocomplete="off" CssClass="form-control" ID="txtPassword2" runat="server"></asp:TextBox>
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

<%@ Page Title="Subibaje :: Ciudades" Language="C#" AutoEventWireup="true" MasterPageFile="~/App.Master" CodeBehind="Ciudades.aspx.cs" Inherits="subibaja.Ciudades" %>
<%@ Register TagPrefix="mc" Namespace="subibaja.Controles" Assembly="subibaja" %>

<asp:Content ID="muestraCiudades" ContentPlaceHolderID="contenido" Runat="Server">
    <h2 class="cabecera-titulo">Ciudades</h2>
    <hr class="separa-titulo" />
    <button type="button" class="btn btn-info btn-agregar" data-toggle="modal" data-target="#carga">
        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
    </button>
    <asp:GridView EmptyDataText="No hay ciudades cargadas" 
        CssClass="table table-condensed table-hover" ID="grdCiudades" runat="server" 
        GridLines="None" AutoGenerateColumns="false">
    </asp:GridView>
</asp:Content>

<asp:Content ID="cargaCiudades" ContentPlaceHolderID="modalCarga" Runat="Server">
    <div class="modal fade" id="carga" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">Agregar Ciudad</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label class="col-sm-2 control-label" for="txtNombre">Descripcion:</label>
                        <div class="col-sm-10">
                            <asp:TextBox autocomplete="off" CssClass="form-control" ID="txtNombre" runat="server"></asp:TextBox>
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
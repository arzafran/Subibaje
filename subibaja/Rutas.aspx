﻿<%@ Page Title="Subibaje :: Rutas" Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true" CodeBehind="Rutas.aspx.cs" Inherits="subibaja.Rutas" %>
<%@ Register TagPrefix="mc" Namespace="subibaja.Controles" Assembly="subibaja" %>

<asp:Content ID="muestraRutas" ContentPlaceHolderID="contenido" Runat="Server">
    <h2>Rutas</h2>
    <hr/>
    <button type="button" class="btn btn-info btn-add" data-toggle="modal" data-target="#carga">
        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
    </button>
    <asp:GridView EmptyDataText="No hay servicios urbanos cargados" 
        CssClass="table table-condensed table-hover" ID="grdUrbanos" runat="server" 
        GridLines="None" AutoGenerateColumns="false">
    </asp:GridView>
</asp:Content>

<asp:Content ID="cargaRutas" ContentPlaceHolderID="modalCarga" Runat="Server">
    <div class="modal fade" id="carga" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">Agregar ruta</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label class="col-sm-2 control-label" for="txtNombre">Nombre:</label>
                        <div class="col-sm-10">
                            <asp:TextBox autocomplete="off" CssClass="form-control" ID="txtNombre" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label" for="ddlOrigen">Origen:</label>
                        <div class="col-sm-10">
                            <asp:DropDownList ID="ddlOrigen" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label" for="ddlDestino">Destino:</label>
                        <div class="col-sm-10">
                            <asp:DropDownList ID="ddlDestino" runat="server" CssClass="form-control">
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
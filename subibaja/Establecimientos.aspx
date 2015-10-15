<%@ Page Title="Subibaje :: Establecimientos" Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true" CodeBehind="Establecimientos.aspx.cs" Inherits="subibaja.Establecimientos" %>
<%@ Register TagPrefix="mc" Namespace="subibaja.Controles" Assembly="subibaja" %>

<asp:Content ID="muestraEstablecimientos" ContentPlaceHolderID="contenido" Runat="Server">
    <h2>Establecimientos</h2>
    <hr/>
    <button type="button" class="btn btn-fab btn-material-pink" data-toggle="modal" data-target="#carga">
        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
    </button>
    <asp:GridView EmptyDataText="No hay establecimientos cargados" 
        CssClass="table table-condensed table-hover" ID="grdEstablecimientos" runat="server" 
        GridLines="None" AutoGenerateColumns="False">
    </asp:GridView>
</asp:Content>

<asp:Content ID="cargaEstablecimientos" ContentPlaceHolderID="modalCarga" Runat="Server">
    <div class="modal fade" id="carga" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">Agregar Establecimiento</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label class="col-sm-2 control-label" for="txtNombre">Descripcion:</label>
                        <div class="col-sm-10">
                            <asp:TextBox autocomplete="off" CssClass="form-control" ID="txtNombre" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label" for="ddlCiudad">Ciudad:</label>
                        <div class="col-sm-10">
                            <asp:DropDownList ID="ddlCiudad" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Panel CssClass="col-sm-10 col-sm-offset-2" ID="Panel1" runat="server">
                            <asp:CheckBoxList ID="ckNiveles" DataTextField="Descripcion" DataValueField="Id" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-12 text-right">
                            <mc:ModernButton runat="server" ID="btnAgregar" CssClass="btn btn-success" 
                                onclick="btnAgregar_Click1" >
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
<%@ Page Title="Subibaje :: Estudiantes" Language="C#" AutoEventWireup="true" MasterPageFile="~/App.Master" CodeBehind="Estudiantes.aspx.cs" Inherits="subibaja.Estudiantes" %>
<%@ Register TagPrefix="mc" Namespace="subibaja.Controles" Assembly="subibaja" %>

<asp:Content ID="muestraEstudiantes" ContentPlaceHolderID="contenido" Runat="Server">
    <h2>Estudiantes</h2>
    <hr/>
    <button id="btnMas" type="button" class="btn btn-fab btn-material-pink" data-toggle="modal" data-target="#carga">
        <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
    </button>
    <asp:GridView EmptyDataText="No hay estudiantes cargados"
        CssClass="table table-condensed table-hover sortable" ID="grdEstudiantes" runat="server" 
        GridLines="None" AutoGenerateColumns="false" DataKeyNames="id" 
        onrowcommand="grdEstudiantes_RowCommand" OnRowDataBound="grdEstudiantes_RowDataBound">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="Id" />
            <asp:BoundField DataField="Usuario" HeaderText="Estudiante" />
            <asp:TemplateField HeaderText="DNI">
                <itemtemplate>
                    <%#DataBinder.Eval(Container.DataItem, "Usuario.Dni")%>
                </itemtemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="EMAIL">
                <itemtemplate>
                    <%#DataBinder.Eval(Container.DataItem, "Usuario.Email")%>
                </itemtemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="borrado" HeaderText="Ingresado" />

            <asp:TemplateField ItemStyle-Width="40px">
                <ItemTemplate>
                    <asp:LinkButton CssClass="mentira" OnClientClick="return confirm('Estás seguro?');" ID="linkBorrado" runat="server" Text="<span class='glyphicon glyphicon-remove'></span>" CommandName="comandoBorrado" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>

<asp:Content ID="cargaEstudiantes" ContentPlaceHolderID="modalCarga" Runat="Server">
    <asp:HiddenField ID="idEdicion" runat="server" />
    <div class="modal fade" id="carga" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">Agregar Estudiante</h4>
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
                        <label class="col-sm-2 control-label" for="txtDni">DNI:</label>
                        <div class="col-sm-9">
                            <asp:TextBox autocomplete="off" CssClass="form-control" ID="txtDni" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-sm-1">
                            <asp:RequiredFieldValidator ID="requiredDni" runat="server" CssClass="validadores"
                                ErrorMessage="El DNI es obligatorio" SetFocusOnError="True"
                                ControlToValidate="txtDni">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label" for="txtEmail">Email:</label>
                        <div class="col-sm-9">
                            <asp:TextBox autocomplete="off" CssClass="form-control" ID="txtEmail" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-sm-1">
                            <asp:RequiredFieldValidator ID="requiredEmail" runat="server" CssClass="validadores"
                                ErrorMessage="El email es obligatorio" SetFocusOnError="True"
                                ControlToValidate="txtEmail">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-12 text-right">
                            <mc:ModernButton runat="server" ID="btnAgregar" CssClass="btn btn-success" 
                                onclick="btnAgregar_Click" ><span class="glyphicon glyphicon-ok" aria-hidden="true"></span></mc:ModernButton>
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
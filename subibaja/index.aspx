<%@ Page Title="Subibaje :: Inicio" Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="subibaja.index" %>
<asp:Content ID="index" ContentPlaceHolderID="contenido" Runat="Server">
    <img class="imagenMenu titilar" src="img/menu.PNG" />
    <img class="imagenInicio titilar" src="img/inicio.PNG" />
    <img class="imagenOpciones" src="img/opciones.PNG" />
    <h1 class="text-center">BIENVENIDO</h1>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $titilar = $('.titilar');
            $opciones = $('.imagenOpciones');
            $titilar.fadeOut(700).fadeIn(700).fadeOut(700).fadeIn(700);
            $('#trigger').on('click', function () {
                $titilar.fadeToggle();
                $opciones.fadeToggle();
            });
        });
    </script>
</asp:Content>
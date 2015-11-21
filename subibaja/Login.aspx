<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="subibaja.Login" %>
<%@ Register TagPrefix="mc" Namespace="subibaja.Controles" Assembly="subibaja" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Subibaje :: Login</title>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link href="app/css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/login.css" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" href="/img/updown.png" type="image/png" />
    <style type="text/css">
        body 
        {
            display:block;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="app" class="container-fluid">
        	<div class="row">
				<div class="login-box">
					<div class="login-body">
						<div class="login-hora">
                            <asp:Label ID="lblError" runat="server" Text=":: Login ::"></asp:Label>
						</div>
						<div class="circulo">
							<img src="/img/updown.png">
						</div>
					</div>
					<div class="login-form">
							<div class="form-group">
								<label for="email">E-Mail</label>
                                <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
								<span class="bar"></span>
							</div>
							<div class="form-group">
								<label for="password">Password</label>
                                <asp:TextBox ID="txtPassword" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
								<span class="bar"></span>
							</div>
							<div class="form-group">
                                <mc:ModernButton runat="server" ID="btnAgregar"
                                onclick="btnAgregar_Click" >
                                    <span class="glyphicon glyphicon-ok" aria-hidden="true"></span>
                                </mc:ModernButton>
							</div>	
					</div>
				</div>
			</div>
        </div>
        <!-- SCRIPTS -->
        <!--<script src="app/js/app.js" type="text/javascript"></script>-->
<!--        <script type"text/javascript">
            $(document).ready(function () {
                $('body').show();
            });
        </script>
        <script type="text/javascript">
            $(function () {
                var d = new Date();
                var h = d.getHours();
                var m = ('0' + d.getMinutes()).slice(-2);
                var ampm = h > 12 ? " PM" : " AM";
                $('.login-hora').html(h + ':' + m + ampm);
            });
		</script>-->
    </form>
</body>
</html>
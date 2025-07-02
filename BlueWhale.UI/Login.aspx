<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BlueWhale.UI.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>BlueWhale ERP System</title>
    <link href="skin/default/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="scripts/jquery/jquery-1.10.2.min.js"></script>
    <link href="favicon.ico" rel="icon" type="image/x-icon" />
    <script type="text/javascript">
        $(function () {
            //Detect IE
            if ('undefined' == typeof (document.body.style.maxHeight)) {
                window.location.href = 'ie6update.html';
            }
        });
        window.setInterval("refreshPage()", 180000); 
        function refreshPage() {
            window.location.reload();
        }
    </script>
    </head>
    <body style="background: url(images/login_bg.png) no-repeat;background-position: center;background-attachment: fixed;">
        <form id="form1" runat="server">
            <div class="login-title">
               <a href="Login.aspx" style=" text-decoration:none;">
                <img src="images/logo.png" alt="BlueWhale ERP" height="80px" />
               </a>
            </div>
            <div class="login-screen">
                <div class="login-form">
                    <h1>Login</h1>
                    <div class="control-group">
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="login-field" Text="" placeholder="Account" title="Account"></asp:TextBox>
                        <label class="login-field-icon user" for="txtUserName"></label>
                    </div>
                    <div class="control-group">
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="login-field" Text="" TextMode="Password" placeholder="Password" title="Password"></asp:TextBox>
                        <label class="login-field-icon pwd" for="txtPassword"></label>
                    </div>
                       <div class="control-group">
                        <asp:TextBox ID="captcha" runat="server" CssClass="login-field" Width="230px" Text="" placeholder="Captcha" title="Captcha"></asp:TextBox>
                          <asp:Image ID="Image3" src="Captcha.aspx" ToolTip="Click to refresh captcha" style="cursor:pointer;" 
                              onclick="this.src='Captcha.aspx?id='+Math.random()" runat="server" Height="35px" Width="60px" ImageAlign="Top"  />
                        </div>
                    <div>
                        <asp:Button ID="btnSubmit" runat="server" Text="Login" CssClass="btn-login" onclick="BtnSubmit_Click"/>
                    </div>
                </div>
            </div>
        </form>
    </body>
</html>
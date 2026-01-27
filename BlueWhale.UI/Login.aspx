<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BlueWhale.UI.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>BlueWhale ERP System</title>
    <link href="favicon.ico" rel="icon" type="image/x-icon" />
    <link href="https://fonts.googleapis.com/css2?family=Nunito+Sans&display=swap" rel="stylesheet"/>
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
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
    <style>
        body, body * {
            font-family: 'Nunito Sans', sans-serif !important;
        }

        input,
        button {
            font-family: 'Nunito Sans', sans-serif !important;
        }

        body {
            background: url('images/login_bg.png') no-repeat center fixed;
            background-size: cover;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }

        .login-container {
            backdrop-filter: blur(10px);
            background: rgba(255, 255, 255, 0.1);
            border: 1px solid rgba(255, 255, 255, 0.2);
            padding: 40px;
            border-radius: 12px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.3);
            width: 350px;
            text-align: center;
        }

        .login-container h1 {
            color: #f2f2f2;
            margin-bottom: 30px;
            letter-spacing: 2px;
        }

        .login-field {
            width: 93%;
            padding: 12px;
            margin: 10px 0;
            border: none;
            border-radius: 6px;
            background: rgba(255, 255, 255, 0.2);
            color: #fff;
            font-size: 14px;
        }

        .btn-login {
            width: 100%;
            padding: 12px;
            margin-top: 20px;
            background: #00b4ff;
            border: none;
            border-radius: 6px;
            color: #fff;
            font-size: 16px;
            cursor: pointer;
            transition: 0.3s;
        }

        input.login-field::placeholder {
            color: white;
            opacity: 0.7;
        }

        .btn-login:hover {
            background: #008ecf;
        }

        img.logo {
            height: 80px;
            margin-bottom: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <img src="images/logo.png" alt="BlueWhale ERP" class="logo" />
            <h1>Login</h1>
            <asp:TextBox ID="txtUserName" runat="server" CssClass="login-field" placeholder="Account"></asp:TextBox>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="login-field" TextMode="Password" placeholder="Password"></asp:TextBox>
            <div class="captcha-group">
                <asp:TextBox ID="captcha" runat="server" CssClass="login-field captcha-input" placeholder="Captcha"></asp:TextBox>
                <asp:Image ID="Image3" src="Captcha.aspx" ToolTip="Click to refresh captcha" Style="cursor: pointer;" onclick="this.src='Captcha.aspx?id='+Math.random()" runat="server" Height="40px" Width="100px" />
            </div>
            <asp:Button ID="btnSubmit" runat="server" Text="Login" CssClass="btn-login" OnClick="BtnSubmit_Click" />
        </div>
    </form>
</body>
</html>

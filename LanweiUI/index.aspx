<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Lanwei.Weixin.UI.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>蓝微·云ERP系统</title>
<link href="skin/default/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="scripts/jquery/jquery-1.10.2.min.js"></script>

    <link href="favicon.ico" rel="icon" type="image/x-icon" />


<script type="text/javascript">
    $(function () {
        //检测IE
        if ('undefined' == typeof (document.body.style.maxHeight)) {
            window.location.href = 'ie6update.html';
        }
    });

    window.setInterval("updateYZM()", 180000); //三分钟定时刷新页面，防止验证码过期
    function updateYZM() {
        window.location.reload();

    }

    function check() {

        var names = document.getElementById("txtUserName").value;
        var pwd = document.getElementById("txtPassword").value;
        var yzm = document.getElementById("txtYZM").value;

        if (names == "") {
            alert("请输入手机号！");
            document.getElementById("txtUserName").focus();
            return false;

        }
        else {
            if (pwd == "") {
                alert("请输入密码！");
                document.getElementById("txtPassword").focus();
                return false;
            }
            else {
                if (yzm == "") {
                    alert("请输入验证码！");
                    document.getElementById("txtYZM").focus();
                    return false;
                }
                else {
                    return true;
                }
            }
        }

    }


</script>
</head>

<body style="background: url(images/login_bg.png) no-repeat;background-position: center;background-attachment: fixed;">


<form id="form1" runat="server">


    <div class="login-title">

       
        
       <a href="index.aspx" style=" text-decoration:none;">
     
        <img src="images/logo.png" alt="蓝微.云ERP"   height="80px" />
             </a>


    </div>

    



<div class="login-screen">
    <div class="login-form">
        <h1>登录</h1>
        <div class="control-group">
            <asp:TextBox ID="txtUserName" runat="server" CssClass="login-field" Text="" TextMode="Phone" placeholder="手机号码" title="手机号码"></asp:TextBox>
            <label class="login-field-icon user" for="txtUserName"></label>
        </div>
        <div class="control-group">
            <asp:TextBox ID="txtPassword" runat="server" CssClass="login-field" Text="123456" TextMode="Password" placeholder="登录密码" title="登录密码"></asp:TextBox>
            <label class="login-field-icon pwd" for="txtPassword"></label>
        </div>

           <div class="control-group">
            <asp:TextBox ID="txtYZM" runat="server" CssClass="login-field" Width="230px" Text="" placeholder="验证码" title="验证码"></asp:TextBox>


              <asp:Image ID="Image3" src="ValidataCode.aspx" ToolTip="点击刷新验证码" 
                            style="cursor:pointer;" 
                            onclick="this.src='ValidataCode.aspx?id='+Math.random()" runat="server" 
                            Height="35px" Width="60px"
                            ImageAlign="Top"  />

        </div>



        <div><asp:Button ID="btnSubmit" runat="server" Text="登 录" CssClass="btn-login" onclick="btnSubmit_Click" OnClientClick="return check()"/></div>
       
         <span class="login-tips" style="">
         
          

              
          

        </span>

         <span class="login-tips">
            
             技术支持：0512-68276837  QQ：931577807
               
        </span>


    </div>
    <%--<i class="arrow">箭头</i>--%>

</div>
</form>
</body>
</html>
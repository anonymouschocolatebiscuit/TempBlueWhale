<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pwd.aspx.cs" Inherits="Lanwei.Weixin.UI.Pwd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>修改密码</title>
    
    <link href="lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="lib/json2.js" type="text/javascript"></script>

   <script type="text/javascript">

       $(function () {

           var form = $("#form").ligerForm();

       });


   </script>


</head>
<body>
    <form id="form1" runat="server">
    
    
    <table id="form" border="0" cellpadding="0" cellspacing="10" style="width:680px; line-height:40px;">
    <tr>
    <td style="width:80px; text-align:right;">原密码：</td>
    <td>
    
    
    
        <asp:TextBox ID="TextBoxOld"  runat="server" TextMode="Password"></asp:TextBox>
         
    
    
    </td>
    
    </tr>
    
    
    <tr>
    <td style="text-align:right;">新密码：</td>
    <td>
    
    
        <asp:TextBox ID="TextBoxNews" runat="server" TextMode="Password"></asp:TextBox>
          
            
                </td>
    
    </tr>
    
    
    
    <tr>
    <td style="text-align:right;">确认密码：</td>
    <td>
    
    
        <asp:TextBox ID="TextBoxNews1" runat="server" TextMode="Password"></asp:TextBox>
       
            
                </td>
    
    </tr>
    
    
    
    <tr>
    <td style="text-align:right;">&nbsp;</td>
    <td style="text-align:left; padding-left:30px;">
    
    
        <asp:Button ID="ButtonUpdate" runat="server" class="ui_state_highlight" 
                  onclick="ButtonUpdate_Click" 
                  Text="修 改" />
    
     
    
    </td>
    
    </tr>
    
    
    
    </table>
    
    
    
    
   
    </form>
</body>
</html>

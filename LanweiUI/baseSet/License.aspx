<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="License.aspx.cs" Inherits="LanweiWeb.BaseSet.License" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>使用许可</title>
  <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
    <script type="text/javascript">
    
 
      $(function ()
        {
        
          var form = $("#form").ligerForm();
          
            var TextBox1 =  $.ligerui.get("TextBox1");
            TextBox1.set("Width", 350);
            
             var TextBox2 =  $.ligerui.get("TextBox2");
            TextBox2.set("Width", 350);
            
             var TextBox3 =  $.ligerui.get("TextBox3");
            TextBox3.set("Width", 350);
            
             var TextBox4 =  $.ligerui.get("TextBox4");
            TextBox4.set("Width", 350);
            
             var TextBox5 =  $.ligerui.get("TextBox5");
            TextBox5.set("Width", 350);
            
            
          
          });
          
        </script>
    
  
</head>
<body style="padding-top:10px;">
   
    <form id="form1" runat="server">
   
   
   
   <table id="form" style=" line-height:35px;" >
            <tr>
                <td style="width:100px;text-align:right;">
                    公司名称：</td>
                <td style="width:400px;">
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                   </td>
            </tr>
            <tr>
                <td style="width:100px;text-align:right;">
                    公司地址：</td>
                <td style="width:400px;">
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                   </td>
            </tr>
            <tr>
                <td style="width:100px;text-align:right;">
                    联系电话：</td>
                <td style="width:400px;">
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                   </td>
            </tr>
            <tr>
                <td style="width:100px;text-align:right;">
                    联系传真：</td>
                <td style="width:400px;">
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                                   </td>
            </tr>
            <tr>
                <td style="width:100px;text-align:right;">
                    邮政编码：</td>
                <td style="width:400px;">
                    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                                                   </td>
            </tr>
            <tr>
                <td style="width:100px;text-align:right;">
                    开通时间：</td>
                <td style="width:400px;">
                    <asp:TextBox ID="txtDateStart" runat="server"></asp:TextBox>
                                                   </td>
            </tr>
            <tr>
                <td style="width:100px;text-align:right;">
                    到期时间：</td>
                <td style="width:400px;">
                    <asp:TextBox ID="txtDateEnd" runat="server"></asp:TextBox>
                                                   </td>
            </tr>
            <tr>
                <td style="width:100px;text-align:right;">
                    许可用户：</td>
                <td style="width:400px;">
                    <asp:TextBox ID="txtUserNum" runat="server"></asp:TextBox>
                                                   </td>
            </tr>
            <tr>
                <td style="width:100px;text-align:right;">
                    短信余额：</td>
                <td style="width:400px;">
                    <asp:TextBox ID="txtMsgNum" runat="server"></asp:TextBox>
                                                   </td>
            </tr>
            </table>
   
   
   
    </form>
</body>
</html>

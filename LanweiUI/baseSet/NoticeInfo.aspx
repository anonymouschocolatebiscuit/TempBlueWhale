<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoticeInfo.aspx.cs" Inherits="LanweiWeb.BaseSet.NoticeInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>系统公告设置</title>
  <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
     
    
  
</head>
<body style="padding-top:10px;">
   
    <form id="form1" runat="server">
   
   
   <table id="sss" style=" line-height:35px;" >
            <tr>
                <td style="width:100px;text-align:right;">
                    公告内容：</td>
                <td style="width:400px;">
                    <asp:TextBox ID="txtContents" runat="server" Height="161px" 
                        TextMode="MultiLine" Width="254px"></asp:TextBox>
                                                   </td>
            </tr>
            <tr>
                <td style="width:150px;text-align:right;">
                    &nbsp;</td>
                <td style="width:400px;">
                    <asp:Button ID="btnSave" runat="server" CssClass="ui-btn ui-btn-sp mrb"
                        onclick="btnSave_Click" Text="保 存" />
                                                   </td>
            </tr>
            </table>
   
   
   
    </form>
</body>
</html>

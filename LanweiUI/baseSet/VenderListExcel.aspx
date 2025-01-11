<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VenderListExcel.aspx.cs" Inherits="Lanwei.Weixin.UI.BaseSet.VenderListExcel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>批量导入</title>
      <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
   <script type="text/javascript">
   
    $(function ()
        {
        
          var form = $("#form").ligerForm();
          
         });
   
   
   </script>
      
</head>
<body style=" padding:30px;">
    <form id="form1" runat="server">
    
     <table id="form" border="0" cellpadding="0" cellspacing="10" style="width:480px; line-height:40px;">
    <tr>
    <td style="text-align:left;" colspan="2">
    <b>批量导入供应商信息及初始余额</b>
    </td>
    </tr>
    <tr>
    <td style="width:80px; text-align:right;">选择文件：</td>
    <td>
        <asp:FileUpload ID="fload" runat="server" />
        <asp:Button ID="btnExcelTo" runat="server" class="ui_state_highlight" 
        Text="开始导入" onclick="btnExcelTo_Click" />
                 </td>
    </tr>
    <tr>
    <td style="width:80px; text-align:right;">&nbsp;</td>
    <td>
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
    <asp:Label ID="Label1" runat="server" Text="Label" Visible="False" 
        ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
    <td style="width:80px; text-align:right;">
    <b>
    温馨提示：</b></td>
    <td>

导入模板的格式不能修改，录入方法请参考演示模板。</td>
    </tr>
    <tr>
    <td style="width:80px; text-align:right;">&nbsp;</td>
    <td>
        <asp:LinkButton ID="lbtnDownExcel" runat="server" 
        onclick="lbtnDownExcel_Click">下载导入模板</asp:LinkButton>
 
        </td>
    </tr>
    <tr>
    <td style="width:80px; text-align:right;">&nbsp;</td>
    <td>
        <asp:LinkButton ID="lbtnDownExample" runat="server" 
        onclick="lbtnDownExample_Click">下载演示模板</asp:LinkButton>
        </td>
    </tr>
    </table>
    
    
&nbsp;</form>
</body>
</html>

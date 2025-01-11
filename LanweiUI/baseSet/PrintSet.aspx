<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintSet.aspx.cs" Inherits="Lanwei.Weixin.UI.baseSet.PrintSet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>打印设置</title>

   <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
  
   
    
    
    
     
</head>
<body>
    <form id="form1" runat="server">
    
    
    
    
    
   
    <table border="0" cellpadding="0" cellspacing="0" style="width:800px; line-height:40px;">
        
    
    <tr>
    <td align="center" valign="middle" style="height:50px;">
    
        采购订单<br />
        备注</td>
    
    <td align="center" valign="middle" style="height:50px;">
    
      <asp:TextBox ID="txtRemarksPurOrder" runat="server" Width="650px" Rows="20" 
                    TextMode="MultiLine"></asp:TextBox>
    
    </td>
    
    </tr>
    
    
    <tr>
    <td align="center" valign="middle" style="height:50px;">
    
        &nbsp;</td>
    
    <td align="center" valign="middle" style="height:50px;">
    
        <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="保 存"  />
    
    
    </td>
    
    </tr>
    
    
    <tr>
    <td align="center" valign="middle" style="height:50px;">
    
        销售出库<br />
        备注</td>
    
    <td align="center" valign="middle" style="height:50px;">
    
      <asp:TextBox ID="txtRemarksSalesOrder" runat="server" Width="650px" Rows="20" 
                    TextMode="MultiLine"></asp:TextBox>
    
    
    </td>
    
    </tr>
    
    
    <tr>
    <td align="center" valign="middle" style="height:50px;">
    
        &nbsp;</td>
    
    <td align="center" valign="middle" style="height:50px;">
    
        <asp:Button ID="btnSaveSalesOrderRemarks" runat="server" OnClick="btnSaveSalesOrderRemarks_Click" Text="保 存" />
    
    
    </td>
    
    </tr>
    
    
    </table>
    
   


    </form>
</body>
</html>

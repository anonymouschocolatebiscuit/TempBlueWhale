<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintSet.aspx.cs" Inherits="BlueWhale.UI.baseSet.PrintSet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Print Setting</title>

   <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
  
   
    
    
    
     
</head>
<body>
    <form id="form1" runat="server">
    
    
    
    
    
   
    <table border="0" cellpadding="0" cellspacing="0" style="line-height:40px;">

    <tr style="height:35px;"></tr>
        
    
    <tr>
  <td align="left" valign="top" style="height:150px; width:155px; text-align: left;">

      Purchase Order Remark
  </td>

  <td align="right" valign="middle" style="height:50px;">

    <asp:TextBox ID="txtRemarksPurOrder" runat="server" Width="1200px" Rows="20" 
                  TextMode="MultiLine" style="float: right;"></asp:TextBox>

  </td>
</tr>

    
    
    <tr>
    <td align="left" valign="middle" style="height:50px;" >
    
        &nbsp;</td>
    
    <td align="right" valign="middle" style="height:50px;">
    
        <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Save"  />
    
    
    </td>
    
    </tr>
    
    
    <tr>
    <td align="left" valign="top" style="height:150px; width:155px;"  text-align: left;">
    
        Sales Order Remark</td>
    
    <td align="right" valign="middle" style="height:50px;">
    
      <asp:TextBox ID="txtRemarksSalesOrder" runat="server" Width="1200px" Rows="20" 
                    TextMode="MultiLine" style="float: right;"></asp:TextBox>
    
    
    </td>
    
    </tr>
    
    
    <tr>
    <td align="left" valign="middle" style="height:50px;">
    
        &nbsp;</td>
    
    <td align="right" valign="middle" style="height:50px;">
    
        <asp:Button ID="btnSaveSalesOrderRemarks" runat="server" OnClick="btnSaveSalesOrderRemarks_Click" Text="Save" />
    
    
    </td>
    
    </tr>
    
    
    </table>
    
   


    </form>
</body>
</html>

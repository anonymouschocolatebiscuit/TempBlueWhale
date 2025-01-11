<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="makePDF.aspx.cs" Inherits="Lanwei.Weixin.UI.sales.makePDF" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        LOGO左：<asp:TextBox ID="TextBox1" runat="server">0</asp:TextBox>
&nbsp;顶：<asp:TextBox ID="TextBox2" runat="server">100</asp:TextBox>
        <br />
        <br />
        ZHANG右：<asp:TextBox ID="TextBox3" runat="server">250</asp:TextBox>
&nbsp;底：<asp:TextBox ID="TextBox4" runat="server">100</asp:TextBox>
        <br />
    
    </div>
    <asp:Button ID="btnPDF" runat="server" onclick="btnPDF_Click" Text="生成PDF" />
    </form>
</body>
</html>

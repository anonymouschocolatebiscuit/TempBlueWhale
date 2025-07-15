<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogoSet.aspx.cs" Inherits="BlueWhale.UI.baseSet.LogoSet" Async="true" %>

<!DOCTYPE htmlPUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Logo Set Edit</title>
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            <table border="0" cellpadding="2" cellspacing="1" style="width:100%;">
                <tr>
                    <td align="center" height="30" colspan="2">Edit LogoSet Picture</td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        Change Picture: 
                        <asp:RadioButton ID="RB1" runat="server" Checked="True" GroupName="id" Text="Stamp" />
                        &nbsp;
                        <asp:RadioButton ID="RB2" runat="server" GroupName="id" Text="LogoSet" />
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        Image: 
                        <asp:FileUpload ID="ProductImg" runat="server" Width="194px" />&nbsp;
                        <asp:Button ID="ButtonUpload" runat="server" onclick="ButtonUpload_Click" Text="Upload" />
                    </td>
                </tr>
                <tr>
                    <td style="width:100px;">LOGO（60*60）</td>
                    <td>
                        <asp:Image ID="Image1" runat="server" Height="60px" Width="60px"
                            ImageUrl="../sales/pdf/logo100.jpg" />
                    </td>
                </tr>
                <tr>
                    <td style="width:100px;">Stamp（180*180）</td>
                    <td>
                        <asp:Image ID="Image2" runat="server" Height="180px" Width="180px"
                            ImageUrl="../sales/pdf/zhang.jpg" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>

</html>
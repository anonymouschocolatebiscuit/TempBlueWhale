<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pwd.aspx.cs" Inherits="BlueWhale.UI.Pwd" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
                      "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Change Password</title>
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
        <table id="form" border="0" cellpadding="10px" cellspacing="10" style="width: 680px; line-height: 40px;">
            <tr>
                <td style="width: 80px; text-align: right;">Old Password:</td>
                <td style="padding-left:10px;">
                    <asp:TextBox ID="TextBoxOld" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">New Password:</td>
                <td style="padding-left:10px;">
                    <asp:TextBox ID="TextBoxNews" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 150px;">Reconfirm Password:</td>
                <td style="padding-left:10px;">
                    <asp:TextBox ID="TextBoxNews1" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">&nbsp;</td>
                <td style="text-align: left; padding-left: 30px;">
                    <asp:Button ID="ButtonUpdate" runat="server" CssClass="ui_state_highlight" 
                                OnClick="ButtonUpdate_Click" Text="Change" Style="width:auto;height:auto;"/>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
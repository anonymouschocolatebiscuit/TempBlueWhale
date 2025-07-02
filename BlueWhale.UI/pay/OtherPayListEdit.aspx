<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OtherPayListEdit.aspx.cs" Inherits="BlueWhale.UI.pay.OtherPayListEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Other Payment</title>
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
    <script src="js/OtherPayListEdit.js" type="text/javascript"></script>
</head>
<body style="padding-top: 10px; padding-left: 10px;">
    <form id="form1" runat="server">
        <table id="form" border="0" cellpadding="0" cellspacing="0" style="line-height: 40px;">
            <tr>
                <td style="text-align: right;">Contact Unit：</td>
                <td style="text-align: left; width: 250px; padding-right: 10px;">
                    <asp:DropDownList ID="ddlVenderList" runat="server" Width="250px">
                    </asp:DropDownList>
                </td>
                <td style="text-align: right;">Payment Date：</td>
                <td style="text-align: left; width: 180px; padding-right: 10px;">
                    <asp:TextBox ID="txtBizDate" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                </td>
                <td style="text-align: right;">Settlement Account：</td>
                <td style="text-align: left;">
                    <asp:DropDownList ID="ddlBankList" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <div id="maingrid"></div>
        <table id="tbFooter" border="0" cellpadding="0" cellspacing="0" style="width: 99%; line-height: 50px;">
            <tr>
                <td style="width: 80px; text-align: right;">Remark：</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtRemarks" runat="server" Width="540px" TextMode="MultiLine"></asp:TextBox>
                </td>
                <td style="text-align: right; padding-right: 30px;">
                    <input id="btnSave" runat="server" class="ui-btn ui-btn-sp mrb" type="button" value="Save" onclick="save()" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

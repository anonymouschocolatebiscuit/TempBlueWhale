<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssembleList.aspx.cs" Inherits="BlueWhale.UI.store.AssembleList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Assembly- View</title>
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
    <script src="js/AssembleList.js" type="text/javascript"></script>
</head>
<body style="padding-top: 10px; padding-left: 10px;">
    <form id="form1" runat="server">
        <table id="form" border="0" cellpadding="0" cellspacing="0" style="width: 99%; line-height: 30px;">
            <tr>
                <td style="width: 70px; text-align: center;">Keyword: </td>
                <td style="text-align: left; width: 180px;">
                    <asp:TextBox ID="txtKeys" runat="server" placeholder="Please enter the receipt no./bundled product/remarks"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 70px;">Start Date: </td>
                <td style="text-align: left; width: 180px;">
                    <asp:TextBox ID="txtDateStart" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 70px;">End Date: </td>
                <td style="text-align: left; width: 180px;">
                    <asp:TextBox ID="txtDateEnd" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 80px;">
                    <input id="btnSearch" type="button" value="Search" class="ui-btn ui-btn-sp mrb" onclick="search()" />
                </td>
                <td style="text-align: right; padding-right: 20px;">
                    <input id="btnAdd" type="button" value="Add" class="ui-btn" onclick="add()" />
                    <input id="btnCheck" type="button" value="Review" class="ui-btn" onclick="checkRow()" />
                    <input id="btnCheckNo" type="button" value="Reject" class="ui-btn" onclick="checkNoRow()" />
                    <input id="btnReload" class="ui-btn" type="button" value="Delete" onclick="deleteRow()" />
                </td>
            </tr>
            <tr>
                <td align="center" style="font-weight: bold; height: 30px;">Product After Assembly</td>
                <td style="text-align: left;" colspan="7">&nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="8">
                    <div id="maingrid"></div>
                </td>
            </tr>
            <tr>
                <td align="center" style="font-weight: bold;">Assembled Product</td>
                <td style="text-align: left;" colspan="7">&nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="8">
                    <div id="maingridsub"></div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

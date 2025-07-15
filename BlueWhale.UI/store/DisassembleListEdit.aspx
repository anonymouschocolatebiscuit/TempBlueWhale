<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisassembleListEdit.aspx.cs" Inherits="BlueWhale.UI.store.DisassembleListEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add New Product Disassembly Order</title>

    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />

    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
    <script src="js/DisassembleListEdit.js" type="text/javascript"></script>
</head>
<body style="padding-top: 10px; padding-left: 10px;">

    <form id="form1" runat="server">
        <table id="form" border="0" cellpadding="0" cellspacing="0" style="width: 99%; line-height: 40px;">
            <tr>
                <td style="width: 130px; text-align: right;">Disassembly Date: </td>
                <td style="text-align: left; width: 250px;">
                    <asp:TextBox ID="txtBizDate" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                </td>

                <td style="text-align: right; width: 130px;">Disassembly Cost: </td>
                <td style="text-align: left; width: 180px;">
                    <asp:TextBox ID="txtFee" runat="server" Text="0"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 80px;">&nbsp;</td>
                <td style="text-align: left;">&nbsp;</td>
            </tr>

            <tr>
                <td align="left" style="font-weight: bold; padding-left: 8px;" colspan="2">Disassembled Product</td>
                <td style="text-align: left; width: 250px;">&nbsp;</td>
                <td style="text-align: right; width: 80px;">&nbsp;</td>
                <td style="text-align: left; width: 180px;">&nbsp;</td>
                <td style="text-align: right; width: 80px;">&nbsp;</td>
                <td style="text-align: left;">&nbsp;</td>
            </tr>

            <tr>
                <td align="center" colspan="6">
                    <div id="maingrid"></div>
                </td>
            </tr>

            <tr>
                <td align="left" style="font-weight: bold; padding-left: 8px;" colspan="2">Product After Disassembly</td>
                <td style="text-align: left; width: 250px;">&nbsp;</td>
                <td style="text-align: right; width: 80px;">&nbsp;</td>
                <td style="text-align: left; width: 180px;">&nbsp;</td>
                <td style="text-align: right; width: 80px;">&nbsp;</td>
                <td style="text-align: left;">&nbsp;</td>
            </tr>

            <tr>
                <td align="center" colspan="6">
                    <div id="maingridsub"></div>
                </td>
            </tr>
        </table>

        <table id="tbFooter" border="0" cellpadding="0" cellspacing="0" style="width: 99%; line-height: 50px;">
            <tr>
                <td style="width: 80px; text-align: right;">Remarks: </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtRemarks" runat="server" Width="540px" TextMode="MultiLine"></asp:TextBox>
                </td>
                <td style="text-align: right; padding-right: 30px;">
                    <input id="btnSave" class="ui-btn ui-btn-sp mrb" type="button" value="Save" onclick="save()" runat="server" />
                </td>
            </tr>
        </table>

        <div id="target1" style="width: 200px; margin: 3px; display: none; text-align: center;">
            <asp:DropDownList ID="ddlInventoryList" runat="server">
            </asp:DropDownList>

            <input id="btnSelect" type="button" value="Select" onclick="selectInventory()" />
        </div>
    </form>
</body>
</html>

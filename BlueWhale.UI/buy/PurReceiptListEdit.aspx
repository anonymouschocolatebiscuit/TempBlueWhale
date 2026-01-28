<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PurReceiptListEdit.aspx.cs" Inherits="BlueWhale.UI.buy.PurReceiptListEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Purchase Receipt Edit</title>
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib.1.3.1/Source/lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
    <script src="js/PurReceiptListEdit.js?v=2018.11.13.01" type="text/javascript"></script>
    <style type="text/css">
        .tdLbl {
            text-align: right;
            width: 80px;
            white-space: nowrap;
            padding:10px;
        }

        .tdTxt {
            text-align: left;
            width: 280px;
        }
    </style>
</head>
<body style="padding-top: 10px; padding-left: 10px;">
    <form id="form1" runat="server">
        <table id="form" border="0" cellpadding="0" cellspacing="0" style="width: 99%; line-height: 40px;">
            <tr>
                <td class="tdLbl">Supplier：</td>
                <td class="tdTxt">
                    <asp:TextBox ID="clientName" runat="server"></asp:TextBox>
                    <input type="hidden" id="clientId" runat="server" value="" />
                    <input type="hidden" id="txtClientName" runat="server" value="" />
                </td>
                <td class="tdLbl">Stock In Date：</td>
                <td class="tdTxt">
                    <asp:TextBox ID="txtBizDate" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                </td>
                <td class="tdLbl">Purchaser：</td>
                <td style="text-align: left; width: 180px;">
                    <asp:DropDownList ID="ddlYWYList" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>

        <div id="maingrid"></div>
        <table id="tbFooter" border="0" cellpadding="0" cellspacing="0" style="width: 99%; line-height: 50px;">
            <tr>
                <td class="tdLbl">Current Payment：</td>
                <td class="tdTxt">
                    <asp:TextBox ID="txtPayNow" runat="server">0</asp:TextBox>
                </td>
                <td class="tdLbl">Current Debt：</td>
                <td class="tdTxt">
                    <asp:TextBox ID="txtPayNo" runat="server"
                        BackColor="#FFFFCC" ToolTip="Auto Calculation">0</asp:TextBox>
                </td>
                <td class="tdLbl">Settlement Account：</td>
                <td class="tdTxt">
                    <asp:DropDownList ID="ddlBankList" runat="server">
                    </asp:DropDownList>
                </td>
                <td style="text-align: right; padding-right: 30px;">&nbsp;</td>
            </tr>
            <tr>
                <td class="tdLbl">Remarks：</td>
                <td class="tdTxt">
                    <asp:TextBox ID="txtRemarks" runat="server" Width="540px" TextMode="MultiLine"></asp:TextBox>
                </td>
                <td style="padding-left:10px">
                    <input id="btnSave" class="ui-btn ui-btn-sp" type="button" value="Save" runat="server"
                        onclick="save()" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

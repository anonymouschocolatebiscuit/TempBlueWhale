<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PurReceiptListAdd.aspx.cs" Inherits="BlueWhale.UI.buy.PurReceiptListAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add New Purchase To Warehouse</title>

    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib.1.3.1/Source/lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
    <script src="js/PurReceiptListAdd.js?v=2018.11.13.01" type="text/javascript"></script>

</head>
<body style="padding-top: 10px; padding-left: 10px;">

    <form id="form1" runat="server">

        <table id="form" border="0" cellpadding="0" cellspacing="0" style="width: 99%; line-height: 40px;">
            <tr>
                <td style="width: 80px; text-align: right;">Vender:</td>
                <td style="text-align: left; width: 250px;">

                    <asp:TextBox ID="clientName" runat="server"></asp:TextBox>

                    <input type="hidden" id="clientId" runat="server" value="" />
                    <input type="hidden" id="txtClientName" runat="server" value="" />

                </td>
                <td style="text-align: right; width: 120px;">Stock In Date:</td>
                <td style="text-align: left; width: 180px;">
                    <asp:TextBox ID="txtBizDate" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 100px;">Purchaser:</td>
                <td style="text-align: left; width: 180px;">
                    <asp:DropDownList ID="ddlYWYList" runat="server">
                    </asp:DropDownList>
                </td>
                <td style="text-align: right; width: 80px;">&nbsp;</td>
                <td style="text-align: left;">&nbsp;</td>
            </tr>
        </table>

        <div id="maingrid"></div>

        <table id="tbFooter" border="0" cellpadding="0" cellspacing="0" style="width: 99%; line-height: 50px;">
            <tr>
                <td style="text-align: right; width: 150px;;">Current Payment:</td>
                <td style="text-align: left; width: 100px;">

                    <asp:TextBox ID="txtPayNow" runat="server">0</asp:TextBox>

                </td>
                <td style="text-align: right; width: 150px;">Current Debt:</td>
                <td style="text-align: left; width: 100px;">

                    <asp:TextBox ID="txtPayNo" runat="server"
                        BackColor="#FFFFCC" ToolTip="Auto Calculation">0</asp:TextBox>

                </td>
                <td style="text-align: right; width: 150px;">Settlement Account:</td>
                <td style="text-align: left;">

                    <asp:DropDownList ID="ddlBankList" runat="server">
                    </asp:DropDownList>

                </td>
                <td style="text-align: right; padding-right: 30px;">

                    <asp:HiddenField ID="hf" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 150px; text-align: right;">Remarks:</td>
                <td style="text-align: left;" colspan="5">

                    <asp:TextBox ID="txtRemarks" runat="server" Width="540px" TextMode="MultiLine"></asp:TextBox>

                </td>
                <td style="text-align: right; padding-right: 30px;">

                    <input id="Button1" class="ui-btn ui-btn-sp mrb" type="button" value="Add" onclick="save()" />

                </td>
            </tr>

        </table>

    </form>

</body>
</html>

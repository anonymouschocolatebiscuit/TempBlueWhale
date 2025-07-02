<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalesOrderListEdit.aspx.cs" Inherits="BlueWhale.UI.sales.SalesOrderListEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sales Order - Edit</title>

    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />

    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <%-- Legacy LigerUI reference commented out --%>
    <%-- <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script> --%>
    <script src="../lib.1.3.1/Source/lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>

    <script src="../lib/json2.js" type="text/javascript"></script>
    <script src="js/SalesOrderListEdit.js" type="text/javascript"></script>

    <style type="text/css">
        .l-grid-header {
            height: 56px !important; /* Increase header height */
        }
    </style>
</head>
<body style="padding-top:10px; padding-left:10px;">
    <form id="form1" runat="server">
        <table id="form" border="0" cellpadding="0" cellspacing="0" style="width:99%; line-height:40px;">
            <tr>
                <td style="width:80px; text-align:center;">Sales Unit:</td>
                <td style="text-align:left; width:250px;">
                    <asp:TextBox ID="clientName" runat="server"></asp:TextBox>
                    <input type="hidden" id="clientId" runat="server" value="" />
                    <input type="hidden" id="txtClientName" runat="server" value="" />
                </td>

                <td style="text-align:center; width:110px;">Inbound Date:</td>
                <td style="text-align:left; width:180px;">
                    <asp:TextBox ID="txtBizDate" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                </td>

                <td style="text-align:center; width:110px;">Delivery Date:</td>
                <td style="text-align:left; width:180px;">
                    <asp:TextBox ID="txtSendDate" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                </td>

                <td style="text-align:center; width:100px;">Salesperson:</td>
                <td style="text-align:left;">
                    <asp:DropDownList ID="ddlYWYList" runat="server"></asp:DropDownList>
                </td>
            </tr>
        </table>

        <div id="maingrid"></div>

        <table id="tbFooter" border="0" cellpadding="0" cellspacing="0" style="width:99%; line-height:50px;">
            <tr>
                <td style="width:80px; text-align:right;">Remarks:</td>
                <td style="text-align:left;">
                    <asp:TextBox ID="txtRemarks" runat="server" Width="540px" TextMode="MultiLine"></asp:TextBox>
                </td>
                <td style="text-align:right; padding-right:30px;">
                    <input id="btnSave" class="ui-btn ui-btn-sp mrb" type="button" value="Save" onclick="save()" runat="server" style="display:none;" />
                </td>
                <td style="text-align:right; padding-right:30px;">
                    <input id="Button1" class="ui-btn ui-btn-sp mrb" type="button" value="Save" onclick="save()" runat="server" />&nbsp;
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

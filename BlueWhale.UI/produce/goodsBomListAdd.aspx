﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="goodsBomListAdd.aspx.cs" Inherits="BlueWhale.UI.produce.goodsBomListAdd" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add BOM</title>

    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />

    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>

    <script src="js/goodsBomListAdd.js" type="text/javascript"></script>
</head>
<body style="padding-top:10px; padding-left:10px;">
    <form id="form1" runat="server">
        <table id="form" border="0" cellpadding="0" cellspacing="0" style="width:99%; line-height:40px;">
            <tr>
                <td style="width:80px; text-align:right;">BOM Group:</td>
                <td style="text-align:left; width:250px;">
                    <asp:DropDownList ID="ddlTypeList" runat="server"></asp:DropDownList>
                </td>
                <td style="text-align:right; width:80px;">Version:</td>
                <td style="text-align:left; width:180px;">
                    <asp:TextBox ID="txtEdition" runat="server"></asp:TextBox>
                </td>
                <td style="text-align:right; width:80px;">Drawing No.:</td>
                <td style="text-align:left;">
                    <asp:TextBox ID="txtTuhao" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:80px; text-align:right;">Select Product:</td>
                <td style="text-align:left; width:250px;">
                    <asp:TextBox ID="txtGoodsName" runat="server"></asp:TextBox>
                    <input type="hidden" id="hfGoodsId" runat="server" value="" />
                </td>
                <td style="text-align:right; width:80px;">Specification:</td>
                <td style="text-align:left; width:180px;">
                    <asp:TextBox ID="txtSpec" runat="server"></asp:TextBox>
                </td>
                <td style="text-align:right; width:80px;">Unit:</td>
                <td style="text-align:left;">
                    <asp:TextBox ID="txtUnitName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:80px; text-align:right;">Quantity:</td>
                <td style="text-align:left; width:250px;">
                    <asp:TextBox ID="txtNum" runat="server"></asp:TextBox>
                </td>
                <td style="text-align:right; width:80px;">Yield Rate (%):</td>
                <td style="text-align:left; width:180px;">
                    <asp:TextBox ID="txtRate" runat="server" Text="100"></asp:TextBox>
                </td>
                <td style="text-align:right; width:80px;"></td>
                <td style="text-align:left;"></td>
            </tr>
        </table>

        <div id="maingridsub"></div>

        <table id="tbFooter" border="0" cellpadding="0" cellspacing="0" style="width:99%; line-height:50px;">
            <tr>
                <td style="width:80px; text-align:right;">Remarks:</td>
                <td style="text-align:left;">
                    <asp:TextBox ID="txtRemarks" runat="server" Width="540px" TextMode="MultiLine"></asp:TextBox>
                </td>
                <td style="text-align:right; padding-right:30px;">
                    <input id="Button1" class="ui-btn ui-btn-sp mrb" type="button" value="Add" onclick="save()" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

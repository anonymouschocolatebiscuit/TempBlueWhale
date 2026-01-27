<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OtherInListAdd.aspx.cs" Inherits="BlueWhale.UI.store.OtherInListAdd" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add New Purchase Order</title>

    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />

    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <%--  <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>--%>
    <script src="../lib.1.3.1/Source/lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
    <script src="js/OtherInListAdd.js" type="text/javascript"></script>
</head>
<body style="padding-top: 10px; padding-left: 10px;">

    <form id="form1" runat="server">
        <table id="form" border="0" cellpadding="0" cellspacing="0" style="width: 99%; line-height: 40px;">
            <tr>
                <td style="width: 120px; text-align: center;">Contact Unit：</td>
                <td style="text-align: left; width: 250px;">
                    <asp:DropDownList ID="ddlVenderList" runat="server" Width="250px">
                    </asp:DropDownList>
                </td>
                <td style="text-align: right; width: 120px;">Stock In Date：</td>
                <td style="text-align: left; width: 180px;">
                    <asp:TextBox ID="txtBizDate" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 120px;">Storage Type：</td>
                <td style="text-align: left;">
                    <asp:RadioButton ID="rb1" runat="server" Checked="True" GroupName="t"
                        Text="Other Stock In" />
                    <asp:RadioButton ID="rb2" runat="server" GroupName="t" Text="Excess Warehouse Stock In" />
                </td>
            </tr>
        </table>

        <div id="maingrid"></div>

        <table id="tbFooter" border="0" cellpadding="0" cellspacing="0" style="width: 99%; line-height: 50px;">
            <tr>
                <td style="width: 80px; text-align: right;">Remarks：</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtRemarks" runat="server" Width="540px" Style="margin-top: 5px;" TextMode="MultiLine"></asp:TextBox>
                </td>
                <td style="text-align: right; padding-right: 30px;">
                    <input id="Button1" class="ui-btn ui-btn-sp mrb" type="button" value="Add" onclick="save()" />
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
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProduceListReport.aspx.cs" Inherits="BlueWhale.UI.report.ProduceListReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Produce Order Tracking Report</title>

    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />

    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>

    <script src="js/ProduceListReport.js" type="text/javascript"></script>

</head>
<body onload="search()">
    <form id="form1" runat="server">
        <table id="form" border="0" cellpadding="0" cellspacing="0" style="width: 99%; line-height: 40px;">
            <tr>
                <td style="text-align: right; width: 70px;">Schedule date:  
                </td>
                <td style="text-align: left; width: 120px;">
                    <asp:TextBox ID="txtDateStart" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                </td>
                <td style="text-align: center; width: 30px;">To
                </td>
                <td style="text-align: left; width: 120px;">
                    <asp:TextBox ID="txtDateEnd" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 80px;">Order no: </td>
                <td style="text-align: left; width: 180px;">
                    <asp:TextBox ID="txtKeys" runat="server" placeholder="Please enter oder no/remark"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 60px;">Supplier: </td>
                <td style="text-align: left; width: 120px;">
                    <input type="text" id="clientName" runat="server" value="" />
                    <input type="hidden" id="clientId" runat="server" value="" />
                </td>
                <td style="text-align: right; width: 50px;">Product: 
                </td>
                <td style="text-align: left; width: 100px;">
                    <input type="text" id="txtGoodsName" runat="server" value="" />
                    <input type="hidden" id="txtGoodsId" runat="server" value="" />
                </td>
                <td style="text-align: right; width: 60px;">Status: 
                </td>
                <td style="text-align: left; width: 80px;">
                    <input type="text" id="txtFlagList" />
                </td>
                <td style="text-align: right; padding-right: 20px;">
                    <input id="btnSearch" type="button" value="Search" class="ui-btn ui-btn-sp mrb" onclick="search()" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left; height: 300px;" colspan="13">
                    <div id="maingrid"></div>
                    <div style="display: none;">
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisassembleListAdd.aspx.cs" Inherits="BlueWhale.UI.store.DisassembleListAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Product Disassemble Order</title>
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" /> 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script> 
    <script src="js/DisassembleListAdd.js" type="text/javascript"></script>
     <style type="text/css">
     .tdLbl{
         text-align: right;
         width: 80px;
         white-space:nowrap;
     }

     .tdTxt {
         text-align: left;
         width: 280px;
     }

     .t-Start{
         text-align: left;
     }

     .t-End {
         text-align: right;
     }
 </style>
</head>
<body style="padding-top: 10px; padding-left: 10px;">
    <form id="form1" runat="server">

        <table id="form" border="0" cellpadding="0" cellspacing="0" style="width: 99%; line-height: 40px;">
            <tr>
                <td style="width: 130px; padding-left: 4px;">Disassembly Date：</td>
                <td class="tdTxt">
                    <asp:TextBox ID="txtBizDate" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 130px; padding-left: 4px;">Disassembly Cost：</td>
                <td class="tdTxt">
                    <asp:TextBox ID="txtFee" runat="server" Text="0"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" style="font-weight: bold; white-space:nowrap;">Disassembled Product</td>
                <td class="t-Start" style="width: 250px;">&nbsp;</td>
                <td class="t-End" style="width: 80px;">&nbsp;</td>
                <td class="t-Start" style="width: 180px;">&nbsp;</td>
                <td class="t-End" style="width: 80px;">&nbsp;</td>
                <td class="t-Start">&nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="6">
                    <div id="maingrid"></div>
                </td>
            </tr>
            <tr>
                <td align="center" style="font-weight: bold; white-space:nowrap;">Product After Disassembly</td>
                <td class="t-Start" style="width: 250px;">&nbsp;</td>
                <td class="t-End" style="width: 80px;">&nbsp;</td>
                <td class="t-Start" style="width: 180px;">&nbsp;</td>
                <td class="t-End" style="width: 80px;">&nbsp;</td>
                <td class="t-Start">&nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="6">
                    <div id="maingridsub"></div>
                </td>
            </tr>
        </table>

        <table id="tbFooter" border="0" cellpadding="0" cellspacing="0" style="width: 99%; line-height: 50px;">
            <tr>
                <td style="width: 80px; text-align: right;">Remarks：</td>
                <td class="t-Start">
                    <asp:TextBox ID="txtRemarks" runat="server" Width="540px" TextMode="MultiLine"></asp:TextBox>
                </td>
                <td class="t-End" padding-right: 30px;">
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
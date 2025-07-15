<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PurOrderListView.aspx.cs" Inherits="BlueWhale.UI.buy.PurOrderListView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
    <head runat="server">
        <title>Purchase Order</title>
  
        <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
        <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />

        <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
        <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
        <script src="../lib/json2.js" type="text/javascript"></script>
        <script src="js/PurOrderListEdit.js" type="text/javascript"></script>
         <style type="text/css"> 
             .l-grid-header { height: 64px !important; } /* Increase height */
         </style>
    </head>
    <body style=" padding-top:10px; padding-left:10px;">
        <form id="form1" runat="server">
            <table id="form" border="0" cellpadding="0" cellspacing="0" style="width:99%; line-height:40px;">
                <tr>
                    <td style="width:80px; text-align:center;">Purchasing Unit: </td>
                    <td style="text-align:left; width:250px;">
                        <asp:DropDownList ID="ddlVenderList" runat="server" Width="250px"></asp:DropDownList>
                    </td>
                    <td style="text-align:right; width:80px;">Order Date: </td>
                    <td style="text-align:left; width:180px;">
                        <asp:TextBox ID="txtBizDate" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                    </td>
                    <td style="text-align:right; width:80px;">Send Date: </td>
                    <td style="text-align:left; width:180px;" >
                        <asp:TextBox ID="txtSendDate" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                    </td>
                    <td style="text-align:right; width:80px;">Purchase Category: </td>
                    <td style="text-align:left;">
                        <asp:RadioButton ID="rb1" runat="server" Checked="True" GroupName="t" Text="Purchase" />
                        <asp:RadioButton ID="rb2" runat="server" GroupName="t" Text="Return" />
                    </td>
                </tr>
            </table>

            <div id="maingrid"></div>

            <table id="tbFooter" border="0" cellpadding="0" cellspacing="0" style="width:99%; line-height:50px;">
                <tr>
                    <td style="width:80px; text-align:right;">Remarks: </td>
                    <td style="text-align:left; ">
                        <asp:TextBox ID="txtRemarks" runat="server" Width="540px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td style="text-align:right; padding-right:30px; ">
                       <input id="Button1" class="ui-btn ui-btn-sp mrb" type="button" value="Generate StockIn Receipt" onclick="makeBill()" />
                    </td>
                </tr>
            </table>

            <div id="target1" style="width:200px; margin:3px; display:none; text-align:center;">
                <asp:DropDownList ID="ddlInventoryList" runat="server"></asp:DropDownList>

                <input id="btnSelect" type="button" value="Choose" onclick="selectInventory()" />
            </div>
        </form>
    </body>
</html>
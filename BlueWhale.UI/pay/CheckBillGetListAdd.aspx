<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckBillGetListAdd.aspx.cs" Inherits="BlueWhale.UI.pay.CheckBillGetListAdd" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Collection Write-Off - Create</title>
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
    <script src="js/CheckBillGetListAdd.js" type="text/javascript"></script>
</head>
<body style="padding-top:10px; padding-left:10px;">
    <form id="form1" runat="server">
        <table id="form" border="0" cellpadding="0" cellspacing="0" style="width:99%; line-height:40px;">
            <tr>
                <td style="width:140px; text-align:left;">Sales Unit: </td>
                <td style="text-align:left; width:250px;">
                    <asp:DropDownList ID="ddlVenderList" runat="server" Width="250px"></asp:DropDownList>
                </td>
                <td style="text-align:center; width:170px;">Write-Off Date: </td>
                <td style="text-align:left; width:180px;">
                    <asp:TextBox ID="txtBizDate" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                </td>
                <td style="text-align:left; width:80px;">&nbsp;</td>
                <td style="text-align:left;">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align:left;" colspan="6">
                    <table id="Table2" border="0" cellpadding="0" cellspacing="0" style="line-height:40px;">
                        <tr>
                            <td style="text-align:left; width:140px;">Collection Number: </td>
                            <td style="text-align:left; width:140px;">
                                <asp:TextBox ID="txtKeysGet" runat="server" placeholder="Please Enter Receipt No."></asp:TextBox>
                            </td>
                            <td style="text-align:center; width:65px;">Date: </td>
                            <td style="text-align:left; width:120px;">
                                <asp:TextBox ID="txtDateStar1" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                            </td>
                            <td style="text-align:center; width:30px;">To</td>
                            <td style="text-align:left; width:120px">
                                <asp:TextBox ID="txtDateEnd1" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                            </td>
                            <td style="text-align:left;">
                                <input id="btnSelectPay" type="button" value="Search Payment Receipt" class="ui-btn" style="margin-left:10px;" onclick="selectGet()" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr><td colspan="6" style="height:15px;"></td></tr>
            <tr>
                <td align="left" colspan="6">
                    <div id="maingrid"></div>
                </td>
            </tr>
            <tr><td colspan="6" style="height:15px;"></td></tr>
            <tr>
                <td align="left" colspan="6">
                    <table id="Table1" border="0" cellpadding="0" cellspacing="0" style="line-height:40px;">
                        <tr>
                            <td style="text-align:left; width:200px;">Accounts Receivable (AR) No.: </td>
                            <td style="text-align:left; width:140px;">
                                <asp:TextBox ID="txtKeys" runat="server" placeholder="Please Enter Receipt No."></asp:TextBox>
                            </td>
                            <td style="text-align:center; width:65px; padding-left:10px;">Date: </td>
                            <td style="text-align:left; width:120px;">
                                <asp:TextBox ID="txtDateStart" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                            </td>
                            <td style="text-align:center; width:30px;">To</td>
                            <td style="text-align:left; width:120px;">
                                <asp:TextBox ID="txtDateEnd" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                            </td>
                            <td style="text-align:left;">
                                <input id="btnSelectBill" type="button" value="Search AR Invoice" class="ui-btn" style="margin-left:10px;" onclick="selectBill()" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr><td colspan="6" style="height:15px;"></td></tr>
            <tr>
                <td align="left" colspan="6">
                    <div id="maingridsub"></div>
                </td>
            </tr>
            <tr><td colspan="6" style="height:15px;"></td></tr>
            <tr>
                <td style="text-align:left;">Remarks: </td>
                <td style="text-align:left; width:150px;" colspan="3">
                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
                <td style="text-align:left;">
                    <input id="Button1" class="ui-btn ui-btn-sp mrb" type="button" value="Create" style="margin-left:10px;" onclick="save()" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
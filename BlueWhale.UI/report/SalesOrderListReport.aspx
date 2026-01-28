<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalesOrderListReport.aspx.cs" Inherits="BlueWhale.UI.report.SalesOrderListReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
    <head runat="server">
        <title>Sales Order List Report</title>
    
        <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
        <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
        <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
        <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
        <script src="../lib/json2.js" type="text/javascript"></script>
        <script type="text/javascript">
            // Client Start

            // Contact Unit Start
            function f_selectClient() {
                $.ligerDialog.open({
                    title: 'Choose Client', name: 'winselector', width: 800, height: 540, url: '../baseSet/ClientListSelect.aspx', buttons: [
                        { text: 'Confirm', onclick: f_selectClientOK },
                        { text: 'Close', onclick: f_selectClientCancel }
                    ]
                });
                return false;
            }

            function f_selectClientOK(item, dialog) {
                var fn = dialog.frame.f_select || dialog.frame.window.f_select;
                var data = fn();
                if (!data) {
                    alert('Please Select A Row!');
                    return;
                }

                $("#txtVenderList").val(data.names);
                $("#txtVenderCode").val(data.code);

                dialog.close();
            }

            function f_selectClientCancel(item, dialog) {
                dialog.close();
            }

            // Contact Unit End

            // Product Start
            function f_selectContact() {
                $.ligerDialog.open({
                    title: 'Choose Goods', name: 'winselector', width: 840, height: 540, url: '../baseSet/GoodsListSelect.aspx', buttons: [
                        { text: 'Confirm', onclick: f_selectContactOK },
                        { text: 'Cancel', onclick: f_selectContactCancel }
                    ]
                });
                return false;
            }
            function f_selectContactOK(item, dialog) {
                var fn = dialog.frame.f_select || dialog.frame.window.f_select;
                var data = fn();
                if (!data) {
                    alert('Please Select A Row!');
                    return;
                }

                var valueText = "";
                var valueCode = "";
                for (var i = 0; i < data.length; i++) {
                    valueText += data[i].names + ";";
                    valueCode += data[i].code + ";";
                }

                valueText = valueText.substring(0, valueText.length - 1);
                valueCode = valueCode.substring(0, valueCode.length - 1);

                $("#txtGoodsList").val(valueCode);
                $("#txtGoodsCode").val(valueCode);

                dialog.close();
            }
            function f_selectContactCancel(item, dialog) {
                dialog.close();
            }

            // Product End

            $(function () {
                $("#txtVenderList").ligerComboBox({
                    onBeforeOpen: f_selectClient, valueFieldID: 'txtVenderCode', width: 300
                });

                $("#txtGoodsList").ligerComboBox({
                    onBeforeOpen: f_selectContact, valueFieldID: 'txtGoodsCode', width: 300
                });

                $("#txtFlagList").ligerComboBox({
                    isShowCheckBox: true, isMultiSelect: true,
                    data: [
                        { text: 'Not Out', id: '1' },
                        { text: 'Partially Out', id: '2' },
                        { text: 'All Out', id: '44' }
                    ], valueFieldID: 'flag'
                });
            });
        </script>
        <script src="js/SalesOrderListReport.js" type="text/javascript"></script>    
    </head>
    <body>
        <form id="form1" runat="server">
            <table id="form" border="0" cellpadding="0" cellspacing="0" style="width:99%; line-height:40px;">
                <tr>
                    <td style="text-align:right; width:90px;">Order Date：</td>
                    <td style="text-align:left; width:120px;">     
                        <asp:TextBox ID="txtDateStart" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                    </td>
                    <td style="text-align:center; width:30px;">To</td>
                    <td style="text-align:left; width:120px;">
                        <asp:TextBox ID="txtDateEnd" runat="server" ltype="date" validate="{required:true}"></asp:TextBox> 
                    </td>
                    <td style="text-align:right; width:60px;">Supplier：</td>
                    <td style="text-align:left; width:120px;">
                        <input type="text" id="txtVenderList"/> 
                    </td>
                    <td style="text-align:right;width:60px;">Goods：</td>
                    <td style="text-align:left;width:100px;">
                        <input type="text" id="txtGoodsList"/> 
                    </td>
                    <td style="text-align:right;width:60px;">Status：</td>
                    <td style="text-align:left;width:80px;">
                        <input type="text" id="txtFlagList"/> 
                    </td>
                    <td style="text-align:right; padding-right:20px;">
                        <input id="btnSearch" type="button" value="Search" class="ui-btn ui-btn-sp mrb" onclick="search()" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left; height:300px;" colspan="11">
                        <div id="maingrid"></div>  
                        <div style="display:none;"></div>
                    </td>
                </tr>
            </table>     
        </form>
    </body>
</html>
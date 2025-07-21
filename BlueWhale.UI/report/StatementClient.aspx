<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatementClient.aspx.cs" Inherits="BlueWhale.UI.report.StatementClient" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Client Statement</title>

    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />

    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
    <script type="text/javascript">
        function f_selectClient() {
            $.ligerDialog.open({
                title: 'Select Client', name: 'winselector', width: 800, height: 540, url: '../baseSet/ClientListSelect.aspx', buttons: [
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
                alert('Please select a row!');
                return;
            }

            $("#txtVenderList").val(data.names);
            $("#txtVenderCode").val(data.code);

            dialog.close();
        }

        function f_selectClientCancel(item, dialog) {
            dialog.close();
        }

        $(function () {
            $("#txtVenderList").ligerComboBox({
                onBeforeOpen: f_selectClient, valueFieldID: 'txtVenderCode', width: 300
            });
        });
    </script>
    <script src="js/StatementClient.js" type="text/javascript"></script>
</head>
<body>
    <form id="form2" runat="server">
        <table id="form" border="0" cellpadding="0" cellspacing="0" style="width: 99%; line-height: 40px;">
            <tr>
                <td style="text-align: right; width: 90px; padding-right: 8px;">Start-End Date: 
                </td>

                <td style="text-align: left; width: 120px;">
                    <asp:TextBox ID="txtDateStart" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                </td>

                <td style="text-align: center; width: 30px;">To
                </td>

                <td style="text-align: left; width: 120px;">
                    <asp:TextBox ID="txtDateEnd" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                </td>

                <td style="text-align: right; width: 60px;">Client: 
                </td>

                <td style="text-align: left; width: 80px;">
                    <input type="text" id="txtVenderList" />
                </td>

                <td style="text-align: right; padding-right: 20px;">
                    <input id="btnSearch" type="button" value="Search" class="ui-btn ui-btn-sp mrb" onclick="search()" />
                </td>
            </tr>

            <tr>
                <td style="text-align: left; height: 300px;" colspan="7">
                    <div id="maingrid"></div>
                    <div style="display: none;">
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

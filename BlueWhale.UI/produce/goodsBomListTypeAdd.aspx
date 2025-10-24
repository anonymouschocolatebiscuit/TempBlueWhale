<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="goodsBomListTypeAdd.aspx.cs" Inherits="BlueWhale.UI.produce.goodsBomListTypeAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Create BOM Grouping</title>

        <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
        <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />

        <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
        <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
        <script src="../lib/json2.js" type="text/javascript"></script>
        <script type="text/javascript">
            var dialog = frameElement.dialog; // Call the dialog object of the page (ligerui object)

            $(function () {
                // Create form structure
                var form = $("#form").ligerForm();
            });

            function closeDialog() {
                var dialog = frameElement.dialog;

                dialog.close(); // Close dialog
            }
        </script>
    </head>
    <body>
        <form id="form1" runat="server">
            <table id="form" border="0" cellpadding="0" cellspacing="10" style="width: 380px; line-height: 40px;">
                <tr>
                    <td style="width: 80px; text-align: right; padding-right: 10px;">Name:</td>
                    <td>
                        <asp:TextBox ID="txtNames" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px; text-align: right; padding-right: 10px;">Sort:</td>
                    <td>
                        <asp:TextBox ID="txtSortId" runat="server" Text="99" placeholder="Smaller numbers come first"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <asp:Button ID="btnSave" runat="server" Text="Save" class="ui_state_highlight" OnClick="btnSave_Click" />
                        <input id="btnCancel" class="ui-btn" type="button" value="Close" onclick="closeDialog()" />
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hfId" runat="server" />
        </form>
    </body>
</html>
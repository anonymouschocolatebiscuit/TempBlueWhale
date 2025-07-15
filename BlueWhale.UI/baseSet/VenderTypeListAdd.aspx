<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VenderTypeListAdd.aspx.cs" Inherits="BlueWhale.UI.baseSet.VenderTypeListAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Supplier Category</title>

    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />

    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>

    <script type="text/javascript">
        var dialog = frameElement.dialog;

        $(function () {
            var form = $("#form").ligerForm();
            liger.get("ddlRepeatWeek").set('disabled', true);
            liger.get("txtRepeatDays").set('disabled', true);
        });

        function closeDialog() {
            var dialog = frameElement.dialog;
            dialog.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table id="form" border="0" cellpadding="0" cellspacing="10" style="width:380px; line-height:40px;">
            <tr>
                <td style="width:160px; text-align:right;">Category Name: </td>
                <td>
                    <asp:TextBox ID="txtNames" runat="server"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="width:160px; text-align:right;">Display Order: </td>
                <td>
                    <asp:TextBox ID="txtFlag" runat="server" ltype='spinner' ligerui="{type:'int'}" value="0"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="text-align:right;">&nbsp;</td>
                <td style="text-align:right; padding-right:30px;">
                    <asp:Button ID="btnSave" runat="server" Text="Save" class="ui_state_highlight" onclick="btnSave_Click" />
                    <input id="btnCancel" class="ui-btn" type="button" value="Close" onclick="closeDialog()" />
                </td>
            </tr>
        </table>

        <asp:HiddenField ID="hfId" runat="server" />
    </form>
</body>
</html>

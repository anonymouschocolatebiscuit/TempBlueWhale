<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsTypeListAdd.aspx.cs" Inherits="BlueWhale.UI.baseSet.GoodsTypeListAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Goods Type</title>
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>


    <script src="../lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../lib/jquery-validation/jquery.validate.min.js" type="text/javascript"></script>
    <script src="../lib/jquery-validation/jquery.metadata.js" type="text/javascript"></script>
    <script src="../lib/jquery-validation/messages_cn.js" type="text/javascript"></script>

    <script type="text/javascript">
         var dialog = frameElement.dialog;
        
         $(function() {
             var form = $("#form").ligerForm();
         });
     
         function closeDialog() {
             var dialog = frameElement.dialog;
             dialog.close();
         }
    </script>
</head>
    <body>
        <form id="form1" runat="server">
            <table id="form" border="0" cellpadding="0" cellspacing="10" style="width: 380px; line-height: 40px;">
                <tr>
                    <td style="width: 120px; text-align: right;">Category Name：</td>
                    <td>
                        <asp:TextBox ID="txtNames" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">Sorting：</td>
                    <td>
                        <asp:TextBox ID="txtFlag" runat="server" ltype='spinner'
                            ligerui="{type:'int'}" value="0"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">Under Category：</td>
                    <td>
                        <asp:TextBox ID="txtParentName" runat="server" BackColor="#FFFFCC"
                            Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">&nbsp;</td>
                    <td style="text-align: right; padding-right: 30px;">
                        <asp:Button ID="btnSave" runat="server" Text="Save" class="ui_state_highlight"
                            OnClick="btnSave_Click" />
                        <input id="btnCancel" class="ui-btn" type="button" value="Close" onclick="closeDialog()" />
                    </td>
                </tr>
            </table>

            <asp:HiddenField ID="hfId" runat="server" />
            <asp:HiddenField ID="hfParentId" runat="server" />
        </form>
</body>
</html>

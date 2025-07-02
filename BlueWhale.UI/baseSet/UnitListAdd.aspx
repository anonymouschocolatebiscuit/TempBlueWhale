<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnitListAdd.aspx.cs" Inherits="BlueWhale.UI.baseSet.UnitListAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Unit Measurement</title>

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
        var dialog = frameElement.dialog; // Call the dialog object of the page (ligerUI object)

        $(function () {
            // Create form structure
            var form = $("#form").ligerForm();
            liger.get("ddlRepeatWeek").set('disabled', true);
            liger.get("txtRepeatDays").set('disabled', true);
        });

        function closeDialog() {
            var dialog = frameElement.dialog;
            dialog.close(); // Close dialog
        }
    </script>

    <script type="text/javascript">
        $(function () {
            $("#cbRepeat").change(function () {
                if (this.checked) {
                    liger.get("ddlRepeatWeek").set('disabled', false);
                } else {
                    liger.get("ddlRepeatWeek").set('disabled', true);
                }
            });

            $("#ddlRepeatWeek").change(function () {
                var checkValue = $("#ddlRepeatWeek").val();
                if (checkValue == "Customize") {
                    liger.get("txtRepeatDays").set('disabled', false);
                    $("#txtRepeatDays").val("0");
                } else {
                    liger.get("txtRepeatDays").set('disabled', true);

                    if (checkValue == "Daily") {
                        $("#txtRepeatDays").val("1");
                    }
                    if (checkValue == "Weekly") {
                        $("#txtRepeatDays").val("7");
                    }
                    if (checkValue == "Monthly") {
                        $("#txtRepeatDays").val("30");
                    }
                    if (checkValue == "Quarterly") {
                        $("#txtRepeatDays").val("90");
                    }
                    if (checkValue == "Semi-Annually") {
                        $("#txtRepeatDays").val("180");
                    }
                    if (checkValue == "Annually") {
                        $("#txtRepeatDays").val("365");
                    }
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table id="form" border="0" cellpadding="0" cellspacing="10" style="width:380px; line-height:40px;">
            <tr>
                <td style="width:80px; text-align:right;">Name:</td>
                <td>
                    <asp:TextBox ID="txtNames" runat="server"></asp:TextBox>
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

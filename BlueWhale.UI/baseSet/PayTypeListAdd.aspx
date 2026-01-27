<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayTypeListAdd.aspx.cs" Inherits="BlueWhale.UI.baseSet.PayTypeListAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Settlement Method</title>

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
    <script type="text/javascript">
        $(function () {
            $("#cbRepeat").change(function () {
                if (this.checked) {
                    liger.get("ddlRepeatWeek").set('disabled', false);
                }
                else {
                    liger.get("ddlRepeatWeek").set('disabled', true);
                }
            });

            $("#ddlRepeatWeek").change(function () {
                var checkValue = $("#ddlRepeatWeek").val();
                if (checkValue == "Custom") {
                    liger.get("txtRepeatDays").set('disabled', false);
                    $("#txtRepeatDays").val("0");
                }
                else {
                    liger.get("txtRepeatDays").set('disabled', true);

                    if (checkValue == "Dayly") {
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
                    if (checkValue == "Half Year") {
                        $("#txtRepeatDays").val("180");
                    }
                    if (checkValue == "Yearly") {
                        $("#txtRepeatDays").val("365");
                    }
                }
            });
        });
    </script>

    <style type="text/css">
        .button-container {
            text-align: center;
        }

        .ui_state_highlight,
        .ui-btn {
            display: inline-block;
            vertical-align: middle;
        }

        .form-container {
            padding: 3px 12px;
        }

        .form-row {
            display: flex;
            align-items: center;
            margin-bottom: 20px;
        }

            .form-row label {
                width: 80px; /* 對應你原本 td 的 80px */
                text-align: right;
                margin-right: 10px;
            }

        .form-actions {
            text-align: right; 
        }
            .form-control{
                width: 100%;
            }

            .form-actions input,
            .form-actions button {
                margin: 0 6px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-container">

            <div class="form-row">
                <label for="txtNames">Name：</label>
                <asp:TextBox
                    ID="txtNames"
                    runat="server"
                    CssClass="form-control" />
            </div>

            <div class="form-actions">
                <asp:Button
                    ID="btnSave"
                    runat="server"
                    Text="Save"
                    CssClass="ui_state_highlight"
                    OnClick="btnSave_Click" />
                <input
                    id="btnCancel"
                    class="ui-btn"
                    type="button"
                    style="margin-right:0px;"
                    value="Close"
                    onclick="closeDialog()" />
            </div>

            <asp:HiddenField ID="hfId" runat="server" />
        </div>
    </form>
</body>
</html>

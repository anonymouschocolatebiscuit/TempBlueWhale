<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountListAdd.aspx.cs" Inherits="BlueWhale.UI.BaseSet.AccountListAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Add Settlement Account</title>
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />

    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>

    <script type="text/javascript">
        var dialog = frameElement.dialog; // Call the dialog object of the parent page (ligerui object)

        $(function () {
            // Create the form structure
            var form = $("#form").ligerForm();
        });

        function closeDialog() {
            var dialog = frameElement.dialog;
            dialog.close(); // Close the dialog
        }

    </script>

    <style type="text/css">
        body {
            font-size: 12px;
        }

        .l-table-edit-td {
            padding: 4px;
        }

        .l-button-submit,
        .l-button-test {
            width: 80px;
            float: left;
            margin-left: 10px;
            padding-bottom: 2px;
        }

        .l-verify-tip {
            left: 230px;
            top: 120px;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <table id="form" style="height:250px;">
            <tr>
                <td style="text-align:right; white-space: nowrap;">
                    Account Number：</td>
                <td style="width:300px;">
                    <asp:TextBox ID="txtCode" runat="server" validate="{required:true}"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align:right; white-space: nowrap;">
                    Account Name：</td>
                <td>
                    <asp:TextBox ID="txtNames" runat="server" validate="{required:true}"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align:right; white-space: nowrap;">
                    Balance Date：</td>
                <td>
                    <asp:TextBox data-name="addDate" ID="txtYueDate" runat="server" ltype="date"
                        validate="{required:true}"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align:right; white-space: nowrap;">
                    Account Balance：</td>
                <td>
                    <asp:TextBox ID="txtYuePrice" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align:right; white-space: nowrap;">
                    Account Type：</td>
                <td>
                    <asp:DropDownList ID="ddlTypes" runat="server">
                        <asp:ListItem>Cash</asp:ListItem>
                        <asp:ListItem>Bank Deposit</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align:right; white-space: nowrap;">
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" class="ui_state_highlight"
                        onclick="btnSave_Click" />
                    &nbsp;<input id="btnCancel" class="ui-btn" type="button" value="Cancel" onclick="closeDialog()" />
                </td>
            </tr>
        </table>
    </form>
</body>

</html>

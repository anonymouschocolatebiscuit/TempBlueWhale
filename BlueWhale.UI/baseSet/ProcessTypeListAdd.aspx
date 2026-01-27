<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessTypeListAdd.aspx.cs" Inherits="BlueWhale.UI.baseSet.ProcessTypeListAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Process Category</title>

    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />

    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>

    <script type="text/javascript">
        var dialog = frameElement.dialog;

        $(function () {
            var form = $("#form").ligerForm();
        });

        function closeDialog() {
            var dialog = frameElement.dialog;
            dialog.close();
        }
    </script>
    <style type="text/css">
        .form-container {
            width: 380px;
        }

        .form-row {
            display: flex;
            align-items: center;
            margin-bottom: 15px;
        }

            .form-row label {
                width: 150px;
                text-align: right;
                margin-right: 10px;
            }

        .form-control {
            width: 200px;
            padding: 4px 6px;
        }

        .form-actions {
            text-align: right;
            padding-right: 4px;
        }

            .form-actions input,
            .form-actions button {
                margin-left: 10px;
            }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div class="form-container">

            <div class="form-row">
                <label for="txtNames">Process Category Name:</label>
                <asp:TextBox ID="txtNames" runat="server" CssClass="form-control" />
            </div>

            <div class="form-row">
                <label for="txtSeq">Display Order:</label>
                <asp:TextBox
                    ID="txtSeq"
                    runat="server"
                    CssClass="form-control"
                    ltype="spinner"
                    ligerui="{type:'int'}"
                    Text="1" />
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
                    value="Close"
                    onclick="closeDialog()" />
            </div>

            <asp:HiddenField ID="hfId" runat="server" />
        </div>
    </form>
</body>
</html>

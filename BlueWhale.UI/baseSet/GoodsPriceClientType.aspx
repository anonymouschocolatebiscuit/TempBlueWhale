<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsPriceClientType.aspx.cs" Inherits="BlueWhale.UI.baseSet.GoodsPriceClientType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Client Tier Configuration</title>
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>

    <script type="text/javascript">

        var manager;
        $(function () {
            manager = $("#maingrid").ligerGrid({
                columns: [

                    { display: 'Tier Name', name: 'names', id: 'names', width: 250, align: 'left' },

                    { display: 'Unit Price', name: 'price', width: 150, type: 'float', align: 'right', editor: { type: 'float' } }

                ], width: '550', pageSizeOptions: [5, 10, 15, 20], height: '300',
                url: 'GoodsPriceClientType.aspx?Action=GetDataList&id=' + getUrlParam("id"),
                alternatingRow: false,
                enabledEdit: true, //Control whether editing is allowed
                usePager: false,
                rownumbers: true
            });
        });

        function save() {

            var goodsId = getUrlParam("id");

            var data = manager.getData();

            var headJson = { goodsId: goodsId };

            var dataNew = [];
            dataNew.push(headJson);

            var list = JSON.stringify(headJson);

            var goodsList = [];

            list = list.substring(0, list.length - 1); // Remove the last curly brace

            list += ",\"Rows\":";
            list += JSON.stringify(data);
            list += "}";

            var postData = JSON.parse(list); // Final JSON

            $.ajax({
                type: "POST",
                url: 'ashx/GoodsPriceClientType.ashx',
                contentType: "application/json", // Must have this
                //dataType: "json", // Indicates the return type, not required
                data: JSON.stringify(postData),  // Equivalent to //data: "{'str1':'foovalue', 'str2':'barvalue'}",
                success: function (jsonResult) {

                    if (jsonResult == "Execution successful!") {

                        $.ligerDialog.waitting('Execution successful!');
                        setTimeout(function () { $.ligerDialog.closeWaitting(); location.reload(); }, 2000);

                    }
                    else {
                        $.ligerDialog.warn(jsonResult);
                    }
                },
                error: function (xhr) {
                    alert("An error occurred, please try again later:" + xhr.responseText);
                }
            });
        }

        function deleteRow() {

            var goodsId = getUrlParam("id");

            $.ligerDialog.confirm('Cannot be recovered after clearing. Confirm deletion?', function (type) {

                if (type) {

                    $.ajax({
                        type: "GET",
                        url: "GoodsPriceClientType.aspx",
                        data: "Action=clear&id=" + goodsId + "&ranid=" + Math.random(), //encodeURI
                        success: function (resultString) {
                            $.ligerDialog.alert(resultString, 'Notification');
                            reload();
                        },
                        error: function (msg) {

                            $.ligerDialog.alert("Network error, please contact the administrator", 'Notification');
                        }
                    });

                }

            });
        }

        var dialog = frameElement.dialog; // Calls the dialog object from the page (ligerui object)

        function closeDialog() {
            var dialog = frameElement.dialog; // Calls the dialog object from the page (ligerui object)
            dialog.close(); // Close dialog 
        }

        function getSelected() {
            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('Please select a row'); return; }
            alert(JSON.stringify(row));
        }
        function getData() {
            var data = manager.getData();
            alert(JSON.stringify(data));
        }

        function reload() {
            manager.reload();
        }

        function getUrlParam(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");

            var r = window.location.search.substr(1).match(reg);

            if (r != null) return unescape(r[2]); return null;
        }

    </script>
    <style type="text/css">
    .l-button { width: 120px; float: left; margin-left: 10px; margin-bottom: 2px; margin-top: 2px; }
    </style>

</head>
<body style="padding-left:10px; padding-top:10px;">
    <form id="form1" runat="server">

        <table border="0" width="550px">

            <tr>

                <td>

                    <div id="maingrid">
                    </div>

                </td>
            </tr>

            <tr>

                <td style="line-height:40px; text-align:center;padding-right:20px;">

                    <input id="Button1" class="ui-btn ui-btn-sp mrb" type="button" value="Save" onclick="save()" />

                    <input id="Button2" class="ui-btn ui-btn-sp mrb" type="button" value="Clear" onclick="deleteRow()" />

                    <input id="btnCancel" class="ui-btn" type="button" value="Close" onclick="closeDialog()" />

                </td>
            </tr>

        </table>

    </form>
</body>
</html>

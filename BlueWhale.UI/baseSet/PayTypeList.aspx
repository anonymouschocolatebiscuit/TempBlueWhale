<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayTypeList.aspx.cs" Inherits="BlueWhale.UI.baseSet.PayTypeList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PayType Setting</title>

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
                    { display: 'Settlement Method', name: 'names', id: 'levelName', width: 250, align: 'left' }
                ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '99%',
                url: 'PayTypeList.aspx?Action=GetDataList',
                alternatingRow: false,
                rownumbers: true, //Display serial number
                onDblClickRow: function (data, rowindex, rowobj) {
                    editRow();
                },
                toolbar: {
                    items: [
                        { text: 'Refresh', click: reload, img: '../lib/ligerUI/skins/icons/refresh.png' },
                        { line: true },

                        { text: "Add", click: addRowTop, img: '../lib/ligerUI/skins/icons/add.gif' },
                        { line: true },

                        { text: "Edit", click: editRow, img: '../lib/ligerUI/skins/icons/modify.gif' },
                        { line: true },

                        { text: "Delete", click: deleteRow, img: '../lib/ligerUI/skins/icons/delete.gif' }
                    ]
                }
            });
        });

        function editRow() {
            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('Please select the row you want to edit!'); return; }

            var title = "Edit Settlement Method";

            $.ligerDialog.open({
                title: title,
                url: "PayTypeListAdd.aspx?id=" + row.id + "&names=" + row.names,
                height: 200,
                width: 280,
                modal: true
            });
        }

        function deleteRow() {
            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('Please select the row you want to delete'); return; }

            $.ligerDialog.confirm('Deletion cannot be restored, are you sure to delete?', function (type) {
                if (type) {
                    $.ajax({
                        type: "GET",
                        url: "PayTypeList.aspx",
                        data: "Action=delete&id=" + row.id + "&ranid=" + Math.random(), //encodeURI
                        success: function (resultString) {
                            $.ligerDialog.alert(resultString, 'Prompt Message');
                            reload();
                        },
                        error: function (msg) {
                            $.ligerDialog.alert("Network error, please contact the administrator", 'Prompt Message');
                        }
                    });
                }
            });
        }

        function getSelected() {
            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('Please select row'); return; }
            alert(JSON.stringify(row));
        }

        function getData() {
            var data = manager.getData();
            alert(JSON.stringify(data));
        }

        function reload() {
            manager.reload();
        }

        function addRowTop() {
            var title = "Add Settlement Method";

            $.ligerDialog.open({
                title: title,
                url: 'PayTypeListAdd.aspx',
                height: 200,
                width: 320,
                modal: true
            });
        }
    </script>

    <style type="text/css">
        .l-button {
            width: 120px;
            float: left;
            margin-left: 10px;
            margin-bottom: 2px;
            margin-top: 2px;
        }
    </style>
</head>
<body style="padding-left: 10px; padding-top: 10px;">
    <form id="form1" runat="server">
        <div id="maingrid">
        </div>
    </form>
</body>
</html>

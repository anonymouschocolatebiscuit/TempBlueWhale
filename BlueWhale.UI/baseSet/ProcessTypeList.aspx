<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessTypeList.aspx.cs" Inherits="BlueWhale.UI.baseSet.ProcessTypeList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Process Category</title>

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
                    { display: 'Name', name: 'names', width: 250, align: 'left' },
                    { display: 'Display Order', name: 'seq', width: 250, align: 'left' }
                ],
                width: '99%',
                height: '99%',
                pageSizeOptions: [5, 10, 15, 20],
                url: 'ProcessTypeList.aspx?Action=GetDataList',
                alternatingRow: false,
                rownumbers: true,
                onDblClickRow: editRow,
                toolbar: {
                    items: [
                        { text: 'Refresh', click: reload, img: '../lib/ligerUI/skins/icons/refresh.png' },
                        { line: true },
                        { text: 'Add', click: addRow, img: '../lib/ligerUI/skins/icons/add.gif' },
                        { line: true },
                        { text: 'Edit', click: editRow, img: '../lib/ligerUI/skins/icons/modify.gif' },
                        { line: true },
                        { text: 'Delete', click: deleteRow, img: '../lib/ligerUI/skins/icons/delete.gif' }
                    ]
                }
            });
        });

        function reload() {
            manager.reload();
        }

        function addRow() {
            $.ligerDialog.open({
                title: "Add Process Category",
                url: 'ProcessTypeListAdd.aspx',
                height: 200,
                width: 400,
                modal: true
            });
        }

        function editRow() {
            var row = manager.getSelectedRow();
            if (!row) {
                $.ligerDialog.warn('Please select the row to edit!');
                return;
            }

            $.ligerDialog.open({
                title: "Edit Process Category",
                url: "ProcessTypeListAdd.aspx?id=" + row.id + "&names=" + encodeURIComponent(row.names) + "&seq=" + row.seq,
                height: 200,
                width: 400,
                modal: true
            });
        }

        function deleteRow() {
            var row = manager.getSelectedRow();
            if (!row) {
                $.ligerDialog.warn('Please select the row to delete!');
                return;
            }

            $.ligerDialog.confirm('Deletion cannot be restored. Confirm deletion?', function (type) {
                if (type) {
                    $.ajax({
                        type: "GET",
                        url: "ProcessTypeList.aspx",
                        data: {
                            Action: "delete",
                            id: row.id,
                            ranid: Math.random()
                        },
                        success: function (resultString) {
                            $.ligerDialog.alert(resultString, 'Notification');
                            reload();
                        },
                        error: function () {
                            $.ligerDialog.alert("Network error, please contact the administrator", 'Notification');
                        }
                    });
                }
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
        <div id="maingrid"></div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="goodsBomListType.aspx.cs" Inherits="BlueWhale.UI.produce.goodsBomListType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>BOM Group</title>

        <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
        <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
        <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
        <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
        <script src="../lib/json2.js" type="text/javascript"></script>
        <script src="../jsData/TreeDeptData.js" type="text/javascript"></script>
        <script type="text/javascript">
            var manager;
            $(function () {
                manager = $("#maingrid").ligerGrid({
                    columns: [
                        { display: 'Names', name: 'names', id: 'names', width: 250, align: 'left' },
                        { display: 'Sort', name: 'sortId', id: 'sortId', width: 250, align: 'left' }
                    ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '99%',
                    url: 'goodsBomListType.aspx?Action=GetDataList',
                    alternatingRow: false,
                    rownumbers: true,
                    onDblClickRow: function (data, rowindex, rowobj) {
                        // $.ligerDialog.alert('Selected ' + data.id);
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

                if (!row) { $.ligerDialog.warn('Please select a row to edit!'); return; }

                var title = "Edit BOM Group";

                $.ligerDialog.open({
                    title: title,
                    url: "goodsBomListTypeAdd.aspx?id=" + row.id + "&names=" + row.names + "&sortId=" + row.sortId,
                    height: 210,
                    width: 400,
                    modal: true
                });
            }

            function deleteRow() {
                var row = manager.getSelectedRow();
                if (!row) { $.ligerDialog.warn('Please select a row to delete'); return; }

                $.ligerDialog.confirm('Once deleted, it cannot be recovered. Confirm deletion?', function (type) {
                    if (type) {
                        $.ajax({
                            type: "GET",
                            url: "goodsBomListType.aspx",
                            data: "Action=delete&id=" + row.id + "&ranid=" + Math.random(), //encodeURI
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

            function addRowTop() {
                var title = "Add BOM Group";

                $.ligerDialog.open({
                    title: title,
                    url: 'goodsBomListTypeAdd.aspx',
                    height: 210,
                    width: 400,
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
            <div id="maingrid"></div>
        </form>
    </body>
</html>
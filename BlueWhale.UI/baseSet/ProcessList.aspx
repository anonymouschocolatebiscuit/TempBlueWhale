<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessList.aspx.cs" Inherits="BlueWhale.UI.baseSet.ProcessList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Process Settings</title>

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
                    { display: 'Category', name: 'typeName', id: 'typeName', width: 150, align: 'center' },
                    { display: 'Name', name: 'names', id: 'names', width: 250, align: 'left' },
                    { display: 'Unit', name: 'unitName', id: 'unitName', width: 80, align: 'center' },
                    { display: 'Unit Price', name: 'price', id: 'price', width: 100, align: 'right' },
                    { display: 'Display Order', name: 'seq', id: 'seq', width: 110, align: 'center' }
                ], width: '99%', pageSizeOptions: [20,50,100,200], height: '99%',

                url: 'ProcessList.aspx?Action=GetDataList',
                alternatingRow: false,
                rownumbers: true,
                onDblClickRow: function (data, rowindex, rowobj) {
                    editRow();
                },

                toolbar: {
                    items: [
                        { text: 'Refresh', click: refresh, img: '../lib/ligerUI/skins/icons/refresh.png' },
                        { line: true },

                        { text: "Add", click: addRowTop, img: '../lib/ligerUI/skins/icons/add.gif' },
                        { line: true },

                        { text: "Modify", click: editRow, img: '../lib/ligerUI/skins/icons/modify.gif' },
                        { line: true },

                        { text: "Delete", click: deleteRow, img: '../lib/ligerUI/skins/icons/delete.gif' }
                    ]
                }
            }
            );
        });

        function editRow() {
            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('Please select the row to modify!'); return; }

            var title = "Modify process";

            $.ligerDialog.open({
                title: title,
                url: "ProcessListAdd.aspx?id=" + row.id + "&names=" + row.names + "&seq="
                    + row.seq + "&price=" + row.price + "&typeId=" + row.typeId + "&unitId=" + row.unitId,
                height: 350,
                width: 500,
                modal: true
            });
        }

        function deleteRow() {

            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('Please select the rows you want to delete'); return; }

            $.ligerDialog.confirm('Deletion cannot be restored. Confirm deletion?', function (type) {

                if (type) {

                    $.ajax({
                        type: "GET",
                        url: "ProcessList.aspx",
                        data: "Action=delete&id=" + row.id + "&ranid=" + Math.random(), //encodeURI
                        success: function (resultString) {
                            $.ligerDialog.alert(resultString, 'Prompt message');
                            refresh();

                        },
                        error: function (msg) {

                            $.ligerDialog.alert("Network abnormality, please contact the administrator", 'Prompt message');
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

        function refresh() {
            manager.reload();
        }

        function addRowTop() {

            var title = "Add Process";


            $.ligerDialog.open({
                title: title,
                url: 'ProcessListAdd.aspx',
                height: 350,
                width: 480,
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

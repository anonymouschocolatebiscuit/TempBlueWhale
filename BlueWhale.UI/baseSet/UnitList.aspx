<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnitList.aspx.cs" Inherits="BlueWhale.UI.baseSet.UnitList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Unit of Measurement</title>

    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />

    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
    <script src="../jsData/TreeDeptData.js" type="text/javascript"></script>

    <script type="text/javascript">
        let manager;

        $(function () {
            manager = $("#maingrid").ligerGrid({
                columns: [
                    { display: 'Unit Measurement Name', name: 'names', id: 'levelName', width: 250, align: 'left' }
                ],
                width: '99%',
                height: '99%',
                pageSizeOptions: [5, 10, 15, 20],
                url: 'UnitList.aspx?Action=GetDataList',
                alternatingRow: false,
                rownumbers: true, // Display serial number
                onDblClickRow: function (data, rowindex, rowobj) {
                    editRow();
                },
                toolbar: {
                    items: [
                        { text: 'Refresh', click: reload, img: '../lib/ligerUI/skins/icons/refresh.png' },
                        { line: true },
                        { text: 'Add', click: addRowTop, img: '../lib/ligerUI/skins/icons/add.gif' },
                        { line: true },
                        { text: 'Edit', click: editRow, img: '../lib/ligerUI/skins/icons/modify.gif' },
                        { line: true },
                        { text: 'Delete', click: deleteRow, img: '../lib/ligerUI/skins/icons/delete.gif' }
                    ]
                }
            });
        });

        function editRow() {
            const row = manager.getSelectedRow();
            if (!row) {
                $.ligerDialog.warn('Please select a row to modify!');
                return;
            }

            const title = "Edit Unit Measurement";

            $.ligerDialog.open({
                title: title,
                url: "UnitListAdd.aspx?id=" + row.id + "&names=" + row.names,
                height: 170,
                width: 350,
                modal: true
            });
        }

        function deleteRow() {
            const row = manager.getSelectedRow();
            if (!row) {
                $.ligerDialog.warn('Please select a row to delete!');
                return;
            }

            $.ligerDialog.confirm('Deletion cannot be restored. Confirm deletion?', function (type) {
                if (type) {
                    $.ajax({
                        type: "GET",
                        url: "UnitList.aspx",
                        data: "Action=delete&id=" + row.id + "&ranid=" + Math.random(),
                        success: function (resultString) {
                            $.ligerDialog.alert(resultString, 'Notification');
                            reload();
                        },
                        error: function () {
                            $.ligerDialog.alert('Network error, please contact the administrator.', 'Notification');
                        }
                    });
                }
            });
        }

        function getSelected() {
            const row = manager.getSelectedRow();
            if (!row) {
                $.ligerDialog.warn('Please select a row!');
                return;
            }
            alert(JSON.stringify(row));
        }

        function getData() {
            const data = manager.getData();
            alert(JSON.stringify(data));
        }

        function reload() {
            manager.reload();
        }

        function addRowTop() {
            const title = "Add New Unit Measurement";

            $.ligerDialog.open({
                title: title,
                url: 'UnitListAdd.aspx',
                height: 170,
                width: 350,
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

        .l-toolbar-item {
            margin-top: 3px;
            margin-bottom: 3px;
        }

        .l-panel-topbar {
            height: 30px !important;
            line-height: 30px !important;
        }
    </style>
</head>
<body style="padding-left:10px; padding-top:10px;">
    <form id="form1" runat="server">
        <div id="maingrid"></div>
    </form>
</body>
</html>

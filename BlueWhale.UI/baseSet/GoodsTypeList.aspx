<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsTypeList.aspx.cs" Inherits="BlueWhale.UI.baseSet.GoodsTypeList" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Category Settings</title>
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
                    {
                        display: 'Picture', name: 'picUrl', id: 'picUrl', width: 70, align: 'center',
                        render: function (rowdata, rowindex, value) {
                            var h = "";
                            if (!rowdata._editing) {
                                h += "<img src='" + value + "' style='width:50px;height:50px;padding:5px;' />";
                            }
                            return h;
                        }
                    },
                    { display: 'Category Name', name: 'names', id: 'names', width: 250, align: 'left' },
                    { display: 'Product Quantity', name: 'num', width: 120, type: 'int', align: 'center' },
                    { display: 'Display Order', name: 'seq', width: 100, align: 'center' },
                    {
                        display: 'Mini Program Display', name: 'isShowXCX', id: 'isShowXCX', width: 160, align: 'center',
                        render: function (rowdata, rowindex, value) {
                            return !rowdata._editing ? (value == "1" ? "Yes" : "No") : "";
                        }
                    },
                    {
                        display: 'Official Account Display', name: 'isShowGZH', id: 'isShowGZH', width: 170, align: 'center',
                        render: function (rowdata, rowindex, value) {
                            return !rowdata._editing ? (value == "1" ? "Yes" : "No") : "";
                        }
                    },
                    { display: 'Parent ID', name: 'parentId', width: 60, align: 'center', hide: true },
                    { display: 'Parent Name', name: 'parentName', width: 60, align: 'center', hide: true },
                    {
                        display: 'Action', isSort: false, width: 130, align: 'center',
                        render: function (rowdata, rowindex, value) {
                            var h = "";
                            if (!rowdata._editing) {
                                h += "<a href='javascript:addSubRow()' title='Add Sub Category'>Add Sub Category</a>";
                            }
                            return h;
                        }
                    }
                ],
                width: '99%',
                pageSizeOptions: [5, 10, 15, 20],
                height: '100%',
                url: 'GoodsTypeList.aspx?Action=GetDataList',
                alternatingRow: false,
                rownumbers: true, // Display serial number
                tree: {
                    columnId: 'names',
                    idField: 'id',
                    parentIDField: 'parentId'
                },
                onDblClickRow: function (data, rowindex, rowobj) {
                    editRow();
                },
                toolbar: {
                    items: [
                        { text: 'Refresh', click: reload, img: '../lib/ligerUI/skins/icons/refresh.png' },
                        { line: true },
                        { text: "Add Main Category", click: addRowTop, img: '../lib/ligerUI/skins/icons/add.gif' },
                        { line: true },
                        { text: "Edit Category", click: editRow, img: '../lib/ligerUI/skins/icons/modify.gif' },
                        { line: true },
                        { text: 'Edit Picture', click: editPic, img: '../lib/ligerUI/skins/icons/photograph.gif' },
                        { line: true },
                        { text: "Delete Category", click: deleteRow, img: '../lib/ligerUI/skins/icons/delete.gif' },
                        { line: true },
                        { text: 'Expand All', click: expandAll, img: '../lib/ligerUI/skins/icons/expand.png' },
                        { line: true },
                        { text: 'Collapse All', click: collapseAll, img: '../lib/ligerUI/skins/icons/collapse.png' }
                    ]
                }
            });
        });

        function editRow() {
            var row = manager.getSelectedRow();
            if (!row) {
                $.ligerDialog.warn('Please select the row(s) to modify!');
                return;
            }
            var title = "Edit Category - " + row.names;
            $.ligerDialog.open({
                title: title,
                url: 'GoodsTypeListAdd.aspx?id=' + row.id + "&names=" + row.names + "&parentId=" + row.parentId + "&parentName=" + row.parentName + "&seq=" + row.seq,
                height: 250,
                width: 400,
                modal: false
            });
        }

        function editPic() {
            var row = manager.getSelectedRow();
            if (!row) {
                $.ligerDialog.warn('Please select the row(s) to modify!');
                return;
            }
            var title = "Edit Category Image - " + row.names;
            $.ligerDialog.open({
                title: title,
                url: 'GoodsTypeListPic.aspx?id=' + row.id + "&names=" + row.names + "&picUrl=" + row.picUrl + "&parentName=" + row.parentName + "&seq=" + row.seq,
                height: 350,
                width: 400,
                modal: false
            });
        }

        function deleteRow() {
            var row = manager.getSelectedRow();
            if (!row) {
                $.ligerDialog.warn('Please select the row(s) to delete');
                return;
            }
            $.ligerDialog.confirm('Cannot be recovered after clearing. Confirm deletion?', function (type) {
                if (type) {
                    $.ajax({
                        type: "GET",
                        url: "GoodsTypeList.aspx",
                        data: "Action=delete&id=" + row.id + "&ranid=" + Math.random(), // encodeURI
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
            if (!row) {
                $.ligerDialog.warn('Please select a row');
                return;
            }
            alert(JSON.stringify(row));
        }

        function getData() {
            var data = manager.getData();
            alert(JSON.stringify(data));
        }

        // Collapse All
        function collapseAll() {
            manager.collapseAll();
        }

        // Expand All
        function expandAll() {
            manager.expandAll();
        }

        function reload() {
            manager.reload();
        }

        function addSubRow() {
            var row = manager.getSelectedRow();
            var title = "Add Sub Category - " + row.names;
            $.ligerDialog.open({
                title: title,
                url: "GoodsTypeListAdd.aspx?id=0&names=&parentId=" + row.id + "&parentName=" + row.names + "&seq=0",
                height: 250,
                width: 400,
                modal: false
            });
        }

        function addRowTop() {
            var title = "Add Main Category";
            $.ligerDialog.open({
                title: title,
                url: "GoodsTypeListAdd.aspx?id=0&names=&parentId=0&parentName=MainCategory&seq=0",
                height: 250,
                width: 400,
                modal: false
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

<body style="padding-left:10px; padding-top:10px;">
    <form id="form1" runat="server">
        <div id="maingrid"></div>
    </form>
</body>
</html>

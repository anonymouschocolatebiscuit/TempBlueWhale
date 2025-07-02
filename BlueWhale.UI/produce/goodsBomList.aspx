<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="goodsBomList.aspx.cs" Inherits="BlueWhale.UI.produce.goodsBomList" %>
<%@ Import namespace="BlueWhale.Common" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>BOM Management</title>

    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />

    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>

    <style type="text/css">
        body { padding: 5px; margin: 0; padding-bottom: 15px; }
        #layout1 { width: 100%; margin: 0; padding: 0; }
        .l-page-top { height: 80px; background: #f8f8f8; margin-bottom: 3px; }
        h4 { margin: 20px; }
        .l-button { width: 120px; float: left; margin-left: 10px; margin-bottom: 2px; margin-top: 2px; }
    </style>

    <script type="text/javascript">
        var manager;
        var maingridItem;

        $(function () {
            $("#layout1").ligerLayout({ leftWidth: 180, allowLeftCollapse: false });

            $("#form").ligerForm();

            $("#tree1").ligerTree({
                url: "goodsBomList.aspx?Action=GetTreeList",
                checkbox: false,
                parentIcon: 'folder',
                childIcon: 'leaf',
                slide: false,
                treeLine: true,
                idFieldName: 'id',
                isExpand: 3,
                parentIDFieldName: 'pid',
                onSelect: function (note) {
                    manager._setUrl("goodsBomList.aspx?Action=GetDataList&typeId=" + note.data.id);
                }
            });

            manager = $("#maingrid").ligerGrid({
                columns: [
                    { display: 'BOM Group', name: 'typeName', width: 70, align: 'center' },
                    { display: 'BOM Number', name: 'number', width: 160, align: 'center' },
                    { display: 'Edition', name: 'edition', width: 70, align: 'center' },
                    { display: 'Drawing Number', name: 'tuhao', width: 70, align: 'center' },
                    { display: 'Status', name: 'flagCheck', width: 50, align: 'center' },
                    { display: 'Material Code', name: 'code', width: 100, align: 'center' },
                    { display: 'Material Name', name: 'goodsName', width: 180, align: 'center' },
                    { display: 'Specification Model', name: 'spec', width: 100, align: 'center' },
                    { display: 'Unit', name: 'unitName', width: 70, align: 'center' },
                    { display: 'Quantity', name: 'num', width: 70, align: 'center' },
                    { display: 'Yield Rate %', name: 'rate', width: 70, align: 'center' },
                    { display: 'Created By', name: 'makeName', width: 70, align: 'center' },
                    { display: 'Created Date', name: 'makeDate', width: 80, align: 'center' },
                    { display: 'Reviewed By', name: 'checkName', width: 70, align: 'center' },
                    { display: 'Review Date', name: 'checkDate', width: 80, align: 'center' },
                    { display: 'Remarks', name: 'remarks', width: 180, align: 'center' }
                ],
                width: '99%',
                height: '50%',
                url: 'goodsBomList.aspx?Action=GetDataList&typeId=0',
                alternatingRow: false,
                rownumbers: true,
                usePager: false,
                onDblClickRow: editRow,
                onSelectRow: viewRow,
                toolbar: {
                    items: [
                        { text: 'Refresh', click: reload, img: '../lib/ligerUI/skins/icons/refresh.png' },
                        { line: true },
                        { text: 'Add', click: addRow, img: '../lib/ligerUI/skins/icons/add.gif' },
                        { line: true },
                        { text: 'Edit', click: editRow, img: '../lib/ligerUI/skins/icons/modify.gif' },
                        { line: true },
                        { text: 'Review', click: checkRow, img: '../lib/ligerUI/skins/icons/ok.gif' },
                        { line: true },
                        { text: 'Cancel Review', click: checkNoRow, img: '../lib/ligerUI/skins/icons/back.gif' },
                        { line: true },
                        { text: 'Delete', click: deleteRow, img: '../lib/ligerUI/skins/icons/delete.gif' }
                    ]
                }
            });

            maingridItem = $("#maingridItem").ligerGrid({
                columns: [
                    { display: 'Material Code', name: 'code', width: 80, align: 'center' },
                    { display: 'Material Name', name: 'goodsName', width: 150, align: 'center' },
                    { display: 'Specification Model', name: 'spec', width: 100, align: 'center' },
                    { display: 'Unit', name: 'unitName', width: 100, align: 'center' },
                    { display: 'Quantity', name: 'num', width: 100, align: 'left' },
                    { display: 'Loss Rate', name: 'rate', width: 100, type: 'int', align: 'center' },
                    { display: 'Remarks', name: 'remarks', width: 150, align: 'center' }
                ],
                width: '99%',
                height: '50%',
                rownumbers: true,
                alternatingRow: false,
                usePager: false
            });
        });

        function addRow() {
            parent.f_addTab('goodsBomListAdd', 'BOM List - Add', 'produce/goodsBomListAdd.aspx');
        }

        function editRow() {
            var row = manager.getSelectedRow();
            if (!row) return $.ligerDialog.warn('Please select a row to edit.');
            if (row.flagCheck == "Reviewed") return $.ligerDialog.warn('Reviewed records cannot be modified.');

            parent.f_addTab('goodsBomListEdit', 'BOM List - Edit', 'produce/goodsBomListEdit.aspx?id=' + row.id);
        }

        function deleteRow() {
            var row = manager.getSelectedRow();
            if (!row) return $.ligerDialog.warn('Please select a row to delete.');
            if (row.flagCheck == "Reviewed") return $.ligerDialog.warn('Reviewed records cannot be deleted.');

            $.ligerDialog.confirm('Deletion is irreversible. Confirm delete?', function (type) {
                if (type) {
                    $.ajax({
                        type: "GET",
                        url: "goodsBomList.aspx",
                        data: "Action=delete&id=" + row.id + "&ranid=" + Math.random(),
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

        function checkRow() {
            var row = manager.getSelectedRow();
            if (!row) return $.ligerDialog.warn('Please select a row to review.');
            $.ajax({
                type: "GET",
                url: "goodsBomList.aspx",
                data: "Action=checkRow&id=" + row.id + "&ranid=" + Math.random(),
                success: function (resultString) {
                    $.ligerDialog.alert(resultString, 'Notification');
                    reload();
                },
                error: function () {
                    $.ligerDialog.alert("Network error, please contact the administrator", 'Notification');
                }
            });
        }

        function checkNoRow() {
            var row = manager.getSelectedRow();
            if (!row) return $.ligerDialog.warn('Please select a row to cancel review.');
            $.ajax({
                type: "GET",
                url: "goodsBomList.aspx",
                data: "Action=checkNoRow&id=" + row.id + "&ranid=" + Math.random(),
                success: function (resultString) {
                    $.ligerDialog.alert(resultString, 'Notification');
                    reload();
                },
                error: function () {
                    $.ligerDialog.alert("Network error, please contact the administrator", 'Notification');
                }
            });
        }

        function viewRow() {
            var row = manager.getSelectedRow();
            if (!row) return alert('Please select a row.');
            maingridItem._setUrl("goodsBomList.aspx?Action=GetDataListSub&pId=" + row.id);
        }

        function reload() {
            manager.reload();
        }

        function reloadItem() {
            maingridItem.reload();
        }

    </script>
</head>
<body style="padding-left:10px;">
    <form id="form1" runat="server">
        <div id="layout1">
            <div id="showTitle" position="left" title="BOM Group">
                <div id="Div1" style="overflow: auto; width: 99%; height: 430px; text-align: left;">
                    <ul id="tree1"></ul>
                </div>
            </div>
            <div id="rigthTitle" position="center" title="BOM List">
                <div id="maingrid"></div>
                <div id="maingridItem"></div>
            </div>
        </div>
    </form>
</body>
</html>

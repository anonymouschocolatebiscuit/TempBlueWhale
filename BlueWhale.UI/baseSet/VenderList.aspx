<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VenderList.aspx.cs" Inherits="BlueWhaleUI.baseSet.VenderList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vender Management</title>
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />

    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
    <script type="text/javascript">
        var manager;
        $(function () {
            manager = $("#maingrid").ligerGrid({
                checkbox: true,
                columns: [
                    { display: 'Vender Category', name: 'typeName', width: 130, type: 'int', align: 'center' },
                    { display: 'Vender Code', name: 'code', width: 100, align: 'center' },
                    { display: 'Vender Name', name: 'names', width: 150, align: 'left' },
                    { display: 'Balance Date', name: 'dueDate', width: 90, align: 'center' },
                    { display: 'Opening Balance', name: 'balance', width: 120, align: 'center' },
                    { display: 'Tax Rate%', name: 'tax', width: 80, align: 'center' },
                    { display: 'Tax Number', name: 'taxNumber', width: 100, align: 'center' },
                    { display: 'Bank Name', name: 'bankName', width: 120, align: 'center' },
                    { display: 'Bank Account Number', name: 'bankNumber', width: 150, align: 'center' },
                    { display: 'Address', name: 'address', width: 160, align: 'center' },
                    { display: 'Primary Contact', name: 'linkMan', width: 150, align: 'center' },
                    { display: 'Phone No.', name: 'phone', width: 100, align: 'center' },
                    { display: 'Fax no.', name: 'tel', width: 100, align: 'center', type: "date" },
                    { display: 'Status', name: 'flag', width: 80, align: 'center' },
                    { display: 'Entry Date', name: 'makeDate', width: 80, align: 'center', type: "date" }
                ], width: '99%',
                //pageSizeOptions: [5, 10, 15, 20],
                height: '99%',
                // pageSize: 15,
                dataAction: 'local', //Local sorting
                // usePager: true,
                usePager: false,
                url: 'VenderList.aspx?Action=GetDataList',
                alternatingRow: false,
                isChecked: f_isChecked, onCheckRow: f_onCheckRow, onCheckAllRow: f_onCheckAllRow,
                rownumbers: true, //Display serial number            
                onDblClickRow: function (data, rowindex, rowobj) {
                    editRow();
                },
                toolbar: {
                    items: [
                        { text: 'Refresh', click: reload, img: '../lib/ligerUI/skins/icons/refresh.png' },
                        { line: true },
                        { text: 'Search', click: search, img: '../lib/ligerUI/skins/icons/search.gif' },
                        { line: true },
                        { text: "Add Vender", click: addRowTop, img: '../lib/ligerUI/skins/icons/add.gif' },
                        { line: true },
                        { text: "Manage Contact", click: linkManForm, img: '../lib/ligerUI/skins/icons/customers.gif' },
                        { line: true },
                        { text: "Approve", click: checkRow, img: '../lib/ligerUI/skins/icons/true.gif' },
                        { line: true },
                        { text: "Reject", click: checkNoRow, img: '../lib/ligerUI/skins/icons/refresh.gif' },
                        { line: true },
                        { text: 'Batch Import', click: excel, img: '../lib/ligerUI/skins/icons/xls.gif' }
                    ]
                }
            });
        });

        function linkManForm() {
            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('Please select a vender !'); return; }

            var title = "Contact Management - " + row.names;

            $.ligerDialog.open({
                title: title,
                url: 'VenderLinkMan.aspx?id=' + row.id,
                height: 400,
                width: 650,
                modal: true
            });
        }

        function search() {
            $.ligerDialog.prompt('You can search by name, phone number, and primary contact', '', function (yes, value) {
                if (yes) {
                    var key = value;
                    manager.changePage("first");
                    manager._setUrl("VenderList.aspx?Action=GetDataListSearch&keys=" + key);
                }
            });
        }


        function addRowTop() {
            var title = "Add vender";

            $.ligerDialog.open({
                title: title,
                url: 'VenderListAdd.aspx?id=0',
                height: 500,
                width: 650,
                modal: false
            });
        }

        function checkRow() {
            var row = manager.getSelectedRow();

            if (!row) { $.ligerDialog.warn('Please select row'); return; }

            var idString = checkedCustomer.join(',');

            $.ajax({
                type: "GET",
                url: "VenderList.aspx",
                data: "Action=checkRow&idString=" + idString + "&ranid=" + Math.random(), //encodeURI
                success: function (resultString) {
                    $.ligerDialog.alert(resultString, 'Information');
                    reload();
                },
                error: function (msg) {
                    $.ligerDialog.alert("Network error, please contact an administrator", 'Information');
                }
            });
        }

        function checkNoRow() {

            var row = manager.getSelectedRow();

            if (!row) { $.ligerDialog.warn('Please select row'); return; }

            var idString = checkedCustomer.join(',');

            $.ajax({
                type: "GET",
                url: "VenderList.aspx",
                data: "Action=checkNoRow&idString=" + idString + "&ranid=" + Math.random(), //encodeURI
                success: function (resultString) {
                    $.ligerDialog.alert(resultString, 'Information');
                    reload();
                },
                error: function (msg) {
                    $.ligerDialog.alert("Network error, please contact an administrator", 'Information');
                }
            });
        }

        function excel() {
            var title = "Import Vender";

            $.ligerDialog.open({
                title: title,
                url: 'VenderListExcel.aspx',
                height: 450,
                width: 550,
                modal: true
            });
        }

        function reload() {
            manager.reload();
        }


        function f_onCheckAllRow(checked) {
            for (var rowid in this.records) {
                if (checked)
                    addCheckedCustomer(this.records[rowid]['id']);
                else
                    removeCheckedCustomer(this.records[rowid]['id']);
            }
        }

        var checkedCustomer = [];
        function findCheckedCustomer(id) {
            for (var i = 0; i < checkedCustomer.length; i++) {
                if (checkedCustomer[i] == id) return i;
            }
            return -1;
        }
        function addCheckedCustomer(id) {
            if (findCheckedCustomer(id) == -1)
                checkedCustomer.push(id);
        }
        function removeCheckedCustomer(id) {
            var i = findCheckedCustomer(id);
            if (i == -1) return;
            checkedCustomer.splice(i, 1);
        }
        function f_isChecked(rowdata) {
            if (findCheckedCustomer(rowdata.id) == -1)
                return false;
            return true;
        }
        function f_onCheckRow(checked, data) {
            if (checked) addCheckedCustomer(data.id);
            else removeCheckedCustomer(data.id);
        }
        function f_getChecked() {
            alert(checkedCustomer.join(','));
        }
    </script>
</head>

<body style="padding-left: 10px; padding-top: 10px;">
    <form id="form1" runat="server">
        <div id="maingrid"></div>
        <div style="display: none;"></div>
    </form>
</body>
</html>

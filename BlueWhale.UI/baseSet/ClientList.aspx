<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientList.aspx.cs" Inherits="BlueWhale.UI.baseSet.ClientList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Client Management</title>
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
    <script type="text/javascript">
        var manager;
        $(function() {
            manager = $("#maingrid").ligerGrid(
                {
                    checkbox: true,
                    columns: [
                        { display: 'Action', isSort: false, width: 60, align: 'center' },
                        { display: 'Client Category', name: 'typeName', width: 130, type: 'int', align: 'center' },
                        { display: 'Client Number', name: 'code', width: 125, align: 'center' },
                        { display: 'Client Name', name: 'names', width: 115, align: 'left' },
                        { display: 'Balance Date', name: 'yueDate', width: 90, align: 'center' },
                        { display: 'Opening Balance', name: 'balance', width: 120, align: 'center' },
                        { display: 'Tax Rate %', name: 'tax', width: 80, align: 'center' },
                        { display: 'Tax Number', name: 'taxNumber', width: 85, align: 'center' },
                        { display: 'Bank of Account', name: 'bankName', width: 100, align: 'center' },
                        { display: 'Bank Account Number', name: 'bankNumber', width: 160, align: 'center' },
                        { display: 'Address', name: 'dizhi', width: 60, align: 'center' },
                        { display: 'Primary Contact Person', name: 'linkMan', width: 160, align: 'center' },
                        { display: 'Phone No.', name: 'phone', width: 100, align: 'center' },
                        { display: 'Fax No.', name: 'tel', width: 110, align: 'center', type: "date" },
                        { display: 'Status', name: 'flag', width: 50, align: 'center' },
                        { display: 'Entry Date', name: 'makeDate', width: 80, align: 'center', type: "date" }
                    ],
                    width: '99%',
                    height: '99%',
                    dataAction: 'local',
                    usePager: false,
                    url: 'ClientList.aspx?Action=GetDataList', 
                    alternatingRow: false,
                    isChecked: f_isChecked, onCheckRow: f_onCheckRow, onCheckAllRow: f_onCheckAllRow,
                    rownumbers: true, 
                    toolbar: {
                        items: [
                            { text: 'Refresh', click: reload, img: '../lib/ligerUI/skins/icons/refresh.png'},
                            { line: true },
                            { text: 'Filter Query', click: search, img: '../lib/ligerUI/skins/icons/search.gif'},
                            { line: true },
                            { text: "Add Client", click: addRowTop, img: '../lib/ligerUI/skins/icons/add.gif'},
                            { line: true },
                            { text: "Modify Client", click: editRow, img: '../lib/ligerUI/skins/icons/modify.gif' },
                            { line: true },
                            { text: "Manage Contact", click: linkManForm, img: '../lib/ligerUI/skins/icons/customers.gif' },
                            { line: true },
                            { text: "Approve", click: checkRow, img: '../lib/ligerUI/skins/icons/true.gif' },
                            { line: true },
                            { text: "Reject", click: checkNoRow, img: '../lib/ligerUI/skins/icons/refresh.gif' },
                            { line: true },
                            { text: "Reset Password", click: setPwd, img: '../lib/ligerUI/skins/icons/settings.gif' },
                            { line: true },
                            { text: "Delete Client", click: deleteRow, img: '../lib/ligerUI/skins/icons/delete.gif' },
                            { line: true },
                            { text: 'Batch Import', click: excel, img: '../lib/ligerUI/skins/icons/xls.gif' }
                        ]
                    }
                }
            );
        });

        function reload() {
            manager.reload();
        }

        function search() {
            $.ligerDialog.prompt('Can search by name, mobile phone, telephone number, address, remarks','', function (yes,value) {
                 if(yes) 
                 {
                    var key = value;
                    manager.changePage("first");
                    manager._setUrl("ClientList.aspx?Action=GetDataListSearch&keys="+key);
                 }
             });
        }

        function addRowTop() {
            var title = "Add Client";

            $.ligerDialog.open({
                title: title,
                url: 'ClientListAdd.aspx?id=0',
                height: 500,
                width: 1050,
                modal: false
            });
        } 

        function editRow() {
            var row = manager.getSelectedRow();
            if (!row) {  $.ligerDialog.warn('Please select the row to modify!'); return; }

            var title="Modify Client-"+row.names;
           
            $.ligerDialog.open({ 
                title : title,
                url: 'ClientListAdd.aspx?id='+row.id,
                height:500,
                width:1050,
                modal:true
            });
        }

        function linkManForm() {
            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('Please select customer!'); return; }

            var title = "Contact Management-" + row.names;

            $.ligerDialog.open({
                title: title,
                url: 'ClientLinkMan.aspx?id=' + row.id,
                height: 450,
                width: 950,
                modal: true

            });
        }

        function checkRow() {
            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('Please select the row to operate on'); return; }

            var idString = checkedCustomer.join(',');

            $.ajax({
                type: "GET",
                url: "ClientList.aspx",
                data: "Action=checkRow&idString=" + idString + "&ranid=" + Math.random(),
                success: function (resultString) {
                    $.ligerDialog.alert(resultString, 'Notification');
                    reload();

                },
                error: function (msg) {
                    $.ligerDialog.alert("Network error, please contact the administrator", 'Notification');
                }
            });
        }

        function checkNoRow() {
            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('Please select the row to operate on'); return; }

            var idString = checkedCustomer.join(',');

            $.ajax({
                type: "GET",
                url: "ClientList.aspx",
                data: "Action=checkNoRow&idString=" + idString + "&ranid=" + Math.random(),
                success: function (resultString) {
                    $.ligerDialog.alert(resultString, 'Notification');
                    reload();

                },
                error: function (msg) {

                    $.ligerDialog.alert("Network error, please contact the administrator", 'Notification');
                }
            });
        }

        function setPwd() {
            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('Please select the user to set'); return; }

            $.ligerDialog.confirm('The initial password is 123456，Confirm Reset？', function (type) {
                if (type) {
                    $.ajax({
                        type: "GET",
                        url: "ClientList.aspx",
                        data: "Action=setPwd&id=" + row.id + " &ranid=" + Math.random(),
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

        function deleteRow() {
            var row = manager.getSelectedRow();
            if (!row) {  $.ligerDialog.warn('Please select the rows you want to delete'); return; }
            
            var idString = checkedCustomer.join(',');
           
            $.ligerDialog.confirm('Deletion cannot be restored. Confirm deletion?', function(type) {
                if (type) {
                    $.ajax({
                        type: "GET",
                        url: "ClientList.aspx",
                        data: "Action=delete&idString=" + idString + "&ranid=" + Math.random(), //encodeURI
                        success: function(resultString) {
                            $.ligerDialog.alert(resultString, 'Notification');
                            reload();
                        },
                        error: function(msg) {
                            $.ligerDialog.alert("Network error, please contact the administrator", 'Notification');
                        }
                    });
                }
            });
        }

        function excel() {
            var title = "Import Customers";

            $.ligerDialog.open({
                title: title,
                url: 'ClientListExcel.aspx',
                height: 450,
                width: 550,
                modal: true
            });
        }

        var checkedCustomer = [];
        function findCheckedCustomer(id) {
            for (var i = 0; i < checkedCustomer.length; i++) {
                if (checkedCustomer[i] == id) return i;
            }
            return -1;
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

        function addCheckedCustomer(id) {
            if (findCheckedCustomer(id) == -1)
                checkedCustomer.push(id);
        }

        function removeCheckedCustomer(id) {
            var i = findCheckedCustomer(id);
            if (i == -1) return;
            checkedCustomer.splice(i, 1);
        }

        function f_onCheckAllRow(checked) {
            for (var rowid in this.records) {
                if (checked)
                    addCheckedCustomer(this.records[rowid]['id']);
                else
                    removeCheckedCustomer(this.records[rowid]['id']);
            }
        }
    </script>  
    <style type="text/css">
        .l-button{width: 120px; float: left; margin-left: 10px; margin-bottom:2px; margin-top:2px;}
    </style>
</head>
<body style="padding-left:10px;padding-top:10px;">
    <form id="form1" runat="server">
       <div id="maingrid"></div>  
        <div style="display:none;"></div>
    </form>
</body>
</html>

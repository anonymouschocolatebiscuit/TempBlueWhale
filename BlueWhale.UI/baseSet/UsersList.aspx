<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsersList.aspx.cs" Inherits="BlueWhale.UI.baseSet.UsersList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Management</title>

    <!-- LigerUI Stylesheets -->
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />

    <!-- JavaScript Libraries -->
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>

    <script type="text/javascript">
        var manager;
        $(function () {
            manager = $("#maingrid").ligerGrid({
                columns: [
                    { display: 'Mobile No.', name: 'phone', width: 150, align: 'center' },
                    { display: 'Name', name: 'names', width: 120, align: 'center' },
                    { display: 'Phone No.', name: 'tel', width: 120, align: 'center' },
                    { display: 'Email', name: 'email', width: 120, align: 'center' },
                    { display: 'Address', name: 'address', width: 100, align: 'center' },
                    { display: 'Date of Birth', name: 'brithDay', width: 100, align: 'center', type: "date" },
                    { display: 'Onboard Date', name: 'comeDate', width: 130, align: 'center', type: "date" },
                    { display: 'Status', name: 'flag', width: 80, align: 'center' },
                ],
                width: '99%',
                pageSizeOptions: [20, 50, 100, 200],
                height: '100%',
                url: 'UsersList.aspx?Action=GetDataList',
                rownumbers: true,
                pageSize: 50,
                dataAction: 'local',
                usePager: true,
                alternatingRow: false,
                onDblClickRow: function (data, rowindex, rowobj) {
                    editRow();
                },
                toolbar: {
                    items: [
                        { text: 'Reload', click: reload, img: '../lib/ligerUI/skins/icons/refresh.png' },
                        { line: true },
                        { text: "Create User", click: addRowTop, img: '../lib/ligerUI/skins/icons/add.gif' },
                        { line: true },
                        { text: "Edit User", click: editRow, img: '../lib/ligerUI/skins/icons/modify.gif' },
                        { line: true },
                        { text: "Delete User", click: deleteRow, img: '../lib/ligerUI/skins/icons/delete.gif' },
                        { line: true },
                        { text: 'Reset Password', click: setPwd, img: '../lib/ligerUI/skins/icons/settings.gif' },
                        { line: true },
                        { text: 'Edit User Permission', click: setRight, img: '../lib/ligerUI/skins/icons/lock.gif' }
                    ]
                }
            });
        });

        function downQY() {
            $.ligerDialog.confirm('This action will sync all existing employee data to the enterprise account. Do you confirm the operation?', function (type) {
                if (type) {
                    $.ajax({
                        type: "GET",
                        url: "UsersList.aspx",
                        data: "Action=downQY&ranid=" + Math.random(),
                        success: function (resultString) {
                            $.ligerDialog.alert(resultString, 'Notification');
                            reload();
                        },
                        error: function (msg) {
                            $.ligerDialog.alert("Network error, please contact the administrator!", 'Notification');
                        }
                    });
                }
            });
        }

        function getSelected() {
            var row = manager.getSelectedRow();
            if (!row) { alert('Please select row'); return; }
            alert(JSON.stringify(row));
        }

        function getData() {
            var data = manager.getData();
            alert(JSON.stringify(data));
        }

        function editRow() {
            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('Please select the row to edit!'); return; }

            var title = "Edit User";
            $.ligerDialog.open({
                title: title,
                url: 'UsersListAdd.aspx?id=' + row.id,
                height: 450,
                width: 650,
                modal: true
            });
        }

        function deleteRow() {
            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('Please select the rows you want to delete'); return; }

            if (row.flag == "启用") {
                $.ligerDialog.warn('Status cannot be deleted when enabled!');
                return;
            }

            $.ligerDialog.confirm('Deletion cannot be restored. Confirm deletion?', function (type) {
                if (type) {
                    $.ajax({
                        type: "GET",
                        url: "UsersList.aspx",
                        data: "Action=delete&id=" + row.id + "&ranid=" + Math.random(),
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

        function setPwd() {
            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('Please select the user to set'); return; }

            $.ligerDialog.confirm('The initial password is 123456. Confirm reset?', function (type) {
                if (type) {
                    $.ajax({
                        type: "GET",
                        url: "UsersList.aspx",
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

        function setRight() {
            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('Please select the user to set'); return; }

            var title = "Edit User Permission--" + row.names;
            $.ligerDialog.open({
                title: title,
                url: 'UsersListRight.aspx?id=' + row.id,
                height: 550,
                width: 400,
                modal: true
            });
        }

        function reload() {
            manager.changePage("first");
            manager.reload();
        }

        function addRowTop() {
            var title = "Create User";
            $.ligerDialog.open({
                title: title,
                url: 'UsersListAdd.aspx',
                height: 450,
                width: 650,
                modal: false
            });
        }
    </script>

    <!-- Custom Styles -->
    <style type="text/css">
        .l-button { width: 120px; float: left; margin-left: 10px; margin-bottom: 2px; margin-top: 2px; }
    </style>

</head>
<body style="padding-left: 10px; padding-top: 10px;">
    <form id="form1" runat="server">
        <div id="maingrid"></div>
    </form>
</body>
</html>

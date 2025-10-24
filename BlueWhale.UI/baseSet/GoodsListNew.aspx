<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsListNew.aspx.cs" Inherits="BlueWhale.UI.baseSet.GoodsListNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Good List</title>

    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />

    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
    <script type="text/javascript">

        document.onkeydown = keyDownSearch;

        function keyDownSearch(e) {
            var theEvent = e || window.event;
            var code = theEvent.keyCode || theEvent.which || theEvent.charCode;

            if (code == 13) {
                $("#btnSearch").click();
                return false;
            }
            return true;
        }

        var manager = null;

        $(function () {
            var form = $("#formTB").ligerForm();
            var txtKeys = $.ligerui.get("txtKeys");
            txtKeys.set("Width", 300);
            manager = $("#maingrid4").ligerGrid({
                checkbox: true,
                columns: [
                    { display: 'Good Code', name: 'code', width: 100, align: 'center' },
                    { display: 'Good Name', name: 'names', width: 150, align: 'left' },
                    { display: 'Specification', name: 'spec', width: 70, align: 'center' },
                    { display: 'Unit', name: 'unitName', width: 70, align: 'center' },
                    { display: 'Preferred Warehouse', name: 'ckName', width: 60, align: 'center' },
                    { display: 'Unit Cost', name: 'costUnit', width: 70, align: 'center' },
                    { display: 'Initial Total Price', name: 'sumNumStart', width: 100, align: 'center' },
                    { display: 'Estimated Purchase Price', name: 'priceCost', width: 80, align: 'center', type: "date" },
                    { display: 'Estimated Sales Price', name: 'priceSales', width: 80, align: 'center' },
                    { display: 'Barcode', name: 'barcode', width: 100, align: 'center' },
                    { display: 'Minimum Stock', name: 'numMin', width: 60, align: 'center' },
                    { display: 'Maximum Stock', name: 'numMax', width: 60, align: 'center' },
                    { display: 'Weighing', name: 'isWeight', width: 60, align: 'center' },
                    { display: 'Status', name: 'flag', width: 80, align: 'center' },
                    { display: 'Opening Stock', name: 'sumNumStart', width: 80, align: 'center', type: "date" }
                ], pageSize: 10,
                usePager: false,
                url: 'GoodsListSelect.aspx?Action=GetDataList',
                width: '690', height: '600',
                isChecked: f_isChecked, onCheckRow: f_onCheckRow, onCheckAllRow: f_onCheckAllRow,
                rownumbers: true,
                toolbar: {
                    items: [
                        { text: 'Refresh', click: reload, img: '../lib/ligerUI/skins/icons/refresh.png' },
                        { line: true },
                        { text: "Add Product", click: addRowTop, img: '../lib/ligerUI/skins/icons/add.gif' },
                        { line: true },
                        { text: "Edit Product", click: editRow, img: '../lib/ligerUI/skins/icons/modify.gif' },
                        { line: true },
                        { text: "Delete Product", click: deleteRow, img: '../lib/ligerUI/skins/icons/delete.gif' },
                        { line: true },
                        { text: 'Initial Stock Management', click: linkManForm, img: '../lib/ligerUI/skins/icons/view.gif' },
                        { line: true },
                        { text: 'Batch Import', click: excel, img: '../lib/ligerUI/skins/icons/xls.gif' }
                    ]
                }
            });
            $("#pageloading").hide();
        });

        function f_select() {
            return manager.getSelectedRows();
        }

        function search() {
            var keys = document.getElementById("txtKeys").value;

            if (keys == "Please enter code/name/specification") {
                keys = "";
            }

            manager.changePage("first");
            manager._setUrl("GoodsListSelect.aspx?Action=GetDataListSearch&keys=" + keys + "&typeId=" + typeId);
        }

        var typeId = 0;

        $(function () {
            $("#tree1").ligerTree({
                url: "GoodsTypeList.aspx?Action=GetTreeList",
                onSelect: onSelect,
                parentIcon: null,
                childIcon: null,
                checkbox: false,
                slide: false,
                treeLine: true,
                idFieldName: 'id',
                parentIDFieldName: 'pid'
            }
            );
        });

        function onSelect(note) {
            //  alert('onSelect:' + note.data.id);
            typeId = note.data.id;
            search();
        }

        function excel() {
            var title = "Import Good";

            $.ligerDialog.open({
                title: title,
                url: 'GoodsListExcel.aspx',
                height: 450,
                width: 550,
                modal: true
            });
        }

        function addRowTop() {
            var title = "Add Good";

            $.ligerDialog.open({
                title: title,
                url: 'GoodsListAdd.aspx?id=0',
                height: 500,
                width: 650,
                modal: false
            });
        }

        function editRow() {
            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('Please select the row to modify!'); return; }
            var title = "Edit Good-" + row.names;

            $.ligerDialog.open({
                title: title,
                url: 'GoodsListAdd.aspx?id=' + row.id,
                height: 500,
                width: 650,
                modal: true
            });
        }

        function linkManForm() {
            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('Please select good!'); return; }
            var title = "Good Opening Stock-" + row.names;

            $.ligerDialog.open({
                title: title,
                url: 'GoodsListNumStart.aspx?id=' + row.id,
                height: 450,
                width: 650,
                modal: true
            });
        }

        function deleteRow() {
            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('Please select the rows you want to delete!'); return; }
            $.ligerDialog.confirm('Deletion cannot be restored. Confirm deletion?', function (type) {

                if (type) {
                    $.ajax({
                        type: "GET",
                        url: "GoodsList.aspx",
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
<body>
    <form id="form1" runat="server">
        <table id="formTB" border="0" style="width: 100%; line-height: 40px;">
            <tr>
                <td>
                    <asp:TextBox ID="txtKeys" runat="server" placeholder="Please enter code/name/specification"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <input id="btnSearch" type="button" value="Search" class="ui-btn" onclick="search()" /></td>
            </tr>
            <tr>
                <td>
                    <div id="maingrid4" style="margin: 0; padding: 0"></div>
                </td>
                <td valign="top">
                    <div style="width: 200px; position: relative; height: 600px; display: block; margin: 10px; background: white; border: 1px solid #ccc; overflow: auto;">
                        <ul id="tree1">
                        </ul>
                    </div>
                    <div style="display: none">
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

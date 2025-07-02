<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsList.aspx.cs" Inherits="BlueWhale.UI.baseSet.GoodsList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Product Management</title>

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
                    { display: 'Product Code', name: 'code', width: 100, align: 'center' },
                    { display: 'Product Name', name: 'names', width: 120, align: 'left' },
                    { display: 'Barcode', name: 'barcode', width: 100, align: 'center' },
                    { display: 'Product Type', name: 'typeName', width: 100, align: 'center' },
                    { display: 'Brand', name: 'brandName', width: 70, align: 'center' },
                    { display: 'Specification', name: 'spec', width: 120, align: 'center' },
                    { display: 'Unit', name: 'unitName', width: 70, align: 'center' },
                    { display: 'Default Warehouse', name: 'ckName', width: 140, align: 'center' },
                    { display: 'Place of Origin', name: 'place', width: 120, align: 'center' },
                    { display: 'Purchase Price', name: 'priceCost', width: 120, align: 'center', type: "date" },
                    { display: 'Wholesale Price', name: 'priceSalesWhole', width: 120, align: 'center' },
                    { display: 'Retail Price', name: 'priceSalesRetail', width: 100, align: 'center' },
                    { display: 'Minimum Stock', name: 'numMin', width: 120, align: 'center' },
                    { display: 'Maximum Stock', name: 'numMax', width: 120, align: 'center' },
                    { display: 'Status', name: 'isShow', width: 80, align: 'center' }
                ],
                usePager: false,
                url: 'GoodsList.aspx?Action=GetDataList',
                width: '690', height: '98%',
                isChecked: f_isChecked, onCheckRow: f_onCheckRow, onCheckAllRow: f_onCheckAllRow,
                onDblClickRow: function (data, rowindex, rowobj) {
                    editRow();
                },
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
                        { text: 'Opening Stock', click: linkManForm, img: '../lib/ligerUI/skins/icons/view.gif' },
                        { line: true },
                        //{ text: 'Navigation Type', click: f_showTypeList, img: '../lib/ligerUI/skins/icons/view.gif' },
                        //{ line: true },
                        { text: 'Image Management', click: f_showImage, img: '../lib/ligerUI/skins/icons/photograph.gif' },
                        { line: true },
                        { text: 'Item Details', click: f_hideImage, img: '../lib/ligerUI/skins/icons/view.gif' },
                        { line: true },
                        { text: 'Products on Sale', click: searchOnLine, img: '../lib/ligerUI/skins/icons/miniicons/date_new.gif' },
                        { line: true },
                        { text: 'Batch Take Down', click: checkNoRow, img: '../lib/ligerUI/skins/icons/outbox.gif' },
                        { line: true },
                        { text: 'Pending Products', click: searchOffLine, img: '../lib/ligerUI/skins/icons/miniicons/date_delete.gif' },
                        { line: true },
                        { text: 'Batch Listing', click: checkRow, img: '../lib/ligerUI/skins/icons/true.gif' },
                        { line: true },
                    ]
                }
            });
            $("#pageloading").hide();
        });

        function f_select() {
            return manager.getSelectedRows();
        }

        function f_hideImage() {
            var row = manager.getSelectedRow();

            if (!row) { $.ligerDialog.warn('Please select the row to modify!'); return; }

            var title = "Product Detail-" + row.names;
            $.ligerDialog.open({
                title: title,
                url: 'GoodsListDetail.aspx?id=' + row.id,
                height: 600,
                width: 650,
                modal: true
            });
        }

        function f_showImage() {
            var row = manager.getSelectedRow();

            if (!row) { $.ligerDialog.warn('Please select the row to modify!'); return; }

            var title = "Product Image-" + row.names;
            $.ligerDialog.open({
                title: title,
                url: 'GoodsListImages.aspx?id=' + row.id,
                height: 650,
                width: 850,
                modal: true
            });
        }

        //function f_showTypeList() {
        //    var row = manager.getSelectedRow();

        //    if (!row) { $.ligerDialog.warn('Please select the row to modify!'); return; }

        //    var title = "Product Navigation Type-" + row.names;
        //    $.ligerDialog.open({
        //        title: title,
        //        url: 'GoodsListTypeList.aspx?id=' + row.id,
        //        height: 450,
        //        width: 450,
        //        modal: true
        //    });
        //}

        function f_showPrice() {
            var row = manager.getSelectedRow();

            if (!row) { $.ligerDialog.warn('Please select a row!'); return; }

            var title = "Product Level Price-" + row.names;
            $.ligerDialog.open({
                title: title,
                url: 'GoodsPriceClientType.aspx?id=' + row.id,
                height: 450,
                width: 550,
                modal: true
            });
        }

        function search() {
            var keys = document.getElementById("txtKeys").value;
            if (keys == "Please enter code/barcode/name/specification") {
                keys = "";
            }
            manager.changePage("first");
            manager._setUrl("GoodsList.aspx?Action=GetDataListSearch&keys=" + keys + "&typeId=" + typeId + "&isShow=" + isShow);
        }

        function searchOnLine() {
            isShow = 1;
            search();
        }

        function searchOffLine() {
            isShow = 0;
            search();
        }

        var typeId = 0;

        var isShow = -1;

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
            });
        });

        function onSelect(note) {
            //  alert('onSelect:' + note.data.id);
            typeId = note.data.id;
            search();
        }

        function excel() {
            var title = "Import Product";
            $.ligerDialog.open({
                title: title,
                url: 'GoodsListExcel.aspx',
                height: 450,
                width: 550,
                modal: true
            });
        }

        function addRowTop() {
            var title = "Add Product";
            $.ligerDialog.open({
                title: title,
                url: 'GoodsListAdd.aspx?id=0',
                height: 710,
                width: 750,
                modal: false
            });
        }

        function editRow() {
            var row = manager.getSelectedRow();

            if (!row) { $.ligerDialog.warn('Please select the row to modify!'); return; }

            var title = "Edit Product-" + row.names;
            $.ligerDialog.open({
                title: title,
                url: 'GoodsListAdd.aspx?id=' + row.id,
                height: 710,
                width: 750,
                modal: true
            });
        }

        function linkManForm() {
            var row = manager.getSelectedRow();

            if (!row) { $.ligerDialog.warn('Please select Product!'); return; }

            var title = "Product Opening Stock-" + row.names;
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

            if (!row) { $.ligerDialog.warn('Please select the row to delete'); return; }

            var idString = checkedCustomer.join(',');
            $.ligerDialog.confirm('Deletion cannot be restored. Confirm deletion?', function (type) {
                if (type) {
                    window.open("GoodsList.aspx?Action=delete&idString=" + idString + " &ranid=" + Math.random());
                    $.ajax({
                        type: "GET",
                        url: "GoodsList.aspx",
                        data: "Action=delete&idString=" + idString + " &ranid=" + Math.random(),
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

        function checkRow() {
            var row = manager.getSelectedRow();

            if (!row) { $.ligerDialog.warn('Please select the row to operate on'); return; }

            var idString = checkedCustomer.join(',');
            $.ajax({
                type: "GET",
                url: "GoodsList.aspx",
                data: "Action=checkRow&idString=" + idString + "&ranid=" + Math.random(), //encodeURI
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
                url: "GoodsList.aspx",
                data: "Action=checkNoRow&idString=" + idString + "&ranid=" + Math.random(), //encodeURI
                success: function (resultString) {
                    $.ligerDialog.alert(resultString, 'Notification');
                    reload();
                },
                error: function (msg) {
                    $.ligerDialog.alert("Network error, please contact the administrator", 'Notification');
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
        <table id="formTB" border="0" style="width: 99%; line-height: 40px;">
            <tr>
                <td>
                    <asp:TextBox ID="txtKeys" runat="server" placeholder="Please enter code/barcode/name/specification"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <input id="btnSearch" type="button" value="Search" class="ui-btn" onclick="search()" />
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div id="maingrid4" style="margin: 0; padding: 0"></div>
                </td>
                <td valign="top">
                    <div
                        style="width: 200px; position: relative; height: 520px; display: block; margin: 10px; background: white; border: 1px solid #ccc; overflow: auto;">
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
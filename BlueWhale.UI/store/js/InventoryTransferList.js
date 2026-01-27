
var manager;
$(function () {

    var form = $("#form").ligerForm();

    var menu = $.ligerMenu({
        width: 120, items:
            [
                { text: 'Add', click: add, icon: 'add' },
                { text: 'Edit', click: editRow },
                { line: true },
                { text: 'View', click: viewRow },
                { line: true },
                { text: 'Outbound', click: makeBill }
            ]
    });

    var txtKeys = $.ligerui.get("txtKeys");
    txtKeys.set("Width", 140);

    var dateStart = $.ligerui.get("txtDateStart");
    dateStart.set("Width", 110);

    var dateEnd = $.ligerui.get("txtDateEnd");
    dateEnd.set("Width", 110);

    var txtInventoryOut = $.ligerui.get("txtInventoryOut");
    txtInventoryOut.set("Width", 100);

    var txtInventoryIn = $.ligerui.get("txtInventoryIn");
    txtInventoryIn.set("Width", 100);

    manager = $("#maingrid").ligerGrid({
        checkbox: true,
        columns: [
            {
                display: 'Action', isSort: false, width: 50, align: 'center', render: function (rowdata, rowindex, value) {
                    var h = "";
                    if (!rowdata._editing) {
                        h += "<a href='javascript:editRow()' title='Edit Row' style='float:left;'><div class='ui-icon ui-icon-pencil'></div></a> ";
                        h += "<a href='javascript:deleteRow()' title='Delete row' style='float:right;'><div class='ui-icon ui-icon-trash'></div></a> ";
                    }
                    else {
                        h += "<a href='javascript:endEdit(" + rowindex + ")'>Submit</a> ";
                        h += "<a href='javascript:cancelEdit(" + rowindex + ")'>Cancel</a> ";
                    }
                    return h;
                }
            },
            {
                display: 'Inventory Transfer Date', name: 'bizDate', width: 160, align: 'center', valign: 'center',
                totalSummary:
                {
                    type: 'count',
                    render: function (e) {  //Summary renderer, returns HTML to load into the cell
                        //e Summary Object (including sum, max, min, avg, count)
                        return 'Total:';
                    }
                }
            },
            { display: 'Receipt Number', name: 'number', width: 150, align: 'center' },
            { display: 'Item Number', name: 'code', width: 70, align: 'center' },
            { display: 'Item Name', name: 'goodsName', width: 120, align: 'left' },
            { display: 'Specification', name: 'spec', width: 120, align: 'center' },
            { display: 'Unit', name: 'unitName', width: 50, align: 'center' },
            { display: 'Source Warehouse', name: 'ckNameOut', width: 120, align: 'center' },
            { display: 'Destination Warehouse', name: 'ckNameIn', width: 120, align: 'right' },
            {
                display: 'Quantity', name: 'num', width: 70, align: 'right',
                totalSummary:
                {
                    align: 'right',   //Summary cell content alignment: left/center/right
                    type: 'sum',
                    render: function (e) {  //Summary renderer, returns HTML to load into the cell
                        //e Summary Object (including sum, max, min, avg, count)
                        return Math.round(e.sum * 100) / 100;
                    }
                }
            },
            { display: 'Created By', name: 'makeName', width: 90, align: 'center' },
            { display: 'Status', name: 'flag', width: 70, align: 'center' },
            { display: 'Remarks', name: 'remarks', width: 100, align: 'left' }
        ], width: '96%',
        //pageSizeOptions: [5, 10, 15, 20],
        height: '98%',
        // pageSize: 15,
        dataAction: 'local', //Local sorting
        usePager: false,
        //url: "InventoryTransferList.aspx?Action=GetDataList", 
        alternatingRow: false,
        onDblClickRow: function (data, rowindex, rowobj) {
            
            viewRow();
        },

        isChecked: f_isChecked, onCheckRow: f_onCheckRow, onCheckAllRow: f_onCheckAllRow
    });
});

function f_set() {
    form.setData({
        keys: "",
        dateStart: new Date("<% =start%>"),
        dateEnd: new Date("<% =end%>")
    });
}

function search() {
    var keys = document.getElementById("txtKeys").value;
    if (keys == "Please Enter Receipt No./Item/Remarks") {
        keys = "";
    }
    var start = $("#txtDateStart").val();
    var end = $("#txtDateEnd").val();
    var ckIdIn = $("#txtInventoryIn").val();
    var ckIdOut = $("#txtInventoryOut").val();

    //            alert(ckIdIn);
    //            alert(ckIdOut);

    manager.changePage("first");
    manager._setUrl("InventoryTransferList.aspx?Action=GetDataList&keys=" + keys + "&start=" + start + "&end=" + end + "&ckIdIn=" + ckIdIn + "&ckIdOut=" + ckIdOut);
}

function deleteRow() {

    var row = manager.getSelectedRow();
    if (!row) { $.ligerDialog.warn('Please select the row(s) to delete'); return; }

    var idString = checkedCustomer.join(',');

    $.ligerDialog.confirm('Cannot be recovered after clearing. Confirm deletion？', function (type) {

        if (type) {
            $.ajax({
                type: "GET",
                url: "InventoryTransferList.aspx",
                data: "Action=delete&id=" + idString + " &ranid=" + Math.random(),
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
    //alert(idString);

    $.ajax({
        type: "POST",
        url: "InventoryTransferList.aspx",
        data: "Action=checkRow&idString=" + idString + "&ranid=" + Math.random(), //encodeURI
        success: function (resultString) {
            $.ligerDialog.alert(resultString, 'Notification');
            reload();
        },
        error: function (msg) {
            alert(msg);
            $.ligerDialog.alert("Network error, please contact the administrator", 'Notification');
        }
    });
}

function checkNoRow() {
    var row = manager.getSelectedRow();
    if (!row) { $.ligerDialog.warn('Please select the row to operate on'); return; }

    var idString = checkedCustomer.join(',');

    $.ajax({
        type: "POST",
        url: "InventoryTransferList.aspx",
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

function add() {
    parent.f_addTab('InventoryTransferListAdd', 'InventoryTransfer - Create', 'store/InventoryTransferListAdd.aspx');
    //          top.topManager.openPage({
    //            id : 'InventoryTransferListAdd',
    //            href : 'store/InventoryTransferListAdd.aspx',
    //            title : 'InventoryTransfer - Create'
    //          });
}

function makeBill() {
    var row = manager.getSelectedRow();

    //alert(row.id);
    // return;
    //window.open("PurReceiptListAdd.aspx?id="+row.id);    
    //return;
    // alert('buy/PurReceiptListAdd.aspx?id='+row.id);

    top.topManager.openPage({
        id: 'SalesReceiptListAdds',
        href: 'sales/SalesReceiptListAdd.aspx?id=' + row.id,
        title: 'Sales Outbound - Create'
    });
}

function editRow() {
    var row = manager.getSelectedRow();

    parent.f_addTab('InventoryTransferListEdits', 'Inventory Transfer - Edit', 'store/InventoryTransferListEdit.aspx?id=' + row.id);

    top.topManager.openPage({
        id: 'InventoryTransferListEdits',
        href: 'store/InventoryTransferListEdit.aspx?id=' + row.id,
        title: 'Inventory Transfer - Edit'
    });
}

function viewRow() {
    var row = manager.getSelectedRow();

    parent.f_addTab('InventoryTransferListView', 'Inventory Transfer - View', 'store/InventoryTransferListView.aspx?id=' + row.id);

    top.topManager.openPage({
        id: 'InventoryTransferListView',
        href: 'store/InventoryTransferListView.aspx?id=' + row.id,
        title: 'Inventory Transfer - View'
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
var manager;
$(function () {
    var form = $("#form").ligerForm();

    var menu = $.ligerMenu({
        width: 120, items:
            [
                { text: 'Add', click: add, icon: 'add' },
                { text: 'Modify', click: editRow },
                { line: true },
                { text: 'View', click: viewRow },
                { line: true },
                { text: 'Ship', click: makeBill },
                { line: true },
                { text: 'Print(Including Unit Price)', click: makePDF },
                { line: true },
                { text: 'Print(No Unit Price)', click: makePDFNoPrice }
            ]
    });

    manager = $("#maingrid").ligerGrid({
        checkbox: true,
        columns: [
            {
                display: 'Action', isSort: false, width: 50, align: 'center', render: function (rowdata, rowindex, value) {
                    var h = "";
                    if (!rowdata._editing) {
                        h += "<a href='javascript:editRow()' title='Edit Row' style='float:left;'><div class='ui-icon ui-icon-pencil'></div></a> ";
                        h += "<a href='javascript:deleteRow()' title='Delete Row' style='float:right;'><div class='ui-icon ui-icon-trash'></div></a> ";
                    }
                    else {
                        h += "<a href='javascript:endEdit(" + rowindex + ")'>Submit</a> ";
                        h += "<a href='javascript:cancelEdit(" + rowindex + ")'>Cancel</a> ";
                    }
                    return h;
                }
            },
            {
                display: 'Order Date', name: 'bizDate', width: 80, align: 'center', valign: 'center',

                totalSummary:
                {
                    type: 'count',
                    render: function (e) {
                        return 'Total: ';
                    }
                }
            },
            { display: 'Order Number', name: 'number', width: 150, align: 'center' },
            { display: 'Customer', name: 'wlName', width: 170, align: 'left' },
            {
                display: 'Sales Amount', name: 'sumPriceAll', width: 120, align: 'right',

                totalSummary:
                {
                    align: 'right',
                    type: 'sum',
                    render: function (e) {
                        return Math.round(e.sum * 100) / 100;
                    }
                }
            },
            {
                display: 'Quantity', name: 'sumNum', width: 100, align: 'center',

                totalSummary:
                {
                    align: 'right',
                    type: 'sum',
                    render: function (e) {
                        return Math.round(e.sum * 100) / 100;
                    }
                }
            },
            { display: 'Order Status', name: 'flag', width: 100, align: 'center' },
            { display: 'Send Date', name: 'sendDate', width: 80, align: 'center' },
            { display: 'Created By', name: 'makeName', width: 100, align: 'center' },
            { display: 'Salesperson', name: 'bizName', width: 100, align: 'center' },
            { display: 'Reviewed By', name: 'checkName', width: 100, align: 'center' },
            { display: 'Remark', name: 'remarks', width: 100, align: 'left' }
        ], width: '98%',
        height: '98%',
        dataAction: 'local',
        usePager: false,
        url: "SalesOrderList.aspx?Action=GetDataList",
        alternatingRow: false,
        onDblClickRow: function (data, rowindex, rowobj) {
            viewRow();
        },

        onRClickToSelect: true,
        onContextmenu: function (parm, e) {
            actionCustomerID = parm.data.id;
            menu.show({ top: e.pageY, left: e.pageX });
            return false;
        },

        isChecked: f_isChecked, onCheckRow: f_onCheckRow, onCheckAllRow: f_onCheckAllRow
    });
});

function search() {
    var keys = document.getElementById("txtKeys").value;
    if (keys == "Please Enter Order No./Client/Remark") {
        keys = "";
    }
    var start = document.getElementById("txtDateStart").value;
    var end = document.getElementById("txtDateEnd").value;

    manager.changePage("first");
    manager._setUrl("SalesOrderList.aspx?Action=GetDataListSearch&types=0&keys=" + keys + "&start=" + start + "&end=" + end);
}

function deleteRow() {
    var row = manager.getSelectedRow();
    if (!row) { $.ligerDialog.warn('Please select the rows you want to delete'); return; }

    var idString = checkedCustomer.join(',');

    $.ligerDialog.confirm('Deletion cannot be restored. Confirm deletion?', function (type) {
        if (type) {
            $.ajax({
                type: "GET",
                url: "SalesOrderList.aspx",
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

    $.ajax({
        type: "GET",
        url: "SalesOrderList.aspx",
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
        url: "SalesOrderList.aspx",
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
    parent.f_addTab('SalesOrderListAdd', 'Add Sales Order', 'sales/SalesOrderListAdd.aspx');
}

function makeBill() {
    var row = manager.getSelectedRow();

    parent.f_addTab('SalesReceiptListAdd', 'Add Sales Outbound', 'sales/SalesReceiptListAdd.aspx?id=' + row.id);
}

function editRow() {
    var row = manager.getSelectedRow();
    if (!row) { alert('Please select a row'); return; }

    parent.f_addTab('SalesOrderListEdit', 'Edit Sales Order', 'sales/SalesOrderListEdit.aspx?id=' + row.id);
}

function viewRow() {
    var row = manager.getSelectedRow();
    if (!row) { alert('Please select a row'); return; }

    parent.f_addTab('SalesOrderListEdit', 'Sales Order = Details', 'sales/SalesOrderListEdit.aspx?id=' + row.id);
}

function makePDF() {
    var shopId = $("#hfShopId").val();
    var row = manager.getSelectedRow();
    var id = row.id;
    var number = row.number;

    $.ajax({
        type: 'GET',
        url: 'SalesOrderList.aspx',
        data: 'Action=makePDF&id=' + id + '&number=' + number + '&ranid=' + Math.random(), //encodeURI
        success: function (resultString) {
            if (resultString == 'Generate Successfully!') {
                parent.f_addTab('pdf', 'Sales Order - Print Review', 'sales/pdf/' + shopId + '-' + number + '.pdf');
            }
            else {
                $.ligerDialog.alert(resultString, 'Notification');
            }
        },
        error: function (msg) {
            $.ligerDialog.alert('Network error, please contact the administrator', 'Notification');
        }
    });
}

function makePDFNoPrice() {
    var shopId = $("#hfShopId").val();
    var row = manager.getSelectedRow();
    var id = row.id;
    var number = row.number;

    $.ajax({
        type: 'GET',
        url: 'SalesOrderList.aspx',
        data: 'Action=makePDFNoPrice&id=' + id + '&number=' + number + '&ranid=' + Math.random(), //encodeURI
        success: function (resultString) {
            if (resultString == 'Generate Successfully!') {
                parent.f_addTab('pdf', 'Sales Order - Print Review', 'sales/pdf/' + shopId + '-' + number + '-01.pdf');
            }
            else {
                $.ligerDialog.alert(resultString, 'Notification');
            }
        },
        error: function (msg) {
            $.ligerDialog.alert('Network error, please contact the administrator', 'Notification');
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
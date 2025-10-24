var manager;

$(function () {
    var form = $("#form").ligerForm();

    manager = $("#maingrid").ligerGrid({
        checkbox: true,
        columns: [
            {
                display: 'Action', isSort: false, width: 60, align: 'center', render: function (rowdata, rowindex, value) {
                    var h = "";
                    if (!rowdata._editing) {
                        h += "<a href='javascript:editRow()' title='Edit Row' style='float:left;'><div class='ui-icon ui-icon-pencil'></div></a> ";
                        h += "<a href='javascript:deleteRow()' title='Delete Row' style='float:right;'><div class='ui-icon ui-icon-trash'></div></a> ";
                    } else {
                        h += "<a href='javascript:endEdit(" + rowindex + ")'>Submit</a> ";
                        h += "<a href='javascript:cancelEdit(" + rowindex + ")'>Cancel</a> ";
                    }
                    return h;
                }
            },
            {
                display: 'Payment Date', name: 'bizDate', width: 140, align: 'center', valign: 'center',
                totalSummary: {
                    type: 'count',
                    render: function (e) {
                        return 'Total:';
                    }
                }
            },
            { display: 'Receipt Number', name: 'number', width: 150, align: 'center' },
            { display: 'Vender', name: 'wlName', width: 120, align: 'left' },
            {
                display: 'Payment Amount', name: 'payPriceSum', width: 140, align: 'right',
                totalSummary: {
                    align: 'right',
                    type: 'sum',
                    render: function (e) {
                        return Math.round(e.sum * 100) / 100;
                    }
                }
            },
            {
                display: 'Write-off Amount', name: 'priceCheckNowSum', width: 140, align: 'right',
                totalSummary: {
                    align: 'right',
                    type: 'sum',
                    render: function (e) {
                        return Math.round(e.sum * 100) / 100;
                    }
                }
            },
            {
                display: 'Overall Discount', name: 'disPrice', width: 140, align: 'right',
                totalSummary: {
                    align: 'right',
                    type: 'sum',
                    render: function (e) {
                        return Math.round(e.sum * 100) / 100;
                    }
                }
            },
            {
                display: 'Advance Payment', name: 'payPriceNowMore', width: 150, align: 'right',
                totalSummary: {
                    align: 'right',
                    type: 'sum',
                    render: function (e) {
                        return Math.round(e.sum * 100) / 100;
                    }
                }
            },
            { display: 'Status', name: 'flag', width: 80, align: 'center' },
            { display: 'Created By', name: 'makeName', width: 90, align: 'center' },
            { display: 'Reviewed By', name: 'checkName', width: 90, align: 'center' },
            { display: 'Remarks', name: 'remarks', width: 100, align: 'left' }
        ],
        width: '98%',
        height: '98%',
        dataAction: 'local',
        usePager: false,
        alternatingRow: false,
        onDblClickRow: function (data, rowindex, rowobj) {
            viewRow();
        },
        rownumbers: true,
        onRClickToSelect: true,
        onContextmenu: function (parm, e) {
            actionCustomerID = parm.data.id;
            menu.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        isChecked: f_isChecked,
        onCheckRow: f_onCheckRow,
        onCheckAllRow: f_onCheckAllRow
    });
});

function search() {
    var keys = document.getElementById("txtKeys").value;
    keys = "";
    var start = document.getElementById("txtDateStart").value;
    var end = document.getElementById("txtDateEnd").value;

    manager.changePage("first");
    manager._setUrl("PayMentList.aspx?Action=GetDataListSearch&keys=" + keys + "&start=" + start + "&end=" + end);
}

function deleteRow() {
    var row = manager.getSelectedRow();
    if (!row) {
        $.ligerDialog.warn('Please select a row to delete');
        return;
    }

    var idString = checkedCustomer.join(',');

    $.ligerDialog.confirm('Once deleted, it cannot be recovered. Confirm delete?', function (type) {
        if (type) {
            $.ajax({
                type: "GET",
                url: "PayMentList.aspx",
                data: "Action=delete&id=" + idString + "&ranid=" + Math.random(),
                success: function (resultString) {
                    $.ligerDialog.alert(resultString, 'Notification');
                    reload();
                },
                error: function () {
                    $.ligerDialog.alert("Network error, please contact administrator", 'Notification');
                }
            });
        }
    });
}

function checkRow() {
    var row = manager.getSelectedRow();
    if (!row) {
        $.ligerDialog.warn('Please select a row to operate on');
        return;
    }

    var idString = checkedCustomer.join(',');

    $.ajax({
        type: "GET",
        url: "PayMentList.aspx",
        data: "Action=checkRow&idString=" + idString + "&ranid=" + Math.random(),
        success: function (resultString) {
            $.ligerDialog.alert(resultString, 'Notification');
            reload();
        },
        error: function () {
            $.ligerDialog.alert("Network error, please contact administrator", 'Notification');
        }
    });
}

function checkNoRow() {
    var row = manager.getSelectedRow();
    if (!row) {
        $.ligerDialog.warn('Please select a row to operate on');
        return;
    }

    var idString = checkedCustomer.join(',');

    $.ajax({
        type: "GET",
        url: "PayMentList.aspx",
        data: "Action=checkNoRow&idString=" + idString + "&ranid=" + Math.random(),
        success: function (resultString) {
            $.ligerDialog.alert(resultString, 'Notification');
            reload();
        },
        error: function () {
            $.ligerDialog.alert("Network error, please contact administrator", 'Notification');
        }
    });
}

function add() {
    parent.f_addTab('PayMentListAdd', 'Purchase Payment - Create', 'pay/PayMentListAdd.aspx');
    top.topManager.openPage({
        id: 'PayMentListAdd',
        href: 'pay/PayMentListAdd.aspx',
        title: 'Purchase Payment - Create'
    });
}

function editRow() {
    var row = manager.getSelectedRow();
    parent.f_addTab('PayMentListEdit', 'Purchase Payment - Edit', 'pay/PayMentListEdit.aspx?id=' + row.id);
    top.topManager.openPage({
        id: 'PayMentListEdit',
        href: 'pay/PayMentListEdit.aspx?id=' + row.id,
        title: 'Purchase Payment - Edit'
    });
}

function viewRow() {
    var row = manager.getSelectedRow();
    parent.f_addTab('PayMentListEdit', 'Purchase Payment - Details', 'pay/PayMentListEdit.aspx?id=' + row.id);
    top.topManager.openPage({
        id: 'OtherPayListView',
        href: 'store/OtherPayListView.aspx?id=' + row.id,
        title: 'Other Payment - Details'
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

// ========== Checkbox logic for multi-page selection ==========
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
    return findCheckedCustomer(rowdata.id) !== -1;
}

function f_onCheckRow(checked, data) {
    if (checked) addCheckedCustomer(data.id);
    else removeCheckedCustomer(data.id);
}

function f_getChecked() {
    alert(checkedCustomer.join(','));
}

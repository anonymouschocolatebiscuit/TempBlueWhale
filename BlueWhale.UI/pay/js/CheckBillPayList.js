var manager;
$(function () {
    var form = $("#form").ligerForm();

    var txtDateStart = $.ligerui.get("txtDateStart");
    txtDateStart.set("Width", 120);

    var txtDateEnd = $.ligerui.get("txtDateEnd");
    txtDateEnd.set("Width", 120);

    manager = $("#maingrid").ligerGrid({
        checkbox: true,
        columns: [
            {
                display: 'Action',
                isSort: false,
                width: 60,
                align: 'center',
                render: function (rowdata, rowindex, value) {
                    var h = "";
                    if (!rowdata._editing) {
                        h += "<div style='display:flex; justify-content:center; align-items:center; height:100%; gap:6px;'>";
                        h += "<a href='javascript:editRow()' title='Edit Row'><div class='ui-icon ui-icon-pencil'></div></a> ";
                        h += "<a href='javascript:deleteRow()' title='Delete Row'><div class='ui-icon ui-icon-trash'></div></a> ";
                        h += "</div>";
                    } else {
                        h += "<a href='javascript:endEdit(" + rowindex + ")'>Submit</a> ";
                        h += "<a href='javascript:cancelEdit(" + rowindex + ")'>Cancel</a> ";
                    }
                    return h;
                }
            },
            {
                display: 'Write-off Date',
                name: 'bizDate',
                width: 110,
                align: 'center',
                valign: 'center',
                totalSummary: {
                    type: 'count',
                    render: function (e) {
                        // Summary renderer, return HTML to load into the cell
                        // e Summary Object (includes sum, max, min, avg, count)
                        return 'Total:';
                    }
                }
            },
            { display: 'Receipt Number', name: 'number', width: 150, align: 'center' },
            { display: 'Supplier', name: 'clientName', width: 170, align: 'left' },
            {
                display: 'Write-off Amount',
                name: 'checkPrice',
                width: 150,
                align: 'right',
                totalSummary: {
                    align: 'right', // Summary cell content alignment: left/center/right
                    type: 'sum',
                    render: function (e) {
                        // Summary renderer, return HTML to load into the cell
                        // e Summary Object (includes sum, max, min, avg, count)
                        return Math.round(e.sum * 100) / 100;
                    }
                }
            },
            { display: 'Status', name: 'flag', width: 70, align: 'center' },
            { display: 'Created By', name: 'makeName', width: 100, align: 'center' },
            { display: 'Reviewed By', name: 'checkName', width: 100, align: 'center' },
            { display: 'Remarks', name: 'remarks', width: 150, align: 'left' }
        ],
        width: '98%',
        height: '98%',
        dataAction: 'local', // Local sorting
        usePager: false,
        // url: "ReceivableList.aspx?Action=GetDataList",
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
        isChecked: f_isChecked,
        onCheckRow: f_onCheckRow,
        onCheckAllRow: f_onCheckAllRow
    });
});

function search() {
    var keys = document.getElementById("txtKeys").value;
    if (keys == "Please Enter Receipt No./Supplier/Remark") {
        keys = "";
    }
    var start = document.getElementById("txtDateStart").value;
    var end = document.getElementById("txtDateEnd").value;

    manager.changePage("first");
    manager._setUrl("CheckBillPayList.aspx?Action=GetDataListSearch&keys=" + keys + "&start=" + start + "&end=" + end);
}

function deleteRow() {
    var row = manager.getSelectedRow();
    if (!row) {
        $.ligerDialog.warn('Please select a row to delete');
        return;
    }

    var idString = checkedCustomer.join(','); // Get selected ID string, separated by commas, pass to backend

    $.ligerDialog.confirm('This action cannot be undone, confirm delete?', function (type) {
        if (type) {
            $.ajax({
                type: "GET",
                url: "CheckBillPayList.aspx",
                data: "Action=delete&id=" + idString + "&ranid=" + Math.random(),
                success: function (resultString) {
                    $.ligerDialog.alert(resultString, 'Information');
                    reload();
                },
                error: function (msg) {
                    $.ligerDialog.alert("Network error, please contact the administrator", 'Information');
                }
            });
        }
    });
}

function checkRow() {
    var row = manager.getSelectedRow();
    if (!row) {
        $.ligerDialog.warn('Please select a row to operate');
        return;
    }

    var idString = checkedCustomer.join(','); // Get selected ID string, separated by commas, pass to backend

    $.ajax({
        type: "GET",
        url: "CheckBillPayList.aspx",
        data: "Action=checkRow&idString=" + idString + "&ranid=" + Math.random(),
        success: function (resultString) {
            $.ligerDialog.alert(resultString, 'Information');
            reload();
        },
        error: function (msg) {
            $.ligerDialog.alert("Network error, please contact the administrator", 'Information');
        }
    });
}

function checkNoRow() {
    var row = manager.getSelectedRow();
    if (!row) {
        $.ligerDialog.warn('Please select a row to operate');
        return;
    }

    var idString = checkedCustomer.join(','); // Get selected ID string, separated by commas, pass to backend

    $.ajax({
        type: "GET",
        url: "CheckBillPayList.aspx",
        data: "Action=checkNoRow&idString=" + idString + "&ranid=" + Math.random(),
        success: function (resultString) {
            $.ligerDialog.alert(resultString, 'Information');
            reload();
        },
        error: function (msg) {
            $.ligerDialog.alert("Network error, please contact the administrator", 'Information');
        }
    });
}

function add() {
    top.topManager.openPage({
        id: 'CheckBillPayListAdd',
        href: 'pay/CheckBillPayListAdd.aspx',
        title: 'Payment Write-off - Create'
    });
}

function editRow() {
    var row = manager.getSelectedRow();
    top.topManager.openPage({
        id: 'CheckBillPayListEdit',
        href: 'pay/CheckBillPayListEdit.aspx?id=' + row.id,
        title: 'Payment Write-off - Edit'
    });
}

function reload() {
    manager.reload();
}

function f_onCheckAllRow(checked) {
    for (var rowid in this.records) {
        if (checked) {
            addCheckedCustomer(this.records[rowid]['id']);
        } else {
            removeCheckedCustomer(this.records[rowid]['id']);
        }
    }
}

/*
 This example implements multi-selection with form pagination.
 It uses onCheckRow to remember selected rows, and isChecked to initialize rows as selected.
*/
var checkedCustomer = [];

function findCheckedCustomer(id) {
    for (var i = 0; i < checkedCustomer.length; i++) {
        if (checkedCustomer[i] == id) return i;
    }
    return -1;
}

function addCheckedCustomer(id) {
    if (findCheckedCustomer(id) == -1) {
        checkedCustomer.push(id);
    }
}

function removeCheckedCustomer(id) {
    var i = findCheckedCustomer(id);
    if (i == -1) return;
    checkedCustomer.splice(i, 1);
}

function f_isChecked(rowdata) {
    return findCheckedCustomer(rowdata.id) != -1;
}

function f_onCheckRow(checked, data) {
    if (checked) {
        addCheckedCustomer(data.id);
    } else {
        removeCheckedCustomer(data.id);
    }
}

function f_getChecked() {
    alert(checkedCustomer.join(','));
}

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
                display: 'Action', isSort: false, width: 60, align: 'center',
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
                display: 'Write-off Date', name: 'bizDate', width: 130, align: 'center', valign: 'center',
                totalSummary:
                {
                    type: 'count',
                    render: function () {
                        return 'Total：';
                    }
                }
            },
            { display: 'Receipt Number', name: 'number', width: 150, align: 'center' },
            { display: 'Client Name', name: 'clientName', width: 170, align: 'left' },
            {
                display: 'Write-off Amount', name: 'checkPrice', width: 150, align: 'right',
                totalSummary: {
                    align: 'right',
                    type: 'sum',
                    render: function (e) {
                        return Math.round(e.sum * 100) / 100;
                    }
                }
            },
            { display: 'Status', name: 'flag', width: 80, align: 'center' },
            { display: 'Created By', name: 'makeName', width: 110, align: 'center' },
            { display: 'Reviewed By', name: 'checkName', width: 110, align: 'center' },
            { display: 'Remarks', name: 'remarks', width: 150, align: 'left' }
        ],
        width: '98%',
        height: '98%',
        dataAction: 'local',
        usePager: false,
        alternatingRow: false,
        onDblClickRow: function () {
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
    if (keys === "Search Receipt No./Client/Remarks") keys = "";
    var start = document.getElementById("txtDateStart").value;
    var end = document.getElementById("txtDateEnd").value;

    manager.changePage("first");
    manager._setUrl("CheckBillGetList.aspx?Action=GetDataListSearch&keys=" + keys + "&start=" + start + "&end=" + end);
}

function deleteRow() {
    var row = manager.getSelectedRow();
    if (!row) {
        $.ligerDialog.warn('Please select the rows you want to delete');
        return;
    }

    var idString = checkedCustomer.join(',');

    $.ligerDialog.confirm('Deletion cannot be restored. Confirm deletion?', function (type) {
        if (type) {
            $.ajax({
                type: "GET",
                url: "CheckBillGetList.aspx",
                data: "Action=delete&id=" + idString + "&ranid=" + Math.random(),
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
    if (!row) {
        $.ligerDialog.warn('Please select the row to operate on');
        return;
    }

    var idString = checkedCustomer.join(',');
    $.ajax({
        type: "GET",
        url: "CheckBillGetList.aspx",
        data: "Action=checkRow&idString=" + idString + "&ranid=" + Math.random(),
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
    if (!row) {
        $.ligerDialog.warn('Please select the row to operate on');
        return;
    }

    var idString = checkedCustomer.join(',');
    $.ajax({
        type: "GET",
        url: "CheckBillGetList.aspx",
        data: "Action=checkNoRow&idString=" + idString + "&ranid=" + Math.random(),
        success: function (resultString) {
            $.ligerDialog.alert(resultString, 'Notification');
            reload();
        },
        error: function () {
            $.ligerDialog.alert("Network error, please contact the administrator", 'Notification');
        }
    });
}

function add() {
    top.topManager.openPage({
        id: 'CheckBillGetListAdd',
        href: 'pay/CheckBillGetListAdd.aspx',
        title: 'Collection Write-off - Create'
    });
}

function editRow() {
    var row = manager.getSelectedRow();
    top.topManager.openPage({
        id: 'CheckBillGetListEdit',
        href: 'pay/CheckBillGetListEdit.aspx?id=' + row.id,
        title: 'Collection Write-Off - Edit'
    });
}

function reload() {
    manager.reload();
}

// Row check handling
function f_onCheckAllRow(checked) {
    for (var rowid in this.records) {
        if (checked) addCheckedCustomer(this.records[rowid]['id']);
        else removeCheckedCustomer(this.records[rowid]['id']);
    }
}

var checkedCustomer = [];

function findCheckedCustomer(id) {
    for (var i = 0; i < checkedCustomer.length; i++) {
        if (checkedCustomer[i] === id) return i;
    }
    return -1;
}

function addCheckedCustomer(id) {
    if (findCheckedCustomer(id) === -1) checkedCustomer.push(id);
}

function removeCheckedCustomer(id) {
    var i = findCheckedCustomer(id);
    if (i === -1) return;
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

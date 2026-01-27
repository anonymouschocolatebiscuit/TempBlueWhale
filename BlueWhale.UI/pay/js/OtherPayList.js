var manager;
$(function () {

    var form = $("#form").ligerForm();

    var menu = $.ligerMenu({
        width: 120, items:
            [
                { text: 'Add', click: add, icon: 'add' },
                { text: 'Edit', click: editRow },
                { line: true },
                { text: 'Search', click: viewRow }
            ]
    });

    manager = $("#maingrid").ligerGrid({
        checkbox: true,
        columns: [
            {
                display: 'Action', isSort: false, width: 60, align: 'center', render: function (rowdata, rowindex, value) {
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
                display: 'Payment Date', name: 'bizDate', width: 130, align: 'center', valign: 'center',
                totalSummary:
                {
                    type: 'count',
                    render: function (e) {
                        return 'Total：';
                    }
                }
            },
            { display: 'Document Number', name: 'number', width: 150, align: 'center' },
            { display: 'Settlement Account', name: 'bkName', width: 150, align: 'center' },
            { display: 'Supplier', name: 'wlName', width: 150, align: 'left' },
            {
                display: 'Payment Amount', name: 'sumPrice', width: 130, align: 'right',

                totalSummary:
                {
                    align: 'right',
                    type: 'sum',
                    render: function (e) {
                        return Math.round(e.sum * 100) / 100;
                    }
                }

            },
            { display: 'Status', name: 'flag', width: 80, align: 'center' },
            { display: 'Created By', name: 'makeName', width: 90, align: 'center' },
            { display: 'Reviewed By', name: 'checkName', width: 100, align: 'center' },
            { display: 'Remark', name: 'remarks', width: 200, align: 'left' }
        ], width: '98%',
        height: '98%',
        dataAction: 'local',
        usePager: false,
        url: "OtherPayList.aspx?Action=GetDataList",
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

        isChecked: f_isChecked, onCheckRow: f_onCheckRow, onCheckAllRow: f_onCheckAllRow
    }
    );
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
    if (keys == "Please enter the doc no/supplier/remarks") {
        keys = "";
    }

    var start = document.getElementById("txtDateStart").value;
    var end = document.getElementById("txtDateEnd").value;

    manager.changePage("first");
    manager._setUrl("OtherPayList.aspx?Action=GetDataListSearch&types=0&keys=" + keys + "&start=" + start + "&end=" + end);
}

function deleteRow() {
    var row = manager.getSelectedRow();
    if (!row) { $.ligerDialog.warn('Please select the row to delete'); return; }

    var idString = checkedCustomer.join(',');

    $.ligerDialog.confirm('Deletion cannot be restored, are you sure to delete?', function (type) {
        if (type) {
            $.ajax({
                type: "GET",
                url: "OtherPayList.aspx",
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
        url: "OtherPayList.aspx",
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
        url: "OtherPayList.aspx",
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

function add() {
    parent.f_addTab('OtherPayListAdd', 'Other Payments-Add', 'pay/OtherPayListAdd.aspx');
    top.topManager.openPage({
        id: 'OtherPayListAdd',
        href: 'pay/OtherPayListAdd.aspx',
        title: 'Other Payments-Add'
    });
}
function editRow() {
    var row = manager.getSelectedRow();

    parent.f_addTab('OtherPayListEdit', 'Other Payments-Edit', 'pay/OtherPayListEdit.aspx?id=' + row.id);

    top.topManager.openPage({
        id: 'OtherPayListEdit',
        href: 'pay/OtherPayListEdit.aspx?id=' + row.id,
        title: 'Other Payments-Edit'
    });
}

function viewRow() {
    var row = manager.getSelectedRow();

    parent.f_addTab('OtherPayListEdit', 'Other Payments-Details', 'pay/OtherPayListEdit.aspx?id=' + row.id);

    top.topManager.openPage({
        id: 'OtherPayListView',
        href: 'store/OtherPayListView.aspx?id=' + row.id,
        title: 'Other Payments-Details'
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

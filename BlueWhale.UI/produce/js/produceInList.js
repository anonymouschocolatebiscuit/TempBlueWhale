
var manager;
$(function () {
    var form = $("#form").ligerForm();

    var menu = $.ligerMenu({
        width: 120, items:
            [
                { text: 'Add', click: add, icon: 'add' },
                { text: 'Modify', click: editRow },
                { line: true }
            ]
    });

    manager = $("#maingrid").ligerGrid({
        checkbox: true,
        columns: [
            {
                display: 'Action', isSort: false, width: 70, align: 'center', render: function (rowdata, rowindex, value) {
                    var h = "";
                    if (!rowdata._editing) {
                        h += "<a href='javascript:editRow()' title='EditRow' style='float:left;'><div class='ui-icon ui-icon-pencil'></div></a> ";
                        h += "<a href='javascript:deleteRow()' title='DeleteRow' style='float:right;'><div class='ui-icon ui-icon-trash'></div></a> ";
                    }
                    else {
                        h += "<a href='javascript:endEdit(" + rowindex + ")'>Submit</a> ";
                        h += "<a href='javascript:cancelEdit(" + rowindex + ")'>Cancel</a> ";
                    }
                    return h;
                }
            },
            {
                display: 'Inbound Date', name: 'bizDate', width: 100, align: 'center', valign: 'center',

                totalSummary:
                {
                    type: 'count',
                    render: function (e) {
                        return 'Total: ';
                    }
                }
            },
            { display: 'Receipt Number', name: 'number', width: 160, align: 'center' },
            {
                display: 'Stock-in Quantity', name: 'sumNum', width: 130, align: 'right',

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
                display: 'Cost Amount', name: 'sumPriceAll', width: 130, align: 'right',

                totalSummary:
                {
                    align: 'right',
                    type: 'sum',
                    render: function (e) {
                        return Math.round(e.sum * 100) / 100;
                    }
                }
            },
            { display: 'Status', name: 'flag', width: 100, align: 'center' },
            { display: 'Created By', name: 'makeName', width: 100, align: 'center' },
            { display: 'Stock-in Operator', name: 'bizName', width: 150, align: 'center' },
            { display: 'Reviewed By', name: 'checkName', width: 100, align: 'center' },
            { display: 'Remarks', name: 'remarks', width: 200, align: 'left' }
        ], width: '98%',
        height: '98%',
        dataAction: 'local',
        usePager: false,
        url: "produceInList.aspx?Action=GetDataList",
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
    if (keys == "Please Enter Receipt No./Remarks") {

        keys = "";

    }

    var start = document.getElementById("txtDateStart").value;
    var end = document.getElementById("txtDateEnd").value;

    manager.changePage("first");
    manager._setUrl("produceInList.aspx?Action=GetDataListSearch&types=0&keys=" + keys + "&start=" + start + "&end=" + end);
}

function deleteRow() {

    var row = manager.getSelectedRow();
    if (!row) { $.ligerDialog.warn('Please select the rows you want to delete'); return; }

    var idString = checkedCustomer.join(',');

    $.ligerDialog.confirm('Deletion cannot be restored. Confirm deletion?', function (type) {
        if (type) {
            $.ajax({
                type: "GET",
                url: "produceInList.aspx",
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
        url: "produceInList.aspx",
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
        url: "produceInList.aspx",
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
    parent.f_addTab('produceInListAdd', 'Production Stock In - Create', 'produce/produceInListAdd.aspx?id=0');

    top.topManager.openPage({
        id: 'produceInListAdd',
        href: 'produce/produceInListAdd.aspx?id=0',
        title: 'Production Stock In - Create'
    });
}

function editRow() {
    var row = manager.getSelectedRow();

    parent.f_addTab('produceInListEdit', 'Production Stock In - Modify', 'produce/produceInListEdit.aspx?id=' + row.id);

    top.topManager.openPage({
        id: 'produceInListEdit',
        href: 'produce/produceInListEdit.aspx?id=' + row.id,
        title: 'Production Stock In - Modify'
    });
}

function viewRow() {
    var row = manager.getSelectedRow();

    top.topManager.openPage({
        id: 'produceInListEdit',
        href: 'produce/produceInListEdit.aspx?id=' + row.id,
        title: 'Production Stock In - Details'
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
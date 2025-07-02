function search() {
    var keys = document.getElementById("txtKeys").value;
    if (keys == "Please enter the receipt no./bundled product/remarks") {
        keys = "";
    }
    var start = document.getElementById("txtDateStart").value;
    var end = document.getElementById("txtDateEnd").value;

    manager.changePage("first");
    manager._setUrl("AssembleList.aspx?Action=GetDataListSearch&types=0&keys=" + keys + "&start=" + start + "&end=" + end);
}

function reload() {
    manager.reload();
    managersub.reload();
}

function editRow() {
    var row = manager.getSelectedRow();

    parent.f_addTab('AssembleListEdit', 'Assemble List-Edit', 'store/AssembleListEdit.aspx?id=' + row.id);

    top.topManager.openPage({
        id: 'AssembleListEdit',
        href: 'store/AssembleListEdit.aspx?id=' + row.id,
        title: 'Assemble List-Edit'
    });
}

function deleteRow() {

    var row = manager.getSelectedRow();
    if (!row) { $.ligerDialog.warn('Please select the row to delete'); return; }

    var idString = checkedCustomer.join(',');

    $.ligerDialog.confirm('Deletion cannot be undone, are you sure you want to delete?', function (type) {

        if (type) {

            $.ajax({
                type: "GET",
                url: "AssembleList.aspx",
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
    if (!row) {
        $.ligerDialog.warn('Please select the row to edit!'); return;
    }

    var idString = checkedCustomer.join(',');

    $.ajax({
        type: "GET",
        url: "AssembleList.aspx",
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
    if (!row) {
        $.ligerDialog.warn('Please select the row to edit!'); return;
    }

    var idString = checkedCustomer.join(',');

    $.ajax({
        type: "GET",
        url: "AssembleList.aspx",
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

    parent.f_addTab('AssembleListAdd', 'Product Assembly - Create', 'store/AssembleListAdd.aspx');

    top.topManager.openPage({
        id: 'AssembleListAdd',
        href: 'store/AssembleListAdd.aspx',
        title: 'Product Assembly - Create'
    });
}

var manager;
$(function () {
    var form = $("#form").ligerForm();

    window['g'] =
        manager = $("#maingrid").ligerGrid({
            checkbox: true,
            columns: [
                {
                    display: 'Action', isSort: false, width: 50, align: 'center', render: function (rowdata, rowindex, value) {
                        var h = "";
                        if (!rowdata._editing) {
                            h += "<a href='javascript:editRow()' title='Edit' style='float:left;'><div class='ui-icon ui-icon-pencil'></div></a> ";
                            h += "<a href='javascript:deleteRow()' title='Delete' style='float:right;'><div class='ui-icon ui-icon-trash'></div></a> ";
                        }
                        else {
                            h += "<a href='javascript:endEdit(" + rowindex + ")'>Submit</a> ";
                            h += "<a href='javascript:cancelEdit(" + rowindex + ")'>Cancel</a> ";
                        }
                        return h;
                    }
                },
                {
                    display: 'Assembly Date', name: 'bizDate', width: 150, align: 'center', valign: 'center'
                },
                {
                    display: 'Receipt Number', name: 'number', width: 150, align: 'center'
                },
                {
                    display: 'Assembled Item', name: 'goodsName', width: 200, align: 'left'
                },
                {
                    display: 'Specification', name: 'spec', width: 100, align: 'center'
                },
                {
                    display: 'Unit', name: 'unitName', width: 60, align: 'center'
                },
                {
                    display: 'Stock In Quantity', name: 'num', width: 150, type: 'float', align: 'right'
                },
                {
                    display: 'Stock In Unit Price', name: 'price', width: 150, type: 'float', align: 'right'
                },
                { display: 'Amount', name: 'sumPrice', width: 80, type: 'float', align: 'right' },
                {
                    display: 'Stock-In Warehouse', name: 'ckId', width: 150, isSort: false, textField: 'ckName',
                    editor: {
                        type: 'select',
                        url: "../baseSet/InventoryList.aspx?Action=GetDDLList&r=" + Math.random(),
                        valueField: 'ckId', textField: 'ckName'
                    }

                },
                {
                    display: 'Status', name: 'flag', width: 60, align: 'center'
                },
                {
                    display: 'Created By', name: 'makeName', width: 90, align: 'center'
                },
                {
                    display: 'Reviewed By', name: 'checkName', width: 90, align: 'center'
                },
                {
                    display: 'Remark', name: 'remarks', width: 100, align: 'left', type: 'text'
                }
            ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '250',
            rownumbers: true,

            onDblClickRow: function (data, rowindex, rowobj) {
                editRow();
            },

            frozenRownumbers: true,
            dataAction: 'local',
            usePager: true,
            allowUnSelectRow: true,
            onSelectRow: function (data, rowindex, rowobj) {
                getItemList(data.id);
            },
            totalSummary: false,
            isChecked: f_isChecked, onCheckRow: f_onCheckRow, onCheckAllRow: f_onCheckAllRow
        });
});

var managersub;
$(function () {
    window['gsub'] =
        managersub = $("#maingridsub").ligerGrid({
            columns: [
                {
                    display: 'Disassembled Item Name', name: 'goodsName', width: 200, align: 'left',
                    totalSummary:
                    {
                        type: 'count',
                        render: function (e) {
                            return 'Total：';
                        }
                    }
                },
                {
                    display: 'Specification', name: 'spec', width: 100, align: 'center'
                },
                {
                    display: 'Unit', name: 'unitName', width: 80, align: 'center'
                },
                {
                    display: 'Outbound Quantity', name: 'num', width: 150, type: 'float', align: 'right',
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
                    display: 'Outbound Unit Price', name: 'price', width: 150, type: 'float', align: 'right'
                },
                {
                    display: 'Price', name: 'sumPrice', width: 80, type: 'float', align: 'right',
                    totalSummary:
                    {
                        align: 'center',
                        type: 'sum',
                        render: function (e) {
                            return Math.round(e.sum * 100) / 100;
                        }
                    }
                },
                {
                    display: 'Outbound Warehouse', name: 'ckId', width: 150, isSort: false, textField: 'ckName',
                    editor: {
                        type: 'select',
                        url: "../baseSet/InventoryList.aspx?Action=GetDDLList&r=" + Math.random(),
                        valueField: 'ckId', textField: 'ckName'
                    }
                },

                { display: 'Remark', name: 'remarks', width: 150, align: 'left', type: 'text' }
            ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '210',
            rownumbers: true,
            frozenRownumbers: true,
            dataAction: 'local',
            usePager: false,
            alternatingRow: false,
            totalSummary: true
        }
        );
});

var rowNumber = 9;
function getItemList(pId) {
    if (pId != 0) {
        managersub._setUrl("AssembleList.aspx?Action=GetDataListSearchSub&pId=" + pId);
    }
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

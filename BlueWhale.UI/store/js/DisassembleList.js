function search() {

    var keys = document.getElementById("txtKeys").value;
    if (keys == "Please enter the receipt no./bundled product/remarks") {
        keys = "";
    }
    var start = document.getElementById("txtDateStart").value;
    var end = document.getElementById("txtDateEnd").value;
    manager.changePage("first");
    manager._setUrl("DisassembleList.aspx?Action=GetDataListSearch&types=0&keys=" + keys + "&start=" + start + "&end=" + end);
}

function reload() {
    manager.reload();
    managersub.reload();
}

function editRow() {
    var row = manager.getSelectedRow();
    parent.f_addTab('DisassembleListEdit', 'Product Disassembly Order - Edit', 'store/DisassembleListEdit.aspx?id=' + row.id);

    //          top.topManager.openPage({
    //            id : 'DisassembleListEdit',
    //            href : 'store/DisassembleListEdit.aspx?id='+row.id,
    //            title : 'Product Disassembly Order - Edit'
    //          });
}

function deleteRow() {

    var row = manager.getSelectedRow();
    if (!row) { $.ligerDialog.warn('Please select the row to delete'); return; }

    var idString = checkedCustomer.join(',');
    $.ligerDialog.confirm('Deletion cannot be undone. Confirm delete？', function (type) {
        if (type) {
            $.ajax({
                type: "GET",
                url: "DisassembleList.aspx",
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
    if (!row) { $.ligerDialog.warn('Please select the row to operate'); return; }

    var idString = checkedCustomer.join(',');
    $.ajax({
        type: "GET",
        url: "DisassembleList.aspx",
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
    if (!row) { $.ligerDialog.warn('Please select the row to operate'); return; }

    var idString = checkedCustomer.join(',');
    $.ajax({
        type: "GET",
        url: "DisassembleList.aspx",
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
    parent.f_addTab('DisassembleListAdd', 'Product Disassembly - Add', 'store/DisassembleListAdd.aspx');

    top.topManager.openPage({
        id: 'DisassembleListAdd',
        href: 'store/DisassembleListAdd.aspx',
        title: 'Product Disassembly - Add'
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
                    display: 'Operation', isSort: false, width: '5%', minWidth: 85, align: 'center', render: function (rowdata, rowindex, value) {
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

                { display: 'Disassembly Date', name: 'bizDate', width: '8%', minWidth: 135, align: 'center', valign: 'center' },
                { display: 'Bill Number', name: 'number', width: '6%', minWidth: 100, align: 'center' },
                { display: 'Bundled Product', name: 'goodsName', width: '10%', minWidth: 170, align: 'left' },
                { display: 'Specification', name: 'spec', width: '6%', minWidth: 100, align: 'center' },
                { display: 'Unit', name: 'unitName', width: '5%', minWidth: 85, align: 'center' },
                { display: 'Outbound Quantity', name: 'num', width: '9%', minWidth: 155, type: 'float', align: 'right' },
                {
                    display: 'Outbound Unit Price', name: 'price', width: '10%', minWidth: 160, type: 'float', align: 'right'

                },
                { display: 'Amount', name: 'sumPrice', width: '5%', type: 'float', align: 'right' },
                {
                    display: 'Outbound Warehouse', name: 'ckId', width: '10%', minWidth: 160, isSort: false, textField: 'ckName',
                    editor: {
                        type: 'select',
                        url: "../baseSet/InventoryList.aspx?Action=GetDDLList&r=" + Math.random(),
                        valueField: 'ckId', textField: 'ckName'
                    }
                },
                { display: 'Status', name: 'flag', width: '5%', minWidth: 85, align: 'center' },
                { display: 'Created By', name: 'makeName', width: '5%', minWidth: 85, align: 'center' },
                { display: 'Reviewed By', name: 'checkName', width: '5%', minWidth: 85, align: 'center' },
                { display: 'Remarks', name: 'remarks', width: '7%', minWidth: 120, align: 'left', type: 'text' }
            ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '250',
            // url: 'DisassembleList.aspx?Action=GetData',
            rownumbers: true,

            onDblClickRow: function (data, rowindex, rowobj) {
                // $.ligerDialog.alert('The choice is' + data.id);
                editRow();
            },
            frozenRownumbers: true,
            dataAction: 'local',
            usePager: true,
            allowUnSelectRow: true,
            onSelectRow: function (data, rowindex, rowobj) {
                //$.ligerDialog.alert('The choice is' + data.id);
                getItemList(data.id);
            },
            totalSummary: false,
            isChecked: f_isChecked, onCheckRow: f_onCheckRow, onCheckAllRow: f_onCheckAllRow
        }
        );
});

var managersub;
$(function () {
    window['gsub'] =
        managersub = $("#maingridsub").ligerGrid({
            columns: [
                {
                    display: 'Disassembled Product Name', name: 'goodsName', width: '14%', minWidth: 240, align: 'left',
                    totalSummary:
                    {
                        type: 'count',
                        render: function (e) { 
                            return 'Total：';
                        }
                    }
                },

                { display: 'Specification', name: 'spec', width: '8%', minWidth: 135, align: 'center' },
                { display: 'Unit', name: 'unitName', width: '6%', minWidth: 100, align: 'center' },
                {
                    display: 'Inbound Quantity', name: 'num', width: '8%', minWidth: 140, type: 'float', align: 'right',
                    totalSummary:
                    {
                        align: 'right',
                        type: 'sum',
                        render: function (e) { 
                            return Math.round(e.sum * 100) / 100;
                        }
                    }
                },
                { display: 'Inbound Unit Price', name: 'price', width: '9%', minWidth: 150, type: 'float', align: 'right' },
                {
                    display: 'Amount', name: 'sumPrice', width: '6%', minWidth: 100, type: 'float', align: 'right',
                    totalSummary:
                    {
                        align: 'center',
                        type: 'sum',
                        render: function (e) {
                            // alert("Summarized");
                            return Math.round(e.sum * 100) / 100;
                        }
                    }
                },
                {
                    display: 'Outbound Warehouse', name: 'ckId', width: '9%', minWidth: 150, isSort: false, textField: 'ckName',
                    editor: {
                        type: 'select',
                        url: "../baseSet/InventoryList.aspx?Action=GetDDLList&r=" + Math.random(),
                        valueField: 'ckId', textField: 'ckName'
                    }
                },
                { display: 'Remarks', name: 'remarks', width: '12%', minWidth: 200, align: 'left', type: 'text' }
            ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '210',
            // url: 'DisassembleListAdd.aspx?Action=GetDataSub',
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
        managersub._setUrl("DisassembleList.aspx?Action=GetDataListSearchSub&pId=" + pId);
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
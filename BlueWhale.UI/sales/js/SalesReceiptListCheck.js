var manager;
$(function () {
    var form = $("#form").ligerForm();

    var menu = $.ligerMenu({
        width: 120,
        items: [
            { text: 'Print Preview', click: makePDF },
            { line: true }
        ]
    });

    manager = $("#maingrid").ligerGrid({
        checkbox: true,
        columns: [
            {
                display: 'Operate',
                isSort: false,
                width: 60,
                align: 'center',
                render: function (rowdata, rowindex, value) {
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
                display: 'Outbound Date',
                name: 'bizDate',
                width: 80,
                align: 'center',
                valign: 'center',
                totalSummary: {
                    type: 'count',
                    render: function (e) {
                        return 'Total:';
                    }
                }
            },
            {
                display: 'Document Number',
                name: 'number',
                width: 150,
                align: 'center'
            },
            {
                display: 'Business Type',
                name: 'types',
                width: 70,
                align: 'center',
                render: function (row) {
                    var html = row.types == 1 ? "Purchase" : "<span style='color:green'>Return</span>";
                    return html;
                }
            },
            {
                display: 'Customer',
                name: 'wlName',
                width: 170,
                align: 'left'
            },
            {
                display: 'Sales Quantity',
                name: 'sumNum',
                width: 100,
                align: 'right',
                totalSummary: {
                    align: 'right',
                    type: 'sum',
                    render: function (e) {
                        return Math.round(e.sum * 100) / 100;
                    }
                }
            },
            {
                display: 'Status',
                name: 'flag',
                width: 60,
                align: 'center'
            },
            {
                display: 'Logistics Company',
                name: 'sendName',
                width: 80,
                align: 'center'
            },
            {
                display: 'Tracking Number',
                name: 'sendNumber',
                width: 100,
                align: 'center'
            },
            {
                display: 'Created By',
                name: 'makeName',
                width: 80,
                align: 'center'
            },
            {
                display: 'Salesperson',
                name: 'bizName',
                width: 80,
                align: 'center'
            },
            {
                display: 'Approved By',
                name: 'checkName',
                width: 90,
                align: 'center'
            },
            {
                display: 'Remarks',
                name: 'remarks',
                width: 100,
                align: 'left'
            }
        ],
        width: '99%',
        height: '99%',
        dataAction: 'local',
        usePager: false,
        url: "SalesReceiptList.aspx?Action=GetDataList",
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

function makePDF() {
    var row = manager.getSelectedRow();
    var id = row.id;
    var number = row.number;
    $.ajax({
        type: "GET",
        url: "SalesReceiptListCheck.aspx",
        data: "Action=makePDF&id=" + id + "&number=" + number + "&ranid=" + Math.random(),
        success: function (resultString) {
            if (resultString == "生成成功！") {
                parent.f_addTab('pdf', 'Sales Outbound - Print Preview', 'sales/pdf/' + number + '.pdf');
            }
            else {
                $.ligerDialog.alert(resultString, 'Prompt');
            }
        },
        error: function (msg) {
            $.ligerDialog.alert("Network exception, please contact administrator", 'Prompt');
        }
    });
}

function makeBill() {
    var row = manager.getSelectedRow();
    var number = row.number;

    if (row.pdfURL == "") {
        $.ligerDialog.alert("PDF not generated yet, please generate first!", 'Prompt');
        return;
    }

    parent.f_addTab('pdf', 'Sales Quotation - PDF', 'sales/pdf/' + number + '.pdf');
}

function f_set() {
    form.setData({
        keys: "",
        dateStart: new Date("<% =start%>"),
        dateEnd: new Date("<% =end%>")
    });
}

function search() {
    var keys = document.getElementById("txtKeys").value;
    if (keys == "请输入单据号/客户/备注") {
        keys = "";
    }
    var start = document.getElementById("txtDateStart").value;
    var end = document.getElementById("txtDateEnd").value;

    manager.changePage("first");
    manager._setUrl("SalesReceiptList.aspx?Action=GetDataListSearch&types=0&keys=" + keys + "&start=" + start + "&end=" + end);
}

function deleteRow() {
    var row = manager.getSelectedRow();
    if (!row) {
        $.ligerDialog.warn('Please select row to delete');
        return;
    }

    var idString = checkedCustomer.join(',');

    $.ligerDialog.confirm('Cannot recover after deletion, confirm delete?', function (type) {
        if (type) {
            $.ajax({
                type: "GET",
                url: "SalesReceiptList.aspx",
                data: "Action=delete&id=" + idString + "&ranid=" + Math.random(),
                success: function (resultString) {
                    $.ligerDialog.alert(resultString, 'Prompt');
                    reload();
                },
                error: function (msg) {
                    $.ligerDialog.alert("Network exception, please contact administrator", 'Prompt');
                }
            });
        }
    });
}

function checkRow() {
    var row = manager.getSelectedRow();
    if (!row) {
        $.ligerDialog.warn('Please select row to operate');
        return;
    }

    var idString = checkedCustomer.join(',');

    $.ajax({
        type: "GET",
        url: "SalesReceiptListCheck.aspx",
        data: "Action=checkRow&idString=" + idString + "&ranid=" + Math.random(),
        success: function (resultString) {
            $.ligerDialog.alert(resultString, 'Prompt');
            reload();
        },
        error: function (msg) {
            $.ligerDialog.alert("Network exception, please contact administrator", 'Prompt');
        }
    });
}

function checkNoRow() {
    var row = manager.getSelectedRow();
    if (!row) {
        $.ligerDialog.warn('Please select row to operate');
        return;
    }

    var idString = checkedCustomer.join(',');

    $.ajax({
        type: "GET",
        url: "SalesReceiptListCheck.aspx",
        data: "Action=checkNoRow&idString=" + idString + "&ranid=" + Math.random(),
        success: function (resultString) {
            $.ligerDialog.alert(resultString, 'Prompt');
            reload();
        },
        error: function (msg) {
            $.ligerDialog.alert("Network exception, please contact administrator", 'Prompt');
        }
    });
}

function add() {
    parent.f_addTab('SalesReceiptListAdd', 'Sales Outbound - Add', 'sales/SalesReceiptListAdd.aspx?id=0');
}

function editRow() {
    var row = manager.getSelectedRow();
    parent.f_addTab('SalesReceiptListView', 'Sales Outbound - Details', 'sales/SalesReceiptListView.aspx?id=' + row.id);
}

function viewRow() {
    var row = manager.getSelectedRow();
    top.topManager.openPage({
        id: 'SalesReceiptListView',
        href: 'sales/SalesReceiptListView.aspx?id=' + row.id,
        title: 'Sales Outbound - Details'
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

function viewRow() {
    var row = manager.getSelectedRow();

    if (!row) $.ligerDialog.alert("Please Select a row", 'Information');

    parent.f_addTab('SalesReceiptListView', 'Sales Outbound-Detail', 'sales/SalesReceiptListView.aspx?id=' + row.id);

    //top.topManager.openPage({
    //    id: 'SalesReceiptListView',
    //    href: 'sales/SalesReceiptListView.aspx?id=' + row.id,
    //    title: 'Sales Outbound-Detail'
    //});
}

function deleteRow() {
    var row = manager.getSelectedRow();

    if (!row) {
        $.ligerDialog.warn('Please select a row to delete');
        return;
    }

    var idString = checkedCustomer.join(','); // Get selected ID string separated by commas for backend

    $.ligerDialog.confirm('Deleted data cannot be recovered. Confirm deletion?', function (type) {
        if (type) {
            $.ajax({
                type: "GET",
                url: "SalesReceiptList.aspx",
                data: "Action=delete&id=" + idString + " &ranid=" + Math.random(),
                success: function (resultString) {
                    $.ligerDialog.alert(resultString, 'Notification');
                    reload();
                },
                error: function (msg) {
                    $.ligerDialog.alert("Network exception, please contact administrator", 'Notification');
                }
            });
        }
    });
}

function editRow() {
    var row = manager.getSelectedRow();

    parent.f_addTab('SalesReceiptListView', 'Sales Outbound-Detail', 'sales/SalesReceiptListView.aspx?id=' + row.id);
}
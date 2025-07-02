﻿
var manager;
$(function () {
    var form = $("#form").ligerForm();

    var menu = $.ligerMenu({
        width: 120, items:
            [
                { text: 'View', click: viewRow }
            ]
    });

    manager = $("#maingrid").ligerGrid({
        checkbox: true,
        columns: [
            {
                display: 'Operate', isSort: false, width: 50, align: 'center', render: function (rowdata, rowindex, value) {
                    var h = "";
                    if (!rowdata._editing) {
                        h += "<a href='javascript:editRow()' title='EditRow' style='float:left;'><div class='ui-icon ui-icon-pencil'></div></a> ";
                    }
                    else {
                        h += "<a href='javascript:endEdit(" + rowindex + ")'>Submit</a> ";
                        h += "<a href='javascript:cancelEdit(" + rowindex + ")'>Cancel</a> ";
                    }
                    return h;
                }
            },
            {
                display: 'Stock In Date', name: 'bizDate', width: 80, align: 'center', valign: 'center',

                totalSummary:
                {
                    type: 'count',
                    render: function (e) {  
                        //e Object(Include sum,max,min,avg,count) 
                        return 'Total：';
                    }
                }
            },
            { display: 'Receipt Number', name: 'number', width: 150, align: 'center' },
            {
                display: 'Business Type', name: 'types', width: 70, align: 'center',

                render: function (row) {
                    var html = row.types == 1 ? "Purchase" : "<span style='color:green'>Return of Goods</span>";
                    return html;
                }
            },
            { display: 'Vendor', name: 'wlName', width: 170, align: 'left' },
            {
                display: 'Purchase Quantity', name: 'sumNum', width: 100, align: 'right',

                totalSummary:
                {
                    align: 'right', 
                    type: 'sum',
                    render: function (e) {
                        //e Object(Include sum,max,min,avg,count) 
                        return Math.round(e.sum * 100) / 100;
                    }
                }
            },
            { display: 'Status', name: 'flag', width: 60, align: 'center' },
            { display: 'Created By', name: 'makeName', width: 70, align: 'center' },
            { display: 'Purchaser', name: 'bizName', width: 70, align: 'center' },
            { display: 'Reviewed By', name: 'checkName', width: 70, align: 'center' },
            { display: 'Remarks', name: 'remarks', width: 100, align: 'left' }
        ], width: '98%',
        //pageSizeOptions: [5, 10, 15, 20],
        height: '98%',
        // pageSize: 15,
        dataAction: 'local', 
        usePager: false,
        url: "PurReceiptListCheck.aspx?Action=GetDataList",
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
    if (keys == "Please Enter Receipt No./Customer/Product/Remarks") {
        keys = "";
    }
    var start = document.getElementById("txtDateStart").value;
    var end = document.getElementById("txtDateEnd").value;

    manager.changePage("first");
    manager._setUrl("PurReceiptListCheck.aspx?Action=GetDataListSearch&types=0&keys=" + keys + "&start=" + start + "&end=" + end);
}





function checkRow() {

    var row = manager.getSelectedRow();
    if (!row) { $.ligerDialog.warn('Please select the row to operate on'); return; }

    var idString = checkedCustomer.join(',');

    $.ajax({
        type: "GET",
        url: "PurReceiptListCheck.aspx",
        data: "Action=checkRow&idString=" + idString + "&ranid=" + Math.random(), //encodeURI
        success: function (resultString) {
            $.ligerDialog.alert(resultString, 'Notification');
            reload();

        },
        error: function (msg) {

            $.ligerDialog.alert("Internet error, please contact the administrator", 'Notification');
        }
    });
}


function checkNoRow() {

    var row = manager.getSelectedRow();
    if (!row) { $.ligerDialog.warn('Please select the row to operate on'); return; }

    var idString = checkedCustomer.join(',');

    $.ajax({
        type: "GET",
        url: "PurReceiptListCheck.aspx",
        data: "Action=checkNoRow&idString=" + idString + "&ranid=" + Math.random(), //encodeURI
        success: function (resultString) {
            $.ligerDialog.alert(resultString, 'Notification');
            reload();
        },
        error: function (msg) {
            $.ligerDialog.alert("Internet error, please contact the administrator", 'Notification');
        }
    });
}

function editRow() {
    var row = manager.getSelectedRow();

    parent.f_addTab('PurReceiptListView', 'Purchase Receipt-Details', 'buy/PurReceiptListView.aspx?id=' + row.id);
}

function viewRow() {
    var row = manager.getSelectedRow();
    parent.f_addTab('PurReceiptListView', 'Purchase Receipt-Details', 'buy/PurReceiptListView.aspx?id=' + row.id);
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
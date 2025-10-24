var manager;

$(function () {

    var form = $("#form").ligerForm();

    var menu = $.ligerMenu({
        width: 120, items:
            [
                { text: 'print preview', click: makePDF },
                { line: true }
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
                display: 'Delivery Date', name: 'bizDate', width: 120, align: 'center', valign: 'center',
                totalSummary:
                {
                    type: 'count',
                    render: function (e) {  //Total renderer, returns HTML to be loaded into the cell
                        // e Total Object (including sum, max, min, avg, count)"
                        return 'Total：';
                    }
                }
            },
            { display: 'Receipt No.', name: 'number', width: 150, align: 'center' },
            {
                display: 'Business Category', name: 'types', width: 160, align: 'center',
                render: function (row) {
                    var html = row.types == 1 ? "Purchase" : "<span style='color:green'>Refund</span>";
                    return html;
                }
            },
            { display: 'Customer', name: 'wlName', width: 120, align: 'left' },
            {
                display: 'Sales Quantity', name: 'sumNum', width: 120, align: 'right',
                totalSummary:
                {
                    align: 'right',   //total cell content alignment:left/center/right 
                    type: 'sum',
                    render: function (e) {  //Total renderer, return HTML to load into the cell
                        //e Total Object(sum,max,min,avg,count) 
                        return Math.round(e.sum * 100) / 100;
                    }
                }
            },
            { display: 'Status', name: 'flag', width: 90, align: 'center' },
            { display: 'Logistics Company', name: 'sendName', width: 150, align: 'center' },
            { display: 'Logistics Tracking Number', name: 'sendNumber', width: 250, align: 'center' },
            { display: 'Creator', name: 'makeName', width: 120, align: 'center' },
            { display: 'Sales Person', name: 'bizName', width: 120, align: 'center' },
            { display: 'Reviewer', name: 'checkName', width: 70, align: 'center' },
            { display: 'Remarks', name: 'remarks', width: 150, align: 'left' }
        ], width: '98%',
        //pageSizeOptions: [5, 10, 15, 20],
        height: '98%',
        // pageSize: 15,
        dataAction: 'local', //Local sorting
        usePager: false,
        url: "SalesReceiptList.aspx?Action=GetDataList",
        alternatingRow: false,
        onDblClickRow: function (data, rowindex, rowobj) {
            // $.ligerDialog.alert('Selected is' + data.id);
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
    if (keys == "Please Enter Receipt No./Customer/Product/Remarks") {
        keys = "";
    }
    var start = document.getElementById("txtDateStart").value;
    var end = document.getElementById("txtDateEnd").value;

    manager.changePage("first");
    manager._setUrl("SalesReceiptListCheck.aspx?Action=GetDataListSearch&types=0&keys=" + keys + "&start=" + start + "&end=" + end);
}


function makePDF() {
    var row = manager.getSelectedRow();
    var id = row.id;
    var number = row.number;

    $.ajax({
        type: "GET",
        url: "SalesReceiptListCheck.aspx",
        data: "Action=makePDF&id=" + id + "&number=" + number + "&ranid=" + Math.random(), //encodeURI
        success: function (resultString) {

            if (resultString == "Generate Successful！") {
                parent.f_addTab('pdf', 'Sales Outbound-Print Preview', 'sales/pdf/' + number + '.pdf');
            }
            else {
                $.ligerDialog.alert(resultString, 'Information');
            }
        },
        error: function (msg) {

            $.ligerDialog.alert("Network Error，Please Contact an Administrator", 'Information');
        }
    });
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

function editRow() {
    var row = manager.getSelectedRow();

    parent.f_addTab('SalesReceiptListView', '销售出库-详情', 'sales/SalesReceiptListView.aspx?id=' + row.id);
}

var manager;
$(function () {

    var form = $("#form").ligerForm();


    var menu = $.ligerMenu({
        width: 120, items:
            [
                { text: 'Add', click: add, icon: 'add' },
                { text: 'Edit', click: editRow },
                { line: true },
                { text: 'makePDF', click: makePDF },
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
                        h += "<a href='javascript:editRow()' title='Edit Line' style='float:left;'><div class='ui-icon ui-icon-pencil'></div></a> ";
                        h += "<a href='javascript:deleteRow()' title='Delete Line' style='float:right;'><div class='ui-icon ui-icon-trash'></div></a> ";
                    }
                    else {
                        h += "<a href='javascript:endEdit(" + rowindex + ")'>Submit</a> ";
                        h += "<a href='javascript:cancelEdit(" + rowindex + ")'>Cancel</a> ";
                    }
                    return h;
                }
            },

            {
                display: 'Outbound Date', name: 'bizDate', width: 120, align: 'center', valign: 'center',

                totalSummary:
                {
                    type: 'count',
                    render: function (e) { 
                        return 'Total:';
                    }
                }


            },
            { display: 'Receipt Number', name: 'number', width: 150, align: 'center' },
            {
                display: 'Business Type', name: 'types', width: 125, align: 'center',

                render: function (row) {
                    var html = row.types == 1 ? "Purchase" : "<span style='color:green'>Return of Goods</span>";
                    return html;
                }


            },
            { display: 'Customer', name: 'wlName', width: 170, align: 'left' },
            {
                display: 'Quantity', name: 'sumNum', width: 150, align: 'right',

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
                display: 'Sales Amount', name: 'sumPriceAll', width: 100, align: 'right',

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
                display: 'Collected Payment Amount', name: 'priceCheckNowSum', width: 225, align: 'right',

                totalSummary:
                {
                    align: 'right', 
                    type: 'sum',
                    render: function (e) {
                        return Math.round(e.sum * 100) / 100;
                    }
                }

            },

            { display: 'Discount%', name: 'dis', width: 80, align: 'center' },
            {
                display: 'Discount amount', name: 'disPrice', width: 125, align: 'right',

                totalSummary:
                {
                    align: 'right',
                    type: 'sum',
                    render: function (e) {
                        return Math.round(e.sum * 100) / 100;
                    }
                }

            },


            { display: 'Status', name: 'flag', width: 60, align: 'center' },

            { display: 'Logistics Company', name: 'sendName', width: 140, align: 'center' },
            { display: 'Tracking Number', name: 'sendNumber', width: 140, align: 'center' },

            { display: 'Created By', name: 'makeName', width: 100, align: 'center' },
            { display: 'Salesperson', name: 'bizName', width: 90, align: 'center' },
            { display: 'Reviewed By', name: 'checkName', width: 100, align: 'center' },
            { display: 'Remarks', name: 'remarks', width: 100, align: 'left' }


        ], width: '98%',
        //pageSizeOptions: [5, 10, 15, 20],
        height: '98%',
        // pageSize: 15,
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

        isChecked: f_isChecked, onCheckRow: f_onCheckRow, onCheckAllRow: f_onCheckAllRow



    }
    );

});


function makePDF() {

    var shopId = $("#hfShopId").val();

    var row = manager.getSelectedRow();

    var id = row.id;
    var number = row.number;
    $.ajax({
        type: "GET",
        url: "SalesReceiptList.aspx",
        data: "Action=makePDF&id=" + id + "&number=" + number + "&ranid=" + Math.random(), //encodeURI
        success: function (resultString) {


            if (resultString == "Generate Successfully!") {

                parent.f_addTab('pdf', 'Sales Outbound-Print Review', 'sales/pdf/' + shopId + '-' + number + '.pdf');

            }
            else {
                $.ligerDialog.alert(resultString, 'Notification');
                // reload();
            }



        },
        error: function (msg) {

            $.ligerDialog.alert("Network Error, please contact the administrator", 'Notification');
        }
    });

}


function makeBill() {
    var row = manager.getSelectedRow();

    var number = row.number;

    if (row.pdfURL == "") {
        $.ligerDialog.alert("PDF has not been generated yet. Please click to generate it first!", 'Notification');
        return;

    }

    parent.f_addTab('pdf', 'Sales Quotation-PDF', 'sales/pdf/' + number + '.pdf');

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
    if (keys == "Please Enter Receipt No./Customer/Remarks") {

        keys = "";

    }
    var start = document.getElementById("txtDateStart").value;
    var end = document.getElementById("txtDateEnd").value;

    manager.changePage("first");
    manager._setUrl("SalesReceiptList.aspx?Action=GetDataListSearch&types=0&keys=" + keys + "&start=" + start + "&end=" + end);
}



function deleteRow() {

    var row = manager.getSelectedRow();
    if (!row) { $.ligerDialog.warn('Please select the rows you want to delete'); return; }

    var idString = checkedCustomer.join(',');

    $.ligerDialog.confirm('Deletion cannot be restored. Confirm deletion?', function (type) {


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

                    $.ligerDialog.alert("Network Error, please contact the administrator", 'Notification');
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
        url: "SalesReceiptList.aspx",
        data: "Action=checkRow&idString=" + idString + "&ranid=" + Math.random(), //encodeURI
        success: function (resultString) {
            $.ligerDialog.alert(resultString, 'Notification');
            reload();

        },
        error: function (msg) {

            $.ligerDialog.alert("Network Error, please contact the administrator", 'Notification');
        }
    });




}


function checkNoRow() {

    var row = manager.getSelectedRow();
    if (!row) { $.ligerDialog.warn('Please select the row to operate on'); return; }

    var idString = checkedCustomer.join(','); 

    $.ajax({
        type: "GET",
        url: "SalesReceiptList.aspx",
        data: "Action=checkNoRow&idString=" + idString + "&ranid=" + Math.random(), //encodeURI
        success: function (resultString) {
            $.ligerDialog.alert(resultString, 'Notification');
            reload();

        },
        error: function (msg) {

            $.ligerDialog.alert("Network Error, please contact the administrator", 'Notification');
        }
    });


}


function add() {

    parent.f_addTab('SalesReceiptListAdd', 'Sales Outbound-Add', 'sales/SalesReceiptListAdd.aspx?id=0');

    top.topManager.openPage({
        id: 'SalesReceiptListAdd',
        href: 'sales/SalesReceiptListAdd.aspx?id=0',
        title: 'Sales Outbound-Add'
    });


}

function editRow() {
    var row = manager.getSelectedRow();

    parent.f_addTab('SalesReceiptListEdit', 'Sales Outbound-Edit', 'sales/SalesReceiptListEdit.aspx?id=' + row.id);

    top.topManager.openPage({
        id: 'SalesReceiptListEdit',
        href: 'sales/SalesReceiptListEdit.aspx?id=' + row.id,
        title: 'Sales Outbound-Edit'
    });


}

function viewRow() {
    var row = manager.getSelectedRow();

    top.topManager.openPage({
        id: 'SalesReceiptListView',
        href: 'sales/SalesReceiptListView.aspx?id=' + row.id,
        title: 'Sales Outbount-View'
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
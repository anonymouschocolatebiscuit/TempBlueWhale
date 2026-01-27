var manager;

$(function () {

    var form = $('#form').ligerForm();

    var menu = $.ligerMenu({
        width: 100, items:
            [
                { text: 'Add', click: add },
                { line: true },
                { text: 'Edit', click: editRow },
                { line: true },
                { text: 'View', click: viewRow },
                { line: true },
                { text: 'Inbound', click: makeBill },
                { line: true },
                { text: 'Print (IUP)', click: makePDF },
                { line: true },
                { text: 'Print (NUP)', click: makePDFNoPrice },
            ]
    });

    manager = $('#maingrid').ligerGrid({
        checkbox: true,
        columns: [
            {
                display: 'Operate', isSort: false, width: 90, align: 'center', render: function (rowdata, rowindex, value) {
                    var h = '';
                    if (!rowdata._editing) {
                        h += '<div style="text-align: center;">';
                        if (rowdata.flag === 'Save') {
                            h += '<a href="javascript:editRow()" title="EditRow" style="margin-right: 10px; display: inline-block;"><div class="ui-icon ui-icon-pencil" style="margin: auto;"></div></a>';
                        } else {
                            h += '<a href="javascript:viewRow()" title="ViewRow" style="margin-right: 10px; display: inline-block;"><div class="ui-icon ui-icon-search" style="margin: auto;"></div></a>';
                        }
                        h += '<a href="javascript:deleteRow()" title="DeleteRow" style="display: inline-block;"><div class="ui-icon ui-icon-trash" style="margin: auto;"></div></a>';
                        h += '</div>';
                    } else {
                        h += '<div style="text-align: center;">';
                        h += '<a href="javascript:endEdit(' + rowindex + ')" style="margin-right: 10px;">Submit</a>';
                        h += '<a href="javascript:cancelEdit(' + rowindex + ')">Cancel</a>';
                        h += '</div>';
                    }

                    return h;
                }
            },

            {
                display: 'Order Date', name: 'bizDate', width: 100, align: 'center', valign: 'center',

                totalSummary:
                {
                    type: 'count',
                    render: function (e) {  // Summary renderer, returns HTML to be loaded into the cell
                        // e Aggregation of Objects (including sum, max, min, avg, count)
                        return 'Total:';
                    }
                }
            },
            { display: 'Order Number', name: 'number', width: 150, align: 'center' },

            { display: 'Vender', name: 'wlName', width: 170, align: 'left' },
            {
                display: 'Total Price', name: 'sumPriceAll', width: 100, align: 'right',

                totalSummary:
                {
                    align: 'right',   // Summary cell content alignment: left/center/right
                    type: 'sum',
                    render: function (e) {  // Summary renderer, returns HTML to be loaded into the cell
                        // e Aggregation of Objects (including sum, max, min, avg, count)
                        return Math.round(e.sum * 100) / 100;
                    }
                }
            },
            {
                display: 'Quantity', name: 'sumNum', width: 100, align: 'center',

                totalSummary:
                {
                    align: 'right',   // Summary cell content alignment: left/center/right
                    type: 'sum',
                    render: function (e) {  // Summary renderer, returns HTML to be loaded into the cell
                        // e Aggregates Object (including sum, max, min, avg, count)
                        return Math.round(e.sum * 100) / 100;
                    }
                }
            },
            { display: 'Order Status', name: 'flag', width: 100, align: 'center' },
            { display: 'Send Date', name: 'sendDate', width: 100, align: 'center' },
            { display: 'Created By', name: 'makeName', width: 100, align: 'center' },
            { display: 'Purchaser', name: 'bizName', width: 100, align: 'center' },
            { display: 'Review By', name: 'checkName', width: 100, align: 'center' },
            { display: 'Remark', name: 'remarks', width: 120, align: 'left' }
        ], width: '98%',
        //pageSizeOptions: [5, 10, 15, 20],
        height: '98%',
        // pageSize: 15,
        dataAction: 'local', // Local Sorting
        usePager: false,
        url: 'PurOrderList.aspx?Action=GetDataList',
        alternatingRow: false,
        onDblClickRow: function (data, rowindex, rowobj) {
            // $.ligerDialog.alert('The Selection is' + data.id);
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
        type: 'GET',
        url: 'PurOrderList.aspx',
        data: 'Action=makePDF&id=' + id + '&number=' + number + '&ranid=' + Math.random(), //encodeURI
        success: function (resultString) {
            if (resultString == 'Generate Successfully !') {
                parent.f_addTab('pdf', 'Purchase Order - Print Preview', 'buy/pdf/' + shopId + '-' + number + '.pdf');
            }
            else {
                $.ligerDialog.alert(resultString, 'Notification');
            }
        },
        error: function (msg) {
            $.ligerDialog.alert('Network error, please contact the administrator', 'Notification');
        }
    });
}

function makePDFNoPrice() {

    var shopId = $("#hfShopId").val();

    var row = manager.getSelectedRow();

    var id = row.id;

    var number = row.number;

    $.ajax({
        type: 'GET',
        url: 'PurOrderList.aspx',
        data: 'Action=makePDFNoPrice&id=' + id + '&number=' + number + '&ranid=' + Math.random(), //encodeURI
        success: function (resultString) {
            if (resultString == 'Generate Successfully !') {
                parent.f_addTab('pdf', 'Purchase Order - Print Preview', 'buy/pdf/' + shopId + '-' + number + '.pdf');
            }
            else {
                $.ligerDialog.alert(resultString, 'Notification');
            }
        },
        error: function (msg) {

            $.ligerDialog.alert('Network error, please contact the administrator', 'Notification');
        }
    });
}

function f_set() {

    form.setData({

        keys: '',
        dateStart: new Date('<% =start%>'),
        dateEnd: new Date('<% =end%>')
    });
}

function search() {

    var keys = document.getElementById('txtKeys').value;

    if (keys == 'Please Enter Order No./Vender/Remark') {
        keys = '';
    }

    var start = document.getElementById('txtDateStart').value;

    var end = document.getElementById('txtDateEnd').value;

    if (!start) {
        $.ligerDialog.warn('Please select a start date'); return;
    } else if (!end) {
        $.ligerDialog.warn('Please select a end date'); return;
    } 

    if (new Date(start) > new Date()) {
        $.ligerDialog.warn('Start date cannot exceed current date'); return;
    } else if (new Date(end) > new Date()) {
        $.ligerDialog.warn('End date cannot exceed current date'); return;
    }

    manager.changePage('first');

    manager._setUrl('PurOrderList.aspx?Action=GetDataListSearch&types=0&keys=' + keys + '&start=' + start + '&end=' + end);
}

function deleteRow() {

    var row = manager.getSelectedRow();

    if (!row) { $.ligerDialog.warn('Please select the rows you want to delete'); return; }

    var idString = checkedCustomer.join(','); // Get the selected ID string, separated ',', and pass it to the backend.

    $.ligerDialog.confirm('Deletion cannot be restored. Confirm deletion ?', function (type) {
        if (type) {
            $.ajax({
                type: 'GET',
                url: 'PurOrderList.aspx',
                data: 'Action=delete&id=' + idString + ' &ranid=' + Math.random(),
                success: function (resultString) {
                    $.ligerDialog.alert(resultString, 'Notification');
                    reload();
                },
                error: function (msg) {
                    $.ligerDialog.alert('Network error, please contact the administrator', 'Notification');
                }
            });
        }
    });
}

function checkRow() {

    var row = manager.getSelectedRow();

    if (!row) { $.ligerDialog.warn('Please select the row to operate on'); return; }

    var idString = checkedCustomer.join(','); // Get the selected ID string, separated ',', and pass it to the backend.

    $.ajax({
        type: 'GET',
        url: 'PurOrderList.aspx',
        data: 'Action=checkRow&idString=' + idString + '&ranid=' + Math.random(), //encodeURI
        success: function (resultString) {
            $.ligerDialog.alert(resultString, 'Notification');
            reload();
        },
        error: function (msg) {
            $.ligerDialog.alert('Network error, please contact the administrator', 'Notification');
        }
    });
}

function checkNoRow() {

    var row = manager.getSelectedRow();

    if (!row) { $.ligerDialog.warn('Please select the row to operate on'); return; }

    var idString = checkedCustomer.join(','); // Get the selected ID string, separated ',', and pass it to the backend.

    $.ajax({
        type: 'GET',
        url: 'PurOrderList.aspx',
        data: 'Action=checkNoRow&idString=' + idString + '&ranid=' + Math.random(), //encodeURI
        success: function (resultString) {
            $.ligerDialog.alert(resultString, 'Notification');
            reload();
        },
        error: function (msg) {
            $.ligerDialog.alert('Network error, please contact the administrator', 'Notification');
        }
    });
}

function add() {
    var row = manager.getSelectedRow();

    if (!row) { $.ligerDialog.warn('Please select the row to operate on'); return; }

    parent.f_addTab('PurOrderListAdd', 'Purchase Order - Add', 'buy/PurOrderListAdd.aspx?id=' + row.id);
}

function makeBill() {

    var row = manager.getSelectedRow();

    parent.f_addTab('PurReceiptListAdd', 'Purchase Receipt - Add', 'buy/PurReceiptListAdd.aspx?id=' + row.id);
}

function editRow() {
    var row = manager.getSelectedRow();

    if (!row) { alert('Please select a row'); return; }

    parent.f_addTab('purOrderListEdit', 'Purchase Order - Edit', 'buy/purOrderListEdit.aspx?id=' + row.id);
}

function viewRow() {
    var row = manager.getSelectedRow();

    if (!row) { alert('Please select a row'); return; }

    parent.f_addTab('purOrderListEdit', 'Purchase Order - Details', 'buy/purOrderListEdit.aspx?id=' + row.id);
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

/*
This example implements multi-select with pagination in a form.
It uses `onCheckRow` to remember the selected rows and `isChecked` to initialize the selection of the remembered rows.
*/
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
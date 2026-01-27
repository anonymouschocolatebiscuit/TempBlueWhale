
var manager;
$(function () {

    var form = $("#form").ligerForm();


    var menu = $.ligerMenu({
        width: 120, items:
            [
                { text: 'Add', click: add, icon: 'add' },
                { text: 'Edit', click: editRow },
                { line: true },
                { text: 'View', click: editRow },

                { line: true },
                { text: 'Production Dispense', click: getRow },

                { line: true },
                { text: 'Production Stock In', click: inRow }


            ]
    });



    manager = $("#maingrid").ligerGrid({
        checkbox: true,
        columns: [


            {
                display: 'Operate', isSort: false, width: 70, align: 'center', render: function (rowdata, rowindex, value) {
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
                display: 'Order Date', name: 'makeDate', width: 80, align: 'center', valign: 'center',

                totalSummary:
                {
                    type: 'count',
                    render: function (e) {  //Summary renderer, returns html loaded into cell
                        //e Sum Object(include sum,max,min,avg,count) 
                        return 'Total：';
                    }
                }

            },
            { display: 'Start Date', name: 'dateStart', width: 80, align: 'center', valign: 'center' },
            {
                display: 'End Date', name: 'dateEnd', width: 80, align: 'center', valign: 'center',


            },
            { display: 'Receipt Number', name: 'number', width: 130, align: 'center' },
            { display: 'Plan Category', name: 'typeName', width: 130, align: 'center' },
            { display: 'Order Number', name: 'orderNumber', width: 130, align: 'center' },

            { display: 'Client', name: 'wlName', width: 170, align: 'left' },

            { display: 'Item Number', name: 'code', width: 100, align: 'center' },
            { display: 'Item Name', name: 'goodsName', width: 120, align: 'center' },
            { display: 'Specification', name: 'spec', width: 100, align: 'center' },
            { display: 'Unit', name: 'unitName', width: 70, align: 'center' },

            {
                display: 'Quantity', name: 'num', width: 80, align: 'center',

                totalSummary:
                {
                    align: 'right',   //Summary cell content alignment:left/center/right 
                    type: 'sum',
                    render: function (e) {  //Summary renderer, returns html loaded into cell
                        //e Sum Object(include sum,max,min,avg,count) 
                        return Math.round(e.sum * 100) / 100;
                    }
                }

            },
            { display: 'Status', name: 'flag', width: 80, align: 'center' },

            { display: 'Created By', name: 'makeName', width: 80, align: 'center' },
            { display: 'Review By', name: 'checkName', width: 80, align: 'center' },
            { display: 'Remarks', name: 'remarks', width: 100, align: 'left' }


        ], width: '1120',
        //pageSizeOptions: [5, 10, 15, 20],
        height: '98%',
        // pageSize: 15,
        dataAction: 'local', //Local arrange
        usePager: false,
        url: "produceList.aspx?Action=GetDataList",
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

function getRow() {

    var row = manager.getSelectedRow();
    if (!row) { $.ligerDialog.warn('Please select the row to operate on'); return; }

    var idString = checkedCustomer.join(',');//Get the selected ID string, separate it with ‘,’, and pass it to the backend

    //  alert(idString);

    parent.f_addTab('produceGetListAdd', 'Production Material Collection-Add', 'produce/produceGetListAdd.aspx?id=' + idString);

    //  return;


}


function inRow() {

    var row = manager.getSelectedRow();
    if (!row) { $.ligerDialog.warn('Please select the row to operate on'); return; }

    var idString = checkedCustomer.join(',');//Get the selected ID string, separate it with ‘,’, and pass it to the backend

    //  alert(idString);

    parent.f_addTab('produceInListAdd', 'Production Stock In-Add', 'produce/produceInListAdd.aspx?id=' + idString);

    //  return;


}

function search() {

    var keys = document.getElementById("txtKeys").value;
    if (keys == "Please Enter Receipt No./Customer/Product/Remarks") {

        keys = "";

    }
    var start = document.getElementById("txtDateStart").value;
    var end = document.getElementById("txtDateEnd").value;


    manager.changePage("first");
    manager._setUrl("produceList.aspx?Action=GetDataListSearch&types=0&keys=" + keys + "&start=" + start + "&end=" + end);
}



function deleteRow() {

    var row = manager.getSelectedRow();
    if (!row) { $.ligerDialog.warn('Please select the rows you want to delete'); return; }

    var idString = checkedCustomer.join(',');//Get the selected ID string, separate it with ‘,’, and pass it to the backend

    $.ligerDialog.confirm('Deletion cannot be restored. Confirm deletion?', function (type) {


        if (type) {

            $.ajax({
                type: "GET",
                url: "produceList.aspx",
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

    var idString = checkedCustomer.join(',');//Get the selected ID string, separate it with ‘,’, and pass it to the backend

    $.ajax({
        type: "GET",
        url: "produceList.aspx",
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

    var idString = checkedCustomer.join(','); //Get the selected ID string, separate it with ‘,’, and pass it to the backend

    $.ajax({
        type: "GET",
        url: "produceList.aspx",
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
    parent.f_addTab('produceListAdd', 'Production Plan-Add', 'produce/produceListAdd.aspx');
}




function editRow() {
    var row = manager.getSelectedRow();
    parent.f_addTab('produceListEdit', 'Production Plan-Edit', 'produce/produceListEdit.aspx?id=' + row.id);
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
This example implements form pagination and multiple selections
That is, use onCheckRow to memorize the selected row, and use isChecked to initialize the memorized row.
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
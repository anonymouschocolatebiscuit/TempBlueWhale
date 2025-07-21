var manager;
$(function () {

    var form = $("#form").ligerForm();

    var menu = $.ligerMenu({
        width: 120, items:
            [
                { text: 'Add', click: add, icon: 'add' },
                { text: 'Edit', click: editRow },
                { line: true },
                { text: 'View', click: editRow }

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
            { display: 'Material Receiver', name: 'bizName', width: 60, align: 'center' },
            {
                display: 'Material Pickup Date', name: 'bizDate', width: 80, align: 'center', valign: 'center',

                totalSummary:
                {
                    type: 'count',
                    render: function (e) {  
                        //e Object(Include sum,max,min,avg,count) 
                        return 'Total:';
                    }
                }
            },
            { display: 'Receipt Number', name: 'number', width: 150, align: 'center' },
            { display: 'Production Plan No', name: 'planNumber', width: 150, align: 'center' },
            { display: 'Item Number', name: 'code', width: 100, align: 'center' },
            { display: 'Item Name', name: 'goodsName', width: 120, align: 'center' },
            { display: 'Product Specification', name: 'spec', width: 100, align: 'center' },
            { display: 'Item Unit', name: 'unitName', width: 70, align: 'center' },
            {
                display: 'Total Material Quantity', name: 'sumNum', width: 120, align: 'center',
                totalSummary:
                {
                    align: 'right',   //Align:left/center/right 
                    type: 'sum',
                    render: function (e) {  
                        //e Object(Include sum,max,min,avg,count) 
                        return Math.round(e.sum * 100) / 100;
                    }
                }
            },

            {
                display: 'Total Value of Materials', name: 'sumPrice', width: 80, align: 'center',

                totalSummary:
                {
                    align: 'right',   //Align:left/center/right 
                    type: 'sum',
                    render: function (e) {  
                        //e Object(Include sum,max,min,avg,count) 
                        return Math.round(e.sum * 100) / 100;
                    }
                }
            },
            { display: 'Created By', name: 'makeName', width: 80, align: 'center' },
            { display: 'Order Date', name: 'bizDate', width: 80, align: 'center', valign: 'center' },
            { display: 'Reviewed By', name: 'checkName', width: 80, align: 'center' },
            { display: 'Order Status', name: 'flag', width: 80, align: 'center' },
            { display: 'Remarks', name: 'remarks', width: 100, align: 'left' }
        ], width: '1120',
        //pageSizeOptions: [5, 10, 15, 20],
        height: '98%',
        // pageSize: 15,
        dataAction: 'local',
        usePager: false,
        url: "produceGetList.aspx?Action=GetDataList",
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
    if (keys == "Please Enter Receipt No./Product/Remarks") {

        keys = "";

    }
    var start = document.getElementById("txtDateStart").value;
    var end = document.getElementById("txtDateEnd").value;
    manager.changePage("first");
    manager._setUrl("produceGetList.aspx?Action=GetDataListSearch&types=0&keys=" + keys + "&start=" + start + "&end=" + end);

    //  window.open("produceGetList.aspx?Action=GetDataListSearch&types=0&keys=" + keys + "&start=" + start + "&end=" + end);
}

function deleteRow() {
    var row = manager.getSelectedRow();
    if (!row) { $.ligerDialog.warn('Please select the rows you want to delete'); return; }

    var idString = checkedCustomer.join(',');

    $.ligerDialog.confirm('Deletion cannot be restored. Confirm deletion?', function (type) {
        if (type) {
            $.ajax({
                type: "GET",
                url: "produceGetList.aspx",
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
        url: "produceGetList.aspx",
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
        url: "produceGetList.aspx",
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
    parent.f_addTab('produceGetListAdd', 'Production Material Collection-Add', 'produce/produceGetListAdd.aspx');
}

function editRow() {
    var row = manager.getSelectedRow();

    parent.f_addTab('produceGetListEdit', 'Production Material Collection-Edit', 'produce/produceGetListEdit.aspx?pId=' + row.id);
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
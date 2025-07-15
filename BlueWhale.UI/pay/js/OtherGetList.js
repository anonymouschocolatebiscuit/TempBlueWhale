var manager;
$(function () {
    var form = $("#form").ligerForm();
    var menu = $.ligerMenu({
        width: 120, items:
            [
                { text: 'Add', click: add, icon: 'add' },
                { text: 'Edit', click: editRow },
                { line: true },
                { text: 'Search', click: viewRow }
            ]
    });

    manager = $("#maingrid").ligerGrid({
        checkbox: true,
        columns: [
            {
                display: 'Action', isSort: false, width: 50, align: 'center', render: function (rowdata, rowindex, value) {
                    var h = "";
                    if (!rowdata._editing) {
                        h += "<a href='javascript:editRow()' title='Edit row' style='float:left;'><div class='ui-icon ui-icon-pencil'></div></a> ";
                        h += "<a href='javascript:deleteRow()' title='Delete row' style='float:right;'><div class='ui-icon ui-icon-trash'></div></a> ";
                    }
                    else {
                        h += "<a href='javascript:endEdit(" + rowindex + ")'>submit</a> ";
                        h += "<a href='javascript:cancelEdit(" + rowindex + ")'>cancel</a> ";
                    }
                    return h;
                }
            },

            {
                display: 'Payment date', name: 'bizDate', width: 150, align: 'center', valign: 'center',

                totalSummary:
                {
                    type: 'count',
                    render: function (e) {  
                        return 'Total:';
                    }
                }

            },
            { display: 'Receipt number', name: 'number', width: 150, align: 'center' },
            { display: 'Settlement Account', name: 'bkName', width: 150, align: 'center' },
            { display: 'Customer', name: 'wlName', width: 80, align: 'left' },
            {
                display: 'Amount of payment', name: 'sumPrice', width: 180, align: 'right',

                totalSummary:
                {
                    align: 'right',   
                    type: 'sum',
                    render: function (e) {  
                        return Math.round(e.sum * 100) / 100;
                    }
                }

            },
            { display: 'Status', name: 'flag', width: 80, align: 'center' },

            { display: 'Make name', name: 'makeName', width: 80, align: 'center' },
            { display: 'Review name', name: 'checkName', width: 90, align: 'center' },
            { display: 'Remark', name: 'remarks', width: 200, align: 'left' }


        ], width: '98%',
        //pageSizeOptions: [5, 10, 15, 20],
        height: '98%',
        // pageSize: 15,
        dataAction: 'local', 
        usePager: false,
        url: "OtherGetList.aspx?Action=GetDataList",
        alternatingRow: false,
        onDblClickRow: function (data, rowindex, rowobj) {
            viewRow();
        },
        rownumbers: true,
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
    if (keys == "Please enter receipt number/customer/remark") {
        keys = "";
    }
    var start = document.getElementById("txtDateStart").value;
    var end = document.getElementById("txtDateEnd").value;
    manager.changePage("first");
    manager._setUrl("OtherGetList.aspx?Action=GetDataListSearch&types=0&keys=" + keys + "&start=" + start + "&end=" + end);
}

function deleteRow() {
    var row = manager.getSelectedRow();
    if (!row) { $.ligerDialog.warn('Please select the row you want to delete'); return; }
    var idString = checkedCustomer.join(',');

    $.ligerDialog.confirm('Deletion cannot be restored, are you sure to delete?', function (type) {
        if (type) {
            $.ajax({
                type: "GET",
                url: "OtherGetList.aspx",
                data: "Action=delete&id=" + idString + " &ranid=" + Math.random(),
                success: function (resultString) {

                    $.ligerDialog.alert(resultString, 'Prompt Message');

                    reload();
                },
                error: function (msg) {

                    $.ligerDialog.alert("Network error, please contact the administrator", 'Prompt Message');
                }
            });
        }
    });
}


function checkRow() {

    var row = manager.getSelectedRow();
    if (!row) { $.ligerDialog.warn('Please select the row you want to check'); return; }

    var idString = checkedCustomer.join(',');

    $.ajax({
        type: "GET",
        url: "OtherGetList.aspx",
        data: "Action=checkRow&idString=" + idString + "&ranid=" + Math.random(), //encodeURI
        success: function (resultString) {
            $.ligerDialog.alert(resultString, 'Prompt Message');
            reload();

        },
        error: function (msg) {

            $.ligerDialog.alert("Network error, please contact the administrator", 'Prompt Message');
        }
    });
}

function checkNoRow() {
    var row = manager.getSelectedRow();
    if (!row) { $.ligerDialog.warn('Please select the row you want to check'); return; }

    var idString = checkedCustomer.join(','); 

    $.ajax({
        type: "GET",
        url: "OtherGetList.aspx",
        data: "Action=checkNoRow&idString=" + idString + "&ranid=" + Math.random(), //encodeURI
        success: function (resultString) {
            $.ligerDialog.alert(resultString, 'Prompt Message');
            reload();

        },
        error: function (msg) {

            $.ligerDialog.alert("Network error, please contact the administrator", 'Prompt Message');
        }
    });
}

function add() {

    parent.f_addTab('OtherGetListAdd', 'Other Get List - Add', 'pay/OtherGetListAdd.aspx?id=0');

    top.topManager.openPage({
        id: 'OtherGetListAdd',
        href: 'pay/OtherGetListAdd.aspx',
        title: 'Other Get List - Add'
    });


}

function editRow() {
    var row = manager.getSelectedRow();

    parent.f_addTab('OtherGetListEdit', 'Other Get List - Edit', 'pay/OtherGetListEdit.aspx?id=' + row.id);

    top.topManager.openPage({
        id: 'OtherGetListEdit',
        href: 'pay/OtherGetListEdit.aspx?id=' + row.id,
        title: 'Other Get List - Edit'
    });


}

function viewRow() {
    var row = manager.getSelectedRow();

    parent.f_addTab('OtherGetListEdit', 'Other Get List - Details', 'pay/OtherGetListEdit.aspx?id=' + row.id);

    top.topManager.openPage({
        id: 'OtherGetListView',
        href: 'store/OtherGetListView.aspx?id=' + row.id,
        title: 'Other Get List - Details'
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
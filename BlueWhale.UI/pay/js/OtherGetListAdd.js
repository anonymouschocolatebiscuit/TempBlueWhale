var data = [{
    UnitPrice: 10,
    Quantity: 2,
    Price: 20
}];

function formatCurrency(x) {
    var f_x = parseFloat(x);
    if (isNaN(f_x)) {
        // alert('function:changeTwoDecimal->parameter error');
        return "0.00";
    }

    f_x = Math.round(x * 100) / 100;
    var s_x = f_x.toString();
    var pos_decimal = s_x.indexOf('.');

    if (pos_decimal < 0) {
        pos_decimal = s_x.length;
        s_x += '.';
    }

    while (s_x.length <= pos_decimal + 2) {
        s_x += '0';
    }

    return s_x;
}

var inventorylist = {};

$.ligerDefaults.Grid.formatters['numberbox'] = function (value, column) {
    var precision = column.editor.precision;
    return value.toFixed(precision);
};

var manager;

$(function () {
    var form = $("#form").ligerForm();
    var g = $.ligerui.get("ddlVenderList");
    g.set("Width", 250);

    var paymentDate = $.ligerui.get("txtBizDate");
    paymentDate.set("Width", 120);

    window['g'] = manager = $("#maingrid").ligerGrid({
        columns: [
            {
                display: '',
                isSort: false,
                width: 40,
                align: 'center',
                frozen: true,
                render: function (rowdata, rowindex, value) {
                    var h = "";
                    if (!rowdata._editing) {
                        h += "<a href='javascript:addNewRow()' title='Add row' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                        h += "<a href='javascript:deleteRow()' title='Delete row' style='float:right;'><div class='ui-icon ui-icon-trash'></div></a> ";
                    } else {
                        // h += "<a href='javascript:endEdit(" + rowindex + ")'>Submit</a> ";
                        // h += "<a href='javascript:cancelEdit(" + rowindex + ")'>Cancel</a> "; 
                    }
                    return h;
                }
            },
            {
                display: 'Income Category',
                name: 'typeId',
                width: 200,
                isSort: false,
                textField: 'typeName',
                editor: {
                    type: 'select',
                    url: "../baseSet/PayGetList.aspx?Action=GetDDLList&type=Income&r=" + Math.random(),
                    valueField: 'typeId',
                    textField: 'typeName'
                }
            },
            {
                display: 'Amount',
                name: 'price',
                width: 190,
                type: 'float',
                align: 'right',
                editor: { type: 'float' },
                totalSummary: {
                    align: 'center', // Summary cell content alignment: left/center/right
                    type: 'sum',
                    render: function (e) { // Summary renderer, return html to be loaded into the cell
                        // e Summary Object (including sum, max, min, avg, count)
                        return Math.round(e.sum * 100) / 100;
                    }
                }
            },
            {
                display: 'Remarks',
                name: 'remarks',
                width: 250,
                align: 'left',
                type: 'text',
                editor: { type: 'text' }
            }
        ],
        width: '99%',
        pageSizeOptions: [5, 10, 15, 20],
        height: '450',
        url: 'OtherGetListAdd.aspx?Action=GetData',
        rownumbers: true, // Display serial number
        frozenRownumbers: true, // Whether the row number is in a fixed column
        dataAction: 'local', // Local sort
        usePager: false,
        alternatingRow: false,
        totalSummary: true,
        enabledEdit: true
    });
});

var rowNumber = 9;

function f_onSelected(e) {
    if (!e.data || !e.data.length) return;

    var grid = liger.get("maingrid");
    var selected = e.data[0];

    grid.updateRow(grid.lastEditRow, {
        CustomerID: selected.CustomerID,
        CompanyName: selected.CompanyName
    });

    var out = JSON.stringify(selected);
    $("#message").html('Final Selection: ' + out);
}

function deleteRow() {
    if (manager.rows.length === 1) {
        $.ligerDialog.warn('At least keep one row!');
    } else {
        manager.deleteSelectedRow();
    }
}

var newrowid = 100;

function addNewRow() {
    var gridData = manager.getData();
    var rowNum = gridData.length;

    manager.addRow({
        id: rowNum + 1,
        typeId: "",
        typeName: "",
        price: "",
        remarks: ""
    });
}

function getSelected() {
    var row = manager.getSelectedRow();
    if (!row) {
        alert('Please select a row!');
        return;
    }
    alert(JSON.stringify(row));
}

function getData() {
    var data = manager.getData();
    alert(JSON.stringify(data));
}

function save() {
    var data = manager.getData();

    for (var i = data.length - 1; i >= 0; i--) {
        if (data[i].typeId == 0 || data[i].typeName === "") {
            data.splice(i, 1);
        }
    }

    // 2. Determine whether a product is selected
    if (data.length === 0) {
        $.ligerDialog.warn('Please select payment type!');
        return;
    }

    // 3. Determine whether the product amount has been entered
    for (var j = 0; j < data.length; j++) {
        if (data[j].price <= 0 || data[j].price === "" || data[j].price === "0" || data[j].price === "0.00") {
            $.ligerDialog.warn("Please enter the " + (j + 1) + " row income amount!");
            return;
        }
    }

    var venderId = $("#ddlVenderList").val();
    var bkId = $("#ddlBankList").val();
    var bizDate = $("#txtBizDate").val();

    if (bizDate === "") {
        $.ligerDialog.warn("Please enter payment date!");
        return;
    }

    var remarks = $("#txtRemarks").val();
    var headJson = {
        venderId: venderId,
        bizDate: bizDate,
        remarks: remarks,
        bkId: bkId
    };

    var list = JSON.stringify(headJson);
    list = list.substring(0, list.length - 1);
    list += ",\"Rows\":";
    list += JSON.stringify(data);
    list += "}";

    var postData = JSON.parse(list);

    $.ajax({
        type: "POST",
        url: 'ashx/OtherGetListAdd.ashx',
        contentType: "application/json",
        data: JSON.stringify(postData),
        success: function (jsonResult) {
            if (jsonResult === "Execution successful！") {
                $.ligerDialog.waitting('Execution successful！');
                setTimeout(function () {
                    $.ligerDialog.closeWaitting();
                    location.reload();
                }, 2000);
            } else {
                $.ligerDialog.warn(jsonResult);
            }
        },
        error: function (xhr) {
            alert("An error occurred, please try again later: " + xhr.responseText);
        }
    });
}

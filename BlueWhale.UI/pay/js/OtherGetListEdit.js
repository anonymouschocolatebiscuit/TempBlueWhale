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
    var f_x = Math.round(x * 100) / 100;
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

var cangkulist = {};


$.ligerDefaults.Grid.formatters['numberbox'] = function (value, column) {
    var precision = column.editor.precision;
    return value.toFixed(precision);
};

var manager;
$(function () {

    var form = $("#form").ligerForm();

    var g = $.ligerui.get("ddlVenderList");
    g.set("Width", 250);


    window['g'] =
        manager = $("#maingrid").ligerGrid({
            columns: [

                {
                    display: '', isSort: false, width: 40, align: 'center', frozen: true, render: function (rowdata, rowindex, value) {
                        var h = "";
                        if (!rowdata._editing) {
                            h += "<a href='javascript:addNewRow()' title='Add row' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                            h += "<a href='javascript:deleteRow()' title='Delete Row' style='float:right;'><div class='ui-icon ui-icon-trash'></div></a> ";
                        }
                        else {
                        }
                        return h;
                    }
                },
                {
                    display: 'Income Category', name: 'typeId', width: 200, isSort: false, textField: 'typeName',
                    editor: {
                        type: 'select',
                        url: "../baseSet/PayGetList.aspx?Action=GetDDLList&type=收入&r=" + Math.random(),
                        valueField: 'typeId', textField: 'typeName'
                    }
                },
                {
                    display: 'Amount', name: 'price', width: 190, type: 'float', align: 'right', editor: { type: 'float' },
                    totalSummary:
                    {
                        align: 'center',   
                        type: 'sum',
                        render: function (e) {  
                            return Math.round(e.sum * 100) / 100;
                        }
                    }
                },
                { display: 'Remarks', name: 'remarks', width: 250, align: 'left', type: 'text', editor: { type: 'text' } }
            ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '450',
            url: "OtherGetListEdit.aspx?Action=GetData&id=" + getUrlParam("id"),
            rownumbers: true,//
            frozenRownumbers: true,//
            dataAction: 'local',//
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
    $("#message").html('last select:' + out);
}

function deleteRow() {
    if (manager.rows.length == 1) {
        $.ligerDialog.warn('Keep at least one row!')

    }
    else {
        manager.deleteSelectedRow();
    }
}

var newrowid = 100;

function addNewRow() {
    var gridData = manager.getData();
    var rowNum = gridData.length;
    manager.addRow({
        id: rowNum + 1,
        id: rowNum + 1,
        typeId: "",
        typeName: "",
        price: "",
        remarks: ""
    });
}

function getSelected() {
    var row = manager.getSelectedRow();
    if (!row) { alert('Please select a row'); return; }
    alert(JSON.stringify(row));
}

function getData() {
    var data = manager.getData();
    alert(JSON.stringify(data));
}

function save() {
    var data = manager.getData();
    //1, First delete the blank rows
    for (var i = data.length - 1; i >= 0; i--) {
        if (data[i].typeId == 0 || data[i].typeName == "") {
            data.splice(i, 1);
        }
    }

    //2, Check if a product is selected
    if (data.length == 0) {
        $.ligerDialog.warn('Please select income category!');
        return;
        alert("I won't execute it.!");
    }

    //3, Check if the quantity for all products has been entered
    for (var i = 0; i < data.length; i++) {
        if (data[i].price <= 0 || data[i].price == "" || data[i].price == "0" || data[i].price == "0.00") {
            $.ligerDialog.warn("Please enter" + (i + 1) + "row of amount!");
            return;
            alert("I won't execute it.!");
        }
    }

    var venderId = $("#ddlVenderList").val();  
    var bkId = $("#ddlBankList").val();  
    var bizDate = $("#txtBizDate").val();
    if (bizDate == "") {
        $.ligerDialog.warn("Please enter the payment date!");
        return;
    }

    var remarks = $("#txtRemarks").val();
    var headJson = { id: getUrlParam("id"), venderId: venderId, bizDate: bizDate, remarks: remarks, bkId: bkId };
    var dataNew = [];
    dataNew.push(headJson);
    var list = JSON.stringify(headJson);
    var goodsList = [];
    list = list.substring(0, list.length - 1);
    list += ",\"Rows\":";
    list += JSON.stringify(data);
    list += "}";

    var postData = JSON.parse(list);
    $.ajax({
        type: "POST",
        url: 'ashx/OtherGetListEdit.ashx',
        contentType: "application/json", 
        data: JSON.stringify(postData),  
        success: function (jsonResult) {

            if (jsonResult == "Success!") {

                $.ligerDialog.waitting('Success!'); setTimeout(function () { $.ligerDialog.closeWaitting(); location.reload(); }, 2000);

            }
            else {
                $.ligerDialog.warn(jsonResult);
            }
        },
        error: function (xhr) {
            alert("An error occurred, please try again later:" + xhr.responseText);
        }
    });
}

function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);

    if (r != null) return unescape(r[2]); return null;
}

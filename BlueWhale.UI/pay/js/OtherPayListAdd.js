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

// Extend the formatting function of the numberbox type
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

                // { display: 'Primary Key', name: 'id', width: 50, type: 'int',hide:true},
                {
                    display: '', isSort: false, width: 40, align: 'center', frozen: true, render: function (rowdata, rowindex, value) {
                        var h = "";
                        if (!rowdata._editing) {
                            h += "<a href='javascript:addNewRow()' title='Add row' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                            h += "<a href='javascript:deleteRow()' title='Delete row' style='float:right;'><div class='ui-icon ui-icon-trash'></div></a> ";
                        }
                        else {
                            //                        h += "<a href='javascript:endEdit(" + rowindex + ")'>Submit</a> ";
                            //                        h += "<a href='javascript:cancelEdit(" + rowindex + ")'>Cancel</a> "; 
                        }
                        return h;
                    }
                }
                ,
                {
                    display: 'Payment Type', name: 'typeId', width: 200, isSort: false, textField: 'typeName',
                    editor: {
                        type: 'select',
                        url: "../baseSet/PayGetList.aspx?Action=GetDDLList&type=expenditure&r=" + Math.random(),
                        valueField: 'typeId', textField: 'typeName'
                    }
                },
                {
                    display: 'Amount', name: 'price', width: 190, type: 'float', align: 'right', editor: { type: 'float' },

                    totalSummary:
                    {
                        align: 'center',   // Alignment of summary cell contents: left/center/right
                        type: 'sum',
                        render: function (e) {  // Summary renderer, returns HTML loaded into the cell
                            //e Summary Object (including sum, max, min, avg, count)

                            // alert("Summarized");

                            return Math.round(e.sum * 100) / 100;
                        }
                    }
                },
                { display: 'Remarks', name: 'remarks', width: 250, align: 'left', type: 'text', editor: { type: 'text' } }
            ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '450',
            url: 'OtherPayListAdd.aspx?Action=GetData',
            rownumbers: true,// Display serial number
            frozenRownumbers: true,// Whether the row number is in a fixed column
            dataAction: 'local',// Local sorting
            usePager: false,
            alternatingRow: false,
            totalSummary: true,
            enabledEdit: true
        }
        );
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
    $("#message").html('Final Choice:' + out);
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
    // Delete the blank lines first

    var data = manager.getData();

    // 1.Delete the blank lines first
    for (var i = data.length - 1; i >= 0; i--) {
        if (data[i].typeId == 0 || data[i].typeName == "") {
            data.splice(i, 1);
        }
    }

    // 2. Determine whether to select a product
    if (data.length == 0) {
        $.ligerDialog.warn('Please select payment type!');

        return;
        alert("Won't execute it!");
    }

    // 3. Determine whether the quantity of goods has been entered.
    for (var i = 0; i < data.length; i++) {
        if (data[i].price <= 0 || data[i].price == "" || data[i].price == "0" || data[i].price == "0.00") {

            $.ligerDialog.warn("Please enter the" + (i + 1) + "row payment amount of the bank");

            return;
            alert("Won't execute it!");
        }
    }

    //    var checkText=$("#ddlVenderList").find("option:selected").text();  // Get the selected text
    var venderId = $("#ddlVenderList").val();  // Get the selected value

    var bkId = $("#ddlBankList").val();  // Get the selected value

    var bizDate = $("#txtBizDate").val();
    if (bizDate == "") {
        $.ligerDialog.warn("Please enter payment date!");
        return;
    }

    var remarks = $("#txtRemarks").val();

    var headJson = { venderId: venderId, bizDate: bizDate, remarks: remarks, bkId: bkId };

    var dataNew = [];
    dataNew.push(headJson);

    var list = JSON.stringify(headJson);

    var goodsList = [];

    list = list.substring(0, list.length - 1);

    list += ",\"Rows\":";
    list += JSON.stringify(data);
    list += "}";

    var postData = JSON.parse(list);

    //        alert(postData.Rows[0].id);
    //        
    //        alert(postData.bizDate);
    //        
    //        alert(postData.Rows[0].goodsName);

    //        alert(JSON.stringify(postData));

    //       $("#txtRemarks").val(JSON.stringify(postData));

    $.ajax({
        type: "POST",
        url: 'ashx/OtherPayListAdd.ashx',
        contentType: "application/json", // required
        //dataType: "json", // Indicates the return value type, not required
        data: JSON.stringify(postData),  // Equal //data: "{'str1':'foovalue', 'str2':'barvalue'}",
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

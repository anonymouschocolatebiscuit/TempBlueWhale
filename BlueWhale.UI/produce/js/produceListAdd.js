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

$(document).bind('keydown.grid', function (event) {
    if (event.keyCode == 13 || event.keyCode == 39 || event.keyCode == 9) //enter,right arrow,tap
    {
        manager.endEditToNext();
    }
});

function addNewRow() {

    manager.addRow({

        processId: "",
        num: "",
        price: "",
        sumPrice: "",
        remarks: ""

    });

    f_onAfterEdit();//从新计算


}


// New style introduction line

function f_selectContact() {
    $.ligerDialog.open({
        title: 'Select process', name: 'winselector', width: 840, height: 540, url: '../baseSet/ProcessListSelect.aspx', buttons: [
            { text: 'OK', onclick: f_selectContactOK },
            { text: 'Close', onclick: f_selectContactCancel }
        ]
    });
    return false;
}
function f_selectContactOK(item, dialog) {
    var fn = dialog.frame.f_select || dialog.frame.window.f_select;
    var data = fn();
    if (!data) {
        alert('Please select a row!');
        return;
    }

    f_onGoodsChanged(data);

    dialog.close();

}


function f_selectContactCancel(item, dialog) {
    dialog.close();
}



//Order start

$(function () {
    $("#txtOrderNumber").ligerComboBox({
        onBeforeOpen: f_selectOrder, valueFieldID: 'hfOrderNumber', width: 300
    });
});

function f_selectOrder() {
    $.ligerDialog.open({
        title: 'Select Order', name: 'winselector', width: 950, height: 600, url: '../sales/SalesOrderListSelect.aspx', buttons: [
            { text: 'OK', onclick: f_selectOrderOK },
            { text: 'Close', onclick: f_selectOrderCancel }
        ]
    });
    return false;
}

function f_selectOrderOK(item, dialog) {
    var fn = dialog.frame.f_select || dialog.frame.window.f_select;
    var data = fn();
    if (!data) {
        alert('Please select a row!');
        return;
    }

    $("#txtOrderNumber").val(data[0].number);
    $("#hfOrderNumber").val(data[0].hfOrderNumber);
    $("#txtGoodsName").val(data[0].goodsName);
    $("#hfGoodsId").val(data[0].goodsId);
    $("#txtSpec").val(data[0].spec);
    $("#txtNum").val(data[0].num);
    $("#txtUnitName").val(data[0].unitName);


    var goodsId = data[0].goodsId;

    var num = data[0].num;

    manager.changePage("first"); manager._setUrl("produceListAdd.aspx?Action=GetDataBom&goodsId=" + goodsId + "&num=" + num);


    dialog.close();

}


function f_selectOrderCancel(item, dialog) {
    dialog.close();
}

//Order ends

//Product begins

$(function () {
    $("#txtGoodsName").ligerComboBox({
        onBeforeOpen: f_selectGoods, valueFieldID: 'hfGoodsId', width: 300
    });
});


function f_selectGoods() {
    $.ligerDialog.open({
        title: 'Select Goods', name: 'winselector', width: 800, height: 540, url: '../baseSet/GoodsListSelect.aspx', buttons: [
            { text: 'OK', onclick: f_selectGoodsOK },
            { text: 'Close', onclick: f_selectGoodsCancel }
        ]
    });
    return false;
}


function f_selectGoodsOK(item, dialog) {
    var fn = dialog.frame.f_select || dialog.frame.window.f_select;
    var data = fn();
    if (!data) {
        alert('请选择行!');
        return;
    }

    $("#txtOrderNumber").val("");
    $("#hfOrderNumber").val("");
    $("#txtGoodsName").val(data[0].names);
    $("#hfGoodsId").val(data[0].id);
    $("#txtSpec").val(data[0].spec);
    $("#txtUnitName").val(data[0].unitName);

    $("#txtNum").val("1");

    //start---------添加Bom明细

    var goodsId = data[0].id;


    manager.changePage("first");
    manager._setUrl("produceListAdd.aspx?Action=GetDataBom&goodsId=" + goodsId + "&num=1");



    //end

    dialog.close();

}


function f_selectGoodsCancel(item, dialog) {
    dialog.close();
}

//Product End

//Product Change Event: Get unit, price, and other information
function f_onGoodsChanged(e) {

    if (!e || !e.length) return;

    //1. Update the subsequent data of the current row first

    var grid = liger.get("maingrid");

    var selected = e[0];// e.data[0];

    // alert(selected.names);

    var selectedRow = manager.getSelected();

    grid.updateRow(selectedRow, {

        processId: selected.id,
        processName: selected.names,
        unitId: selected.unitId,
        unitName: selected.unitName,
        num: 1,
        price: selected.price,
        sumPrice: selected.price,
        remarks: ""

    });

    if (e.length > 1) // If there are multiple rows, delete the blank row first, then insert the following
    {

        var data = manager.getData();
        for (var i = data.length - 1; i >= 0; i--) {
            if (data[i].processId == 0 || data[i].processName == "") {
                manager.deleteRow(i);
                // alert("Delete row:"+i);
            }

        }

        for (var i = 1; i < e.length; i++) {
            grid.addRow({
                id: rowNumber,
                processId: e[i].id,
                processName: e[i].names,
                unitId: e[i].unitId,
                unitName: e[i].unitName,
                num: 1,
                price: e[i].price,

                sumPrice: e[i].price,


                remarks: ""

            });

            rowNumber = rowNumber + 1;

        }

    }

}



//New style introduction line end


var managerProcess;
$(function () {

    var form = $("#form").ligerForm();


    var txtGoodsName = $.ligerui.get("txtGoodsName");
    txtGoodsName.set("Width", 250);

    var txtOrderNumber = $.ligerui.get("txtOrderNumber");
    txtOrderNumber.set("Width", 250);

    var txtNum = $.ligerui.get("txtNum");
    txtNum.set("Width", 250);



    window['g'] =
        managerProcess = $("#maingrid9999").ligerGrid({
            columns: [

                {
                    display: '', isSort: false, width: 60, align: 'center', frozen: true, render: function (rowdata, rowindex, value) {
                        var h = "";
                        if (!rowdata._editing) {
                            h += "<a href='javascript:addNewRow()' title='New row' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                            h += "<a href='javascript:deleteRow()' title='Delete row' style='float:left;'><div class='ui-icon ui-icon-trash'></div></a> ";
                            h += "<a href='javascript:f_selectContact()' title='Select Process' style='float:right;'><div class='ui-icon ui-icon-search'></div></a> ";
                        }
                        else {
                            // h += "<a href='javascript:endEdit(" + rowindex + ")'>Submit</a> ";
                            // h += "<a href='javascript:cancelEdit(" + rowindex + ")'>Cancel</a>";
                        }
                        return h;
                    }
                }
                ,

                {
                    display: 'Process Name', name: 'processName', width: 250, align: 'left',

                    totalSummary:
                    {
                        type: 'count',
                        render: function (e) { // Summary renderer, return HTML to load into cell
                            //e Summary Object (including sum, max, min, avg, count)
                            return 'Total:';
                        }
                    }

                },

                { display: 'Unit', name: 'unitName', width: 100, align: 'center' },

                {
                    display: 'Quantity of single product process', name: 'num', width: 120, type: 'float', align: 'right', editor: { type: 'float' },

                    totalSummary:
                    {
                        align: 'right', //Summary cell content alignment: left/center/right
                        type: 'sum',
                        render: function (e) { //Summary renderer, returns HTML to load into the cell
                            //e Summary Object (including sum, max, min, avg, count)
                            return Math.round(e.sum * 100) / 100;
                        }
                    }

                },

                { display: 'Process Unit Price', name: 'price', width: 120, align: 'right', type: 'float', editor: { type: 'float' } },
                {
                    display: 'Product Process Amount', name: 'sumPrice', width: 120, align: 'right', type: 'float',
                    totalSummary:
                    {
                        align: 'right', //Summary cell content alignment: left/center/right
                        type: 'sum',
                        render: function (e) { //Summary renderer, returns HTML to load into the cell
                            //e Summary Object (including sum, max, min, avg, count)
                            var itemSumPrice = e.sum;
                            return "<span id='sumPriceItem'>" + Math.round(itemSumPrice * 10000) / 10000 + "</span>";//formatCurrency(suminf.sum)

                        }
                    }

                },

                { display: 'Remarks', name: 'remarks', width: 220, align: 'left', type: 'text', editor: { type: 'text' } }
            ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '350',
            url: 'produceListAdd.aspx?Action=GetData',
            rownumbers: true, //Display row numbers
            frozenRownumbers: true, //Should row numbers be in a fixed column?
            dataAction: 'local', //Local sorting
            usePager: false,
            alteringRow: false,

            totalSummary: false,
            enabledEdit: true, //Controls editing

            onAfterEdit: f_onAfterEdit //Operations after updating a cell
        }
        );
});
//

//Edited event---------payment amount


function f_onAfterEdit(e) {



    var num, price, sumPrice;


    num = Number(e.record.num);

    price = Number(e.record.price);




    var processId, processName;

    processId = e.record.processId;
    processName = e.record.processName;

    if (processId == "" || processName == "") {
        return;
    }




    if (e.column.name == "num") // Quantity Change --- Start
    {
        // Quantity Change: [Discount Rate, Tax Rate] Calculate [Discount Amount, Amount, Tax Amount, Price-Tax Total]

        num = Number(e.value);

        num = Math.round(num * 100) / 100;

        price = Math.round(price * 100) / 100;

        sumPrice = Math.round(sumPrice * 100) / 100;

        // Start Assigning Values

        manager.updateCell("num", num, e.record);

        // 2. Amount
        manager.updateCell('sumPrice', sumPrice, e.record);

    } // Quantity Change --- End

    if (e.column.name == "price") // Unit price changes --- start, calculate amount, discount, tax, and total price and tax
    {
        // Unit price changes: [Quantity, Discount Rate, Tax Rate] Calculate [Discount Amount, Amount, Tax Amount, and Total Price and Tax];
        price = Number(e.value);

        // 2. Amount = Quantity * Unit Price - Discount Amount
        sumPrice = Number(num) * Number(price);

        num = Math.round(num * 100) / 100;
        price = Math.round(price * 100) / 100;

        sumPrice = Math.round(sumPrice * 100) / 100;

        // Start assigning values

        // 1. Discount Amount

        manager.updateCell("price", price, e.record);

        // 2. Amount
        manager.updateCell('sumPrice', sumPrice, e.record);

    } // Change unit price --- End

    // Finally, change the value of the summary row


    updateTotal();


}

function updateTotal() {


    var data = manager.getData();//getData
    var sumPriceItem = 0;//

    //1. Delete the blank line first

    for (var i = data.length - 1; i >= 0; i--) {
        if (data[i].processId == 0 || data[i].processId == "" || data[i].processName == "") {
            data.splice(i, 1);

        }

    }

    for (var i = 0; i < data.length; i++) {

        sumPriceItem += Number(data[i].num) * Number(data[i].price);

    }

    $("#sumPriceItem").html(formatCurrency(sumPriceItem));



}



var rowNumber = 9;






function deleteRow() {

    if (manager.rows.length == 1) {
        $.ligerDialog.warn('Keep at least one line!')

    }
    else {
        manager.deleteSelectedRow();


    }

}


var newrowid = 100;



function getSelected() {
    var row = manager.getSelectedRow();
    if (!row) { alert('Please select a row'); return; }
    alert(JSON.stringify(row));
}
function getData() {
    var data = manager.getData();
    alert(JSON.stringify(data));
}


var manager;
$(function () {


    window['gBom'] =
        manager = $("#maingrid").ligerGrid({
            columns: [

                {
                    display: '', isSort: false, width: 60, align: 'center', frozen: true, render: function (rowdata, rowindex, value) {
                        var h = "";
                        if (!rowdata._editing) {
                            // h += "<a href='javascript:addNewRow()' title='Add New Row' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                            h += "<a href='javascript:deleteRow()' title='Delete row' style='float:left;'><div class='ui-icon ui-icon-trash'></div></a> ";
                            //h += "<a href='javascript:f_selectContact()' title='Select Material' style='float:right;'><div class='ui-icon ui-icon-search'></div></a> ";
                        }
                        else {
                            // h += "<a href='javascript:endEdit(" + rowindex + ")'>Submit</a> ";
                            // h += "<a href='javascript:cancelEdit(" + rowindex + ")'>Cancel</a> ";
                        }
                        return h;
                    }
                }
                ,
                { display: 'Product Code', name: 'code', width: 100, align: 'center' },
                {
                    display: 'Product Name', name: 'goodsName', width: 250, align: 'left',

                    totalSummary:
                    {
                        type: 'count',
                        render: function (e) { // Summary renderer, return HTML to load into cell
                            //e Summary Object (including sum, max, min, avg, count)

                            return 'Total:';

                        }

                    }

                },

                { display: 'Specification Model', name: 'spec', width: 130, align: 'center' },

                { display: 'Unit', name: 'unitName', width: 70, align: 'center' },

                {
                    display: 'Standard Usage', name: 'numBom', width: 120, type: 'float', align: 'right',

                    totalSummary:

                    {
                        align: 'right', //Summary cell content alignment: left/center/right

                        type: 'sum',

                        render: function (e) { //Summary renderer, return HTML to load into cell

                            //e Summary Object (including sum, max, min, avg, count)

                            return Math.round(e.sum * 100) / 100;
                        }
                    }

                },

                { display: 'Lose Rate', name: 'rate', width: 60, align: 'right', type: 'float' },
                {
                    display: 'Planned usage', name: 'num', width: 120, align: 'right', type: 'float', editor: { type: 'float' },
                    totalSummary:
                    {
                        align: 'right', // Summary cell content alignment: left/center/right
                        type: 'sum',
                        render: function (e) { // Summary renderer, return HTML to load into cell
                            //e Summary Object (including sum, max, min, avg, count)
                            var itemSumPrice = e.sum;
                            return "<span id='sumPriceItem'>" + Math.round(itemSumPrice * 10000) / 10000 + "</span>"; //formatCurrency(suminf.sum)

                        }

                    }

                },

                //{ display: 'Warehouse', name: 'ckName', width: 100, align: 'center', type: 'text' },

                { display: 'Remarks', name: 'remarks', width: 220, align: 'left', type: 'text', editor: { type: 'text' } }
            ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '350',
            url: "produceListAdd.aspx?Action=GetData",
            rownumbers: true, //Display row numbers
            frozenRownumbers: true, //Are row numbers in a fixed column?
            dataAction: 'local', //Local sorting
            usePager: false,
            alternatingRow: false,

            totalSummary: false,
            enabledEdit: true, //Controls editing

            onAfterEdit: f_onAfterEdit //Operations after updating a cell
        }
        );
});

function save() {

    var hfGoodsId = $("#hfGoodsId").val();
    if (hfGoodsId == "" || hfGoodsId == 0) {
        $.ligerDialog.warn("Please select the product to be produced!");
        return;

    }

    var txtNum = $("#txtNum").val();
    if (txtNum == "" || txtNum == 0) {
        $.ligerDialog.warn("Please enter the production quantity!");
        return;

    }

    // Delete blank lines first

    var data = manager.getData();
    // alert(JSON.stringify(data));

    // 1. Delete blank lines first
    for (var i = data.length - 1; i >= 0; i--) {
        if (data[i].goodsId == 0 || data[i].goodsName == "" || data[i].goodsId == "") {
            data.splice(i, 1);

        }

    }

    var isGongxu = false;

    //2. Determine whether to select a product
    if (data.length == 0) {

        if (confirm("Are you sure you don't want to add a material plan?")) {
            isGongxu = false;

        }
        else {

            isGongxu = true;

        }
    }

    if (isGongxu) {
        if (data.length == 0) {
            $.ligerDialog.warn("Please select a raw material product!");

            return;
            alert("I won't execute this!");

        }
        for (var i = 0; i < data.length; i++) {

            if (data[i].num <= 0 || data[i].num == "") {

                $.ligerDialog.warn("Please enter the number " + (i + 1) + "Planned usage for the row!");

                return;
                alert("I won't execute it!");
            }

        }

    }

    var dateStart = $("#txtDateStart").val();
    if (dateStart == "") {
        $.ligerDialog.warn("Please enter the start date!");
        return;

    }

    var dateEnd = $("#txtDateEnd").val();
    if (dateStart == "") {
        $.ligerDialog.warn("Please enter the end date!");
        return;

    }

    var typeName = $("#ddlTypeName").val();

    var orderNumber = $("#txtOrderNumber").val();

    var remarks = $("#txtRemarks").val();

    var headJson = {
        orderNumber: orderNumber,
        typeName: typeName,
        goodsId: hfGoodsId,
        num: txtNum,
        dateStart: dateStart,
        dateEnd: dateEnd,
        remarks: remarks
    };

    // alert(JSON.stringify(headJson));

    var dataNew = [];

    dataNew.push(headJson);

    var list = JSON.stringify(headJson); // Return to string, table header

    list = list.substring(0, list.length - 1); // Remove the last curly brace

    list += ",\"Rows\":";

    list += JSON.stringify(data); // Insert account information

    list += "}";

    var postData = JSON.parse(list); // Final JSON

    // alert(JSON.stringify(postData));

    // return;
    // alert(postData.Rows[0].id);
    //
    // alert(postData.bizDate);
    //
    // alert(postData.Rows[0].goodsName);

    // alert(JSON.stringify(postData));

    // $("#txtRemarks").val(JSON.stringify(postData));

    // return;

    $.ajax({
        type: "POST",
        url: 'ashx/produceListAdd.ashx',
        contentType: "application/json", // required
        // dataType: "json", // indicates the return value type, not required
        data: JSON.stringify(postData), // equivalent to // data: "{'str1':'foovalue', 'str2':'barvalue'}",
        success: function (jsonResult) {

            if (jsonResult == "Operation successful!") {

                $.ligerDialog.waitting('Operation successful!'); setTimeout(function () { $.ligerDialog.closeWaitting(); location.reload(); }, 2000);

            }
            else {
                $.ligerDialog.warn(jsonResult);

            }
        },
        error: function (xhr) {
            alert("An error occurred. Please try again later:" + xhr.responseText);
        }
    });

}

function getBomList() {

    var goodsId = $("#hfGoodsId").val();
    var num = $("#txtNum").val();

    //alert("goodsId:" + goodsId + " num:" + num);

    if (goodsId > 0 && num > 0) {
        manager.changePage("first");
        manager._setUrl("produceListAdd.aspx?Action=GetDataBom&goodsId=" + goodsId + "&num=" + num);
    }


}
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

    f_onAfterEdit();
}

function f_selectContact() {
    $.ligerDialog.open({
        title: 'Select process', name: 'winselector', width: 840, height: 540, url: '../baseSet/ProcessListSelect.aspx', buttons: [
            { text: 'Confirm', onclick: f_selectContactOK },
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

$(function () {
    $("#txtOrderNumber").ligerComboBox({
        onBeforeOpen: f_selectOrder, valueFieldID: 'hfOrderNumber', width: 300
    });
});

function f_selectOrder() {
    $.ligerDialog.open({
        title: 'Select Production Plan', name: 'winselector', width: 950, height: 600, url: 'produceListSelect.aspx', buttons: [
            { text: 'Confirm', onclick: f_selectOrderOK },
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

    $("#hfPId").val(data[0].id);
    $("#txtOrderNumber").val(data[0].number);
    $("#hfOrderNumber").val(data[0].hfOrderNumber);
    $("#txtGoodsName").val(data[0].goodsName);
    $("#hfGoodsId").val(data[0].goodsId);
    $("#txtSpec").val(data[0].spec);
    $("#txtNum").val(data[0].finishNumNo);
    $("#txtUnitName").val(data[0].unitName);

    var pId = data[0].id;
    var goodsId = data[0].goodsId;
    var num = data[0].finishNumNo;

    manager.changePage("first");
    manager._setUrl("produceGetListAdd.aspx?Action=GetDataBom&pId=" + pId + "&goodsId=" + goodsId + "&num=" + num);

    //window.open("produceGetListAdd.aspx?Action=GetDataBom&pId=" + pId + "&goodsId=" + goodsId + "&num=" + num);

    dialog.close();
}

function f_selectOrderCancel(item, dialog) {
    dialog.close();
}

$(function () {
    //$("#txtGoodsName").ligerComboBox({
    //    onBeforeOpen: f_selectGoods, valueFieldID: 'hfGoodsId', width: 300
    //});
});

function f_selectGoods() {
    $.ligerDialog.open({
        title: 'Select product', name: 'winselector', width: 800, height: 540, url: '../baseSet/GoodsListSelect.aspx', buttons: [
            { text: 'Confirm', onclick: f_selectGoodsOK },
            { text: 'Close', onclick: f_selectGoodsCancel }
        ]
    });
    return false;
}

function f_selectGoodsOK(item, dialog) {
    var fn = dialog.frame.f_select || dialog.frame.window.f_select;
    var data = fn();
    if (!data) {
        alert('Please select a row!');
        return;
    }

    $("#txtOrderNumber").val("");
    $("#hfOrderNumber").val("");
    $("#txtGoodsName").val(data[0].names);
    $("#hfGoodsId").val(data[0].id);
    $("#txtSpec").val(data[0].spec);
    $("#txtUnitName").val(data[0].unitName);

    $("#txtNum").val("1");

    var goodsId = data[0].id;

    manager.changePage("first");
    manager._setUrl("produceGetListAdd.aspx?Action=GetDataBom&goodsId=" + goodsId + "&num=1");

    dialog.close();
}

function f_selectGoodsCancel(item, dialog) {
    dialog.close();
}

function f_onGoodsChanged(e) {
    if (!e || !e.length) return;

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

    if (e.length > 1) {
        var data = manager.getData();
        for (var i = data.length - 1; i >= 0; i--) {
            if (data[i].processId == 0 || data[i].processName == "") {
                manager.deleteRow(i);
                // alert("Delete row："+i);
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
                            h += "<a href='javascript:addNewRow()' title='Add a new row' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                            h += "<a href='javascript:deleteRow()' title='Delete row' style='float:left;'><div class='ui-icon ui-icon-trash'></div></a> ";
                            h += "<a href='javascript:f_selectContact()' title='Select process' style='float:right;'><div class='ui-icon ui-icon-search'></div></a> ";
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
                    display: 'Process Name', name: 'processName', width: 250, align: 'left',

                    totalSummary:
                    {
                        type: 'count',
                        render: function (e) {  //Summary renderer, returns HTML to load into the cell
                            //e Summary Object (including sum, max, min, avg, count)
                            return 'Total:';
                        }
                    }
                },
                { display: 'Unit', name: 'unitName', width: 100, align: 'center' },
                {
                    display: 'No of single product processes', name: 'num', width: 120, type: 'float', align: 'right', editor: { type: 'float' },
                    totalSummary:
                    {
                        align: 'right',   //Summary cell content alignment: left/center/right
                        type: 'sum',
                        render: function (e) {  //Summary renderer, returns HTML to load into the cell
                            //e Summary Object (including sum, max, min, avg, count)
                            return Math.round(e.sum * 100) / 100;
                        }
                    }
                },
                { display: 'Process unit price', name: 'price', width: 120, align: 'right', type: 'float', editor: { type: 'float' } },
                {
                    display: 'Amount per product process', name: 'sumPrice', width: 120, align: 'right', type: 'float',
                    totalSummary:
                    {
                        align: 'right',   //Summary cell content alignment: left/center/right
                        type: 'sum',
                        render: function (e) {  //Summary renderer, returns HTML to load into the cell
                            //e Summary Object (including sum, max, min, avg, count)
                            var itemSumPrice = e.sum;
                            return "<span id='sumPriceItem'>" + Math.round(itemSumPrice * 10000) / 10000 + "</span>";//formatCurrency(suminf.sum)
                        }
                    }
                },
                { display: 'Remarks', name: 'remarks', width: 220, align: 'left', type: 'text', editor: { type: 'text' } }
            ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '350',
            url: 'produceGetListAdd.aspx?Action=GetData',
            rownumbers: true,//Display serial number
            frozenRownumbers: true,//Is the row number in a fixed column
            dataAction: 'local',//Local sorting
            usePager: false,
            alternatingRow: false,
            totalSummary: false,
            enabledEdit: true, //Control whether editing is allowed
            onAfterEdit: f_onAfterEdit //Actions after updating the cell
        }
        );
});

function f_onAfterEdit(e) {
    var num, price, sumPrice;
    num = Number(e.record.num);
    price = Number(e.record.price);
    var goodsId, goodsName;
    goodsId = e.record.goodsId;
    goodsName = e.record.goodsName;

    if (goodsId == "" || goodsName == "") {
        return;
    }

    if (e.column.name == "num") {
        num = Number(e.value);
        num = Math.round(num * 100) / 100;
        price = Math.round(price * 100) / 100;
        sumPrice = Math.round(num * price * 100) / 100;
        manager.updateCell("num", num, e.record);
        manager.updateCell('sumPrice', sumPrice, e.record);
    }

    if (e.column.name == "price") {
        price = Number(e.value);
        sumPrice = Number(num) * Number(price);
        num = Math.round(num * 100) / 100;
        price = Math.round(price * 100) / 100;
        sumPrice = Math.round(sumPrice * 100) / 100;
        manager.updateCell("price", price, e.record);
        manager.updateCell('sumPrice', sumPrice, e.record);
    }

    updateTotal();
}

function updateTotal() {
    var data = manager.getData();
    var sumPriceItem = 0;

    for (var i = data.length - 1; i >= 0; i--) {
        if (data[i].goodsId == 0 || data[i].goodsId == "" || data[i].goodsName == "") {
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
        $.ligerDialog.warn('Keep at least one row!')
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
                    display: '', isSort: false, width: 40, align: 'center', frozen: true, render: function (rowdata, rowindex, value) {
                        var h = "";
                        if (!rowdata._editing) {
                            // h += "<a href='javascript:addNewRow()' title='Add row' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                            h += "<a href='javascript:deleteRow()' title='Delete row' style='text-align :center;'><div class='ui-icon ui-icon-trash'></div></a> ";
                            //h += "<a href='javascript:f_selectContact()' title='Select contact' style='float:right;'><div class='ui-icon ui-icon-search'></div></a> ";
                        }
                        else {
                            //                        h += "<a href='javascript:endEdit(" + rowindex + ")'>Submit</a> ";
                            //                        h += "<a href='javascript:cancelEdit(" + rowindex + ")'>Cancel</a> "; 
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
                        render: function (e) {  //Summary renderer, returns HTML to load into the cell
                            //e Summary Object (including sum, max, min, avg, count)
                            return 'Total:';
                        }
                    }
                },

                { display: 'Specifications', name: 'spec', width: 100, align: 'center' },
                { display: 'Unit', name: 'unitName', width: 70, align: 'center' },
                {
                    display: 'Picking Warehouse', name: 'ckId', width: 80, isSort: false, textField: 'ckName',
                    editor: {
                        type: 'select',
                        url: "../baseSet/InventoryList.aspx?Action=GetDDLList&r=" + Math.random(),
                        valueField: 'ckId', textField: 'ckName'
                    }

                },

                {
                    display: 'Number of applications', name: 'numApply', width: 80, type: 'float', align: 'right',

                    totalSummary:
                    {
                        align: 'right',   //Summary cell content alignment: left/center/right
                        type: 'sum',
                        render: function (e) {  //Summary renderer, returns HTML to load into the cell
                            //e Summary Object (including sum, max, min, avg, count)
                            return Math.round(e.sum * 100) / 100;
                        }
                    }


                },

                {
                    display: 'Actual quantity', name: 'num', width: 80, align: 'right', type: 'float', editor: { type: 'float' },
                    totalSummary:
                    {
                        align: 'right',   //Summary cell content alignment: left/center/right
                        type: 'sum',
                        render: function (e) {  //Summary renderer, returns HTML to load into the cell
                            //e Summary Object (including sum, max, min, avg, count)
                            var itemSumPrice = e.sum;
                            return "<span id='sumNumItem'>" + Math.round(itemSumPrice * 10000) / 10000 + "</span>";//formatCurrency(suminf.sum)


                        }
                    }

                },

                { display: 'Unit Price', name: 'price', width: 70, type: 'float', align: 'right', editor: { type: 'float' } },

                {
                    display: 'Amount', name: 'sumPrice', width: 80, type: 'float', align: 'right',




                    totalSummary:
                    {
                        align: 'center',   //Summary cell content alignment: left/center/right
                        type: 'sum',
                        render: function (e) {  //Summary renderer, returns HTML to load into the cell

                            var itemSumPriceNow = e.sum;
                            return "<span id='sumPriceItem'>" + Math.round(itemSumPriceNow * 10000) / 10000 + "</span>";//formatCurrency(suminf.sum)
                        }
                    }

                },




                { display: 'Remarks', name: 'remarks', width: 220, align: 'left', type: 'text', editor: { type: 'text' } }
            ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '350',
            url: "produceGetListAdd.aspx?Action=GetData",
            rownumbers: true,//Display serial number
            frozenRownumbers: true,//Is the row number in a fixed column
            dataAction: 'local',//Local sorting
            usePager: false,
            alternatingRow: false,

            totalSummary: false,
            enabledEdit: true, //Control whether editing is allowed

            onAfterEdit: f_onAfterEdit //Actions after updating the cell
        }
        );
});

function save() {
    var hfPId = $("#hfPId").val();

    if (hfPId == "" || hfPId == 0) {
        $.ligerDialog.warn("Please select a production plan!");
        return;

    }

    var hfGoodsId = $("#hfGoodsId").val();
    if (hfGoodsId == "" || hfGoodsId == 0) {
        $.ligerDialog.warn("Please select the product you want to produce!");
        return;

    }

    var txtNum = $("#txtNum").val();
    if (txtNum == "" || txtNum == 0) {
        $.ligerDialog.warn("Please fill in the input output!");
        return;

    }

    //先删掉空白行

    var data = manager.getData();

    //1, First delete the blank rows
    for (var i = data.length - 1; i >= 0; i--) {
        if (data[i].goodsId == 0 || data[i].goodsName == "" || data[i].goodsId == "") {
            data.splice(i, 1);

        }
    }

    var isGongxu = false;

    //2, Check if a product is selected
    if (data.length == 0) {
        $.ligerDialog.warn("Please select the product to be picked up!");

        return;
        alert("Execution skipped!");

    }
    for (var i = 0; i < data.length; i++) {


        if (data[i].ckId == 0 || data[i].ckId == "" || data[i].ckId == "0" || data[i].ckName == "") {

            $.ligerDialog.warn("Please enter the" + (i + 1) + "row of Warehouse");

            return;
            alert("Execution skipped!");
        }

        if (data[i].num <= 0 || data[i].num == "") {

            $.ligerDialog.warn("Please enter the" + (i + 1) + "row of the quantity!");

            return;
            alert("Execution skipped!");
        }
    }

    var bizId = $("#ddlYWYList").val();

    var bizDate = $("#txtBizDate").val();
    if (bizDate == "") {
        $.ligerDialog.warn("Please fill in the date of receipt");
        return;
    }

    var orderNumber = $("#txtOrderNumber").val();

    var remarks = $("#txtRemarks").val();

    var headJson = {
        bizId: bizId,
        bizDate: bizDate,
        planNumber: orderNumber,
        goodsId: hfGoodsId,
        num: txtNum,
        remarks: remarks
    };

    // alert(JSON.stringify(headJson));

    var dataNew = [];

    dataNew.push(headJson);

    var list = JSON.stringify(headJson); // Return serialization to string, header

    list = list.substring(0, list.length - 1); // Remove the last curly brace

    list += ",\"Rows\":";
    list += JSON.stringify(data); // Insert account information   

    list += "}";

    var postData = JSON.parse(list);// Final json

    //   alert(JSON.stringify(postData));

    //  return;
    //        alert(postData.Rows[0].id);
    //        
    //        alert(postData.bizDate);
    //        
    //        alert(postData.Rows[0].goodsName);

    //   alert(JSON.stringify(postData));

    //       $("#txtRemarks").val(JSON.stringify(postData));

    // return;

    $.ajax({
        type: "POST",
        url: 'ashx/produceGetListAdd.ashx',
        contentType: "application/json",
        //dataType: "json",
        data: JSON.stringify(postData),  // Equal //data: "{'str1':'foovalue', 'str2':'barvalue'}",
        success: function (jsonResult) {

            if (jsonResult == "Operation Successful!") {
                $.ligerDialog.waitting('Operation Successful!'); setTimeout(function () { $.ligerDialog.closeWaitting(); location.reload(); }, 2000);
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

function getBomList() {
    var pId = $("#hfPId").val();
    var goodsId = $("#hfGoodsId").val();
    var num = $("#txtNum").val();

    //alert("goodsId:" + goodsId + " num:" + num);

    if (goodsId > 0 && num > 0) {
        manager.changePage("first");
        manager._setUrl("produceGetListAdd.aspx?Action=GetDataBom&pId=" + pId + "&goodsId=" + goodsId + "&num=" + num);
    }

    //window.open("produceGetListAdd.aspx?Action=GetDataBom&pId=" + pId + "&goodsId=" + goodsId + "&num=" + num);
}
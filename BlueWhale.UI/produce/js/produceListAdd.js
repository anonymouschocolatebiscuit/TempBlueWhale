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
    if (event.keyCode == 13 || event.keyCode == 39 || event.keyCode == 9) // enter, right arrow, tab
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

    f_onAfterEdit();// Recalculate
}

function f_selectContact() {
    $.ligerDialog.open({
        title: 'Select Process', name: 'winselector', width: 840, height: 540, url: '../baseSet/ProcessListSelect.aspx', buttons: [
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
        title: 'Select Order', name: 'winselector', width: 950, height: 600, url: '../sales/SalesOrderListSelect.aspx', buttons: [
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

    $("#txtOrderNumber").val(data[0].number);
    $("#hfOrderNumber").val(data[0].hfOrderNumber);
    $("#txtGoodsName").val(data[0].goodsName);
    $("#hfGoodsId").val(data[0].goodsId);
    $("#txtSpec").val(data[0].spec);
    $("#txtNum").val(data[0].num);
    $("#txtUnitName").val(data[0].unitName);

    var goodsId = data[0].goodsId;

    var num = data[0].num;

    manager.changePage("first");
    manager._setUrl("produceListAdd.aspx?Action=GetDataBom&goodsId=" + goodsId + "&num=" + num);

    dialog.close();
}

function f_selectOrderCancel(item, dialog) {
    dialog.close();
}

$(function () {
    $("#txtGoodsName").ligerComboBox({
        onBeforeOpen: f_selectGoods, valueFieldID: 'hfGoodsId', width: 300
    });
});

function f_selectGoods() {
    $.ligerDialog.open({
        title: 'Select Product', name: 'winselector', width: 800, height: 540, url: '../baseSet/GoodsListSelect.aspx', buttons: [
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
    manager._setUrl("produceListAdd.aspx?Action=GetDataBom&goodsId=" + goodsId + "&num=1");

    //end
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

    if (e.length > 1) // if multiple data, remove the last line then insert data
    {
        var data = manager.getData();
        for (var i = data.length - 1; i >= 0; i--) {
            if (data[i].processId == 0 || data[i].processName == "") {
                manager.deleteRow(i);
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
                            h += "<a href='javascript:addNewRow()' title='Insert row' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                            h += "<a href='javascript:deleteRow()' title='Delete row' style='float:left;'><div class='ui-icon ui-icon-trash'></div></a> ";
                            h += "<a href='javascript:f_selectContact()' title='Select process' style='float:right;'><div class='ui-icon ui-icon-search'></div></a> ";
                        }
                        else {
                            //                        h += "<a href='javascript:endEdit(" + rowindex + ")'>Submit</a> ";
                            //                        h += "<a href='javascript:cancelEdit(" + rowindex + ")'>Cancel</a> "; 
                        }
                        return h;
                    }
                },
                {
                    display: 'Process Name', name: 'processName', width: 250, align: 'left',

                    totalSummary:
                    {
                        type: 'count',
                        render: function (e) {  //Total Renderer，return html load into unit cell
                            //e Total Object(Including sum,max,min,avg,count) 
                            return 'Total：';
                        }
                    }
                },
                { display: 'Unit', name: 'unitName', width: 100, align: 'center' },
                {
                    display: 'Single Product Process Quantity', name: 'num', width: 120, type: 'float', align: 'right', editor: { type: 'float' },
                    totalSummary:
                    {
                        align: 'right',
                        type: 'sum',
                        render: function (e) {
                            return Math.round(e.sum * 100) / 100;
                        }
                    }
                },
                { display: 'Single Process Price', name: 'price', width: 120, align: 'right', type: 'float', editor: { type: 'float' } },
                {
                    display: 'Single Product Process Price', name: 'sumPrice', width: 120, align: 'right', type: 'float',
                    totalSummary:
                    {
                        align: 'right',
                        type: 'sum',
                        render: function (e) {
                            var itemSumPrice = e.sum;
                            return "<span id='sumPriceItem'>" + Math.round(itemSumPrice * 10000) / 10000 + "</span>";// formatCurrency(suminf.sum)
                        }
                    }
                },
                { display: 'Remarks', name: 'remarks', width: 220, align: 'left', type: 'text', editor: { type: 'text' } }
            ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '350',
            url: 'produceListAdd.aspx?Action=GetData',
            rownumbers: true,
            frozenRownumbers: true,
            dataAction: 'local',
            usePager: false,
            alternatingRow: false,
            totalSummary: false,
            enabledEdit: true,
            onAfterEdit: f_onAfterEdit
        });
});

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

    if (e.column.name == "num")
    {
        num = Number(e.value);
        num = Math.round(num * 100) / 100;
        price = Math.round(price * 100) / 100;
        sumPrice = Math.round(sumPrice * 100) / 100;

        manager.updateCell("num", num, e.record);
        manager.updateCell('sumPrice', sumPrice, e.record);
    }

    if (e.column.name == "price") 
    {
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
        $.ligerDialog.warn('At least one row is required！')
    }
    else {
        manager.deleteSelectedRow();
    }
}

var newrowid = 100;

function getSelected() {
    var row = manager.getSelectedRow();
    if (!row)
    {
        alert('Please select a row');
        return;
    }

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
                            // h += "<a href='javascript:addNewRow()' title='Insert row' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                            h += "<a href='javascript:deleteRow()' title='Delete row' style='float:left;'><div class='ui-icon ui-icon-trash'></div></a> ";
                            //h += "<a href='javascript:f_selectContact()' title='Select BOM' style='float:right;'><div class='ui-icon ui-icon-search'></div></a> ";
                        }
                        else {
                            //                        h += "<a href='javascript:endEdit(" + rowindex + ")'>Submit</a> ";
                            //                        h += "<a href='javascript:cancelEdit(" + rowindex + ")'>Cancel</a> "; 
                        }
                        return h;
                    }
                },
                { display: 'Item Code', name: 'code', width: 100, align: 'center' },
                {
                    display: 'Item Name', name: 'goodsName', width: 250, align: 'left',

                    totalSummary:
                    {
                        type: 'count',
                        render: function (e) {
                            return 'Total：';
                        }
                    }
                },
                { display: 'Specification model', name: 'spec', width: 100, align: 'center' },
                { display: 'Unit', name: 'unitName', width: 70, align: 'center' },
                {
                    display: 'Standard Usage', name: 'numBom', width: 80, type: 'float', align: 'right',

                    totalSummary:
                    {
                        align: 'right',
                        type: 'sum',
                        render: function (e) {
                            return Math.round(e.sum * 100) / 100;
                        }
                    }
                },
                { display: 'Loss Rate', name: 'rate', width: 60, align: 'right', type: 'float' },
                {
                    display: 'Planned Usage', name: 'num', width: 80, align: 'right', type: 'float', editor: { type: 'float' },
                    totalSummary:
                    {
                        align: 'right',
                        type: 'sum',
                        render: function (e) {
                            var itemSumPrice = e.sum;
                            return "<span id='sumPriceItem'>" + Math.round(itemSumPrice * 10000) / 10000 + "</span>"; //formatCurrency(suminf.sum)
                        }
                    }
                },
                { display: 'Remarks', name: 'remarks', width: 220, align: 'left', type: 'text', editor: { type: 'text' } }
            ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '350',
            url: "produceListAdd.aspx?Action=GetData",
            rownumbers: true,
            frozenRownumbers: true,
            dataAction: 'local',
            usePager: false,
            alternatingRow: false,
            totalSummary: false,
            enabledEdit: true,
            onAfterEdit: f_onAfterEdit
        });
});

function save() {
    var hfGoodsId = $("#hfGoodsId").val();
    if (hfGoodsId == "" || hfGoodsId == 0) {
        $.ligerDialog.warn("Please select the goods to produce！");
        return;
    }

    var txtNum = $("#txtNum").val();
    if (txtNum == "" || txtNum == 0) {
        $.ligerDialog.warn("Please enter the quantity to produce！");
        return;
    }

    var data = manager.getData();
    for (var i = data.length - 1; i >= 0; i--) {
        if (data[i].goodsId == 0 || data[i].goodsName == "" || data[i].goodsId == "") {
            data.splice(i, 1);
        }
    }

    var isProcess = false;
    if (data.length == 0) {
        if (confirm("Confirm not to add material usage plan？")) {
            isProcess = false;
        }
        else {
            isProcess = true;

        }
    }

    if (isProcess) {
        if (data.length == 0) {
            $.ligerDialog.warn("Please select BOM！");

            return;
        }
        for (var i = 0; i < data.length; i++) {
            if (data[i].num <= 0 || data[i].num == "") {
                $.ligerDialog.warn("Please enter planned usage of " + (i + 1) + "th row！");

                return;
            }
        }
    }

    var dateStart = $("#txtDateStart").val();
    if (dateStart == "") {
        $.ligerDialog.warn("Please select the start date！");
        return;
    }

    var dateEnd = $("#txtDateEnd").val();
    if (dateStart == "") {
        $.ligerDialog.warn("Please select the end date！");
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

    // alert(JSON.stringify(headJson)); // for testing purpose, can be removed if deemed unnecessary

    var dataNew = [];

    dataNew.push(headJson);

    var list = JSON.stringify(headJson);

    list = list.substring(0, list.length - 1);// remove the last paranthesis
    list += ",\"Rows\":";
    list += JSON.stringify(data);
    list += "}";

    var postData = JSON.parse(list);// final json data

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
        url: 'ashx/produceListAdd.ashx',
        contentType: "application/json",
        //dataType: "json", // only to indicate return type, not a must
        data: JSON.stringify(postData),  //example value //data: "{'str1':'foovalue', 'str2':'barvalue'}",
        success: function (jsonResult) {
            if (jsonResult == "Created succesfully！") {
                $.ligerDialog.waitting('Created succesfully！'); setTimeout(function () { $.ligerDialog.closeWaitting(); location.reload(); }, 2000);
            }
            else {
                $.ligerDialog.warn(jsonResult);
            }
        },
        error: function (xhr) {
            alert("An error occured，please try again later:" + xhr.responseText);
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
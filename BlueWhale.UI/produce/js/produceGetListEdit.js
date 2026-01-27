var data = [{
    UnitPrice: 10,
    Quantity: 2,
    Price: 20
}];

function formatCurrency(x) {
    var f_x = parseFloat(x);
    if (isNaN(f_x)) {
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
    if (event.keyCode == 13 || event.keyCode == 39 || event.keyCode == 9) { // Enter, Right Arrow, Tab
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
    f_onAfterEdit(); // Recalculate
}

// New style row introduction
function f_selectContact() {
    $.ligerDialog.open({
        title: 'Select Process',
        name: 'winselector',
        width: 840,
        height: 540,
        url: '../baseSet/ProcessListSelect.aspx',
        buttons: [
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

// Order section start
$(function () {
    $("#txtOrderNumber").ligerComboBox({
        onBeforeOpen: f_selectOrder,
        valueFieldID: 'hfOrderNumber',
        width: 300
    });
});

function f_selectOrder() {
    $.ligerDialog.open({
        title: 'Select Production Plan',
        name: 'winselector',
        width: 950,
        height: 600,
        url: 'produceListSelect.aspx',
        buttons: [
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
    manager._setUrl("produceGetListEdit.aspx?Action=GetDataBom&pId=" + pId + "&goodsId=" + goodsId + "&num=" + num);
    dialog.close();
}

function f_selectOrderCancel(item, dialog) {
    dialog.close();
}
// Order section end

// Goods section start
$(function () {
    // $("#txtGoodsName").ligerComboBox({
    //     onBeforeOpen: f_selectGoods, valueFieldID: 'hfGoodsId', width: 300
    // });
});

function f_selectGoods() {
    $.ligerDialog.open({
        title: 'Select Product',
        name: 'winselector',
        width: 800,
        height: 540,
        url: '../baseSet/GoodsListSelect.aspx',
        buttons: [
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

    // Start - Add Bom details
    var goodsId = data[0].id;
    manager.changePage("first");
    manager._setUrl("produceGetListEdit.aspx?Action=GetDataBom&goodsId=" + goodsId + "&num=1");
    // End

    dialog.close();
}

function f_selectGoodsCancel(item, dialog) {
    dialog.close();
}
// Goods section end

// Product change event: Get unit, price, etc.
function f_onGoodsChanged(e) {
    if (!e || !e.length) return;

    // 1. First update subsequent data of the current row
    var grid = liger.get("maingrid");
    var selected = e[0]; // e.data[0];
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

    if (e.length > 1) { // If there are multiple rows, delete blank rows first, then insert below
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
// New style row introduction end

var managerProcess;
$(function () {
    var form = $("#form").ligerForm();
    var planNumber = $("#hfOrderNumber").val();
    $("#txtOrderNumber").val(planNumber);

    var txtGoodsName = $.ligerui.get("txtGoodsName");
    txtGoodsName.set("Width", 250);

    var txtOrderNumber = $.ligerui.get("txtOrderNumber");
    txtOrderNumber.set("Width", 250);

    var txtNum = $.ligerui.get("txtNum");
    txtNum.set("Width", 250);

    window['g'] = managerProcess = $("#maingrid9999").ligerGrid({
        columns: [
            {
                display: '',
                isSort: false,
                width: 60,
                align: 'center',
                frozen: true,
                render: function (rowdata, rowindex, value) {
                    var h = "";
                    if (!rowdata._editing) {
                        h += "<a href='javascript:addNewRow()' title='Add Row' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                        h += "<a href='javascript:deleteRow()' title='Delete Row' style='float:left;'><div class='ui-icon ui-icon-trash'></div></a> ";
                        h += "<a href='javascript:f_selectContact()' title='Select Process' style='float:right;'><div class='ui-icon ui-icon-search'></div></a> ";
                    }
                    return h;
                }
            },
            {
                display: 'Process Name',
                name: 'processName',
                width: 250,
                align: 'left',
                totalSummary: {
                    type: 'count',
                    render: function (e) {
                        return 'Total:';
                    }
                }
            },
            { display: 'Unit', name: 'unitName', width: 100, align: 'center' },
            {
                display: 'Process Quantity Per Item',
                name: 'num',
                width: 120,
                type: 'float',
                align: 'right',
                editor: { type: 'float' },
                totalSummary: {
                    align: 'right',
                    type: 'sum',
                    render: function (e) {
                        return Math.round(e.sum * 100) / 100;
                    }
                }
            },
            { display: 'Process Unit Price', name: 'price', width: 120, align: 'right', type: 'float', editor: { type: 'float' } },
            {
                display: 'Process Amount Per Item',
                name: 'sumPrice',
                width: 120,
                align: 'right',
                type: 'float',
                totalSummary: {
                    align: 'right',
                    type: 'sum',
                    render: function (e) {
                        var itemSumPrice = e.sum;
                        return "<span id='sumPriceItem'>" + Math.round(itemSumPrice * 10000) / 10000 + "</span>";
                    }
                }
            },
            { display: 'Remarks', name: 'remarks', width: 220, align: 'left', type: 'text', editor: { type: 'text' } }
        ],
        width: '99%',
        pageSizeOptions: [5, 10, 15, 20],
        height: '350',
        url: 'produceGetListAdd.aspx?Action=GetData',
        rownumbers: true, // Show sequence numbers
        frozenRownumbers: true, // Row numbers in frozen column
        dataAction: 'local', // Local sorting
        usePager: false,
        alternatingRow: false,
        totalSummary: false,
        enabledEdit: true, // Control edit ability
        onAfterEdit: f_onAfterEdit // Operations after cell update
    });
});

// Edit event - Calculate amount
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

    if (e.column.name == "num") { // Quantity changed - start
        num = Number(e.value);
        num = Math.round(num * 100) / 100;
        price = Math.round(price * 100) / 100;
        sumPrice = Math.round(num * price * 100) / 100;

        manager.updateCell("num", num, e.record);
        manager.updateCell('sumPrice', sumPrice, e.record);
    } // Quantity changed - end

    if (e.column.name == "price") { // Unit price changed - start, calculate amount, discount, tax, total
        price = Number(e.value);
        sumPrice = Number(num) * Number(price);
        num = Math.round(num * 100) / 100;
        price = Math.round(price * 100) / 100;
        sumPrice = Math.round(sumPrice * 100) / 100;

        manager.updateCell("price", price, e.record);
        manager.updateCell('sumPrice', sumPrice, e.record);
    } // Unit price changed - end

    // Finally change the value of the summary row
    updateTotal();
}

function updateTotal() {
    var data = manager.getData(); // getData
    var sumPriceItem = 0;

    // 1. First delete blank rows
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
        $.ligerDialog.warn('At least one row must be retained!');
    } else {
        manager.deleteSelectedRow();
    }
}

var newrowid = 100;

function getSelected() {
    var row = manager.getSelectedRow();
    if (!row) {
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
    window['gBom'] = manager = $("#maingrid").ligerGrid({
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
                        h += "<a href='javascript:deleteRow()' title='Delete Row' style='text-align :center;'><div class='ui-icon ui-icon-trash'></div></a> ";
                    }
                    return h;
                }
            },
            { display: 'Product Code', name: 'code', width: 100, align: 'center' },
            {
                display: 'Product Name',
                name: 'goodsName',
                width: 250,
                align: 'left',
                totalSummary: {
                    type: 'count',
                    render: function (e) {
                        return 'Total:';
                    }
                }
            },
            { display: 'Specification', name: 'spec', width: 100, align: 'center' },
            { display: 'Unit', name: 'unitName', width: 70, align: 'center' },
            {
                display: 'Receiving Warehouse',
                name: 'ckId',
                width: 80,
                isSort: false,
                textField: 'ckName',
                editor: {
                    type: 'select',
                    url: "../baseSet/InventoryList.aspx?Action=GetDDLList&r=" + Math.random(),
                    valueField: 'ckId',
                    textField: 'ckName'
                }
            },
            {
                display: 'Requested Quantity',
                name: 'numApply',
                width: 80,
                type: 'float',
                align: 'right',
                totalSummary: {
                    align: 'right',
                    type: 'sum',
                    render: function (e) {
                        return Math.round(e.sum * 100) / 100;
                    }
                }
            },
            {
                display: 'Actual Issued Quantity',
                name: 'num',
                width: 80,
                align: 'right',
                type: 'float',
                editor: { type: 'float' },
                totalSummary: {
                    align: 'right',
                    type: 'sum',
                    render: function (e) {
                        var itemSumPrice = e.sum;
                        return "<span id='sumNumItem'>" + Math.round(itemSumPrice * 10000) / 10000 + "</span>";
                    }
                }
            },
            { display: 'Unit Price', name: 'price', width: 70, type: 'float', align: 'right', editor: { type: 'float' } },
            {
                display: 'Amount',
                name: 'sumPrice',
                width: 80,
                type: 'float',
                align: 'right',
                totalSummary: {
                    align: 'center',
                    type: 'sum',
                    render: function (e) {
                        var itemSumPriceNow = e.sum;
                        return "<span id='sumPriceItem'>" + Math.round(itemSumPriceNow * 10000) / 10000 + "</span>";
                    }
                }
            },
            { display: 'Remarks', name: 'remarks', width: 220, align: 'left', type: 'text', editor: { type: 'text' } }
        ],
        width: '99%',
        pageSizeOptions: [5, 10, 15, 20],
        height: '350',
        url: "produceGetListEdit.aspx?Action=GetData&pId=" + param,
        rownumbers: true, // Show sequence numbers
        frozenRownumbers: true, // Row numbers in frozen column
        dataAction: 'local', // Local sorting
        usePager: false,
        alternatingRow: false,
        totalSummary: false,
        enabledEdit: true, // Control edit ability
        onAfterEdit: f_onAfterEdit // Operations after cell update
    });
});

function save() {
    var hfPId = $("#hfPId").val();
    if (hfPId == "" || hfPId == 0) {
        $.ligerDialog.warn("Please select a production plan!");
        return;
    }

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

    // First delete blank rows
    var data = manager.getData();

    // 1. Delete blank rows
    for (var i = data.length - 1; i >= 0; i--) {
        if (data[i].goodsId == 0 || data[i].goodsName == "" || data[i].goodsId == "") {
            data.splice(i, 1);
        }
    }

    var isGongxu = false;

    // 2. Check if products are selected
    if (data.length == 0) {
        $.ligerDialog.warn("Please select materials to be issued!");
        return;
    }

    for (var i = 0; i < data.length; i++) {
        if (data[i].ckId == 0 || data[i].ckId == "" || data[i].ckId == "0" || data[i].ckName == "") {
            $.ligerDialog.warn("Please enter the warehouse for row " + (i + 1) + "!");
            return;
        }

        if (data[i].num <= 0 || data[i].num == "") {
            $.ligerDialog.warn("Please enter the issued quantity for row " + (i + 1) + "!");
            return;
        }
    }

    var bizId = $("#ddlYWYList").val();
    var bizDate = $("#txtBizDate").val();
    if (bizDate == "") {
        $.ligerDialog.warn("Please enter the material issuing date!");
        return;
    }

    var orderNumber = $("#txtOrderNumber").val();
    var remarks = $("#txtRemarks").val();

    var headJson = {
        id: param,
        bizId: bizId,
        bizDate: bizDate,
        planNumber: orderNumber,
        goodsId: hfGoodsId,
        num: txtNum,
        remarks: remarks
    };

    var dataNew = [];
    dataNew.push(headJson);
    var list = JSON.stringify(headJson); // Serialize to string, header
    list = list.substring(0, list.length - 1); // Remove last curly brace
    list += ",\"Rows\":";
    list += JSON.stringify(data); // Insert account information
    list += "}";

    var postData = JSON.parse(list); // Final JSON

    $.ajax({
        type: "POST",
        url: 'ashx/produceGetListEdit.ashx',
        contentType: "application/json", // Required
        data: JSON.stringify(postData),
        success: function (jsonResult) {
            if (jsonResult == "Operation successful!") {
                $.ligerDialog.waitting('Operation successful!');
                setTimeout(function () {
                    $.ligerDialog.closeWaitting();
                    location.reload();
                }, 2000);
            } else {
                $.ligerDialog.warn(jsonResult);
            }
        },
        error: function (xhr) {
            alert("An error occurred, please try again later:" + xhr.responseText);
        }
    });
}

function getBomList() {
    var pId = $("#hfPId").val(); // Production plan ID
    var goodsId = $("#hfGoodsId").val();
    var num = $("#txtNum").val();

    if (goodsId > 0 && num > 0 && pId > 0) {
        manager.changePage("first");
        manager._setUrl("produceGetListEdit.aspx?Action=GetDataBom&pId=" + pId + "&goodsId=" + goodsId + "&num=" + num);
    }
}

var param = getUrlParam("pId");

function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]);
    return null;
}
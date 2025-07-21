
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
    if (event.keyCode == 13 || event.keyCode == 39 || event.keyCode == 9) {
        manager.endEditToNext();
    }
});


function f_selectContact() {
    $.ligerDialog.open({
        title: 'Select product', name: 'winselector', width: 840, height: 540, url: '../baseSet/GoodsListSelect.aspx', buttons: [
            { text: 'Confirm', onclick: f_selectContactOK },
            { text: 'Cancel', onclick: f_selectContactCancel }
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

function f_selectClient() {
    $.ligerDialog.open({
        title: 'Select customer', name: 'winselector', width: 800, height: 540, url: '../baseSet/ClientListSelect.aspx', buttons: [
            { text: 'Confirm', onclick: f_selectClientOK },
            { text: 'Close', onclick: f_selectClientCancel }
        ]
    });
    return false;
}
function f_selectClientOK(item, dialog) {
    var fn = dialog.frame.f_select || dialog.frame.window.f_select;
    var data = fn();
    if (!data) {
        alert('Please select row!');
        return;
    }

    $("#clientName").val(data.names);
    $("#clientId").val(data.id);
    dialog.close();
}

function f_selectClientCancel(item, dialog) {
    dialog.close();
}

$.ligerDefaults.Grid.formatters['numberbox'] = function (value, column) {
    var precision = column.editor.precision;
    return value.toFixed(precision);
};

$(function () {
    $("#clientName").ligerComboBox({
        onBeforeOpen: f_selectClient, valueFieldID: 'clientId', width: 250
    });
});

var manager;
$(function () {

    var form = $("#form").ligerForm();
    var form1 = $("#tbFooter").ligerForm();
    var form2 = $("#form22").ligerForm();

    var g = $.ligerui.get("clientName");
    g.set("Width", 250);
    window['g'] =
        manager = $("#maingrid").ligerGrid({
            columns: [
                {
                    display: '', isSort: false, width: 60, height: 50, align: 'center', frozen: true, render: function (rowdata, rowindex, value) {
                        var h = "";
                        if (!rowdata._editing) {
                            h += "<a href='javascript:addNewRow()' title='Add row' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                            h += "<a href='javascript:deleteRow()' title='Delete row' style='float:left;'><div class='ui-icon ui-icon-trash'></div></a> ";
                            h += "<a href='javascript:f_selectContact()' title='Select product' style='float:left;'><div class='ui-icon ui-icon-search'></div></a> ";
                        }
                        return h;
                    }
                },
                {
                    display: 'Item Name', name: 'goodsName', width: 200, align: 'left',
                    totalSummary:
                    {
                        type: 'count',
                        render: function (e) {
                            return 'Total: ';
                        }
                    }
                },
                { display: 'Specification', name: 'spec', width: 100, align: 'center' },
                { display: 'Unit', name: 'unitName', width: 60, align: 'center' },
                {
                    display: 'Inventory', name: 'ckId', width: 80, isSort: false, textField: 'ckName',
                    editor: {
                        type: 'select',
                        url: "../baseSet/InventoryList.aspx?Action=GetDDLList&r=" + Math.random(),
                        valueField: 'ckId', textField: 'ckName'
                    }
                },
                {
                    display: 'Amount', name: 'num', width: 70, type: 'float', align: 'right', editor: { type: 'float' },

                    totalSummary:
                    {
                        align: 'right',
                        type: 'sum',
                        render: function (e) {
                            return Math.round(e.sum * 100) / 100;
                        }
                    }
                },
                {
                    display: 'Original price', name: 'price', width: 110, type: 'float', align: 'right', editor: { type: 'float' }

                },
                {
                    display: 'Discount%', name: 'dis', width: 80, type: 'float', align: 'right', editor: { type: 'float' }

                },
                {
                    display: 'Discount Amount', name: 'sumPriceDis', width: 150, type: 'float', align: 'right', editor: { type: 'float' },
                    totalSummary:
                    {
                        align: 'center',
                        type: 'sum',
                        render: function (e) {
                            var itemSumPriceDis = e.sum;
                            return "<span id='sumPriceItemDis'>" + Math.round(itemSumPriceDis * 10000) / 10000 + "</span>";
                        }
                    }
                },
                {
                    display: 'Current Price', name: 'priceNow', width: 100, type: 'float', align: 'right', editor: { type: 'float' }
                },
                {
                    display: 'Amount', name: 'sumPriceNow', width: 80, type: 'float', align: 'right', editor: { type: 'float' },

                    totalSummary:
                    {
                        align: 'center',
                        type: 'sum',
                        render: function (e) {
                            var itemSumPriceNow = e.sum;
                            return "<span id='sumPriceItemNow'>" + Math.round(itemSumPriceNow * 10000) / 10000 + "</span>";
                        }
                    }

                },
                { display: 'Tax amount%', name: 'tax', width: 100, type: 'int', align: 'center', editor: { type: 'int' } },
                { display: 'Unit Price Including Tax', name: 'priceTax', width: 180, type: 'float', align: 'center', editor: { type: 'float' } },
                {
                    display: 'Tax amount', name: 'sumPriceTax', width: 80, type: 'float', align: 'right',

                    totalSummary:
                    {
                        align: 'center',
                        type: 'sum',
                        render: function (e) {
                            var itemSumPriceTax = e.sum;
                            return "<span id='sumPriceItemTax'>" + Math.round(itemSumPriceTax * 10000) / 10000 + "</span>";
                        }
                    }
                },
                {
                    display: 'Total Price Including Tax', name: 'sumPriceAll', width: 170, type: 'float', align: 'right', editor: { type: 'float' },
                    totalSummary:
                    {
                        align: 'center',
                        type: 'sum',
                        render: function (e) {
                            var itemSumPriceAll = e.sum;
                            return "<span id='sumPriceItemAll'>" + Math.round(itemSumPriceAll * 10000) / 10000 + "</span>";
                        }
                    }

                },

                { display: 'Remarks', name: 'remarks', width: 140, align: 'left', type: 'text', editor: { type: 'text' } }

            ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '350',
            url: 'SalesOrderListAdd.aspx?Action=GetData&id=' + param,
            rownumbers: true,
            frozenRownumbers: true,
            dataAction: 'local',
            usePager: false,
            alternatingRow: false,
            totalSummary: true,
            enabledEdit: true,
            onAfterEdit: f_onAfterEdit
        }
        );
});

var rowNumber = 9;
var itemSumPriceAll = 0;

function addNewRow() {
    var gridData = manager.getData();
    var rowNum = gridData.length;

    manager.addRow({
        id: rowNum + 1,
        id: rowNum + 1,
        goodsId: "",
        goodsName: "",
        spec: "",
        unitName: "",
        num: "",
        price: "",
        dis: "",
        sumPriceDis: "",
        priceNow: "",
        sumPriceNow: "",
        tax: "",
        priceTax: "",
        sumPriceTax: "",
        sumPriceAll: "",
        ckId: "",
        ckName: "",
        remarks: "",
        sourceNumber: "",
        itemId: 0
    });

    updateTotal();
}

function f_onGoodsChanged(e) {
    if (!e || !e.length) return;

    var grid = liger.get("maingrid");

    var selected = e[0];

    var selectedRow = manager.getSelected();

    grid.updateRow(selectedRow, {
        goodsId: selected.id,
        goodsName: selected.names,
        spec: selected.spec,
        unitName: selected.unitName,
        num: 1,
        price: selected.priceSalesRetail,
        dis: 0,
        sumPriceDis: 0,
        priceNow: selected.priceSalesRetail,
        sumPriceNow: selected.priceSalesRetail,
        tax: 0,
        priceTax: selected.priceSalesRetail,
        sumPriceTax: 0,
        sumPriceAll: selected.priceSalesRetail,
        ckId: selected.ckId,
        ckName: selected.ckName,
        sourceNumber: "",
        itemId: 0,
        remarks: ""
    });

    if (e.length > 1) {
        var data = manager.getData();
        for (var i = data.length - 1; i >= 0; i--) {
            if (data[i].goodsId == 0 || data[i].goodsName == "") {
                manager.deleteRow(i);
            }
        }

        for (var i = 1; i < e.length; i++) {
            grid.addRow({
                id: rowNumber,
                goodsId: e[i].id,
                goodsName: e[i].names,
                spec: e[i].spec,
                unitName: e[i].unitName,

                num: 1,
                price: e[i].priceSalesRetail,
                dis: 0,
                sumPriceDis: 0,
                priceNow: e[i].priceSalesRetail,
                sumPriceNow: e[i].priceSalesRetail,
                tax: 0,
                priceTax: e[i].priceSalesRetail,
                sumPriceTax: 0,
                sumPriceAll: e[i].priceSalesRetail,

                ckId: e[i].ckId,
                ckName: e[i].ckName,
                sourceNumber: "",
                itemId: 0,
                remarks: ""
            });

            rowNumber = rowNumber + 1;
        }
    }
    updateTotal();
}

function updateTotal() {
    var data = manager.getData();
    var sumPriceItemDis = 0;
    var sumPriceItemNow = 0;
    var sumPriceItemTax = 0;
    var sumPriceItemAll = 0;

    for (var i = data.length - 1; i >= 0; i--) {
        if (data[i].goodsId == 0 || data[i].goodsId == "" || data[i].goodsName == "") {
            data.splice(i, 1);
        }
    }

    for (var i = 0; i < data.length; i++) {

        sumPriceItemDis += Number(data[i].sumPriceDis);

        sumPriceItemNow += Number(data[i].sumPriceNow);

        sumPriceItemTax += Number(data[i].sumPriceTax);

        sumPriceItemAll += Number(data[i].sumPriceAll);
    }

    $("#sumPriceItemDis").html(formatCurrency(sumPriceItemDis));

    $("#sumPriceItemNow").html(formatCurrency(sumPriceItemNow));

    $("#sumPriceItemTax").html(formatCurrency(sumPriceItemTax));

    $("#sumPriceItemAll").html(formatCurrency(sumPriceItemAll));
}
function f_onAfterEdit(e) {

    var num, price, dis, sumPriceDis, priceNow, sumPriceNow, tax, priceTax, sumPriceTax, sumPriceAll;

    num = Number(e.record.num);
    price = Number(e.record.price);
    dis = Number(e.record.dis);
    sumPriceDis = Number(e.record.sumPriceDis);
    priceNow = Number(e.record.priceNow);
    sumPriceNow = Number(e.record.sumPriceNow);
    tax = Number(e.record.tax);
    priceTax = Number(e.record.priceTax);
    sumPriceTax = Number(e.record.sumPriceTax);
    sumPriceAll = Number(e.record.sumPriceAll);

    var goodsId, goodsName;

    goodsId = e.record.goodsId;
    goodsName = e.record.goodsName;

    if (goodsId == "" || goodsName == "") {
        return;
    }

    if (e.column.name == "num") {
        num = Number(e.value);
        sumPriceDis = Number(num) * Number(price) * Number(dis) / 100;
        sumPriceDis = Math.round(sumPriceDis * 10000) / 10000;
        sumPriceNow = Number(num) * Number(priceNow);
        sumPriceNow = Math.round(sumPriceNow * 10000) / 10000;
        sumPriceTax = Number(num) * Number(priceNow) * Number(tax) / 100;
        sumPriceTax = Math.round(sumPriceTax * 10000) / 10000;
        sumPriceAll = Number(num) * Number(priceTax);
        sumPriceAll = Math.round(sumPriceAll * 10000) / 10000;
        manager.updateCell("num", num, e.record);
        manager.updateCell('sumPriceDis', sumPriceDis.toString(), e.record);
        manager.updateCell('sumPriceNow', sumPriceNow.toString(), e.record);
        manager.updateCell('sumPriceTax', sumPriceTax.toString(), e.record);
        manager.updateCell('sumPriceAll', sumPriceAll.toString(), e.record);
    }

    if (e.column.name == "price") {
        price = Number(e.value);
        sumPriceDis = num * price * dis / 100;
        priceNow = price * (1 + dis / 100);
        sumPriceNow = num * priceNow;
        sumPriceNow = Math.round(sumPriceNow * 10000) / 10000;
        priceTax = priceNow * (1 + tax / 100);
        sumPriceTax = num * priceNow * tax / 100;
        sumPriceAll = num * priceTax;
        sumPriceDis = Math.round(sumPriceDis * 10000) / 10000;
        sumPriceNow = Math.round(sumPriceNow * 10000) / 10000;
        sumPriceTax = Math.round(sumPriceTax * 10000) / 10000;
        sumPriceAll = Math.round(sumPriceAll * 10000) / 10000;
        manager.updateCell("price", price, e.record);
        manager.updateCell("priceNow", priceNow, e.record);
        manager.updateCell("priceTax", priceTax, e.record);
        manager.updateCell('sumPriceDis', sumPriceDis.toString(), e.record);
        manager.updateCell('sumPriceNow', sumPriceNow.toString(), e.record);
        manager.updateCell('sumPriceTax', sumPriceTax.toString(), e.record);
        manager.updateCell('sumPriceAll', sumPriceAll.toString(), e.record);
    }

    if (e.column.name == "dis") {
        dis = Number(e.value);

        if (dis != 0) {
            priceNow = price * (dis / 100);
            sumPriceDis = num * price * (1 - dis / 100);
        }
        else {
            priceNow = price;
            sumPriceDis = 0;
        }

        sumPriceNow = num * priceNow;
        sumPriceNow = Math.round(sumPriceNow * 10000) / 10000;
        priceTax = priceNow * (1 + tax / 100);
        sumPriceTax = num * priceNow * tax / 100;
        sumPriceAll = num * priceTax;
        sumPriceDis = Math.round(sumPriceDis * 10000) / 10000;
        sumPriceNow = Math.round(sumPriceNow * 10000) / 10000;
        sumPriceTax = Math.round(sumPriceTax * 10000) / 10000;
        sumPriceAll = Math.round(sumPriceAll * 10000) / 10000;
        manager.updateCell("dis", dis, e.record);
        manager.updateCell("priceNow", priceNow, e.record);
        manager.updateCell("priceTax", priceTax, e.record);
        manager.updateCell('sumPriceDis', sumPriceDis.toString(), e.record);
        manager.updateCell('sumPriceNow', sumPriceNow.toString(), e.record);
        manager.updateCell('sumPriceTax', sumPriceTax.toString(), e.record);
        manager.updateCell('sumPriceAll', sumPriceAll.toString(), e.record);
    }

    if (e.column.name == "sumPriceDis") {
        sumPriceDis = Number(e.value);

        if (sumPriceDis >= num * price) {
            alert("Please fill in the correct discount amount!");
            return;
        }

        if (num * price != 0) {
            dis = (1 - sumPriceDis / (num * price)) * 100;
        }
        else {
            alert("Please fill in the quantity and unit price!");
            return;
        }

        priceNow = price * dis / 100;
        sumPriceNow = num * priceNow;
        sumPriceNow = Math.round(sumPriceNow * 10000) / 10000;
        priceTax = priceNow * (1 + tax / 100);
        sumPriceTax = num * priceNow * tax / 100;
        sumPriceAll = priceTax * num;
        sumPriceAll = Math.round(sumPriceAll * 10000) / 10000;
        sumPriceDis = Math.round(sumPriceDis * 10000) / 10000;
        sumPriceNow = Math.round(sumPriceNow * 10000) / 10000;
        sumPriceTax = Math.round(sumPriceTax * 10000) / 10000;
        sumPriceAll = Math.round(sumPriceAll * 10000) / 10000;
        manager.updateCell("dis", dis, e.record);
        manager.updateCell("priceNow", priceNow, e.record);
        manager.updateCell("priceTax", priceTax, e.record);
        manager.updateCell('sumPriceDis', sumPriceDis.toString(), e.record);
        manager.updateCell('sumPriceNow', sumPriceNow.toString(), e.record);
        manager.updateCell('sumPriceTax', sumPriceTax.toString(), e.record);
        manager.updateCell('sumPriceAll', sumPriceAll.toString(), e.record);
    }

    if (e.column.name == "priceNow") {
        priceNow = Number(e.value);

        if (price != 0) {
            dis = priceNow / price * 100;
        }
        else {
            price = priceNow;
            dis = 0;
        }

        sumPriceDis = num * (price - priceNow);
        sumPriceNow = num * priceNow;
        sumPriceNow = Math.round(sumPriceNow * 10000) / 10000;
        priceTax = priceNow * (1 + tax / 100);
        sumPriceTax = num * priceNow * tax / 100;
        sumPriceAll = priceTax * num;
        sumPriceDis = Math.round(sumPriceDis * 10000) / 10000;
        sumPriceNow = Math.round(sumPriceNow * 10000) / 10000;
        sumPriceTax = Math.round(sumPriceTax * 10000) / 10000;
        sumPriceAll = Math.round(sumPriceAll * 10000) / 10000;
        manager.updateCell("price", price, e.record);
        manager.updateCell("dis", dis, e.record);
        manager.updateCell("priceNow", priceNow, e.record);
        manager.updateCell("priceTax", priceTax, e.record);
        manager.updateCell('sumPriceDis', sumPriceDis.toString(), e.record);
        manager.updateCell('sumPriceNow', sumPriceNow.toString(), e.record);
        manager.updateCell('sumPriceTax', sumPriceTax.toString(), e.record);
        manager.updateCell('sumPriceAll', sumPriceAll.toString(), e.record);
    }

    if (e.column.name == "sumPriceNow") {
        sumPriceNow = Number(e.value);

        if (num != 0) {
            priceNow = (sumPriceNow) / num;
        }
        else {
            num = 1;
            priceNow = sumPriceNow;
        }

        sumPriceDis = num * (price - priceNow);

        if (price != 0) {
            dis = priceNow / price * 100;
        }
        else {
            price = priceNow;
            dis = 0;
        }

        priceTax = tax * priceNow / 100;
        if (tax == 0) {
            priceTax = priceNow;
        }

        price = Math.round(price * 10000) / 10000;
        priceTax = Math.round(priceTax * 10000) / 10000;
        sumPriceNow = Math.round(sumPriceNow * 10000) / 10000;
        sumPriceTax = Number(num) * Number(price) * Number(tax) / 100;
        sumPriceTax = Math.round(sumPriceTax * 10000) / 10000;
        sumPriceAll = Number(num) * Number(priceTax);
        sumPriceAll = Math.round(sumPriceAll * 10000) / 10000;
        manager.updateCell("price", price, e.record);
        manager.updateCell("dis", dis, e.record);
        manager.updateCell("priceNow", priceNow, e.record);
        manager.updateCell("priceTax", priceTax, e.record);
        manager.updateCell('sumPriceDis', sumPriceDis, e.record);
        manager.updateCell('sumPriceNow', sumPriceNow, e.record);
        manager.updateCell('sumPriceTax', sumPriceTax, e.record);
        manager.updateCell('sumPriceAll', sumPriceAll, e.record);
    }

    if (e.column.name == "tax") {
        tax = Number(e.value);
        priceTax = priceNow * (1 + tax / 100);
        if (tax == 0) {
            priceTax = priceNow;
        }

        priceTax = Math.round(priceTax * 10000) / 10000;
        sumPriceTax = Number(num) * Number(priceNow) * Number(tax) / 100;
        sumPriceTax = Math.round(sumPriceTax * 10000) / 10000;
        sumPriceAll = Number(num) * Number(priceTax);
        sumPriceAll = Math.round(sumPriceAll * 10000) / 10000;
        manager.updateCell("priceTax", priceTax, e.record);
        manager.updateCell('sumPriceTax', sumPriceTax.toString(), e.record);
        manager.updateCell('sumPriceAll', sumPriceAll.toString(), e.record);
    }

    if (e.column.name == "priceTax") {
        priceTax = Number(e.value);
        priceNow = priceTax / (1 + tax / 100);
        priceNow = Math.round(priceNow * 10000) / 10000;
        priceTax = Math.round(priceTax * 10000) / 10000;
        dis = priceNow / price * 100;
        if (dis == 100) {
            dis = 0;
        }
        dis = Math.round(dis * 10000) / 10000;
        sumPriceDis = num * (price - priceNow);
        sumPriceDis = Math.round(sumPriceDis * 10000) / 10000;
        sumPriceNow = num * priceNow;
        sumPriceNow = Math.round(sumPriceNow * 10000) / 10000;
        sumPriceTax = Number(num) * Number(price) * Number(tax) / 100;
        sumPriceTax = Math.round(sumPriceTax * 10000) / 10000;
        sumPriceAll = Number(num) * Number(priceTax);
        sumPriceAll = Math.round(sumPriceAll * 10000) / 10000;
        manager.updateCell("dis", dis, e.record);
        manager.updateCell("priceNow", priceNow, e.record);
        manager.updateCell("priceTax", priceTax, e.record);
        manager.updateCell('sumPriceDis', sumPriceDis, e.record);
        manager.updateCell('sumPriceNow', sumPriceNow, e.record);
        manager.updateCell('sumPriceTax', sumPriceTax.toString(), e.record);
        manager.updateCell('sumPriceAll', sumPriceAll.toString(), e.record);
    }

    if (e.column.name == "sumPriceAll") {
        sumPriceAll = Number(e.value);

        if (num != 0) {
            priceTax = sumPriceAll / num;
        }

        priceNow = priceTax / (1 + tax / 100);
        priceNow = Math.round(priceNow * 10000) / 10000;

        sumPriceNow = priceNow * num;
        sumPriceNow = Math.round(sumPriceNow * 10000) / 10000;


        dis = priceNow / price * 100;
        if (dis == 100) {
            dis = 0;
        }

        dis = Math.round(dis * 10000) / 10000;
        sumPriceDis = num * (price - priceNow);
        sumPriceDis = Math.round(sumPriceDis * 10000) / 10000;
        sumPriceTax = Number(num) * Number(price) * Number(tax) / 100;
        sumPriceTax = Math.round(sumPriceTax * 10000) / 10000;
        sumPriceAll = Number(num) * Number(priceTax);
        sumPriceAll = Math.round(sumPriceAll * 10000) / 10000;
        manager.updateCell("priceNow", priceNow, e.record);
        manager.updateCell("dis", dis, e.record);
        manager.updateCell("priceTax", priceTax, e.record);
        manager.updateCell('sumPriceDis', sumPriceDis, e.record);
        manager.updateCell('sumPriceNow', sumPriceNow, e.record);
        manager.updateCell('sumPriceTax', sumPriceTax, e.record);
        manager.updateCell('sumPriceAll', sumPriceAll, e.record);
    }

    updateTotal();
}

function f_onBeforeSubmitEdit(e) {
    if (e.column.name == "dis") {
        if (e.value < 0 || e.value > 100) return false;
    }

    return true;
}

function deleteRow() {
    if (manager.rows.length == 1) {
        $.ligerDialog.warn('At least keep one row!')
    }
    else {
        manager.deleteSelectedRow();
    }
}
var newrowid = 100;

function save() {

    var venderId = $("#clientId").val();
    var bizId = $("#ddlYWYList").val();

    if (venderId == 0) {
        $.ligerDialog.warn('Please select customer!');

        return;
    }

    if (bizId == 0) {
        $.ligerDialog.warn('Please select sales person!');

        return;
    }

    var bizDate = $("#txtBizDate").val();
    if (bizDate == "") {
        $.ligerDialog.warn("Please enter order date!");
        return;

    }

    var sendDate = $("#txtSendDate").val();
    if (sendDate == "") {
        $.ligerDialog.warn("Please enter delivery date!");
        return;

    }

    var remarks = $("#txtRemarks").val();

    var data = manager.getData();

    for (var i = data.length - 1; i >= 0; i--) {
        if (data[i].goodsId == 0 || data[i].goodsId == "" || data[i].goodsName == "") {
            data.splice(i, 1);

        }

    }

    if (data.length == 0) {
        $.ligerDialog.warn('Please select product!');

        return;
        alert("Execution skipped!");
    }

    for (var i = 0; i < data.length; i++) {
        if (data[i].num <= 0 || data[i].num == "" || data[i].num == "0" || data[i].num == "0.00") {

            $.ligerDialog.warn("Please enter the" + (i + 1) + "!");

            return;
            alert("Execution skipped!");
        }
    }

    var headJson = { venderId: venderId, bizDate: bizDate, sendDate: sendDate, bizId: bizId, remarks: remarks };

    var dataNew = [];
    dataNew.push(headJson);

    var list = JSON.stringify(headJson);

    list = list.substring(0, list.length - 1);

    list += ",\"Rows\":";
    list += JSON.stringify(data);
    list += "}";

    var postData = JSON.parse(list);

    $.ajax({
        type: "POST",
        url: 'ashx/SalesOrderListAdd.ashx',
        contentType: "application/json",
        data: JSON.stringify(postData),
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

var param = getUrlParam("id");

function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");

    var r = window.location.search.substr(1).match(reg);

    if (r != null) return unescape(r[2]); return null;
}

function getthedate(dd, dadd) {
    var a = new Date(dd)
    a = a.valueOf()
    a = a + dadd * 24 * 60 * 60 * 1000
    a = new Date(a);
    var m = a.getMonth() + 1;
    if (m.toString().length == 1) {
        m = '0' + m;
    }
    var d = a.getDate();
    if (d.toString().length == 1) {
        d = '0' + d;
    }
    return a.getFullYear() + "-" + m + "-" + d;
}
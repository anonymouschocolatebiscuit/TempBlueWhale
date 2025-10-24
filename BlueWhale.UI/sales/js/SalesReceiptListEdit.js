
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

//New version
function f_selectContact() {
    $.ligerDialog.Open({
        title: 'Select Goods', name: 'winselector', width: 840, height: 540, url: '../baseSet/GoodsListSelect.aspx', buttons: [
            { text: 'OK', onclick: f_selectContactOK },
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

//New style introduction line end

//Client starts
function f_selectClient() {
    $.ligerDialog.open({
        title: 'Select a client', name: 'winselector', width: 800, height: 540, url: '../baseSet/ClientListSelect.aspx', buttons: [
            { text: 'OK', onclick: f_selectClientOK },
            { text: 'Close', onclick: f_selectClientCancel }
        ]
    });
    return false;
}

function f_selectClientOK(item, dialog) {
    var fn = dialog.frame.f_select || dialog.frame.window.f_select;
    var data = fn();
    if (!data) {
        alert('Please select a row!');
        return;
    }

    $("#clientName").val(data.names);
    $("#clientId").val(data.id);

    dialog.close();
}

function f_selectClientCancel(item, dialog) {
    dialog.close();
}

//Client ends

//Extended numberbox type formatting function
$.ligerDefaults.Grid.formatters['numberbox'] = function (value, column) {
    var precision = column.editor.precision;
    return value.toFixed(precision);
};

$(function () {
    $("#clientName").ligerComboBox({
        onBeforeOpen: f_selectClient, valueFieldID: 'clientId', width: 300
    });
});

var manager;
$(function () {

    var form = $("#form").ligerForm();
    var form1 = $("#tbFooter").ligerForm();
    var form2 = $("#form22").ligerForm();

    var g = $.ligerui.get("clientName");
    g.set("Width", 250);

    var wlName = $("#txtClientName").val();
    $("#clientName").val(wlName);

    window['g'] =
        manager = $("#maingrid").ligerGrid({
            columns: [
                {
                    display: '', isSort: false, width: 60, align: 'center', frozen: true, render: function (rowdata, rowindex, value) {
                        var h = "";
                        if (!rowdata._editing) {
                            h += "<a href='javascript:addNewRow()' title='Add new row' style='float:left;'><div class='ui-icon ui-icon-plus'></div> </a> ";
                            h += "<a href='javascript:deleteRow()' title='Delete row' style='float:left;'><div class='ui-icon ui-icon-trash'></div>< /a> ";
                            h += "<a href='javascript:f_selectContact()' title='Select Products' style='float:left;'><div class='ui-icon ui-icon-search'></div>< /a> ";
                        }

                        return h;
                    }
                },
                {
                    display: 'Product Name', name: 'goodsName', width: 250, align: 'left',

                    totalSummary:
                    {
                        type: 'count',
                        render: function (e) { //Summary renderer, return HTML to load into cell
                            //e Summary Object (including sum, max, min, avg, count)
                            return 'Total:';
                        }
                    }
                },
                { display: 'Specification', name: 'spec', width: 100, align: 'center' },
                { display: 'Unit', name: 'unitName', width: 80, align: 'center' },
                {
                    display: 'warehouse', name: 'ckId', width: 80, isSort: false, textField: 'ckName',
                    editor:
                        type: 'select',
                    url: "../baseSet/InventoryList.aspx?Action=GetDDLList&r=" + Math.random(),
                    valueField: 'ckId', textField: 'ckName'
                }
                },
                        {
                            display: 'Quantity', name: 'num', width: 70, type: 'float', align: 'right', editor: { type: 'float' },

                            totalSummary:
                            {
                                align: 'right', //Alignment of summary cell contents: left/center/right
                                type: 'sum',
                                render: function (e) { //Summary renderer, return HTML to load into cell
                                    //e Summary Object (including sum, max, min, avg, count)
                                    return Math.round(e.sum * 100) / 100;
                                }
                            }
                        },
                        {
                            display: 'Original price', name: 'price', width: 70, type: 'float', align: 'right', editor: { type: 'float' }
                        },
                        {
                            display: 'Discount%', name: 'dis', width: 60, type: 'float', align: 'right', editor: { type: 'float' }
                        },
                        {
                            display: 'Discount amount', name: 'sumPriceDis', width: 70, type: 'float', align: 'right', editor: { type: 'float' },
                            totalSummary:
                            {
                                align: 'center', //Alignment of summary cell contents: left/center/right
                                type: 'sum',
                                render: function (e) { //Summary renderer, return HTML to load into cell

                                    var itemSumPriceDis = e.sum;
                                    return "<span id='sumPriceItemDis'>" + Math.round(itemSumPriceDis * 10000) / 10000 + "</span>";//formatCurrency(suminf.sum)
                                }
                            }
                        },
                        {
                            display: 'Current Price', name: 'priceNow', width: 70, type: 'float', align: 'right', editor: { type: 'float' }
                        },
                        {
                            display: 'Amount', name: 'sumPriceNow', width: 80, type: 'float', align: 'right', editor: { type: 'float' },

                            totalSummary:
                            {
                                align: 'center', //Alignment of summary cell contents: left/center/right
                                type: 'sum',
                                render: function (e) { //Summary renderer, return HTML to load into cell

                                    var itemSumPriceNow = e.sum;
                                    return "<span id='sumPriceItemNow'>" + Math.round(itemSumPriceNow * 10000) / 10000 + "</span>"; //formatCurrency(suminf.sum)
                                }
                            }
                        },
                        { display: 'Tax rate%', name: 'tax', width: 60, type: 'int', align: 'center', editor: { type: 'int' } },
                        { display: 'Unit price including tax', name: 'priceTax', width: 70, type: 'float', align: 'center', editor: { type: 'float' } },
                        {
                            display: 'Tax amount', name: 'sumPriceTax', width: 80, type: 'float', align: 'right',

                            totalSummary:
                            {
                                align: 'center', //Alignment of summary cell contents: left/center/right
                                type: 'sum',
                                render: function (e) { //Summary renderer, return HTML to load into the cell
                                    //e Summary Object (including sum, max, min, avg, count)

                                    var itemSumPriceTax = e.sum;
                                    return "<span id='sumPriceItemTax'>" + Math.round(itemSumPriceTax * 10000) / 10000 + "</span>"; //formatCurrency(suminf.sum)

                                }
                            }
                        },
                        {
                            display: 'Price and tax total', name: 'sumPriceAll', width: 80, type: 'float', align: 'right', editor: { type: 'float' },
                            totalSummary:
                            {
                                align: 'center', //Alignment of summary cell contents: left/center/right
                                type: 'sum',
                                render: function (e) { //Summary renderer, return HTML to load into the cell
                                    //e Summary Object (including sum, max, min, avg, count)

                                    var itemSumPriceAll = e.sum;
                                    return "<span id='sumPriceItemAll'>" + Math.round(itemSumPriceAll * 10000) / 10000 + "</span>"; //formatCurrency(suminf.sum)

                                }
                            }
                        },
                        { display: 'Remarks', name: 'remarks', width: 150, align: 'left', type: 'text', editor: { type: 'text' } },
                        { display: 'Source order number', name: 'sourceNumber', width: 180, align: 'left', type: 'text' }
                     ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '350',
            url: 'SalesReceiptListEdit.aspx?Action=GetData&id=' + param,
            rownumbers: true, //display serial number
            frozenRownumbers: true, //Whether the row number is in a fixed column
            dataAction: 'local', // local sorting
            usePager: false,
            alternatingRow: false,

            totalSummary: true,
            enabledEdit: true, //Control whether it can be edited
            // onBeforeEdit: f_onBeforeEdit, //If the product has not been selected, the following columns cannot be edited
            // onBeforeSubmitEdit: f_onBeforeSubmitEdit, // Check before submitting the edit

            //totalRender:f_totalRender, //summary

            onAfterEdit: f_onAfterEdit //Operations after updating the cell
        });
});

var rowNumber = 9;
var itemSumPriceAll = 0;

function f_totalRender(data, currentPageData) {
    //return "Total warehouse quantity:"+data.sumPriceAll;
}

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
        priceTax: "", //Unit price including tax
        sumPriceTax: "", //tax amount
        sumPriceAll: "",
        ckId: "",
        ckName: "",
        remarks: "",
        sourceNumber: "",
        itemId: 0
    });

    updateTotal();
}

//Product change event: Get unit, unit price and other information
function f_onGoodsChanged(e) {

    if (!e || !e.length) return;

    //1. Update the subsequent data of the current row first
    var grid = liger.get("maingrid");

    var selected = e[0];// e.data[0]; 

    // alert(selected.names);

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
        priceTax: selected.priceSalesRetail, //Unit price including tax, default is unit price excluding tax
        sumPriceTax: 0, //tax amount
        sumPriceAll: selected.priceSalesRetail,
        ckId: selected.ckId,
        ckName: selected.ckName,
        sourceNumber: "",
        itemId: 0,
        remarks: ""
    });

    if (e.length > 1) //If there are multiple lines, delete the blank lines first, then insert the following
    {
        var data = manager.getData();
        for (var i = data.length - 1; i >= 0; i--) {
            if (data[i].goodsId == 0 || data[i].goodsName == "") {
                manager.deleteRow(i);
                // alert("Delete row: "+i);
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
                priceTax: e[i].priceSalesRetail, //Unit price including tax
                sumPriceTax: 0, //tax amount
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

    var data = manager.getData();//getData
    var sumPriceItemDis = 0; //discount amount
    var sumPriceItemNow = 0; //Amount before tax
    var sumPriceItemTax = 0; //tax amount
    var sumPriceItemAll = 0; //Total price and tax

    //1. Delete the blank line first
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

    //01. Discount amount
    $("#sumPriceItemDis").html(formatCurrency(sumPriceItemDis));

    //01, amount before tax
    $("#sumPriceItemNow").html(formatCurrency(sumPriceItemNow));

    //02. Tax amount
    $("#sumPriceItemTax").html(formatCurrency(sumPriceItemTax));

    //03. Price and tax total
    $("#sumPriceItemAll").html(formatCurrency(sumPriceItemAll));

    var payNow = Number($("#txtPayNow").val());//this payment
    var payNo = sumPriceItemAll - payNow;

    $("#txtPayNo").val(Math.round(payNo * 10000) / 10000);
}

//Post-Edit Event
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

    //Quantity changes---start
    if (e.column.name == "num") {

        num = Number(e.value);

        //0, discount amount = quantity * unit price * discount / 100
        sumPriceDis = Number(num) * Number(price) * Number(dis) / 100;
        sumPriceDis = Math.round(sumPriceDis * 10000) / 10000;

        //1. Amount = quantity * unit price
        sumPriceNow = Number(num) * Number(priceNow);
        sumPriceNow = Math.round(sumPriceNow * 10000) / 10000;

        //2. Tax amount = quantity * unit price * tax rate / 100
        sumPriceTax = Number(num) * Number(priceNow) * Number(tax) / 100;
        sumPriceTax = Math.round(sumPriceTax * 10000) / 10000;

        //3. Total price and tax = quantity * unit price including tax
        sumPriceAll = Number(num) * Number(priceTax);
        sumPriceAll = Math.round(sumPriceAll * 10000) / 10000;

        //Start assigning values
        manager.updateCell("num", num, e.record);

        //0, discount amount
        manager.updateCell('sumPriceDis', sumPriceDis.toString(), e.record);
        //1. Amount
        manager.updateCell('sumPriceNow', sumPriceNow.toString(), e.record);
        //2. Tax amount
        manager.updateCell('sumPriceTax', sumPriceTax.toString(), e.record);
        //3. Price and tax total
        manager.updateCell('sumPriceAll', sumPriceAll.toString(), e.record);

    } //Quantity changes---end

    if (e.column.name == "price") //Unit price changes---start
    {
        price = Number(e.value);
        sumPriceDis = num * price * dis / 100;
        priceNow = price * (1 + dis / 100);
        sumPriceNow = num * priceNow;
        sumPriceNow = Math.round(sumPriceNow * 10000) / 10000;
        priceTax = priceNow * (1 + tax / 100);
        sumPriceTax = num * priceNow * tax / 100; //Tax amount = quantity * current price * tax rate / 100;
        sumPriceAll = num * priceTax;

        //format
        sumPriceDis = Math.round(sumPriceDis * 10000) / 10000;
        sumPriceNow = Math.round(sumPriceNow * 10000) / 10000;
        sumPriceTax = Math.round(sumPriceTax * 10000) / 10000;
        sumPriceAll = Math.round(sumPriceAll * 10000) / 10000;

        //Start assigning values
        manager.updateCell("price", price, e.record);
        manager.updateCell("priceNow", priceNow, e.record);
        manager.updateCell("priceTax", priceTax, e.record);

        //0, discount amount
        manager.updateCell('sumPriceDis', sumPriceDis.toString(), e.record);
        //1. Amount
        manager.updateCell('sumPriceNow', sumPriceNow.toString(), e.record);
        //2. Tax amount
        manager.updateCell('sumPriceTax', sumPriceTax.toString(), e.record);
        //3. Price and tax total
        manager.updateCell('sumPriceAll', sumPriceAll.toString(), e.record);

    } //Unit price change---end

    if (e.column.name == "dis") //Discount change---start
    {
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
        sumPriceTax = num * priceNow * tax / 100; //Tax amount = quantity * current price * tax rate / 100;
        sumPriceAll = num * priceTax;

        //format
        sumPriceDis = Math.round(sumPriceDis * 10000) / 10000;
        sumPriceNow = Math.round(sumPriceNow * 10000) / 10000;
        sumPriceTax = Math.round(sumPriceTax * 10000) / 10000;
        sumPriceAll = Math.round(sumPriceAll * 10000) / 10000;

        //Start assigning values

        manager.updateCell("dis", dis, e.record);
        manager.updateCell("priceNow", priceNow, e.record);
        manager.updateCell("priceTax", priceTax, e.record);

        //0, discount amount
        manager.updateCell('sumPriceDis', sumPriceDis.toString(), e.record);
        //1. Amount
        manager.updateCell('sumPriceNow', sumPriceNow.toString(), e.record);
        //2. Tax amount
        manager.updateCell('sumPriceTax', sumPriceTax.toString(), e.record);
        //3. Price and tax total
        manager.updateCell('sumPriceAll', sumPriceAll.toString(), e.record);

    } //Discount change---end

    if (e.column.name == "sumPriceDis") //Discount amount changes---start
    {
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
        sumPriceTax = num * priceNow * tax / 100; //Tax amount = quantity * current price * tax rate / 100;

        sumPriceAll = priceTax * num;
        sumPriceAll = Math.round(sumPriceAll * 10000) / 10000;

        //format
        sumPriceDis = Math.round(sumPriceDis * 10000) / 10000;
        sumPriceNow = Math.round(sumPriceNow * 10000) / 10000;
        sumPriceTax = Math.round(sumPriceTax * 10000) / 10000;
        sumPriceAll = Math.round(sumPriceAll * 10000) / 10000;

        //Start assigning values
        manager.updateCell("dis", dis, e.record);
        manager.updateCell("priceNow", priceNow, e.record);
        manager.updateCell("priceTax", priceTax, e.record);

        //0, discount amount
        manager.updateCell('sumPriceDis', sumPriceDis.toString(), e.record);
        //1. Amount
        manager.updateCell('sumPriceNow', sumPriceNow.toString(), e.record);
        //2. Tax amount
        manager.updateCell('sumPriceTax', sumPriceTax.toString(), e.record);
        //3. Price and tax total
        manager.updateCell('sumPriceAll', sumPriceAll.toString(), e.record);

    } //Discount amount changed---end

    if (e.column.name == "priceNow") //Current price changes---start
    {
        priceNow = Number(e.value);

        if (price != 0) {
            dis = priceNow / price * 100;
        }
        else {
            price = priceNow;
            dis = 0;
        }

        sumPriceDis = num * (price - priceNow);
        // priceNow = price * (1 + dis / 100);

        sumPriceNow = num * priceNow;
        sumPriceNow = Math.round(sumPriceNow * 10000) / 10000;

        priceTax = priceNow * (1 + tax / 100);
        sumPriceTax = num * priceNow * tax / 100; //Tax amount = quantity * current price * tax rate / 100;

        sumPriceAll = priceTax * num;

        //format
        sumPriceDis = Math.round(sumPriceDis * 10000) / 10000;
        sumPriceNow = Math.round(sumPriceNow * 10000) / 10000;
        sumPriceTax = Math.round(sumPriceTax * 10000) / 10000;
        sumPriceAll = Math.round(sumPriceAll * 10000) / 10000;

        //Start assigning values
        manager.updateCell("price", price, e.record);
        manager.updateCell("dis", dis, e.record);
        manager.updateCell("priceNow", priceNow, e.record);
        manager.updateCell("priceTax", priceTax, e.record);

        //0, discount amount
        manager.updateCell('sumPriceDis', sumPriceDis.toString(), e.record);
        //1. Amount
        manager.updateCell('sumPriceNow', sumPriceNow.toString(), e.record);
        //2. Tax amount
        manager.updateCell('sumPriceTax', sumPriceTax.toString(), e.record);
        //3. Price and tax total
        manager.updateCell('sumPriceAll', sumPriceAll.toString(), e.record);

    } //Current price changes---end

    if (e.column.name == "sumPriceNow") // amount changed
    {
        //Amount changes: [quantity, discount amount, tax rate] Calculate [discount rate, unit price, tax amount, price and tax total]
        sumPriceNow = Number(e.value);

        //1. Calculate the current price
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

        //2. Calculate the unit price including tax
        priceTax = tax * priceNow / 100;
        if (tax == 0) {
            priceTax = priceNow;
        }

        price = Math.round(price * 10000) / 10000;
        priceTax = Math.round(priceTax * 10000) / 10000;
        sumPriceNow = Math.round(sumPriceNow * 10000) / 10000;

        //2. Tax amount = quantity * unit price * tax rate / 100
        sumPriceTax = Number(num) * Number(price) * Number(tax) / 100;
        sumPriceTax = Math.round(sumPriceTax * 10000) / 10000;

        //2. Total price and tax = quantity * unit price including tax
        sumPriceAll = Number(num) * Number(priceTax);
        sumPriceAll = Math.round(sumPriceAll * 10000) / 10000;

        //Start assigning values


        //1. Variables
        manager.updateCell("price", price, e.record);
        manager.updateCell("dis", dis, e.record);
        manager.updateCell("priceNow", priceNow, e.record);
        manager.updateCell("priceTax", priceTax, e.record);

        //2. Amount
        manager.updateCell('sumPriceDis', sumPriceDis, e.record);
        manager.updateCell('sumPriceNow', sumPriceNow, e.record);
        manager.updateCell('sumPriceTax', sumPriceTax, e.record);
        manager.updateCell('sumPriceAll', sumPriceAll, e.record);

    } //Amount changed---end

    if (e.column.name == "tax") //tax rate changes
    {
        //Amount changes: [quantity, discount amount, tax rate] Calculate [discount rate, unit price, tax amount, price and tax total]

        tax = Number(e.value);

        //1. Calculate the unit price including tax

        priceTax = priceNow * (1 + tax / 100);
        if (tax == 0) {
            priceTax = priceNow;
        }

        priceTax = Math.round(priceTax * 10000) / 10000;

        //2. Tax amount = quantity * unit price * tax rate / 100
        sumPriceTax = Number(num) * Number(priceNow) * Number(tax) / 100;
        sumPriceTax = Math.round(sumPriceTax * 10000) / 10000;

        //2. Total price and tax = quantity * unit price including tax
        sumPriceAll = Number(num) * Number(priceTax);
        sumPriceAll = Math.round(sumPriceAll * 10000) / 10000;

        //Start assigning values


        //1. Variables
        manager.updateCell("priceTax", priceTax, e.record);

        //2. Tax amount
        manager.updateCell('sumPriceTax', sumPriceTax.toString(), e.record);
        //3. Price and tax total
        manager.updateCell('sumPriceAll', sumPriceAll.toString(), e.record);

    } //Tax rate change---end

    if (e.column.name == "priceTax") // Change of unit price including tax
    {
        //Amount changes: [quantity, discount amount, tax rate] Calculate [discount rate, unit price, tax amount, price and tax total]
        priceTax = Number(e.value);

        //1. Calculate unit price
        priceNow = priceTax / (1 + tax / 100);
        priceNow = Math.round(priceNow * 10000) / 10000;
        priceTax = Math.round(priceTax * 10000) / 10000;

        dis = priceNow / price * 100;
        if (dis == 100) {
            dis = 0;
        }
        dis = Math.round(dis * 10000) / 10000;

        //1. Discount = quantity * unit price * tax rate / 100
        sumPriceDis = num * (price - priceNow);
        sumPriceDis = Math.round(sumPriceDis * 10000) / 10000;

        //1. Discount = quantity * unit price * tax rate / 100
        sumPriceNow = num * priceNow;
        sumPriceNow = Math.round(sumPriceNow * 10000) / 10000;

        //2. Tax amount = quantity * unit price * tax rate / 100
        sumPriceTax = Number(num) * Number(price) * Number(tax) / 100;
        sumPriceTax = Math.round(sumPriceTax * 10000) / 10000;

        //2. Total price and tax = quantity * unit price including tax
        sumPriceAll = Number(num) * Number(priceTax);
        sumPriceAll = Math.round(sumPriceAll * 10000) / 10000;

        //Start assigning values


        //1. Variables
        manager.updateCell("dis", dis, e.record);
        manager.updateCell("priceNow", priceNow, e.record);
        manager.updateCell("priceTax", priceTax, e.record);

        //1. Discount
        manager.updateCell('sumPriceDis', sumPriceDis, e.record);
        //2. Amount
        manager.updateCell('sumPriceNow', sumPriceNow, e.record);
        //3. Tax amount
        manager.updateCell('sumPriceTax', sumPriceTax.toString(), e.record);
        //4. Price and tax total
        manager.updateCell('sumPriceAll', sumPriceAll.toString(), e.record);

    } //Change of unit price including tax---End

    if (e.column.name == "sumPriceAll") // price and tax total changed
    {
        //Amount changes: [quantity, discount amount, tax rate] Calculate [discount rate, unit price, tax amount, price and tax total]
        sumPriceAll = Number(e.value);

        //1. Calculate the unit price including tax
        if (num != 0) {
            priceTax = sumPriceAll / num;
        }

        //2. Calculate the unit price before tax
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

        //2. Tax amount = quantity * unit price * tax rate / 100
        sumPriceTax = Number(num) * Number(price) * Number(tax) / 100;
        sumPriceTax = Math.round(sumPriceTax * 10000) / 10000;

        //2. Total price and tax = quantity * unit price including tax
        sumPriceAll = Number(num) * Number(priceTax);
        sumPriceAll = Math.round(sumPriceAll * 10000) / 10000;


        //Start assigning values


        //1. Variables
        manager.updateCell("priceNow", priceNow, e.record);
        manager.updateCell("dis", dis, e.record);
        manager.updateCell("priceTax", priceTax, e.record);

        //2. Amount
        manager.updateCell('sumPriceDis', sumPriceDis, e.record);
        //2. Amount
        manager.updateCell('sumPriceNow', sumPriceNow, e.record);
        //2. Tax amount
        manager.updateCell('sumPriceTax', sumPriceTax, e.record);
        //3. Price and tax total
        manager.updateCell('sumPriceAll', sumPriceAll, e.record);

    } // Price and tax total changed --- end


    //Finally change the value of the summary row
    updateTotal();
}

//Only allow editing of rows where products have been added
function f_onBeforeEdit(e) {

    //            if(e.data.goodsId!="" && e.data.goodsName!="") return true;
    //            return false;
    //            
    //            if(e.rowindex<=2) return true;
    //            return false;
}
//Limit discounts and tax rates

function f_onBeforeSubmitEdit(e) {
    if (e.column.name == "dis") {
        if (e.value < 0 || e.value > 100) return false;
    }

    return true;
}

function deleteRow() {

    if (manager.rows.length == 1) {
        $.ligerDialog.warn('Keep at least one line!')

    }
    else {
        manager.deleteSelectedRow();
    }
}
var newrowid = 100;

function save() {

    var venderId = $("#clientId").val(); //Get the Selected Value

    if (venderId == "" || Number(venderId) == 0) {
        $.ligerDialog.warn("Please select a customer!");
        return;
    }

    var bizId = $("#ddlYWYList").val();
    var bkId = $("#ddlBankList").val(); //Get the Selected Value

    var bizDate = $("#txtBizDate").val();
    if (bizDate == "") {
        $.ligerDialog.warn("Please fill in the delivery date!");
        return;

    }
    // Delete the blank line first
    var data = manager.getData();

    //1. Delete the blank line first
    for (var i = data.length - 1; i >= 0; i--) {
        if (data[i].goodsId == 0 || data[i].goodsId == "" || data[i].goodsName == "") {
            data.splice(i, 1);

        }
    }

    //2. Determine whether to select a product
    if (data.length == 0) {
        $.ligerDialog.warn('Please select a product!');

        return;
        alert("I won't execute it!");
    }

    //3. Determine whether the quantity of goods has been entered.
    for (var i = 0; i < data.length; i++) {
        if (data[i].num <= 0 || data[i].num == "" || data[i].num == "0" || data[i].num == "0.00") {

            $.ligerDialog.warn("Please enter the quantity of goods in the " + (i + 1) + " row!");

            return;
            alert("I won't execute it!");
        }

        if (data[i].ckId == 0 || data[i].ckId == "" || data[i].ckId == "0" || data[i].ckName == "") {

            $.ligerDialog.warn("Please enter the warehouse of line " + (i + 1) + "!");

            return;
            alert("I won't execute it!");
        }
    }

    var remarks = $("#txtRemarks").val();

    var payNow = $("#txtPayNow").val();
    var payNowNo = $("#txtPayNo").val();

    if (Number(payNow) != 0) {

        if (bkId == "0") {
            $.ligerDialog.warn("Please select the settlement account!");
            return;
        }

    }

    var dis = $("#txtDis").val();
    var disPrice = $("#txtDisPrice").val();

    var sendId = $("#ddlSendCompanyList").val();
    var sendNumber = $("#txtSendNumber").val();
    var sendPayType = $("#ddlSendPayTypeList").val();
    var sendPrice = $("#txtSendPrice").val();

    var getName = $("#txtGetName").val();
    var phone = $("#txtPhone").val();
    var address = $("#txtAddress").val();

    var headJson = {
        id: param,
        venderId: venderId,
        bizDate: bizDate,
        bizId: bizId,
        remarks: remarks,
        payNow: payNow,
        payNowNo: payNowNo,
        bkId: bkId,
        SendId: sendId,
        SendNumber: sendNumber,
        SendPayType: sendPayType,
        SendPrice: Number(sendPrice),
        GetName: getName,
        Phone: phone,
        Address: address,
        dis: dis,
        disPrice: disPrice

    };

    var dataNew = [];
    dataNew.push(headJson);

    var list = JSON.stringify(headJson);

    var goodsList = [];

    list = list.substring(0, list.length - 1); // remove the last curly brace

    list += ",\"Rows\":";
    list += JSON.stringify(data);
    list += "}";

    var postData = JSON.parse(list); //final json

    // alert(JSON.stringify(postData));
    //
    // return;

    $.ajax({
        type: "POST",
        url: 'ashx/SalesReceiptListEdit.ashx',
        contentType: "application/json", //Required
        //dataType: "json", // indicates the return value type, not required
        data: JSON.stringify(postData), // equivalent to //data: "{'str1':'foovalue', 'str2':'barvalue'}",
        success: function (jsonResult) {

            if (jsonResult == "Operation successful!") {

                $.ligerDialog.waitting('Operation successful!'); setTimeout(function () { $.ligerDialog.closeWaitting(); location.reload(); }, 2000);
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

function calcPayNo(payNow) {

    // Knowing this payment, calculate the current amount owed

    //1. Get the discounted amount
    payNow = $("#txtPayNow").val();

    var sumPrice = $("#sumPriceItemAll").html();// document.getElementById('<%=txtSumPrice.ClientID %>').value;//Discounted amount

    // alert(sumPrice);
    //2. Calculate the current amount owed
    var payNo = sumPrice - payNow;

    $("#txtPayNo").val(Math.round(payNo * 10000) / 10000);
}

//Calculate the security deposit
function calcDisPrice(dis) {

    // Knowing this payment, calculate the current amount owed

    //1. Get the guarantee deposit ratio
    dis = $("#txtDis").val();

    var sumPriceAll = $("#sumPriceItemAll").html();// document.getElementById('<%=txtSumPrice.ClientID %>').value;//Discounted amount

    // alert(sumPrice);
    //2. Calculate the warranty amount
    var disPrice = sumPriceAll * dis / 100;
    $("#txtDisPrice").val(Math.round(disPrice * 10000) / 10000);
} }

var param = getUrlParam("id");

function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");

    var r = window.location.search.substr(1).match(reg);

    if (r != null) return unescape(r[2]); return null;
}

function getthedate(dd, dadd) {
    //You can add error handling
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
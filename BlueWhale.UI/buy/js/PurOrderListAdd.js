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
$(document)
    .bind('keydown.grid', function (event) {
        if (event.keyCode == 13 || event.keyCode == 39 || event.keyCode == 9) //enter,right arrow,tap
        {
            manager.endEditToNext();
        }
    });
// New Entry
function f_selectContact() {
    $.ligerDialog.open({
        title: 'Select Item',
        name: 'winselector',
        width: 840,
        height: 540,
        url: '../baseSet/GoodsListSelect.aspx',
        buttons: [{
            text: 'Confirm',
            onclick: f_selectContactOK
        }, {
            text: 'Cancel',
            onclick: f_selectContactCancel
        }]
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
// New Entry end
// Client start
function f_selectClient() {
    $.ligerDialog.open({
        title: 'Please Select Supplier',
        name: 'winselector',
        width: 800,
        height: 540,
        url: '../baseSet/VenderListSelect.aspx',
        buttons: [{
            text: 'Confirm',
            onclick: f_selectClientOK
        }, {
            text: 'Cancel',
            onclick: f_selectClientCancel
        }]
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
    $("#clientName")
        .val(data.names);
    $("#clientId")
        .val(data.id);
    dialog.close();
}

function f_selectClientCancel(item, dialog) {
    dialog.close();
}
// Client end
// expend numberbox type function
$.ligerDefaults.Grid.formatters['numberbox'] = function (value, column) {
    var precision = column.editor.precision;
    return value.toFixed(precision);
};
$(function () {
    $("#clientName")
        .ligerComboBox({
            onBeforeOpen: f_selectClient,
            valueFieldID: 'clientId',
            width: 300
        });
});
var manager;
$(function () {
    var form = $("#form")
        .ligerForm();
    var form1 = $("#tbFooter")
        .ligerForm();
    var form2 = $("#form22")
        .ligerForm();
    var g = $.ligerui.get("clientName");
    g.set("Width", 250);
    window['g'] =
        manager = $("#maingrid")
            .ligerGrid({
                columns: [{
                    display: '',
                    isSort: false,
                    width: 60,
                    align: 'center',
                    frozen: true,
                    render: function (rowdata, rowindex, value) {
                        var h = "";
                        if (!rowdata._editing) {
                            h += "<a href='javascript:addNewRow()' title='Create Row' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                            h += "<a href='javascript:deleteRow()' title='Delete Row' style='float:left;'><div class='ui-icon ui-icon-trash'></div></a> ";
                            h += "<a href='javascript:f_selectContact()' title='Select Item' style='float:left;'><div class='ui-icon ui-icon-search'></div></a> ";
                        }
                        return h;
                    }
                }, {
                    display: 'Item Name',
                    name: 'goodsName',
                    width: 218,
                    align: 'left',
                    totalSummary: {
                        type: 'count',
                        render: function (e) { // Summary renderer, returns HTML loaded into the cell
                            //e Summary Object (including sum, max, min, avg, count)
                            return 'Total：';
                        }
                    }
                }, {
                    display: 'Format',
                    name: 'spec',
                    width: 100,
                    align: 'center'
                }, {
                    display: 'Unit',
                    name: 'unitName',
                    width: 80,
                    align: 'center'
                }, {
                    display: 'Warehouse',
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
                }, {
                    display: 'Amount',
                    name: 'num',
                    width: 80,
                    type: 'float',
                    align: 'right',
                    editor: {
                        type: 'float',
                        precision: 3
                    },
                    totalSummary: {
                        align: 'right', // Alignment of summary cell contents: left/center/right
                        type: 'sum',
                        render: function (e) { // Summary renderer, returns HTML loaded into the cell
                            //e Summary Object (including sum, max, min, avg, count)
                            return Math.round(e.sum * 1000) / 1000;
                        }
                    }
                }, {
                    display: 'Original Price',
                    name: 'price',
                    width: 100,
                    type: 'float',
                    align: 'right',
                    editor: {
                        type: 'float'
                    }
                }, {
                    display: 'Discount Amount%',
                    name: 'dis',
                    width: 90,
                    type: 'float',
                    align: 'right',
                    editor: {
                        type: 'float'
                    }
                }, {
                    display: 'Discount Amount',
                    name: 'sumPriceDis',
                    width: 100,
                    type: 'float',
                    align: 'right',
                    editor: {
                        type: 'float'
                    },
                    totalSummary: {
                        align: 'center', // Alignment of summary cell contents: left/center/right
                        type: 'sum',
                        render: function (e) { // Summary renderer, returns HTML loaded into the cell
                            var itemSumPriceDis = e.sum;
                            return "<span id='sumPriceItemDis'>" + Math.round(itemSumPriceDis * 10000) / 10000 + "</span>"; //formatCurrency(suminf.sum)
                        }
                    }
                }, {
                    display: 'Current Price',
                    name: 'priceNow',
                    width: 100,
                    type: 'float',
                    align: 'right',
                    editor: {
                        type: 'float'
                    }
                }, {
                    display: 'Total Price',
                    name: 'sumPriceNow',
                    width: 90,
                    type: 'float',
                    align: 'right',
                    editor: {
                        type: 'float'
                    },
                    totalSummary: {
                        align: 'center', // Alignment of summary cell contents: left/center/right
                        type: 'sum',
                        render: function (e) { // Summary renderer, returns HTML loaded into the cell
                            var itemSumPriceNow = e.sum;
                            return "<span id='sumPriceItemNow'>" + Math.round(itemSumPriceNow * 10000) / 10000 + "</span>"; //formatCurrency(suminf.sum)
                        }
                    }
                }, {
                    display: 'Tax%',
                    name: 'tax',
                    width: 60,
                    type: 'int',
                    align: 'center',
                    editor: {
                        type: 'int'
                    }
                }, {
                    display: 'Price W/ Tax',
                    name: 'priceTax',
                    width: 100,
                    type: 'float',
                    align: 'center',
                    editor: {
                        type: 'float'
                    }
                }, {
                    display: 'Total Tax Amount',
                    name: 'sumPriceTax',
                    width: 100,
                    type: 'float',
                    align: 'right',
                    totalSummary: {
                        align: 'center', // Alignment of summary cell contents: left/center/right
                        type: 'sum',
                        render: function (e) { // Summary renderer, returns HTML loaded into the cell
                            //e Summary Object (including sum, max, min, avg, count)
                            var itemSumPriceTax = e.sum;
                            return "<span id='sumPriceItemTax'>" + Math.round(itemSumPriceTax * 10000) / 10000 + "</span>"; //formatCurrency(suminf.sum)
                        }
                    }
                }, {
                    display: 'Sum Price',
                    name: 'sumPriceAll',
                    width: 100,
                    type: 'float',
                    align: 'right',
                    editor: {
                        type: 'float'
                    },
                    totalSummary: {
                        align: 'center', // Alignment of summary cell contents: left/center/right
                        type: 'sum',
                        render: function (e) { // Summary renderer, returns HTML loaded into the cell
                            //e Summary Object (including sum, max, min, avg, count)
                            var itemSumPriceAll = e.sum;
                            return "<span id='sumPriceItemAll'>" + Math.round(itemSumPriceAll * 10000) / 10000 + "</span>"; //formatCurrency(suminf.sum)
                        }
                    }
                }, {
                    display: 'Remark',
                    name: 'remarks',
                    width: 150,
                    align: 'left',
                    type: 'text',
                    editor: {
                        type: 'text'
                    }
                }, {
                    display: 'Associated Sales Order Number',
                    name: 'sourceNumber',
                    width: 155,
                    align: 'left',
                    type: 'text'
                }],
                width: '99%',
                pageSizeOptions: [5, 10, 15, 20],
                height: 342,
                url: 'PurOrderListAdd.aspx?Action=GetData&id=' + param,
                rownumbers: true, //Display row number
                frozenRownumbers: true, //Whether the row number is in a fixed column
                dataAction: 'local', //Local sorting
                usePager: false,
                alternatingRow: false,
                totalSummary: true,
                enabledEdit: true, //Controls whether editable
                // onBeforeEdit: f_onBeforeEdit,//Control that if you haven't selected a product, you can't edit the following columns
                // onBeforeSubmitEdit: f_onBeforeSubmitEdit,//Check before submitting your edits
                //totalRender:f_totalRender,//Summary
                onAfterEdit: f_onAfterEdit //Actions after updating a cell
            });
});
var rowNumber = 9;

function f_totalRender(data, currentPageData) { }

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
        sumPriceTax: "", //Total tax
        sumPriceAll: "",
        ckId: "",
        ckName: "",
        remarks: "",
        sourceNumber: "",
        itemId: 0
    });
    updateTotal();
}
//Product change event: get unit, unit price and other information
function f_onGoodsChanged(e) {
    if (!e || !e.length) return;
    //1. Update the subsequent data of the current row first
    var grid = liger.get("maingrid");
    var selected = e[0]; // e.data[0]; 
    // alert(selected.names);
    var selectedRow = manager.getSelected();
    grid.updateRow(selectedRow, {
        goodsId: selected.id,
        goodsName: selected.names,
        spec: selected.spec,
        unitName: selected.unitName,
        num: 1,
        price: selected.priceCost,
        dis: 0,
        sumPriceDis: 0,
        priceNow: selected.priceCost,
        sumPriceNow: selected.priceCost,
        tax: 0,
        priceTax: selected.priceCost, //Unit price including tax, default is unit price excluding tax
        sumPriceTax: 0, //tax
        sumPriceAll: selected.priceCost,
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
                // alert("Remove Row："+i);
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
                price: e[i].priceCost,
                dis: 0,
                sumPriceDis: 0,
                priceNow: e[i].priceCost,
                sumPriceNow: e[i].priceCost,
                tax: 0,
                priceTax: e[i].priceCost, //Unit price including tax
                sumPriceTax: 0, //tax
                sumPriceAll: e[i].priceCost,
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
    var data = manager.getData(); //getData
    var sumPriceItemDis = 0; //Discount amount
    var sumPriceItemNow = 0; //Amount before tax
    var sumPriceItemTax = 0; //tax
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
    $("#sumPriceItemDis")
        .html(formatCurrency(sumPriceItemDis));
    //01. Amount before tax
    $("#sumPriceItemNow")
        .html(formatCurrency(sumPriceItemNow));
    //02. Tax amount
    $("#sumPriceItemTax")
        .html(formatCurrency(sumPriceItemTax));
    //03. Total price and tax
    $("#sumPriceItemAll")
        .html(formatCurrency(sumPriceItemAll));
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
    //Quantity Change---Start
    if (e.column.name == "num") {
        num = Number(e.value);
        //0. Discount amount = quantity * unit price * discount / 100
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
        //Start Assignment
        manager.updateCell("num", num, e.record);
        //0. Discount amount
        manager.updateCell('sumPriceDis', sumPriceDis.toString(), e.record);
        //1. Amount
        manager.updateCell('sumPriceNow', sumPriceNow.toString(), e.record);
        //2. Tax amount
        manager.updateCell('sumPriceTax', sumPriceTax.toString(), e.record);
        //3. Total price and tax
        manager.updateCell('sumPriceAll', sumPriceAll.toString(), e.record);
    } //Quantity Change---End
    if (e.column.name == "price") //Unit price change---start
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
        //Start Assignment
        manager.updateCell("price", price, e.record);
        manager.updateCell("priceNow", priceNow, e.record);
        manager.updateCell("priceTax", priceTax, e.record);
        //0. Discount amount
        manager.updateCell('sumPriceDis', sumPriceDis.toString(), e.record);
        //1. Amount
        manager.updateCell('sumPriceNow', sumPriceNow.toString(), e.record);
        //2. Tax amount
        manager.updateCell('sumPriceTax', sumPriceTax.toString(), e.record);
        //3. Total price and tax
        manager.updateCell('sumPriceAll', sumPriceAll.toString(), e.record);
    } //Unit price change---end
    if (e.column.name == "dis") //Discount changes---start
    {
        dis = Number(e.value);
        if (dis != 0) {
            priceNow = price * (dis / 100);
            sumPriceDis = num * price * (1 - dis / 100);
        } else {
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
        //Start Assignment
        manager.updateCell("dis", dis, e.record);
        manager.updateCell("priceNow", priceNow, e.record);
        manager.updateCell("priceTax", priceTax, e.record);
        //0. Discount amount
        manager.updateCell('sumPriceDis', sumPriceDis.toString(), e.record);
        //1. Amount
        manager.updateCell('sumPriceNow', sumPriceNow.toString(), e.record);
        //2. Tax amount
        manager.updateCell('sumPriceTax', sumPriceTax.toString(), e.record);
        //3. Total price and tax
        manager.updateCell('sumPriceAll', sumPriceAll.toString(), e.record);
    } //Discount Changes --- End
    if (e.column.name == "sumPriceDis") //Discount amount changes---Start
    {
        sumPriceDis = Number(e.value);
        if (sumPriceDis >= num * price) {
            alert("Please fill in the correct discount amount!");
            return;
        }
        if (num * price != 0) {
            dis = (1 - sumPriceDis / (num * price)) * 100;
        } else {
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
        //Start Assignment
        manager.updateCell("dis", dis, e.record);
        manager.updateCell("priceNow", priceNow, e.record);
        manager.updateCell("priceTax", priceTax, e.record);
        //0. Discount amount
        manager.updateCell('sumPriceDis', sumPriceDis.toString(), e.record);
        //1. Amount
        manager.updateCell('sumPriceNow', sumPriceNow.toString(), e.record);
        //2. Tax amount
        manager.updateCell('sumPriceTax', sumPriceTax.toString(), e.record);
        //3. Total price and tax
        manager.updateCell('sumPriceAll', sumPriceAll.toString(), e.record);
    } //Discount amount changed---End
    if (e.column.name == "priceNow") //Current price change---start
    {
        priceNow = Number(e.value);
        if (price != 0) {
            dis = priceNow / price * 100;
        } else {
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
        //Start Assignment
        manager.updateCell("price", price, e.record);
        manager.updateCell("dis", dis, e.record);
        manager.updateCell("priceNow", priceNow, e.record);
        manager.updateCell("priceTax", priceTax, e.record);
        //0. Discount amount
        manager.updateCell('sumPriceDis', sumPriceDis.toString(), e.record);
        //1. Amount
        manager.updateCell('sumPriceNow', sumPriceNow.toString(), e.record);
        //2. Tax amount
        manager.updateCell('sumPriceTax', sumPriceTax.toString(), e.record);
        //3. Total price and tax
        manager.updateCell('sumPriceAll', sumPriceAll.toString(), e.record);
    } //Current price changes --- End
    if (e.column.name == "sumPriceNow") //Amount Change
    {
        //Amount changes: [Quantity, discount amount, tax rate] Calculate [discount rate, unit price, tax amount, price and tax total]
        sumPriceNow = Number(e.value);
        //1. Calculate the current price
        if (num != 0) {
            priceNow = (sumPriceNow) / num;
        } else {
            num = 1;
            priceNow = sumPriceNow;
        }
        sumPriceDis = num * (price - priceNow);
        if (price != 0) {
            dis = priceNow / price * 100;
        } else {
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
        //Start Assignment
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
    } //Amount changed---End
    if (e.column.name == "tax") //Tax rate changes
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
        //Start Assignment
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
        // Start assigning values
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
    } // Change of unit price including tax---End
    if (e.column.name == "sumPriceAll") // Price and tax total changed
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
        // Start assigning values
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
    // Finally change the value of the summary row
    updateTotal();
}
//Limit discounts and tax rates
function f_onBeforeEdit(e) { }

function f_onBeforeSubmitEdit(e) {
    if (e.column.name == "dis") {
        if (e.value < 0 || e.value > 100) return false;
    }
    return true;
}

function deleteRow() {
    if (manager.rows.length == 1) {
        $.ligerDialog.warn('Leave at least one row!')
    } else {
        manager.deleteSelectedRow();
    }
}
var newrowid = 100;

function save() {
    var venderId = $("#clientId")
        .val(); //Get selected value
    var bizId = $("#ddlYWYList")
        .val();
    if (venderId == 0) {
        $.ligerDialog.warn('Please choose a vender!');
        return;
    }
    if (bizId == 0) {
        $.ligerDialog.warn('Please choose a purchaser!');
        return;
    }
    var bizDate = $("#txtBizDate")
        .val();
    if (bizDate == "") {
        $.ligerDialog.warn("Please fill the order date!");
        return;
    }
    var sendDate = $("#txtSendDate")
        .val();
    if (sendDate == "") {
        $.ligerDialog.warn("Please fill the delivery date!");
        return;
    }
    var remarks = $("#txtRemarks")
        .val();
    //Delete the blank lines first
    var data = manager.getData();
    //1. Delete the blank line first
    for (var i = data.length - 1; i >= 0; i--) {
        if (data[i].goodsId == 0 || data[i].goodsId == "" || data[i].goodsName == "") {
            data.splice(i, 1);
        }
    }
    //2. Determine whether to select a product
    if (data.length == 0) {
        $.ligerDialog.warn('Please choose an item!');
        return;
        alert("Won't execute!");
    }
    //3. Determine whether the quantity of goods has been entered.
    for (var i = 0; i < data.length; i++) {
        if (data[i].num <= 0 || data[i].num == "" || data[i].num == "0" || data[i].num == "0.00") {
            $.ligerDialog.warn("Please fill in " + (i + 1) + "th row item count");
            return;
            alert("Won't execute!");
        }
    }
    var headJson = {
        venderId: venderId,
        bizDate: bizDate,
        sendDate: sendDate,
        bizId: bizId,
        remarks: remarks
    };
    var dataNew = [];
    dataNew.push(headJson);
    var list = JSON.stringify(headJson);
    var goodsList = [];
    list = list.substring(0, list.length - 1); //Remove the last curly brace
    list += ",\"Rows\":";
    list += JSON.stringify(data);
    list += "}";
    var postData = JSON.parse(list); //Final json
    $.ajax({
        type: "POST",
        url: 'ashx/PurOrderListAdd.ashx',
        contentType: "application/json", //Must have
        //dataType: "json", //Indicates the return value type, not required
        data: JSON.stringify(postData), //Equivalent to //data: "{'str1':'foovalue', 'str2':'barvalue'}",
        success: function (jsonResult) {
            if (jsonResult == "Operation success!") {
                $.ligerDialog.waitting('Operation success!');
                setTimeout(function () {
                    $.ligerDialog.closeWaitting();
                    location.reload();
                }, 2000);
            } else {
                $.ligerDialog.warn(jsonResult);
            }
        },
        error: function (xhr) {
            alert("Error, please try again later!" + xhr.responseText);
        }
    });
}
var param = getUrlParam("id");

function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1)
        .match(reg);
    if (r != null) return unescape(r[2]);
    return null;
}

function getthedate(dd, dadd) {
    //can add error handle
    var a = new Date(dd)
    a = a.valueOf()
    a = a + dadd * 24 * 60 * 60 * 1000
    a = new Date(a);
    var m = a.getMonth() + 1;
    if (m.toString()
        .length == 1) {
        m = '0' + m;
    }
    var d = a.getDate();
    if (d.toString()
        .length == 1) {
        d = '0' + d;
    }
    return a.getFullYear() + "-" + m + "-" + d;
}
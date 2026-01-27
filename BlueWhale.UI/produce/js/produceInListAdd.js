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

function f_selectContact() {
    $.ligerDialog.open({
        title: 'Select Goods', name: 'winselector', width: 840, height: 540, url: '../baseSet/GoodsListSelect.aspx', buttons: [
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

$.ligerDefaults.Grid.formatters['numberbox'] = function (value, column) {
    var precision = column.editor.precision;
    return value.toFixed(precision);
};

var manager;
$(function () {
    var form = $("#form").ligerForm();
    var form1 = $("#tbFooter").ligerForm();
    window['g'] =
        manager = $("#maingrid").ligerGrid({
            columns: [
                {
                    display: '', isSort: false, width: 60, align: 'center', frozen: true, render: function (rowdata, rowindex, value) {
                        var h = "";
                        if (!rowdata._editing) {
                            h += "<a href='javascript:addNewRow()' title='Add row' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                            h += "<a href='javascript:deleteRow()' title='Delete row' style='float:left;'><div class='ui-icon ui-icon-trash'></div></a> ";
                            h += "<a href='javascript:f_selectContact()' title='Select goods' style='float:left;'><div class='ui-icon ui-icon-search'></div></a> ";
                        }

                        return h;
                    }
                },
                {
                    display: 'Goods Name', name: 'goodsName', width: 250, align: 'left',
                    totalSummary:
                    {
                        type: 'count',
                        render: function (e) {  //Summary renderer, returns HTML to load into the cell
                            //e Summary Object (including sum, max, min, avg, count)
                            return 'Total:';
                        }
                    }
                },

                { display: 'Specification', name: 'spec', width: 100, align: 'center' },
                { display: 'Unit', name: 'unitName', width: 80, align: 'center' },
                {
                    display: 'Inbound Warehouse', name: 'ckId', width: 120, isSort: false, textField: 'ckName',
                    editor: {
                        type: 'select',
                        url: "../baseSet/InventoryList.aspx?Action=GetDDLList&r=" + Math.random(), //Wating 125 pr
                        valueField: 'ckId', textField: 'ckName'
                    }
                },
                {
                    display: 'Quantity', name: 'num', width: 100, type: 'float', align: 'right', editor: { type: 'float' },
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
                    display: 'Cost Unit Price', name: 'price', width: 100, type: 'float', align: 'right', editor: { type: 'float' }
                },
                {
                    display: 'Cost Amount', name: 'sumPriceNow', width: 120, type: 'float', align: 'right', editor: { type: 'float' },
                    totalSummary:
                    {
                        align: 'center',   //Summary cell content alignment: left/center/right
                        type: 'sum',
                        render: function (e) {  //Summary renderer, returns HTML to load into the cell
                            var itemSumPriceNow = e.sum;
                            return "<span id='sumPriceItemNow'>" + Math.round(itemSumPriceNow * 10000) / 10000 + "</span>";//formatCurrency(suminf.sum)
                        }
                    }
                },
                { display: 'Remarks', name: 'remarks', width: 150, align: 'left', type: 'text', editor: { type: 'text' } },
                { display: 'Associated Production Plan Order Number', name: 'sourceNumber', width: 150, align: 'left', type: 'text' }
            ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '350',
            url: 'produceInListAdd.aspx?Action=GetData&id=' + param,
            rownumbers: true,//Display serial number
            frozenRownumbers: true,//Is the row number in a fixed column
            dataAction: 'local',//Local sorting
            usePager: false,
            alternatingRow: false,

            totalSummary: true,
            enabledEdit: true, //Control whether editing is allowed
            // onBeforeEdit: f_onBeforeEdit,
            // onBeforeSubmitEdit: f_onBeforeSubmitEdit,
            //totalRender:f_totalRender,
            onAfterEdit: f_onAfterEdit //Actions after updating the cell
        });
});

var rowNumber = 9;
//var itemSumPriceAll = 0;

function f_totalRender(data, currentPageData) {
    //return "Total: "+data.sumPriceAll; 
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
    var selected = e[0];// e.data[0]; 
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
        priceTax: selected.priceCost,
        sumPriceTax: 0,
        sumPriceAll: selected.priceCost,
        ckId: selected.ckId,
        ckName: selected.ckName,
        sourceNumber: "",
        itemId: 0,
        remarks: ""
    });

    if (e.length > 1) 
    {
        var data = manager.getData();
        for (var i = data.length - 1; i >= 0; i--) {
            if (data[i].goodsId == 0 || data[i].goodsName == "") {
                manager.deleteRow(i);
                // alert("Delete row："+i);
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
                priceTax: e[i].priceCost,
                sumPriceTax: 0,
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
    var data = manager.getData();//getData
    var sumPriceItemDis = 0;
    var sumPriceItemNow = 0;
    var sumPriceItemTax = 0;
    var sumPriceItemAll = 0;

    //1, First delete the blank rows
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

    $("#sumPriceItemNow").html(formatCurrency(sumPriceItemNow));
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

        //0、discountAmount = quantity * unitPrice * discount / 100
        sumPriceDis = Number(num) * Number(price) * Number(dis) / 100;
        sumPriceDis = Math.round(sumPriceDis * 10000) / 10000;

        //1、sumPrice = quantity * unitPrice
        sumPriceNow = Number(num) * Number(priceNow);
        sumPriceNow = Math.round(sumPriceNow * 10000) / 10000;

        //2、taxPrice = quantity * unitPrice * tax / 100
        sumPriceTax = Number(num) * Number(priceNow) * Number(tax) / 100;
        sumPriceTax = Math.round(sumPriceTax * 10000) / 10000;

        //3、sumPriceAll = Quantity * UnitPriceWithTax
        sumPriceAll = Number(num) * Number(priceTax);
        sumPriceAll = Math.round(sumPriceAll * 10000) / 10000;

        manager.updateCell("num", num, e.record);
        manager.updateCell('sumPriceDis', sumPriceDis.toString(), e.record);
        manager.updateCell('sumPriceNow', sumPriceNow.toString(), e.record);
        manager.updateCell('sumPriceTax', sumPriceTax.toString(), e.record);
        manager.updateCell('sumPriceAll', sumPriceAll.toString(), e.record);
    }

    if (e.column.name == "price")
    {
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

    if (e.column.name == "dis")
    {
        dis = Number(e.value);
        if (dis != 0)
        {
            priceNow = price * (dis / 100);
            sumPriceDis = num * price * (1 - dis / 100);
        }
        else
        {
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

    if (e.column.name == "sumPriceDis")
    {
        sumPriceDis = Number(e.value);
        if (sumPriceDis >= num * price)
        {
            alert("Please enter the correct discount amount!");
            return;
        }

        if (num * price != 0)
        {
            dis = (1 - sumPriceDis / (num * price)) * 100;
        }
        else
        {
            alert("Please enter the quantity and unit price!");
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

    if (e.column.name == "priceNow")
    {
        priceNow = Number(e.value);

        if (price != 0)
        {
            dis = priceNow / price * 100;
        }
        else
        {
            price = priceNow;
            dis = 0;
        }

        sumPriceDis = num * (price - priceNow);
        // priceNow = price * (1 + dis / 100);
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

    if (e.column.name == "sumPriceNow")
    {
        sumPriceNow = Number(e.value);

        if (num != 0)
        {
            priceNow = (sumPriceNow) / num;
        }
        else
        {
            num = 1;
            priceNow = sumPriceNow;
        }

        sumPriceDis = num * (price - priceNow);
        if (price != 0)
        {
            dis = priceNow / price * 100;
        }
        else
        {
            price = priceNow;
            dis = 0;
        }

        priceTax = tax * priceNow / 100;
        if (tax == 0)
        {
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

    if (e.column.name == "tax")
    {
        tax = Number(e.value);
        priceTax = priceNow * (1 + tax / 100);
        if (tax == 0)
        {
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

    if (e.column.name == "priceTax")
    {
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

    if (e.column.name == "sumPriceAll")
    {
        sumPriceAll = Number(e.value);

        if (num != 0)
        {
            priceTax = sumPriceAll / num;
        }

        priceNow = priceTax / (1 + tax / 100);
        priceNow = Math.round(priceNow * 10000) / 10000;
        sumPriceNow = priceNow * num;
        sumPriceNow = Math.round(sumPriceNow * 10000) / 10000;

        dis = priceNow / price * 100;
        if (dis == 100)
        {
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

function f_onBeforeEdit(e) {
    // left empty for now
}

function f_onBeforeSubmitEdit(e) {
    if (e.column.name == "dis")
    {
        if (e.value < 0 || e.value > 100) return false;
    }
    return true;
}

function deleteRow() {
    if (manager.rows.length == 1) {
        $.ligerDialog.warn('Keep at least one row！')
    }
    else {
        manager.deleteSelectedRow();
    }
}

var newrowid = 100;
function save() {
    var venderId = 0;

    var bizId = $("#ddlYWYList").val();
    if (bizId == 0) {
        $.ligerDialog.warn('Please select a receiver！');

        return;
    }

    var bizDate = $("#txtBizDate").val();
    if (bizDate == "") {
        $.ligerDialog.warn("Fill in the storage date！");
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
        $.ligerDialog.warn('Please Select Product！');

        return;
    }

    for (var i = 0; i < data.length; i++) {
        if (data[i].num <= 0 || data[i].num == "" || data[i].num == "0" || data[i].num == "0.00") {
            $.ligerDialog.warn("Please enter the quantity for row " + (i + 1) + "！");

            return;
            alert("Execution skipped!");
        }

        if (data[i].ckId == 0 || data[i].ckId == "" || data[i].ckId == "0" || data[i].ckName == "") {
            $.ligerDialog.warn("Please select the inventory for row " + (i + 1) + "！");

            return;
            alert("Execution skipped!");
        }
    }

    var headJson = {
        venderId: venderId,
        bizDate: bizDate,
        bizId: bizId,
        remarks: remarks
    };
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
        url: 'ashx/produceInListAdd.ashx',
        contentType: "application/json",
        //dataType: "json",
        data: JSON.stringify(postData),
        success: function (jsonResult) {
            if (jsonResult == "Operation Successful!") {

                $.ligerDialog.waitting('Operation Successful!'); setTimeout(function () {
                    $.ligerDialog.closeWaitting();
                    window.location.href = "produceInListAdd.aspx?id=0";
                }, 2000);
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

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




//新样式引入行

function f_selectContact() {
    $.ligerDialog.open({
        title: '选择商品', name: 'winselector', width: 840, height: 540, url: '../baseSet/GoodsListSelect.aspx', buttons: [
            { text: '确定', onclick: f_selectContactOK },
            { text: '取消', onclick: f_selectContactCancel }
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



//新样式引入行end






//扩展 numberbox 类型的格式化函数
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
                            h += "<a href='javascript:addNewRow()' title='新增行' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                            h += "<a href='javascript:deleteRow()' title='Delete row' style='float:left;'><div class='ui-icon ui-icon-trash'></div></a> ";
                            h += "<a href='javascript:f_selectContact()' title='选商品' style='float:left;'><div class='ui-icon ui-icon-search'></div></a> ";
                        }

                        return h;
                    }
                }
                ,

                {
                    display: '商品名称', name: 'goodsName', width: 250, align: 'left',

                    totalSummary:
                    {
                        type: 'count',
                        render: function (e) {  //Summary renderer, returns HTML to load into the cell
                            //e Summary Object (including sum, max, min, avg, count)
                            return 'Total:';
                        }
                    }


                },

                { display: '规格', name: 'spec', width: 100, align: 'center' },
                { display: '单位', name: 'unitName', width: 80, align: 'center' },

                {
                    display: '入库仓库', name: 'ckId', width: 120, isSort: false, textField: 'ckName',
                    editor: {
                        type: 'select',
                        url: "../baseSet/InventoryList.aspx?Action=GetDDLList&r=" + Math.random(), //Wating 125 pr
                        valueField: 'ckId', textField: 'ckName'
                    }

                },

                {
                    display: '数量', name: 'num', width: 100, type: 'float', align: 'right', editor: { type: 'float' },

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
                    display: '成本单价', name: 'price', width: 100, type: 'float', align: 'right', editor: { type: 'float' }

                },


                {
                    display: '成本金额', name: 'sumPriceNow', width: 120, type: 'float', align: 'right', editor: { type: 'float' },




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



                { display: '备注', name: 'remarks', width: 150, align: 'left', type: 'text', editor: { type: 'text' } },
                { display: '关联生产计划单号', name: 'sourceNumber', width: 150, align: 'left', type: 'text' }

            ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '350',
            url: 'produceInListAdd.aspx?Action=GetData&id=' + param,
            rownumbers: true,//Display serial number
            frozenRownumbers: true,//Is the row number in a fixed column
            dataAction: 'local',//Local sorting
            usePager: false,
            alternatingRow: false,

            totalSummary: true,
            enabledEdit: true, //Control whether editing is allowed
            // onBeforeEdit: f_onBeforeEdit,//控制如果还没选择商品，就不能编辑后面的列
            // onBeforeSubmitEdit: f_onBeforeSubmitEdit,//提交编辑之前检查

            //totalRender:f_totalRender,//汇总

            onAfterEdit: f_onAfterEdit //Actions after updating the cell
        }
        );
});

var rowNumber = 9;
//var itemSumPriceAll = 0;

function f_totalRender(data, currentPageData) {
    //return "总仓库数量:"+data.sumPriceAll; 
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
        priceTax: "",//含税单价
        sumPriceTax: "",//税额
        sumPriceAll: "",

        ckId: "",
        ckName: "",
        remarks: "",
        sourceNumber: "",
        itemId: 0

    });

    updateTotal();
}


//商品 改变事件: 获取单位、单价等信息
function f_onGoodsChanged(e) {


    if (!e || !e.length) return;

    //1、先更新当前行的后续数据

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
        priceTax: selected.priceCost,//含税单价、默认为不含税单价
        sumPriceTax: 0,//税额
        sumPriceAll: selected.priceCost,

        ckId: selected.ckId,
        ckName: selected.ckName,
        sourceNumber: "",
        itemId: 0,
        remarks: ""


    });



    if (e.length > 1) //如果有多行的、先删除空白行，然后插入下面
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
                price: e[i].priceCost,
                dis: 0,
                sumPriceDis: 0,
                priceNow: e[i].priceCost,
                sumPriceNow: e[i].priceCost,
                tax: 0,
                priceTax: e[i].priceCost,//含税单价
                sumPriceTax: 0,//税额
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

    var sumPriceItemDis = 0;//折扣金额
    var sumPriceItemNow = 0;//未税金额
    var sumPriceItemTax = 0;//税额
    var sumPriceItemAll = 0;//价税合计

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


    //03、价税合计
    $("#sumPriceItemNow").html(formatCurrency(sumPriceItemNow));



}

//编辑后事件 
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

    //数量改变---开始
    if (e.column.name == "num") {

        num = Number(e.value);

        //0、折扣金额=数量*单价*折扣/100
        sumPriceDis = Number(num) * Number(price) * Number(dis) / 100;
        sumPriceDis = Math.round(sumPriceDis * 10000) / 10000;

        //1、金额=数量*单价
        sumPriceNow = Number(num) * Number(priceNow);
        sumPriceNow = Math.round(sumPriceNow * 10000) / 10000;

        //2、税额=数量*单价*税率/100
        sumPriceTax = Number(num) * Number(priceNow) * Number(tax) / 100;
        sumPriceTax = Math.round(sumPriceTax * 10000) / 10000;

        //3、价税合计=数量*含税单价
        sumPriceAll = Number(num) * Number(priceTax);
        sumPriceAll = Math.round(sumPriceAll * 10000) / 10000;



        //开始赋值
        manager.updateCell("num", num, e.record);

        //0、折扣金额
        manager.updateCell('sumPriceDis', sumPriceDis.toString(), e.record);
        //1、金额
        manager.updateCell('sumPriceNow', sumPriceNow.toString(), e.record);
        //2、税额
        manager.updateCell('sumPriceTax', sumPriceTax.toString(), e.record);
        //3、价税合计
        manager.updateCell('sumPriceAll', sumPriceAll.toString(), e.record);


    } //数量改变---结束

    if (e.column.name == "price") //单价改变---开始
    {

        price = Number(e.value);

        sumPriceDis = num * price * dis / 100;


        priceNow = price * (1 + dis / 100);
        sumPriceNow = num * priceNow;
        sumPriceNow = Math.round(sumPriceNow * 10000) / 10000;

        priceTax = priceNow * (1 + tax / 100);
        sumPriceTax = num * priceNow * tax / 100;//税额=数量*现价*税率/100；

        sumPriceAll = num * priceTax;

        //格式化
        sumPriceDis = Math.round(sumPriceDis * 10000) / 10000;
        sumPriceNow = Math.round(sumPriceNow * 10000) / 10000;
        sumPriceTax = Math.round(sumPriceTax * 10000) / 10000;
        sumPriceAll = Math.round(sumPriceAll * 10000) / 10000;



        //开始赋值

        manager.updateCell("price", price, e.record);
        manager.updateCell("priceNow", priceNow, e.record);
        manager.updateCell("priceTax", priceTax, e.record);


        //0、折扣金额
        manager.updateCell('sumPriceDis', sumPriceDis.toString(), e.record);
        //1、金额
        manager.updateCell('sumPriceNow', sumPriceNow.toString(), e.record);
        //2、税额
        manager.updateCell('sumPriceTax', sumPriceTax.toString(), e.record);
        //3、价税合计
        manager.updateCell('sumPriceAll', sumPriceAll.toString(), e.record);



    } //单价改变---结束

    if (e.column.name == "dis") //折扣改变---开始
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
        sumPriceTax = num * priceNow * tax / 100;//税额=数量*现价*税率/100；
        sumPriceAll = num * priceTax;

        //格式化
        sumPriceDis = Math.round(sumPriceDis * 10000) / 10000;
        sumPriceNow = Math.round(sumPriceNow * 10000) / 10000;
        sumPriceTax = Math.round(sumPriceTax * 10000) / 10000;
        sumPriceAll = Math.round(sumPriceAll * 10000) / 10000;



        //开始赋值

        manager.updateCell("dis", dis, e.record);
        manager.updateCell("priceNow", priceNow, e.record);
        manager.updateCell("priceTax", priceTax, e.record);


        //0、折扣金额
        manager.updateCell('sumPriceDis', sumPriceDis.toString(), e.record);
        //1、金额
        manager.updateCell('sumPriceNow', sumPriceNow.toString(), e.record);
        //2、税额
        manager.updateCell('sumPriceTax', sumPriceTax.toString(), e.record);
        //3、价税合计
        manager.updateCell('sumPriceAll', sumPriceAll.toString(), e.record);



    } //折扣改变---结束

    if (e.column.name == "sumPriceDis") //折扣金额改变---开始
    {

        sumPriceDis = Number(e.value);

        if (sumPriceDis >= num * price) {
            alert("请填写正确的折扣金额!");
            return;
        }

        if (num * price != 0) {
            dis = (1 - sumPriceDis / (num * price)) * 100;
        }
        else {
            alert("请填写数量和单价!");
            return;
        }

        priceNow = price * dis / 100;
        sumPriceNow = num * priceNow;
        sumPriceNow = Math.round(sumPriceNow * 10000) / 10000;

        priceTax = priceNow * (1 + tax / 100);
        sumPriceTax = num * priceNow * tax / 100;//税额=数量*现价*税率/100；

        sumPriceAll = priceTax * num;
        sumPriceAll = Math.round(sumPriceAll * 10000) / 10000;



        //格式化
        sumPriceDis = Math.round(sumPriceDis * 10000) / 10000;
        sumPriceNow = Math.round(sumPriceNow * 10000) / 10000;
        sumPriceTax = Math.round(sumPriceTax * 10000) / 10000;
        sumPriceAll = Math.round(sumPriceAll * 10000) / 10000;



        //开始赋值

        manager.updateCell("dis", dis, e.record);
        manager.updateCell("priceNow", priceNow, e.record);
        manager.updateCell("priceTax", priceTax, e.record);


        //0、折扣金额
        manager.updateCell('sumPriceDis', sumPriceDis.toString(), e.record);
        //1、金额
        manager.updateCell('sumPriceNow', sumPriceNow.toString(), e.record);
        //2、税额
        manager.updateCell('sumPriceTax', sumPriceTax.toString(), e.record);
        //3、价税合计
        manager.updateCell('sumPriceAll', sumPriceAll.toString(), e.record);



    } //折扣金额改变---结束

    if (e.column.name == "priceNow") //现价改变---开始
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
        sumPriceTax = num * priceNow * tax / 100;//税额=数量*现价*税率/100；

        sumPriceAll = priceTax * num;

        //格式化
        sumPriceDis = Math.round(sumPriceDis * 10000) / 10000;
        sumPriceNow = Math.round(sumPriceNow * 10000) / 10000;
        sumPriceTax = Math.round(sumPriceTax * 10000) / 10000;
        sumPriceAll = Math.round(sumPriceAll * 10000) / 10000;



        //开始赋值

        manager.updateCell("price", price, e.record);
        manager.updateCell("dis", dis, e.record);
        manager.updateCell("priceNow", priceNow, e.record);
        manager.updateCell("priceTax", priceTax, e.record);


        //0、折扣金额
        manager.updateCell('sumPriceDis', sumPriceDis.toString(), e.record);
        //1、金额
        manager.updateCell('sumPriceNow', sumPriceNow.toString(), e.record);
        //2、税额
        manager.updateCell('sumPriceTax', sumPriceTax.toString(), e.record);
        //3、价税合计
        manager.updateCell('sumPriceAll', sumPriceAll.toString(), e.record);



    } //现价改变---结束


    if (e.column.name == "sumPriceNow") //金额改变
    {
        //金额改变: 【数量、折扣额、税率】 计算【折扣率、单价、税额、价税合计】   

        sumPriceNow = Number(e.value);

        //1、计算现价

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


        //2、计算含税单价
        priceTax = tax * priceNow / 100;
        if (tax == 0) {
            priceTax = priceNow;
        }


        price = Math.round(price * 10000) / 10000;
        priceTax = Math.round(priceTax * 10000) / 10000;
        sumPriceNow = Math.round(sumPriceNow * 10000) / 10000;

        //2、税额=数量*单价*税率/100
        sumPriceTax = Number(num) * Number(price) * Number(tax) / 100;
        sumPriceTax = Math.round(sumPriceTax * 10000) / 10000;

        //2、价税合计=数量*含税单价
        sumPriceAll = Number(num) * Number(priceTax);
        sumPriceAll = Math.round(sumPriceAll * 10000) / 10000;


        //开始赋值


        //1、变量

        manager.updateCell("price", price, e.record);
        manager.updateCell("dis", dis, e.record);
        manager.updateCell("priceNow", priceNow, e.record);
        manager.updateCell("priceTax", priceTax, e.record);


        //2、金额
        manager.updateCell('sumPriceDis', sumPriceDis, e.record);
        manager.updateCell('sumPriceNow', sumPriceNow, e.record);
        manager.updateCell('sumPriceTax', sumPriceTax, e.record);
        manager.updateCell('sumPriceAll', sumPriceAll, e.record);



    } //金额改变---结束


    if (e.column.name == "tax") //税率改变
    {
        //金额改变: 【数量、折扣额、税率】 计算【折扣率、单价、税额、价税合计】   

        tax = Number(e.value);

        //1、计算含税单价

        priceTax = priceNow * (1 + tax / 100);
        if (tax == 0) {
            priceTax = priceNow;
        }



        priceTax = Math.round(priceTax * 10000) / 10000;


        //2、税额=数量*单价*税率/100
        sumPriceTax = Number(num) * Number(priceNow) * Number(tax) / 100;
        sumPriceTax = Math.round(sumPriceTax * 10000) / 10000;

        //2、价税合计=数量*含税单价
        sumPriceAll = Number(num) * Number(priceTax);
        sumPriceAll = Math.round(sumPriceAll * 10000) / 10000;


        //开始赋值


        //1、变量


        manager.updateCell("priceTax", priceTax, e.record);



        //2、税额
        manager.updateCell('sumPriceTax', sumPriceTax.toString(), e.record);
        //3、价税合计
        manager.updateCell('sumPriceAll', sumPriceAll.toString(), e.record);



    } //税率改变---结束

    if (e.column.name == "priceTax") //含税单价改变
    {
        //金额改变: 【数量、折扣额、税率】 计算【折扣率、单价、税额、价税合计】   

        priceTax = Number(e.value);

        //1、计算单价

        priceNow = priceTax / (1 + tax / 100);
        priceNow = Math.round(priceNow * 10000) / 10000;
        priceTax = Math.round(priceTax * 10000) / 10000;


        dis = priceNow / price * 100;
        if (dis == 100) {
            dis = 0;
        }
        dis = Math.round(dis * 10000) / 10000;

        //1、折扣=数量*单价*税率/100
        sumPriceDis = num * (price - priceNow);
        sumPriceDis = Math.round(sumPriceDis * 10000) / 10000;


        //1、折扣=数量*单价*税率/100
        sumPriceNow = num * priceNow;
        sumPriceNow = Math.round(sumPriceNow * 10000) / 10000;


        //2、税额=数量*单价*税率/100
        sumPriceTax = Number(num) * Number(price) * Number(tax) / 100;
        sumPriceTax = Math.round(sumPriceTax * 10000) / 10000;

        //2、价税合计=数量*含税单价
        sumPriceAll = Number(num) * Number(priceTax);
        sumPriceAll = Math.round(sumPriceAll * 10000) / 10000;


        //开始赋值


        //1、变量

        manager.updateCell("dis", dis, e.record);
        manager.updateCell("priceNow", priceNow, e.record);
        manager.updateCell("priceTax", priceTax, e.record);


        //1、折扣
        manager.updateCell('sumPriceDis', sumPriceDis, e.record);
        //2、金额
        manager.updateCell('sumPriceNow', sumPriceNow, e.record);
        //3、税额
        manager.updateCell('sumPriceTax', sumPriceTax.toString(), e.record);
        //4、价税合计
        manager.updateCell('sumPriceAll', sumPriceAll.toString(), e.record);



    } //含税单价改变---结束


    if (e.column.name == "sumPriceAll") //价税合计改变
    {
        //金额改变: 【数量、折扣额、税率】 计算【折扣率、单价、税额、价税合计】   

        sumPriceAll = Number(e.value);

        //1、计算含税单价
        if (num != 0) {
            priceTax = sumPriceAll / num;
        }

        //2、计算未税单价
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

        //2、税额=数量*单价*税率/100
        sumPriceTax = Number(num) * Number(price) * Number(tax) / 100;
        sumPriceTax = Math.round(sumPriceTax * 10000) / 10000;

        //2、价税合计=数量*含税单价
        sumPriceAll = Number(num) * Number(priceTax);
        sumPriceAll = Math.round(sumPriceAll * 10000) / 10000;


        //开始赋值


        //1、变量


        manager.updateCell("priceNow", priceNow, e.record);
        manager.updateCell("dis", dis, e.record);
        manager.updateCell("priceTax", priceTax, e.record);


        //2、金额
        manager.updateCell('sumPriceDis', sumPriceDis, e.record);
        //2、金额
        manager.updateCell('sumPriceNow', sumPriceNow, e.record);
        //2、税额
        manager.updateCell('sumPriceTax', sumPriceTax, e.record);
        //3、价税合计
        manager.updateCell('sumPriceAll', sumPriceAll, e.record);



    } //价税合计改变---结束


    //最后改变汇总行的值

    updateTotal();


}



//只允许编辑已经添加商品的行
function f_onBeforeEdit(e) {

    //            if(e.data.goodsId!="" && e.data.goodsName!="") return true;
    //            return false;
    //            
    //            if(e.rowindex<=2) return true;
    //            return false;




}
//限制折扣、税率范围

function f_onBeforeSubmitEdit(e) {
    if (e.column.name == "dis") {
        if (e.value < 0 || e.value > 100) return false;
    }




    return true;
}


function deleteRow() {

    if (manager.rows.length == 1) {
        $.ligerDialog.warn('至少保留一行!')

    }
    else {
        manager.deleteSelectedRow();
    }

}
var newrowid = 100;




function save() {


    var venderId = 0;  //获取Select选择的Value
    var bizId = $("#ddlYWYList").val();



    if (bizId == 0) {
        $.ligerDialog.warn('请选择入库人!');

        return;
    }



    var bizDate = $("#txtBizDate").val();
    if (bizDate == "") {
        $.ligerDialog.warn("填写入库日期!");
        return;

    }




    var remarks = $("#txtRemarks").val();



    //先删掉空白行


    var data = manager.getData();


    //1, First delete the blank rows
    for (var i = data.length - 1; i >= 0; i--) {
        if (data[i].goodsId == 0 || data[i].goodsId == "" || data[i].goodsName == "") {
            data.splice(i, 1);

        }

    }


    //2, Check if a product is selected
    if (data.length == 0) {
        $.ligerDialog.warn('Please Select Product!');

        return;
    }



    //3, Check if the quantity for all products has been entered
    for (var i = 0; i < data.length; i++) {
        if (data[i].num <= 0 || data[i].num == "" || data[i].num == "0" || data[i].num == "0.00") {

            $.ligerDialog.warn("请输入第" + (i + 1) + "行的商品数量!");

            return;
            alert("Execution skipped!");
        }


        if (data[i].ckId == 0 || data[i].ckId == "" || data[i].ckId == "0" || data[i].ckName == "") {

            $.ligerDialog.warn("请输入第" + (i + 1) + "行的仓库!");

            return;
            alert("Execution skipped!");
        }\
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




    list = list.substring(0, list.length - 1);//去掉最后一个花括号

    list += ",\"Rows\":";
    list += JSON.stringify(data);
    list += "}";



    var postData = JSON.parse(list);//最终的json


    //       alert(JSON.stringify(postData));
    //        
    //         return;


    $.ajax({
        type: "POST",
        url: 'ashx/produceInListAdd.ashx',
        contentType: "application/json", //必须有
        //dataType: "json", //表示返回值类型，不必须
        data: JSON.stringify(postData),  //相当于 //data: "{'str1':'foovalue', 'str2':'barvalue'}",
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
    //可以加上错误处理
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
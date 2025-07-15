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

var itemType = "main";

function f_selectContact(type) {
    if (type == 0) {
        itemType = "main";
    }
    if (type == 1) {
        itemType = "sub";
    }

    $.ligerDialog.open({
        title: 'Select Item', name: 'winselector', width: 840, height: 540, url: '../baseSet/GoodsListSelect.aspx', buttons: [
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
        alert('Please select row!');
        return;
    }

    if (itemType == "main") {
        f_onGoodsChanged(data);
    }
    if (itemType == "sub") {
        f_onGoodsChangedSub(data);
    }
    dialog.close();
}

function f_selectContactCancel(item, dialog) {
    dialog.close();
}

//扩展 numberbox 类型的格式化函数
$.ligerDefaults.Grid.formatters['numberbox'] = function (value, column) {
    var precision = column.editor.precision;
    return value.toFixed(precision);
};

var manager;
$(function () {
    var form = $("#form").ligerForm();
    window['g'] =
        manager = $("#maingrid").ligerGrid({
            columns: [
                {
                    display: '', isSort: false, width: 60, align: 'center', frozen: true, render: function (rowdata, rowindex, value) {
                        var h = "";
                        if (!rowdata._editing) {
                            h += "<a href='javascript:f_selectContact(0)' title='Select Product'><div class='ui-icon ui-icon-search' style='margin:0 auto;'></div></a> ";
                        }
                        return h;
                    }
                },
                {
                    display: 'Item Name', name: 'goodsName', width: 250, align: 'left',
                    totalSummary:
                    {
                        type: 'count',
                        render: function (e) {  //汇总渲染器，返回html加载到单元格
                            return 'Total: ';
                        }
                    }
                },
                {
                    display: 'Specification', name: 'spec', width: 100, align: 'center'
                },
                {
                    display: 'Unit', name: 'unitName', width: 80, align: 'center'
                },
                {
                    display: 'Stock In Quantity', name: 'num', width: 100, type: 'float', align: 'right', editor: { type: 'float' }
                },
                {
                    display: 'Stock In Unit Price', name: 'price', width: 90, type: 'float', align: 'right', editor: { type: 'float', precision: 4 }
                },
                {
                    display: 'Amount', name: 'sumPrice', width: 80, type: 'float', align: 'right', editor: { type: 'float' }
                },
                {
                    display: 'Stock-in Warehouse', name: 'ckId', width: 120, isSort: false, textField: 'ckName',
                    editor: {
                        type: 'select',
                        url: "../baseSet/InventoryList.aspx?Action=GetDDLList&r=" + Math.random(),
                        valueField: 'ckId', textField: 'ckName'
                    }
                },
                {
                    display: 'Remarks', name: 'remarks', width: 150, align: 'left', type: 'text', editor: { type: 'text' }
                }
            ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '110',
            url: 'AssembleListEdit.aspx?Action=GetData&id=' + getUrlParam("id"),
            rownumbers: true,//显示序号
            frozenRownumbers: true,//行序号是否在固定列中
            dataAction: 'local',//本地排序
            usePager: false,
            alternatingRow: false,
            totalSummary: false,
            enabledEdit: true, //控制能否编辑的
            onAfterEdit: f_onAfterEdit //更新单元格后的操作
        }
        );
});

var managersub;
$(function () {
    window['gsub'] =
        managersub = $("#maingridsub").ligerGrid({
            columns: [
                {
                    display: '', isSort: false, width: 60, align: 'center', frozen: true, render: function (rowdata, rowindex, value) {
                        var h = "";
                        if (!rowdata._editing) {
                            h += "<a href='javascript:addNewRow()' title='Add' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                            h += "<a href='javascript:deleteRow()' title='Delete' style='float:left;'><div class='ui-icon ui-icon-trash'></div></a> ";
                            h += "<a href='javascript:f_selectContact(1)' title='Select' style='float:left;'><div class='ui-icon ui-icon-search'></div></a> ";
                        }
                        return h;
                    }
                },
                {
                    display: 'Item Name', name: 'goodsName', width: 250, align: 'left',
                    totalSummary:
                    {
                        type: 'count',
                        render: function (e) {  //汇总渲染器，返回html加载到单元格
                            //e 汇总Object(包括sum,max,min,avg,count) 
                            return 'Total: ';
                        }
                    }
                },
                {
                    display: 'Specification', name: 'spec', width: 100, align: 'center'
                },
                {
                    display: 'Unit', name: 'unitName', width: 80, align: 'center'
                },
                {
                    display: 'Outbound Quantity', name: 'num', width: 100, type: 'float', align: 'right', editor: { type: 'float' },
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
                    display: 'Outbound Unit Price', name: 'price', width: 90, type: 'float', align: 'right', editor: { type: 'float', precision: 4 }
                },
                {
                    display: 'Amount', name: 'sumPrice', width: 80, type: 'float', align: 'right', editor: { type: 'float' },
                    totalSummary:
                    {
                        align: 'center',
                        type: 'sum',
                        render: function (e) {
                            var itemSumPrice = e.sum;
                            return "<span id='sumPriceItem'>" + Math.round(itemSumPrice * 10000) / 10000 + "</span>";
                        }
                    }
                },
                {
                    display: 'Outbound Warehouse', name: 'ckId', width: 120, isSort: false, textField: 'ckName',
                    editor: {
                        type: 'select',
                        url: "../baseSet/InventoryList.aspx?Action=GetDDLList&r=" + Math.random(),
                        valueField: 'ckId', textField: 'ckName'
                    }
                },
                {
                    display: 'Remarks', name: 'remarks', width: 150, align: 'left', type: 'text', editor: { type: 'text' }
                }
            ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '320',
            url: 'AssembleListEdit.aspx?Action=GetDataSub&id=' + getUrlParam("id"),
            rownumbers: true,
            frozenRownumbers: true,
            dataAction: 'local',
            usePager: false,
            alternatingRow: false,
            totalSummary: true,
            enabledEdit: true,
            onAfterEdit: f_onAfterEditSub
        }
        );
});

function updateTotal() {
    var data = managersub.getData();
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

function f_totalRender(data, currentPageData) {
    //return "总仓库数量:"+data.sumPriceAll; 
}

function setCangku() {
    $.ligerDialog.open({ target: $("#target1") });
}

function selectCangku() {
    var ckName = $("#ddlCangkuList").find("option:selected").text();
    var ckId = $("#ddlCangkuList").val();
    alert(ckName);
    alert(ckId);

    var grid = liger.get("maingrid");
    var data = manager.getData();

    alert(data.length);

    for (var i = 0; i < data.length; i++) {
        alert(data[i].goodsId);

        grid.updateCell("ckId", ckId, i);
        grid.updateCell("ckName", ckName, i);
    }

    $(".l-dialog,.l-window-mask").remove();

    $.ligerDialog.close(); //关闭dialog
}

//商品 改变事件: 获取单位、单价等信息
function f_onGoodsChanged(e) {

    if (!e || !e.length) return;

    //1、先更新当前行的后续数据

    var grid = liger.get("maingrid");

    var selected = e[0];

    var selectedRow = manager.getSelected();

    grid.updateRow(selectedRow, {
        goodsId: selected.id,
        goodsName: selected.names,
        unitName: selected.unitName,
        num: 1,
        price: selected.priceCost,
        spec: selected.spec,
        sumPrice: selected.priceCost,
        ckId: selected.ckId,
        ckName: selected.ckName,
        remarks: ""
    });
}

//商品 改变事件: 获取单位、单价等信息
function f_onGoodsChangedSub(e) {

    if (!e || !e.length) return;

    //1、先更新当前行的后续数据

    var grid = liger.get("maingridsub");

    var selected = e[0];

    var selectedRow = managersub.getSelected();

    grid.updateRow(selectedRow, {
        goodsId: selected.id,
        goodsName: selected.names,
        unitName: selected.unitName,
        num: 1,
        price: selected.priceCost,
        spec: selected.spec,
        sumPrice: selected.priceCost,
        ckId: selected.ckId,
        ckName: selected.ckName,
        remarks: ""
    });

    if (e.length > 1) {
        var data = managersub.getData();
        for (var i = data.length - 1; i >= 0; i--) {
            if (data[i].goodsId == 0 || data[i].goodsName == "") {
                managersub.deleteRow(i);
            }
        }

        for (var i = 1; i < e.length; i++) {
            grid.addRow({
                id: rowNumber,
                goodsId: e[i].id,
                goodsName: e[i].names,
                unitName: e[i].unitName,
                num: 1,
                price: e[i].priceCost,
                spec: e[i].spec,
                sumPrice: e[i].priceCost,
                ckId: e[i].ckId,
                ckName: e[i].ckName,
                remarks: ""
            });

            rowNumber = rowNumber + 1;
        }
    }
    updateTotal();
}

function f_createCityData(e) {
    var Country = e.record.Country;
    var options = {
        data: getCityData(Country)
    };
    return options;
}

function f_onSelected(e) {
    if (!e.data || !e.data.length) return;

    var grid = liger.get("maingrid");

    var selected = e.data[0];
    grid.updateRow(grid.lastEditRow, {
        CustomerID: selected.CustomerID,
        CompanyName: selected.CompanyName
    });

    var out = JSON.stringify(selected);
    $("#message").html('Last Select:' + out);
}

//编辑后事件 
function f_onAfterEdit(e) {
    var num, price, sumPrice;

    num = Number(e.record.num);

    price = Number(e.record.price);

    sumPrice = Number(e.record.sumPrice);

    var goodsId, goodsName;

    goodsId = e.record.goodsId;
    goodsName = e.record.goodsName;

    if (goodsId == "" || goodsName == "") {
        return;
    }

    if (e.column.name == "num") {
        num = Number(e.value);

        sumPrice = Number(num) * Number(price);

        num = Math.round(num * 100) / 100;
        price = Math.round(price * 100) / 100;

        sumPrice = Math.round(sumPrice * 100) / 100;

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

    if (e.column.name == "sumPrice") {
        sumPrice = Number(e.value);

        if (num != 0) {
            price = (sumPrice) / num;
        }
        else {
            price = 0;
        }

        num = Math.round(num * 100) / 100;
        price = Math.round(price * 100) / 100;
        sumPrice = Math.round(sumPrice * 100) / 100;

        manager.updateCell("price", price, e.record);

        manager.updateCell('sumPrice', sumPrice, e.record);

    }
}

//编辑后事件 
function f_onAfterEditSub(e) {
    var num, price, sumPrice;

    num = Number(e.record.num);

    price = Number(e.record.price);

    sumPrice = Number(e.record.sumPrice);

    var goodsId, goodsName;

    goodsId = e.record.goodsId;
    goodsName = e.record.goodsName;

    if (goodsId == "" || goodsName == "") {
        return;
    }

    if (e.column.name == "num") //数量改变---开始
    {
        //数量改变: 【折扣率、税率】 计算【折扣额、金额、税额、价税合计】
        num = Number(e.value);

        //2、金额=数量*单价-折扣额
        sumPrice = Number(num) * Number(price);

        num = Math.round(num * 100) / 100;
        price = Math.round(price * 100) / 100;

        sumPrice = Math.round(sumPrice * 100) / 100;

        //开始赋值

        managersub.updateCell("num", num, e.record);

        //2、金额
        managersub.updateCell('sumPrice', sumPrice, e.record);

    } //数量改变---结束

    if (e.column.name == "price") //单价改变---开始、计算金额、折扣额、税额、价税合计
    {
        //单价改变: 【数量、折扣率、税率】 计算【折扣额、金额、税额、价税合计】; 
        price = Number(e.value);

        //2、金额=数量*单价-折扣额
        sumPrice = Number(num) * Number(price);
        num = Math.round(num * 100) / 100;
        price = Math.round(price * 100) / 100;
        sumPrice = Math.round(sumPrice * 100) / 100;

        //开始赋值

        //1、折扣额

        managersub.updateCell("price", price, e.record);

        //2、金额
        managersub.updateCell('sumPrice', sumPrice, e.record);

    } //单价改变---结束

    if (e.column.name == "sumPrice") //金额改变
    {
        //金额改变: 【数量、折扣额、税率】 计算【折扣率、单价、税额、价税合计】   

        sumPrice = Number(e.value);

        //1、计算单价
        if (num != 0) {
            price = (sumPrice) / num;
        }
        else {
            price = 0;
        }

        num = Math.round(num * 100) / 100;
        price = Math.round(price * 100) / 100;

        sumPrice = Math.round(sumPrice * 100) / 100;

        //开始赋值

        //1、单价
        managersub.updateCell("price", price, e.record);

        //2、折扣率
        managersub.updateCell('sumPrice', sumPrice, e.record);

    } //金额改变---结束

    //最后改变汇总行的值

    updateTotal();

}

function f_onBeforeEdit(e) {
}

function f_onBeforeSubmitEdit(e) {
    if (e.column.name == "dis") {
        if (e.value < 0 || e.value > 100) return false;
    }

    if (e.column.name == "tax") {
        if (e.value < 0 || e.value > 100) return false;
    }
    return true;
}

function beginEdit() {
    var row = manager.getSelectedRow();
    if (!row) { alert('Please select行'); return; }
    manager.beginEdit(row);
}

function cancelEdit() {
    var row = manager.getSelectedRow();
    if (!row) { alert('Please select行'); return; }
    manager.cancelEdit(row);
}

function cancelAllEdit() {
    manager.cancelEdit();
}

function endEdit() {
    var row = manager.getSelectedRow();
    if (!row) { alert('Please select row'); return; }
    manager.endEdit(row);
}

function endAllEdit() {
    manager.endEdit();
}
function deleteRow() {
    if (managersub.rows.length == 1) {
        $.ligerDialog.warn('Keep at least one row!')

    }
    else {
        managersub.deleteSelectedRow();
    }
}

var newrowid = 100;

function addNewRow() {
    var gridData = managersub.getData();
    var rowNum = gridData.length;

    managersub.addRow({
        id: rowNum + 1,
        id: rowNum + 1,
        goodsId: "",
        goodsName: "",
        unitName: "",
        num: "",
        spec: "",
        sumPrice: "",
        ckId: "",
        ckName: "",
        remarks: ""
    });
}

function updateRow() {
    var selected = manager.getSelected();
    if (!selected) {
        alert('Please select row'); return;
    }
}

function getSelected() {
    var row = manager.getSelectedRow();
    if (!row) {
        alert('Please select row'); return;
    }
    alert(JSON.stringify(row));
}

function getData() {
    var data = manager.getData();
    alert(JSON.stringify(data));
}

function save() {
    var data = manager.getData();

    //1、先删掉空白行
    for (var i = data.length - 1; i >= 0; i--) {
        if (data[i].goodsId == 0 || data[i].goodsName == "") {
            data.splice(i, 1);

        }

    }

    //2、判断是否选择商品
    if (data.length == 0) {
        $.ligerDialog.warn('Please select item to assemble!');

        return;
    }

    //3、判断商品数量是否都输入了。
    for (var i = 0; i < data.length; i++) {
        if (data[i].num <= 0 || data[i].num == "" || data[i].num == "0" || data[i].num == "0.00") {

            $.ligerDialog.warn("Please fill in " + (i + 1) + "th row item count");
            return;
        }

        if (data[i].ckId == 0 || data[i].ckId == "" || data[i].ckId == "0" || data[i].ckName == "") {

            $.ligerDialog.warn("Please fill in " + (i + 1) + "th row item's warehouse");

            return;
        }
    }

    var goodsId = data[0].goodsId;
    var num = data[0].num;
    var price = data[0].price;
    var ckId = data[0].ckId;
    var remarksItem = data[0].remarks;
    var datasub = managersub.getData();

    //1、先删掉空白行
    for (var i = datasub.length - 1; i >= 0; i--) {
        if (datasub[i].goodsId == 0 || datasub[i].goodsName == "") {
            datasub.splice(i, 1);
        }
    }

    //2、判断是否选择商品
    if (datasub.length == 0) {
        $.ligerDialog.warn('Please select assembled item!');

        return;
    }

    //3、判断商品数量是否都输入了。
    for (var i = 0; i < datasub.length; i++) {
        if (datasub[i].num <= 0 || datasub[i].num == "" || datasub[i].num == "0" || datasub[i].num == "0.00") {
            $.ligerDialog.warn("Please fill in " + (i + 1) + "th row item count!");

            return;
        }

        if (datasub[i].ckId == 0 || datasub[i].ckId == "" || datasub[i].ckId == "0" || datasub[i].ckName == "") {
            $.ligerDialog.warn("Please fill in " + (i + 1) + "th row item's warehouse!");

            return;
        }
    }

    var bizDate = $("#txtBizDate").val();
    if (bizDate == "") {
        $.ligerDialog.warn("Please fill the assemble date!");
        return;
    }

    var remarks = $("#txtRemarks").val();
    var fee = $("#txtFee").val() == "" ? 0 : $("#txtFee").val();
    var headJson = { id: getUrlParam("id"), fee: fee, bizDate: bizDate, remarks: remarks, goodsId: goodsId, num: num, price: price, ckId: ckId, remarksItem: remarksItem };

    var dataNew = [];
    dataNew.push(headJson);

    var list = JSON.stringify(headJson);
    list = list.substring(0, list.length - 1);
    list += ",\"Rows\":";
    list += JSON.stringify(datasub);
    list += "}";

    var postData = JSON.parse(list);

    $.ajax({
        type: "POST",
        url: 'ashx/AssembleListEdit.ashx',
        contentType: "application/json",
        data: JSON.stringify(postData),
        success: function (jsonResult) {

            if (jsonResult == "Execution successful!") {

                $.ligerDialog.waitting('Execution successful!'); setTimeout(function () { $.ligerDialog.closeWaitting(); location.reload(); }, 2000);

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

function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");

    var r = window.location.search.substr(1).match(reg);

    if (r != null) return unescape(r[2]); return null;
}


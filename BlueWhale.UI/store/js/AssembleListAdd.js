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

    else if (type == 1) {
        itemType = "sub";
    }

    $.ligerDialog.open({
        title: 'Select Product', name: 'winselector', width: 840, height: 540, url: '../baseSet/GoodsListSelect.aspx', buttons: [
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
                { display: 'Specification', name: 'spec', width: 100, align: 'center' },
                { display: 'UnitName', name: 'unitName', width: 80, align: 'center' },
                { display: 'Number', name: 'num', width: 80, type: 'float', align: 'right', editor: { type: 'float' } },
                { display: 'Price', name: 'price', width: 70, type: 'float', align: 'right', editor: { type: 'float', precision: 4 } },
                { display: 'Total Price', name: 'sumPrice', width: 80, type: 'float', align: 'right', editor: { type: 'float' } },
                {
                    display: 'Inbound Warehouse', name: 'ckId', width: 150, isSort: false, textField: 'ckName',
                    editor: {
                        type: 'select',
                        url: "../baseSet/InventoryList.aspx?Action=GetDDLList&r=" + Math.random(),
                        valueField: 'ckId', textField: 'ckName'
                    }
                },
                { display: 'Remarks', name: 'remarks', width: 150, align: 'left', type: 'text', editor: { type: 'text' } }
            ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '106',
            url: 'AssembleListAdd.aspx?Action=GetData',
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

var managersub;

$(function () {
    window['gsub'] =
        managersub = $("#maingridsub").ligerGrid({
            columns: [
                {
                    display: '', isSort: false, width: 60, align: 'center', frozen: true, render: function (rowdata, rowindex, value) {
                        var h = "";
                        if (!rowdata._editing) {
                            h += "<a href='javascript:addNewRow()' title='Add Row' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                            h += "<a href='javascript:deleteRow()' title='Delete row' style='float:left;'><div class='ui-icon ui-icon-trash'></div></a> ";
                            h += "<a href='javascript:f_selectContact(1)' title='Select Product' style='float:left;'><div class='ui-icon ui-icon-search'></div></a> ";
                        }
                        return h;
                    }
                },
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
                { display: 'Specification', name: 'spec', width: 100, align: 'center' },
                { display: 'UnitName', name: 'unitName', width: 80, align: 'center' },
                {
                    display: 'Number', name: 'num', width: 80, type: 'float', align: 'right', editor: { type: 'float' },
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
                    display: 'Price', name: 'price', width: 70, type: 'float', align: 'right', editor: { type: 'float', precision: 4 }
                },
                {
                    display: 'Total Price', name: 'sumPrice', width: 80, type: 'float', align: 'right', editor: { type: 'float' },
                    totalSummary:
                    {
                        align: 'center',   //Summary cell content alignment: left/center/right
                        type: 'sum',
                        render: function (e) {  //Summary renderer, returns HTML to load into the cell
                            var itemSumPrice = e.sum;
                            return "<span id='sumPriceItem'>" + Math.round(itemSumPrice * 10000) / 10000 + "</span>";//formatCurrency(suminf.sum)
                        }
                    }
                },
                {
                    display: 'Outbound Warehouse', name: 'ckId', width: 150, isSort: false, textField: 'ckName',
                    editor: {
                        type: 'select',
                        url: "../baseSet/InventoryList.aspx?Action=GetDDLList&r=" + Math.random(),
                        valueField: 'ckId', textField: 'ckName'
                    }

                },
                { display: 'Remarks', name: 'remarks', width: 150, align: 'left', type: 'text', editor: { type: 'text' } }
            ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '311',
            url: 'AssembleListAdd.aspx?Action=GetDataSub',
            rownumbers: true,//Display serial number
            frozenRownumbers: true,//Is the row number in a fixed column
            dataAction: 'local',//Local sorting
            usePager: false,
            alternatingRow: false,
            totalSummary: true,
            enabledEdit: true, //Control whether editing is allowed
            onAfterEdit: f_onAfterEditSub //Actions after updating the cell
        }
        );
});

function updateTotal() {
    var data = managersub.getData();//getData
    var sumPriceItem = 0;//

    //1, First delete the blank rows
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
    var ckName = $("#ddlCangkuList").find("option:selected").text();  //获取Select选择的Text
    var ckId = $("#ddlCangkuList").val();  //获取Select选择的Value

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



//商品 改变事件：获取UnitName、单价等信息
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

//商品 改变事件：获取UnitName、单价等信息
function f_onGoodsChangedSub(e) {
    if (!e || !e.length) return;

    //1、先更新当前行的后续数据

    var grid = liger.get("maingridsub");

    var selected = e[0];// e.data[0]; 

    // alert(selected.names);

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

    if (e.length > 1) //如果有多行的、先删除空白行，然后插入下面
    {

        var data = managersub.getData();
        for (var i = data.length - 1; i >= 0; i--) {
            if (data[i].goodsId == 0 || data[i].goodsName == "") {
                managersub.deleteRow(i);
                // alert("Delete row："+i);
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

//城市 下拉框 数据初始化,这里也可以改成 改变服务器参数( parms,url )
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
    $("#message").html('Final choice:' + out);
}

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
    } //数量改变---结束

    if (e.column.name == "price") //单价改变---开始、计算TotalPrice、折扣额、税额、价税合计
    {
        //单价改变：【数量、折扣率、税率】 计算【折扣额、TotalPrice、税额、价税合计】; 
        price = Number(e.value);



        //2、TotalPrice=数量*单价-折扣额
        sumPrice = Number(num) * Number(price);
        num = Math.round(num * 100) / 100;
        price = Math.round(price * 100) / 100;

        sumPrice = Math.round(sumPrice * 100) / 100;

        manager.updateCell("price", price, e.record);

        //2、TotalPrice
        manager.updateCell('sumPrice', sumPrice, e.record);
    } //单价改变---结束




    if (e.column.name == "sumPrice") //TotalPrice改变
    {
        //TotalPrice改变：【数量、折扣额、税率】 计算【折扣率、单价、税额、价税合计】   

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

        manager.updateCell("price", price, e.record);
        manager.updateCell('sumPrice', sumPrice, e.record);
    } //TotalPrice改变---结束
}

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
        //数量改变：【折扣率、税率】 计算【折扣额、TotalPrice、税额、价税合计】
        num = Number(e.value);

        //2、TotalPrice=数量*单价-折扣额
        sumPrice = Number(num) * Number(price);

        num = Math.round(num * 100) / 100;
        price = Math.round(price * 100) / 100;

        sumPrice = Math.round(sumPrice * 100) / 100;




        //开始赋值

        managersub.updateCell("num", num, e.record);



        //2、TotalPrice
        managersub.updateCell('sumPrice', sumPrice, e.record);
    }

    if (e.column.name == "price") {
        price = Number(e.value);
        sumPrice = Number(num) * Number(price);
        num = Math.round(num * 100) / 100;
        price = Math.round(price * 100) / 100;
        sumPrice = Math.round(sumPrice * 100) / 100;
        managersub.updateCell("price", price, e.record);
        managersub.updateCell('sumPrice', sumPrice, e.record);
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
        managersub.updateCell("price", price, e.record);
        managersub.updateCell('sumPrice', sumPrice, e.record);
    }
    updateTotal();
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
    if (!row) { alert('Please select a row'); return; }
    manager.beginEdit(row);
}

function cancelEdit() {
    var row = manager.getSelectedRow();
    if (!row) { alert('Please select a row'); return; }
    manager.cancelEdit(row);
}
function cancelAllEdit() {
    manager.cancelEdit();
}

function endEdit() {
    var row = manager.getSelectedRow();
    if (!row) { alert('Please select a row'); return; }
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
    if (!selected) { alert('Please select a row'); return; }

}

function getSelected() {
    var row = manager.getSelectedRow();
    if (!row) { alert('Please select a row'); return; }
    alert(JSON.stringify(row));
}
function getData() {
    var data = manager.getData();
    alert(JSON.stringify(data));
}

function save() {
    var data = manager.getData();

    //1, First delete the blank rows
    for (var i = data.length - 1; i >= 0; i--) {
        if (data[i].goodsId == 0 || data[i].goodsName == "") {
            data.splice(i, 1);

        }

    }

    //2, Check if a product is selected
    if (data.length == 0) {
        $.ligerDialog.warn('Please select the product to be assembled!');

        return;
        alert("Execution skipped!");
    }

    //3, Check if the quantity for all products has been entered
    for (var i = 0; i < data.length; i++) {
        if (data[i].num <= 0 || data[i].num == "" || data[i].num == "0" || data[i].num == "0.00") {

            $.ligerDialog.warn("Please enter the quantity of assembled products in row" + (i + 1) + "！");

            return;
            alert("Execution skipped!");
        }

        if (data[i].ckId == 0 || data[i].ckId == "" || data[i].ckId == "0" || data[i].ckName == "") {

            $.ligerDialog.warn("Please enter the warehouse where the assembled goods in row " + (i + 1) + " are located!");

            return;
            alert("Execution skipped!");
        }
    }

    var goodsId = data[0].goodsId;
    var num = data[0].num;
    var price = data[0].price;
    var ckId = data[0].ckId;
    var remarksItem = data[0].remarks;
    var datasub = managersub.getData();


    //1, First delete the blank rows
    for (var i = datasub.length - 1; i >= 0; i--) {
        if (datasub[i].goodsId == 0 || datasub[i].goodsName == "") {
            datasub.splice(i, 1);

        }

    }

    //2, Check if a product is selected
    if (datasub.length == 0) {
        $.ligerDialog.warn('Please select the item to be assembled!');

        return;
        alert("Execution skipped!");
    }



    //3, Check if the quantity for all products has been entered
    for (var i = 0; i < datasub.length; i++) {
        if (datasub[i].num <= 0 || datasub[i].num == "" || datasub[i].num == "0" || datasub[i].num == "0.00") {

            $.ligerDialog.warn("Please enter the number of items to be assembled in row " + (i + 1) + "!");

            return;
            alert("Execution skipped!");
        }

        if (datasub[i].ckId == 0 || datasub[i].ckId == "" || datasub[i].ckId == "0" || datasub[i].ckName == "") {

            $.ligerDialog.warn("Please enter the warehouse where the goods in row " + (i + 1) + " are assembled!");

            return;
            alert("Execution skipped!");
        }
    }

    var bizDate = $("#txtBizDate").val();
    if (bizDate == "") {
        $.ligerDialog.warn("Please enter assembly date!");
        return;
    }

    var remarks = $("#txtRemarks").val();

    var fee = $("#txtFee").val() == "" ? 0 : $("#txtFee").val();


    var headJson = { fee: fee, bizDate: bizDate, remarks: remarks, goodsId: goodsId, num: num, price: price, ckId: ckId, remarksItem: remarksItem };



    var dataNew = [];
    dataNew.push(headJson);



    var list = JSON.stringify(headJson);


    var goodsList = [];




    list = list.substring(0, list.length - 1);//去掉最后一个花括号

    list += ",\"Rows\":";
    list += JSON.stringify(datasub);
    list += "}";



    var postData = JSON.parse(list);//最终的json

    //        alert(postData.Rows[0].id);
    //        
    //        alert(postData.bizDate);
    //        
    //        alert(postData.Rows[0].goodsName);

    //   alert(JSON.stringify(postData));

    //   $("#txtRemarks").val(JSON.stringify(postData));

    // return;

    $.ajax({
        type: "POST",
        url: 'ashx/AssembleListAdd.ashx',
        contentType: "application/json", //必须有
        //dataType: "json", //表示返回值类型，不必须
        data: JSON.stringify(postData),  //相当于 //data: "{'str1':'foovalue', 'str2':'barvalue'}",
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

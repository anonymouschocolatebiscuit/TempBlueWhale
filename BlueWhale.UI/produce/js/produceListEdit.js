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
    if (event.keyCode == 13 || event.keyCode == 39 || event.keyCode == 9) 
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
        alert('Please select row!');
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
        alert('Please select row!');
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
    manager._setUrl("produceListEdit.aspx?Action=GetDataBom&goodsId=" + goodsId + "&num=" + num);


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
        title: 'Select Item', name: 'winselector', width: 800, height: 540, url: '../baseSet/GoodsListSelect.aspx', buttons: [
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
        alert('Please select row!');
        return;
    }

    $("#txtOrderNumber").val("");
    $("#hfOrderNumber").val("");
    $("#txtGoodsName").val(data[0].names);
    $("#hfGoodsId").val(data[0].id);
    $("#txtSpec").val(data[0].spec);
    $("#txtUnitName").val(data[0].unitName);


    var goodsId = data[0].id;

    manager.changePage("first");
    manager._setUrl("produceListEdit.aspx?Action=GetDataBom&goodsId=" + goodsId + "&num=1");

    dialog.close();

}


function f_selectGoodsCancel(item, dialog) {
    dialog.close();
}

function f_onGoodsChanged(e) {


    if (!e || !e.length) return;

    var grid = liger.get("maingrid");

    var selected = e[0];

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

    if (e.length > 1) 

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

    var orderNumber = $("#hfOrderNumber").val();
    $("#txtOrderNumber").val(orderNumber);

    var goodsName = $("#hfGoodsName").val();
    $("#txtGoodsName").val(goodsName);

    var wlName = $("#txtClientName").val();
    $("#clientName").val(wlName);


    window['g'] =
        managerProcess = $("#maingrid9999").ligerGrid({
            columns: [

                {
                    display: '', isSort: false, width: 60, align: 'center', frozen: true, render: function (rowdata, rowindex, value) {
                        var h = "";
                        if (!rowdata._editing) {
                            h += "<a href='javascript:addNewRow()' title='Add Row' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                            h += "<a href='javascript:deleteRow()' title='Delete Row' style='float:left;'><div class='ui-icon ui-icon-trash'></div></a> ";
                            h += "<a href='javascript:f_selectContact()' title='Select Process' style='float:right;'><div class='ui-icon ui-icon-search'></div></a> ";
                        }
                        else {
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
                        render: function (e) {  
                            return 'Total: ';
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
                            return "<span id='sumPriceItem'>" + Math.round(itemSumPrice * 10000) / 10000 + "</span>";//formatCurrency(suminf.sum)

                        }
                    }

                },
                { display: 'Remarks', name: 'remarks', width: 220, align: 'left', type: 'text', editor: { type: 'text' } }
            ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '350',
            url: 'produceListEdit.aspx?Action=GetData&id=' + param,
            rownumbers: true,
            frozenRownumbers: true,
            dataAction: 'local',
            usePager: false,
            alternatingRow: false,

            totalSummary: false,
            enabledEdit: true, 

            onAfterEdit: f_onAfterEdit 
        }
        );
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
        $.ligerDialog.warn('At least keep one row!')

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
                    display: '', isSort: false, width: 60, align: 'center', frozen: true, render: function (rowdata, rowindex, value) {
                        var h = "";
                        if (!rowdata._editing) {
                            h += "<a href='javascript:deleteRow()' title='Delete Row' style='float:left;'><div class='ui-icon ui-icon-trash'></div></a> ";
                        }
                        else {
                        }
                        return h;
                    }
                }
                ,
                { display: 'Item Code', name: 'code', width: 100, align: 'center' },
                {
                    display: 'Item Name', name: 'goodsName', width: 250, align: 'left',

                    totalSummary:
                    {
                        type: 'count',
                        render: function (e) {  
                      
                            return 'Total: ';
                        }
                    }


                },

                { display: 'Specification model', name: 'spec', width: 100, align: 'center' },

                { display: 'Unit', name: 'unitName', width: 70, align: 'center' },

                {
                    display: 'Standard dosage', name: 'numBom', width: 80, type: 'float', align: 'right',

                    totalSummary:
                    {
                        align: 'right',   
                        type: 'sum',
                        render: function (e) {  
                            
                            return Math.round(e.sum * 100) / 100;
                        }
                    }


                },

                { display: 'Loss rate', name: 'rate', width: 60, align: 'right', type: 'float' },
                {
                    display: 'Planned usage', name: 'num', width: 80, align: 'right', type: 'float', editor: { type: 'float' },
                    totalSummary:
                    {
                        align: 'right',   
                        type: 'sum',
                        render: function (e) {  
                            var itemSumPrice = e.sum;
                            return "<span id='sumPriceItem'>" + Math.round(itemSumPrice * 10000) / 10000 + "</span>";//formatCurrency(suminf.sum)


                        }
                    }

                },

                { display: 'Remark', name: 'remarks', width: 220, align: 'left', type: 'text', editor: { type: 'text' } }
            ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '350',
            url: "produceListEdit.aspx?Action=GetData&id=" + param,
            rownumbers: true,
            frozenRownumbers: true,
            dataAction: 'local',
            usePager: false,
            alternatingRow: false,

            totalSummary: false,
            enabledEdit: true, 

            onAfterEdit: f_onAfterEdit 
        }
        );
});


function save() {

    var hfGoodsId = $("#hfGoodsId").val();
    if (hfGoodsId == "" || hfGoodsId == 0) {
        $.ligerDialog.warn("Please select the product to be produced!");
        return;
    }

    var txtNum = $("#txtNum").val();
    if (txtNum == "" || txtNum == 0) {
        $.ligerDialog.warn("Please enter production quantity!");
        return;

    }

    var data = manager.getData();


    for (var i = data.length - 1; i >= 0; i--) {
        if (data[i].goodsId == 0 || data[i].goodsName == "" || data[i].goodsId == "") {
            data.splice(i, 1);

        }
    }

    var isGongxu = false;

    if (data.length == 0) {

        if (confirm("Confirm not to add material usage plan？")) {
            isGongxu = false;
        }
        else {
            isGongxu = true;
        }
    }

    if (isGongxu) {
        if (data.length == 0) {
            $.ligerDialog.warn("Please select raw material products!");

            return;
            alert("I won't execute it!");

        }
        for (var i = 0; i < data.length; i++) {

            if (data[i].num <= 0 || data[i].num == "") {

                $.ligerDialog.warn("Please enter the" + (i + 1) + "row planned usage!");

                return;
                alert("I won't execute it!");
            }

        }

    }

    var dateStart = $("#txtDateStart").val();
    if (dateStart == "") {
        $.ligerDialog.warn("Please enter start date!");
        return;

    }

    var dateEnd = $("#txtDateEnd").val();
    if (dateStart == "") {
        $.ligerDialog.warn("Please enter end date!");
        return;

    }


    var typeName = $("#ddlTypeName").val();

    var orderNumber = $("#txtOrderNumber").val();

    var remarks = $("#txtRemarks").val();


    var headJson = {
        id: param,
        orderNumber: orderNumber,
        typeName: typeName,
        goodsId: hfGoodsId,
        num: txtNum,
        dateStart: dateStart,
        dateEnd: dateEnd,
        remarks: remarks
    };

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
        url: 'ashx/produceListEdit.ashx',
        contentType: "application/json", 
        data: JSON.stringify(postData),  
        success: function (jsonResult) {

            if (jsonResult == "Operate Successful!") {

                $.ligerDialog.waitting('Operate Successful!'); setTimeout(function () { $.ligerDialog.closeWaitting(); location.reload(); }, 2000);

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


function getBomList() {

    var goodsId = $("#hfGoodsId").val();
    var num = $("#txtNum").val();

    if (goodsId > 0 && num > 0) {
        manager.changePage("first");
        manager._setUrl("produceListEdit.aspx?Action=GetDataBom&goodsId=" + goodsId + "&num=" + num + "&id=" + param);
    }

}
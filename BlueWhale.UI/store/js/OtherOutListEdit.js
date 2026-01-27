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

var cangkulist = {};

//扩展 numberbox 类型的格式化函数
$.ligerDefaults.Grid.formatters['numberbox'] = function (value, column) {
    var precision = column.editor.precision;
    return value.toFixed(precision);
};

var manager;
$(function () {
    var form = $("#form").ligerForm();
    var g = $.ligerui.get("ddlVenderList");
    g.set("Width", 250);

    window['g'] =
        manager = $("#maingrid").ligerGrid({
            columns: [
                {
                    display: 'Action', isSort: false, width: 40, align: 'center', frozen: true, render: function (rowdata, rowindex, value) {
                        var h = "";
                        if (!rowdata._editing) {
                            h += "<a href='javascript:addNewRow()' title='Add Row' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                            h += "<a href='javascript:deleteRow()' title='Delete Row' style='float:right;'><div class='ui-icon ui-icon-trash'></div></a> ";
                        }
                        else {
                            h += "<a href='javascript:endEdit(" + rowindex + ")'>Submit</a> ";
                            h += "<a href='javascript:cancelEdit(" + rowindex + ")'>Cancel</a> ";
                        }
                        return h;
                    }
                },
                {
                    display: 'Item Name', name: 'goodsName', width: 200, align: 'left',

                    render: function (row) {
                        var selectName = row.goodsName.split(";");
                        return selectName[0];
                    },
                    editor:
                    {
                        type: 'popup',
                        valueField: 'names',
                        grid:
                        {
                            url: "../baseSet/GoodsList.aspx?Action=GetDataList",
                            checkbox: true,
                            dataAction: 'local',
                            usePager: true,
                            columns:
                                [
                                    { display: 'Item Code', name: 'code', width: 100 },
                                    { display: 'Item Name', name: 'names', width: 200 },
                                    { display: 'Specification', name: 'spec', width: 100 },
                                    { display: 'Unit', name: 'unitName', width: 100 },
                                    { display: 'Default Warehouse', name: 'ckName', width: 120 },
                                    { display: 'Warehouse ID', name: 'ckId', width: 40, hide: true }
                                ]
                        },
                        condition:
                        {
                            fields:
                                [
                                    { name: 'code', type: 'text', label: 'Keywords', width: 200 }
                                ]
                        },
                        onSelected: f_onGoodsChanged
                    },
                    totalSummary:
                    {
                        type: 'count',
                        render: function (e) {
                            return 'Total:';
                        }
                    }
                },
                { display: 'Specification', name: 'spec', width: 100, align: 'center' },
                { display: 'Unit', name: 'unitName', width: 80, align: 'center' },
                {
                    display: 'Quantity', name: 'num', width: 80, type: 'float', align: 'right', editor: { type: 'float' },
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
                    display: 'Unit Price', name: 'price', width: 70, type: 'float', align: 'right', editor: { type: 'float', precision: 4 }
                },
                {
                    display: 'Amount', name: 'sumPrice', width: 80, type: 'float', align: 'right', editor: { type: 'float' },
                    totalSummary:
                    {
                        align: 'center',
                        type: 'sum',
                        render: function (e) {
                            var itemSumPrice = e.sum;

                            return "<span id='sumPriceItem'>" + Math.round(itemSumPrice * 10000) / 10000 + "</span>";//formatCurrency(suminf.sum)
                        }
                    }
                },
                {
                    display: 'Warehouse', name: 'ckId', width: 100, isSort: false, textField: 'ckName',
                    editor: {
                        type: 'select',
                        url: "../baseSet/InventoryList.aspx?Action=GetDDLList&r=" + Math.random(),
                        valueField: 'ckId', textField: 'ckName'
                    }
                },
                { display: 'Remarks', name: 'remarks', width: 150, align: 'left', type: 'text', editor: { type: 'text' } }
            ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '450',
            url: 'OtherOutListEdit.aspx?Action=GetData&id=' + getUrlParam("id"),//获取参数
            rownumbers: true,
            frozenRownumbers: true,
            dataAction: 'local',
            usePager: false,
            alternatingRow: false,
            totalSummary: true,
            enabledEdit: true,
            // onBeforeEdit: f_onBeforeEdit,
            // onBeforeSubmitEdit: f_onBeforeSubmitEdit,
            // totalRender: f_totalRender,
            onAfterEdit: f_onAfterEdit
        });
});

var rowNumber = 9;
function f_totalRender(data, currentPageData) {
    //return "Warehouse Total:"+data.sumPriceAll; 
}

function setWarehouse() {
    $.ligerDialog.open({ target: $("#target1") });
    // $.ligerDialog.open({ url: '../../welcome.htm', height: 250,width:null, buttons: [ { text: 'Confirm', onclick: function (item, dialog) { alert(item.text); } }, { text: 'Cancel', onclick: function (item, dialog) { dialog.close(); } } ] });
}

function selectWarehouse() {
    var warehouseName = $("#ddlWarehouse").find("option:selected").text();
    var warehouseId = $("#ddlWarehouse").val();

    alert(warehouseName);
    alert(warehouseId);

    var grid = liger.get("maingrid");
    var data = manager.getData();

    alert(data.length);

    for (var i = 0; i < data.length; i++) {
        alert(data[i].goodsId);

        grid.updateCell("warehouseId", ckId, i);
        grid.updateCell("warehouseName", ckName, i);
    }

    $(".l-dialog,.l-window-mask").remove();
    $.ligerDialog.close();
}

function updateTotal() {
    var data = manager.getData();
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

function f_onGoodsChanged(e) {
    if (!e.data || !e.data.length) return;

    var grid = liger.get("maingrid");
    var selected = e.data[0];
    grid.updateRow(grid.lastEditRow, {
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

    if (e.data.length > 1) {
        var data = manager.getData();

        for (var i = data.length - 1; i >= 0; i--) {
            if (data[i].goodsId == 0 || data[i].goodsName == "") {
                manager.deleteRow(i);
            }
        }

        for (var i = 1; i < e.data.length; i++) {
            grid.addRow({
                id: rowNumber,
                goodsId: e.data[i].id,
                goodsName: e.data[i].names,
                unitName: e.data[i].unitName,
                num: 1,
                price: e.data[i].priceCost,
                spec: e.data[i].spec,
                sumPrice: e.data[i].priceCost,
                ckId: e.data[i].ckId,
                ckName: e.data[i].ckName,
                remarks: ""
            });

            rowNumber = rowNumber + 1;
        }
    }
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
    $("#message").html('Last selected:' + out);
}

function f_onAfterEdit(e) {
    var num, price, sumPrice;

    num = Number(e.record.num);
    price = Number(e.record.price);
    sumPrice = Number(e.record.sumPrice);

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

    updateTotal();
}

function f_onBeforeEdit(e) {
    // if(e.data.goodsId!="" && e.data.goodsName!="") return true;
    // return false;
    //            
    // if(e.rowindex<=2) return true;
    // return false;
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
    if (!row) {
        alert('Please select row'); return;
    }
    manager.beginEdit(row);
}

function cancelEdit() {
    var row = manager.getSelectedRow();
    if (!row) {
        alert('Please select row'); return;
    }
    manager.cancelEdit(row);
}

function cancelAllEdit() {
    manager.cancelEdit();
}

function endEdit() {
    var row = manager.getSelectedRow();
    if (!row) {
        alert('Please select row'); return;
    }
    manager.endEdit(row);
}

function endAllEdit() {
    manager.endEdit();
}

function deleteRow() {
    if (manager.rows.length == 1) {
        $.ligerDialog.warn('At least 1 row is required！')
    }
    else {
        manager.deleteSelectedRow();
    }
}

var newrowid = 100;
function addNewRow() {
    var gridData = manager.getData();
    var rowNum = gridData.length;

    manager.addRow({
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
    if (!selected) { alert('Please select row'); return; }
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

    for (var i = data.length - 1; i >= 0; i--) {
        if (data[i].goodsId == 0 || data[i].goodsName == "") {
            data.splice(i, 1);
        }
    }

    if (data.length == 0) {
        $.ligerDialog.warn('Please select item！');

        return;
        alert("No operation will be carried out！");
    }

    for (var i = 0; i < data.length; i++) {
        if (data[i].num <= 0 || data[i].num == "" || data[i].num == "0" || data[i].num == "0.00") {

            $.ligerDialog.warn("Please enter the quantiity of row " + (i + 1) + "！");

            return;
            alert("No operation will be carried out！");
        }
    }

    var typeId = 1;
    if ($("#rb1").attr("checked")) {
        typeId = 1;
    }
    if ($("#rb2").attr("checked")) {
        typeId = -1;
    }

    // var checkText=$("#ddlVenderList").find("option:selected").text();
    var venderId = $("#ddlVenderList").val();
    var bizDate = $("#txtBizDate").val();
    if (bizDate == "") {
        $.ligerDialog.warn("Please enter inbound date！");
        return;
    }

    var remarks = $("#txtRemarks").val();
    var headJson = { id: getUrlParam("id"), venderId: venderId, bizDate: bizDate, remarks: remarks, typeId: typeId };
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
        url: 'ashx/OtherOutListEdit.ashx',
        contentType: "application/json",
        data: JSON.stringify(postData),
        success: function (jsonResult) {
            if (jsonResult == "Operation successful！") {
                $.ligerDialog.waitting('Operation successful！'); setTimeout(function () { $.ligerDialog.closeWaitting(); location.reload(); }, 2000);
            }
            else {
                $.ligerDialog.warn(jsonResult);
            }
        },
        error: function (xhr) {
            alert("An error has occured，please try again later:" + xhr.responseText);
        }
    });
}

function checkBill() {
    var data = manager.getData();
    if (data.length == 0) {
        $.ligerDialog.warn('Please select item');
        return false;
    }
    else {
        for (var i = 0; i < data.length; i++) {
            if (data.Rows[i].goodsName == "" || data.Rows[i].goodsId == 0) {
                $.ligerDialog.warn('Row：' + i + " has no item information！");
                return false;
            }

            if (data.Rows[i].num == 0) {
                $.ligerDialog.warn('Please enter quantity of item for row ' + i + "！");
                return false;
            }
        }
    }
};

function makeBill() {
    var row = manager.getSelectedRow();

    //alert(row.id);
    // return;
    //window.open("PurReceiptListAdd.aspx?id="+row.id);    
    //return;
    // alert('buy/PurReceiptListAdd.aspx?id='+row.id);

    top.topManager.openPage({
        id: 'PurReceiptListAdds',
        href: 'buy/PurReceiptListAdd.aspx?id=' + row.id,
        title: 'Purchase Inbound - Create'
    });
}

function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);

    if (r != null) return unescape(r[2]); return null;
}
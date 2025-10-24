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
    if (event.keyCode == 13 || event.keyCode == 39 || event.keyCode == 9) //enter,right arrow,tap
    {
        manager.endEditToNext();
    }
});

function f_selectContact() {
    $.ligerDialog.open({
        title: 'select product', name: 'winselector', width: 840, height: 540, url: '../baseSet/GoodsListSelect.aspx', buttons: [
            { text: 'confirm', onclick: f_selectContactOK },
            { text: 'close', onclick: f_selectContactCancel }
        ]
    });
    return false;
}
function f_selectContactOK(item, dialog) {
    var fn = dialog.frame.f_select || dialog.frame.window.f_select;
    var data = fn();
    if (!data) {
        alert('please select row!');
        return;
    }

    f_onGoodsChanged(data);

    dialog.close();
}

function f_selectContactCancel(item, dialog) {
    dialog.close();
}

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
                            h += "<a href='javascript:addNewRow()' title='add row' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                            h += "<a href='javascript:deleteRow()' title='delete row' style='float:left;'><div class='ui-icon ui-icon-trash'></div></a> ";
                            h += "<a href='javascript:f_selectContact()' title='select product' style='float:left;'><div class='ui-icon ui-icon-search'></div></a> ";
                        }
                        return h;
                    }
                },
                {
                    display: 'product name', name: 'goodsName', width: 250, align: 'left',

                    totalSummary:
                    {
                        type: 'count',
                        render: function (e) {  //汇总渲染器，返回html加载到单元格
                            return 'total：';
                        }
                    }
                },
                { display: 'specification', name: 'spec', width: 180, align: 'center' },
                { display: 'unit', name: 'unitName', width: 80, align: 'center' },
                {
                    display: 'number', name: 'num', width: 80, type: 'float', align: 'right', editor: { type: 'float' },

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
                    display: 'transfer out', name: 'ckIdOut', width: 100, isSort: false, textField: 'ckNameOut',
                    editor: {
                        type: 'select',
                        url: "../baseSet/InventoryList.aspx?Action=GetDDLList&r=" + Math.random(),
                        valueField: 'ckId', textField: 'ckName'
                    }
                },
                {
                    display: 'transfer in', name: 'ckIdIn', width: 100, isSort: false, textField: 'ckNameIn',
                    editor: {
                        type: 'select',
                        url: "../baseSet/InventoryList.aspx?Action=GetDDLList&r=" + Math.random(),
                        valueField: 'ckId', textField: 'ckName'
                    }
                },
                { display: 'remark', name: 'remarks', width: 150, align: 'left', type: 'text', editor: { type: 'text' } }
            ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '450',
            url: 'InventoryTransferListEdit.aspx?Action=GetData&id=' + getUrlParam("id"),//
            rownumbers: true,//
            frozenRownumbers: true,//
            dataAction: 'local',//
            usePager: false,
            alternatingRow: false,
            totalSummary: true,
            enabledEdit: true //
        }
        );
});

var rowNumber = 9;

function setCangku() {
    $.ligerDialog.open({ target: $("#target1") });
}
function selectCangku() {
    $(".l-dialog,.l-window-mask").remove();

    $.ligerDialog.close();
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
        num: "",
        ckIdOut: selected.ckId,
        ckNameOut: selected.ckName,
        ckIdIn: "",
        ckNameOut: "",
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
                num: "",
                ckIdOut: e[i].ckId,
                ckNameOut: e[i].ckName,
                ckIdIn: "",
                ckNameOut: "",
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
    $("#message").html('last select:' + out);
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
    if (!row) { alert('please select row'); return; }
    manager.beginEdit(row);
}
function cancelEdit() {
    var row = manager.getSelectedRow();
    if (!row) { alert('please select row'); return; }
    manager.cancelEdit(row);
}
function cancelAllEdit() {
    manager.cancelEdit();
}
function endEdit() {
    var row = manager.getSelectedRow();
    if (!row) { alert('please select row'); return; }
    manager.endEdit(row);
}
function endAllEdit() {
    manager.endEdit();
}
function deleteRow() {

    if (manager.rows.length == 1) {
        $.ligerDialog.warn('Keep at least one row!')

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
        goodsId: "",
        goodsName: "",
        spec: "",
        unitName: "",
        num: "",
        ckIdIn: "",
        ckIdOut: "",
        ckNameIn: "",
        ckNameOut: "",
        remarks: ""
    });
}


function getSelected() {
    var row = manager.getSelectedRow();
    if (!row) { alert('please select row'); return; }
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
        $.ligerDialog.warn('please select row');

        return;
        alert("no execute!");
    }

    for (var i = 0; i < data.length; i++) {
        if (data[i].num <= 0 || data[i].num == "" || data[i].num == "0" || data[i].num == "0.00") {

            $.ligerDialog.warn("Please enter the quantity of the product in row " + (i + 1) + "!");

            return;
            alert("no execute!");
        }

        if (data[i].ckIdIn <= 0 || data[i].ckIdIn == "" || data[i].ckIdIn == "0") {

            $.ligerDialog.warn("Please select the transfer warehouse in row " + (i + 1) + "!");

            return;
            alert("no execute!");
        }

        if (data[i].ckIdOut <= 0 || data[i].ckIdOut == "" || data[i].ckIdOut == "0") {

            $.ligerDialog.warn("Please select the transferred warehouse in row " + (i + 1) + "!");

            return;
            alert("no execute!");
        }


        if (data[i].ckIdIn == data[i].ckIdOut) {

            $.ligerDialog.warn("The warehouses for " + (i + 1) + " are the same");

            return;
            alert("no execute!");
        }
    }

    var bizDate = $("#txtBizDate").val();
    if (bizDate == "") {
        $.ligerDialog.warn("Please enter the order date！");
        return;
    }

    var remarks = $("#txtRemarks").val();

    var headJson = { id: getUrlParam("id"), bizDate: bizDate, remarks: remarks };

    var dataNew = [];
    dataNew.push(headJson);

    var list = JSON.stringify(headJson);

    var goodsList = [];
    list = list.substring(0, list.length - 1);//
    list += ",\"Rows\":";
    list += JSON.stringify(data);
    list += "}";

    var postData = JSON.parse(list);
    $.ajax({
        type: "POST",
        url: 'ashx/InventoryTransferListEdit.ashx',
        contentType: "application/json",
        data: JSON.stringify(postData),
        success: function (jsonResult) {
            if (jsonResult == "operate successful!") {
                $.ligerDialog.waitting('operate successful!'); setTimeout(function () { $.ligerDialog.closeWaitting(); location.reload(); }, 2000);
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
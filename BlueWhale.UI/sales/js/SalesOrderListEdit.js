function formatCurrency(x) {
    var f_x = parseFloat(x);
    if (isNaN(f_x)) {
        return "0.00";
    }
    f_x = Math.round(f_x * 100) / 100;
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
        title: 'Select Item',
        name: 'winselector',
        width: 840,
        height: 540,
        url: '../baseSet/GoodsListSelect.aspx',
        buttons: [
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
        alert('Please select row!');
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
        title: 'Select Client',
        name: 'winselector',
        width: 1400,
        height: 540,
        url: '../baseSet/ClientListSelect.aspx',
        buttons: [
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

$.ligerDefaults.Grid.formatters['numberbox'] = function (value, column) {
    var precision = column.editor.precision;
    return value.toFixed(precision);
};

var manager;
$(function () {
    $("#clientName").ligerComboBox({
        onBeforeOpen: f_selectClient,
        valueFieldID: 'clientId',
        width: 300
    });

    var wlName = $("#txtClientName").val();
    $("#clientName").val(wlName);

    $("#form").ligerForm();
    $("#tbFooter").ligerForm();
    $("#form22").ligerForm();

    $.ligerui.get("clientName").set("Width", 250);

    manager = $("#maingrid").ligerGrid({
        columns: [
            {
                display: '', isSort: false, width: 60, align: 'center', frozen: true,
                render: function (rowdata) {
                    if (!rowdata._editing) {
                        return "<a href='javascript:addNewRow()' title='Add new row' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> " +
                            "<a href='javascript:deleteRow()' title='Delete row' style='float:left;'><div class='ui-icon ui-icon-trash'></div></a> " +
                            "<a href='javascript:f_selectContact()' title='Select item' style='float:left;'><div class='ui-icon ui-icon-search'></div></a> ";
                    }
                    return "";
                }
            },
            {
                display: 'Item Name', name: 'goodsName', width: 250, align: 'left',
                totalSummary: {
                    type: 'count',
                    render: function () { return 'Total: '; }
                }
            },
            { display: 'Specification', name: 'spec', width: 100, align: 'center' },
            { display: 'Unit', name: 'unitName', width: 80, align: 'center' },
            {
                display: 'Warehouse', name: 'ckId', width: 90, isSort: false, textField: 'ckName',
                editor: {
                    type: 'select',
                    url: "../baseSet/InventoryList.aspx?Action=GetDDLList&r=" + Math.random(),
                    valueField: 'ckId',
                    textField: 'ckName'
                }
            },
            {
                display: 'Quantity', name: 'num', width: 80, type: 'float', align: 'right', editor: { type: 'float' },
                totalSummary: {
                    align: 'right',
                    type: 'sum',
                    render: function (e) {
                        return Math.round(e.sum * 100) / 100;
                    }
                }
            },
            { display: 'Original Price', name: 'price', width: 120, type: 'float', align: 'right', editor: { type: 'float' } },
            { display: 'Discount%', name: 'dis', width: 100, type: 'float', align: 'right', editor: { type: 'float' } },
            {
                display: 'Discount Amount', name: 'sumPriceDis', width: 120, type: 'float', align: 'right', editor: { type: 'float' },
                totalSummary: {
                    align: 'center',
                    type: 'sum',
                    render: function (e) {
                        return "<span id='sumPriceItemDis'>" + Math.round(e.sum * 10000) / 10000 + "</span>";
                    }
                }
            },
            { display: 'Current Price', name: 'priceNow', width: 110, type: 'float', align: 'right', editor: { type: 'float' } },
            {
                display: 'Amount', name: 'sumPriceNow', width: 80, type: 'float', align: 'right', editor: { type: 'float' },
                totalSummary: {
                    align: 'center',
                    type: 'sum',
                    render: function (e) {
                        return "<span id='sumPriceItemNow'>" + Math.round(e.sum * 10000) / 10000 + "</span>";
                    }
                }
            },
            { display: 'Tax Rate%', name: 'tax', width: 100, type: 'int', align: 'center', editor: { type: 'int' } },
            { display: 'Unit Price Including Tax', name: 'priceTax', width: 100, type: 'float', align: 'center', editor: { type: 'float' } },
            {
                display: 'Tax Amount', name: 'sumPriceTax', width: 90, type: 'float', align: 'right',
                totalSummary: {
                    align: 'center',
                    type: 'sum',
                    render: function (e) {
                        return "<span id='sumPriceItemTax'>" + Math.round(e.sum * 10000) / 10000 + "</span>";
                    }
                }
            },
            {
                display: 'Total Price Including Tax', name: 'sumPriceAll', width: 120, type: 'float', align: 'right', editor: { type: 'float' },
                totalSummary: {
                    align: 'center',
                    type: 'sum',
                    render: function (e) {
                        return "<span id='sumPriceItemAll'>" + Math.round(e.sum * 10000) / 10000 + "</span>";
                    }
                }
            },
            { display: 'Remarks', name: 'remarks', width: 150, align: 'left', type: 'text', editor: { type: 'text' } }
        ],
        width: '99%',
        pageSizeOptions: [5, 10, 15, 20],
        height: 350,
        url: 'SalesOrderListEdit.aspx?Action=GetData&id=' + param,
        rownumbers: true,
        frozenRownumbers: true,
        dataAction: 'local',
        usePager: false,
        alternatingRow: false,
        totalSummary: true,
        enabledEdit: true,
        onAfterEdit: f_onAfterEdit
    });
});

var rowNumber = 9;
var itemSumPriceAll = 0;

function f_totalRender(data, currentPageData) {
}

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

// Event triggered when goods change: update unit, price, etc.
function f_onGoodsChanged(e) {
    if (!e || !e.length) return;

    var grid = liger.get("maingrid");
    var selected = e[0];
    var selectedRow = manager.getSelected();

    // Update current row with selected goods info
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
        priceTax: selected.priceSalesRetail, // Tax included price, default equals no-tax price
        sumPriceTax: 0, // Tax amount
        sumPriceAll: selected.priceSalesRetail,
        ckId: selected.ckId,
        ckName: selected.ckName,
        sourceNumber: "",
        itemId: 0,
        remarks: ""
    });

    // If multiple goods selected, remove empty rows and add new ones
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
            rowNumber++;
        }
    }

    updateTotal();
}

// Update total summary display
function updateTotal() {
    var data = manager.getData();

    var sumPriceItemDis = 0; // Discount amount
    var sumPriceItemNow = 0; // Amount before tax
    var sumPriceItemTax = 0; // Tax amount
    var sumPriceItemAll = 0; // Total including tax

    // Remove empty rows from data array
    for (var i = data.length - 1; i >= 0; i--) {
        if (!data[i].goodsId || data[i].goodsId == 0 || data[i].goodsName == "") {
            data.splice(i, 1);
        }
    }

    // Sum up values from all rows
    for (var i = 0; i < data.length; i++) {
        sumPriceItemDis += Number(data[i].sumPriceDis);
        sumPriceItemNow += Number(data[i].sumPriceNow);
        sumPriceItemTax += Number(data[i].sumPriceTax);
        sumPriceItemAll += Number(data[i].sumPriceAll);
    }

    // Update HTML elements with formatted currency values
    $("#sumPriceItemDis").html(formatCurrency(sumPriceItemDis));
    $("#sumPriceItemNow").html(formatCurrency(sumPriceItemNow));
    $("#sumPriceItemTax").html(formatCurrency(sumPriceItemTax));
    $("#sumPriceItemAll").html(formatCurrency(sumPriceItemAll));
}

// After cell edit event handler
function f_onAfterEdit(e) {
    // Extract values from the edited record, ensuring numbers
    var num = Number(e.record.num);
    var price = Number(e.record.price);
    var dis = Number(e.record.dis);
    var sumPriceDis = Number(e.record.sumPriceDis);
    var priceNow = Number(e.record.priceNow);
    var sumPriceNow = Number(e.record.sumPriceNow);
    var tax = Number(e.record.tax);
    var priceTax = Number(e.record.priceTax);
    var sumPriceTax = Number(e.record.sumPriceTax);
    var sumPriceAll = Number(e.record.sumPriceAll);

    var goodsId = e.record.goodsId;
    var goodsName = e.record.goodsName;

    if (!goodsId || !goodsName) return; // Ignore if no goods selected

    switch (e.column.name) {
        case "num":
            num = Number(e.value);
            sumPriceDis = num * price * dis / 100;
            sumPriceDis = round4(sumPriceDis);

            sumPriceNow = num * priceNow;
            sumPriceNow = round4(sumPriceNow);

            sumPriceTax = num * priceNow * tax / 100;
            sumPriceTax = round4(sumPriceTax);

            sumPriceAll = num * priceTax;
            sumPriceAll = round4(sumPriceAll);

            updateRowCells(e.record, {
                num,
                sumPriceDis,
                sumPriceNow,
                sumPriceTax,
                sumPriceAll
            });
            break;

        case "price":
            price = Number(e.value);
            sumPriceDis = num * price * dis / 100;
            priceNow = price * (1 + dis / 100);
            sumPriceNow = round4(num * priceNow);
            priceTax = priceNow * (1 + tax / 100);
            sumPriceTax = round4(num * priceNow * tax / 100);
            sumPriceAll = round4(num * priceTax);
            sumPriceDis = round4(sumPriceDis);

            updateRowCells(e.record, {
                price,
                priceNow,
                priceTax,
                sumPriceDis,
                sumPriceNow,
                sumPriceTax,
                sumPriceAll
            });
            break;

        case "dis":
            dis = Number(e.value);
            if (dis != 0) {
                priceNow = price * (dis / 100);
                sumPriceDis = num * price * (1 - dis / 100);
            } else {
                priceNow = price;
                sumPriceDis = 0;
            }

            sumPriceNow = round4(num * priceNow);
            priceTax = priceNow * (1 + tax / 100);
            sumPriceTax = round4(num * priceNow * tax / 100);
            sumPriceAll = round4(num * priceTax);
            sumPriceDis = round4(sumPriceDis);

            updateRowCells(e.record, {
                dis,
                priceNow,
                priceTax,
                sumPriceDis,
                sumPriceNow,
                sumPriceTax,
                sumPriceAll
            });
            break;

        case "sumPriceDis":
            sumPriceDis = Number(e.value);
            if (sumPriceDis >= num * price) {
                alert("Please enter a valid discount amount!");
                return;
            }
            if (num * price !== 0) {
                dis = (1 - sumPriceDis / (num * price)) * 100;
            } else {
                alert("Please enter quantity and price!");
                return;
            }

            priceNow = price * dis / 100;
            sumPriceNow = round4(num * priceNow);
            priceTax = priceNow * (1 + tax / 100);
            sumPriceTax = round4(num * priceNow * tax / 100);
            sumPriceAll = round4(num * priceTax);
            sumPriceDis = round4(sumPriceDis);

            updateRowCells(e.record, {
                dis,
                priceNow,
                priceTax,
                sumPriceDis,
                sumPriceNow,
                sumPriceTax,
                sumPriceAll
            });
            break;

        case "priceNow":
            priceNow = Number(e.value);
            if (price !== 0) {
                dis = priceNow / price * 100;
            } else {
                price = priceNow;
                dis = 0;
            }

            sumPriceDis = round4(num * (price - priceNow));
            sumPriceNow = round4(num * priceNow);
            priceTax = priceNow * (1 + tax / 100);
            sumPriceTax = round4(num * priceNow * tax / 100);
            sumPriceAll = round4(priceTax * num);

            updateRowCells(e.record, {
                price,
                dis,
                priceNow,
                priceTax,
                sumPriceDis,
                sumPriceNow,
                sumPriceTax,
                sumPriceAll
            });
            break;

        case "sumPriceNow":
            sumPriceNow = Number(e.value);
            priceNow = num !== 0 ? sumPriceNow / num : sumPriceNow;
            sumPriceDis = round4(num * (price - priceNow));
            dis = price !== 0 ? priceNow / price * 100 : 0;

            priceTax = tax === 0 ? priceNow : tax * priceNow / 100;
            sumPriceTax = round4(num * price * tax / 100);
            sumPriceAll = round4(num * priceTax);

            price = round4(price);
            priceTax = round4(priceTax);
            sumPriceNow = round4(sumPriceNow);
            dis = round4(dis);

            updateRowCells(e.record, {
                price,
                dis,
                priceNow,
                priceTax,
                sumPriceDis,
                sumPriceNow,
                sumPriceTax,
                sumPriceAll
            });
            break;

        case "tax":
            tax = Number(e.value);
            priceTax = priceNow * (1 + tax / 100);
            sumPriceTax = round4(num * priceNow * tax / 100);
            sumPriceAll = round4(num * priceTax);

            updateRowCells(e.record, {
                tax,
                priceTax,
                sumPriceTax,
                sumPriceAll
            });
            break;

        case "priceTax":
            priceTax = Number(e.value);
            sumPriceAll = round4(num * priceTax);
            tax = priceNow !== 0 ? (priceTax / priceNow - 1) * 100 : 0;
            sumPriceTax = round4(num * priceNow * tax / 100);

            updateRowCells(e.record, {
                priceTax,
                tax,
                sumPriceTax,
                sumPriceAll
            });
            break;

        case "sumPriceTax":
            sumPriceTax = Number(e.value);
            tax = priceNow !== 0 ? sumPriceTax / (num * priceNow) * 100 : 0;
            priceTax = priceNow * (1 + tax / 100);
            sumPriceAll = round4(num * priceTax);

            updateRowCells(e.record, {
                tax,
                priceTax,
                sumPriceTax,
                sumPriceAll
            });
            break;
    }

    // Finally update totals after edit
    updateTotal();
}

// Utility function to update multiple cells of a row in the grid
function updateRowCells(record, values) {
    for (var key in values) {
        record[key] = values[key];
        manager.updateCell(key, values[key]);
    }
}

// Helper: round to 4 decimal places
function round4(num) {
    return Math.round(num * 10000) / 10000;
}

// Format number as currency string (2 decimal places)
function formatCurrency(num) {
    return num.toFixed(2);
}
// Only allow editing rows with already added goods
function f_onBeforeEdit(e) {
    // Uncomment below if you want to allow editing only if goodsId and goodsName exist
    // if (e.data.goodsId != "" && e.data.goodsName != "") return true;
    // return false;

    // Uncomment below to allow editing only for first 3 rows (example)
    // if (e.rowindex <= 2) return true;
    // return false;
}

// Limit discount and tax rate ranges before submitting edits
function f_onBeforeSubmitEdit(e) {
    if (e.column.name == "dis") {
        if (e.value < 0 || e.value > 100) return false;
    }
    return true;
}

// Delete selected row, but keep at least one row
function deleteRow() {
    if (manager.rows.length == 1) {
        $.ligerDialog.warn('At least one row must be retained!');
    } else {
        manager.deleteSelectedRow();
    }
}

var newrowid = 100;

function save() {
    var venderId = $("#clientId").val();  // Get selected value from client select
    var bizId = $("#ddlYWYList").val();

    if (venderId == 0) {
        $.ligerDialog.warn('Please select a client!');
        return;
    }

    if (bizId == 0) {
        $.ligerDialog.warn('Please select a salesperson!');
        return;
    }

    var bizDate = $("#txtBizDate").val();
    if (bizDate === "") {
        $.ligerDialog.warn("Please fill in the order date!");
        return;
    }

    var sendDate = $("#txtSendDate").val();
    if (sendDate === "") {
        $.ligerDialog.warn("Please fill in the delivery date!");
        return;
    }

    var remarks = $("#txtRemarks").val();

    // Get all grid data
    var data = manager.getData();

    // 1. Remove empty rows
    for (var i = data.length - 1; i >= 0; i--) {
        if (data[i].goodsId == 0 || data[i].goodsId === "" || data[i].goodsName === "") {
            data.splice(i, 1);
        }
    }

    // 2. Check if any product is selected
    if (data.length === 0) {
        $.ligerDialog.warn('Please select at least one product!');
        return;
    }

    // 3. Validate that all product quantities are entered and valid
    for (var i = 0; i < data.length; i++) {
        if (data[i].num <= 0 || data[i].num === "" || data[i].num === "0" || data[i].num === "0.00") {
            $.ligerDialog.warn("Please enter a valid quantity for product in row " + (i + 1) + "!");
            return;
        }
    }

    var headJson = {
        id: param,
        venderId: venderId,
        bizDate: bizDate,
        sendDate: sendDate,
        bizId: bizId,
        remarks: remarks
    };

    // Build the JSON object with header and rows
    var list = JSON.stringify(headJson);
    list = list.substring(0, list.length - 1); // Remove the last closing brace
    list += ",\"Rows\":" + JSON.stringify(data) + "}";

    var postData = JSON.parse(list); // Final JSON to post

    $.ajax({
        type: "POST",
        url: 'ashx/SalesOrderListEdit.ashx',
        contentType: "application/json", // Required
        data: JSON.stringify(postData),
        success: function (jsonResult) {
            if (jsonResult === "Operation Successful!") {
                $.ligerDialog.waitting('Operation successful!');
                setTimeout(function () {
                    $.ligerDialog.closeWaitting();
                    location.reload();
                }, 2000);
            } else {
                $.ligerDialog.warn(jsonResult);
            }
        },
        error: function (xhr) {
            alert("An error occurred, please try again later: " + xhr.responseText);
        }
    });
}

var param = getUrlParam("id");

function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]);
    return null;
}

function getthedate(dd, dadd) {
    // Adds dadd days to date dd
    var a = new Date(dd).valueOf();
    a += dadd * 24 * 60 * 60 * 1000;
    a = new Date(a);
    var m = a.getMonth() + 1;
    m = m < 10 ? '0' + m : m;
    var d = a.getDate();
    d = d < 10 ? '0' + d : d;
    return a.getFullYear() + "-" + m + "-" + d;
}

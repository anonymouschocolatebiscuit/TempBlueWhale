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

function selectGet() {
    var start = $("#txtDateStar1").val();
    var end = $("#txtDateEnd1").val();
    var wlId = $("#ddlVenderList").val();
    var keys = document.getElementById("txtKeysGet").value;
    if (keys == "Please enter the document number") {
        keys = "";
    }
    manager._setUrl("CheckBillPayListAdd.aspx?Action=GetDataListSearchGet&keys=" + keys + "&start=" + start + "&end=" + end + "&wlId=" + wlId);
}

function selectBill() {
    var start = $("#txtDateStart").val();
    var end = $("#txtDateEnd").val();
    var wlId = $("#ddlVenderList").val();
    var keys = document.getElementById("txtKeys").value;
    if (keys == "Please enter the document number") {
        keys = "";
    }
    managersub._setUrl("CheckBillPayListAdd.aspx?Action=GetDataListSearch&keys=" + keys + "&start=" + start + "&end=" + end + "&wlId=" + wlId);
}

function addNewRow() {
    manager.addRow({
        bkId: "",
        payPrice: "",
        payTypeId: "",
        payNumber: "",
        remarks: ""
    });
    f_onAfterEdit(); // Recalculate
}

var manager;
$(function () {
    var form = $("#form").ligerForm();
    var target1 = $("#target1").ligerForm();

    var g = $.ligerui.get("ddlVenderList");
    g.set("Width", 250);

    var txtDateStart = $.ligerui.get("txtDateStart");
    txtDateStart.set("Width", 120);

    var txtDateEnd = $.ligerui.get("txtDateEnd");
    txtDateEnd.set("Width", 120);

    var txtDateStar1 = $.ligerui.get("txtDateStar1");
    txtDateStar1.set("Width", 120);

    var txtDateEnd1 = $.ligerui.get("txtDateEnd1");
    txtDateEnd1.set("Width", 120);

    window['g'] =
        manager = $("#maingrid").ligerGrid({
            columns: [
                {
                    display: '', isSort: false, width: 40, align: 'center', frozen: true, render: function (rowdata, rowindex, value) {
                        var h = "";
                        if (!rowdata._editing) {
                            h += "<a href='javascript:deleteRow()' title='Delete row' style='text-align:center;'><div class='ui-icon ui-icon-trash'></div></a>";
                        }
                        return h;
                    }
                },
                {
                    display: 'Payment Number', name: 'sourceNumber', width: 220, align: 'center',
                    totalSummary: {
                        type: 'count',
                        render: function (e) {
                            return 'Total:';
                        }
                    }
                },
                { display: 'Business Type', name: 'bizType', width: 120, align: 'center' },
                { display: 'Document Date', name: 'bizDate', width: 120, align: 'center' },
                { display: 'Document Amount', name: 'sumPriceAll', width: 120, align: 'right' },
                { display: 'Written-off Amount', name: 'priceCheckNowSum', width: 150, align: 'right' },
                { display: 'Unwritten-off Amount', name: 'priceCheckNo', width: 160, align: 'right' },
                {
                    display: 'Current Write-off Amount', name: 'priceCheckNow', width: 180, type: 'float', align: 'right', editor: { type: 'float', precision: 4 },
                    totalSummary: {
                        align: 'right',
                        type: 'sum',
                        render: function (e) {
                            return Math.round(e.sum * 100) / 100;
                        }
                    }
                }
            ],
            width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '190',
            url: 'CheckBillPayListAdd.aspx?Action=GetData',
            rownumbers: true,
            frozenRownumbers: true,
            dataAction: 'local',
            usePager: false,
            alternatingRow: false,
            totalSummary: false,
            enabledEdit: true,
            onBeforeSubmitEdit: f_onBeforeSubmitEdit,
            onAfterEdit: f_onAfterEdit
        });
});

// After editing event - payment amount
function f_onAfterEdit() {
    var sumPrice = 0;
    var data = manager.getData();
    for (var i = 0; i < data.length; i++) {
        sumPrice += Number(data[i].payPrice);
    }

    var sumPayPrice = 0;
    var datasub = managersub.getData();
    for (var i = 0; i < datasub.length; i++) {
        sumPayPrice += Number(datasub[i].priceCheckNow);
    }

    if (Number(sumPayPrice) > Number(sumPrice)) {
        $.ligerDialog.warn("Total write-off amount cannot exceed total payment amount!");
        return;
    }

    var disPrice = $("#txtDisPrice").val(); // Order discount

    var payPriceNowMore = Number(sumPrice) - Number(sumPayPrice) - Number(disPrice);

    $("#txtPayPriceNowMore").val(payPriceNowMore);
}

var managersub;
$(function () {
    window['gsub'] =
        managersub = $("#maingridsub").ligerGrid({
            columns: [
                {
                    display: '', isSort: false, width: 40, align: 'center', frozen: true, render: function (rowdata, rowindex, value) {
                        var h = "";
                        if (!rowdata._editing) {
                            h += "<a href='javascript:deleteRowSub()' title='Delete row' style='text-align:center;'><div class='ui-icon ui-icon-trash'></div></a>";
                        }
                        return h;
                    }
                },
                {
                    display: 'Payable Number', name: 'sourceNumber', width: 220, align: 'center',
                    totalSummary: {
                        type: 'count',
                        render: function (e) {
                            return 'Total:';
                        }
                    }
                },
                { display: 'Business Type', name: 'bizType', width: 120, align: 'center' },
                { display: 'Document Date', name: 'bizDate', width: 120, align: 'center' },
                { display: 'Document Amount', name: 'sumPriceAll', width: 120, align: 'right' },
                { display: 'Written-off Amount', name: 'priceCheckNowSum', width: 150, align: 'right' },
                { display: 'Unwritten-off Amount', name: 'priceCheckNo', width: 160, align: 'right' },
                {
                    display: 'Current Write-off Amount', name: 'priceCheckNow', width: 180, type: 'float', align: 'right', editor: { type: 'float', precision: 4 },
                    totalSummary: {
                        align: 'right',
                        type: 'sum',
                        render: function (e) {
                            return Math.round(e.sum * 100) / 100;
                        }
                    }
                }
            ],
            width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '190',
            url: 'CheckBillPayListAdd.aspx?Action=GetDataSub',
            rownumbers: true,
            frozenRownumbers: true,
            dataAction: 'local',
            usePager: false,
            alternatingRow: false,
            totalSummary: true,
            enabledEdit: true,
            onBeforeSubmitEdit: f_onBeforeSubmitEditSub,
            onAfterEdit: f_onAfterEdit
        });
});

function f_onBeforeEdit(e) {
    if (e.rowindex <= 2) return true;
    return false;
}

// Validate before submit
function f_onBeforeSubmitEdit(e) {
    var data = manager.getData();
    if (Number(e.value) > Number(data[e.rowindex].priceCheckNo)) {
        $.ligerDialog.warn("Write-off amount cannot exceed unwritten-off amount!");
        return false;
    }
    return true;
}

function f_onBeforeSubmitEditSub(e) {
    var data = managersub.getData();
    if (Number(e.value) > Number(data[e.rowindex].priceCheckNo)) {
        $.ligerDialog.warn("Write-off amount cannot exceed unwritten-off amount!");
        return false;
    }
    return true;
}

var rowNumber = 9;

function deleteRow() {
    manager.deleteSelectedRow();
}

function deleteRowSub() {
    managersub.deleteSelectedRow();
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

function save() {
    var data = manager.getData();
    var datasub = managersub.getData();

    // 1. Remove blank rows
    for (var i = data.length - 1; i >= 0; i--) {
        if (data[i].sourceNumber == "") {
            data.splice(i, 1);
        }
    }

    // 2. Validate selected payment info
    if (data.length == 0) {
        $.ligerDialog.warn('Please select payment info!');
        return;
    }

    for (var i = 0; i < data.length; i++) {
        if (Number(data[i].priceCheckNow) <= 0 && data[i].sourceNumber != "") {
            $.ligerDialog.warn("Please enter write-off amount for payment row " + (i + 1));
            return;
        }
    }

    for (var i = datasub.length - 1; i >= 0; i--) {
        if (datasub[i].sourceNumber == "") {
            datasub.splice(i, 1);
        }
    }

    if (datasub.length == 0) {
        $.ligerDialog.warn('Please select payable info!');
        return;
    }

    for (var i = 0; i < datasub.length; i++) {
        if (Number(datasub[i].priceCheckNow) <= 0 && datasub[i].sourceNumber != "") {
            $.ligerDialog.warn("Please enter write-off amount for payable row " + (i + 1));
            return;
        }
    }

    var bizDate = $("#txtBizDate").val();
    if (bizDate == "") {
        $.ligerDialog.warn("Please enter write-off date!");
        return;
    }

    var remarks = $("#txtRemarks").val();
    var wlId = $("#ddlVenderList").val();

    var CheckPrice = 0;
    for (var i = 0; i < data.length; i++) {
        CheckPrice += Number(data[i].priceCheckNow);
    }

    var sumPayPrice = 0;
    for (var i = 0; i < datasub.length; i++) {
        sumPayPrice += Number(datasub[i].priceCheckNow);
    }

    if (CheckPrice != sumPayPrice) {
        $.ligerDialog.warn("Total write-off amounts do not match, please adjust!");
        return;
    }

    var headJson = { ClientId: wlId, BizDate: bizDate, Remarks: remarks, CheckPrice: CheckPrice };
    var dataNew = [];
    dataNew.push(headJson);

    var list = JSON.stringify(headJson);
    list = list.substring(0, list.length - 1);
    list += ",\"Rows\":";
    list += JSON.stringify(data);
    list += ",\"RowsBill\":";
    list += JSON.stringify(datasub);
    list += "}";

    var postData = JSON.parse(list);

    $.ajax({
        type: "POST",
        url: 'ashx/CheckBillPayListAdd.ashx',
        contentType: "application/json",
        data: JSON.stringify(postData),
        success: function (jsonResult) {
            if (jsonResult == "操作成功！") {
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

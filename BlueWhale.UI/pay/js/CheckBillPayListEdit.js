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

function selectGet() {
    var start = $("#txtDateStar1").val();
    var end = $("#txtDateEnd1").val();

    var wlId = $("#ddlVenderList").val();

    var keys = document.getElementById("txtKeysGet").value;
    if (keys == "Please enter the document number") {
        keys = "";
    }

    manager._setUrl("CheckBillPayListEdit.aspx?Action=GetDataListSearchGet&keys=" + keys + "&start=" + start + "&end=" + end + "&wlId=" + wlId + "&id=" + getUrlParam("id"));
}

function selectBill() {
    var start = $("#txtDateStart").val();
    var end = $("#txtDateEnd").val();

    var wlId = $("#ddlVenderList").val();

    var keys = document.getElementById("txtKeys").value;
    if (keys == "Please enter the document number") {
        keys = "";
    }

    managersub._setUrl("CheckBillPayListEdit.aspx?Action=GetDataListSearch&keys=" + keys + "&start=" + start + "&end=" + end + "&wlId=" + wlId + "&id=" + getUrlParam("id"));
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

    window['g'] = manager = $("#maingrid").ligerGrid({
        columns: [
            {
                display: '', isSort: false, width: 40, align: 'center', frozen: true, render: function (rowdata, rowindex, value) {
                    var h = "";
                    if (!rowdata._editing) {
                        // h += "<a href='javascript:addNewRow()' title='Add Row' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                        h += "<a href='javascript:deleteRow()' title='Delete Row' style='text-align:center;'><div class='ui-icon ui-icon-trash'></div></a>";
                    } else {
                        // h += "<a href='javascript:endEdit(" + rowindex + ")'>Submit</a> ";
                        // h += "<a href='javascript:cancelEdit(" + rowindex + ")'>Cancel</a>";
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
            { display: 'Document Date', name: 'bizDate', width: 80, align: 'center' },
            { display: 'Document Amount', name: 'sumPriceAll', width: 120, align: 'right' },
            { display: 'Amount Written Off', name: 'priceCheckNowSum', width: 120, align: 'right' },
            { display: 'Amount Not Written Off', name: 'priceCheckNo', width: 120, align: 'right' },
            {
                display: 'Amount to be Written Off', name: 'priceCheckNow', width: 120, type: 'float', align: 'right', editor: { type: 'float', precision: 4 },
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
        url: 'CheckBillPayListEdit.aspx?Action=GetData&id=' + getUrlParam("id"),
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

// After Edit Event -------- Payment Amount
function f_onAfterEdit() {
    var sumPrice = 0; // Total payment amount
    var data = manager.getData();
    for (var i = 0; i < data.length; i++) {
        sumPrice += Number(data[i].payPrice);
    }

    var sumPayPrice = 0; // Total written off amount
    var datasub = managersub.getData();
    for (var i = 0; i < datasub.length; i++) {
        sumPayPrice += Number(datasub[i].priceCheckNow);
    }

    if (Number(sumPayPrice) > Number(sumPrice)) {
        $.ligerDialog.warn("Total written off amount cannot exceed the total payment amount!");
        return;
    }

    var disPrice = $("#txtDisPrice").val(); // Total discount

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
                            // h += "<a href='javascript:addNewRow()' title='Add Row' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                            h += "<a href='javascript:deleteRowSub()' title='Delete Row' style='text-align:center;'><div class='ui-icon ui-icon-trash'></div></a>";
                        } else {
                            // h += "<a href='javascript:endEdit(" + rowindex + ")'>Submit</a> ";
                            // h += "<a href='javascript:cancelEdit(" + rowindex + ")'>Cancel</a>";
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
                { display: 'Document Date', name: 'bizDate', width: 80, align: 'center' },
                { display: 'Document Amount', name: 'sumPriceAll', width: 120, align: 'right' },
                { display: 'Amount Written Off', name: 'priceCheckNowSum', width: 120, align: 'right' },
                { display: 'Amount Not Written Off', name: 'priceCheckNo', width: 120, align: 'right' },
                {
                    display: 'Amount to be Written Off', name: 'priceCheckNow', width: 120, type: 'float', align: 'right', editor: { type: 'float', precision: 4 },
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
            url: 'CheckBillPayListEdit.aspx?Action=GetDataSub&id=' + getUrlParam("id"),
            rownumbers: true,
            frozenRownumbers: true,
            dataAction: 'local',
            usePager: false,
            alternatingRow: false,
            totalSummary: false,
            enabledEdit: true,
            onBeforeSubmitEdit: f_onBeforeSubmitEditSub,
            onAfterEdit: f_onAfterEditSub
        });
});

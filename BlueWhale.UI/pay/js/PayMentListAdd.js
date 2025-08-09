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

function selectBill() {
    var start = $("#txtDateStart").val();
    var end = $("#txtDateEnd").val();

    var wlId = $("#ddlVenderList").val();

    var keys = document.getElementById("txtKeys").value;
    if (keys == "Please Enter Receipt No.") {
        keys = "";
    }

    managersub._setUrl("PayMentListAdd.aspx?Action=GetDataListSearch&keys=" + keys + "&start=" + start + "&end=" + end + "&wlId=" + wlId);
}

function addNewRow() {
    manager.addRow({

        bkId: "",
        payPrice: "",
        payTypeId: "",
        payNumber: "",

        remarks: ""
    });

    f_onAfterEdit();//Count again
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

    window['g'] =
        manager = $("#maingrid").ligerGrid({
            columns: [

                {
                    display: '', isSort: false, width: 40, align: 'center', frozen: true, render: function (rowdata, rowindex, value) {
                        var h = "";
                        if (!rowdata._editing) {
                            h += "<a href='javascript:addNewRow()' title='Add row' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                            h += "<a href='javascript:deleteRow()' title='Delete row' style='float:right;'><div class='ui-icon ui-icon-trash'></div></a> ";
                        }
                        return h;
                    }
                },
                {
                    display: 'Settlement Account', name: 'bkId', width: 220, isSort: false, textField: 'bkName',
                    editor: {
                        type: 'select',
                        url: "../baseSet/AccountList.aspx?Action=GetDDLList&r=" + Math.random(),
                        valueField: 'id', textField: 'names'
                    },
                    totalSummary:
                    {
                        type: 'count',
                        render: function (e) {
                            return 'Total: ';
                        }
                    }

                },
                {
                    display: 'Collection Amount', name: 'payPrice', width: 140, type: 'float', align: 'right', editor: { type: 'float' },

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
                    display: 'Settlement Method', name: 'payTypeId', width: 140, isSort: false, textField: 'payTypeName',
                    editor: {
                        type: 'select',
                        url: "../baseSet/PayTypeList.aspx?Action=GetDDLList&r=" + Math.random(),
                        valueField: 'typeId', textField: 'typeName'
                    }

                },
                { display: 'Settlement Number', name: 'payNumber', width: 140, align: 'left', type: 'text', editor: { type: 'text' } },

                { display: 'Remarks', name: 'remarks', width: 220, align: 'left', type: 'text', editor: { type: 'text' } }
            ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '170',
            url: 'PayMentListAdd.aspx?Action=GetData',
            rownumbers: true,
            frozenRownumbers: true,
            dataAction: 'local',
            usePager: false,
            alternatingRow: false,

            totalSummary: false,
            enabledEdit: true,

            onAfterEdit: f_onAfterEdit
        });
});

//After Edit---------Payment Amount
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
        $.ligerDialog.warn("Amount written off cannot greater than payment amount!");
        return;
    }

    var disPrice = $("#txtDisPrice").val();

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
                            h += "<a href='javascript:deleteRowSub()' title='Delete row' style='text-align:center;'><div class='ui-icon ui-icon-trash'></div></a> ";
                        }
                        return h;
                    }
                },
                {
                    display: 'Receipt Number', name: 'sourceNumber', width: 220, align: 'center',

                    totalSummary:
                    {
                        type: 'count',
                        render: function (e) {
                            return 'Total: ';
                        }
                    }
                },
                { display: 'Business Type', name: 'bizType', width: 120, align: 'center' },
                { display: 'Invoice Date', name: 'bizDate', width: 160, align: 'center' },
                { display: 'Invoice Amount', name: 'sumPriceAll', width: 180, align: 'right' },
                { display: 'Written-off Amount', name: 'priceCheckNowSum', width: 180, align: 'right' },
                { display: 'Remaining Balance', name: 'priceCheckNo', width: 180, align: 'right' },
                {
                    display: 'Write-off Amount', name: 'priceCheckNow', width: 280, type: 'float', align: 'right', editor: { type: 'float', precision: 4 },
                    totalSummary:
                    {
                        align: 'right',
                        type: 'sum',
                        render: function (e) {
                            return Math.round(e.sum * 100) / 100;
                        }
                    }
                }
            ],
            width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '225',
            url: 'PayMentListAdd.aspx?Action=GetDataSub',
            rownumbers: true,
            frozenRownumbers: true,
            dataAction: 'local',
            usePager: false,
            alternatingRow: false,

            totalSummary: true,
            enabledEdit: true,
            onBeforeSubmitEdit: f_onBeforeSubmitEdit,
            onAfterEdit: f_onAfterEdit
        }
        );
});

//Only allow to edit first three rows
function f_onBeforeEdit(e) {
    if (e.rowindex <= 2) return true;
    return false;
}

function f_onBeforeSubmitEdit(e) {
    var data = managersub.getData();

    if (Number(e.value) > Number(data[e.rowindex].priceCheckNo)) {
        $.ligerDialog.warn("The verification amount cannot be greater than the unverified amount!");
        return false;
    }
    return true;
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

function deleteRowSub() {
    managersub.deleteSelectedRow();
}

var newrowid = 100;

function getSelected() {
    var row = manager.getSelectedRow();
    if (!row) {
        alert('Please select a row'); return;
    }
    alert(JSON.stringify(row));
}

function getData() {
    var data = manager.getData();
    alert(JSON.stringify(data));
}

function save() {
    var data = manager.getData();
    var datasub = managersub.getData();

    //1. Remove spacing
    for (var i = data.length - 1; i >= 0; i--) {
        if (data[i].bkId == 0 || data[i].bkName == "") {
            data.splice(i, 1);
        }
    }

    //2、Decide to choose the product or not
    if (data.length == 0) {
        $.ligerDialog.warn('Please enter payment detail');

        return;
        alert("Execution skipped");
    }

    //3、Decide if the product amount is entered
    for (var i = 0; i < data.length; i++) {
        if (data[i].bkId <= 0 || data[i].bkName == "") {
            $.ligerDialog.warn("Please select the settlement account for row " + (i + 1) + "!");

            return;
            alert("Execution skipped");
        }

        if (data[i].payPrice <= 0 || data[i].payPrice == "") {
            $.ligerDialog.warn("Please enter the payment amount for row " + (i + 1) + "!");

            return;
            alert("Execution skipped");
        }


        if (data[i].payTypeId <= 0 || data[i].payTypeName == "") {
            $.ligerDialog.warn("Please select the settlement method for row " + (i + 1) + "!");

            return;
            alert("Execution skipped");
        }
    }

    // Delete extra spacing
    for (var i = datasub.length - 1; i >= 0; i--) {
        if (datasub[i].sourceNumber == "") {
            datasub.splice(i, 1);
        }
    }

    // Check whether all product quantities have been entered
    for (var i = 0; i < datasub.length; i++) {
        if (Number(datasub[i].priceCheckNow) <= 0 && datasub[i].sourceNumber != "") {

            $.ligerDialog.warn("Please enter the verification amount for row " + (i + 1) + "!");

            return;
            alert("Execution skipped");
        }
    }

    var bizDate = $("#txtBizDate").val();
    if (bizDate == "") {
        $.ligerDialog.warn("Please enter the payment date!");
        return;
    }

    var remarks = $("#txtRemarks").val();

    var disPrice = $("#txtDisPrice").val();
    var payPriceNowMore = $("#txtPayPriceNowMore").val();
    var wlId = $("#ddlVenderList").val();

    var headJson = { wlId: wlId, disPrice: disPrice, payPriceNowMore: payPriceNowMore, bizDate: bizDate, remarks: remarks };

    var dataNew = [];
    dataNew.push(headJson);

    var list = JSON.stringify(headJson);// Deserialize into a string, table header  
    list = list.substring(0, list.length - 1);// Remove the last curly brace  

    list += ",\"Rows\":";
    list += JSON.stringify(data);// Insert account information       

    list += ",\"RowsBill\":";

    list += JSON.stringify(datasub);// Insert verification information     

    list += "}";

    var postData = JSON.parse(list);// Final json

    $.ajax({
        type: "POST",
        url: 'ashx/PayMentListAdd.ashx',
        contentType: "application/json",
        data: JSON.stringify(postData),
        success: function (jsonResult) {

            if (jsonResult == "Execution successful") {
                $.ligerDialog.waitting('Execution successful'); setTimeout(function () { $.ligerDialog.closeWaitting(); location.reload(); }, 2000);
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

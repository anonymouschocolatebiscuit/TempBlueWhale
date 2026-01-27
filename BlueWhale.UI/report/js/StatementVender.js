var manager;

$(function () {
    $("#form").ligerForm();

    var dateStart = $.ligerui.get("txtDateStart");
    dateStart.set("Width", 110);
    var dateEnd = $.ligerui.get("txtDateEnd");
    dateEnd.set("Width", 110);
    var txtVenderList = $.ligerui.get("txtVenderList");
    txtVenderList.set("Width", 310);

    manager = $("#maingrid").ligerGrid({
        columns: [
            {
                display: 'Invoice Date', name: 'bizDate', width: 100, align: 'center',
                render: function (row) {
                    var html = row.bizType == "Opening balance" ? "" : row.bizDate;
                    return html;
                },
                totalSummary:
                {
                    type: 'count',
                    render: function (e) {
                        return 'Total：';
                    }
                }
            },
            { display: 'Receipt Number', name: 'number', width: 120, align: 'center' },
            { display: 'Business Type', name: 'bizType', width: 120, align: 'center' },
            {
                display: 'Purchase Amount', name: 'sumPrice', width: 120, align: 'right',
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
                display: 'Discount Amount', name: 'disPrice', width: 180, align: 'right',

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
                display: 'Payable Amount', name: 'payNeed', width: 120, align: 'right',
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
                display: 'Actual Amount Paid', name: 'payReady', width: 165, align: 'right',
                totalSummary:
                {
                    align: 'right',
                    type: 'sum',
                    render: function (e) {
                        return Math.round(e.sum * 100) / 100;
                    }
                }
            },
            { display: 'Payable Balance', name: 'payEnd', width: 120, align: 'right' }
        ], width: '98%',
        height: '98%',
        dataAction: 'local',
        usePager: false,
        rownumbers: true,
        alternatingRow: false,
        onDblClickRow: function (data, rowindex, rowobj) {
            viewRow();
        }
    }
    );

});

function search() {
    var start = $("#txtDateStart").val();
    var end = $("#txtDateEnd").val();
    var typeId = $("#txtVenderList").val();
    var typeIdString = typeId.split(";");

    if (typeIdString != "") {
        typeId = "";
        for (var i = 0; i < typeIdString.length; i++) {
            typeId += "'" + typeIdString[i] + "'" + ",";
        }
        typeId = typeId.substring(0, typeId.length - 1);

    }
    if (typeId == "") {
        $.ligerDialog.warn('Please select a vendor！');
        return;
    }
    manager._setUrl("StatementVender.aspx?Action=GetDataList&start=" + start + "&end=" + end + "&typeId=" + typeId);
}

function viewRow() {
    var row = manager.getSelectedRow();
}

function reload() {
    manager.reload();
}

function openBill(number, bizType) {
    if (bizType == "Regular Purchase" || bizType == "Purchase Return") {
        window.location.href = "../sales/PurReceiptListView.aspx?id=0&number=" + number;
    }
    if (bizType == "Payment") {
        window.location.href = "../pay/PayMentListView.aspx?id=0&number=" + number;
    }
}
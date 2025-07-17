var manager;
$(function () {
    var form = $("#form").ligerForm();
    var dateStart = $.ligerui.get("txtDateStart");
    dateStart.set("Width", 110);
    var dateEnd = $.ligerui.get("txtDateEnd");
    dateEnd.set("Width", 110);
    var txtVenderList = $.ligerui.get("txtVenderList");
    txtVenderList.set("Width", 310);

    manager = $("#maingrid").ligerGrid({
        columns: [
            {
                display: 'Vendor', name: 'wlName', width: 200, align: 'left',
                totalSummary:
                {
                    type: 'count',
                    render: function (e) {
                        return 'Total:';
                    }
                }
            },
            {
                display: 'Invoice Date', name: 'bizDate', width: 120, align: 'center',
                render: function (row) {
                    var html = row.bizType == "Opening Balance" ? "" : row.bizDate;
                    return html;
                }
            },
            { display: 'Receipt Number', name: 'number', width: 150, align: 'center' },
            { display: 'Business Type', name: 'bizType', width: 150, align: 'center' },
            {
                display: 'Add payable', name: 'payNeed', width: 180, align: 'right',
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
                display: 'Add prepayment', name: 'payReady', width: 180, align: 'right',
                totalSummary:
                {
                    align: 'right',
                    type: 'sum',
                    render: function (e) {
                        return Math.round(e.sum * 100) / 100;
                    }
                }
            },
            { display: 'Payable balance', name: 'payEnd', width: 180, align: 'right' }

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
        for (var i = 0; i < typeIdString.length; i++) { typeId += "'" + typeIdString[i] + "'" + ","; }
        typeId = typeId.substring(0, typeId.length - 1);
    }
    manager._setUrl("VendorNeedPayReport.aspx?Action=GetDataList&start=" + start + " &end=" + end + " &typeId=" + typeId);
}

function viewRow() {
    var row = manager.getSelectedRow();
}

function reload() {
    manager.reload();
}

function openBill(number, bizType) {
    if (bizType == "Regular Purchase" || bizType == "Purchase Return") {
        window.location.href = "../buy/PurReceiptListView.aspx?id=0&number="
            + number;
    } if (bizType == "Purchase") { window.location.href = "../pay/PayMentListView.aspx?id=0&number=" + number; }
}
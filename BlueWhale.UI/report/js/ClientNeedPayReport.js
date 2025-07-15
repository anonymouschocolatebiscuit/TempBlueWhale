
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
                display: 'Client', name: 'wlName', width: 100, align: 'left',

                totalSummary:
                {
                    type: 'count',
                    render: function (e) {
                        return 'Total: ';
                    }
                }
            },
            {
                display: 'Bill Date', name: 'bizDate', width: 80, align: 'center',
                render: function (row) {
                    var html = row.bizType == "Opening Balance" ? "" : row.bizDate;
                    return html;
                }
            },
            { display: 'Bill No', name: 'number', width: 150, align: 'center' },
            { display: 'Business Type', name: 'bizType', width: 120, align: 'center' },
            {
                display: 'Increase in Receivables', name: 'payNeed', width: 180, align: 'right', type: 'float',
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
                display: 'Increase in Advance Payments', name: 'payReady', width: 240, align: 'right', type: 'float',
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
                display: 'Receivable Balance', name: 'payEnd', width: 180, align: 'right', type: 'float'

            }
        ], width: '100%%',
        //pageSizeOptions: [5, 10, 15, 20],
        height: '98%',
        // pageSize: 15,
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
    //alert(typeId);
    manager._setUrl("ClientNeedPayReport.aspx?Action=GetDataList&start=" + start + "&end=" + end + "&typeId=" + typeId);
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


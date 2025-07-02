var manager;
$(function () {
    var form = $("#form").ligerForm();

    var dateStart = $.ligerui.get("txtDateStart");
    dateStart.set("Width", 110);

    var dateEnd = $.ligerui.get("txtDateEnd");
    dateEnd.set("Width", 110);

    manager = $("#maingrid").ligerGrid({
        columns: [
            { display: 'Account Code', name: 'code', width: 120, align: 'center' },
            {
                display: 'Account Name', name: 'bkName', width: 120, align: 'left',
                totalSummary: {
                    type: 'count',
                    render: function (e) {
                        return 'Total:';
                    }
                }
            },
            {
                display: 'Date', name: 'bizDate', width: 80, align: 'center',
                render: function (row) {
                    return row.bizType == "Opening Balance" ? "" : row.bizDate;
                }
            },
            { display: 'Document Number', name: 'number', width: 150, align: 'center' },
            { display: 'Business Type', name: 'bizType', width: 130, align: 'center' },
            { display: 'Transaction Party', name: 'wlName', width: 170, align: 'left' },
            {
                display: 'Opening Balance', name: 'priceBegin', width: 130, align: 'right'
            },
            {
                display: 'Income', name: 'priceIn', width: 80, align: 'right',
                totalSummary: {
                    align: 'right',
                    type: 'sum',
                    render: function (e) {
                        return Math.round(e.sum * 100) / 100;
                    }
                }
            },
            {
                display: 'Expenditure', name: 'priceOut', width:100, align: 'right',
                totalSummary: {
                    align: 'right',
                    type: 'sum',
                    render: function (e) {
                        return Math.round(e.sum * 100) / 100;
                    }
                }
            },
            {
                display: 'Account Balance', name: 'priceEnd', width: 130, align: 'right',
                render: function (row) {
                    var balance = Number(row.priceBegin) + Number(row.priceIn) - Number(row.priceOut);
                    if (row.bizType == "Opening Balance") {
                        balance = row.priceEnd;
                    }
                    return Math.round(balance * 100) / 100;
                }
            }
        ],
        width: '98%',
        height: '98%',
        dataAction: 'local',
        usePager: false,
        rownumbers: true,
        alternatingRow: false,
        onDblClickRow: function (data, rowindex, rowobj) {
            viewRow();
        }
    });
});

function search() {
    var start = $("#txtDateStart").val();
    var end = $("#txtDateEnd").val();
    var typeId = $("#txtFlagList").val();
    var typeIdString = typeId.split(";");

    if (typeIdString != "") {
        typeId = "";
        for (var i = 0; i < typeIdString.length; i++) {
            typeId += "'" + typeIdString[i] + "',";
        }
        typeId = typeId.substring(0, typeId.length - 1);
    }

    manager._setUrl("AccountFlowReport.aspx?Action=GetDataList&start=" + start + "&end=" + end + "&typeId=" + typeId);
}

function viewRow() {
    var row = manager.getSelectedRow();
    // Open detail page if needed
}

function reload() {
    manager.reload();
}

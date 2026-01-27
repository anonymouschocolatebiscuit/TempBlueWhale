var manager;

$(function () {
    var form = $("#form").ligerForm();

    var dateStart = $.ligerui.get("txtDateStart");
    dateStart.set("Width", 110);

    var dateEnd = $.ligerui.get("txtDateEnd");
    dateEnd.set("Width", 110);

    var txtFlagList = $.ligerui.get("txtFlagList");
    txtFlagList.set("Width", 150);

    var txtGoodsList = $.ligerui.get("txtGoodsList");
    txtGoodsList.set("Width", 250);

    manager = $("#maingrid").ligerGrid({
        columns: [
            { display: 'Item Code', name: 'code', width: 110, align: 'center', frozen: true },
            { display: 'Item Name', name: 'goodsName', width: 140, align: 'center', frozen: true },
            { display: 'Specification', name: 'spec', width: 140, align: 'center', frozen: true },
            { display: 'Unit Name', name: 'unitName', width: 90, align: 'center', frozen: true },
            { display: 'Inventory Name', name: 'storeName', width: 130, align: 'center' },
            {
                display: 'Begin Period', columns: [
                    { display: 'Quantity', name: 'sumNumBegin', width: 70, align: 'right' },
                    { display: 'Cost', name: 'sumPriceBegin', width: 70, align: 'right' }
                ]
            },
            {
                display: 'Current Period Revenue', columns: [
                    { display: 'Quantity', name: 'sumNumIn', width: 80, align: 'right' },
                    { display: 'Cost', name: 'sumPriceIn', width: 80, align: 'right' }
                ]
            },
            {
                display: 'Year-to-Date Revenue', columns: [
                    { display: 'Quantity', name: 'sumNumInAll', width: 80, align: 'right' },
                    { display: 'Cost', name: 'sumPriceInAll', width: 80, align: 'right' }
                ]
            },
            {
                display: 'Current Period Sale', columns: [
                    { display: 'Quantity', name: 'sumNumOut', width: 70, align: 'right' },
                    { display: 'Cost', name: 'sumPriceOut', width: 70, align: 'right' }
                ]
            },
            {
                display: 'Year-to-Date Sales', columns: [
                    { display: 'Quantity', name: 'sumNumOutAll', width: 70, align: 'right' },
                    { display: 'Cost', name: 'sumPriceOutAll', width: 70, align: 'right' }
                ]
            },
            {
                display: 'Current Period Balance', columns: [
                    { display: 'Quantity', name: 'sumNumEnd', width: 80, align: 'right' },
                    { display: 'Cost', name: 'sumPriceEnd', width: 80, align: 'right' }
                ]
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
    var goodsList = $("#txtGoodsList").val();
    var typeId = $("#txtFlagList").val();

    var goodsIdString = goodsList.split(";");
    var typeIdString = typeId.split(";");

    if (goodsIdString !== "") {
        goodsList = "";
        for (var i = 0; i < goodsIdString.length; i++) {
            goodsList += "'" + goodsIdString[i] + "',";
        }
        goodsList = goodsList.substring(0, goodsList.length - 1);
    }

    if (typeIdString !== "") {
        typeId = "";
        for (var j = 0; j < typeIdString.length; j++) {
            typeId += "'" + typeIdString[j] + "',";
        }
        typeId = typeId.substring(0, typeId.length - 1);
    }

    manager._setUrl("GoodsOutInSumReport.aspx?Action=GetDataList&start=" + start + "&end=" + end + "&goodsId=" + goodsList + "&ckId=" + typeId);
}

function viewRow() {
    var row = manager.getSelectedRow();
}

function reload() {
    manager.reload();
}

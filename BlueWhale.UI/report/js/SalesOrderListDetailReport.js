var manager;
$(function () {
    var form = $("#form").ligerForm();

    var dateStart = $.ligerui.get("txtDateStart");
    dateStart.set("Width", 110);

    var dateEnd = $.ligerui.get("txtDateEnd");
    dateEnd.set("Width", 110);

    var txtFlagList = $.ligerui.get("txtFlagList");
    txtFlagList.set("Width", 100);

    manager = $("#maingrid").ligerGrid({

        columns: [
            { display: 'Sales Date', name: 'bizDate', width: 80, align: 'center', valign: 'center' },
            {
                display: 'Receipt Number', name: 'number', width: 150, align: 'center',

                totalSummary:
                {
                    type: 'count',
                    render: function (e) {
                        return 'Total:';
                    }
                }
            },
            {
                display: 'Business Type', name: 'types', width: 90, align: 'center',

                render: function (row) {
                    var html = row.types == 1 ? "Purchase" : "<span style='color:green'>Return Goods</span>";
                    return html;
                }
            },
            { display: 'Client', name: 'wlName', width: 170, align: 'left' },
            { display: 'Item Number', name: 'code', width: 70, align: 'center' },
            { display: 'Goods Name', name: 'goodsName', width: 120, align: 'left' },
            { display: 'Specification', name: 'spec', width: 80, align: 'center' },
            { display: 'Unit', name: 'unitName', width: 50, align: 'center' },
            { display: 'Inventory', name: 'ckName', width: 80, align: 'center' },
            { display: 'Unit Price', name: 'price', width: 80, align: 'right' },
            {
                display: 'Quantity', name: 'num', width: 70, align: 'right',

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
                display: 'Original Price', name: 'price', width: 70, type: 'float', align: 'right', editor: { type: 'float' }
            },
            {
                display: 'Discount%', name: 'dis', width: 60, type: 'float', align: 'right', editor: { type: 'float' }
            },
            {
                display: 'Discount Amount', name: 'sumPriceDis', width: 70, type: 'float', align: 'right', editor: { type: 'float' },
                totalSummary:
                {
                    align: 'center',
                    type: 'sum',
                    render: function (e) {
                        var itemSumPriceDis = e.sum;
                        return "<span id='sumPriceItemDis'>" + Math.round(itemSumPriceDis * 10000) / 10000 + "</span>";
                    }
                }
            },

            {
                display: 'Current Price', name: 'priceNow', width: 70, type: 'float', align: 'right', editor: { type: 'float' }
            },

            {
                display: 'Amount', name: 'sumPriceNow', width: 80, type: 'float', align: 'right', editor: { type: 'float' },

                totalSummary:
                {
                    align: 'center',
                    type: 'sum',
                    render: function (e) {

                        var itemSumPriceNow = e.sum;
                        return "<span id='sumPriceItemNow'>" + Math.round(itemSumPriceNow * 10000) / 10000 + "</span>";
                    }
                }

            },
            { display: 'Tax rate%', name: 'tax', width: 60, type: 'int', align: 'center', editor: { type: 'int' } },
            { display: 'Unit Price Including Tax', name: 'priceTax', width: 110, type: 'float', align: 'center', editor: { type: 'float' } },
            {
                display: 'Tax Amount', name: 'sumPriceTax', width: 80, type: 'float', align: 'right',

                totalSummary:
                {
                    align: 'center',
                    type: 'sum',
                    render: function (e) {
                        var itemSumPriceTax = e.sum;
                        return "<span id='sumPriceItemTax'>" + Math.round(itemSumPriceTax * 10000) / 10000 + "</span>";
                    }
                }
            },
            {
                display: 'Total Price Including Tax', name: 'sumPriceAll', width: 80, type: 'float', align: 'right', editor: { type: 'float' },
                totalSummary:
                {
                    align: 'center', 
                    type: 'sum',
                    render: function (e) {
                        var itemSumPriceAll = e.sum;
                        return "<span id='sumPriceItemAll'>" + Math.round(itemSumPriceAll * 10000) / 10000 + "</span>";
                    }
                }

            }
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
    var wlId = $("#txtVenderCode").val();
    var goodsList = $("#txtGoodsList").val();
    var typeId = $("#txtFlagList").val();
    var wlIdString = wlId.split(";");
    var goodsIdString = goodsList.split(";");
    var typeIdString = typeId.split(";");

    if (wlIdString != "") {
        wlId = "";
        for (var i = 0; i < wlIdString.length; i++) {
            wlId += "'" + wlIdString[i] + "'" + ",";
        }
        wlId = wlId.substring(0, wlId.length - 1);
    }

    if (goodsIdString != "") {
        goodsList = "";
        for (var i = 0; i < goodsIdString.length; i++) {
            goodsList += "'" + goodsIdString[i] + "'" + ",";
        }
        goodsList = goodsList.substring(0, goodsList.length - 1);
    }

    if (typeIdString != "") {
        typeId = "";
        for (var i = 0; i < typeIdString.length; i++) {
            typeId += "'" + typeIdString[i] + "'" + ",";
        }
        typeId = typeId.substring(0, typeId.length - 1);
    }

    manager._setUrl("SalesOrderListDetailReport.aspx?Action=GetDataList&start=" + start + "&end=" + end + "&wlId=" + wlId + "&goodsId=" + goodsList + "&typeId=" + typeId);
}

function reload() {
    manager.reload();
}
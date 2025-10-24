
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

            { display: 'Insert Warehouse Date', name: 'bizDate', width: 200, align: 'center', valign: 'center' },
            {
                display: 'Invoice Number', name: 'number', width: 150, align: 'center',

                totalSummary:
                {
                    type: 'count',
                    render: function (e) {
                        return 'Total：';
                    }
                }
            },

            { display: 'Goods Code', name: 'code', width: 120, align: 'center' },
            { display: 'Goods Name', name: 'goodsName', width: 180, align: 'left' },
            { display: 'Spec', name: 'spec', width: 120, align: 'center' },
            { display: 'Unit', name: 'unitName', width: 80, align: 'center' },
            { display: 'Warehouse', name: 'ckName', width: 100, align: 'center' },
            {
                display: 'Quantity', name: 'num', width: 100, align: 'right',
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
                display: 'Cost Unit Price', name: 'price', width: 120, type: 'float', align: 'right', editor: { type: 'float' }
            },
            {
                display: 'Cost Price', name: 'sumPriceNow', width: 120, type: 'float', align: 'right', editor: { type: 'float' },

                totalSummary:
                {
                    align: 'center',  
                    type: 'sum',
                    render: function (e) {  

                        var itemSumPriceNow = e.sum;
                        return "<span id='sumPriceItemNow'>" + Math.round(itemSumPriceNow * 10000) / 10000 + "</span>";//formatCurrency(suminf.sum)
                    }
                }
            }
        ], width: '98%',
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
    });
});

function search() {
    var start = $("#txtDateStart").val();
    var end = $("#txtDateEnd").val();

    var wlId = "";
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

    manager._setUrl("ProduceListDetailReport.aspx?Action=GetDataList&start=" + start + "&end=" + end + "&wlId=" + wlId + "&goodsId=" + goodsList + "&typeId=" + typeId);
}

function viewRow() {
    var row = manager.getSelectedRow();

    //          top.topManager.openPage({
    //            id : 'purOrderListView',
    //            href : 'buy/purOrderListView.aspx?id='+row.id,
    //            title : 'Purchase Order - Details'
    //          });
}

function reload() {
    manager.reload();
}



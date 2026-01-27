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
            {
                display: 'Product Code', name: 'code', width: 170, align: 'center',
                totalSummary:
                {
                    type: 'count',
                    render: function (e) { //Summary renderer, return HTML to load into cell
                        //e Summary Object (including sum, max, min, avg, count)
                        return 'Total:';
                    }
                }
            },
            { display: 'Product Name', name: 'goodsName', width: 220, align: 'left' },
            { display: 'Specification', name: 'spec', width: 120, align: 'center' },
            { display: 'Unit', name: 'unitName', width: 100, align: 'center' },
            { display: 'Warehouse', name: 'ckName', width: 100, align: 'center' },
            {
                display: 'Quantity', name: 'sumNum', width: 100, align: 'right',
                totalSummary:
                {
                    align: 'right',   //Summary cell content alignment:left/center/right 
                    type: 'sum',
                    render: function (e) { //Summary renderer, return HTML to load into cell
                        //e Summary Object (including sum, max, min, avg, count)
                        return Math.round(e.sum * 100) / 100;
                    }
                }
            },
            {
                display: 'Average cost unit price', name: 'sumPriceAll', width: 160, align: 'right',
                render: function (row) {

                    var price = Number(row.sumPriceAll) / Number(row.sumNum);
                    price = Math.round(price * 100) / 100;

                    return price;
                }
            },
            {
                display: 'Cost Amount', name: 'sumPriceAll', width: 180, type: 'float', align: 'right', editor: { type: 'float' },
                totalSummary:
                {
                    align: 'center', //Alignment of summary cell contents: left/center/right
                    type: 'sum',
                    render: function (e) { //Summary renderer, return HTML to load into cell
                        var itemSumPriceNow = e.sum;
                        return "<span id='sumPriceItemNow'>" + Math.round(itemSumPriceNow * 10000) / 10000 + "</span>";//formatCurrency(suminf.sum)
                    }
                }

            }
        ], width: '98%',
        //pageSizeOptions: [5, 10, 15, 20],
        height: '98%',
        // pageSize: 15,
        dataAction: 'local', //local sorting
        usePager: false,
        rownumbers: true, //display serial number
        alternatingRow: false,
        onDblClickRow: function (data, rowindex, rowobj) {
            // $.ligerDialog.alert('The selected one is ' + data.id);
            viewRow();
        }
    }
    );
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

    //            alert(typeId);
    //            alert(wlId);
    //            alert(goodsList);

    manager._setUrl("ProduceListSumGoodsReport.aspx?Action=GetDataList&start=" + start + "&end=" + end + "&wlId=" + wlId + "&goodsId=" + goodsList + "&typeId=" + typeId);
}

function viewRow() {
    var row = manager.getSelectedRow();

    //          top.topManager.openPage({
    //            id : 'purOrderListView',
    //            href : 'buy/purOrderListView.aspx?id='+row.id,
    //            title : 'Purchase Order-Details'
    //          });

}

function reload() {
    manager.reload();
}

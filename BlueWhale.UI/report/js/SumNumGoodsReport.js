
var manager;
$(function () {

    var form = $("#form").ligerForm();

    var txtGoodsList = $.ligerui.get("txtGoodsList");
    txtGoodsList.set("Width", 360);

    var dateEnd = $.ligerui.get("txtDateEnd");
    dateEnd.set("Width", 160);

    var txtFlagList = $.ligerui.get("txtFlagList");
    txtFlagList.set("Width", 160);

    //初始化
    manager = $("#maingrid").ligerGrid({

        columns: [
            { display: 'Inventory Code', name: 'code', width: 120, align: 'center' },
            {
                display: 'Inventory Name', name: 'goodsName', width: 220, align: 'left',

                totalSummary:
                {
                    type: 'count',
                    render: function (e) { //Summary renderer, return HTML to load into cell
                    //e Summary Object (including sum, max, min, avg, count)
                        return 'Total:';
                    }
                }
            },
            { display: 'Specification', name: 'spec', width: 120, align: 'center' },
            { display: 'Unit', name: 'unitName', width: 60, align: 'center' },

            { display: 'Field A', name: 'fieldA', width: 100, align: 'center' },
            { display: 'Field B', name: 'fieldB', width: 100, align: 'center' },
            { display: 'Field C', name: 'fieldC', width: 100, align: 'center' },
            { display: 'Field D', name: 'fieldD', width: 100, align: 'center' },

            { display: 'Warehouse', name: 'ckName', width: 100, align: 'center' },
            { display: 'Inventory Price', name: 'priceCost', width: 100, align: 'center' },
            {
                display: 'Quantity', name: 'sumNum', width: 100, align: 'right',
                totalSummary:
                {
                    align: 'right', //Alignment of summary cell contents: left/center/right
                    type: 'sum',
                    render: function (e) { //Summary renderer, return HTML to load into cell
                        //e Summary Object (including sum, max, min, avg, count)
                        return Math.round(e.sum * 100) / 100;
                    }
                }
            },
            {
                display: 'Inventory Cost Amount', name: 'sumPriceStore', width: 160, align: 'right',
                totalSummary:
                {
                    align: 'right', //Alignment of summary cell contents: left/center/right
                    type: 'sum',
                    render: function (e) { //Summary renderer, return HTML to load into cell
                        //e Summary Object (including sum, max, min, avg, count)
                        return Math.round(e.sum * 100) / 100;
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
        alternatingRow: false
    }
    );

    //初始化
    f_changeHeaderText();
});

function search(down) {

    var end = $("#txtDateEnd").val();
    var goodsList = $("#txtGoodsList").val();
    var typeId = $("#txtFlagList").val();

    var goodsIdString = goodsList.split(";");
    var typeIdString = typeId.split(";");

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

    var path = new Date().getTime();

    var url = "SumNumGoodsReport.aspx?Action=GetDataList&end=" + end + "&goodsId=" + goodsList + "&typeId=" + typeId + "&down=" + down + "&path=" + path;

    manager._setUrl(url);

    if (down == 1) {
        window.location.href = url;
    } else {
        manager._setUrl(url);
    }
}

function f_changeHeaderText() {

    var hfFieldA = $("#hfFieldA").val();
    var hfFieldB = $("#hfFieldB").val();
    var hfFieldC = $("#hfFieldC").val();
    var hfFieldD = $("#hfFieldD").val();

    manager.changeHeaderText('fieldA', hfFieldA);
    manager.changeHeaderText('fieldB', hfFieldB);
    manager.changeHeaderText('fieldC', hfFieldC);
    manager.changeHeaderText('fieldD', hfFieldD);
}

function viewRow() {
    var row = manager.getSelectedRow();

    //          top.topManager.openPage({
    //            id : 'purOrderListView',
    //            href : 'buy/purOrderListView.aspx?id='+row.id,
    //            title: 'Sales Order-Details'
    //          });
}

function reload() {
    manager.reload();
}
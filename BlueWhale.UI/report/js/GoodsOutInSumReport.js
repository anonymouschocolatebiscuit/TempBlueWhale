var manager;
$(function () {
    var form = $("#form").ligerForm();
    var dateStart = $.ligerui.get("txtDateStart");
    dateStart.set("Width", 110);
    var dateEnd = $.ligerui.get("txtDateEnd");
    dateEnd.set("Width", 110);
    var txtFlagList = $.ligerui.get("txtFlagList"); txtGoodsList
    txtFlagList.set("Width", 100);
    var txtGoodsList = $.ligerui.get("txtGoodsList");
    txtGoodsList.set("Width", 250);

    manager = $("#maingrid").ligerGrid({
        columns: [
            { display: 'Goods Code', name: 'code', width: 70, align: 'center', frozen: true },
            { display: 'Goods Name', name: 'goodsName', width: 160, align: 'center', frozen: true },
            { display: 'Specific', name: 'spec', width: 80, align: 'center', frozen: true },
            { display: 'Unit Name', name: 'unitName', width: 50, align: 'center', frozen: true },
            { display: 'Store Name', name: 'storeName', width: 70, align: 'center' },

            /
            {
                display: 'Begin Period', columns:
                    [
                        { display: 'Quantity', name: 'sumNumBegin', width: 70, align: 'right' },
                        { display: 'Cost', name: 'sumPriceBegin', width: 70, align: 'right' }
                    ]
            }
            ,/

            /
            {
                display: 'Current Period Revenue', columns:
                    [
                        { display: 'Quantity', name: 'sumNumIn', width: 70, align: 'right' },
                        { display: 'Cost', name: 'sumPriceIn', width: 70, align: 'right' }
                    ]
            },/
            {
                display: 'Year-to-Date Revenue', columns:
                    [
                        { display: 'Quantity', name: 'sumNumInAll', width: 70, align: 'right' },
                        { display: 'Cost', name: 'sumPriceInAll', width: 70, align: 'right' }
                    ]
            },/
            {
                display: 'Current Period Sale', columns:
                    [

                        { display: 'Quantity', name: 'sumNumOut', width: 70, align: 'right' },
                        { display: 'Cost', name: 'sumPriceOut', width: 70, align: 'right' }
                    ]
            },
            {
                display: 'ear-to-Date Sales', columns:
                    [

                        { display: 'Quantity', name: 'sumNumOutAll', width: 70, align: 'right' },
                        { display: 'Cost', name: 'sumPriceOutAll', width: 70, align: 'right' }
                    ]
            },
            {
                display: 'Current Period Balance', columns:
                    [

                        { display: 'Quantity', name: 'sumNumEnd', width: 70, align: 'right' },
                        { display: 'Cost', name: 'sumPriceEnd', width: 70, align: 'right' }
                    ]
            }
        ], width: '98%',
        //pageSizeOptions: [5, 10, 15, 20],
        height: '98%',
        // pageSize: 15,
        dataAction: 'local', //
        usePager: false,
        rownumbers: true,//
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
    manager._setUrl("GoodsOutInSumReport.aspx?Action=GetDataList&start=" + start + "&end=" + end + "&goodsId=" + goodsList + "&ckId=" + typeId);
}

function viewRow() {
    var row = manager.getSelectedRow();
}

function reload() {
    manager.reload();
}



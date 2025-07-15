var manager;
$(function () {
    var form = $("#form").ligerForm();

    var dateStart = $.ligerui.get("txtDateStart");
    dateStart.set("Width", 110);

    var dateEnd = $.ligerui.get("txtDateEnd");
    dateEnd.set("Width", 110);

    var txtVenderList = $.ligerui.get("txtVenderList");
    txtVenderList.set("Width", 200);

    var txtGoodsList = $.ligerui.get("txtGoodsList");
    txtGoodsList.set("Width", 200);

    var txtFlagList = $.ligerui.get("txtFlagList");
    txtFlagList.set("Width", 100);

    manager = $("#maingrid").ligerGrid({

        columns: [

            { display: 'Item Number', name: 'code', width: 100, align: 'center' },
            {
                display: 'Item Name', name: 'goodsName', width: 150, align: 'center',

                totalSummary:
                {
                    type: 'count',
                    render: function (e) {  
                        return 'Total: ';
                    }
                }
            },
            { display: 'Specification', name: 'spec', width: 100, align: 'center' },
            { display: 'Unit', name: 'unitName', width: 50, align: 'center' },

            { display: 'Order Date', name: 'bizDate', width: 80, align: 'center' },
            { display: 'Order Number', name: 'number', width: 150, align: 'center' },

            { display: 'Supplier', name: 'wlName', width: 170, align: 'left' },
            { display: 'Delivery Status', name: 'sendFlag', width: 100, align: 'center' },
            {
                display: 'Quantity', name: 'Num', width: 70, align: 'right',

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
                display: 'Quantity Stock In', name: 'getNum', width: 120, align: 'right',

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
                display: 'Quantity Not Stock In', name: 'getNumNo', width: 150, align: 'right',

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
                display: 'Total Price', name: 'sumPriceAll', width: 80, align: 'right',

                totalSummary:
                {
                    align: 'right',   
                    type: 'sum',
                    render: function (e) {  
                        return Math.round(e.sum * 100) / 100;
                    }
                }
            },

            { display: 'Send Date', name: 'sendDate', width: 80, align: 'center' }

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
    var code = $("#txtVenderCode").val();

    if (code != "") {
        code = "'" + code + "'";
    }

    var goodsList = $("#txtGoodsCode").val();
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

    manager._setUrl("PurOrderListReport.aspx?Action=GetDataList&start=" + start + "&end=" + end + "&wlId=" + code + "&goodsId=" + goodsList + "&typeId=" + typeId);
    var url = "PurOrderListReport.aspx?Action=GetDataList&start=" + start + "&end=" + end + "&wlId=" + code + "&goodsId=" + goodsList + "&typeId=" + typeId;
}

function viewRow() {
    var row = manager.getSelectedRow();
}

function reload() {
    manager.reload();
}

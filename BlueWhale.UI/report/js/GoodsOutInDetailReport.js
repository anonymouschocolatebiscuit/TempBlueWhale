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

            { display: 'Item Number', name: 'code', width: 120, align: 'center', frozen: true },
            {
                display: 'Item Name', name: 'goodsName', width: 120, align: 'center', frozen: true,

                totalSummary:
                {
                    type: 'count',
                    render: function (e) {  
                        return 'Total: ';
                    }
                }
            },
            { display: 'Specification', name: 'spec', width: 100, align: 'center', frozen: true },
            { display: 'Unit', name: 'unitName', width: 80, align: 'center', frozen: true },
            {
                display: 'Date', name: 'bizDate', width: 80, align: 'center', frozen: true,

                render: function (row) {
                    var html = row.bizType == "Opening balance" ? "" : row.bizDate;
                    return html;
                }
            },
            { display: 'Receipt No', name: 'number', width: 150, align: 'center', frozen: true },
            { display: 'Business Type', name: 'bizType', width: 150, align: 'center' },

            { display: 'Contact Unit', name: 'wlName', width: 170, align: 'left' },
            { display: 'Stock-in Warehouse', name: 'ckName', width: 150, align: 'center' },

            {
                display: 'Opening Stock', columns:
                    [
                        {
                            display: 'Quantity', name: 'numBegin', width: 90, align: 'right',

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
                            display: 'Unit Price', name: 'priceBegin', width: 120, align: 'right',
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
                            display: 'Cost', name: 'sumPriceBegin', width: 70, align: 'right',
                            totalSummary:
                            {
                                align: 'right',   
                                type: 'sum',
                                render: function (e) { 
                                    return Math.round(e.sum * 100) / 100;
                                }
                            }
                        }
                    ]
            },
            {
                display: 'Earning', columns:
                    [
                        {
                            display: 'Quantity', name: 'numIn', width: 90, align: 'right',
                            totalSummary:
                            {
                                align: 'right',   
                                type: 'sum',
                                render: function (e) {  
                                    return Math.round(e.sum * 100) / 100;
                                }
                            }
                        },
                        { display: 'Unit Price', name: 'priceIn', width: 90, align: 'right' },
                        { display: 'Cost', name: 'sumPriceIn', width: 70, align: 'right' }
                    ]
            },
            {
                display: 'Issued', columns:
                    [
                        { display: 'Quantity', name: 'numOut', width: 70, align: 'right' },
                        { display: 'Unit Price', name: 'priceOut', width: 90, align: 'right' },
                        { display: 'Cost', name: 'sumPriceOut', width: 70, align: 'right' }
                    ]
            },
            {
                display: 'Ending Balance', columns:
                    [
                        { display: 'Quantity', name: 'numEnd', width: 70, align: 'right' },
                        { display: 'Unit Price', name: 'priceEnd', width: 90, align: 'right' },
                        { display: 'Cost', name: 'sumPriceEnd', width: 70, align: 'right' }
                    ]
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
    manager._setUrl("GoodsOutInDetailReport.aspx?Action=GetDataList&start=" + start + "&end=" + end + "&goodsId=" + goodsList + "&ckId=" + typeId);
}

function viewRow() {
    var row = manager.getSelectedRow();
}


function reload() {
    manager.reload();
}



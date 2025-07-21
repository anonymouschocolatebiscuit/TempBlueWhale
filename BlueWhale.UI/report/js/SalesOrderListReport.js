




var manager;
$(function () {



    var form = $("#form").ligerForm();


    var menu = $.ligerMenu({
        width: 120, items:
            [
                { text: 'View Purchase Records', click: viewRow, icon: 'add' },

                { line: true },
                { text: 'View Outbound Records', click: viewRow },


            ]
    });



    var dateStart = $.ligerui.get("txtDateStart");
    dateStart.set("Width", 110);

    var dateEnd = $.ligerui.get("txtDateEnd");
    dateEnd.set("Width", 110);

    var txtFlagList = $.ligerui.get("txtFlagList");
    txtFlagList.set("Width", 100);





    manager = $("#maingrid").ligerGrid({

        columns: [


            { display: 'Item Code', name: 'code', width: '5%', align: 'center' },

            {
                display: 'Goods Name', name: 'goodsName', width: '9%', align: 'center',

                totalSummary:
                {
                    type: 'count',
                    render: function (e) {
                        return 'Total: ';
                    }
                }


            },
            { display: 'Item Barcode', name: 'barcode', width: '9%', align: 'center' },

            { display: 'Specification', name: 'spec', width: '7%', align: 'center' },
            { display: 'Unit', name: 'unitName', width: '5%', align: 'center' },

            { display: 'Order Date', name: 'bizDate', width: '6%', align: 'center' },
            { display: 'Order Number', name: 'number', width: '9%', align: 'center' },

            { display: 'Client', name: 'wlName', width: '9%', align: 'center' },



            { display: 'Delivery Status', name: 'sendFlag', width: '6%', align: 'center' },

            {
                display: 'Purchase Quantity', name: 'Num', width: '8%', align: 'right',

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
                display: 'Delivered Quantity', name: 'getNum', width: '8%', align: 'right',

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
                display: 'Undelivered Quantity', name: 'getNumNo', width: '9%', align: 'right',

                totalSummary:
                {
                    align: 'right',   
                    type: 'sum',
                    render: function (e) {  
                        return Math.round(e.sum * 100) / 100;
                    }
                }

            },




            { display: 'Delivery Date', name: 'sendDate', width: '6%', align: 'center' }



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
        },
        allowUnSelectRow: true,
        onRClickToSelect: true,
        onContextmenu: function (parm, e) {
            actionCustomerID = parm.data.id;
            menu.show({ top: e.pageY, left: e.pageX });
            return false;
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
    //            alert(typeId);
    //          
    //            alert(wlId);
    //           

    //manager.changePage("first");
    //manager._setUrl("PurOrderList.aspx?Action=GetDataListSearch&types=0&keys=" + keys + "&start=" +start + "&end=" + end);
    manager._setUrl("SalesOrderListReport.aspx?Action=GetDataList&start=" + start + "&end=" + end + "&wlId=" + wlId + "&goodsId=" + goodsList + "&typeId=" + typeId);
    //alert("SalesOrderListReport.aspx?Action=GetDataList&start=" + start + "&end=" + end + "&wlId=" + wlId + "&goodsId=" + goodsList + "&typeId=" + typeId);
    //window.location.href = "SalesOrderListReport.aspx?Action=GetDataList&start=" + start + "&end=" + end + "&wlId=" + wlId + "&goodsId=" + goodsList + "&typeId=" + typeId;


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



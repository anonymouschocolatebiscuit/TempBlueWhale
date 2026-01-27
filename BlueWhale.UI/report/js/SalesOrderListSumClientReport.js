var manager;

$(function () {
    var form = $("#form").ligerForm();

    var dateStart =  $.ligerui.get("txtDateStart");
    dateStart.set("Width", 110);
         
    var  dateEnd=  $.ligerui.get("txtDateEnd");
    dateEnd.set("Width", 110);
         
    var  txtFlagList=  $.ligerui.get("txtFlagList");
    txtFlagList.set("Width", 100);
                     
    manager = $("#maingrid").ligerGrid({   
        columns: [
            { display: 'Customer', name: 'wlName', width: 170, align: 'left', 
                totalSummary:
                {
                    type: 'count', render: function (e) {
                        // Summary renderer, returns HTML to be loaded into the cell
                        // Summary Object (including sum, max, min, avg, count)
                        return 'Total：';
                    }
                }
            },
            { display: 'Item Number', name: 'code', width: 120, align: 'center'},
            { display: 'Item Name', name: 'goodsName', width: 220, align: 'left'},
            { display: 'Specification', name: 'spec', width: 100, align: 'center'},
            { display: 'Unit', name: 'unitName', width: 100, align: 'center'},
            { display: 'Warehouse', name: 'ckName', width: 100, align: 'center'},
            {
                display: 'Average unit price', name: 'sumPriceAll', width: 170, align: 'right', render: function (row) {  
                    var price = Number(row.sumPriceAll) / Number(row.sumNum);
                    price=Math.round(price*100)/100;
                    
                    return price;
                }      
            },
            {
                display: 'Quantity', name: 'sumNum', width: 100, align: 'right',
                totalSummary:
                {
                    align: 'right', // Alignment of summary cell content: left/center/right
                    type: 'sum', render: function (e) {
                        // Summary renderer, returns HTML to be loaded into the cell
                        // Summary Object (including sum, max, min, avg, count)
                        return Math.round(e.sum * 100) / 100;
                    }
                }
            },
            {
                display: 'Amount', name: 'sumPriceNow', width: 80, type: 'float', align: 'right', editor: { type: 'float' },    
                totalSummary:
                {
                    align: 'center', // Alignment of summary cell content: left/center/right
                    type: 'sum', render: function (e) {
                        // Summary renderer, returns HTML to be loaded into the cell
                        var itemSumPriceNow = e.sum;
                        return "<span id='sumPriceItemNow'>" + Math.round(itemSumPriceNow * 10000) / 10000 + "</span>";//formatCurrency(suminf.sum)
                    }
                }
            },
            {
                display: 'Discount amount', name: 'sumPriceDis', width: 170, type: 'float', align: 'right', editor: { type: 'float' },
                totalSummary:
                {
                    align: 'center', // Alignment of summary cell content: left/center/right
                    type: 'sum', render: function (e) {
                        // Summary renderer, returns HTML to be loaded into the cell
                        var itemSumPriceDis = e.sum;
                        return "<span id='sumPriceItemDis'>" + Math.round(itemSumPriceDis * 10000) / 10000 + "</span>";//formatCurrency(suminf.sum)
                    }
                }
            },
            {
                display: 'Tax Amount', name: 'sumPriceTax', width: 80, type: 'float', align: 'right',
                totalSummary:
                {
                    align: 'center', // Alignment of summary cell content: left/center/right
                    type: 'sum',
                    render: function (e) {
                        // Summary renderer, returns HTML to be loaded into the cell
                        // Summary Object (including sum, max, min, avg, count)
                        var itemSumPriceTax = e.sum;
                        return "<span id='sumPriceItemTax'>" + Math.round(itemSumPriceTax * 10000) / 10000 + "</span>"; //formatCurrency(suminf.sum)
                    }
                }
            },
            {
                display: 'Total Price Including Tax', name: 'sumPriceAll', width: 170, align: 'right',
                totalSummary:
                {
                    align: 'right', // Alignment of summary cell content: left/center/right
                    type: 'sum', render: function (e) {
                    // Summary renderer, returns HTML to be loaded into the cell
                    // Summary Object (including sum, max, min, avg, count)
                    return Math.round(e.sum * 100) / 100;
                    }
                }
            }
        ],
        width: '98%', 
        //pageSizeOptions: [5, 10, 15, 20],
        height:'98%',
        //pageSize: 15,
        dataAction: 'local', //Local sorting
        usePager: false,
        rownumbers: true, //Display serial number
        alternatingRow: false,
        onDblClickRow: function(data, rowindex, rowobj) {
            // $.ligerDialog.alert('The selected one is' + data.id);
            viewRow();
        }      
    });
});

function search() {
    var start = $("#txtDateStart").val();
    var end = $("#txtDateEnd").val();
            
    var wlId = $("#txtVenderCode").val();
    var goodsList = $("#txtGoodsList").val();
    var typeId = $("#txtFlagList").val();
            
    var wlIdString=wlId.split(";");
    var goodsIdString=goodsList.split(";");
    var typeIdString=typeId.split(";");
            
    if (wlIdString != "")
    {
        wlId = "";
        for (var i = 0; i < wlIdString.length; i++)
        {
            wlId += "'" + wlIdString[i] + "'" + ",";
        } 
        wlId = wlId.substring(0, wlId.length - 1);
    }
            
    if (goodsIdString != "")
    {
        goodsList="";
        for (var i = 0; i < goodsIdString.length; i++)
        {
            goodsList+="'"+goodsIdString[i]+"'"+",";
        } 
        goodsList=goodsList.substring(0,goodsList.length-1);     
    }
            
    if (typeIdString != "")
    {
        typeId = "";
        for (var i = 0;i < typeIdString.length; i++)
        {
            typeId += "'" + typeIdString[i] + "'" + ",";
        } 
        typeId = typeId.substring(0, typeId.length - 1);       
    }

    manager._setUrl("SalesOrderListSumClientReport.aspx?Action=GetDataList&start=" + start + "&end=" + end + "&wlId=" + wlId + "&goodsId=" + goodsList + "&typeId=" + typeId);
}

function viewRow()
{
    var row = manager.getSelectedRow();
          
    //top.topManager.openPage({
    //    id : 'purOrderListView',
    //    href : 'buy/purOrderListView.aspx?id=' + row.id,
    //    title : 'Sales Order - Details'
    //});
}

function reload() {
    manager.reload();
}    
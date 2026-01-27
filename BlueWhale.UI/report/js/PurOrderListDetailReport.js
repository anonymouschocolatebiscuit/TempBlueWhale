     
       
      
       
       
        var manager;
        $(function() {
        
        
        
        
        var form = $("#form").ligerForm();

        
         var dateStart =  $.ligerui.get("txtDateStart");
         dateStart.set("Width", 110);
         
         var  dateEnd=  $.ligerui.get("txtDateEnd");
         dateEnd.set("Width", 110);
         
         var  txtFlagList=  $.ligerui.get("txtFlagList");
         txtFlagList.set("Width", 100);
         
            
            manager = $("#maingrid").ligerGrid({
            
                columns: [


                 { display: 'Purchase Date', name: 'bizDate', width: 110, align: 'center',valign:'center'},
                 { display: 'Receipt No', name: 'number', width: 100, align: 'center',
                 
                     totalSummary:
                    {
                        type: 'count',
                        render: function (e) 
                        {  // Summary renderer, returns HTML to be loaded into the cell
                           // Aggregation of Objects (including sum, max, min, avg, count)
                            return 'Total：';
                        }
                    }
                 
                 
                 
                 },
                 { display: 'Business Type', name: 'types', width: 110, align: 'center' ,
                 
                     render: function (row) {  
                      var html = row.types == 1 ? "Purchase" : "<span style='color:green'>Return of Goods</span>";  
                      return html;
                     }   
                 
                 
                 },
                  { display: 'Vender', name: 'wlName', width: 70, align: 'left'},
                  { display: 'Item Number', name: 'code', width: 100, align: 'center'},
                 { display: 'Item Name', name: 'goodsName', width: 100, align: 'left'},
                 { display: 'Specification', name: 'spec', width: 80, align: 'center'},
                 { display: 'Unit', name: 'unitName', width: 40, align: 'center'},
              
                 { display: 'Inventory', name: 'ckName', width: 80, align: 'center'},
               
                
                 { display: 'Amount', name: 'num', width: 70, align: 'right',
                 
                    totalSummary:
                    {
                        align: 'right',   // Summary cell content alignment: left/center/right
                        type: 'sum',
                        render: function (e) 
                        {  // Summary renderer, returns HTML to be loaded into the cell
                           // Aggregation of Objects (including sum, max, min, avg, count)
                            return  Math.round(e.sum*100)/100;
                        }
                    }
                 
                 },
            
                 {
                     display: 'Original Price', name: 'price', width: 100, type: 'float', align: 'right', editor: { type: 'float' }

                 },

         {
             display: 'Discount%', name: 'dis', width: 60, type: 'float', align: 'right', editor: { type: 'float' }

         },

          {
              display: 'Discount Amount', name: 'sumPriceDis', width: 110, type: 'float', align: 'right', editor: { type: 'float' },
              totalSummary:
              {
                  align: 'center',   // Summary cell content alignment: left/center/right
                  type: 'sum',
                  render: function (e) {  // Summary renderer, returns HTML to be loaded into the cell

                      var itemSumPriceDis = e.sum;
                      return "<span id='sumPriceItemDis'>" + Math.round(itemSumPriceDis * 10000) / 10000 + "</span>";//formatCurrency(suminf.sum)
                  }
              }

          },

        {
            display: 'Original Price', name: 'priceNow', width: 100, type: 'float', align: 'right', editor: { type: 'float' }

        },

        {
            display: 'Amount', name: 'sumPriceNow', width: 80, type: 'float', align: 'right', editor: { type: 'float' },




            totalSummary:
             {
                 align: 'center',   // Summary cell content alignment: left/center/right
                 type: 'sum',
                 render: function (e) {  // Summary renderer, returns HTML to be loaded into the cell

                     var itemSumPriceNow = e.sum;
                     return "<span id='sumPriceItemNow'>" + Math.round(itemSumPriceNow * 10000) / 10000 + "</span>";//formatCurrency(suminf.sum)
                 }
             }

        },

        { display: 'Tax Rate%', name: 'tax', width: 60, type: 'int', align: 'center', editor: { type: 'int' } },
        { display: 'Unit Price with Tax', name: 'priceTax', width: 130, type: 'float', align: 'center', editor: { type: 'float' } },

        {
            display: 'Tax Amount', name: 'sumPriceTax', width: 80, type: 'float', align: 'right',

            totalSummary:
             {
                 align: 'center',   // Summary cell content alignment: left/center/right
                 type: 'sum',
                 render: function (e) {  // Summary renderer, returns HTML to be loaded into the cell
                     // Aggregation of Objects (including sum, max, min, avg, count)

                     var itemSumPriceTax = e.sum;
                     return "<span id='sumPriceItemTax'>" + Math.round(itemSumPriceTax * 10000) / 10000 + "</span>";//formatCurrency(suminf.sum)

                 }
             }



        },
                {
                    display: 'Total Price with Tax', name: 'sumPriceAll', width: 150, type: 'float', align: 'right', editor: { type: 'float' },
                    totalSummary:
                     {
                         align: 'center',   // Summary cell content alignment: left/center/right
                         type: 'sum',
                         render: function (e) {  // Summary renderer, returns HTML to be loaded into the cell
                             // Aggregation of Objects (including sum, max, min, avg, count)

                             var itemSumPriceAll = e.sum;
                             return "<span id='sumPriceItemAll'>" + Math.round(itemSumPriceAll * 10000) / 10000 + "</span>";//formatCurrency(suminf.sum)

                         }
                     }

                }
                 
               
           
                
            
                ], width: '98%', 
                  //pageSizeOptions: [5, 10, 15, 20],
                  height:'98%',
                 // pageSize: 15,
                dataAction: 'local', //Local sorting
                usePager: false,
               rownumbers:true,//Display serial number
                alternatingRow: false,
                onDblClickRow: function(data, rowindex, rowobj) {
                    // $.ligerDialog.alert('The selected is' + data.id);
                     viewRow();
                }
                
               

                
                
            }
            );

        });
        
        
       

        


        function search(down) {

          
            var start = $("#txtDateStart").val();
            var end = $("#txtDateEnd").val();
            
            var wlId = $("#txtVenderCode").val();


            var goodsList = $("#txtGoodsList").val();
            var typeId = $("#txtFlagList").val();
            
            var wlIdString=wlId.split(";");
            var goodsIdString=goodsList.split(";");
            var typeIdString=typeId.split(";");
            
            if(wlIdString!="")
            {
                wlId="";
                for(var i=0;i<wlIdString.length;i++)
                {
                   wlId+="'"+wlIdString[i]+"'"+",";
                } 
                wlId=wlId.substring(0,wlId.length-1);
                  
            }
            
             if(goodsIdString!="")
            {
                goodsList="";
                for(var i=0;i<goodsIdString.length;i++)
                {
                   goodsList+="'"+goodsIdString[i]+"'"+",";
                } 
                goodsList=goodsList.substring(0,goodsList.length-1);
                  
            }
            
             if(typeIdString!="")
            {
                typeId="";
                for(var i=0;i<typeIdString.length;i++)
                {
                   typeId+="'"+typeIdString[i]+"'"+",";
                } 
                typeId=typeId.substring(0,typeId.length-1);
                  
            }
          
//            alert(typeId);
//            alert(wlId);
//            alert(goodsList);

             var path = new Date().getTime();

             manager._setUrl("PurOrderListDetailReport.aspx?Action=GetDataList&start=" + start + "&end=" + end + "&wlId=" + wlId + "&goodsId=" + goodsList + "&typeId=" + typeId + "&down=" + down + "&path=" + path);

             
             if (down == 1) {


                setTimeout(function () {

                    window.open("../excel/ProductPurchaseOrderDetails" + path + ".xls");

                   // window.open("PurOrderListDetailReport.aspx?Action=GetDataList&start=" + start + "&end=" + end + "&wlId=" + wlId + "&goodsId=" + goodsList + "&typeId=" + typeId + "&down=" + down + "&path=" + path);


                }, 3000);


            }



        }


   
      function viewRow()
      {
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


    
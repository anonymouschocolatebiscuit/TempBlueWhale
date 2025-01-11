     
       
      
       
       
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

                   { display: '客户', name: 'wlName', width: 170, align: 'left',
                   
                       totalSummary:
                    {
                        type: 'count',
                        render: function (e) 
                        {  //汇总渲染器，返回html加载到单元格
                         //e 汇总Object(包括sum,max,min,avg,count) 
                            return '合计：';
                        }
                    }
                   
                   
                   },
              
                  { display: '商品编号', name: 'code', width: 120, align: 'center'},
                 { display: '商品名称', name: 'goodsName', width: 220, align: 'left'},
                 { display: '规格', name: 'spec', width: 100, align: 'center'},
                 { display: '单位', name: 'unitName', width: 100, align: 'center'},
              
                 { display: '仓库', name: 'ckName', width: 100, align: 'center'},
               
                  {
                      display: '平均单价', name: 'sumPriceAll', width: 100, align: 'right',
                  
                  
                       render: function (row) {  
                       
                           var price = Number(row.sumPriceAll) / Number(row.sumNum);
                       price=Math.round(price*100)/100;
                    
                      return price;
                     }   
                  
                  
                  },
                  {
                      display: '数量', name: 'sumNum', width: 100, align: 'right',

                      totalSummary:
                      {
                          align: 'right',   //汇总单元格内容对齐方式:left/center/right 
                          type: 'sum',
                          render: function (e) {  //汇总渲染器，返回html加载到单元格
                              //e 汇总Object(包括sum,max,min,avg,count) 
                              return Math.round(e.sum * 100) / 100;
                          }
                      }

                  },
                  {
                      display: '金额', name: 'sumPriceNow', width: 80, type: 'float', align: 'right', editor: { type: 'float' },




                      totalSummary:
                       {
                           align: 'center',   //汇总单元格内容对齐方式:left/center/right 
                           type: 'sum',
                           render: function (e) {  //汇总渲染器，返回html加载到单元格

                               var itemSumPriceNow = e.sum;
                               return "<span id='sumPriceItemNow'>" + Math.round(itemSumPriceNow * 10000) / 10000 + "</span>";//formatCurrency(suminf.sum)
                           }
                       }

                  },

                  {
                      display: '折扣金额', name: 'sumPriceDis', width: 70, type: 'float', align: 'right', editor: { type: 'float' },
                      totalSummary:
                      {
                          align: 'center',   //汇总单元格内容对齐方式:left/center/right 
                          type: 'sum',
                          render: function (e) {  //汇总渲染器，返回html加载到单元格

                              var itemSumPriceDis = e.sum;
                              return "<span id='sumPriceItemDis'>" + Math.round(itemSumPriceDis * 10000) / 10000 + "</span>";//formatCurrency(suminf.sum)
                          }
                      }

                  },
            {
                display: '税额', name: 'sumPriceTax', width: 80, type: 'float', align: 'right',

                totalSummary:
                 {
                     align: 'center',   //汇总单元格内容对齐方式:left/center/right 
                     type: 'sum',
                     render: function (e) {  //汇总渲染器，返回html加载到单元格
                         //e 汇总Object(包括sum,max,min,avg,count) 

                         var itemSumPriceTax = e.sum;
                         return "<span id='sumPriceItemTax'>" + Math.round(itemSumPriceTax * 10000) / 10000 + "</span>";//formatCurrency(suminf.sum)

                     }
                 }



            },
                 {
                     display: '价税合计', name: 'sumPriceAll', width: 120, align: 'right',

                     totalSummary:
                    {
                        align: 'right',   //汇总单元格内容对齐方式:left/center/right 
                        type: 'sum',
                        render: function (e) {  //汇总渲染器，返回html加载到单元格
                            //e 汇总Object(包括sum,max,min,avg,count) 
                            return Math.round(e.sum * 100) / 100;
                        }
                    }

                 }
               
           
                
            
                ], width: '98%', 
                  //pageSizeOptions: [5, 10, 15, 20],
                  height:'98%',
                 // pageSize: 15,
                dataAction: 'local', //本地排序
                usePager: false,
               rownumbers:true,//显示序号
                alternatingRow: false,
                onDblClickRow: function(data, rowindex, rowobj) {
                    // $.ligerDialog.alert('选择的是' + data.id);
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


            manager._setUrl("SalesOrderListSumClientReport.aspx?Action=GetDataList&start="+start+"&end="+end+"&wlId="+wlId+"&goodsId="+goodsList+"&typeId="+typeId);
        }


   
      function viewRow()
      {
          var row = manager.getSelectedRow();
          
//          top.topManager.openPage({
//            id : 'purOrderListView',
//            href : 'buy/purOrderListView.aspx?id='+row.id,
//            title : '销售订单-详情'
//          });
  
  
      }
        
        
        function reload() {
            manager.reload();
        }


    
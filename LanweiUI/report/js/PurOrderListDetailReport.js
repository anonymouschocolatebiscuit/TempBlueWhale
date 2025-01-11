     
       
      
       
       
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


                 { display: '采购日期', name: 'bizDate', width: 80, align: 'center',valign:'center'},
                 { display: '单据编号', name: 'number', width: 150, align: 'center',
                 
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
                 { display: '业务类别', name: 'types', width: 80, align: 'center' ,
                 
                     render: function (row) {  
                      var html = row.types == 1 ? "购货" : "<span style='color:green'>退货</span>";  
                      return html;
                     }   
                 
                 
                 },
                  { display: '供应商', name: 'wlName', width: 170, align: 'left'},
                  { display: '商品编号', name: 'code', width: 70, align: 'center'},
                 { display: '商品名称', name: 'goodsName', width: 120, align: 'left'},
                 { display: '规格', name: 'spec', width: 80, align: 'center'},
                 { display: '单位', name: 'unitName', width: 50, align: 'center'},
              
                 { display: '仓库', name: 'ckName', width: 80, align: 'center'},
               
                
                 { display: '数量', name: 'num', width: 70, align: 'right',
                 
                    totalSummary:
                    {
                        align: 'right',   //汇总单元格内容对齐方式:left/center/right 
                        type: 'sum',
                        render: function (e) 
                        {  //汇总渲染器，返回html加载到单元格
                         //e 汇总Object(包括sum,max,min,avg,count) 
                            return  Math.round(e.sum*100)/100;
                        }
                    }
                 
                 },
            
                 {
                     display: '原价', name: 'price', width: 70, type: 'float', align: 'right', editor: { type: 'float' }

                 },

         {
             display: '折扣%', name: 'dis', width: 60, type: 'float', align: 'right', editor: { type: 'float' }

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
            display: '现价', name: 'priceNow', width: 70, type: 'float', align: 'right', editor: { type: 'float' }

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

        { display: '税率%', name: 'tax', width: 60, type: 'int', align: 'center', editor: { type: 'int' } },
        { display: '含税单价', name: 'priceTax', width: 70, type: 'float', align: 'center', editor: { type: 'float' } },

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
                    display: '价税合计', name: 'sumPriceAll', width: 80, type: 'float', align: 'right', editor: { type: 'float' },
                    totalSummary:
                     {
                         align: 'center',   //汇总单元格内容对齐方式:left/center/right 
                         type: 'sum',
                         render: function (e) {  //汇总渲染器，返回html加载到单元格
                             //e 汇总Object(包括sum,max,min,avg,count) 

                             var itemSumPriceAll = e.sum;
                             return "<span id='sumPriceItemAll'>" + Math.round(itemSumPriceAll * 10000) / 10000 + "</span>";//formatCurrency(suminf.sum)

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

                    window.open("../excel/商品采购明细表" + path + ".xls");

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
//            title : '采购订单-详情'
//          });
  
  
      }
        
        
        function reload() {
            manager.reload();
        }


    
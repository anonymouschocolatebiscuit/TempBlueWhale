     
       
      
       
       
        var manager;
        $(function() {
        
        
        
        
        var form = $("#form").ligerForm();

        
        var txtGoodsList = $.ligerui.get("txtGoodsList");
        txtGoodsList.set("Width", 360);
         
         var  dateEnd=  $.ligerui.get("txtDateEnd");
         dateEnd.set("Width", 160);
         
         var  txtFlagList=  $.ligerui.get("txtFlagList");
         txtFlagList.set("Width", 160);

         //初始化
        
            
            manager = $("#maingrid").ligerGrid({
            
                columns: [


               
                  { display: '商品编号', name: 'code', width: 120, align: 'center'},
                 { display: '商品名称', name: 'goodsName', width: 220, align: 'left',
                 
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
                 { display: '规格', name: 'spec', width: 120, align: 'center'},
                 { display: '单位', name: 'unitName', width: 60, align: 'center'},
              
                 { display: 'fieldA', name: 'fieldA', width: 70, align: 'center' },
                 { display: 'fieldB', name: 'fieldB', width: 70, align: 'center' },
                 { display: 'fieldC', name: 'fieldC', width: 70, align: 'center' },
                 { display: 'fieldD', name: 'fieldD', width: 70, align: 'center' },

                 { display: '仓库', name: 'ckName', width: 100, align: 'center'},
               

                 { display: '库存单价', name: 'priceCost', width: 100, align: 'center' },



                  
                 { display: '数量', name: 'sumNum', width: 100, align: 'right',
                 
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
                 
                 }

                 ,
            
                 {
                     display: '库存成本金额', name: 'sumPriceStore', width: 160, align: 'right',
                 
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
                 
                 }
                 
               
           
                
            
                ], width: '98%', 
                  //pageSizeOptions: [5, 10, 15, 20],
                  height:'98%',
                 // pageSize: 15,
                dataAction: 'local', //本地排序
                usePager: false,
               rownumbers:true,//显示序号
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
            
          
            var goodsIdString=goodsList.split(";");
            var typeIdString=typeId.split(";");
         
            
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


             var path = new Date().getTime();


             var url = "SumNumGoodsReport.aspx?Action=GetDataList&end=" + end + "&goodsId=" + goodsList + "&typeId=" + typeId + "&down=" + down + "&path=" + path;

             manager._setUrl(url);

            // window.open(url);


             if (down == 1)
             {
                 setTimeout(function () {

                     window.open("../excel/商品库存余额表" + path + ".xls");

                 }, 3000);

                
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


    
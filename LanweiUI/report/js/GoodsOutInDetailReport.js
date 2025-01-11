     
       
      
       
       
        var manager;
        $(function() {
        
        
        
        
        var form = $("#form").ligerForm();

        
         var dateStart =  $.ligerui.get("txtDateStart");
         dateStart.set("Width", 110);
         
         var  dateEnd=  $.ligerui.get("txtDateEnd");
         dateEnd.set("Width", 110);
         
         var  txtFlagList=  $.ligerui.get("txtFlagList");txtGoodsList
         txtFlagList.set("Width", 100);
         
         var  txtGoodsList=  $.ligerui.get("txtGoodsList");
         txtGoodsList.set("Width", 250);
         
            
            manager = $("#maingrid").ligerGrid({
            
                columns: [


                 { display: '商品编号', name: 'code', width: 70, align: 'center', frozen: true},
                 { display: '商品名称', name: 'goodsName', width: 160, align: 'center', frozen: true,
                 
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
                 { display: '规格', name: 'spec', width: 80, align: 'center', frozen: true},
                 { display: '单位', name: 'unitName', width: 50, align: 'center', frozen: true},
              
                 { display: '日期', name: 'bizDate', width: 80, align: 'center', frozen: true,
                 
                     render: function (row) {  
                      var html = row.bizType == "期初余额" ? "":row.bizDate;  
                      return html;
                     }
                 
                 
                 },
                 { display: '单据编号', name: 'number', width: 150, align: 'center', frozen: true},
                 { display: '业务类别', name: 'bizType', width: 70, align: 'center' },
               
                 { display: '往来单位', name: 'wlName', width: 170, align: 'left'},
                 { display: '仓库', name: 'ckName', width: 70, align: 'center'},
                 
                 //期初开始
                { display: '期初', columns:
                [
                   
                       { display: '数量', name: 'numBegin', width: 70, align: 'right',
                 
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
                 { display: '单价', name: 'priceBegin', width: 70, align: 'right',
                 
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
                 { display: '成本', name: 'sumPriceBegin', width: 70, align: 'right',
                 
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
                 
                ]
                
                }
                ,//期初结束
                
                
               
                 //收入开始
                 
                 { display: '收入', columns:
                [
                 
                 { display: '数量', name: 'numIn', width: 70, align: 'right',
                 
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
                 
                 
               
                 { display: '单价', name: 'priceIn', width: 70, align: 'right' },
                 { display: '成本', name: 'sumPriceIn', width: 70, align: 'right' }
                 
                 ]
                 },//收入结束
                 
                 { display: '发出', columns:
                 [
                 
                 { display: '数量', name: 'numOut', width: 70, align: 'right' },
                 { display: '单价', name: 'priceOut', width: 70, align: 'right' },
                 { display: '成本', name: 'sumPriceOut', width:70, align: 'right' }
                 
                 ]}                 
                 ,
                 
                  { display: '结存', columns:
                 [
                 
                 { display: '数量', name: 'numEnd', width: 70, align:'right' },
                 { display: '单价', name: 'priceEnd', width: 70, align: 'right' },
                 { display: '成本', name: 'sumPriceEnd', width: 70, align: 'right' }
                 
                 ]}
                
                
            
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
//            alert(typeId);
          
//            alert(wlId);
//            alert(goodsList);

            //manager.changePage("first");
            //manager._setUrl("PurOrderList.aspx?Action=GetDataListSearch&types=0&keys=" + keys + "&start=" +start + "&end=" + end);
            manager._setUrl("GoodsOutInDetailReport.aspx?Action=GetDataList&start="+start+"&end="+end+"&goodsId="+goodsList+"&ckId="+typeId);
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


    
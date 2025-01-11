     
       
      
       
       
        var manager;
        $(function() {
        
        
        
        
        var form = $("#form").ligerForm();

        
         var dateStart =  $.ligerui.get("txtDateStart");
         dateStart.set("Width", 110);
         
         var  dateEnd=  $.ligerui.get("txtDateEnd");
         dateEnd.set("Width", 110);


         var txtVenderList = $.ligerui.get("txtVenderList");
         txtVenderList.set("Width", 250);

         var txtGoodsList = $.ligerui.get("txtGoodsList");
         txtGoodsList.set("Width", 250);
         
         var  txtFlagList=  $.ligerui.get("txtFlagList");
         txtFlagList.set("Width", 100);
         
            
            manager = $("#maingrid").ligerGrid({
            
                columns: [


                 { display: '商品编号', name: 'code', width: 60, align: 'center'},
                 { display: '商品名称', name: 'goodsName', width: 180, align: 'center',
                 
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
                 { display: '规格', name: 'spec', width: 80, align: 'center'},
                 { display: '单位', name: 'unitName', width: 50, align: 'center'},
              
                 { display: '订单日期', name: 'bizDate', width: 80, align: 'center'},
                 { display: '订单编号', name: 'number', width: 150, align: 'center' },
               
                 { display: '供应商', name: 'wlName', width: 170, align: 'left'},
                 { display: '交货状态', name: 'sendFlag', width: 60, align: 'center' },
                 { display: '数量', name: 'Num', width: 70, align: 'right',
                 
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
                 { display: '入库数量', name: 'getNum', width: 70, align: 'right',
                 
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
                 { display: '未入库数量', name: 'getNumNo', width: 80, align: 'right',
                 
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
                 
                 
                 { display: '购货金额', name: 'sumPriceAll', width: 80, align: 'right',
                 
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
                 
               
                 { display: '交货日期', name: 'sendDate', width: 80, align: 'center' }
                
                
            
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
            
       

            var code = $("#txtVenderCode").val();

            if (code != "")
            {
                code = "'" + code + "'";
            }

            var goodsList = $("#txtGoodsCode").val();

          

            var typeId = $("#txtFlagList").val();
            
           // var wlIdString = code.split(";");
            var goodsIdString=goodsList.split(";");
            var typeIdString=typeId.split(";");
            
            //if(wlIdString!="")
            //{
            //    code = "";
            //    for(var i=0;i<wlIdString.length;i++)
            //    {
            //        code += "'" + wlIdString[i] + "'" + ",";
            //    } 
            //    code = code.substring(0, code.length - 1);
                  
            //}
            
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
//          
//            alert(wlId);
  //          alert(goodsList);

            //manager.changePage("first");
            //manager._setUrl("PurOrderList.aspx?Action=GetDataListSearch&types=0&keys=" + keys + "&start=" +start + "&end=" + end);
             manager._setUrl("PurOrderListReport.aspx?Action=GetDataList&start=" + start + "&end=" + end + "&wlId=" + code + "&goodsId=" + goodsList + "&typeId=" + typeId);

             var url="PurOrderListReport.aspx?Action=GetDataList&start=" + start + "&end=" + end + "&wlId=" + code + "&goodsId=" + goodsList + "&typeId=" + typeId;
             //window.open(url);


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


    
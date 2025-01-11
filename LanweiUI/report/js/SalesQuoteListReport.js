
        var manager;
        $(function() {
        
        
        
        
        var form = $("#form").ligerForm();

        
         var dateStart =  $.ligerui.get("txtDateStart");
         dateStart.set("Width", 110);
         
         var  dateEnd=  $.ligerui.get("txtDateEnd");
         dateEnd.set("Width", 110);
         
         var  txtGoodsList=  $.ligerui.get("txtGoodsList");
         txtGoodsList.set("Width", 250);
         
            
            manager = $("#maingrid").ligerGrid({
            
                columns: [

                 { display: '报价日期', name: 'bizDate', width: 80, align: 'center'},
                 { display: '客户', name: 'wlName', width: 170, align: 'left'},
                 
                 { display: '商品编号', name: 'code', width: 60, align: 'center'},
                 { display: '商品名称', name: 'goodsName', width: 120, align: 'center',
                 
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
                 
                 
                  { display: '单位', name: 'unitName',width: 50, align: 'center' },
                
                { display: '品牌', name: 'brand', width: 60, align: 'left',type:'text'},
                { display: '最小包装', name: 'mpq', width: 60, align: 'left',type:'text'},
                
                { display: '数量', name: 'num', width: 60, type: 'float', align: 'right',
                
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
                
                { display: '成本单价', name: 'priceCost', width: 70, type: 'float', align: 'right'},
                
                 { display: '成本金额', name: 'sumPriceCost', width: 70, type: 'float', align: 'right'},

                { display: '利润%', name: 'profit', width: 60, type: 'float', align: 'center' },
                
                { display: '未税单价', name: 'price', width: 70, type: 'float', align: 'right'},
                
                { display: '未税金额', name: 'sumPrice', width: 70, type: 'float', align: 'right',
                  
                    totalSummary:
                    {
                        align: 'center',   //汇总单元格内容对齐方式:left/center/right 
                        type: 'sum',
                        render: function (e) 
                        {  //汇总渲染器，返回html加载到单元格
                         //e 汇总Object(包括sum,max,min,avg,count) 
                            return Math.round(e.sum*100)/100;
                        }
                    }
                
                
                },
                
                
            
                
                { display: '税率%', name: 'tax', width: 60, type: 'int', align: 'center'},
                { display: '含税单价', name: 'priceTax', width: 60, type: 'int', align: 'center'},
                
                { display: '税额', name: 'sumPriceTax', width: 70, type: 'float', align: 'right',
                
                  
                  
                   totalSummary:
                    {
                        align: 'center',   //汇总单元格内容对齐方式:left/center/right 
                        type: 'sum',
                        render: function (e) 
                        {  //汇总渲染器，返回html加载到单元格
                         //e 汇总Object(包括sum,max,min,avg,count) 
                            return Math.round(e.sum*100)/100;
                        }
                    }
                
                
                
                 },
                
                
                
                
                { display: '价税合计', name: 'sumPriceAll', width: 90, type: 'float', align: 'right',
                
                
                  
                
                   totalSummary:
                    {
                        align: 'center',   //汇总单元格内容对齐方式:left/center/right 
                        type: 'sum',
                        render: function (e) 
                        {  //汇总渲染器，返回html加载到单元格
                         //e 汇总Object(包括sum,max,min,avg,count) 
                            return Math.round(Math.round(e.sum*100)/100*100)/100 ;//Math.round(Math.round(e.sum*100)/100*100)/100
                            
                            
                        }
                    }
                
                },
               
                 { display: '交货期限', name: 'sendDate', width: 80, align: 'center' },
                 { display: '备注', name: 'remarksItem', width: 80, align: 'center' }
                
                
            
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
            
            var wlId = $("#txtVenderList").val();
            var goodsList = $("#txtGoodsList").val();
          
            var wlIdString=wlId.split(";");
            var goodsIdString=goodsList.split(";");
           
            
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
         
            manager._setUrl("SalesQuoteListReport.aspx?Action=GetDataList&start="+start+"&end="+end+"&wlId="+wlId+"&goodsId="+goodsList);
        }


   
      function viewRow()
      {
          var row = manager.getSelectedRow();
          
          parent.f_addTab('SalesQuoteListEdit','销售报价-详情','Sales/SalesQuoteListEdit.aspx?id='+row.id);
  
      }
        
        
        function reload() {
            manager.reload();
        }


    
     
       
      
       
       
        var manager;
        $(function() {
        
        
        
        
        var form = $("#form").ligerForm();

        
         var dateStart =  $.ligerui.get("txtDateStart");
         dateStart.set("Width", 110);
         
         var  dateEnd=  $.ligerui.get("txtDateEnd");
         dateEnd.set("Width", 110);
         
       
            
            manager = $("#maingrid").ligerGrid({
            
                columns: [

                 { display: '账户编号', name: 'code', width: 70, align: 'center'},
                 { display: '账户名称', name: 'bkName', width: 120, align: 'left',
                 
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
                 { display: '日期', name: 'bizDate', width: 80, align: 'center',
                 
                     render: function (row) {  
                      var html = row.bizType == "期初余额" ? "":row.bizDate;  
                      return html;
                     }  
                 
                 
                 },
                 { display: '单据编号', name: 'number', width: 150, align: 'center'},
                 { display: '业务类型', name: 'bizType', width: 80, align: 'center'},
                  { display: '往来单位', name: 'wlName', width: 170, align: 'left'},
                
                 {
                     display: '期初金额', name: 'priceBegin', width: 80, align: 'right'
                 
//                     render: function (row) {  
//                     
//                      var html = row.bizType != "期初余额" ? "上一行期初"+row.numbers:row.priceBegin;
//                      
//                      
//                        
//                      return html;
//                     }  
                 
                 
                 },
                
                 {
                     display: '收入', name: 'priceIn', width: 80, align: 'right',
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
                     display: '支出', name: 'priceOut', width: 80, align: 'right',
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
               
                  { display: '账户余额', name: 'priceEnd', width: 80, align: 'right',
                  
                       render: function (row) {  
                      
                      var html =Number(row.priceBegin)+Number(row.priceIn)-Number(row.priceOut);  
                      
                      if(row.bizType=="期初余额")
                      {
                          html= row.priceEnd;
                      }
                      
                      return Math.round(html * 100) / 100;
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
            
          
            var typeId = $("#txtFlagList").val();
    
            var typeIdString=typeId.split(";");
            
 
             if(typeIdString!="")
            {
                typeId="";
                for(var i=0;i<typeIdString.length;i++)
                {
                   typeId+="'"+typeIdString[i]+"'"+",";
                } 
                typeId=typeId.substring(0,typeId.length-1);
                  
            }
          
            //alert(typeId);

            

            manager._setUrl("AccountFlowReport.aspx?Action=GetDataList&start="+start+"&end="+end+"&typeId="+typeId);
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


    
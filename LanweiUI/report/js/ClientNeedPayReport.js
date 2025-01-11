     
       
      
       
       
        var manager;
        $(function() {
        
        
        
        
        var form = $("#form").ligerForm();

        
         var dateStart =  $.ligerui.get("txtDateStart");
         dateStart.set("Width", 110);
         
         var  dateEnd=  $.ligerui.get("txtDateEnd");
         dateEnd.set("Width", 110);
         
         
         var txtVenderList = $.ligerui.get("txtVenderList");
         txtVenderList.set("Width", 310);
         
       
            
            manager = $("#maingrid").ligerGrid({
            
                columns: [

                 { display: '客户', name: 'wlName', width: 200, align: 'left',
                  
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
                 { display: '单据日期', name: 'bizDate', width: 80, align: 'center',
                 
                     render: function (row) {  
                      var html = row.bizType == "期初余额" ? "":row.bizDate;  
                      return html;
                     }  
                 
                 
                 },
                 { display: '单据编号', name: 'number', width: 150, align: 'center'},
                 { display: '业务类型', name: 'bizType', width: 80, align: 'center'},
                 
                
                 { display: '增加应收款', name: 'payNeed', width: 180, align: 'right', type: 'float',
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
                     display: '增加预收款', name: 'payReady', width: 180, align: 'right', type: 'float',
                 
                       totalSummary:
                    {
                        align: 'right',   //汇总单元格内容对齐方式:left/center/right 
                        type: 'sum',
                        render: function (e) 
                        {  //汇总渲染器，返回html加载到单元格
                         //e 汇总Object(包括sum,max,min,avg,count) 
                            return Math.round(e.sum * 100) / 100; //Math.round(e.sum*100)/100;
                        }
                    } 
                 
                 
                 },
              
             
               
                  {
                      display: '应收款余额', name: 'payEnd', width: 180, align: 'right', type: 'float'

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
            
          
            var typeId = $("#txtVenderList").val();
    
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

            

            manager._setUrl("ClientNeedPayReport.aspx?Action=GetDataList&start="+start+"&end="+end+"&typeId="+typeId);
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

function openBill(number,bizType) {
         
          if(bizType=="普通采购" || bizType=="采购退货")
          {
              window.location.href="../sales/PurReceiptListView.aspx?id=0&number="+number;
          }    
          if(bizType=="付款")
          {
              window.location.href="../pay/PayMentListView.aspx?id=0&number="+number;
          }
       

      } 
  
    
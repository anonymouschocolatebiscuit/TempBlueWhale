     
       
      
       
       
        var manager;
        $(function() {
        
        
               
        var form = $("#form").ligerForm();
        
        
          var menu = $.ligerMenu({ width: 120, items:
            [
            { text: '查看采购记录', click: viewRow, icon: 'add' },
          
            { line: true },
            { text: '查看出库记录', click: viewRow },
          
            
            ]
            }); 
            

        
         var dateStart =  $.ligerui.get("txtDateStart");
         dateStart.set("Width", 110);
         
         var  dateEnd=  $.ligerui.get("txtDateEnd");
         dateEnd.set("Width", 110);
         
         var  txtFlagList=  $.ligerui.get("txtFlagList");
         txtFlagList.set("Width", 100);
         
         
          
         
            
            manager = $("#maingrid").ligerGrid({
            
                columns: [

                 
                 { display: '商品编码', name: 'code', width: 60, align: 'center'},
                 
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
                 { display: '商品条码', name: 'barcode', width: 100, align: 'center'},
                 
                 { display: '规格', name: 'spec', width: 80, align: 'center'},
                 { display: '单位', name: 'unitName', width: 50, align: 'center'},
              
                 { display: '订单日期', name: 'bizDate', width: 80, align: 'center'},
                 { display: '订单编号', name: 'number', width: 160, align: 'center' },
               
                 { display: '客户', name: 'wlName', width: 120, align: 'center'},
                
                
                 
                 { display: '交货状态', name: 'sendFlag', width: 60, align: 'center' },
                 
                 { display: '采购数量', name: 'Num', width: 60, align: 'right',
                 
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
                 { display: '已交货数量', name: 'getNum', width: 80, align: 'right',
                 
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
                 { display: '未交货数量', name: 'getNumNo', width: 80, align: 'right',
                 
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
                },
                allowUnSelectRow:true,
                 onRClickToSelect:true,
                onContextmenu : function (parm,e)
                {
                    actionCustomerID = parm.data.id;
                    menu.show({ top: e.pageY, left: e.pageX });
                    return false;
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
//          
//            alert(wlId);
//           

            //manager.changePage("first");
            //manager._setUrl("PurOrderList.aspx?Action=GetDataListSearch&types=0&keys=" + keys + "&start=" +start + "&end=" + end);
            manager._setUrl("SalesOrderListReport.aspx?Action=GetDataList&start="+start+"&end="+end+"&wlId="+wlId+"&goodsId="+goodsList+"&typeId="+typeId);
            //alert("SalesOrderListReport.aspx?Action=GetDataList&start=" + start + "&end=" + end + "&wlId=" + wlId + "&goodsId=" + goodsList + "&typeId=" + typeId);
            //window.location.href = "SalesOrderListReport.aspx?Action=GetDataList&start=" + start + "&end=" + end + "&wlId=" + wlId + "&goodsId=" + goodsList + "&typeId=" + typeId;


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


    
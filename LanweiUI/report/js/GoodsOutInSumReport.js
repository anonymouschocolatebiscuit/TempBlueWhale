     
       
      
       
       
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
                 { display: '商品名称', name: 'goodsName', width: 160, align: 'center', frozen: true},
                 { display: '规格', name: 'spec', width: 80, align: 'center', frozen: true},
                 { display: '单位', name: 'unitName', width: 50, align: 'center', frozen: true},
              
               
                 { display: '仓库', name: 'ckName', width: 70, align: 'center'},
                 
                 //期初开始
                { display: '期初', columns:
                [
                   
                     { display: '数量', name: 'sumNumBegin', width: 70, align: 'right'},
                     { display: '成本', name: 'sumPriceBegin', width: 70, align: 'right'}
                 
                ]
                }
                ,//期初结束

                 //收入开始
                 
                 { display: '本期收入', columns:
                 [
                 
                     { display: '数量', name: 'sumNumIn', width: 70, align: 'right' },
                     { display: '成本', name: 'sumPriceIn', width: 70, align: 'right' }
                 
                 ]
                 },//收入结束
                 
                  { display: '本年累计收入', columns:
                 [
                 
                     { display: '数量', name: 'sumNumInAll', width: 70, align: 'right' },
                     { display: '成本', name: 'sumPriceInAll', width: 70, align: 'right' }
                 
                 ]
                 },//收入结束
                 
                 
                 { display: '本期发出', columns:
                 [
                 
                     { display: '数量', name: 'sumNumOut', width: 70, align: 'right' },              
                     { display: '成本', name: 'sumPriceOut', width:70, align: 'right' }
                 
                 ]
                 },
                 
                  { display: '本年累计发出', columns:
                 [
                 
                     { display: '数量', name: 'sumNumOutAll', width: 70, align: 'right' },
                     { display: '成本', name: 'sumPriceOutAll', width: 70, align: 'right' }
                 
                 ]
                 },//收入结束
                 
                 
                  { display: '本期结存', columns:
                  [
                 
                     { display: '数量', name: 'sumNumEnd', width: 70, align:'right' },
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
            manager._setUrl("GoodsOutInSumReport.aspx?Action=GetDataList&start="+start+"&end="+end+"&goodsId="+goodsList+"&ckId="+typeId);
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


    
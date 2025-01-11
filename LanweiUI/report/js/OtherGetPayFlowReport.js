

            
      
      
       
       
        var manager;
        $(function() {
      
     
    

 
      var bizTypeData = 
            [
            { id: 0, text: '全部类别' }, 
            { id: 1, text: '其他收入'},
            { id: 2, text: '其他支出'}
            ];
        
        
      
         
      
  
      $("#txtItemList").ligerComboBox({ data: null, isMultiSelect: true, isShowCheckBox: true,valueFieldID: 'itemId' });

     
       
     $("#ddlTypeList").ligerComboBox({
                data: bizTypeData, isMultiSelect: false,valueFieldID: 'bizTypeId',
                onSelected: function (newvalue)
                {

                    var typeName="全部";
                    
                    
                    
                    if(newvalue=="1")
                    {
                       typeName="收入";
                        
                    }
                    
                     if(newvalue=="2")
                    {
                       typeName="支出";
                        
                    }
                    
                    liger.get("txtItemList")._setUrl("../baseSet/PayGetList.aspx?Action=GetDDLList&type="+typeName+"&r=" + Math.random());
                }
            });

      
     
    
      var form = $("#form").ligerForm();
        
        
       
        
         var dateStart =  $.ligerui.get("txtDateStart");
         dateStart.set("Width", 110);
         
         var  dateEnd=  $.ligerui.get("txtDateEnd");
         dateEnd.set("Width", 110);
         
         
       
         
        
         
         
          var  txtItemList=  $.ligerui.get("txtItemList");
         txtItemList.set("Width", 220);
       
            
            manager = $("#maingrid").ligerGrid({
            
                columns: [

                 { display: '业务日期', name: 'bizDate', width: 80, align: 'center'},
                 { display: '账户编号', name: 'code', width: 70, align: 'center'},
                 { display: '账户名称', name: 'bkName', width: 120, align: 'center' },
                 
                 
                 { display: '单据编号', name: 'number', width: 150, align: 'center'},
                 { display: '业务类型', name: 'bizType', width: 80, align: 'center'},
                 { display: '往来单位', name: 'wlName', width: 170, align: 'left'},
             
                 { display: '收支类别', name: 'typeName', width: 100, align: 'center',
                 
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
                 
                
                 { display: '收入', name: 'priceIn', width: 80, align: 'right',
                 
                     totalSummary:
                    {
                        align: 'center',   //汇总单元格内容对齐方式:left/center/right 
                        type: 'sum',
                        render: function (e) 
                        {  //汇总渲染器，返回html加载到单元格
                         //e 汇总Object(包括sum,max,min,avg,count) 
                            return e.sum.toFixed(2);//Math.round(Math.round(e.sum*100)/100*100)/100
                            
                            
                        }
                    }
                 
                 },
              
                 { display: '支出', name: 'priceOut', width: 80, align: 'right',
                 
                    totalSummary:
                    {
                        align: 'center',   //汇总单元格内容对齐方式:left/center/right 
                        type: 'sum',
                        render: function (e) 
                        {  //汇总渲染器，返回html加载到单元格
                         //e 汇总Object(包括sum,max,min,avg,count) 
                            return e.sum.toFixed(2) //Math.round(Math.round(e.sum*100)/100*100)/100 ;//Math.round(Math.round(e.sum*100)/100*100)/100
                            
                            
                        }
                    }
                 
                 },
               
               
                 { display: '经手人', name: 'bizName', width: 80, align: 'center'},
                 
                 { display: '摘要', name: 'remarks', width: 170, align: 'left'},
                 
                 { display: '备注', name: 'remarksMain', width: 170, align: 'left'},
                 
                 { display: '状态', name: 'flag', width: 70, align: 'center'}
               
           
                
            
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
            
          
            var bizType = $("#bizTypeId").val();
            
           
            if(bizType=="0" || bizType=="")
            {
               bizType="全部";
                
            }
                   
                    
            if(bizType=="1")
            {
               bizType='其他收入';
                
            }
            
             if(bizType=="2")
            {
               bizType='其他支出';
                
            }
            
           
                          
            
            var typeId = $("#itemId").val();
            
            var bizId = $("#ddlYWYList").val();
             
    
            var typeIdString=typeId.split(";");
            
 
             if(typeIdString!="")
            {
                typeId="";
                for(var i=0;i<typeIdString.length;i++)
                {
                   typeId+=typeIdString[i]+",";
                } 
                typeId=typeId.substring(0,typeId.length-1);
                  
            }
            
             var path = new Date().getTime();




             var url = "OtherGetPayFlowReport.aspx?Action=GetDataList&start=" + start
                 + "&end=" + end
                 + "&bizType=" + bizType
                 + "&typeIdString=" + typeIdString
                 + "&bizId=" + bizId
                 + "&down=" + down + "&path=" + path;
           


            
             //manager._setUrl("OtherGetPayFlowReport.aspx?Action=GetDataList&start=" + start
             //    + "&end=" + end + "&bizType=" + bizType + "&typeIdString=" + typeId + "&bizId=" + bizId);
            
             //var url = "SumNumGoodsReport.aspx?Action=GetDataList&end=" + end
             //    + "&goodsId=" + goodsList + "&typeId=" + typeId
             //    + "&down=" + down + "&path=" + path;

            manager._setUrl(url);



            if (down == 1) {

                setTimeout(function () {

                    window.open("../excel/其他收支明细报表" + path + ".xls");

                }, 3000);


            }



        }


   
      function viewRow()
      {
          var row = manager.getSelectedRow();
          

  
  
      }
        
        
        function reload() {
            manager.reload();
        }


    
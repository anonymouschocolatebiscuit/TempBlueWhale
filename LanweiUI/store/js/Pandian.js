     
       
      
       
       
        var manager;
        $(function() {
        
        
        
        
        var form = $("#form").ligerForm();

        
       
         
      
         
         var  txtFlagList=  $.ligerui.get("txtFlagList");
         txtFlagList.set("Width", 160);
         
            
            manager = $("#maingrid").ligerGrid({
            
                columns: [

                  { display: '仓库', name: 'ckName', width: 120, align: 'center',frozen:true},
               
                  { display: '商品编号', name: 'code', width: 120, align: 'center'},
                 { display: '商品名称', name: 'goodsName', width: 220, align: 'left'},
                 { display: '规格', name: 'spec', width: 120, align: 'center'},
                 { display: '单位', name: 'unitName', width: 120, align: 'center'},
              
               
               
                  
                 { display: '系统库存', name: 'sumNum', width: 100, align: 'right'},
                 
                 
                 { display: '盘点库存', name: 'sumNumPD', width: 100, align: 'right',editor: { type: 'float' }},
                 
                 
                 { display: '盘盈盘亏', name: 'sumNumPK', width: 100, align: 'right',
                 
                     render: function (row) {  
                      var html = row.sumNumPK > 0 ? "<span style='color:red'>"+row.sumNumPK+"</span>" : "<span style='color:green'>"+row.sumNumPK+"</span>";  
                    return html;
                    }   
                 
                 
                 }
            
                
               
           
                
            
                ], width: '98%', 
                  //pageSizeOptions: [5, 10, 15, 20],
                  height:'450',
                 // pageSize: 15,
                dataAction: 'local', //本地排序
                usePager: false,
               rownumbers:true,//显示序号
               enabledEdit: true, //控制能否编辑的
                alternatingRow: false,
                onAfterEdit: f_onAfterEdit //更新单元格后的操作
               
               

                
                
            }
            );

        });
        
        
       

        


        function search() {

          
            var cangkuId = $("#txtFlagList").val();
            var typeList = $("#txtTypeList").val();
            var goodsList = $("#txtGoodsList").val();
           
            
          
            
            var cangkuIdString=cangkuId.split(";");
         
            var typeIdString=typeList.split(";");
           
            var goodsIdString=goodsList.split(";");
            
             if(cangkuIdString!="")
            {
                cangkuId="";
                for(var i=0;i<cangkuIdString.length;i++)
                {
                   cangkuId+="'"+cangkuIdString[i]+"'"+",";
                } 
                cangkuId=cangkuId.substring(0,cangkuId.length-1);
                  
            }
            
             if(typeIdString!="")
            {
                typeList="";
                for(var i=0;i<typeIdString.length;i++)
                {
                   typeList+="'"+typeIdString[i]+"'"+",";
                } 
                typeList=typeList.substring(0,typeList.length-1);
                  
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
            
            
          
//            alert(cangkuId);
//            alert(typeList);
//            alert(goodsList);
          
            
          
            //return ;

            manager._setUrl("Pandian.aspx?Action=GetDataList&goodsId="+goodsList+"&typeId="+typeList+"&ckId="+cangkuId);
        }


   
              //编辑后事件 
        function f_onAfterEdit(e)
        {
            var sumNum,sumNumPD,sumPK;
            
            
            sumNumPD=Number(e.record.num);
            
           
            
          
            if (e.column.name == "sumNumPD") //数量改变---开始
            {
               //数量改变：【折扣率、税率】 计算【折扣额、金额、税额、价税合计】
                 sumNumPD=Number(e.value);
               
                
                 sumNumPK=Number(e.record.sumNum)-sumNumPD;
                 
                  
                  
                 //开始赋值
                 
                 manager.updateCell("sumNumPK",sumNumPK, e.record);
                
               
                 
                 
                
                 

            } //数量改变---结束
          
          //manager.reRender();
         //  manager.totalRender();
        }

        
        
        function reload() {
            manager.reload();
        }


    function save()
{

     
     //先删掉空白行


     var data = manager.getData();
     
     
    //1、先删掉空白行
      for(var i=data.length-1;i>=0;i--)
     {
         if(data[i].goodsId==0 || data[i].goodsName=="")
         {
             data.splice(i,1);
            
         }
        
     }
     
     
     //2、判断是否选择商品
      if(data.length==0)
     {
          $.ligerDialog.warn('请选择商品！');
          
          return;
          alert("我就不执行了！");
     }     
     
     
     
     //3、判断商品数量是否都输入了。
      for(var i=0;i<data.length;i++)
      {
          //|| data[i].sumNumPD=="0" || data[i].sumNumPD=="0.00"

         if(data[i].sumNumPD< 0 || data[i].sumNumPD=="" )
         {
             
             $.ligerDialog.warn("请输入第"+(i+1)+"行的商品数量！");
             
             return;
             alert("我就不执行了！");
         }

     }
     
 

    
      
            
            var remarks=$("#txtRemarks").val();
            
      
        
           var headJson={remarks:remarks};
      
    
        
        var dataNew = [];
        dataNew.push(headJson);
        
   
        
        var list=JSON.stringify(headJson);
        
        
        var goodsList=[];
        
        
   
        
        list=list.substring(0,list.length-1);//去掉最后一个花括号
        
        list+=",\"Rows\":";
        list+=JSON.stringify(data);      
        list+="}";
       
      
        
        var postData=JSON.parse(list);//最终的json
        
//        alert(postData.Rows[0].id);
//        
//        alert(postData.bizDate);
//        
//        alert(postData.Rows[0].goodsName);

//        alert(JSON.stringify(postData));

//       $("#txtRemarks").val(JSON.stringify(postData));
       
    
         
         $.ajax({
            type: "POST",
            url: 'ashx/Pandian.ashx',
            contentType: "application/json", //必须有
            //dataType: "json", //表示返回值类型，不必须
            data:JSON.stringify(postData),  //相当于 //data: "{'str1':'foovalue', 'str2':'barvalue'}",
            success: function (jsonResult) {
                
                if(jsonResult=="操作成功！")
                {
                
                    $.ligerDialog.waitting('操作成功！'); setTimeout(function () { $.ligerDialog.closeWaitting();location.reload();}, 2000);
                    
                }
                else
                {
                   $.ligerDialog.warn(jsonResult);
                   
                }
            },
            error: function (xhr) {
                alert("出现错误，请稍后再试:" + xhr.responseText);
            }
        });
            
           
}

        var data = [{
            UnitPrice: 10,
            Quantity: 2,
            Price: 20
        }];
        function formatCurrency(x)
        {
              
            var f_x = parseFloat(x);
            if (isNaN(f_x)) {
               // alert('function:changeTwoDecimal->parameter error');
                return "0.00";
            }
            var f_x = Math.round(x * 100) / 100;
            var s_x = f_x.toString();
            var pos_decimal = s_x.indexOf('.');
            if (pos_decimal < 0) {
                pos_decimal = s_x.length;
                s_x += '.';
            }
            while (s_x.length <= pos_decimal + 2) {
                s_x += '0';
            }
            return s_x;
            
            
        }
        
        
      
       var cangkulist={};
       
      
        //扩展 numberbox 类型的格式化函数
        $.ligerDefaults.Grid.formatters['numberbox'] = function (value, column) {
            var precision = column.editor.precision;
            return value.toFixed(precision);
        };

     
                    
    var manager;
        $(function ()
        {
        
          var form = $("#form").ligerForm();
          
            var g =  $.ligerui.get("ddlVenderList");
            g.set("Width", 250);
            
          
            window['g'] = 
            manager = $("#maingrid").ligerGrid({
                columns: [
                
                // { display: '主键', name: 'id', width: 50, type: 'int',hide:true},
                 { display: '', isSort: false, width: 40,align:'center',frozen:true, render: function (rowdata, rowindex, value)
                 {
                    var h = "";
                    if (!rowdata._editing)
                    {
                        h += "<a href='javascript:addNewRow()' title='新增行' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                        h += "<a href='javascript:deleteRow()' title='删除行' style='float:right;'><div class='ui-icon ui-icon-trash'></div></a> "; 
                    }
                    else
                    {
//                        h += "<a href='javascript:endEdit(" + rowindex + ")'>提交</a> ";
//                        h += "<a href='javascript:cancelEdit(" + rowindex + ")'>取消</a> "; 
                    }
                    return h;
                }
                }
                ,
               
         
               { display: '支付类别', name: 'typeId', width: 200, isSort: false,textField:'typeName',
                    editor: { type: 'select',
                              url:"../baseSet/PayGetList.aspx?Action=GetDDLList&type=支出&r=" + Math.random(), 
                              valueField: 'typeId',textField:'typeName'}

                },
                
                
                { display: '金额', name: 'price', width: 190, type: 'float', align: 'right',editor: { type: 'float' },
                
                 
                
                  totalSummary:
                    {
                        align: 'center',   //汇总单元格内容对齐方式:left/center/right 
                        type: 'sum',
                        render: function (e) 
                        {  //汇总渲染器，返回html加载到单元格
                         //e 汇总Object(包括sum,max,min,avg,count) 
                         
                         // alert("汇总了");
                          
                            return Math.round(e.sum*100)/100;
                            
                           
                            
                            
                        }
                    }
                
                
                },
                
              
                { display: '备注', name: 'remarks', width: 250, align: 'left',type:'text',editor: { type: 'text' } }
                ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '450',
                url: 'OtherPayListAdd.aspx?Action=GetData',
               rownumbers:true,//显示序号
               frozenRownumbers:true,//行序号是否在固定列中
                dataAction: 'local',//本地排序
                usePager:false,
                alternatingRow: false,
                
                totalSummary:true,
                enabledEdit: true
                
            }
            );
        });
 
        var rowNumber=9;
 
     
     

       function f_onSelected(e) { 
            if (!e.data || !e.data.length) return;
 
            var grid = liger.get("maingrid");
 
            var selected = e.data[0]; 
            grid.updateRow(grid.lastEditRow, {
                CustomerID: selected.CustomerID,
                CompanyName: selected.CompanyName
            });
 
            var out = JSON.stringify(selected);
            $("#message").html('最后选择:'+out);
        }

       
     
       
    

     
        function deleteRow()
        { 
           
            if(manager.rows.length==1)
            {
                $.ligerDialog.warn('至少保留一行！')
                
            }
            else
            {
               manager.deleteSelectedRow();
            }
            
        }
        var newrowid = 100;
        
        function addNewRow()
        {
             var gridData = manager.getData();
             var rowNum=gridData.length;
             
           
             manager.addRow({ 
                id: rowNum+1,
                    id : rowNum+1,
                    typeId :"",
                    typeName : "",
                    price : "",
                    remarks : ""
            });

             
        } 
         
    
        function getSelected()
        { 
            var row = manager.getSelectedRow();
            if (!row) { alert('请选择行'); return; }
            alert(JSON.stringify(row));
        }
        function getData()
        { 
            var data = manager.getData();
            alert(JSON.stringify(data));
        } 
        


function save()
{

     
     //先删掉空白行


     var data = manager.getData();
     
     
    //1、先删掉空白行
      for(var i=data.length-1;i>=0;i--)
     {
         if(data[i].typeId==0 || data[i].typeName=="")
         {
             data.splice(i,1);
            
         }
        
     }
     
     
     //2、判断是否选择商品
      if(data.length==0)
     {
          $.ligerDialog.warn('请选择支付类别！');
          
          return;
          alert("我就不执行了！");
     }     
     
     
     
     //3、判断商品数量是否都输入了。
      for(var i=0;i<data.length;i++)
     {
         if(data[i].price<=0 || data[i].price=="" || data[i].price=="0" || data[i].price=="0.00")
         {
             
             $.ligerDialog.warn("请输入第"+(i+1)+"行的支付金额！");
             
             return;
             alert("我就不执行了！");
         }
       
        
     }
     
      
   
 
//    var checkText=$("#ddlVenderList").find("option:selected").text();  //获取Select选择的Text
      var venderId=$("#ddlVenderList").val();  //获取Select选择的Value
     
      var bkId=$("#ddlBankList").val();  //获取Select选择的Value


            
      var bizDate=$("#txtBizDate").val();
      if(bizDate=="")
      {
          $.ligerDialog.warn("请输付款日期！");
          return;
          
      }

         
            
            var remarks=$("#txtRemarks").val();
            
      
        
           var headJson={venderId:venderId,bizDate:bizDate,remarks:remarks,bkId:bkId};
      
    
        
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
            url: 'ashx/OtherPayListAdd.ashx',
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

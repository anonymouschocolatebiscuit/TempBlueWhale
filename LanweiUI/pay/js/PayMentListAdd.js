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

        function selectBill()
        {
            var start = $("#txtDateStart").val();
            var end = $("#txtDateEnd").val();
            
          
            var wlId = $("#ddlVenderList").val();
    
             var keys = document.getElementById("txtKeys").value;
            if (keys == "请输入单据号") {

                keys = "";

            }
           
           managersub._setUrl("PayMentListAdd.aspx?Action=GetDataListSearch&keys=" + keys + "&start=" +start + "&end=" + end+"&wlId="+wlId);
              
        }

         function addNewRow()
        {
            
             manager.addRow({ 
              
                    bkId :"",
                    payPrice :"",
                    payTypeId : "",
                    payNumber : "",
                   
                    remarks : ""
            });
            
            f_onAfterEdit();//从新计算

             
        } 
         

  
    var manager;
        $(function ()
        {
        
          var form = $("#form").ligerForm();
          var target1 = $("#target1").ligerForm();
          
            var g =  $.ligerui.get("ddlVenderList");
            g.set("Width", 250);
            
            var txtDateStart =  $.ligerui.get("txtDateStart");
            txtDateStart.set("Width", 120);
            
            var txtDateEnd =  $.ligerui.get("txtDateEnd");
            txtDateEnd.set("Width", 120);
            
            
            
            window['g'] = 
            manager = $("#maingrid").ligerGrid({
                columns: [
                
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
               
                { display: '结算账户', name: 'bkId', width: 220, isSort: false,textField:'bkName',
                    editor: { type: 'select',
                              url:"../baseSet/AccountList.aspx?Action=GetDDLList&r=" + Math.random(), 
                              valueField: 'id',textField:'names'}
                              
                    ,
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
                
               
              

                { display: '付款金额', name: 'payPrice', width: 120, type: 'float', align: 'right',editor: { type: 'float' },
                
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
             
                { display: '结算方式', name: 'payTypeId', width: 120, isSort: false,textField:'payTypeName',
                    editor: { type: 'select',
                              url:"../baseSet/PayTypeList.aspx?Action=GetDDLList&r=" + Math.random(), 
                              valueField: 'typeId',textField:'typeName'}

                },
                { display: '结算号', name: 'payNumber', width: 120, align: 'left',type:'text',editor: { type: 'text' } },
                
                { display: '备注', name: 'remarks', width: 220, align: 'left',type:'text',editor: { type: 'text' } }
                ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '155',
                url: 'PayMentListAdd.aspx?Action=GetData',
               rownumbers:true,//显示序号
               frozenRownumbers:true,//行序号是否在固定列中
                dataAction: 'local',//本地排序
                usePager:false,
                alternatingRow: false,
                
                totalSummary:false,
                enabledEdit: true, //控制能否编辑的
            
                onAfterEdit: f_onAfterEdit //更新单元格后的操作
            }
            );
        });
// 
 
          //编辑后事件---------付款金额
     
        
        function f_onAfterEdit()
        {
           var sumPrice=0;//本次付款总金额
           var data = manager.getData();
           for(var i=0;i<data.length;i++)
           {
               sumPrice+=Number(data[i].payPrice);
               
           }
          
           var sumPayPrice=0;//本次核销总金额
            var datasub = managersub.getData();
           for(var i=0;i<datasub.length;i++)
           {
               sumPayPrice+=Number(datasub[i].priceCheckNow);
               
           }
           
           if(Number(sumPayPrice)>Number(sumPrice))
           {
               $.ligerDialog.warn("核销总金额不能大于付款总额！");
              return;
           }
           
//           alert("sumPrice："+sumPrice);
//           alert("sumPayPrice："+sumPayPrice);
//          
           var disPrice=$("#txtDisPrice").val();//整单折扣
           
      
           
           var payPriceNowMore=Number(sumPrice)-Number(sumPayPrice)-Number(disPrice);
           
           $("#txtPayPriceNowMore").val(payPriceNowMore);
         
        }
        
        
        
        
 
       var managersub;
        $(function ()
        {
         
          
          
            window['gsub'] = 
            managersub = $("#maingridsub").ligerGrid({
                columns: [
                
                // { display: '主键', name: 'id', width: 50, type: 'int',hide:true},
                 { display: '', isSort: false, width: 40,align:'center',frozen:true, render: function (rowdata, rowindex, value)
                 {
                    var h = "";
                    if (!rowdata._editing)
                    {
                       // h += "<a href='javascript:addNewRow()' title='新增行' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                        h += "<a href='javascript:deleteRowSub()' title='删除行' style='text-align:center;'><div class='ui-icon ui-icon-trash'></div></a> "; 
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
               
               
                
                { display: '单据编号', name: 'sourceNumber',width: 220, align: 'center',
                
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
                
                { display: '业务类别', name: 'bizType',width: 120, align: 'center' },
                { display: '单据日期', name: 'bizDate',width: 80, align: 'center' },
                { display: '单据金额', name: 'sumPriceAll',width: 120, align: 'right' },
                { display: '已核销金额', name: 'priceCheckNowSum',width: 120, align: 'right' },
                { display: '未核销金额', name: 'priceCheckNo',width: 120, align: 'right' },
                
                { display: '本次核销金额', name: 'priceCheckNow', width: 120, type: 'float', align: 'right', editor: { type: 'float', precision: 4 },
                
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

               
                ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '210',
               url: 'PayMentListAdd.aspx?Action=GetDataSub',
               rownumbers:true,//显示序号
               frozenRownumbers:true,//行序号是否在固定列中
                dataAction: 'local',//本地排序
                usePager:false,
                alternatingRow: false,
                
                totalSummary:true,
                enabledEdit: true, //控制能否编辑的
               // onBeforeEdit: f_onBeforeEdit,
                onBeforeSubmitEdit: f_onBeforeSubmitEdit,
                onAfterEdit: f_onAfterEdit //更新单元格后的操作
            }
            );
        });
 

         //只允许编辑前3行
        function f_onBeforeEdit(e)
        { 
            if(e.rowindex<=2) return true;
            return false;
        }
        //限制年龄
        function f_onBeforeSubmitEdit(e)
        {     
             var data = managersub.getData();
           
             if (Number(e.value) > Number(data[e.rowindex].priceCheckNo))//本次核销 大于 未核销金额 
             {
                $.ligerDialog.warn("核销金额不能大于未核销金额！");
               return false;
             }
             return true;
        }

 
        var rowNumber=9;
 
     
    
    
      

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
        
        function deleteRowSub()
        { 
            
            managersub.deleteSelectedRow();
            
       
            
            
            
        }
        
        
        var newrowid = 100;
        
       
      
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
    // alert(JSON.stringify(data));
 
      var datasub = managersub.getData();
    // alert(JSON.stringify(datasub));
     
    // return ;
     
    //1、先删掉空白行
      for(var i=data.length-1;i>=0;i--)
     {
         if(data[i].bkId==0 || data[i].bkName=="")
         {
             data.splice(i,1);
            
         }
        
     }
     
     
     //2、判断是否选择商品
      if(data.length==0)
     {
          $.ligerDialog.warn('请输入付款信息！');
          
          return;
          alert("我就不执行了！");
     }     
     
     
     
     //3、判断商品数量是否都输入了。
      for(var i=0;i<data.length;i++)
     {
         if(data[i].bkId<=0 || data[i].bkName=="")
         {
             
             $.ligerDialog.warn("请选择第"+(i+1)+"行的结算账户！");
             
             return;
             alert("我就不执行了！");
         }
         
         if(data[i].payPrice<=0 || data[i].payPrice=="")
         {
             
             $.ligerDialog.warn("请输入第"+(i+1)+"行的付款金额！");
             
             return;
             alert("我就不执行了！");
         }
         
         
         if(data[i].payTypeId<=0 || data[i].payTypeName=="")
         {
             
             $.ligerDialog.warn("请选择第"+(i+1)+"行的结算方式！");
             
             return;
             alert("我就不执行了！");
         }
         else
         {
            
         }
       
        
     }
     
  
     // var datasub = managersub.getData();
     
     
    //1、先删掉空白行
      for(var i=datasub.length-1;i>=0;i--)
     {
         if(datasub[i].sourceNumber=="")
         {
             datasub.splice(i,1);
            
         }
        
     }
     
     
    
     
     //3、判断商品数量是否都输入了。
      for(var i=0;i<datasub.length;i++)
     {
         if(Number(datasub[i].priceCheckNow)<=0 && datasub[i].sourceNumber!="")
         {
             
             $.ligerDialog.warn("请输入第"+(i+1)+"行的核销金额！");
             
             return;
             alert("我就不执行了！");
         }
       
        
     }
     
     
     
     //
     
      
     
      var bizDate=$("#txtBizDate").val();
      if(bizDate=="")
      {
          $.ligerDialog.warn("请输入付款日期！");
          return;
          
      }

            var remarks=$("#txtRemarks").val();
            
            var disPrice=$("#txtDisPrice").val();
             var payPriceNowMore=$("#txtPayPriceNowMore").val();
           var wlId=$("#ddlVenderList").val();  //获取Select选择的Value
        
           var headJson={wlId:wlId,disPrice:disPrice,payPriceNowMore:payPriceNowMore,bizDate:bizDate,remarks:remarks};
      
   //  alert(JSON.stringify(headJson));
        
        var dataNew = [];
        dataNew.push(headJson);
        
   
        
        var list=JSON.stringify(headJson);//返序列化成字符串、表头
        
      
        list=list.substring(0,list.length-1);//去掉最后一个花括号
        
        list+=",\"Rows\":";
        list+=JSON.stringify(data);//插入账户信息     
        
        list+=",\"RowsBill\":";
       
        list+=JSON.stringify(datasub);//插入核销信息     
         
        list+="}";
       
      
        
        var postData=JSON.parse(list);//最终的json
        
     //   alert(JSON.stringify(postData));
        
      //  return;
//        alert(postData.Rows[0].id);
//        
//        alert(postData.bizDate);
//        
//        alert(postData.Rows[0].goodsName);

    //    alert(JSON.stringify(postData));

//       $("#txtRemarks").val(JSON.stringify(postData));
       
      // return;
         
         $.ajax({
            type: "POST",
            url: 'ashx/PayMentListAdd.ashx',
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

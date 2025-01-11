    
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
        
  
    
            $(document).bind('keydown.grid', function (event)
            {
                if (event.keyCode == 13 || event.keyCode == 39 || event.keyCode == 9) //enter,right arrow,tap
                { 
                   manager.endEditToNext();
                }
            });

    
        
        
       //新样式引入行
        
        function f_selectContact()
        {
            $.ligerDialog.open({ title: '选择商品', name:'winselector',width: 840, height:540, url: '../baseSet/GoodsListSelect.aspx', buttons: [
                { text: '确定', onclick: f_selectContactOK },
                { text: '取消', onclick: f_selectContactCancel }
            ]
            });
            return false;
        }
        function f_selectContactOK(item, dialog)
        {
			var fn = dialog.frame.f_select || dialog.frame.window.f_select; 
            var data = fn(); 
            if (!data)
            {
                alert('请选择行!');
                return;
            }
            
            f_onGoodsChanged(data);
                       
            dialog.close();
            
        }
        
        
        function f_selectContactCancel(item, dialog)
        {
            dialog.close();
        }

        
       
        //新样式引入行end
     
       
      
        //扩展 numberbox 类型的格式化函数
        $.ligerDefaults.Grid.formatters['numberbox'] = function (value, column) {
            var precision = column.editor.precision;
            return value.toFixed(precision);
        };

     
                    
    var manager;
        $(function ()
        {
        
          var form = $("#form").ligerForm();
           var form1 = $("#tbFooter").ligerForm();
            var form2 = $("#form22").ligerForm();
          
            var g =  $.ligerui.get("ddlVenderList");
            g.set("Width", 250);
            
          
            window['g'] = 
            manager = $("#maingrid").ligerGrid({
                columns: [
                
              
                 { display: '', isSort: false, width: 60,align:'center',frozen:true, render: function (rowdata, rowindex, value)
                 {
                    var h = "";
                    if (!rowdata._editing)
                    {
                        h += "<a href='javascript:addNewRow()' title='新增行' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                        h += "<a href='javascript:deleteRow()' title='删除行' style='float:left;'><div class='ui-icon ui-icon-trash'></div></a> "; 
                        h += "<a href='javascript:f_selectContact()' title='选商品' style='float:left;'><div class='ui-icon ui-icon-search'></div></a> ";
                    }
                  
                    return h;
                }
                }
                ,
               
                { display: '商品名称', name: 'goodsName', width: 250, align: 'left',
                
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
                
                { display: '规格', name: 'spec',width: 120, align: 'center' },
                { display: '单位', name: 'unitName',width: 100, align: 'center'},
                
                { display: '仓库', name: 'ckId', width: 100, isSort: false,textField:'ckName',
                    editor: { type: 'select',
                              url:"../baseSet/CangkuList.aspx?Action=GetDDLList&r=" + Math.random(), 
                              valueField: 'ckId',textField:'ckName'}

                },                
               
                { display: '数量', name: 'num', width: 100, type: 'float', align: 'right',editor: { type: 'float' },
                
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
                    display: '未税单价', name: 'price', width: 100, type: 'float', align: 'right', editor: { type: 'float', precision: 4 }
                
                },                
                
                {
                    display: '未税金额', name: 'sumPrice', width: 110, type: 'float', align: 'right', editor: { type: 'float', precision: 4 },
                
                
                  
                
                   totalSummary:
                    {
                        align: 'center',   //汇总单元格内容对齐方式:left/center/right 
                        type: 'sum',
                        render: function (e) 
                        {  //汇总渲染器，返回html加载到单元格
                         //e 汇总Object(包括sum,max,min,avg,count) 
                         
                            //赋值给文本框
                            
                             var itemSumPrice=e.sum;
                            
                        
                          
                        
                          　return "<span id='sumPriceItem'>"+Math.round(itemSumPrice*10000)/10000+"</span>";//formatCurrency(suminf.sum)
                            
                            
                        }
                    }
                
                },
               
                { display: '税率%', name: 'tax', width: 60, type: 'int', align: 'center', editor: { type: 'int' } },
                { display: '含税单价', name: 'priceTax', width: 60, type: 'int', align: 'center', editor: { type: 'float' } },

                {
                    display: '税额', name: 'sumPriceTax', width: 70, type: 'float', align: 'right',



                    totalSummary:
                     {
                         align: 'center',   //汇总单元格内容对齐方式:left/center/right 
                         type: 'sum',
                         render: function (e) {  //汇总渲染器，返回html加载到单元格
                             //e 汇总Object(包括sum,max,min,avg,count) 
                             return Math.round(e.sum * 100) / 100;
                         }
                     }



                },




                {
                    display: '价税合计', name: 'sumPriceAll', width: 90, type: 'float', align: 'right',




                    totalSummary:
                     {
                         align: 'center',   //汇总单元格内容对齐方式:left/center/right 
                         type: 'sum',
                         render: function (e) {  //汇总渲染器，返回html加载到单元格
                             //e 汇总Object(包括sum,max,min,avg,count) 
                             return Math.round(Math.round(e.sum * 100) / 100 * 100) / 100;//Math.round(Math.round(e.sum*100)/100*100)/100


                         }
                     }

                },
               
                
                { display: '备注', name: 'remarks', width: 200, align: 'left',type:'text',editor: { type: 'text' } }
               
                 
                ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '350',
                url: 'PurOrderListAdd.aspx?Action=GetData&id='+param,
               rownumbers:true,//显示序号
               frozenRownumbers:true,//行序号是否在固定列中
                dataAction: 'local',//本地排序
                usePager:false,
                alternatingRow: false,
                
                totalSummary:true,
                enabledEdit: true, //控制能否编辑的
               // onBeforeEdit: f_onBeforeEdit,//控制如果还没选择商品，就不能编辑后面的列
               // onBeforeSubmitEdit: f_onBeforeSubmitEdit,//提交编辑之前检查
                
                //totalRender:f_totalRender,//汇总
                
                onAfterEdit: f_onAfterEdit //更新单元格后的操作
            }
            );
        });
 
        var rowNumber=9;
        var itemSumPriceAll=0;
        
        function f_totalRender(data, currentPageData)
        {
             //return "总仓库数量:"+data.sumPriceAll; 
        }


        //商品 改变事件：获取单位、单价等信息
        function f_onGoodsChanged(e)
        { 
           
         
            if (!e || !e.length) return;
            
            //1、先更新当前行的后续数据
            
            var grid = liger.get("maingrid");

            var selected =e[0];// e.data[0]; 
            
           // alert(selected.names);
          
           var selectedRow = manager.getSelected();

          
            grid.updateRow(selectedRow, {
                
                goodsId: selected.id,
                goodsName:selected.names,
                spec: selected.spec,
                unitName: selected.unitName,
              
                num:1,
                price: selected.priceCost,
             
                sumPrice:selected.priceCost,
                tax: 0,
                priceTax: selected.priceCost,//含税单价、默认为不含税单价
                sumPriceTax: 0,//税额
              
                ckId:selected.ckId,
                ckName:selected.ckName,
                sourceNumber:"",
                itemId:0,
                remarks:""
              
                
            });
            
       

            if(e.length>1) //如果有多行的、先删除空白行，然后插入下面
            {

             var data = manager.getData();
             for(var i=data.length-1;i>=0;i--)
             {
                 if(data[i].goodsId==0 || data[i].goodsName=="")
                 {
                     manager.deleteRow(i);
                    // alert("删除行："+i);
                 }
                
             }

               for(var i=1;i<e.length;i++)
               {
                   grid.addRow({
                        id:rowNumber,
                        goodsId: e[i].id,
                        goodsName:e[i].names,
                        spec:e[i].spec,
                        unitName:e[i].unitName,
                      
                        num:1,
                        price : e[i].priceCost,
                 
                        sumPrice:e[i].priceCost,
                        tax: 0,
                        priceTax: e.data[i].priceCost,//含税单价
                        sumPriceTax: 0,//税额
                      
                        ckId:e[i].ckId,
                        ckName:e[i].ckName,
                        sourceNumber:"",
                        itemId:0,
                        remarks:""
                     
                        
                       });
                    
                    rowNumber=rowNumber+1;         
                 
               }
  
           }
           
           updateTotal();

        }
       
      
      function updateTotal()
      {
      
          
            var data = manager.getData();//getData
            var sumPriceItem=0;//
         
            //1、先删掉空白行
            for(var i=data.length-1;i>=0;i--)
            {
                 if(data[i].goodsId==0 || data[i].goodsId=="" || data[i].goodsName=="")
                 {
                     data.splice(i,1);
                    
                 }
                
             }

           for(var i=0;i<data.length;i++)
           {
                    
               sumPriceItem+=Number(data[i].num)*Number(data[i].price); 
               
           }
                        
           $("#sumPriceItem").html(formatCurrency(sumPriceItem));
           
          
           
      }
 
         //编辑后事件 
        function f_onAfterEdit(e)
        {
            var num,price,sumPrice;
            
            
            num=Number(e.record.num);
            
            price=Number(e.record.price);
            
            var goodsId,goodsName;
            
             goodsId=e.record.goodsId;
             goodsName=e.record.goodsName;
              
             if(goodsId=="" || goodsName=="")
             {
                 return;
             }
             
             
             
    
             
          
            if (e.column.name == "num") //数量改变---开始
            {
               //数量改变：【折扣率、税率】 计算【折扣额、金额、税额、价税合计】
            
                 
            
                  num=Number(e.value);

                  //2、金额=数量*单价-折扣额
                  sumPrice=Number(num)* Number(price);

                  sumPrice=Math.round(sumPrice*10000)/10000;
            
               
                 //开始赋值
                 
                 manager.updateCell("num",num, e.record);
                
            
                 //2、金额
                 manager.updateCell('sumPrice',sumPrice.toString(), e.record);
                 

            } //数量改变---结束
            
            if (e.column.name == "price") //单价改变---开始、计算金额、折扣额、税额、价税合计
            {
               //单价改变：【数量、折扣率、税率】 计算【折扣额、金额、税额、价税合计】; 
                price=Number(e.value);
                
               
                  //2、金额=数量*单价-折扣额
                 sumPrice=Number(num)* Number(price);
                                    
            
                 sumPrice=Math.round(sumPrice*10000)/10000;
                  
                 //开始赋值
                
                 //1、折扣额
                
                 manager.updateCell("price",price, e.record);
                
             
                 //2、金额
                 manager.updateCell('sumPrice',sumPrice, e.record);
                 
            
                 

            } //单价改变---结束
            
          
            
            
            if (e.column.name == "sumPrice") //金额改变
            {
                //金额改变：【数量、折扣额、税率】 计算【折扣率、单价、税额、价税合计】   
               
                sumPrice=Number(e.value);

                //1、计算单价
                
                if(num!=0)
                {
                    price=(sumPrice)/num;
                }
                else
                {
                    price=0;
                }
                
                price=Math.round(price*10000)/10000;
                sumPrice=Math.round(sumPrice*10000)/10000;
              
                 //开始赋值
                
                 //1、单价
                 manager.updateCell("price",price,e.record);
                 
               
                 manager.updateCell('sumPrice',sumPrice, e.record);
                 
                 manager.updateCell("priceTax", priceTax, e.record);

                //3、税额
                 manager.updateCell('sumPriceTax', sumPriceTax, e.record);

                //4、价税合计
                 manager.updateCell('sumPriceAll', sumPriceAll, e.record);

                

            } //金额改变---结束
            
          
        //最后改变汇总行的值
           
          updateTotal();

 
        }


        
        //只允许编辑已经添加商品的行
        function f_onBeforeEdit(e)
        { 
        
//            if(e.data.goodsId!="" && e.data.goodsName!="") return true;
//            return false;
//            
//            if(e.rowindex<=2) return true;
//            return false;


            
            
        }
        //限制折扣、税率范围
        
        function f_onBeforeSubmitEdit(e)
        { 
            if (e.column.name == "dis")
            {
                if (e.value < 0 || e.value > 100) return false;
            }
            
         
            
            
            return true;
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
                    goodsId :"",
                    goodsName : "",
                  
                    spec : "",
                    unitName : "",
                  
                    num : "",
                    
                    price : "",
                    sumPrice : "",
                    ckId : "",
                    ckName : "",
                    remarks : "",
                    sourceNumber:"",
                    itemId:0
                  
            });

             updateTotal();
        } 
         



function save()
{

     
     //先删掉空白行


     var data = manager.getData();
     
     
    //1、先删掉空白行
      for(var i=data.length-1;i>=0;i--)
     {
         if(data[i].goodsId==0 || data[i].goodsId==""  || data[i].goodsName=="" )
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
         if(data[i].num<=0 || data[i].num=="" || data[i].num=="0" || data[i].num=="0.00")
         {
             
             $.ligerDialog.warn("请输入第"+(i+1)+"行的商品数量！");
             
             return;
             alert("我就不执行了！");
         }
         
       
       
        
     }

     var venderId=$("#ddlVenderList").val();  //获取Select选择的Value
     var bizId=$("#ddlYWYList").val(); 
   
            
      var bizDate=$("#txtBizDate").val();
      if(bizDate=="")
      {
          $.ligerDialog.warn("填写订单日期！");
          return;
          
      }

      var sendDate=$("#txtSendDate").val();
      if(sendDate=="")
      {
          $.ligerDialog.warn("填写交货日期！");
          return;
          
      }
        
            
            var remarks=$("#txtRemarks").val();
            
        
         
        
           var headJson={venderId:venderId,bizDate:bizDate,sendDate:sendDate,bizId:bizId,remarks:remarks};
      
    
        
        var dataNew = [];
        dataNew.push(headJson);
        
   
        
        var list=JSON.stringify(headJson);
        
        
        var goodsList=[];
        
        
   
        
        list=list.substring(0,list.length-1);//去掉最后一个花括号
        
        list+=",\"Rows\":";
        list+=JSON.stringify(data);      
        list+="}";
       
      
        
        var postData=JSON.parse(list);//最终的json
       

//         alert(JSON.stringify(postData));
//        
//         return;
    
         
         $.ajax({
            type: "POST",
            url: 'ashx/PurOrderListAdd.ashx',
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



        
var param=getUrlParam("id");


 
function getUrlParam(name)
{
   var reg = new RegExp("(^|&)"+ name +"=([^&]*)(&|$)");

   var r = window.location.search.substr(1).match(reg);

   if (r!=null) return unescape(r[2]); return null;
}


function getthedate(dd,dadd)
{
    //可以加上错误处理
    var a = new Date(dd)
    a = a.valueOf()
    a = a + dadd * 24 * 60 * 60 * 1000
    a = new Date(a);
    var m = a.getMonth() + 1;
    if(m.toString().length == 1){
        m='0'+m;
    }
    var d = a.getDate();
    if(d.toString().length == 1){
        d='0'+d;
    }
    return a.getFullYear() + "-" + m + "-" + d;
}
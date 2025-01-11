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
               
                { display: '商品名称', name: 'goodsName', width: 200, align: 'left',
                
                     render: function (row)
                    {
                       
                        var selectName=row.goodsName.split(";");
                        
                        return selectName[0];
                    },

                   editor: 
                   { 
                      type: 'popup', 
                      valueField: 'names', //获取弹窗的数据字段
                      grid:
                         {
                                url: "../baseSet/GoodsList.aspx?Action=GetDataList",
                                checkbox:true,//目前只支持单选
                                dataAction: 'local',//本地排序
                                usePager:true,
                                columns: 
                                [
                                    { display: '商品编码',name: 'code', width: 80 }, 
                                    { display: '商品名称',name: 'names', width: 100 },
                                    { display: '规格型号',name: 'spec', width: 100 },
                                    { display: '单位',name: 'unitName', width: 60 },
                                    { display: '品牌',name: 'brandName', width: 60 },
                                    { display: '封装',name: 'packages', width: 60 },
                                    { display: '最小包装',name: 'mpq', width: 60 },
                                    { display: '仓库ID',name: 'brandId', width: 40,hide:true}
                                ]
                        }, 
                        condition: 
                        {
                            fields: 
                            [
                               { name: 'code',type:'text',label:'关键字', width:200 }
                            ]
                        }, 
                        onSelected: f_onGoodsChanged //定义选择了以后的处理方法

                    },

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
                { display: '规格型号', name: 'spec',width: 150, align: 'center' },
            
                { display: '单位', name: 'unitName',width: 50, align: 'center' },
                
                { display: '品牌', name: 'brandName', width: 60, align: 'left',type:'text' },
                { display: '最小包装', name: 'mpq', width: 60, align: 'left',type:'text' },
                { display: '封装', name: 'packages', width: 60, align: 'left',type:'text' },
                
                { display: '数量', name: 'num', width: 60, type: 'float', align: 'right',editor: { type: 'float' },
                
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
                
                { display: '未税单价', name: 'price', width: 70, type: 'float', align: 'right', editor: { type: 'float', precision: 4 }
                
                },
                
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
                
                
            
                
                { display: '税率%', name: 'tax', width: 60, type: 'int', align: 'center',editor: { type: 'int' } },
                { display: '含税单价', name: 'priceTax', width: 60, type: 'int', align: 'center',editor: { type: 'float' } },
                
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
                
                { display: '备注', name: 'remarks', width: 150, align: 'left',type:'text',editor: { type: 'text' } }
                ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '350',
                url: 'SalesQuoteListAdd.aspx?Action=GetData',
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
 
        function f_totalRender(data, currentPageData)
        {
             //return "总仓库数量:"+data.sumPriceAll; 
        }

  
   
      
       
        //商品 改变事件：获取单位、单价等信息
        function f_onGoodsChanged(e)
        { 
           
         
            if (!e.data || !e.data.length) return;
            
            //1、先更新当前行的后续数据
            
            var grid = liger.get("maingrid");

            var selected = e.data[0]; 
            grid.updateRow(grid.lastEditRow, {
                
                goodsId: selected.id,
                goodsName: selected.names,
                spec: selected.spec,
                unitName: selected.unitName,
                brandName:selected.brandName,
                mpq:selected.mpq,
                packages:selected.packages,
               
                num:1,
         
                price: selected.priceSales,//未税单价
                sumPrice:selected.priceSales,//未税金额
                tax:0,
                priceTax:selected.priceSales,//含税单价、默认为不含税单价
                sumPriceTax:0,//税额
                sumPriceAll:selected.priceSales,//价税合计
                remarks:""
                
            });

            if(e.data.length>1) //如果有多行的、先删除空白行，然后插入下面
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

               for(var i=1;i<e.data.length;i++)
               {
                   grid.addRow({
                        id:rowNumber,
                        goodsId: e.data[i].id,
                        goodsName:e.data[i].names,
                        spec:e.data[i].spec,
                        unitName:e.data[i].unitName,
                        
                        brandName:e.data[i].brandName,
                        mpq:e.data[i].mpq,
                        packages:e.data[i].packages,
                        
                        num:1,
                       
                        price : e.data[i].priceSales,//未税单价
                        sumPrice:e.data[i].priceSales,//未税金额
                        tax:0,
                        priceTax:e.data[i].priceSales,//含税单价
                        sumPriceTax:0,//税额
                        sumPriceAll:e.data[i].priceSales,
                        remarks:""
                        
                       });
                    
                    rowNumber=rowNumber+1;         
                 
               }
  
           }

        }
        
      
        
        
        //城市 下拉框 数据初始化,这里也可以改成 改变服务器参数( parms,url )
        function f_createCityData(e)
        {
            var Country = e.record.Country;
            var options =  {
                data: getCityData(Country)
            }; 
            return options;
        }  

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

       
     
       
         //编辑后事件 
        function f_onAfterEdit(e)
        {
            var num;//数量
       
       
            var price;//未税单价
            var tax;//税率
            var priceTax;//含税单价
            
         
            var sumPrice;//未税金额
            var sumPriceTax;//税额
            var sumPriceAll;//价税合计
            
            num=Number(e.record.num);
       
         
            
            price=Number(e.record.price);
            
         
            
            tax=Number(e.record.tax);
            
            priceTax=Number(e.record.priceTax);
            
            
        
            sumPrice=Number(e.record.sumPrice);
            sumPriceTax=Number(e.record.sumPriceTax);
            sumPriceAll=Number(e.record.sumPriceAll);
            
          
            if (e.column.name == "num") //数量改变---开始
            {
                 
                 //数量改变：【折扣率、税率】 计算【折扣额、金额、税额、价税合计】
                 num=Number(e.value);
               
                
                
             
                                 
                  //2、未税金额=数量*未税单价
                  sumPrice=Number(num)* Number(price);
                                    
            
                 //3、税额=数量*含税单价*税率/100
                   sumPriceTax=Number(num)*Number(priceTax-price);
               
                 //4、价税合计=金额-折扣+税额
                  sumPriceAll=Number(priceTax)*Number(num);
                  
                  
                  num=Math.round(num*100)/100;
            
                  price=Math.round(price*1000000)/1000000;//6位小数
                  tax=Math.ceil(tax);
                  priceTax=Math.round(priceTax*1000000)/1000000;
                  
                  
       
                  sumPrice=Math.round(sumPrice*1000000)/1000000;
                  sumPriceTax=Math.round(sumPriceTax*1000000)/1000000;
                  sumPriceAll=Math.round(sumPriceAll*1000000)/1000000;
                 
                  
                  
                 //开始赋值
                 
                 manager.updateCell("num",num, e.record);
                
              
                 
                 //2、未税金额
                 manager.updateCell('sumPrice',sumPrice, e.record);
                 
                 //3、税额
                 manager.updateCell('sumPriceTax',sumPriceTax, e.record);
                 
                 //4、价税合计
                 manager.updateCell('sumPriceAll',sumPriceAll, e.record);
                 
                 
                
                 

            } //数量改变---结束
            
   
            
            if (e.column.name == "price") //未税单价改变---开始、计算折扣额、金额、税额、价税合计
            {
            
                 //未税单价改变： 计算【利润、含税单价、未税金额、含税单价、税额、价税合计】; 
                price=Number(e.value);
                
                
          
                
                priceTax=price*(1+tax/100);
               
               
                                 
                  //2、未税金额=数量*未税单价
                  sumPrice=Number(num)* Number(price);
                                    
            
                 //3、税额=数量*含税单价*税率/100
                   sumPriceTax=Number(num)*Number(priceTax-price);
               
                 //4、价税合计=金额-折扣+税额
                   sumPriceAll=Number(priceTax)*Number(num);
                  
                  
                  num=Math.round(num*100)/100;
          
                  price=Math.round(price*1000000)/1000000;//6位小数
                  tax=Math.ceil(tax);
                  priceTax=Math.round(priceTax*1000000)/1000000;
                  
                  
             
                  sumPrice=Math.round(sumPrice*1000000)/1000000;
                  sumPriceTax=Math.round(sumPriceTax*1000000)/1000000;
                  sumPriceAll=Math.round(sumPriceAll*1000000)/1000000;
                 
                  
                  
                 //开始赋值
                 
             
                 manager.updateCell("price",price, e.record);
                 
                 manager.updateCell("priceTax",priceTax, e.record);
                
          
                 
                 //2、未税金额
                 manager.updateCell('sumPrice',sumPrice, e.record);
                 
                 //3、税额
                 manager.updateCell('sumPriceTax',sumPriceTax, e.record);
                 
                 //4、价税合计
                 manager.updateCell('sumPriceAll',sumPriceAll, e.record);
                 
            } //未税单价改变---结束
            
           
             if (e.column.name == "tax") //税率改变---开始、计算税额、价税合计
            {
               //manager.updateCell('Price', e.record.UnitPrice * e.record.Quantity, e.record); 
               
                tax=Number(e.value);
                
                priceTax=price*(1+tax/100);//含税单价
              
               
                //3、税额=数量*含税单价*税率/100
                 sumPriceTax=Number(num)*Number(priceTax-price);
               
                 //4、价税合计=金额-折扣+税额
                 sumPriceAll=Number(priceTax)*Number(num);
                  
                 
                 
              
                  
                 //开始赋值
                 
                 //2、含税单价
                 manager.updateCell('priceTax',priceTax, e.record);
                 
                 //3、税额
                 manager.updateCell('sumPriceTax',sumPriceTax, e.record);
                 
                 //4、价税合计
                 manager.updateCell('sumPriceAll',sumPriceAll, e.record);

            } //税率改变---结束
            
           
            
             if (e.column.name == "priceTax") //含税单价改变
            {
                //含税单价改变：【未税单价、未税金额、利润、税额、价税合计】
               
                priceTax=Number(e.value);

                //1、计算单价
                
                if(tax!=0)
                {
                    price=priceTax/(1+tax/100);//如果税率不为0、就计算未税单价
                }
                else
                {
                    price=priceTax;//如果为0、那就等于含税单价
                }
                
              
                                 
                  //2、未税金额=数量*未税单价
                  sumPrice=Number(num)* Number(price);
                                    
            
                 //3、税额=数量*含税单价*税率/100
                 sumPriceTax=Number(num)*Number(priceTax-price);
               
                 //4、价税合计=金额-折扣+税额
                   sumPriceAll=Number(priceTax)*Number(num);
                  
                  
                  num=Math.round(num*100)/100;
           
                  price=Math.round(price*1000000)/1000000;//6位小数
                  tax=Math.ceil(tax);
                  priceTax=Math.round(priceTax*1000000)/1000000;
                  
                  
              
                  sumPrice=Math.round(sumPrice*1000000)/1000000;
                  sumPriceTax=Math.round(sumPriceTax*1000000)/1000000;
                  sumPriceAll=Math.round(sumPriceAll*1000000)/1000000;
                 
                  
                  
                 //开始赋值
            
                 
                 manager.updateCell("price",price, e.record);
                 
                 manager.updateCell("priceTax",priceTax, e.record);
                
            
                 
                 //2、未税金额
                 manager.updateCell('sumPrice',sumPrice, e.record);
                 
                 //3、税额
                 manager.updateCell('sumPriceTax',sumPriceTax, e.record);
                 
                 //4、价税合计
                 manager.updateCell('sumPriceAll',sumPriceAll, e.record);

            } //含税单价改变---结束
            
            
          
      
           
          
          
        //最后改变汇总行的值
           
           
         // manager.reRender({totalOnly:true});//老大的方法

        
         
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
            if(e.data.goodsId!="" && e.data.goodsName!="")
            {
                if (e.column.name == "profit")
                {
                    if (e.value < 0 || e.value > 100) return false;
                }
                
                if (e.column.name == "tax")
                {
                    if (e.value < 0 || e.value > 100) return false;
                }
                
                return true;
            }
            else
            {
               return false;
            }
            
            
            
        }
       

       function beginEdit() {
            var row = manager.getSelectedRow();
            if (!row) { alert('请选择行'); return; }
            manager.beginEdit(row);
        }
        function cancelEdit() {
            var row = manager.getSelectedRow();
            if (!row) { alert('请选择行'); return; }
            manager.cancelEdit(row);
        }
        function cancelAllEdit()
        {
            manager.cancelEdit();
        }
        function endEdit()
        {
            var row = manager.getSelectedRow();
            if (!row) { alert('请选择行'); return; }
            manager.endEdit(row);
        }
        function endAllEdit()
        {
            manager.endEdit();
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
                    unitName : "",
                    brandName:"",
                    mpq:"",
                    packages:"",
                    num : "",
                    price : "",
                  
                    sumPrice : "",

                    tax : "",
                    priceTax : "",
                    sumPriceTax : "",
                    sumPriceAll : "",
                    ckId : "",
                    ckName : "",

                    remarks : ""
            });

             
        } 
         
        function updateRow()
        {
            var selected = manager.getSelected();
            if (!selected) { alert('请选择行'); return; }
          
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
         if(data[i].num<=0 || data[i].num=="" || data[i].num=="0" || data[i].num=="0.00")
         {
             
             $.ligerDialog.warn("请输入第"+(i+1)+"行的商品数量！");
             
             return;
             alert("我就不执行了！");
         }
         
         if(data[i].price<=0 || data[i].price=="" || data[i].price=="0" || data[i].price=="0.00")
         {
             
             $.ligerDialog.warn("请输入第"+(i+1)+"行的商品未税单价！");
             
             return;
             alert("我就不执行了！");
         }
       
        
     }
     
      
   
 
//    var checkText=$("#ddlVenderList").find("option:selected").text();  //获取Select选择的Text
     var venderId=$("#ddlVenderList").val();  //获取Select选择的Value


            
      var bizDate=$("#txtBizDate").val();
      if(bizDate=="")
      {
          $.ligerDialog.warn("请输入报价日期！");
          return;
          
      }
      
      var deadLine=$("#txtDeadLine").val();
      if(deadLine=="")
      {
          $.ligerDialog.warn("请输入有效日期！");
          return;
          
      }

     var isTax=$("#ddlIsTaxList").val(); 
     
     var bizId=$("#ddlBizId").val(); 
     
     var isFreight=$("#ddlIsFreight").val(); 
     
     var payWay=$("#ddlPayWayList").val(); 
     
     var payDate=$("#ddlPayDateList").val(); 
     
     var freightWay=$("#ddlFreightWayList").val(); 
     
     var packageWay=$("#ddlPackageList").val(); 
           
            var sendDate= $("#txtSendDate").val();

              if(sendDate=="")
              {
                  $.ligerDialog.warn("请输入交货周期！");
                  return;
                  
              }
      
            
            var sendPlace=$("#txtSendPlace").val();
            
            var remarks=$("#txtRemarks").val();
            
      
        
           var headJson={venderId:venderId,bizDate:bizDate,bizId:bizId,deadLine:deadLine,sendPlace:sendPlace,isTax:isTax,isFreight:isFreight,freightWay:freightWay,packageWay:packageWay,payWay:payWay,payDate:payDate,sendDate:sendDate,remarks:remarks};
      
    
        
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
            url: 'ashx/SalesQuoteListAdd.ashx',
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

function checkBill(){

   var data = manager.getData();
   if(data.length==0)
   {
       $.ligerDialog.warn('请选择商品信息');
       return false;
   }
   else
   {
      for(var i=0;i<data.length;i++)
      {
          if(data.Rows[i].goodsName=="" || data.Rows[i].goodsId==0)
          {
              
              $.ligerDialog.warn('第：'+i+"行商品信息为空！");
              return false;
             
          }
          
          if(data.Rows[i].num==0)
          {
              
              $.ligerDialog.warn('请填写第'+i+"行商品数量！");
              return false;
             
          }
          
    
      }
   }
   

};

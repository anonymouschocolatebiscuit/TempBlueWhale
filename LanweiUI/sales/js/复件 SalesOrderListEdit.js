
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
 
      
        //扩展 numberbox 类型的格式化函数
        $.ligerDefaults.Grid.formatters['numberbox'] = function (value, column) {
            var precision = column.editor.precision;
            return value.toFixed(precision);
        };                                 $(function ()
        { 
            $("#ddlClientList").ligerComboBox({
                onBeforeOpen: f_selectContact, valueFieldID: 'hfId',width:300
            });
        });
        function f_selectContact()
        {
            $.ligerDialog.open({ title: '选择客户', name:'winselector',width: 720, height: 500, url: '../baseSet/ClientListSelect.aspx', buttons: [
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
            $("#ddlClientList").val(data.code+","+data.names);
            $("#hfId").val(data.id);
            dialog.close();
        }
        function f_selectContactCancel(item, dialog)
        {
            dialog.close();
        }                                
    var manager;
        $(function ()
        {
              
           
            
            
            

//              $("#ddlClientList").ligerComboBox(
//                {
//                    url: '../../handler/ComboBoxData.ashx',
//                    valueField: 'id',
//                    textField: 'codeName',
//                    selectBoxWidth: 400,
//                    autocomplete: true,
//                    width: 400,
//                    renderItem: function (e)
//                    { 
//                        var data = e.data, key = e.key;
//                        var out = [];
//                        out.push('<div>' + this._highLight(data.codeName, key) + '</div>');
//                        out.push('<div class="desc">地址：' + data.address+ '</div>');
//                        return out.join('');
//                    }
//                    
//               }

                  $("#ddlClientList2").ligerComboBox(
                {
                    url: '../../handler/ComboBoxData.ashx',
                    valueField: 'id',
                    textField: 'codeName', 
                    selectBoxWidth: 250,
                    autocomplete: true,
                    width: 250 
                }
          

            
            
           
            
         );            //监听回车事件开始         $("#maingrid3").bind("keydown",function(e){
        // 兼容FF和IE和Opera    
    var theEvent = e || window.event;    
    var code = theEvent.keyCode || theEvent.which || theEvent.charCode;    
    if (code == 13) {    
        //回车执行查询
           
          // alert("你按回车了哇");
          // endEdit();
          
           
           // $("#queryButton").click();
        }    
    });        //监听回车事件结束                //监听键盘开始    
                        //监听键盘结束
        
          var form = $("#form").ligerForm();
          
           var form1 = $("#tbFooter").ligerForm();
          
        
                $("#ddlYWYList").ligerGetComboBoxManager().setDisabled();        
         var ddlClientList =  $.ligerui.get("ddlClientList");
         ddlClientList.set("Width", 250);
            
         var txtDis =  $.ligerui.get("txtDis");
         txtDis.set("Width", 100);
         
         var  txtDisPrice=  $.ligerui.get("txtDisPrice");
         txtDisPrice.set("Width", 100);
         
         var  txtSumPrice=  $.ligerui.get("txtSumPrice");
         txtSumPrice.set("Width", 100);
         
          var  txtSumPriceWY=  $.ligerui.get("txtSumPriceWY");
         txtSumPriceWY.set("Width", 100);
         
         var  txtSumPricePayReady=  $.ligerui.get("txtSumPricePayReady");
         txtSumPricePayReady.set("Width", 100);
         
          var  txtSumPricePayNeed=  $.ligerui.get("txtSumPricePayNeed");
         txtSumPricePayNeed.set("Width", 100);
         
          var  ddlSendWayList=  $.ligerui.get("ddlSendWayList");
         ddlSendWayList.set("Width", 100);
          
           var  txtSumPriceSend=  $.ligerui.get("txtSumPriceSend");
         txtSumPriceSend.set("Width", 100);
         
         
         
         
          
            window['g'] = 
            manager = $("#maingrid").ligerGrid({
                columns: [
                
                // { display: '主键', name: 'id', width: 50, type: 'int',hide:true},
                 { display: '', isSort: false, width: 60,align:'center',frozen:true, render: function (rowdata, rowindex, value)
                 {
                    var h = "";
                    if (!rowdata._editing)
                    {
                        h += "<a href='javascript:addNewRow()' title='新增行' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                        h += "<a href='javascript:deleteRow()' title='删除行' style='float:left;'><div class='ui-icon ui-icon-trash'></div></a> ";
                        
                        h += "<a href='javascript:searchRow()' title='查库存' style='float:left;'><div class='ui-icon ui-icon-search'></div></a> ";
                         
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
               
                { display: '商品编码', name: 'code', width: 150, align: 'left',type:'text'},//editor: { type: 'text' } 
               
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
                                    { display: '商品编码',name: 'code', width: 100 }, 
                                    { display: '商品名称',name: 'names', width: 250 },
                                    { display: '规格',name: 'spec', width: 100 },
                                    { display: '单位',name: 'unitName', width: 100 },
                                    { display: '售价',name: 'priceSales', width: 100 },
                                    { display: '首选仓库',name: 'ckName', width: 100,hide:true },
                                    { display: '仓库ID',name: 'ckId', width: 40,hide:true}
                                ]
                        }, 
                        condition: 
                        {
                            fields: 
                            [
                               { name: 'code',type:'text',label:'编码', width:200 }
                              
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
                
                { display: '规格', name: 'spec',width: 150, align: 'center' },
                
                { display: '单位', name: 'unitName',width: 60, align: 'center' },
                
                { display: '原价', name: 'price', width: 70, type: 'float', align: 'right' },//, editor: { type: 'float', precision: 4 }原价不能修改
                
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
                
              

                { display: '折扣%', name: 'dis', width: 70, type: 'float', align: 'center',editor: { type: 'float' } },
                
                 { display: '现价', name: 'priceNow', width: 70, type: 'float', align: 'right', editor: { type: 'float', precision: 4 }
                
                },
                
                { display: '折扣金额', name: 'disPrice', width: 80, type: 'float', align: 'right',
                
                   //editor: { type: 'float' },
                  
                    totalSummary:
                    {
                        align: 'center',   //汇总单元格内容对齐方式:left/center/right 
                        type: 'sum',
                        render: function (e) 
                        {  //汇总渲染器，返回html加载到单元格
                         //e 汇总Object(包括sum,max,min,avg,count) 
                            return "<span id='totalPriceDis'>"+Math.round(Math.round(e.sum*100)/100*100)/100+"</span>";//formatCurrency(suminf.sum)
                        }
                    }
                
                
                },
                

                { display: '小计', name: 'sumPriceAll', width: 100, type: 'float', align: 'right',
                
                  //editor: { type: 'float' } ,
                  
                
                   totalSummary:
                    {
                        align: 'center',   //汇总单元格内容对齐方式:left/center/right 
                        type: 'sum',
                        
//                        render: function (e) 
//                        {  //汇总渲染器，返回html加载到单元格
//                         //e 汇总Object(包括sum,max,min,avg,count) 
//                            return Math.round(Math.round(e.sum*100)/100*100)/100 ;//Math.round(Math.round(e.sum*100)/100*100)/100
//                            
//                            
//                            
//                            
//                            
//                        }
                        
                        render:function(e){
　　　　　　            　return "<span id='totalPriceAll'>"+Math.round(Math.round(e.sum*100)/100*100)/100+"</span>";//formatCurrency(suminf.sum)
                        }
                        
                    }
                
                },
               
              
                
                { display: '备注', name: 'remarks', width: 130, align: 'left',type:'text',editor: { type: 'text' } }
                ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '320',
                url: 'SalesOrderListEdit.aspx?Action=GetData&id='+getUrlParam("id"),//获取参数
               rownumbers:true,//显示序号
               frozenRownumbers:true,//行序号是否在固定列中
                dataAction: 'local',//本地排序
                usePager:false,
                alternatingRow: false,
                
                totalSummary:true,
                enabledEdit: true, //控制能否编辑的
               // onBeforeEdit: f_onBeforeEdit,//控制如果还没选择商品，就不能编辑后面的列
               // onBeforeSubmitEdit: f_onBeforeSubmitEdit,//提交编辑之前检查
                onAfterShowData:f_onAfterShowData,//显示完整数据之后、、用于更新汇总和下面的计算
               
                
                onAfterEdit: f_onAfterEdit //更新单元格后的操作
            }
            );
            
            
            //回车事件
            $("#maingrid").bind('keydown.grid', function (event)
            {
                if (event.keyCode == 13) //enter,也可以改成9:tab
                { 
                  
                  // alert("回车事件");
                  
                    var row = manager.getSelectedRow();
            if (!row) { alert('请选择行'); return; }
          
          
          var data = manager.getUpdated();
            alert(JSON.stringify(data));

                   
               
                    
                 //   manager.endEditToNext();
                    
                    
                     var selected = manager.getSelected();
            if (!selected) { alert('请选择行'); return; }
            manager.updateRow(selected,{
                UnitPrice: 40,
                Quantity: parseInt($("#txtQuantity").val())
            });
                    
                    
                }
            });
        
            
            
        });
 
        var rowNumber=9;
        
        
        
       
        
 
      //显示完整数据之后、、用于更新汇总和下面的计算
       function f_onAfterShowData()
       {
          updateTotalSum();
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
                code: selected.code,
                goodsName: selected.names,
                spec:selected.spec,
                unitName: selected.unitName,
                num:1,
                price: selected.priceSales,
                dis:0,
                priceNow: selected.priceSales,
                disPrice:0,
                sumPrice:selected.priceSales,
                tax:0,
                taxPrice:0,
                sumPriceAll:selected.priceSales,
                ckId:selected.ckId,
                ckName:selected.ckName,
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
                        code:e.data[i].code,
                        goodsName:e.data[i].names,
                        spec:e.data[i].spec,
                        unitName:e.data[i].unitName,
                        num:1,
                        price : e.data[i].priceSales,
                        dis : 0,
                        priceNow: e.data[i].priceSales,
                        disPrice:0,
                        sumPrice:e.data[i].priceSales,
                        tax:0,
                        taxPrice:0,
                        sumPriceAll:e.data[i].priceSales,
                        ckId:e.data[i].ckId,
                        ckName:e.data[i].ckName,
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

       
       var stringCode="";
     
     
     
     
       
         //编辑后事件 
        function f_onAfterEdit(e)
        {
            var num,price,dis,disPrice,priceNow,sumPrice,tax,taxPrice,sumPriceAll;
            
            
            num=Number(e.record.num);
            
            price=Number(e.record.price);
                
            dis=Number(e.record.dis);
            
            priceNow=Number(e.record.priceNow);
            
            disPrice=Number(e.record.disPrice);
            
            sumPrice=Number(e.record.sumPrice);    
            
            tax=Number(e.record.tax);
            
            taxPrice=Number(e.record.taxPrice);
            
            sumPriceAll=Number(e.record.sumPriceAll);
            
          
           if (e.column.name == "code") //数量改变---开始
           {
               
               // alert("输入的是什么东西啊？"+e.value);
                
                var code=e.value;
                
               // alert("回车事件："+code);
               
                //获取编号
                stringCode=code;
                
                //结束编辑
                endEdit();
                
                
                
                // manager.endEditToNext();
             
           }
          
           
            if (e.column.name == "num") //数量改变---开始
            {
               //数量改变：【折扣率、税率】 计算【折扣额、金额、税额、价税合计】
                 num=Number(e.value);
               
                
                 //处理扣率问题
                 if(dis==0)
                 {
                    dis=100;
                 }
                
                 //1、折扣额 = 数量*单价*(1-扣率/100)
                 disPrice=Number(num)*Number(price)*(1-dis/100);
                 
                                 
                  //2、金额=数量*单价-折扣额
                  sumPrice=Number(num)* Number(price)-disPrice;
                                    
            
                 //3、税额=数量*单价*税率/100
                  taxPrice=Number(sumPrice)*Number(tax)/100;
                
                
                 //4、价税合计=金额-折扣+税额
                  sumPriceAll=Number(sumPrice) + Number(taxPrice);
                  
                  
                  num=Math.round(num*100)/100;
                  price=Math.round(price*100)/100;
                  priceNow=Math.round(priceNow*100)/100;
                  dis=Math.round(dis*100)/100;
                  disPrice=Math.round(disPrice*100)/100;
                  sumPrice=Math.round(sumPrice*100)/100;
                  tax=Math.ceil(tax);
                  taxPrice=Math.round(taxPrice*100)/100;
                  sumPriceAll=Math.round(sumPriceAll*100)/100;
                 
                  
                  
                 //开始赋值
                 
                 manager.updateCell("num",num, e.record);
                
                 //1、折扣额
                 manager.updateCell("disPrice",disPrice, e.record);
                 
                 //2、金额
                 manager.updateCell('sumPrice',sumPrice, e.record);
                 
                 //3、税额
                 manager.updateCell('taxPrice',taxPrice, e.record);
                 
                 //4、价税合计
                 manager.updateCell('sumPriceAll',sumPriceAll, e.record);
                 
                 
                
                 

            } //数量改变---结束
            
            if (e.column.name == "price") //单价改变---开始、计算金额、折扣额、税额、价税合计
            {
               //单价改变：【数量、折扣率、税率】 计算【折扣额、金额、税额、价税合计】; 
                price=Number(e.value);
                
                 //处理扣率问题
               
                 //现价
                 
                 priceNow=price*dis/100;
                 
                   if(dis==0)
                 {
                    priceNow=price;
                 }
                 
                 
                
                 //1、折扣额 = 数量*单价*(100-扣率)/100
                 disPrice=Number(num)*Number(price-priceNow);

                 
                  //2、金额=数量*现价
                 sumPrice=Number(num)* Number(priceNow);
                                    
            
                 //3、税额=数量*单价*税率/100
                 taxPrice=Number(sumPrice)*Number(tax)/100;
                
                
                 //4、价税合计=金额-折扣+税额
                 sumPriceAll=Number(sumPrice) + Number(taxPrice);
                 
                 
                  num=Math.round(num*100)/100;
                  price=Math.round(price*100)/100;
                  priceNow=Math.round(priceNow*100)/100;
                  dis=Math.round(dis*100)/100;
                  disPrice=Math.round(disPrice*100)/100;
                  sumPrice=Math.round(sumPrice*100)/100;
                  tax=Math.ceil(tax);
                  taxPrice=Math.round(taxPrice*100)/100;
                  sumPriceAll=Math.round(sumPriceAll*100)/100;
                  
                 //开始赋值
                
                
                manager.updateCell("priceNow",priceNow, e.record);
                
                
                 
                 //1、折扣额
                
                 manager.updateCell("price",price, e.record);
                
                 manager.updateCell("disPrice",disPrice, e.record);
                 
                 //2、金额
                 manager.updateCell('sumPrice',sumPrice, e.record);
                 
                 //3、税额
                 manager.updateCell('taxPrice',taxPrice, e.record);
                 
                 //4、价税合计
                 manager.updateCell('sumPriceAll',sumPriceAll, e.record);
                 

            } //单价改变---结束
            
            
            //改变现价---开始
              if (e.column.name == "priceNow") //现价改变---开始、计算折扣率、折扣额、税额、价税合计
            {
               //现价改变：【数量、税率】 计算【折扣率、折扣额、金额、税额、价税合计】; 
                priceNow=Number(e.value);
                
                 //处理扣率问题
               
                 //现价
             
            
             
                dis=Math.round(priceNow/price*100)/100;
                 
                if(priceNow==0)
                {
                    dis=0;
                }
                
                 //1、折扣额 = 数量*单价*(100-扣率)/100
                 disPrice=Number(num)*Number(price-priceNow);

                 
                  //2、金额=数量*现价
                 sumPrice=Number(num)* Number(priceNow);
                                    
            
                 //3、税额=数量*单价*税率/100
                 taxPrice=Number(sumPrice)*Number(tax)/100;
                
                
                 //4、价税合计=金额-折扣+税额
                 sumPriceAll=Number(sumPrice) + Number(taxPrice);
                 
                 
                  num=Math.round(num*100)/100;
                  price=Math.round(price*100)/100;
                  priceNow=Math.round(priceNow*100)/100;
                  dis=Math.round(dis*100)/100;
                  disPrice=Math.round(disPrice*100)/100;
                  sumPrice=Math.round(sumPrice*100)/100;
                  tax=Math.ceil(tax);
                  taxPrice=Math.round(taxPrice*100)/100;
                  sumPriceAll=Math.round(sumPriceAll*100)/100;
                  
                 //开始赋值
                
                
                manager.updateCell("priceNow",priceNow, e.record);
                
                manager.updateCell("dis",Math.ceil(dis*100),e.record);
                 
                 //1、折扣额
                
                 manager.updateCell("price",price, e.record);
                
                 manager.updateCell("disPrice",disPrice, e.record);
                 
                 //2、金额
                 manager.updateCell('sumPrice',sumPrice, e.record);
                 
                 //3、税额
                 manager.updateCell('taxPrice',taxPrice, e.record);
                 
                 //4、价税合计
                 manager.updateCell('sumPriceAll',sumPriceAll, e.record);
                 

            } //单价改变---结束
            
            
            
            if (e.column.name == "dis") //折扣改变---开始、计算折扣额、金额、税额、价税合计
            {
               //折扣改变：【数量、单价、税率】 计算【折扣额、金额、税额、价税合计】; 
                dis=Number(e.value);
               
                
                 //处理扣率问题
                
                
                 //现价
                 
                 priceNow=price*dis/100;
                 
                  if(dis==0)
                 {
                    priceNow=price;//如果是0
                 }
                 
                
                  //1、折扣额 = 数量*单价*(100-扣率)/100
                 disPrice=Number(num)*Number(price-priceNow);

                 
                  //2、金额=数量*单价-折扣额
                 sumPrice=Number(num)* Number(price)-disPrice;
                                    
            
                 //3、税额=数量*单价*税率/100
                 taxPrice=Number(sumPrice)*Number(tax)/100;
                
                
                 //4、价税合计=金额-折扣+税额
                 sumPriceAll=Number(sumPrice) + Number(taxPrice);
                 
                 
                  num=Math.round(num*100)/100;
                  price=Math.round(price*100)/100;
                  priceNow=Math.round(priceNow*100)/100;
                  dis=Math.round(dis*100)/100;
                  disPrice=Math.round(disPrice*100)/100;
                  sumPrice=Math.round(sumPrice*100)/100;
                  tax=Math.ceil(tax);
                  taxPrice=Math.round(taxPrice*100)/100;
                  sumPriceAll=Math.round(sumPriceAll*100)/100;
                  
                 //开始赋值
                
                
                 //1、现价
                 manager.updateCell("priceNow",priceNow, e.record);
                 
                
                 //1、折扣额
                 manager.updateCell("disPrice",disPrice, e.record);
                 
                 //2、金额
                 manager.updateCell('sumPrice',sumPrice, e.record);
                 
                 //3、税额
                 manager.updateCell('taxPrice',taxPrice, e.record);
                 
                 //4、价税合计
                 manager.updateCell('sumPriceAll',sumPriceAll, e.record);

            } //折扣改变---结束
            
             if (e.column.name == "disPrice") //折扣金额改变---开始、计算扣率、金额、税额、价税合计
            {
               //折扣额改变：【单价、数量、税率】 计算【折扣率、金额、税额、价税合计】 
                disPrice=Number(e.value);
                dis=0;
                
                 //处理折扣问题、1、如果折扣金额大于0且小于总金额
                 
                 if(disPrice>0 && disPrice<(num*price))
                 {
                     dis=(num*price -disPrice)/(num*price);//Math.ceil();//Math.round(e.sum*100)/100  折扣额 = 数量*单价*扣率
                 
                 }
                 else
                 {
                    dis=0;
                 }
                 
                 
                 
                //现价
                
                priceNow=price*dis/100;
                 

                  //2、金额=数量*单价-折扣额
                 sumPrice=Number(num)* Number(price)-disPrice;

                 //3、税额=数量*单价*税率/100
                 taxPrice=Number(sumPrice)*Number(tax)/100;
                
                
                 //4、价税合计=金额-折扣+税额
                 sumPriceAll=Number(sumPrice) + Number(taxPrice);
                 
                 
                 num=Math.round(num*100)/100;
                  price=Math.round(price*100)/100;
                  priceNow=Math.round(priceNow*100)/100;
                  dis=Math.round(dis*100)/100;
                  disPrice=Math.round(disPrice*100)/100;
                  sumPrice=Math.round(sumPrice*100)/100;
                  tax=Math.ceil(tax);
                  taxPrice=Math.round(taxPrice*100)/100;
                  sumPriceAll=Math.round(sumPriceAll*100)/100;
                  
                 //开始赋值
                
                
                 //1、现价
                  manager.updateCell('priceNow',priceNow, e.record);
                
                
                 //1、折扣率
                 manager.updateCell("dis",Math.ceil(dis*100),e.record);
                 
                 //2、金额
                 manager.updateCell('sumPrice',sumPrice, e.record);
                 
                 //3、税额
                 manager.updateCell('taxPrice',taxPrice, e.record);
                 
                 //4、价税合计
                 manager.updateCell('sumPriceAll',sumPriceAll, e.record);

            } //折扣金额改变---结束
            
            
             if (e.column.name == "sumPrice") //金额改变
            {
                //金额改变：【数量、折扣额、税率】 计算【折扣率、单价、税额、价税合计】   
               
                sumPrice=Number(e.value);

                //1、计算单价
                
                if(num!=0)
                {
                   
                    priceNow=(sumPrice-disPrice)/num;
                    
                   
                
                }
                else
                {
                  
                    priceNow=0;
                }
                
               
                
                 //2、计算折扣率=折扣额/(折扣额+金额)  
                 
                 //alert(sumPrice);
                 if((Number(disPrice)+Number(sumPrice))!=0)
                 {
                    dis=priceNow/price;
                 }
                 else
                 {
                    dis=0;
                 }
                 
                 //现价
                 
               
                 

                 //3、税额=数量*单价*税率/100
                 taxPrice=Number(sumPrice)*Number(tax)/100;
                
                
                 //4、价税合计=金额-折扣+税额
                 sumPriceAll=Number(sumPrice) + Number(taxPrice);
                 
                 
                 num=Math.round(num*100)/100;
                 price=Math.round(price*100)/100;
                  
                 priceNow=Math.round(priceNow*100)/100;
                  
                  dis=Math.round(dis*100)/100;
                  disPrice=Math.round(disPrice*100)/100;
                  sumPrice=Math.round(sumPrice*100)/100;
                  tax=Math.ceil(tax);
                  taxPrice=Math.round(taxPrice*100)/100;
                  sumPriceAll=Math.round(sumPriceAll*100)/100;
                  
                 //开始赋值
                
                
                
                 //1、单价
                 manager.updateCell("price",price,e.record);
                 
                 //2、折扣率
                 manager.updateCell('dis',Math.ceil(dis*100), e.record);
                
                
                 manager.updateCell("priceNow",priceNow,e.record);
                 
                 manager.updateCell('sumPrice',sumPrice, e.record);
                 
                 //3、税额
                 manager.updateCell('taxPrice',taxPrice, e.record);
                 
                 //4、价税合计
                 manager.updateCell('sumPriceAll',sumPriceAll, e.record);

            } //金额改变---结束
            
            
            if (e.column.name == "tax") //税率改变---开始、计算税额、价税合计
            {
               //manager.updateCell('Price', e.record.UnitPrice * e.record.Quantity, e.record); 
                tax=Number(e.value);
                
              
              
                 //3、税额=数量*单价*税率/100
                 taxPrice=Number(sumPrice)*Number(tax)/100;
                
                
                 //4、价税合计=金额-折扣+税额
                 var sumPriceAll=Number(sumPrice) + Number(taxPrice);
                 
                 
                 
                 num=Math.round(num*100)/100;
                  price=Math.round(price*100)/100;
                  
                  priceNow=Math.round(priceNow*100)/100;
                  
                  dis=Math.round(dis*100)/100;
                  disPrice=Math.round(disPrice*100)/100;
                  sumPrice=Math.round(sumPrice*100)/100;
                  tax=Math.ceil(tax);
                  taxPrice=Math.round(taxPrice*100)/100;
                  sumPriceAll=Math.round(sumPriceAll*100)/100;
                  
                 //开始赋值
               
                 
                 //3、税额
                 manager.updateCell('taxPrice',taxPrice, e.record);
                 
                 //4、价税合计
                 manager.updateCell('sumPriceAll',sumPriceAll, e.record);

            } //税率改变---结束
            
            
            if (e.column.name == "taxPrice") //税额改变---开始、计算税率、价税合计
            {
               //manager.updateCell('Price', e.record.UnitPrice * e.record.Quantity, e.record); 
                taxPrice=Number(e.value);
                
              
              
                 //3、税额=数量*单价*税率/100
                 
                 if(sumPrice>0)
                 {
                     tax=Number(taxPrice)/Number(sumPrice);
                 }
                 else
                 {
                    tax=0;
                 }
                
                 //4、价税合计=金额-折扣+税额
                 
                 sumPriceAll=Number(sumPrice) + Number(taxPrice);
                 
                 
                 num=Math.round(num*100)/100;
                  price=Math.round(price*100)/100;
                  priceNow=Math.round(priceNow*100)/100;
                  dis=Math.round(dis*100)/100;
                  disPrice=Math.round(disPrice*100)/100;
                  sumPrice=Math.round(sumPrice*100)/100;
                  tax=Math.ceil(tax);
                  taxPrice=Math.round(taxPrice*100)/100;
                  sumPriceAll=Math.round(sumPriceAll*100)/100;
                  
                 //开始赋值
               
                 
                 //3、税率
                 manager.updateCell('tax',Math.ceil(tax*100), e.record);
                 
                 //4、价税合计
                 manager.updateCell('sumPriceAll',sumPriceAll, e.record);

            } //税额改变---结束
            
            
            if (e.column.name == "sumPriceAll") //价税合计改变---开始、计算单价、折扣率、金额、税额
            {
               //价税合计改变：【数量、折扣额、税率】 计算【单价、折扣率、金额、税额】
                sumPriceAll=Number(e.value);
                
                 //1、计算单价 
                 if(num>0)
                 {
                     price=Number(sumPriceAll+disPrice)/(Number(num)*(1+tax/100));
                 }
                 else
                 {
                    price=0;
                 }
                 
                 //2、折扣率=折扣额/单价*数量
                 if(sumPriceAll!=0)
                 {
                    dis=Math.ceil(disPrice/(price*num)*100);
                 }
                 else
                 {
                    dis=0;
                 }
                 
                 
                 //现价
                 
                 priceNow=price*dis/100;
                 
                 
                 //3、金额=单价*数量-折扣金额
                 
                 sumPrice=Number(price*num) - Number(disPrice);
                 
                 //4、税额=金额*税率
                 
                 taxPrice=sumPrice*tax/100;
                 
                 
                  num=Math.round(num*100)/100;
                  price=Math.round(price*100)/100;
                  priceNow=Math.round(priceNow*100)/100;
                  dis=Math.round(dis*100)/100;
                  disPrice=Math.round(disPrice*100)/100;
                  sumPrice=Math.round(sumPrice*100)/100;
                  tax=Math.ceil(tax);
                  taxPrice=Math.round(taxPrice*100)/100;
                  sumPriceAll=Math.round(sumPriceAll*100)/100;
                  
                  
                 //开始赋值
               
                 
                 //1、单价
                 manager.updateCell('price',price, e.record);
                 
                 //2、折扣率
                 manager.updateCell('dis',dis, e.record);
   
                 manager.updateCell('priceNow',priceNow, e.record);
   
                 //4、金额
                 manager.updateCell('sumPrice',sumPrice, e.record);
                 
                 //4、税额
                 manager.updateCell('taxPrice',taxPrice, e.record);
                 
                 

            } //价税合计改变---结束
           
          
          
        //最后改变汇总行的值

       updateTotalSum();
        
          
        }
     
      //2015.09.03.自动调整汇总金额  http://blog.okbase.net/qdjianghao/archive/55217.html
      
      function updateTotalSum()
      {
      
           var priceDisItem=0,sumPriceItem=0;//
           
            var data = manager.getData();//getData
           //先删掉空白行   
            //1、先删掉空白行
              for(var i=data.length-1;i>=0;i--)
             {
                 if(data[i].goodsId=="" || data[i].goodsName=="")
                 {
                     data.splice(i,1);
                    
                 }
                
             }

           for(var i=0;i<data.length;i++)
           {
               
               priceDisItem+=Number(data[i].num)*(data[i].price-data[i].priceNow);  
               sumPriceItem+=Number(data[i].num)*Number(data[i].priceNow); 
               
           }
           
         
          // alert(priceDis);

            $("#totalPriceDis").html(formatCurrency(priceDisItem));
                   
            $("#totalPriceAll").html(formatCurrency(sumPriceItem));
            
            
            //开始计算下面的Textbox
          
           
            var dis=Number($("#txtDis").val());
            if($("#txtDis").val()=="")
            {
                dis=0; 
            }
            
           
          
            //运费
            var sumPriceSend=Number($("#txtSumPriceSend").val());
            
            //预收定金
            var sumPricePayReady=Number($("#txtSumPricePayReady").val());
            
           
            
              //计算 ==折扣金额=Item总金额*折扣率、折后金额=总的-折扣金额、剩余尾款=折后金额+运费
             
          //  alert(dis);
           
             
           
           
             //折扣金额
            var disPriceAll=Number(sumPriceItem*dis/100);
            
          //  alert(disPriceAll);
            
            //折后金额
            var sumPriceAll=Number(sumPriceItem)-Number(disPriceAll);
           
          //  alert(sumPriceAll);
            
            
             //剩余尾款
            var sumPricePayNeed=Number(sumPriceAll)+Number(sumPriceSend)-Number(sumPricePayReady);
            
           //  alert(sumPricePayNeed);
            
            //赋值
            
            
             //折扣率
             $("#txtDis").val(dis);
             
             //折扣金额
             
              $("#txtDisPrice").val(disPriceAll)
             
             
             //折后金额
             $("#txtSumPrice").val(sumPriceAll);
              
             //剩余尾款
             $("#txtSumPricePayNeed").val(sumPricePayNeed);
             
          
            
      }
      
      
      function updateTotalSumSub()
      {

           var sumPricePayReady=0,sumPriceAll=0,sumPriceSend=0;//
           
           sumPriceAll=Number($("#txtSumPrice").val());
           
           sumPriceSend=Number($("#txtSumPriceSend").val());
           
           var data = managersub.getData();//getData
           //先删掉空白行   
            //1、先删掉空白行
              for(var i=data.length-1;i>=0;i--)
             {
                 if(data[i].payTypeId=="" || data[i].payTypeName=="")
                 {
                     data.splice(i,1);
                    
                 }
                
             }

           for(var i=0;i<data.length;i++)
           {
               sumPricePayReady+=Number(data[i].payPrice);                
           }
           
         
         
            //开始计算下面的Textbox
              //计算 ==折扣金额=Item总金额*折扣率、折后金额=总的-折扣金额、剩余尾款=折后金额+运费
             
          //  alert(dis);
 
             //剩余尾款=折后金额+运费-预收定金
            var sumPricePayNeed=Number(sumPriceAll)+Number(sumPriceSend)-Number(sumPricePayReady);
            
           //  alert(sumPricePayNeed);
            
            //赋值
            
            //预收定金
             $("#txtSumPricePayReady").val(sumPricePayReady);
              
             //剩余尾款
             $("#txtSumPricePayNeed").val(sumPricePayNeed);

      }
      
      //
  
        //只允许编辑已经添加商品的行
        function f_onBeforeEdit(e)
        { 

           
             
        }
        //限制折扣、税率范围
        
        function f_onBeforeSubmitEdit(e)
        { 
            if (e.column.name == "dis")
            {
                if (e.value < 0 || e.value > 100) return false;
            }
            
            if (e.column.name == "tax")
            {
                if (e.value < 0 || e.value > 100) return false;
            }
            
            
            return true;
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
                    num : "",
                    price : "",
                    dis : "",
                    disPrice : "",
                    sumPrice : "",

                    tax : "",
                    taxPrice : "",
                    sumPriceAll : "",
                    ckId : "",
                    ckName : "",

                    remarks : ""
            });                        updateTotalSum();
             
        } 
        
        
        function searchRow()
        {
            var row = manager.getSelectedRow();
            if (!row) { alert('请选择行'); return; }
            
            if(row.goodsId=="" || row.goodsName=="")
            {
                $.ligerDialog.warn("先选择商品！");
                return;
            }
            else
            {
                $.ligerDialog.warn("商品ID为"+row.goodsId+"商品名称："+row.goodsName);
            }

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


     var wlId= $("#ddlClientList").ligerGetComboBoxManager().getValue(); //获取Select选择的Value
      var clientId= $("#hfId").val();
      
      alert(clientId);
      
      return;
     
        
     if(wlId=="")
     {
         $.ligerDialog.warn("请选择客户！");
          return;
     }
        
     
      var bizDate=$("#txtBizDate").val();
      if(bizDate=="")
      {
          $.ligerDialog.warn("请输入订单日期！");
          return;
          
      }

           
      var sendDate= $("#txtSendDate").val();

      if(sendDate=="")
      {
          $.ligerDialog.warn("请输入交货日期！");
          return;
          
      }
      
     var bizId=$("#ddlYWYList").val();
     
     var sendTypeId=$("#ddlSendWayList").val();
     
     var dis=0,sumPriceDis=0,sumPriceAll=0,sumPriceWY=0,sendPrice=0,sumPricePayReady=0,sumPricePayNeed=0;
     
     if($("#txtDis").val()!="")
     {
         dis=Number($("#txtDis").val());
     }
     
     if($("#txtDisPrice").val()!="")
     {
         sumPriceDis=Number($("#txtDisPrice").val());
     }
     
     if($("#txtSumPrice").val()!="")
     {
         sumPriceAll=Number($("#txtSumPrice").val());
     }
     
     if($("#txtSumPriceWY").val()!="")
     {
         sumPriceWY=Number($("#txtSumPriceWY").val());
     }
     
     if($("#txtSumPriceSend").val()!="")
     {
         sendPrice=Number($("#txtSumPriceSend").val());
     }
     
     if($("#txtSumPricePayReady").val()!="")
     {
         sumPricePayReady=Number($("#txtSumPricePayReady").val());
     }
     
      if($("#txtSumPricePayNeed").val()!="")
     {
         sumPricePayNeed=Number($("#txtSumPricePayNeed").val());
     }
     
     
      
     var remarks=$("#txtRemarks").val();
         
       
        
     var headJson={id:getUrlParam("id"),wlId:wlId,sendDate:sendDate,dis:dis,sumPriceDis:sumPriceDis,sumPriceAll:sumPriceAll,sumPriceWY:sumPriceWY,sumPricePayReady:sumPricePayReady,sumPricePayNeed:sumPricePayNeed,sendTypeId:sendTypeId,sendPrice:sendPrice,remarks:remarks,bizId:bizId,bizDate:bizDate};
      
     //alert(JSON.stringify(headJson));
   
    // return;
   
     var data = manager.getData();
     var datasub = managersub.getData();
     
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
       
        
     }
     
     
     //收款记录
     
      //1、先删掉空白行
      for(var i=datasub.length-1;i>=0;i--)
     {
         if(datasub[i].payTypeId=="" || datasub[i].payTypeName=="")
         {
             datasub.splice(i,1);
            
         }
        
     }
     
     //2、判断付款金额是否都输入了。
      for(var i=0;i<datasub.length;i++)
     {
         if(Number(datasub[i].payPrice)<=0 && datasub[i].payTypeName!="")
         {
             
             $.ligerDialog.warn("请输入第"+(i+1)+"行的收款金额！");
             
             return;
             alert("我就不执行了！");
         }
         
         //非现金的要输入号码
         
         if(datasub[i].payTypeName!="现金" && datasub[i].payNumber=="")
         {
             
             $.ligerDialog.warn("请输入第"+(i+1)+"行的"+datasub[i].payTypeName+"号码！");
             
             return;
             alert("我就不执行了！");
         }
         
       
        
     }
     
          
     //收款记录结束

      
            
    
    
        
        var dataNew = [];
        dataNew.push(headJson);
        
   
        
        var list=JSON.stringify(headJson);//返序列化成字符串、表头
        
        
        var goodsList=[];

        list=list.substring(0,list.length-1);//去掉最后一个花括号
        
        list+=",\"Rows\":";
        list+=JSON.stringify(data);//插入商品信息
        
        list+=",\"RowsBill\":";   
        list+=JSON.stringify(datasub);//插入收款信息     
        
        list+="}";
       
      
        
        var postData=JSON.parse(list);//最终的json
        
//        alert(postData.Rows[0].id);
//        
//        alert(postData.bizDate);
//        
//        alert(postData.Rows[0].goodsName);

      //  alert(JSON.stringify(postData));
        
       // return;
        

//       $("#txtRemarks").val(JSON.stringify(postData));
       
    
         
         $.ajax({
            type: "POST",
            url: '../handler/SalesOrderListEdit.ashx',
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










//以下是收款列表

 function deleteRowSub()
        { 
            if(managersub.rows.length==1)
            {
                $.ligerDialog.warn('至少保留一行！')
                
            }
            else
            {
               managersub.deleteSelectedRow();
            }
        }
    
   function addNewRowSub()
        {
             var gridData = managersub.getData();
             var rowNum=gridData.length;
             
           
             managersub.addRow({ 
                id: rowNum+1,
                    id : rowNum+1,
                   
                    payTypeId : "",
                    payTypeName : "",
                    payNumber : "",
                    payPrice : ""
            });                      
             
        }    
        

  var managersub;
        $(function ()
        {
         
          
          
            window['gsub'] = 
            managersub = $("#maingridsub").ligerGrid({
                columns: [
                
               { display: '', isSort: false, width: 40,align:'center',frozen:true, render: function (rowdata, rowindex, value)
                 {
                    var h = "";
                    if (!rowdata._editing)
                    {
                        h += "<a href='javascript:addNewRowSub()' title='新增行' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                        h += "<a href='javascript:deleteRowSub()' title='删除行' style='float:right;'><div class='ui-icon ui-icon-trash'></div></a> "; 
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
                
                 { display: '收款方式', name: 'payTypeId', width: 120, isSort: false,textField:'payTypeName',
                    editor: { type: 'select',
                              url:"../baseSet/PayTypeList.aspx?Action=GetDDLList&r=" + Math.random(), 
                              valueField: 'typeId',textField:'typeName'},
                              
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
                { display: '号码', name: 'payNumber', width: 150, align: 'left',type:'text',editor: { type: 'text' } },
                
                { display: '金额', name: 'payPrice', width: 120, align: 'right',type:'text',editor: { type: 'text' },
                
                     totalSummary:
                        {
                            align: 'right',   //汇总单元格内容对齐方式:left/center/right 
                            type: 'sum',
                            render: function (e) 
                            {  //汇总渲染器，返回html加载到单元格
                             //e 汇总Object(包括sum,max,min,avg,count) 
                                return Math.round(e.sum*100)/100;
                            }
                        }
                
                
                 }
                
              
                ], width: '320', pageSizeOptions: [5, 10, 15, 20], height: '185',
                url: 'SalesOrderListEdit.aspx?Action=GetDataSub&id='+getUrlParam("id"),//获取参数
               rownumbers:true,//显示序号
               frozenRownumbers:true,//行序号是否在固定列中
                dataAction: 'local',//本地排序
                usePager:false,
                alternatingRow: false,
                
                totalSummary:false,
                enabledEdit: true, //控制能否编辑的
            
                 onAfterShowData:f_onAfterShowDataSub,//显示完整数据之后、、用于更新汇总和下面的计算
               
                
                onAfterEdit: f_onAfterEditSub //更新单元格后的操作
                
                
            }
            );
        });


function f_onAfterShowDataSub()
{
    updateTotalSumSub();
}

function f_onAfterEditSub()
{
   updateTotalSumSub();
}

function calcDis(values)
{
   
           //分录汇总金额
           var sumPriceItem=Number($("#totalPriceAll").html());
          
         
          
          var dis= Number(values);
           
           if(  $("#txtDis").val()=="")
           {
              dis=0;
           }
           
           
           
             //运费
            var sumPriceSend=Number($("#txtSumPriceSend").val());
            
            //预收定金
            var sumPricePayReady=Number($("#txtSumPricePayReady").val());
            
           
   
          
            
            //折后金额
            var sumPriceAll=Number(sumPriceItem)*dis/100;
            
            
             //折扣金额
            var disPriceAll=Number(sumPriceItem)-Number(sumPriceAll);
            
           
           
        
            
             //剩余尾款
            var sumPricePayNeed=Number(sumPriceAll)+Number(sumPriceSend);
            
           //  alert(sumPricePayNeed);
            
            //赋值
            
            
             //折扣率
           
             
             //折扣金额
             
              $("#txtDisPrice").val(disPriceAll)
             
             
             //折后金额
             $("#txtSumPrice").val(sumPriceAll);
              
             //剩余尾款
             $("#txtSumPricePayNeed").val(sumPricePayNeed);
             
}


function calcDisPrice(values)
{
   
           //分录汇总金额
           var sumPriceItem=Number($("#totalPriceAll").html());
          
           //折扣金额
            var disPriceAll=Number(values);
            
           
        
           
           var dis=0;
           
           dis=(sumPriceItem-disPriceAll)/sumPriceItem*100;
            
           dis=Math.round(dis*100)/100;
           
             //运费
            var sumPriceSend=Number($("#txtSumPriceSend").val());
            
            //预收定金
            var sumPricePayReady=Number($("#txtSumPricePayReady").val());

            //折后金额
            var sumPriceAll=Number(sumPriceItem)-Number(disPriceAll);
           
        
            
             //剩余尾款
            var sumPricePayNeed=Number(sumPriceAll)+Number(sumPriceSend);
            
           //  alert(sumPricePayNeed);
            
            //赋值
            
            
             //折扣率
           
             
             //折扣金额
             
              $("#txtDis").val(dis);
             
             
             //折后金额
             $("#txtSumPrice").val(sumPriceAll);
              
             //剩余尾款
             $("#txtSumPricePayNeed").val(sumPricePayNeed);
             
}


function calcSumPrice(values)
{
   
           //分录汇总金额
           var sumPriceItem=Number($("#totalPriceAll").html());
          
           //折后金额
            var sumPriceAll=Number(values);
            
           
        
           
           var dis=0;
           
           dis=sumPriceAll/sumPriceItem*100;
            
           dis=Math.round(dis*100)/100;
           
             //运费
            var sumPriceSend=Number($("#txtSumPriceSend").val());
            
            //预收定金
            var sumPricePayReady=Number($("#txtSumPricePayReady").val());

            //折扣金额
            var sumPriceDis=Number(sumPriceItem)-Number(sumPriceAll);
           
        
            
             //剩余尾款
            var sumPricePayNeed=Number(sumPriceAll)+Number(sumPriceSend);
            
           //  alert(sumPricePayNeed);
            
            //赋值
            
            
             //折扣率
           
             
             //折扣金额
             
              $("#txtDis").val(dis);
             
             
             //折后金额
             $("#txtDisPrice").val(sumPriceDis);
              
             //剩余尾款
             $("#txtSumPricePayNeed").val(sumPricePayNeed);
             
}


function calcSumSendPrice(values)
{
   
           //分录汇总金额
           var sumPriceItem=Number($("#totalPriceAll").html());
          
           //运费
            var sumPriceSend=Number(values);

       
             //折后金额
            var sumPriceAll=Number($("#txtSumPrice").val());
            
         
             //预收定金
            var sumPricePayReady=Number($("#txtSumPricePayReady").val());

         
            
             //剩余尾款
            var sumPricePayNeed=Number(sumPriceAll)+Number(sumPriceSend);
            
           //  alert(sumPricePayNeed);
            
            //赋值

             //剩余尾款
             $("#txtSumPricePayNeed").val(sumPricePayNeed);
             
}


function getUrlParam(name)
{
   var reg = new RegExp("(^|&)"+ name +"=([^&]*)(&|$)");

   var r = window.location.search.substr(1).match(reg);

   if (r!=null) return unescape(r[2]); return null;
}


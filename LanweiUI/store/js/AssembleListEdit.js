
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
        
        
      
  //新样式引入行
        
        var itemType="main";
        
        function f_selectContact(type)
        {
            if(type==0)
            {
               itemType="main";
            }
            if(type==1)
            {
               itemType="sub";
            }
            
            
            $.ligerDialog.open({ title: '选择商品', name:'winselector',width: 840, height:540, url: '../baseSet/GoodsListSelect.aspx', buttons: [
                { text: '确定', onclick: f_selectContactOK },
                { text: '关闭', onclick: f_selectContactCancel }
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
            
          
            
            if(itemType=="main")
            {
                f_onGoodsChanged(data);
            }
            if(itemType=="sub")
            {
                f_onGoodsChangedSub(data);
            }
                       
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
          
          
          
            window['g'] = 
            manager = $("#maingrid").ligerGrid({
                columns: [
                
               
               
               { display: '', isSort: false, width: 60,align:'center',frozen:true, render: function (rowdata, rowindex, value)
                 {
                    var h = "";
                    if (!rowdata._editing)
                    {
                        h += "<a href='javascript:f_selectContact(0)' title='选择商品'><div class='ui-icon ui-icon-search' style='margin:0 auto;'></div></a> ";
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
                
                { display: '规格', name: 'spec',width: 80, align: 'center' },
                
                { display: '单位', name: 'unitName',width: 80, align: 'center' },
                { display: '入库数量', name: 'num', width: 80, type: 'float', align: 'right',editor: { type: 'float' }  },
                
                { display: '入库单价', name: 'price', width: 70, type: 'float', align: 'right', editor: { type: 'float', precision: 4 }
                
                },

                { display: '金额', name: 'sumPrice', width: 80, type: 'float', align: 'right',editor: { type: 'float' }  },
             
                { display: '入库仓库', name: 'ckId', width: 100, isSort: false,textField:'ckName',
                    editor: { type: 'select',
                              url:"../baseSet/CangkuList.aspx?Action=GetDDLList&r=" + Math.random(), 
                              valueField: 'ckId',textField:'ckName'}

                },
                
                { display: '备注', name: 'remarks', width: 150, align: 'left',type:'text',editor: { type: 'text' } }
                ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '55',
                url: 'AssembleListEdit.aspx?Action=GetData&id='+getUrlParam("id"),
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
 
 
       var managersub;
        $(function ()
        {
         
          
          
            window['gsub'] = 
            managersub = $("#maingridsub").ligerGrid({
                columns: [
                
                { display: '', isSort: false, width: 60,align:'center',frozen:true, render: function (rowdata, rowindex, value)
                 {
                    var h = "";
                    if (!rowdata._editing)
                    {
                        h += "<a href='javascript:addNewRow()' title='新增行' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                        h += "<a href='javascript:deleteRow()' title='删除行' style='float:left;'><div class='ui-icon ui-icon-trash'></div></a> "; 
                        h += "<a href='javascript:f_selectContact(1)' title='选商品' style='float:left;'><div class='ui-icon ui-icon-search'></div></a> ";
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
                
                { display: '规格', name: 'spec',width: 80, align: 'center' },
                
                { display: '单位', name: 'unitName',width: 80, align: 'center' },
                { display: '出库数量', name: 'num', width: 80, type: 'float', align: 'right',editor: { type: 'float' },
                
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
                
                { display: '出库单价', name: 'price', width: 70, type: 'float', align: 'right', editor: { type: 'float', precision: 4 }
                
                },

             
                
                
                { display: '金额', name: 'sumPrice', width: 80, type: 'float', align: 'right',editor: { type: 'float' },
                
                 
                
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
                
                
             
                { display: '出库仓库', name: 'ckId', width: 100, isSort: false,textField:'ckName',
                    editor: { type: 'select',
                              url:"../baseSet/CangkuList.aspx?Action=GetDDLList&r=" + Math.random(), 
                              valueField: 'ckId',textField:'ckName'}

                },
                
                { display: '备注', name: 'remarks', width: 150, align: 'left',type:'text',editor: { type: 'text' } }
                ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '320',
                url: 'AssembleListEdit.aspx?Action=GetDataSub&id='+getUrlParam("id"),
               rownumbers:true,//显示序号
               frozenRownumbers:true,//行序号是否在固定列中
                dataAction: 'local',//本地排序
                usePager:false,
                alternatingRow: false,
                
                totalSummary:true,
                enabledEdit: true, //控制能否编辑的
             
                
                onAfterEdit: f_onAfterEditSub //更新单元格后的操作
            }
            );
        });
 
 
      function updateTotal()
      {
      
          
            var data = managersub.getData();//getData
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
 
        var rowNumber=9;
 
        function f_totalRender(data, currentPageData)
        {
             //return "总仓库数量:"+data.sumPriceAll; 
        }

        function setCangku()
        {
        
            $.ligerDialog.open({ target: $("#target1") });

            
            // $.ligerDialog.open({ url: '../../welcome.htm', height: 250,width:null, buttons: [ { text: '确定', onclick: function (item, dialog) { alert(item.text); } }, { text: '取消', onclick: function (item, dialog) { dialog.close(); } } ] });
        }
        
        function selectCangku()
        {
              var ckName=$("#ddlCangkuList").find("option:selected").text();  //获取Select选择的Text
              var ckId=$("#ddlCangkuList").val();  //获取Select选择的Value
              
              alert(ckName);
              alert(ckId);
              
               var grid = liger.get("maingrid");
               var data = manager.getData();
               
               alert(data.length);
               
               
               for(var i=0;i<data.length;i++)
               {
                   alert(data[i].goodsId);
                   
                   grid.updateCell("ckId",ckId,i);
                   grid.updateCell("ckName",ckName,i);
                   
               }
                            
               
               $(".l-dialog,.l-window-mask").remove();
               
//             var dialog = frameElement.dialog;
//            
//             dialog.close(); //关闭dialog

             $.ligerDialog.close(); //关闭dialog
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
                goodsName: selected.names,
                unitName: selected.unitName,
                num:1,
                price: selected.priceCost,
                spec:selected.spec,
                sumPrice:selected.priceCost,
                ckId:selected.ckId,
                ckName:selected.ckName,
                remarks:""
                
            });

           

        }
        
           
    
    
        //商品 改变事件：获取单位、单价等信息
        function f_onGoodsChangedSub(e)
        { 
           
         
            if (!e || !e.length) return;
            
            //1、先更新当前行的后续数据
            
            var grid = liger.get("maingridsub");

             var selected =e[0];// e.data[0]; 
            
          // alert(selected.names);
          
           var selectedRow = managersub.getSelected();
           
            grid.updateRow(selectedRow, {
                
                goodsId: selected.id,
                goodsName: selected.names,
                unitName: selected.unitName,
                num:1,
                price: selected.priceCost,
                spec:selected.spec,
                sumPrice:selected.priceCost,
                ckId:selected.ckId,
                ckName:selected.ckName,
                remarks:""
                
            });

            if(e.length>1) //如果有多行的、先删除空白行，然后插入下面
            {

             var data = managersub.getData();
             for(var i=data.length-1;i>=0;i--)
             {
                 if(data[i].goodsId==0 || data[i].goodsName=="")
                 {
                     managersub.deleteRow(i);
                    // alert("删除行："+i);
                 }
                
             }

               for(var i=1;i<e.length;i++)
               {
                   grid.addRow({
                        id:rowNumber,
                        goodsId: e[i].id,
                        goodsName:e[i].names,
                        unitName:e[i].unitName,
                        num:1,
                        price : e[i].priceCost,
                        spec:e[i].spec,
                        sumPrice:e[i].priceCost,
                   
                        ckId:e[i].ckId,
                        ckName:e[i].ckName,
                        remarks:""
                        
                       });
                    
                    rowNumber=rowNumber+1;         
                 
               }
  
           }
           
           updateTotal();

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
            var num,price,sumPrice;
            
            
            num=Number(e.record.num);
            
            price=Number(e.record.price);
                
       
            sumPrice=Number(e.record.sumPrice);    
            
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

                  num=Math.round(num*100)/100;
                  price=Math.round(price*100)/100;
                
                  sumPrice=Math.round(sumPrice*100)/100;
               
                 
                  
                  
                 //开始赋值
                 
                 manager.updateCell("num",num, e.record);
                
             
                 
                 //2、金额
                 manager.updateCell('sumPrice',sumPrice, e.record);
                 
            
                 
               
                 
                
                 

            } //数量改变---结束
            
            if (e.column.name == "price") //单价改变---开始、计算金额、折扣额、税额、价税合计
            {
               //单价改变：【数量、折扣率、税率】 计算【折扣额、金额、税额、价税合计】; 
                price=Number(e.value);
                
              
                 
                  //2、金额=数量*单价-折扣额
                 sumPrice=Number(num)* Number(price);
                                    
            
          
                 
                  num=Math.round(num*100)/100;
                  price=Math.round(price*100)/100;
               
                  sumPrice=Math.round(sumPrice*100)/100;
             
                  
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
                
              
                 
                 num=Math.round(num*100)/100;
                  price=Math.round(price*100)/100;
               
                  sumPrice=Math.round(sumPrice*100)/100;
             
                  
                 //开始赋值
                
                 //1、单价
                 manager.updateCell("price",price,e.record);
                 
                 //2、折扣率
         
                 
                 manager.updateCell('sumPrice',sumPrice, e.record);
                 
             

            } //金额改变---结束
            
          
          
           
          
          
        //最后改变汇总行的值
           
           
         
         
        }
        
        
       
     
       
         //编辑后事件 
        function f_onAfterEditSub(e)
        {
            var num,price,sumPrice;
            
            
            num=Number(e.record.num);
            
            price=Number(e.record.price);
                
       
            sumPrice=Number(e.record.sumPrice);    
            
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

                  num=Math.round(num*100)/100;
                  price=Math.round(price*100)/100;
                
                  sumPrice=Math.round(sumPrice*100)/100;
               
                 
                  
                  
                 //开始赋值
                 
                 managersub.updateCell("num",num, e.record);
                
             
                 
                 //2、金额
                 managersub.updateCell('sumPrice',sumPrice, e.record);
                 
            
                 
               
                 
                
                 

            } //数量改变---结束
            
            if (e.column.name == "price") //单价改变---开始、计算金额、折扣额、税额、价税合计
            {
               //单价改变：【数量、折扣率、税率】 计算【折扣额、金额、税额、价税合计】; 
                price=Number(e.value);
                
              
                 
                  //2、金额=数量*单价-折扣额
                 sumPrice=Number(num)* Number(price);
                                    
            
          
                 
                  num=Math.round(num*100)/100;
                  price=Math.round(price*100)/100;
               
                  sumPrice=Math.round(sumPrice*100)/100;
             
                  
                 //开始赋值
                
                 //1、折扣额
                
                 managersub.updateCell("price",price, e.record);
                
            
                 //2、金额
                 managersub.updateCell('sumPrice',sumPrice, e.record);
                 
           

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
                
              
                 
                 num=Math.round(num*100)/100;
                  price=Math.round(price*100)/100;
               
                  sumPrice=Math.round(sumPrice*100)/100;
             
                  
                 //开始赋值
                
                 //1、单价
                 managersub.updateCell("price",price,e.record);
                 
                 //2、折扣率
         
                 
                 managersub.updateCell('sumPrice',sumPrice, e.record);
                 
             

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
           
            if(managersub.rows.length==1)
            {
                $.ligerDialog.warn('至少保留一行！')
                
            }
            else
            {
               managersub.deleteSelectedRow();
            }
            
        }
        var newrowid = 100;
        
        function addNewRow()
        {
             var gridData = managersub.getData();
             var rowNum=gridData.length;
             
           
             managersub.addRow({ 
                id: rowNum+1,
                    id : rowNum+1,
                    goodsId :"",
                    goodsName : "",
                    unitName : "",
                    num : "",
                    spec:"",
                    sumPrice : "",

              
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
          $.ligerDialog.warn('请选择要组装的商品！');
          
          return;
          alert("我就不执行了！");
     }     
     
     
     
     //3、判断商品数量是否都输入了。
      for(var i=0;i<data.length;i++)
     {
         if(data[i].num<=0 || data[i].num=="" || data[i].num=="0" || data[i].num=="0.00")
         {
             
             $.ligerDialog.warn("请输入第"+(i+1)+"行的组装商品数量！");
             
             return;
             alert("我就不执行了！");
         }
         
         if(data[i].ckId==0 || data[i].ckId=="" || data[i].ckId=="0" || data[i].ckName=="")
         {
             
             $.ligerDialog.warn("请输入第"+(i+1)+"行组装后商品的仓库！");
             
             return;
             alert("我就不执行了！");
         }
       
        
     }
     
     var goodsId=data[0].goodsId;
     var num=data[0].num;
     var price=data[0].price;
     var ckId=data[0].ckId;
     var remarksItem=data[0].remarks;
     
//     alert("ID："+data[0].goodsId);
//     alert("数量："+data[0].num);
//     alert("单价："+data[0].price);
//     alert("仓库："+data[0].ckId);
     
     //要拆分的商品结束
     
     
     
     //拆分后的商品开始
     
      var datasub = managersub.getData();
     
     
    //1、先删掉空白行
      for(var i=datasub.length-1;i>=0;i--)
     {
         if(datasub[i].goodsId==0 || datasub[i].goodsName=="")
         {
             datasub.splice(i,1);
            
         }
        
     }
     
     
     //2、判断是否选择商品
      if(datasub.length==0)
     {
          $.ligerDialog.warn('请选择被组装的商品！');
          
          return;
          alert("我就不执行了！");
     }     
     
     
     
     //3、判断商品数量是否都输入了。
      for(var i=0;i<datasub.length;i++)
     {
         if(datasub[i].num<=0 || datasub[i].num=="" || datasub[i].num=="0" || datasub[i].num=="0.00")
         {
             
             $.ligerDialog.warn("请输入第"+(i+1)+"行被组装的商品数量！");
             
             return;
             alert("我就不执行了！");
         }
         
          if(datasub[i].ckId==0 || datasub[i].ckId=="" || datasub[i].ckId=="0" || datasub[i].ckName=="")
         {
             
             $.ligerDialog.warn("请输入第"+(i+1)+"行被组装商品的仓库！");
             
             return;
             alert("我就不执行了！");
         }
       
        
     }
     
     
     
     //
     
      
     
      var bizDate=$("#txtBizDate").val();
      if(bizDate=="")
      {
          $.ligerDialog.warn("请输组装日期！");
          return;
          
      }

         
            
            var remarks=$("#txtRemarks").val();
            
            var fee = $("#txtFee").val() == "" ? 0 : $("#txtFee").val();

      
        
           var headJson={id:getUrlParam("id"),fee:fee,bizDate:bizDate,remarks:remarks,goodsId:goodsId,num:num,price:price,ckId:ckId,remarksItem:remarksItem};
      
    
        
        var dataNew = [];
        dataNew.push(headJson);
        
   
        
        var list=JSON.stringify(headJson);
        
        
        var goodsList=[];
        
        
   
        
        list=list.substring(0,list.length-1);//去掉最后一个花括号
        
        list+=",\"Rows\":";
        list+=JSON.stringify(datasub);      
        list+="}";
       
      
        
        var postData=JSON.parse(list);//最终的json
        
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
            url: 'ashx/AssembleListEdit.ashx',
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


function getUrlParam(name)
{
   var reg = new RegExp("(^|&)"+ name +"=([^&]*)(&|$)");

   var r = window.location.search.substr(1).match(reg);

   if (r!=null) return unescape(r[2]); return null;
}


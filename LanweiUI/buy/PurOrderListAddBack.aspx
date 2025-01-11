<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PurOrderListAddBack.aspx.cs" Inherits="LanweiUI.buy.PurOrderListAddBack" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>新增采购订单</title>
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>  
    
    <script src="../lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
     <script src="../lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
 <script src="../lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script> 
 <script src="../lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../lib/jquery-validation/jquery.validate.min.js" type="text/javascript"></script> 
    
    <script src="../lib/json2.js" type="text/javascript"></script>
   <!---->
    
    
   <script src="../jsData/CustomersData.js" type="text/javascript"></script>
   
 
 
   
    <script src="../lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script><!--日期选择-->
   
   
    <script src="../lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerPopupEdit.js" type="text/javascript"></script>
      
     
    <script src="../lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    
    
    <script src="../lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
   
    <script src="../lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    
    <script src="../lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script> <!--弹框-->
    <script src="../lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script><!--弹框-->
   
     
    <script src="../lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../lib/jquery-validation/jquery.validate.min.js" type="text/javascript"></script> 
    <script src="../lib/jquery-validation/jquery.metadata.js" type="text/javascript"></script>
    <script src="../lib/jquery-validation/messages_cn.js" type="text/javascript"></script>
   
    <script src="../lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script><!--GridView可拖动表头-->
  
  
  <!--供应商下拉列表、弹框实时新增-->
  <script type="text/javascript">
 
    var dialog = null, combobox;
    $(function ()
    { 
        combobox = $("#ddlVenderList").ligerComboBox({
          
            url: '../baseSet/VenderList.aspx?Action=GetDDLList',
            valueField : 'id',
            textField: 'names',
            emptyText: '（空）',            //空行
            addRowButton: '新增',           //新增按钮
            addRowButtonClick: function ()  //新增事件
            {
                combobox.clear();
                dialog = $.ligerDialog.open({
                    height: 350,
                    width: 600,
                    title: '新增供应商',
                    url: '../baseSet/VenderListAdd.aspx?id=0',
                    showMax: false,
                    showToggle: true,
                    showMin: false,
                    isResize: true,
                    slide: false,
                    data: {
                        callback: afterSave
                    }
                });
            }
        });
    });
    function afterSave(data)
    {
        dialog.close(); 
        combobox.addItem(data); 
        combobox.setValue(data.id, data.text);
    }
     
</script>
  
  
  <!--提示框-->
   <script type="text/javascript">
         $(function() {
         });
         function f_alert(type,contents)
         {
             $.ligerDialog.alert(contents, '提示', type);
         }
         function f_alert2(type)
         {
             switch (type)
             {
                 case "success":
                     $.ligerDialog.success('提示内容');
                     break;
                 case "warn":
                     $.ligerDialog.warn('提示内容');
                     break;
                 case "question":
                     $.ligerDialog.question('提示内容');
                     break;
                 case "error":
                     $.ligerDialog.error('提示内容');
                     break;
                 case "confirm":
                     $.ligerDialog.confirm('提示内容', function (yes)
                     {
                         alert(yes);
                     });
                     break;
                 case "warning":
                     $.ligerDialog.warning('提示内容', function (type)
                     {
                         alert(type);
                     });
                     break;
                 case "prompt":
                     $.ligerDialog.prompt('提示内容', function (yes, value)
                     {
                         if (yes) alert(value);
                     }); 
                     break;
                 case "prompt2":
                     $.ligerDialog.prompt('提示内容','初始化值', function (yes, value)
                     {
                         if (yes) alert(value);
                     });
                     break;
                 case "prompt3":
                     $.ligerDialog.prompt('提示内容', true, function (yes, value)
                     {
                         if (yes) alert(value);
                     });
                     break;
                 case "prompt4":
                     $.ligerDialog.prompt('提示内容', '初始化多选框值', true, function (yes, value)
                     {
                         if (yes) alert(value);
                     });
                     break;
                 case "waitting":
                     $.ligerDialog.waitting('正在保存中,请稍候...');
                     setTimeout(function ()
                     {
                         $.ligerDialog.closeWaitting();
                     }, 2000);
                     break;
                 case "waitting2":
                     var manager = $.ligerDialog.waitting('正在保存中2,请稍候...');
                     setTimeout(function ()
                     {
                         manager.close();
                     }, 1000);
                     break;
             }
         }
     </script>
    
   
   
   <!--表单格式及验证-->
     
  

    <script type="text/javascript">

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
        
        
       // var cangkuList;
       var cangkulist={};
       
      
       
      // var jsonDataString= JSON.stringify(cangkuListData);//转换成字符串
      // alert(jsonDataString);//DepartmentData
        
           $.ajax({
                            url: "PurOrderListAdd.aspx?Action=GetData",//List 转化为json（URL）
                            cache: false,
                            type: "GET",
                            dataType: "json",
                            //async:false, 
                            success: function(jsonData) {
                              
                               var jsonDataString= JSON.stringify(jsonData);//转换成字符串
                            
                              // alert(jsonDataString);//现在弹框的是这句哈。
                           
                              // cangkuList=jsonData;countrys.newval.push(c);
                            //  cangkulist.newval.push(jsonData);
//                               alert(cangkuList.Rows.length);//这个也能读的
//                               alert(jsonData.Rows.length);//这个也能读的
//                               alert(jsonData.Rows[0].ckName);//这个也能读的

                            },
                            error: function() {
                                alert("您的网络异常，请联系网络管理员!");
                            }
                        });
                        
      //alert("我是仓库的值："+JSON.stringify(cangkulist));
      // alert(cangkuList.Rows.length);//这个也能读的
       
     //  alert(cangkuList);
                    

        var manager;
        $(function ()
        {
            window['g'] = 
            manager = $("#maingrid").ligerGrid({
                columns: [
                
                 
                 { display: '', isSort: false, width: 40,align:'center', render: function (rowdata, rowindex, value)
                 {
                    var h = "";
                    if (!rowdata._editing)
                    {
                        h += "<a href='javascript:addNewRow(" + rowindex + ")' title='新增行' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                        h += "<a href='javascript:deleteRow(" + rowindex + ")' title='删除行' style='float:right;'><div class='ui-icon ui-icon-trash'></div></a> "; 
                    }
                    else
                    {
                        h += "<a href='javascript:endEdit(" + rowindex + ")'>提交</a> ";
                        h += "<a href='javascript:cancelEdit(" + rowindex + ")'>取消</a> "; 
                    }
                    return h;
                }
                }
                ,
               
                { display: '商品名称', name: 'goodsName', width: 250, align: 'left',
                
                  
                   editor: 
                   { 
                   
                      type: 'popup', 
                      valueField: 'goodsName', 
                      //textField: 'goodsName', 
                      
                      grid:
                            {
                               
                                url: "../handler/grid.aspx?view=GetGoodsList",
                                checkbox:false,
                                dataAction: 'local',//本地排序
                                usePager:true,
                                columns: 
                                [
                                    { display: '商品编码',name: 'code', width: 100 }, 
                                    { display: '商品名称',name: 'goodsName', width: 200 },
                                    { display: '规格型号',name: 'spec', width: 100 },
                                    { display: '单位',name: 'unitName', width: 100 }
                                ]
                        }, 
                        condition: 
                        {
                            fields: 
                            [
                               { name: 'goodsName',type:'text',label:'关键字', width:200 }
                            ]
                        }, 
                        onSelected: f_onGoodsChanged //定义选择了以后的处理方法

                       
                    
                    },
                    
//                   render: function(item)
//                  {
//                      var names=item.goodsName.split(';');
//                    
//	                  if(names.length>0){ 
//	                  
//	                     return names[1]; 
//	            
//	                  }
//	                  else
//	                  {
//	             
//	             
//                         return goodsName;
//                      
//                      }
//                  } 
//                   ,
                 
                
                
                
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
                
             
                
                
                
                { display: '单位', name: 'unitName',width: 80, align: 'center' },
                { display: '数量', name: 'num', width: 80, type: 'float', align: 'right',editor: { type: 'float' },
                
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
                
                { display: '单价', name: 'price', width: 70, type: 'float', align: 'right', editor: { type: 'float' },
                
                  render: function(item)
                  {
                    return formatCurrency(item.priceCost);
                  } 
                
                
                },
                
                
                
                { display: '折扣%', name: 'dis', width: 60, type: 'float', align: 'center',editor: { type: 'float' } },
                { display: '折扣金额', name: 'disPrice', width: 70, type: 'float', align: 'right',editor: { type: 'float' } ,
                 
                     render: function(item)
                  {
                    return formatCurrency(item.disPrice);
                  } ,
                  
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
                
                
                
                { display: '金额', name: 'sumPrice', width: 80, type: 'float', align: 'right',editor: { type: 'float' },
                
                   render: function(item)
                  {
                    return formatCurrency(item.sumPrice);
                  } ,
                
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
                { display: '税额', name: 'taxPrice', width: 70, type: 'float', align: 'right',editor: { type: 'float' },
                
                    render: function(item)
                  {
                    return formatCurrency(item.taxPrice);
                  } ,
                  
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
                
                
                
                
                { display: '价税合计', name: 'sumPriceAll', width: 90, type: 'float', align: 'right',editor: { type: 'float' } ,
                
                
                    render: function(item)
                  {
                    return formatCurrency(item.sumPriceAll);
                  } ,
                
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
               
                { display: '仓库A', name: 'ckNames', width: 120, isSort: false,
                    editor: { type: 'select',
                              url:"../handler/ddlList.aspx?view=GetCangkuList&r=" + Math.random(), 
                              valueField: 'names', valueFieldID:'id',
                              textField: 'names' }
//                    , 
//                    render: function (item)
//                    {
//                        for (var i = 0; i < cangkuList.length; i++)
//                        {
//                            if (cangkuList[i]['ckId'] == item.ckId)
//                                return cangkuList[i]['ckName']
//                        }
//                        return item.ckName;
//                    }
                },
                
                { display: '备注', name: 'remark', width: 150, align: 'left',type:'text',editor: { type: 'text' } }
                ], width: '100%', pageSizeOptions: [5, 10, 15, 20], height: '400',
                url: 'PurOrderListAdd.aspx?Action=GetData',
               rownumbers:true,//显示序号
//                heightDiff:2,//高度补差
                dataAction: 'local',//本地排序
                usePager:false,
                alternatingRow: false,
                
                totalSummary:true,
                enabledEdit: true, //控制能否编辑的
                onBeforeSubmitEdit: f_onBeforeSubmitEdit,//提交编辑之前检查
                
               // totalRender:f_totalRender,//汇总
                
                onAfterEdit: f_onAfterEdit //更新单元格后的操作
            }
            );
        });
 
       
        //商品 改变事件：获取单位、单价等信息
        function f_onGoodsChanged(e)
        { 
           
            
            if (!e.data || !e.data.length) return;
            
            

            var grid = liger.get("maingrid");

            var selected = e.data[0]; 
            grid.updateRow(grid.lastEditRow, {
                
              
                unitName: selected.unitName,
                num:1,
                price: selected.price,
                dis:0,
                disPrice:0,
                sumPrice:selected.price,
                tax:0,
                taxPrice:0,
                sumPriceAll:selected.price
                
            });
            
          // var rowSelectName=e.data[0].goodsName.split[","];
            
          // alert(rowSelectName[0]);
          // alert(e.data[0].goodsName); 
           
//            var selectName = grid.getSelected();
//            if (selectName) { 
//            
//                
//                 grid.updateRow(selectName,{
//                 
//                 
//                   goodsName:e.data[0].goodsName
//                
//                });
//            
//            
//            }
           
             
            
            if(e.data.length>1)//如果选择了多行、那就在下面增加行
            {
                for (var i = 1; i < e.data.length; i++)
                {
                    var manager = $("#maingrid").ligerGetGridManager();
                    manager.addRow({
                        goodsId: e.data[i].goodsId,
                        goodsName:e.data[i].goodsName,
                        unitName:e.data[i].unitName,
                        num:1,
                        price : e.data[i].price,
                        dis : 0,
                        disPrice:0,
                        sumPrice:e.data[i].price,
                        tax:0,
                        taxPrice:0,
                        sumPriceAll:e.data[i].price
                        
                    });
                       
                }
            }
            

//            var out = JSON.stringify(selected);
//            $("#message").html('最后选择:'+out);
            
            
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
            var num,price,dis,disPrice,sumPrice,tax,taxPrice,sumPriceAll;
            
            
            num=Number(e.record.num);
            
            price=Number(e.record.priceCost);
                
            dis=Number(e.record.dis);
            
            disPrice=Number(e.record.disPrice);
            
            sumPrice=Number(e.record.sumPrice);    
            
            tax=Number(e.record.tax);
            
            taxPrice=Number(e.record.taxPrice);
            
            sumPriceAll=Number(e.record.sumPriceAll);
            
          
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
            
            if (e.column.name == "priceCost") //单价改变---开始、计算金额、折扣额、税额、价税合计
            {
               //单价改变：【数量、折扣率、税率】 计算【折扣额、金额、税额、价税合计】; 
                price=Number(e.value);
                
                 //处理扣率问题
                 if(dis==0)
                 {
                    dis=100;
                 }
                
                 //1、折扣额 = 数量*单价*(100-扣率)/100
                 disPrice=Number(num)*Number(price)*(100-dis)/100;

                 
                  //2、金额=数量*单价-折扣额
                 sumPrice=Number(num)* Number(price)-disPrice;
                                    
            
                 //3、税额=数量*单价*税率/100
                 taxPrice=Number(sumPrice)*Number(tax)/100;
                
                
                 //4、价税合计=金额-折扣+税额
                 sumPriceAll=Number(sumPrice) + Number(taxPrice);
                 
                 
                  num=Math.round(num*100)/100;
                  price=Math.round(price*100)/100;
                  dis=Math.round(dis*100)/100;
                  disPrice=Math.round(disPrice*100)/100;
                  sumPrice=Math.round(sumPrice*100)/100;
                  tax=Math.ceil(tax);
                  taxPrice=Math.round(taxPrice*100)/100;
                  sumPriceAll=Math.round(sumPriceAll*100)/100;
                  
                 //开始赋值
                
                 //1、折扣额
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
                 if(dis==0)
                 {
                    dis=100;
                 }
                
                 //1、折扣额 = 数量*单价*(100-扣率)/100
                 disPrice=Number(num)*Number(price)*(100-dis)/100;

                 
                  //2、金额=数量*单价-折扣额
                 sumPrice=Number(num)* Number(price)-disPrice;
                                    
            
                 //3、税额=数量*单价*税率/100
                 taxPrice=Number(sumPrice)*Number(tax)/100;
                
                
                 //4、价税合计=金额-折扣+税额
                 sumPriceAll=Number(sumPrice) + Number(taxPrice);
                 
                 
                  num=Math.round(num*100)/100;
                  price=Math.round(price*100)/100;
                  dis=Math.round(dis*100)/100;
                  disPrice=Math.round(disPrice*100)/100;
                  sumPrice=Math.round(sumPrice*100)/100;
                  tax=Math.ceil(tax);
                  taxPrice=Math.round(taxPrice*100)/100;
                  sumPriceAll=Math.round(sumPriceAll*100)/100;
                  
                 //开始赋值
                
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
                     dis=disPrice/(num*price);//Math.ceil();//Math.round(e.sum*100)/100  折扣额 = 数量*单价*扣率
                 
                 }

                
               
               
                 
                  //2、金额=数量*单价-折扣额
                 sumPrice=Number(num)* Number(price)-disPrice;
                 
                  
            
                 //3、税额=数量*单价*税率/100
                 taxPrice=Number(sumPrice)*Number(tax)/100;
                
                
                 //4、价税合计=金额-折扣+税额
                 sumPriceAll=Number(sumPrice) + Number(taxPrice);
                 
                 
                 num=Math.round(num*100)/100;
                  price=Math.round(price*100)/100;
                  dis=Math.round(dis*100)/100;
                  disPrice=Math.round(disPrice*100)/100;
                  sumPrice=Math.round(sumPrice*100)/100;
                  tax=Math.ceil(tax);
                  taxPrice=Math.round(taxPrice*100)/100;
                  sumPriceAll=Math.round(sumPriceAll*100)/100;
                  
                 //开始赋值
                
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
                    price=(sumPrice+disPrice)/num;
                }
                
                 //2、计算折扣率=折扣额/(折扣额+金额)  
                 
                 
                 dis=Number(disPrice)/(Number(disPrice)+Number(sumPrice));
                 

                 //3、税额=数量*单价*税率/100
                 taxPrice=Number(sumPrice)*Number(tax)/100;
                
                
                 //4、价税合计=金额-折扣+税额
                 sumPriceAll=Number(sumPrice) + Number(taxPrice);
                 
                 
                 num=Math.round(num*100)/100;
                  price=Math.round(price*100)/100;
                  dis=Math.round(dis*100)/100;
                  disPrice=Math.round(disPrice*100)/100;
                  sumPrice=Math.round(sumPrice*100)/100;
                  tax=Math.ceil(tax);
                  taxPrice=Math.round(taxPrice*100)/100;
                  sumPriceAll=Math.round(sumPriceAll*100)/100;
                  
                 //开始赋值
                
                 //1、单价
                 manager.updateCell("priceCost",price,e.record);
                 
                 //2、折扣率
                 manager.updateCell('dis',Math.ceil(dis*100), e.record);
                 
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
   
                 //sumPriceAll=num*price-disPrice+tax*num*price;
                 
                 //price=(sumPriceAll+disPrice)/(num*(1+tax/100))
                
                 //1、计算单价 
                 if(num>0)
                 {
                     price=Number(sumPriceAll+disPrice)/(Number(num)*(1+tax/100));
                 }
                 
                 //2、折扣率=折扣额/单价*数量
                 
                 dis=Math.ceil(disPrice/(price*num)*100);
                 
                 //3、金额=单价*数量-折扣金额
                 
                 sumPrice=Number(price*num) - Number(disPrice);
                 
                 //4、税额=金额*税率
                 
                 taxPrice=sumPrice*tax/100;
                 
                 
                  num=Math.round(num*100)/100;
                  price=Math.round(price*100)/100;
                  dis=Math.round(dis*100)/100;
                  disPrice=Math.round(disPrice*100)/100;
                  sumPrice=Math.round(sumPrice*100)/100;
                  tax=Math.ceil(tax);
                  taxPrice=Math.round(taxPrice*100)/100;
                  sumPriceAll=Math.round(sumPriceAll*100)/100;
                  
                  
                 //开始赋值
               
                 
                 //1、单价
                 manager.updateCell('priceCost',price, e.record);
                 
                 //2、折扣率
                 manager.updateCell('dis',dis, e.record);
   
                 //4、金额
                 manager.updateCell('sumPrice',sumPrice, e.record);
                 
                 //4、税额
                 manager.updateCell('taxPrice',taxPrice, e.record);
                 
                 

            } //价税合计改变---结束
           
            //最后改变汇总行的值
            //manager.update();
            
           
            
            
        }


        
        //只允许编辑前3行
        function f_onBeforeEdit(e)
        { 
            if(e.rowindex<=2) return true;
            return false;
        }
        //限制年龄
        function f_onBeforeSubmitEdit(e)
        { 
            if (e.column.name == "dis")
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
                f_alert("warning","至少保留一行！");
            }
            else
            {
               manager.deleteSelectedRow();
            }
            
        }
        var newrowid = 100;
        function addNewRow()
        {
            manager.addEditRow();
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
        
    </script>
    
    <style type="text/css">
   body{ font-size:12px;}
        .l-table-edit {}
        .l-table-edit-td{ padding:4px;}
        .l-button-submit,.l-button-test{width:80px; float:left; margin-left:10px; padding-bottom:2px;}
        .l-verify-tip{ left:230px; top:120px;}
    </style>
    
  
     <script type="text/javascript">
        var eee;
        $(function ()
        {
            $.metadata.setType("attr", "validate");
            var v = $("form").validate({
                debug: true,
                errorPlacement: function (lable, element)
                {
                    if (element.hasClass("l-textarea"))
                    {
                        element.ligerTip({ content: lable.html(), target: element[0] }); 
                    }
                    else if (element.hasClass("l-text-field"))
                    {
                        element.parent().ligerTip({ content: lable.html(), target: element[0] });
                    }
                    else
                    {
                        lable.appendTo(element.parents("td:first").next("td"));
                    }
                },
                success: function (lable)
                {
                    lable.ligerHideTip();
                    lable.remove();
                },
                submitHandler: function ()
                {
                   
//                    $("form .l-text,.l-textarea").ligerHideTip();
//                    alert("Submitted!");
                 
                    
                    
                }
            });
          
            
            var g =  $.ligerui.get("ddlVenderList");
            g.set("Width", 250);
            
        });  
        
        function save()
        {
          
            
             var editor = liger.get("ddlVenderList");
            
            // alert(editor.getValue());
             
             var venderId=editor.getValue();
            
            
            //alert($("#txtBizDate").val());
            
            var bizDate=$("#txtBizDate").val();
            
            //alert($("#txtSendDate").val());
            
            var sendDate= $("#txtSendDate").val();
            
            //alert($("#txtRemarks").val());
            
            var remarks=$("#txtRemarks").val();
            
            
             var obj={"venderId":venderId,"bizDate":bizDate,"sendDate":sendDate,"remarks":remarks};
            
            
            alert(JSON.stringify(obj));
            
             var manager = $("#maingrid").ligerGetGridManager();
                var data = manager.getData();
           
            alert(JSON.stringify(data));
            
            obj.push(data);
            
            alert(JSON.stringify(obj));
            
            
            
            $.ajax({
                    type: "POST",
                    contentType: "application/json",
                    dataType: "json",
                    url:"../handler/saveData.ashx",
                    data:data, 
                    success: function (msg) {
                    
                         
                    
                    },
                    error: function (msg) {
                    
                       alert("您的网络异常！");
                    
                    
                    }
                });
    
    
            
        }
       
    </script>

    
    
</head>
<body style="background:#f5f5f5 url(../images/index_bg.png) 0 bottom no-repeat; background-attachment:fixed; padding-left:10px;">


    <form id="form1" runat="server">
   
 <table border="0" cellpadding="0" cellspacing="0" style="width:100%; line-height:40px;">
           <tr>
           <td style="width:80px; text-align:center;" class="l-table-edit-td">
               购货单位：&nbsp;</td>
           <td style="text-align:left; width:250px;">
            
             <input type="text" id="ddlVenderList" validate="{required:true}"/> 
             
          
                   
            </td>
           <td style="text-align:right; width:80px;">
                                                  订单日期：</td>
           <td style="text-align:left; width:180px;">
                                                  <asp:TextBox ID="txtBizDate" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                   </td>
           <td style="text-align:right; width:80px;">
                                                  交货日期：</td>
           <td style="text-align:left; width:180px;">
                                                  <asp:TextBox ID="txtSendDate" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                   </td>
           <td style="text-align:right; width:80px;">
                                                  购货类别：</td>
           <td style="text-align:left;">
                                                  <asp:RadioButton ID="rb1" runat="server" Checked="True" GroupName="t" 
                   Text="购货" />
               <asp:RadioButton ID="rb2" runat="server" GroupName="t" Text="退货" />
               </td>
           </tr>
           <tr>
           <td style="text-align:left; height:450px;" class="l-table-edit-td" colspan="8">
            
            <div id="maingrid"></div>  
            <div style="display:none;">
   
</div>
            
            </td>
           </tr>
           <tr>
           <td style="width:80px; text-align:right;" class="l-table-edit-td">
               备注信息：</td>
           <td style="text-align:left; " colspan="5">
            
               <asp:TextBox ID="txtRemarks" runat="server" Width="540px" TextMode="MultiLine"></asp:TextBox>
              
                   
                   </td>
           <td style="text-align:right; " colspan="2">
              
              
                   
               <input id="btnSave" class="ui-btn ui-btn-sp mrb" type="button" value="新增" onclick="save()"  />
                      
               <input id="btnReload" class="ui-btn" type="button" value="返回列表"  />
                   
              
              
                   
               </td>
           </tr>
           </table>
           
           
    </form>
</body>
</html>

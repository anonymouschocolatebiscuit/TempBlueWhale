
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
        
        
      
       

     
                    
    var manager;
        $(function ()
        {
        
          var form = $("#form").ligerForm();
          
         
          
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
                   
                    return h;
                }
                },
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
                                    { display: '商品名称',name: 'names', width: 200 },
                                    { display: '规格',name: 'spec', width: 100 },
                                    { display: '单位',name: 'unitName', width: 100 },
                                    { display: '首选仓库',name: 'ckName', width: 100 },
                                    { display: '仓库ID',name: 'ckId', width: 40,hide:true}
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
                { display: '规格', name: 'spec',width: 180, align: 'center' },
                { display: '单位', name: 'unitName',width: 80, align: 'center' },
                { display: '金额', name: 'price', width: 130, type: 'float', align: 'right',editor: { type: 'float' },
                
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
               
                { display: '仓库', name: 'ckId', width: 100, isSort: false,textField:'ckName',
                    editor: { type: 'select',
                              url:"../baseSet/CangkuList.aspx?Action=GetDDLList&r=" + Math.random(), 
                              valueField: 'ckId',textField:'ckName'}

                },
              
                
                
                { display: '备注', name: 'remarks', width: 150, align: 'left',type:'text',editor: { type: 'text' } }
                
                ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '450',
               url: 'CostChangeListEdit.aspx?Action=GetData&id='+getUrlParam("id"),//获取参数
               rownumbers:true,//显示序号
               frozenRownumbers:true,//行序号是否在固定列中
                dataAction: 'local',//本地排序
                usePager:false,
                alternatingRow: false,
                
                totalSummary:true,
                enabledEdit: true //控制能否编辑的
           
            }
            );
        });
 
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
//              var ckName=$("#ddlCangkuList").find("option:selected").text();  //获取Select选择的Text
//              var ckId=$("#ddlCangkuList").val();  //获取Select选择的Value
//              
//              alert(ckName);
//              alert(ckId);
//              
//               var grid = liger.get("maingrid");
//               var data = manager.getData();
//               
//               alert(data.length);
//               
//               
//               for(var i=0;i<data.length;i++)
//               {
//                   alert(data[i].goodsId);
//                   
//                   grid.updateCell("ckId",ckId,i);
//                   grid.updateCell("ckName",ckName,i);
//                   
//               }
                            
               
               $(".l-dialog,.l-window-mask").remove();
               
//             var dialog = frameElement.dialog;
//            
//             dialog.close(); //关闭dialog

             $.ligerDialog.close(); //关闭dialog
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
                price:"",
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
                        goodsName:e.data[i].names,
                        spec:e.data[i].spec,
                        unitName:e.data[i].unitName,
                        price:"",
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
               
                    id :rowNum+1,
                    goodsId :"",
                    goodsName : "",
                    spec :"",
                    unitName : "",
                    num :"",
                    ckIdIn :"",
                    ckIdOut : "",
                    ckNameIn : "",
                    ckNameOut : "",

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
         if(data[i].price<=0 || data[i].price=="" || data[i].price=="0" || data[i].price=="0.00")
         {
             
             $.ligerDialog.warn("请输入第"+(i+1)+"行的商品金额！");
             
             return;
             alert("我就不执行了！");
         }
         
         
          if(data[i].ckId<=0 || data[i].ckId=="" || data[i].ckId=="0")
         {
             
             $.ligerDialog.warn("请选择第"+(i+1)+"行的仓库！");
             
             return;
             alert("我就不执行了！");
         }
         
       
         
         
       
        
     }
     
 

            
      var bizDate=$("#txtBizDate").val();
      if(bizDate=="")
      {
          $.ligerDialog.warn("请输入调整日期！");
          return;
          
      }

        var bizType=$("#ddlBizType").val();
      
       
      
            
            var remarks=$("#txtRemarks").val();
            
      
        
           var headJson={id:getUrlParam("id"),bizDate:bizDate,bizType:bizType,remarks:remarks};
      
    
        
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
            url: '../handler/CostChangeListEdit.ashx',
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

      
//商品开始----BOM主商品

$(function () {
    $("#txtGoodsName").ligerComboBox({
        onBeforeOpen: f_selectGoods, valueFieldID: 'hfGoodsId', width: 300
    });
});


function f_selectGoods() {
    $.ligerDialog.open({
        title: '选择商品', name: 'winselector', width: 800, height: 540, url: '../baseSet/GoodsListSelect.aspx', buttons: [
            { text: '确定', onclick: f_selectGoodsOK },
            { text: '关闭', onclick: f_selectGoodsCancel }
        ]
    });
    return false;
}


function f_selectGoodsOK(item, dialog) {
    var fn = dialog.frame.f_select || dialog.frame.window.f_select;
    var data = fn();
    if (!data) {
        alert('请选择行!');
        return;
    }

    $("#txtGoodsName").val(data[0].names);
    $("#hfGoodsId").val(data[0].id);
    $("#txtSpec").val(data[0].spec);
    $("#txtUnitName").val(data[0].unitName);


    dialog.close();

}


function f_selectGoodsCancel(item, dialog) {
    dialog.close();
}

//主商品结束

        
//新样式引入行


function f_selectContact() {
   


    $.ligerDialog.open({
        title: '选择商品', name: 'winselector', width: 840, height: 540, url: '../baseSet/GoodsListSelect.aspx', buttons: [
            { text: '确定', onclick: f_selectContactOK },
            { text: '关闭', onclick: f_selectContactCancel }
        ]
    });
    return false;
}
function f_selectContactOK(item, dialog) {
    var fn = dialog.frame.f_select || dialog.frame.window.f_select;
    var data = fn();
    if (!data) {
        alert('请选择行!');
        return;

    }

    dialog.close();

    f_onGoodsChangedSub(data);

   

}


function f_selectContactCancel(item, dialog) {
    dialog.close();
}

//商品 改变事件：获取单位、单价等信息
function f_onGoodsChangedSub(e) {


    if (!e || !e.length) return;

    //1、先更新当前行的后续数据

    var grid = liger.get("maingridsub");

    var selected = e[0];// e.data[0]; 

    // alert(selected.names);

    var selectedRow = managersub.getSelected();

    grid.updateRow(selectedRow, {

        goodsId: selected.id,
        goodsName: selected.names,
        unitName: selected.unitName,
        num: 1,
        rate: 0,
        spec: selected.spec,
        remarks: ""

    });

    if (e.length > 1) //如果有多行的、先删除空白行，然后插入下面
    {

        var data = managersub.getData();
        for (var i = data.length - 1; i >= 0; i--) {
            if (data[i].goodsId == 0 || data[i].goodsName == "") {
                managersub.deleteRow(i);
                // alert("删除行："+i);
            }

        }

        for (var i = 1; i < e.length; i++) {
            grid.addRow({
                id: rowNumber,
                goodsId: e[i].id,
                goodsName: e[i].names,
                unitName: e[i].unitName,
                num: 1,
                rate: 0,
                spec: e[i].spec,

                remarks: ""

            });

            rowNumber = rowNumber + 1;

        }

    }

    updateTotal();

}




//新样式引入行end



  //新样式引入行
        
     

     
                    
  

 
       var managersub;
        $(function ()
        {
         
            //创建表单结构
            var form = $("#form").ligerForm();

            var ddlTypeList = $.ligerui.get("ddlTypeList");
            ddlTypeList.set("Width", 250);


            var txtGoodsName = $.ligerui.get("txtGoodsName");
            txtGoodsName.set("Width", 250);

            var txtNum = $.ligerui.get("txtNum");
            txtNum.set("Width", 250);

            var goodsName = $("#hfGoodsName").val();
            $("#txtGoodsName").val(goodsName);

          
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
                
                { display: '规格', name: 'spec',width: 150, align: 'center' },
                
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
                
                { display: '损耗率%', name: 'rate', width: 70, type: 'float', align: 'right', editor: { type: 'float' }
                
                },

                
                { display: '备注', name: 'remarks', width: 150, align: 'left',type:'text',editor: { type: 'text' } }
                ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '320',
                url: 'goodsBomListEdit.aspx?Action=GetDataSub&id=' + param,
               rownumbers:true,//显示序号
               frozenRownumbers:true,//行序号是否在固定列中
                dataAction: 'local',//本地排序
                usePager:false,
                alternatingRow: false,
                
                enabledEdit: true, //控制能否编辑的
             
                
                onAfterEdit: f_onAfterEditSub //更新单元格后的操作
            }
            );
        });
 
 
   

        var rowNumber=9;
 
        function f_totalRender(data, currentPageData)
        {
             //return "总仓库数量:"+data.sumPriceAll; 
        }


     
      

       
         //编辑后事件 
        function f_onAfterEditSub(e)
        {
            var num
            
            
            num=Number(e.record.num);
            
          

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
             
                 

                  
                 //开始赋值
                 
                 managersub.updateCell("num",num, e.record);
                
             
               

                 
               
                 
                
                 

            } //数量改变---结束
            

          
           
          
          
        //最后改变汇总行的值
           
           


        
          
         
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
            if (e.column.name == "rate")
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
     
   

    var typeId = $("#ddlTypeList").val();

    if (typeId == "" || typeId == 0) {
        $.ligerDialog.warn('请选择BOM分组！');

        return;
    }

     var goodsId = $("#hfGoodsId").val();

     var editon = $("#txtEdition").val();
     var tuhao = $("#txtTuhao").val();

     if (goodsId == "" || goodsId == 0) {
         $.ligerDialog.warn('请选择商品！');

         return;
     }


     var num = $("#txtNum").val();

     var rate = $("#txtRate").val();

     if (num == "" || num == 0)
     {
         $.ligerDialog.warn('请输入商品数量！');

         return;
     }

     if (rate == "" || rate == 0 || rate>100 ) {
         $.ligerDialog.warn('请输入成品率！');

         return;
     }

  

     
     //商品开始
     
      var datasub = managersub.getData();
     
     // alert("1111111111111111");

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
          $.ligerDialog.warn('请选择BOM清单的商品！');
          
          return;
          alert("我就不执行了！");
     }     
     
     
     
     //3、判断商品数量是否都输入了。
      for(var i=0;i<datasub.length;i++)
     {
         if(datasub[i].num<=0 || datasub[i].num=="" || datasub[i].num=="0" || datasub[i].num=="0.00")
         {
             
             $.ligerDialog.warn("请输入第"+(i+1)+"行的商品数量！");
             
             return;
             alert("我就不执行了！");
         }
         
         

       
        
     }
     
     
     
     //
     
      
     
    

         
            
            var remarks=$("#txtRemarks").val();
            
           
           

            var postData = {
                id:param,
                typeId: typeId,
                edition: editon,
                tuhao: tuhao,
                goodsId: goodsId,
                num: num,
                rate: rate,                
                remarks: remarks,
                Rows: datasub
               
            };
      
           
            //alert(JSON.stringify(postData));

        
         
         $.ajax({
            type: "POST",
            url: 'ashx/goodsBomListEdit.ashx',
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
                    alert("ssss");
                   $.ligerDialog.warn(jsonResult);
                   
                }
            },
            error: function (xhr) {
                alert("出现错误，请稍后再试:" + xhr.responseText);
            }
        });
            
           
}


var param = getUrlParam("id");



function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");

    var r = window.location.search.substr(1).match(reg);

    if (r != null) return unescape(r[2]); return null;
}
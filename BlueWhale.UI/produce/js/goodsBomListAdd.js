
      
//Product start ---- BOM main product

$(function () {
    $("#txtGoodsName").ligerComboBox({
        onBeforeOpen: f_selectGoods, valueFieldID: 'hfGoodsId', width: 300
    });
});


function f_selectGoods() {
    $.ligerDialog.open({
        title: 'Select product', name: 'winselector', width: 800, height: 540, url: '../baseSet/GoodsListSelect.aspx', buttons: [
            { text: 'Confirm', onclick: f_selectGoodsOK },
            { text: 'Close', onclick: f_selectGoodsCancel }
        ]
    });
    return false;
}


function f_selectGoodsOK(item, dialog) {
    var fn = dialog.frame.f_select || dialog.frame.window.f_select;
    var data = fn();
    if (!data) {
        alert('Please select a row!');
        return;
    }

    $("#txtGoodsName").val(data[0].names);
    $("#hfGoodsId").val(data[0].id);
    $("#txtSpec").val(data[0].spec);
    $("#txtUnitName").val(data[0].unitName);

   // alert("id:" + data[0].id);

    dialog.close();

}


function f_selectGoodsCancel(item, dialog) {
    dialog.close();
}

//Main product ends


function f_selectContact() {
   


    $.ligerDialog.open({
        title: 'Select product', name: 'winselector', width: 840, height: 540, url: '../baseSet/GoodsListSelect.aspx', buttons: [
            { text: 'Confirm', onclick: f_selectContactOK },
            { text: 'Close', onclick: f_selectContactCancel }
        ]
    });
    return false;
}
function f_selectContactOK(item, dialog) {
    var fn = dialog.frame.f_select || dialog.frame.window.f_select;
    var data = fn();
    if (!data) {
        alert('Please select a row!');
        return;

    }

    dialog.close();

    f_onGoodsChangedSub(data);

   

}


function f_selectContactCancel(item, dialog) {
    dialog.close();
}

//Product change event: get unit, unit price and other information
function f_onGoodsChangedSub(e) {


    if (!e || !e.length) return;


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

    if (e.length > 1) 
    {

        var data = managersub.getData();
        for (var i = data.length - 1; i >= 0; i--) {
            if (data[i].goodsId == 0 || data[i].goodsName == "") {
                managersub.deleteRow(i);
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
        
     

     
                    
  

 
       var managersub;
        $(function ()
        {
            var form = $("#form").ligerForm();

            var ddlTypeList = $.ligerui.get("ddlTypeList");
            ddlTypeList.set("Width", 250);


            var txtGoodsName = $.ligerui.get("txtGoodsName");
            txtGoodsName.set("Width", 250);

            var txtNum = $.ligerui.get("txtNum");
            txtNum.set("Width", 250);

          
            window['gsub'] = 
            managersub = $("#maingridsub").ligerGrid({
                columns: [
                
                { display: '', isSort: false, width: 60,align:'center',frozen:true, render: function (rowdata, rowindex, value)
                 {
                    var h = "";
                    if (!rowdata._editing)
                    {
                        h += "<a href='javascript:addNewRow()' title='Add row' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                        h += "<a href='javascript:deleteRow()' title='Delete row' style='float:left;'><div class='ui-icon ui-icon-trash'></div></a> "; 
                        h += "<a href='javascript:f_selectContact()' title='Select Goods' style='float:left;'><div class='ui-icon ui-icon-search'></div></a> ";
                    }
                  
                    return h;
                }
                }
                ,
               
                    {
                        display: 'Goods Name', name: 'goodsName', width: 250, align: 'left',
                
                   totalSummary:
                    {
                        type: 'count',
                        render: function (e) 
                        {
                            return 'Total：';
                        }
                    }
                  
                
                },
                
                    { display: 'Specification', name: 'spec',width: 150, align: 'center' },
                
                { display: 'Unit', name: 'unitName',width: 80, align: 'center' },
                { display: 'Quantity', name: 'num', width: 80, type: 'float', align: 'right',editor: { type: 'float' },
                
                   totalSummary:
                    {
                        align: 'right', 
                        type: 'sum',
                        render: function (e) 
                        {
                            return  Math.round(e.sum*100)/100;
                        }
                    }
                
                },
                
                { display: '%', name: 'rate', width: 70, type: 'float', align: 'right', editor: { type: 'float' }
                
                },

                
                { display: 'Remarks', name: 'remarks', width: 150, align: 'left',type:'text',editor: { type: 'text' } }
                ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '320',
                url: 'goodsBomListAdd.aspx?Action=GetDataSub',
               rownumbers:true,
               frozenRownumbers:true,
                dataAction: 'local',
                usePager:false,
                alternatingRow: false,
                
                enabledEdit: true,
             
                
                onAfterEdit: f_onAfterEditSub
            }
            );
        });
 
 
   

        var rowNumber=9;
 
        function f_totalRender(data, currentPageData)
        {
             //return "Total:"+data.sumPriceAll; 
        }

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
            
          
            if (e.column.name == "num") 
            {
                 num=Number(e.value);
                 managersub.updateCell("num",num, e.record);
            } 
        }

        function f_onBeforeEdit(e)
        { 
        
//            if(e.data.goodsId!="" && e.data.goodsName!="") return true;
//            return false;
//            
//            if(e.rowindex<=2) return true;
//            return false;

            
        }
        
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
           if (!row) { alert('Please select a row'); return; }
            manager.beginEdit(row);
        }
        function cancelEdit() {
            var row = manager.getSelectedRow();
            if (!row) { alert('Please select a row'); return; }
            manager.cancelEdit(row);
        }
        function cancelAllEdit()
        {
            manager.cancelEdit();
        }
        function endEdit()
        {
            var row = manager.getSelectedRow();
            if (!row) { alert('Please select a row'); return; }
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
                $.ligerDialog.warn('At least keep one row!')
                
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
            if (!selected) { alert('Please select a row'); return; }
          
        }


        function getSelected()
        { 
            var row = manager.getSelectedRow();
            if (!row) { alert('Please select a row'); return; }
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
        $.ligerDialog.warn('Please select an BOM Group！');

        return;
    }

     var goodsId = $("#hfGoodsId").val();

     var editon = $("#txtEdition").val();
     var tuhao = $("#txtTuhao").val();

     if (goodsId == "" || goodsId == 0) {
         $.ligerDialog.warn('Please Select Goods Information?');

         return;
     }


     var num = $("#txtNum").val();

     var rate = $("#txtRate").val();

     if (num == "" || num == 0)
     {
         $.ligerDialog.warn('Please insert product quantity!');

         return;
     }

     if (rate == "" || rate == 0 || rate>100 ) {
         $.ligerDialog.warn('Please insert yield rate!');

         return;
     }

      var datasub = managersub.getData();
     
      for(var i=datasub.length-1;i>=0;i--)
     {
         if(datasub[i].goodsId==0 || datasub[i].goodsName=="")
         {
             datasub.splice(i,1);
            
         }
        
     }
     
      if(datasub.length==0)
     {
          $.ligerDialog.warn('Please select the product in the BOM list!');
          
          return;
          alert("I won't execute it！");
     }     
     
     
     
     //3、判断商品数量是否都输入了。
      for(var i=0;i<datasub.length;i++)
     {
         if(datasub[i].num<=0 || datasub[i].num=="" || datasub[i].num=="0" || datasub[i].num=="0.00")
         {
             
             $.ligerDialog.warn("Please enter the row "+(i+1)+" product quantity！");
             
             return;
             alert("I won't execute it！");
         }

         if (datasub[i].goodsId == goodsId)
         {
             alert("Product Row "+ (i + 1) + " repeated！");
             return;
         }
         
         

       
        
     }
     
     
     
     //
     
      
     
     // alert("goodsId:"+goodsId);

      //return;
         
            
            var remarks=$("#txtRemarks").val();
            
           
           

            var postData = {
                typeId: typeId,
                edition: editon,
                tuhao: tuhao,
                goodsId: goodsId,
                num: num,
                rate: rate,                
                remarks: remarks,
                Rows: datasub
               
            };
      
           



            //alert(JSON.stringify(headJson));
            //return;


     
       
      
        
       // var postData=JSON.parse(list);//最终的json
        
//        alert(postData.Rows[0].id);
//        
//        alert(postData.bizDate);
//        
//        alert(postData.Rows[0].goodsName);

      

    //   $("#txtRemarks").val(JSON.stringify(postData));
       
      // return;
         
         $.ajax({
            type: "POST",
            url: 'ashx/goodsBomListAdd.ashx',
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

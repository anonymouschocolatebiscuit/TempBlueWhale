
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
        alert('Please select row!');
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

function f_selectContact() {
   


    $.ligerDialog.open({
        title: 'Please Select Goods Information', name: 'winselector', width: 840, height: 540, url: '../baseSet/GoodsListSelect.aspx', buttons: [
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

    if (e.length > 1) //If there are multiple lines, delete the blank lines first, then insert the following
    {

        var data = managersub.getData();
        for (var i = data.length - 1; i >= 0; i--) {
            if (data[i].goodsId == 0 || data[i].goodsName == "") {
                managersub.deleteRow(i);
                // alert("Delete row："+i);
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
                        h += "<a href='javascript:addNewRow()' title='Add new row' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                        h += "<a href='javascript:deleteRow()' title='Delete row' style='float:left;'><div class='ui-icon ui-icon-trash'></div></a> "; 
                        h += "<a href='javascript:f_selectContact()' title='Select product' style='float:left;'><div class='ui-icon ui-icon-search'></div></a> ";
                    }
                  
                    return h;
                }
                }
                ,
               
                { display: 'Product Name', name: 'goodsName', width: 250, align: 'left',
                
                   totalSummary:
                    {
                        type: 'count',
                        render: function (e) 
                        {
                            return '合计：';
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
                
                { display: 'Lose Rate %', name: 'rate', width: 70, type: 'float', align: 'right', editor: { type: 'float' }
                
                },

                
                { display: 'Remarks', name: 'remarks', width: 150, align: 'left',type:'text',editor: { type: 'text' } }
                ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '320',
                url: 'goodsBomListEdit.aspx?Action=GetDataSub&id=' + param,
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
             //return "total quantity:"+data.sumPriceAll; 
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
            if (!row) { alert('Please select row'); return; }
            manager.beginEdit(row);
        }
        function cancelEdit() {
            var row = manager.getSelectedRow();
            if (!row) { alert('Please select row'); return; }
            manager.cancelEdit(row);
        }
        function cancelAllEdit()
        {
            manager.cancelEdit();
        }
        function endEdit()
        {
            var row = manager.getSelectedRow();
            if (!row) { alert('Please select row'); return; }
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
    var typeId = $("#ddlTypeList").val();

    if (typeId == "" || typeId == 0) {
        $.ligerDialog.warn('Please select a BOM group！');

        return;
    }

     var goodsId = $("#hfGoodsId").val();

     var editon = $("#txtEdition").val();
     var tuhao = $("#txtTuhao").val();

     if (goodsId == "" || goodsId == 0) {
         $.ligerDialog.warn('Please select a row！');

         return;
     }


     var num = $("#txtNum").val();

     var rate = $("#txtRate").val();

     if (num == "" || num == 0)
     {
         $.ligerDialog.warn('Please insert product quantity！');

         return;
     }

     if (rate == "" || rate == 0 || rate>100 ) {
         $.ligerDialog.warn('Please insert yield rate！');

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
          $.ligerDialog.warn('Please select product from BOM list！');
          
          return;
     }     
     
      for(var i=0;i<datasub.length;i++)
     {
         if(datasub[i].num<=0 || datasub[i].num=="" || datasub[i].num=="0" || datasub[i].num=="0.00")
         {
             
             $.ligerDialog.warn("Please insert the quantity for row "+(i+1)+"!");
             
             return;
         }
     }
     
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
      

        
         
         $.ajax({
            type: "POST",
            url: 'ashx/goodsBomListEdit.ashx',
            contentType: "application/json", 
            //dataType: "json", 
            data:JSON.stringify(postData),  
            success: function (jsonResult) {
                
                if (jsonResult =="Execution successful！")
                {
                    $.ligerDialog.waitting('Execution successful！'); setTimeout(function () { $.ligerDialog.closeWaitting();location.reload();}, 2000);   
                }
                else
                {
                   $.ligerDialog.warn(jsonResult);
                   
                }
            },
            error: function (xhr) {
                alert("An error occurred, please try again later:" + xhr.responseText);
            }
        });
            
           
}

var param = getUrlParam("id");
function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");

    var r = window.location.search.substr(1).match(reg);

    if (r != null) return unescape(r[2]); return null;
}
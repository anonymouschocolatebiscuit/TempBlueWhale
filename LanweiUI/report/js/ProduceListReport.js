//客户开始


function f_selectClient() {
    $.ligerDialog.open({
        title: '选择客户', name: 'winselector', width: 800, height: 540, url: '../baseSet/ClientListSelect.aspx', buttons: [
            { text: '确定', onclick: f_selectClientOK },
            { text: '关闭', onclick: f_selectClientCancel }
        ]
    });
    return false;
}
function f_selectClientOK(item, dialog) {
    var fn = dialog.frame.f_select || dialog.frame.window.f_select;
    var data = fn();
    if (!data) {
        alert('请选择行!');
        return;
    }

    $("#clientName").val(data.names);
    $("#clientId").val(data.code);


    dialog.close();

}


function f_selectClientCancel(item, dialog) {
    dialog.close();
}


$(function () {

    $("#clientName").ligerComboBox({
        onBeforeOpen: f_selectClient, valueFieldID: 'clientId', width: 250
    });


    $("#txtFlagList").ligerComboBox({
        isShowCheckBox: true, isMultiSelect: true,
        data: [
            { text: '未生产', id: '1' },
            { text: '进行中', id: '2' },
            { text: '已完成', id: '44' }
        ], valueFieldID: 'flag'
    });


});



//客户结束

 
//商品开始

$(function () {


    $("#txtGoodsName").ligerComboBox({
        onBeforeOpen: f_selectGoods, valueFieldID: 'hfGoodsId', width: 300
    });

    

});


function f_selectGoods() {
    $.ligerDialog.open({
        title: '选择商品', name: 'winselector', width: 800, height: 540, url: '../baseSet/GoodsListSelect.aspx?type=1', buttons: [
            { text: '确定', onclick: f_selectGoodsOK },
            { text: '关闭', onclick: f_selectGoodsCancel }
        ]
    });
    return false;
}


function f_selectGoodsOK(item, dialog) {
    var fn = dialog.frame.f_select || dialog.frame.window.f_select;
    var data = fn();
    if (data.length==0) {
        $.ligerDialog.warn("请选择商品！");
       
        return;
    }

    if(data.length>1)
    {
        $.ligerDialog.warn("请选择一条商品！");
        return;
      
    }

   
    $("#txtGoodsName").val(data[0].names);
    $("#txtGoodsId").val(data[0].code);
  

    dialog.close();

}


function f_selectGoodsCancel(item, dialog) {
    dialog.close();
}

//商品结束


       
       
        var manager;
        $(function() {
        
        
               
        var form = $("#form").ligerForm();
        
        
          var menu = $.ligerMenu({ width: 120, items:
            [
            { text: '查看订单记录', click: viewRow },
          
            { line: true },
            { text: '查看入库记录', click: viewRow },
          
            
            ]
            }); 
            

        
         var dateStart =  $.ligerui.get("txtDateStart");
         dateStart.set("Width", 110);
         
         var  dateEnd=  $.ligerui.get("txtDateEnd");
         dateEnd.set("Width", 110);
         
         var  txtFlagList=  $.ligerui.get("txtFlagList");
         txtFlagList.set("Width", 100);
         
         

        
         
            
            manager = $("#maingrid").ligerGrid({
            
                columns: [

                   {
                       display: '计划日期', name: 'makeDate', width: 80, align: 'center', valign: 'center',

                       totalSummary:
                      {
                          type: 'count',
                          render: function (e) {  //汇总渲染器，返回html加载到单元格
                              //e 汇总Object(包括sum,max,min,avg,count) 
                              return '合计：';
                          }
                      }

                   },
                
                 { display: '单据编号', name: 'number', width: 150, align: 'center' },
                 { display: '计划类别', name: 'typeName', width: 80, align: 'center'},   
                 { display: '订单编号', name: 'orderNumber', width: 150, align: 'center' },                
                
                 { display: '客户', name: 'wlName', width: 170, align: 'left' },

                 { display: '商品编号', name: 'code', width: 100, align: 'center' },
                 { display: '商品名称', name: 'goodsName', width: 120, align: 'center' },
                 { display: '规格', name: 'spec', width: 100, align: 'center' },
                 { display: '单位', name: 'unitName', width: 70, align: 'center' },

                 { display: '计划数量', name: 'num', width: 80, align: 'center',
                 
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
                     display: '已生产数量', name: 'finishNum', width: 80, align: 'right',

                     totalSummary:
                    {
                        align: 'right',   //汇总单元格内容对齐方式:left/center/right 
                        type: 'sum',
                        render: function (e) {  //汇总渲染器，返回html加载到单元格
                            //e 汇总Object(包括sum,max,min,avg,count) 
                            return Math.round(e.sum * 100) / 100;
                        }
                    }

                 },
                 {
                     display: '未生产数量', name: 'finishNumNo', width: 80, align: 'right',

                     totalSummary:
                    {
                        align: 'right',   //汇总单元格内容对齐方式:left/center/right 
                        type: 'sum',
                        render: function (e) {  //汇总渲染器，返回html加载到单元格
                            //e 汇总Object(包括sum,max,min,avg,count) 
                            return Math.round(e.sum * 100) / 100;
                        }
                    }

                 },

                { display: '生产进度', name: 'sendFlag', width: 60, align: 'center' },

                 { display: '计划开始日期', name: 'dateStart', width: 80, align: 'center', valign: 'center' },
                 { display: '计划结束日期', name: 'dateEnd', width: 80, align: 'center', valign: 'center' },

                 { display: '交货日期', name: 'sendDate', width: 80, align: 'center' },

                 { display: '单据状态', name: 'flag', width: 80, align: 'center'},
             
                 { display: '制单人', name: 'makeName', width: 80, align: 'center' },
                 { display: '审核人', name: 'checkName', width: 80, align: 'center' },
                 { display: '备注', name: 'remarks', width: 100, align: 'left' }
                
                ], width: '98%', 
                  //pageSizeOptions: [5, 10, 15, 20],
                  height:'98%',
                 // pageSize: 15,
                dataAction: 'local', //本地排序
                usePager: false,
               rownumbers:true,//显示序号
                alternatingRow: false,
                onDblClickRow: function(data, rowindex, rowobj) {
                    // $.ligerDialog.alert('选择的是' + data.id);
                     viewRow();
                },
                allowUnSelectRow:true,
                 onRClickToSelect:true,
                onContextmenu : function (parm,e)
                {
                    actionCustomerID = parm.data.id;
                    menu.show({ top: e.pageY, left: e.pageX });
                    return false;
                } 
                
               

                
                
            }
            );

        });
        
        
       

        


        function search() {

          
            var start = $("#txtDateStart").val();
            var end = $("#txtDateEnd").val();
            
            var wlId = $("#clientId").val();

            var goodsList = $("#txtGoodsId").val();

            var typeId = $("#txtFlagList").val();
            
            var wlIdString=wlId.split(";");
            var goodsIdString=goodsList.split(";");
            var typeIdString=typeId.split(";");
            
            if(wlIdString!="")
            {
                wlId="";
                for(var i=0;i<wlIdString.length;i++)
                {
                   wlId+="'"+wlIdString[i]+"'"+",";
                } 
                wlId=wlId.substring(0,wlId.length-1);
                  
            }
            
             if(goodsIdString!="")
            {
                goodsList="";
                for(var i=0;i<goodsIdString.length;i++)
                {
                   goodsList+="'"+goodsIdString[i]+"'"+",";
                } 
                goodsList=goodsList.substring(0,goodsList.length-1);
                  
            }
            
             if(typeIdString!="")
            {
                typeId="";
                for(var i=0;i<typeIdString.length;i++)
                {
                   typeId+="'"+typeIdString[i]+"'"+",";
                } 
                typeId=typeId.substring(0,typeId.length-1);
                  
             }

             var keys = $("#txtKeys").val();
             if (keys == "请输入订单号/备注")
             {
                 keys = "";
             }

//            alert(typeId);
//          
//            alert(wlId);
//           

            //manager.changePage("first");
            //manager._setUrl("PurOrderList.aspx?Action=GetDataListSearch&types=0&keys=" + keys + "&start=" +start + "&end=" + end);
             manager._setUrl("ProduceListReport.aspx?Action=GetDataList&keys="+keys+"&start=" + start + "&end=" + end + "&wlId=" + wlId + "&goodsId=" + goodsList + "&typeId=" + typeId);
            //alert("SalesOrderListReport.aspx?Action=GetDataList&start=" + start + "&end=" + end + "&wlId=" + wlId + "&goodsId=" + goodsList + "&typeId=" + typeId);
            // window.location.href = "ProduceListReport.aspx?Action=GetDataList&start=" + start + "&end=" + end + "&wlId=" + wlId + "&goodsId=" + goodsList + "&typeId=" + typeId;
             var url = "ProduceListReport.aspx?Action=GetDataList&keys=" + keys + "&start=" + start + "&end=" + end + "&wlId=" + wlId + "&goodsId=" + goodsList + "&typeId=" + typeId;

            // window.open(url);



        }


   
      function viewRow()
      {
          var row = manager.getSelectedRow();
          
//          top.topManager.openPage({
//            id : 'purOrderListView',
//            href : 'buy/purOrderListView.aspx?id='+row.id,
//            title : '采购订单-详情'
//          });
  
  
      }
        
        
        function reload() {
            manager.reload();
        }


    
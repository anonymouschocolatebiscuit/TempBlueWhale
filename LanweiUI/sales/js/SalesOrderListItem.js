     
       
      
       
       
        var manager;
        $(function() {
        
        
               
        var form = $("#form").ligerForm();

        
         var dateStart =  $.ligerui.get("txtDateStart");
         dateStart.set("Width", 110);
         
         var  dateEnd=  $.ligerui.get("txtDateEnd");
         dateEnd.set("Width", 110);
         
         var  txtFlagList=  $.ligerui.get("txtFlagList");
         txtFlagList.set("Width", 100);
         
         
           var menu = $.ligerMenu({ width: 120, items:
            [
            { text: '立即采购', click: checkRow, icon: 'add' },
            { text: '锁定库存', click: lockNum },
            { line: true },
            { text: '取消锁库', click: lockNumNo },
             { line: true },
            { text: '出库', click: checkRow }
            
            ]
            }); 
            
         
            
            manager = $("#maingrid").ligerGrid({
            checkbox: true,
                columns: [

                 { display: 'ID', name: 'id', width: 40, align: 'center'},
                 { display: '商品ID', name: 'goodsId', width: 50, align: 'center'},
                 { display: '商品编号', name: 'code', width: 80, align: 'center'},
                 { display: '商品条码', name: 'barcode', width: 100, align: 'center'},
                 { display: '商品名称', name: 'goodsName', width: 120, align: 'center',
                 
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
                 { display: '规格', name: 'spec', width: 80, align: 'center'},
                 { display: '单位', name: 'unitName', width: 50, align: 'center'},
              
                 { display: '订单日期', name: 'bizDate', width: 80, align: 'center'},
                 { display: '订单编号', name: 'number', width: 160, align: 'center' },
               
                 { display: '客户', name: 'wlName', width: 70, align: 'left'},
                 { display: '状态', name: 'getNumNo', width: 60, align: 'center',
                 
                      render: function (row) {  
                        var html="";
                        if(row.getNumNo<=0)
                        {
                            html = "全部出库";
                            
                        }
                        if (row.getNumNo != 0 && row.Num>row.getNum)
                        {
                            html = "部分出库";

                        }
                        if (row.getNumNo == row.Num )
                        {
                            html = "未出库";

                        }
                
                        return html;
                     }   
                 
                 },
                 { display: '订单数量', name: 'Num', width: 60, align: 'right',
                 
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
                 
                 { display: '系统库存', name: 'Num', width: 60, align: 'center',
                
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
                 
                
                { display: '可用库存', name: 'Num', width: 60, align: 'center',
                
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
                
              
                
                 { display: '采购状态', name: 'getNumNo', width: 60, align: 'center',
                 
                      render: function (row) {  
                        var html="";
                        if(row.getNumNo<=0)
                        {
                            html = "已采购";
                            
                        }
                       
                        if (row.getNumNo == row.Num )
                        {
                            html = "未采购";

                        }
                
                        return html;
                     }   
                 
                 },
                
                
             
               
                 { display: '交货日期', name: 'sendDate', width: 80, align: 'center' }
                
                
            
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
                
                  onRClickToSelect:true,
                onContextmenu : function (parm,e)
                {
                    actionCustomerID = parm.data.id;
                    menu.show({ top: e.pageY, left: e.pageX });
                    return false;
                } ,
                
                isChecked: f_isChecked, onCheckRow: f_onCheckRow, onCheckAllRow: f_onCheckAllRow
            }
            );

        });
        
           function lockNum() {

            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('请选要操作的择行'); return; }
            
             alert("锁定ItemId:"+row.id+" goodsId:"+row.goodsId+" 可用库存:"+(row.Num-1));
            
            return;
            
            var idStrring = checkedCustomer.join(',');//获取选中的ID字符串，用‘，’隔开，传递到后台即可
            
            alert("锁库ID"+idString);
            
           //  parent.f_addTab('PurOrderListAdd','采购订单-新增','buy/PurOrderListAdd.aspx?id='+idString);
            
          //  return;
          
          
          

//           $.ajax({
//                type: "GET",
//                url: "SalesOrderListItem.aspx",
//                data: "Action=checkRow&idString=" + idString + "&ranid=" + Math.random(), //encodeURI
//                success: function(resultString) {
//                    $.ligerDialog.alert(resultString, '提示信息');
//                    reload();

//                },
//                error: function(msg) {

//                    $.ligerDialog.alert("网络异常，请联系管理员", '提示信息');
//                }
//            });

                    
             
           
        } 
        
          function lockNumNo() {

            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('请选要操作的择行'); return; }
            
            
            alert("取消ItemId:"+row.id+" goodsId:"+row.goodsId+" 可用库存:"+(row.Num-1));
            
            return;
            
            var idString = checkedCustomer.join(',');//获取选中的ID字符串，用‘，’隔开，传递到后台即可
            
            alert("取消锁库ID"+idString);
            
           //  parent.f_addTab('PurOrderListAdd','采购订单-新增','buy/PurOrderListAdd.aspx?id='+idString);
            
          //  return;
          
          
          

//           $.ajax({
//                type: "GET",
//                url: "SalesOrderListItem.aspx",
//                data: "Action=checkRow&idString=" + idString + "&ranid=" + Math.random(), //encodeURI
//                success: function(resultString) {
//                    $.ligerDialog.alert(resultString, '提示信息');
//                    reload();

//                },
//                error: function(msg) {

//                    $.ligerDialog.alert("网络异常，请联系管理员", '提示信息');
//                }
//            });

                    
             
           
        } 
        
        
        function checkRow() {

            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('请选要操作的择行'); return; }
            
            var idString = checkedCustomer.join(',');//获取选中的ID字符串，用‘，’隔开，传递到后台即可
            
          //  alert(idString);
            
             parent.f_addTab('PurOrderListAdd','采购订单-新增','buy/PurOrderListAdd.aspx?id='+idString);
            
          //  return;
          
          
          

//           $.ajax({
//                type: "GET",
//                url: "SalesOrderListItem.aspx",
//                data: "Action=checkRow&idString=" + idString + "&ranid=" + Math.random(), //encodeURI
//                success: function(resultString) {
//                    $.ligerDialog.alert(resultString, '提示信息');
//                    reload();

//                },
//                error: function(msg) {

//                    $.ligerDialog.alert("网络异常，请联系管理员", '提示信息');
//                }
//            });

                    
             
           
        } 

        


        function search() {

          
            var start = $("#txtDateStart").val();
            var end = $("#txtDateEnd").val();
            
            var wlId = "";
            var goodsList = $("#txtGoodsList").val();
            var typeId = $("#txtFlagList").val();
            
            var wlIdString=wlId.split(";");
            var goodsIdString=goodsList.split(";");
            var typeIdString=typeId.split(";");
            
          
            
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
//            alert(typeId);
//          
//            alert(wlId);
//            alert(goodsList);

            //manager.changePage("first");
            //manager._setUrl("PurOrderList.aspx?Action=GetDataListSearch&types=0&keys=" + keys + "&start=" +start + "&end=" + end);
            manager._setUrl("SalesOrderListItem.aspx?Action=GetDataList&start="+start+"&end="+end+"&wlId="+wlId+"&goodsId="+goodsList+"&typeId="+typeId);
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
        
            
function makeBill()
{
    var row = manager.getSelectedRow();
    
     parent.f_addTab('SalesReceiptListAdd','销售出库-新增','sales/SalesReceiptListAdd.aspx?id='+row.id);
     
   
     top.topManager.openPage({
            id : 'SalesReceiptListAdds',
            href : 'sales/SalesReceiptListAdd.aspx?id='+row.id,
            title : '销售出库-新增'
          });
  
}
        
        
        function reload() {
            manager.reload();
        }


    
        function f_onCheckAllRow(checked) {
            for (var rowid in this.records) {
                if (checked)
                    addCheckedCustomer(this.records[rowid]['id']);
                else
                    removeCheckedCustomer(this.records[rowid]['id']);
            }
        }

        /*
        该例子实现 表单分页多选
        即利用onCheckRow将选中的行记忆下来，并利用isChecked将记忆下来的行初始化选中
        */
        var checkedCustomer = [];
        function findCheckedCustomer(id) {
            for (var i = 0; i < checkedCustomer.length; i++) {
                if (checkedCustomer[i] == id) return i;
            }
            return -1;
        }
        function addCheckedCustomer(id) {
            if (findCheckedCustomer(id) == -1)
                checkedCustomer.push(id);
        }
        function removeCheckedCustomer(id) {
            var i = findCheckedCustomer(id);
            if (i == -1) return;
            checkedCustomer.splice(i, 1);
        }
        function f_isChecked(rowdata) {
            if (findCheckedCustomer(rowdata.id) == -1)
                return false;
            return true;
        }
        function f_onCheckRow(checked, data) {
            if (checked) addCheckedCustomer(data.id);
            else removeCheckedCustomer(data.id);
        }
        function f_getChecked() {
            alert(checkedCustomer.join(','));
        }
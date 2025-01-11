     
        var manager;
        $(function() {
        
        var form = $("#form").ligerForm();


           var menu = $.ligerMenu({ width: 120, items:
            [
            { text: '新增', click: add, icon: 'add' },
            { text: '修改', click: editRow },
            { line: true },
            { text: '查看', click: viewRow },
             { line: true },
             
             { text: '生成销售订单', click: makeOrder },
             { line: true },
             
            { text: '生成PDF', click: makePDF },
             { line: true },
            { text: '查看PDF', click: makeBill }
            
            ]
            }); 
            
            
        
            manager = $("#maingrid").ligerGrid({
            checkbox: true,
                columns: [


                 { display: '操作', isSort: false, width: 50, align: 'center', render: function(rowdata, rowindex, value) {
                     var h = "";
                     if (!rowdata._editing) {
                         h += "<a href='javascript:editRow()' title='编辑行' style='float:left;'><div class='ui-icon ui-icon-pencil'></div></a> ";
                         h += "<a href='javascript:deleteRow()' title='删除行' style='float:right;'><div class='ui-icon ui-icon-trash'></div></a> ";
                     }
                     else {
                         h += "<a href='javascript:endEdit(" + rowindex + ")'>提交</a> ";
                         h += "<a href='javascript:cancelEdit(" + rowindex + ")'>取消</a> ";
                     }
                     return h;
                 }
                 },
                { display: '报价日期', name: 'bizDate', width: 80, align: 'center',valign:'center',
                 
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
                 { display: '报价编号', name: 'number', width: 150, align: 'center' },
                 { display: '客户', name: 'wlName', width: 170, align: 'left'},
                 
                 { display: '购货金额', name: 'sumPriceAll', width: 60, align: 'right',
                 
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
                 { display: '数量', name: 'sumNum', width: 60, align: 'center',
                 
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
                 
                 { display: '是否含税', name: 'isTax', width: 60, align: 'center'  },
                 { display: '运费承担', name: 'isFreight', width: 60, align: 'center'  },
                 { display: '付款方式', name: 'payWay', width: 60, align: 'center'  },
                 { display: '付款时间', name: 'payDate', width: 60, align: 'center'  },
                 { display: '交货周期', name: 'sendDate', width: 60, align: 'center' },
                 { display: '运输方式', name: 'freightWay', width: 60, align: 'center' },
                 { display: '包装方式', name: 'package', width: 60, align: 'center' },
                 { display: '报价状态', name: 'flag', width: 60, align: 'center'},
                 
                 { display: '制单人', name: 'makeName', width: 80, align: 'center' },
                 { display: '审核人', name: 'checkName', width: 80, align: 'center' },
                 { display: '备注', name: 'remarks', width: 100, align: 'left' }
                
            
                ], width: '98%', 
                  //pageSizeOptions: [5, 10, 15, 20],
                  height:'98%',
                 // pageSize: 15,
                  dataAction: 'local', //本地排序
                usePager: false,
                url: "SalesQuoteList.aspx?Action=GetDataList", 
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
        

        function f_set() {

            
            form.setData({
            
            keys:"",
            dateStart: new Date("<% =start%>"),
            dateEnd: new Date("<% =end%>")
        });


        }


        function search() {

            var keys = document.getElementById("txtKeys").value;
            if (keys == "请输入报价单号/客户/备注") {

                keys = "";

            }
            var start = document.getElementById("txtDateStart").value;
            var end = document.getElementById("txtDateEnd").value;
         

            manager.changePage("first");
            manager._setUrl("SalesQuoteList.aspx?Action=GetDataListSearch&keys=" + keys + "&start=" +start + "&end=" + end);
        }


       
        function deleteRow() {

            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('请选要删除的择行'); return; }
            
            var idString = checkedCustomer.join(',');//获取选中的ID字符串，用‘，’隔开，传递到后台即可

            $.ligerDialog.confirm('删除后不能恢复，确认删除？', function(type) {

              
                if (type) {
                
                    $.ajax({
                        type: "GET",
                        url: "SalesQuoteList.aspx",
                        data: "Action=delete&id=" + idString + " &ranid=" + Math.random(),
                        success: function(resultString) {
  
                           $.ligerDialog.alert(resultString, '提示信息');
                          
                            reload();
                        },
                        error: function(msg) {
                           
                            $.ligerDialog.alert("网络异常，请联系管理员", '提示信息');
                        }
                    });

                }

            });
             
           
        }
        
        
        function checkRow() {

            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('请选要操作的择行'); return; }
            
            var idString = checkedCustomer.join(',');//获取选中的ID字符串，用‘，’隔开，传递到后台即可

           $.ajax({
                type: "GET",
                url: "SalesQuoteList.aspx",
                data: "Action=checkRow&idString=" + idString + "&ranid=" + Math.random(), //encodeURI
                success: function(resultString) {
                    $.ligerDialog.alert(resultString, '提示信息');
                    reload();

                },
                error: function(msg) {

                    $.ligerDialog.alert("网络异常，请联系管理员", '提示信息');
                }
            });

                    
             
           
        }
        
        
         function checkNoRow() {

            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('请选要操作的择行'); return; }

            var idString = checkedCustomer.join(','); //获取选中的ID字符串，用‘，’隔开，传递到后台即可

            $.ajax({
                type: "GET",
                url: "SalesQuoteList.aspx",
                data: "Action=checkNoRow&idString=" + idString + "&ranid=" + Math.random(), //encodeURI
                success: function(resultString) {
                    $.ligerDialog.alert(resultString, '提示信息');
                    reload();

                },
                error: function(msg) {

                    $.ligerDialog.alert("网络异常，请联系管理员", '提示信息');
                }
            });


        }
        
      
      function add()
      {
      
          parent.f_addTab('SalesQuoteListAdd','销售报价-新增','sales/SalesQuoteListAdd.aspx');
          
          
  
      }
      
  function makeOrder()
      {
      
          var row = manager.getSelectedRow();
          var id=row.id;
    
          parent.f_addTab('SalesOrderListAdd','销售订单-新增','sales/SalesOrderListAdd.aspx?id='+row.id);
          
          
  
      }
      
function makePDF()
{
    var row = manager.getSelectedRow();
    
    var id=row.id;
    var number=row.number;
     $.ajax({
                type: "GET",
                url: "SalesQuoteList.aspx",
                data: "Action=makePDF&id=" + id +"&number="+number+ "&ranid=" + Math.random(), //encodeURI
                success: function(resultString) {
                   
                    
                    if(resultString=="生成成功！")
                    {
                       $.ligerDialog.confirm('生成成功，立即查看PDF？', function (yes) { 
                       
                          if(yes)
                          {
                              parent.f_addTab('pdf','销售报价-PDF','sales/pdf/'+number+'.pdf');
                          }
                          reload();
                       
                       
                        });
                       
                     
                      
                    }
                    else
                    {
                         $.ligerDialog.alert(resultString, '提示信息');
                        // reload();
                    }
                    
                   

                },
                error: function(msg) {

                    $.ligerDialog.alert("网络异常，请联系管理员", '提示信息');
                }
            });
    
    //parent.f_addTab('pdf','销售报价-PDF','sales/pdf/'+number+'.pdf');
     
  
}

      
function makeBill()
{
    var row = manager.getSelectedRow();
    
    var number=row.number;
    
    if(row.pdfURL =="")
    {
       $.ligerDialog.alert("还未生成PDF，请先点击生成！", '提示信息');
        return;
    
    }
    
    parent.f_addTab('pdf','销售报价-PDF','sales/pdf/'+number+'.pdf');
     
   
//     top.topManager.openPage({
//            id : 'SalesReceiptListAdds',
//            href : 'sales/SalesOrderListAdd.aspx?id='+row.id,
//            title : '销售订单-新增'
//          });
  
}


      
      function editRow()
      {
          var row = manager.getSelectedRow();
          
        
          parent.f_addTab('SalesQuoteListEdit','销售报价-修改','sales/SalesQuoteListEdit.aspx?id='+row.id);
        
        
        
     
  
  
      }
      
      function viewRow()
      {
          var row = manager.getSelectedRow();
         
          parent.f_addTab('SalesQuoteListEdit','销售报价-详情','sales/SalesQuoteListEdit.aspx?id='+row.id);
        
        
         
  
  
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
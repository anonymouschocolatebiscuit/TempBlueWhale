     
        var manager;
        $(function() {
        
        var form = $("#form").ligerForm();


           var menu = $.ligerMenu({ width: 120, items:
            [
            { text: '增加', click: add, icon: 'add' },
            { text: '修改', click: editRow },
            { line: true },
            {text: '打印预览', click: makePDF },
             { line: true }
           
            
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
                
                 { display: '出库日期', name: 'bizDate', width: 80, align: 'center',valign:'center',
                 
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
                 { display: '单据编号', name: 'number', width: 150, align: 'center' },
                 { display: '业务类别', name: 'types', width: 70, align: 'center' ,
                 
                     render: function (row) {  
                      var html = row.types == 1 ? "购货" : "<span style='color:green'>退货</span>";  
                      return html;
                     }   
                 
                 
                 },
                 { display: '客户', name: 'wlName', width: 170, align: 'left'},
                  { display: '销售数量', name: 'sumNum', width: 100, align: 'right',
                 
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
                
                 { display: '销售金额', name: 'sumPriceAll', width: 100, align: 'right',
                 
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
                 { display: '已收款金额', name: 'priceCheckNowSum', width: 100, align: 'right',
                 
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
               
                 { display: '折扣%', name: 'dis', width: 60, align: 'center' },
                 {
                     display: '折扣金额', name: 'disPrice', width: 70, align: 'right',

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


                 { display: '状态', name: 'flag', width: 60, align: 'center'},
                 
                 { display: '物流公司', name: 'sendName', width: 80, align: 'center'},
                 { display: '物流单号', name: 'sendNumber', width: 100, align: 'center'},
              
                 { display: '制单人', name: 'makeName', width: 70, align: 'center' },
                 { display: '销售人', name: 'bizName', width: 70, align: 'center' },
                 { display: '审核人', name: 'checkName', width: 70, align: 'center' },
                 { display: '备注', name: 'remarks', width: 100, align: 'left' }
                
            
                ], width: '98%', 
                  //pageSizeOptions: [5, 10, 15, 20],
                  height:'98%',
                 // pageSize: 15,
                  dataAction: 'local', //本地排序
                usePager: false,
                url: "SalesReceiptList.aspx?Action=GetDataList", 
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
        

        function makePDF() {

            var shopId = $("#hfShopId").val();

            var row = manager.getSelectedRow();

            var id = row.id;
            var number = row.number;
            $.ajax({
                type: "GET",
                url: "SalesReceiptList.aspx",
                data: "Action=makePDF&id=" + id + "&number=" + number + "&ranid=" + Math.random(), //encodeURI
                success: function (resultString) {


                    if (resultString == "生成成功！") {

                        parent.f_addTab('pdf', '销售出库-打印预览', 'sales/pdf/' + shopId + '-' + number + '.pdf');

                        //$.ligerDialog.confirm('生成成功，立即查看PDF？', function (yes) {

                        //    if (yes) {
                                
                        //    }
                        //    reload();


                        //});



                    }
                    else {
                        $.ligerDialog.alert(resultString, '提示信息');
                        // reload();
                    }



                },
                error: function (msg) {

                    $.ligerDialog.alert("网络异常，请联系管理员", '提示信息');
                }
            });

            //parent.f_addTab('pdf','销售报价-PDF','sales/pdf/'+number+'.pdf');


        }


        function makeBill() {
            var row = manager.getSelectedRow();

            var number = row.number;

            if (row.pdfURL == "") {
                $.ligerDialog.alert("还未生成PDF，请先点击生成！", '提示信息');
                return;

            }

            parent.f_addTab('pdf', '销售报价-PDF', 'sales/pdf/' + number + '.pdf');


            //     top.topManager.openPage({
            //            id : 'SalesReceiptListAdds',
            //            href : 'sales/SalesOrderListAdd.aspx?id='+row.id,
            //            title : '销售订单-新增'
            //          });

        }


        function f_set() {

            
            form.setData({
            
            keys:"",
            dateStart: new Date("<% =start%>"),
            dateEnd: new Date("<% =end%>")
        });


        }


        function search() {

            var keys = document.getElementById("txtKeys").value;
            if (keys == "请输入单据号/客户/备注") {

                keys = "";

            }
            var start = document.getElementById("txtDateStart").value;
            var end = document.getElementById("txtDateEnd").value;

            manager.changePage("first");
            manager._setUrl("SalesReceiptList.aspx?Action=GetDataListSearch&types=0&keys=" + keys + "&start=" +start + "&end=" + end);
        }


       
        function deleteRow() {

            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('请选要删除的择行'); return; }
            
            var idString = checkedCustomer.join(',');//获取选中的ID字符串，用‘，’隔开，传递到后台即可

            $.ligerDialog.confirm('删除后不能恢复，确认删除？', function(type) {

              
                if (type) {
                
                    $.ajax({
                        type: "GET",
                        url: "SalesReceiptList.aspx",
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
                url: "SalesReceiptList.aspx",
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
                url: "SalesReceiptList.aspx",
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
      
        parent.f_addTab('SalesReceiptListAdd','销售出库-新增','sales/SalesReceiptListAdd.aspx?id=0');
        
          top.topManager.openPage({
            id : 'SalesReceiptListAdd',
            href : 'sales/SalesReceiptListAdd.aspx?id=0',
            title : '销售出库-新增'
          });
  
  
      }
      
      function editRow()
      {
          var row = manager.getSelectedRow();
         
          parent.f_addTab('SalesReceiptListEdit','销售出库-修改','sales/SalesReceiptListEdit.aspx?id='+row.id);
          
          top.topManager.openPage({
            id : 'SalesReceiptListEdit',
            href : 'sales/SalesReceiptListEdit.aspx?id='+row.id,
            title : '销售出库-修改'
          });
  
  
      }
      
      function viewRow()
      {
          var row = manager.getSelectedRow();
          
          top.topManager.openPage({
            id : 'SalesReceiptListView',
            href : 'sales/SalesReceiptListView.aspx?id='+row.id,
            title : '销售出库-详情'
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
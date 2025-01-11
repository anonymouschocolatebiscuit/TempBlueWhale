     
        var manager;
        $(function() {
        
        var form = $("#form").ligerForm();


           var menu = $.ligerMenu({ width: 120, items:
            [
            { text: '增加', click: add, icon: 'add' },
            { text: '修改', click: editRow },
            { line: true },
            { text: '查看', click: editRow },

            { line: true },
            { text: '生产领料', click: getRow },

            { line: true },
            { text: '生产入库', click: inRow }
          
            
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
                
                 { display: '制单日期', name: 'makeDate', width: 80, align: 'center',valign:'center',
                 
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
                 {display: '开始日期', name: 'dateStart', width: 80, align: 'center', valign: 'center'},
                 {display: '结束日期', name: 'dateEnd', width: 80, align: 'center', valign: 'center',

                  
                 },
                 { display: '单据编号', name: 'number', width: 150, align: 'center' },
                 { display: '计划类别', name: 'typeName', width: 80, align: 'center'},   
                 { display: '订单编号', name: 'orderNumber', width: 150, align: 'center' },                
                
                 { display: '客户', name: 'wlName', width: 170, align: 'left' },

                 { display: '商品编号', name: 'code', width: 100, align: 'center' },
                 { display: '商品名称', name: 'goodsName', width: 120, align: 'center' },
                 { display: '规格', name: 'spec', width: 100, align: 'center' },
                 { display: '单位', name: 'unitName', width: 70, align: 'center' },

                 { display: '数量', name: 'num', width: 80, align: 'center',
                 
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
                 { display: '状态', name: 'flag', width: 80, align: 'center'},
             
                 { display: '制单人', name: 'makeName', width: 80, align: 'center' },
                 { display: '审核人', name: 'checkName', width: 80, align: 'center' },
                 { display: '备注', name: 'remarks', width: 100, align: 'left' }
                
            
                ], width: '1120', 
                  //pageSizeOptions: [5, 10, 15, 20],
                  height:'98%',
                 // pageSize: 15,
                  dataAction: 'local', //本地排序
                usePager: false,
                url: "produceList.aspx?Action=GetDataList",
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

        function getRow() {

            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('请选要操作的择行'); return; }

            var idString = checkedCustomer.join(',');//获取选中的ID字符串，用‘，’隔开，传递到后台即可

            //  alert(idString);

            parent.f_addTab('produceGetListAdd', '生产领料-新增', 'produce/produceGetListAdd.aspx?id=' + idString);

            //  return;


        }


        function inRow() {

            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('请选要操作的择行'); return; }

            var idString = checkedCustomer.join(',');//获取选中的ID字符串，用‘，’隔开，传递到后台即可

            //  alert(idString);

            parent.f_addTab('produceInListAdd', '生产入库-新增', 'produce/produceInListAdd.aspx?id=' + idString);

            //  return;


        }

        function search() {

            var keys = document.getElementById("txtKeys").value;
            if (keys == "请输入单据号/客户/商品/备注") {

                keys = "";

            }
            var start = document.getElementById("txtDateStart").value;
            var end = document.getElementById("txtDateEnd").value;
         

            manager.changePage("first");
            manager._setUrl("produceList.aspx?Action=GetDataListSearch&types=0&keys=" + keys + "&start=" + start + "&end=" + end);
        }


       
        function deleteRow() {

            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('请选要删除的择行'); return; }
            
            var idString = checkedCustomer.join(',');//获取选中的ID字符串，用‘，’隔开，传递到后台即可

            $.ligerDialog.confirm('删除后不能恢复，确认删除？', function(type) {

              
                if (type) {
                
                    $.ajax({
                        type: "GET",
                        url: "produceList.aspx",
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
                url: "produceList.aspx",
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
                url: "produceList.aspx",
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
          parent.f_addTab('produceListAdd', '生产计划-新增', 'produce/produceListAdd.aspx');
            
          top.topManager.openPage({
              id: 'produceListAdd',
              href: 'produce/produceListAdd.aspx',
              title: '生产计划-新增'
          });
  
  
      }
      
      

      
      function editRow()
      {
          var row = manager.getSelectedRow();
          
          parent.f_addTab('produceListEdit', '生产计划-修改', 'produce/produceListEdit.aspx?id=' + row.id);
          
          top.topManager.openPage({
              id: 'produceListEdit',
              href: 'produce/produceListEdit.aspx?id=' + row.id,
              title: '生产计划-修改'
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
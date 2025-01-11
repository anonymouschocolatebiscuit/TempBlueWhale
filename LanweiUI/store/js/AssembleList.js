        function search() {

            var keys = document.getElementById("txtKeys").value;
            if (keys == "请输单据号/组装商品/备注") {

                keys = "";

            }
            var start = document.getElementById("txtDateStart").value;
            var end = document.getElementById("txtDateEnd").value;
         

            manager.changePage("first");
            manager._setUrl("AssembleList.aspx?Action=GetDataListSearch&types=0&keys=" + keys + "&start=" +start + "&end=" + end);
        }
       
        function reload() {
            manager.reload();
            managersub.reload();
        }
        
        
           function editRow()
      {
          var row = manager.getSelectedRow();
          
           parent.f_addTab('AssembleListEdit','商品组装单-修改', 'store/AssembleListEdit.aspx?id='+row.id);
          
          top.topManager.openPage({
            id : 'AssembleListEdit',
            href : 'store/AssembleListEdit.aspx?id='+row.id,
            title : '商品组装单-修改'
          });
  
  
      }
       
        function deleteRow() {

            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('请选要删除的择行'); return; }
            
            var idString = checkedCustomer.join(',');//获取选中的ID字符串，用‘，’隔开，传递到后台即可

            $.ligerDialog.confirm('删除后不能恢复，确认删除？', function(type) {

              
                if (type) {
                
                    $.ajax({
                        type: "GET",
                        url: "AssembleList.aspx",
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
                url: "AssembleList.aspx",
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
                url: "AssembleList.aspx",
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
         
         parent.f_addTab('AssembleListAdd','商品组装-新增', 'store/AssembleListAdd.aspx');
         
          top.topManager.openPage({
            id : 'AssembleListAdd',
            href : 'store/AssembleListAdd.aspx',
            title : '商品组装-新增'
          });
  
  
      }
        
        
     
                    
    var manager;
        $(function ()
        {
        
          var form = $("#form").ligerForm();
          
          
          
            window['g'] = 
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
                
                 { display: '组装日期', name: 'bizDate', width: 70, align: 'center',valign:'center'},
                 { display: '单据编号', name: 'number', width: 140, align: 'center' },
               
                { display: '组合商品', name: 'goodsName', width: 200, align: 'left'},
                
                { display: '规格', name: 'spec',width: 80, align: 'center' },
                
                { display: '单位', name: 'unitName',width: 60, align: 'center' },
                { display: '入库数量', name: 'num', width: 70, type: 'float', align: 'right' },
                
                { display: '入库单价', name: 'price', width: 70, type: 'float', align: 'right'
                
                },

                { display: '金额', name: 'sumPrice', width: 80, type: 'float', align: 'right' },
             
                { display: '入库仓库', name: 'ckId', width: 90, isSort: false,textField:'ckName',
                    editor: { type: 'select',
                              url:"../baseSet/CangkuList.aspx?Action=GetDDLList&r=" + Math.random(), 
                              valueField: 'ckId',textField:'ckName'}

                },
                
                { display: '状态', name: 'flag', width: 60, align: 'center'},
             
                 { display: '制单人', name: 'makeName', width: 60, align: 'center' },
                 { display: '审核人', name: 'checkName', width: 60, align: 'center' },
                
                
                { display: '备注', name: 'remarks', width: 100, align: 'left',type:'text' }
                ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '250',
               // url: 'DisassembleList.aspx?Action=GetData',
               rownumbers:true,//显示序号
               
                onDblClickRow: function(data, rowindex, rowobj) {
                    // $.ligerDialog.alert('选择的是' + data.id);
                     editRow();
                },
               
               frozenRownumbers:true,//行序号是否在固定列中
                dataAction: 'local',//本地排序
                usePager:true,
                allowUnSelectRow:true,
                onSelectRow: function (data, rowindex, rowobj)
                {
                    //$.ligerDialog.alert('选择的是' + data.id);
                    getItemList(data.id);
                },
                totalSummary:false,
                 isChecked: f_isChecked, onCheckRow: f_onCheckRow, onCheckAllRow: f_onCheckAllRow
            }
            );
        });
 
 
       var managersub;
        $(function ()
        {

            window['gsub'] = 
            managersub = $("#maingridsub").ligerGrid({
                columns: [

                { display: '拆分商品名称', name: 'goodsName', width: 200, align: 'left',
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
                
                { display: '规格', name: 'spec',width: 80, align: 'center' },
                
                { display: '单位', name: 'unitName',width: 80, align: 'center' },
                { display: '出库数量', name: 'num', width: 80, type: 'float', align: 'right',
                
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
                
                { display: '出库单价', name: 'price', width: 70, type: 'float', align: 'right'},                
                { display: '金额', name: 'sumPrice', width: 80, type: 'float', align: 'right',
                  totalSummary:
                    {
                        align: 'center',   //汇总单元格内容对齐方式:left/center/right 
                        type: 'sum',
                        render: function (e) 
                        {  //汇总渲染器，返回html加载到单元格
                         //e 汇总Object(包括sum,max,min,avg,count) 
                         
                         // alert("汇总了");
                          
                            return Math.round(e.sum*100)/100;  
                        }
                    }

                },
                
                { display: '出库仓库', name: 'ckId', width: 100, isSort: false,textField:'ckName',
                    editor: { type: 'select',
                              url:"../baseSet/CangkuList.aspx?Action=GetDDLList&r=" + Math.random(), 
                              valueField: 'ckId',textField:'ckName'}

                },
                
                { display: '备注', name: 'remarks', width: 150, align: 'left',type:'text' }
                ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '210',
               // url: 'DisassembleListAdd.aspx?Action=GetDataSub',
               rownumbers:true,//显示序号
               frozenRownumbers:true,//行序号是否在固定列中
                dataAction: 'local',//本地排序
                usePager:false,
                alternatingRow: false,
                
                totalSummary:true
              
            }
            );
        });
 
 
        var rowNumber=9;
        
        function getItemList(pId)
        {
            if(pId!=0)
            {
               
               managersub._setUrl("AssembleList.aspx?Action=GetDataListSearchSub&pId=" + pId);
            }
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
 

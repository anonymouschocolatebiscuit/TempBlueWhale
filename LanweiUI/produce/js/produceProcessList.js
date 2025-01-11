     
        var manager;
        $(function() {
        
        var form = $("#form").ligerForm();


         
            
        
            manager = $("#maingrid").ligerGrid({
            checkbox: true,
                columns: [


                
                
                 { display: '生产日期', name: 'bizDate', width: 80, align: 'center',valign:'center',
                 
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
                 { display: '生产员工', name: 'bizName', width: 80, align: 'center' },

                 { display: '生产工序', name: 'processName', width: 80, align: 'center', valign: 'center' },
                 {
                     display: '工序单位', name: 'unitName', width: 70, align: 'center', valign: 'center',

                  
                 },
                 {
                     display: '数量', name: 'num', width: 80, align: 'right',

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

                 { display: '单价', name: 'price', width: 60, align: 'right' },
                 {
                     display: '金额', name: 'sumPrice', width: 80, align: 'right',
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



                 { display: '所属商品名称', name: 'goodsName', width: 120, align: 'center' },
                 { display: '规格', name: 'spec', width: 100, align: 'center' },

                
                 { display: '所属客户', name: 'wlName', width: 170, align: 'left' },
                 { display: '所属订单编号', name: 'orderNumber', width: 150, align: 'center' },

                
             
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
                url: "produceProcessList.aspx?Action=GetDataList",
                alternatingRow: false,
                rownumbers: true, //显示序号
                
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
            if (keys == "请输入工序/员工/商品/客户/备注") {

                keys = "";

            }
            var start = document.getElementById("txtDateStart").value;
            var end = document.getElementById("txtDateEnd").value;
         

            manager.changePage("first");
            manager._setUrl("produceProcessList.aspx?Action=GetDataListSearch&types=0&keys=" + keys + "&start=" + start + "&end=" + end);
        }


       
        function deleteRow() {

            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('请选要删除的择行'); return; }
            
            var idString = checkedCustomer.join(',');//获取选中的ID字符串，用‘，’隔开，传递到后台即可

            $.ligerDialog.confirm('删除后不能恢复，确认删除？', function(type) {

              
                if (type) {
                
                    $.ajax({
                        type: "GET",
                        url: "produceProcessList.aspx",
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
                url: "produceProcessList.aspx",
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
                url: "produceProcessList.aspx",
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
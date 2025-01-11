     
        var manager;
        $(function () {



            var bizTypeData =
          [
          { id: 0, text: '全部类别' },
          { id: 1, text: '其他收入' },
          { id: 2, text: '其他支出' }
          ];






            $("#txtItemList").ligerComboBox({ data: null, isMultiSelect: true, isShowCheckBox: true, valueFieldID: 'itemId' });



            $("#ddlTypeList").ligerComboBox({
                //data: bizTypeData,
                isMultiSelect: false,
                valueFieldID: 'bizTypeId',
                onSelected: function (newvalue) {                    

                    liger.get("txtItemList")._setUrl("../baseSet/ProcessList.aspx?Action=GetDDLList&typeId=" + newvalue + "&r=" + Math.random());
                }
            });



        
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
                 { display: '工序类别', name: 'typeName', width: 100, align: 'center', valign: 'center' },
                 { display: '工序名称', name: 'processName', width: 120, align: 'center', valign: 'center' },
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

          
            var start = $("#txtDateStart").val();
            var end = $("#txtDateEnd").val();


            var typeId = $("#bizTypeId").val();


          

            var itemId = $("#itemId").val();

            var bizId = $("#ddlUserList").val();


            var typeIdString = itemId.split(";");


            if (typeIdString != "") {
                itemId = "";
                for (var i = 0; i < typeIdString.length; i++) {
                    itemId += typeIdString[i] + ",";
                }
                itemId = itemId.substring(0, itemId.length - 1);

            }

            //          
            //   alert(bizType);
            //          
            //  alert(typeId);

            var url = "ProduceProcessListDetailReport.aspx?Action=GetDataList&start=" + start + "&end=" + end + "&typeId=" + typeId + "&itemIdString=" + itemId + "&bizId=" + bizId;

            //  alert(url);
            // 

           
            manager.changePage("first");
            manager._setUrl(url);

            //window.open(url);

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
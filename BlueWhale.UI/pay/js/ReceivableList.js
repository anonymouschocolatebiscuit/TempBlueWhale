     
        var manager;
        $(function() {
        
        var form = $("#form").ligerForm();


          
      
            manager = $("#maingrid").ligerGrid({
            checkbox: true,
                columns: [


                 { display: 'Action', isSort: false, width: 70, align: 'center', render: function(rowdata, rowindex, value) {
                     var h = "";
                     if (!rowdata._editing) {
                         h += "<a href='javascript:editRow()' title='EditRow' style='float:left;'><div class='ui-icon ui-icon-pencil'></div></a> ";
                         h += "<a href='javascript:deleteRow()' title='DeleteRow' style='float:right;'><div class='ui-icon ui-icon-trash'></div></a> ";
                     }
                     else {
                         h += "<a href='javascript:endEdit(" + rowindex + ")'>Submit</a> ";
                         h += "<a href='javascript:cancelEdit(" + rowindex + ")'>Cancel</a> ";
                     }
                     return h;
                 }
                 },
                { display: 'Payment Date', name: 'bizDate', width: 140, align: 'center',valign:'center',
                 
                     totalSummary:
                    {
                        type: 'count',
                        render: function (e) 
                        {  // Summary Renderer，returns html loaded into cell
                           // Summary Object(include sum,max,min,avg,count)
                            return 'Total: ';
                        }
                    }
                 
                 },
                 { display: 'Receipt Number', name: 'number', width: 150, align: 'center' },
                 
                 { display: 'Sales Unit', name: 'wlName', width: 170, align: 'left'},
                 { display: 'Payment Amount', name: 'payPriceSum', width: 140, align: 'right',
                 
                      totalSummary:
                    {
                        align: 'right',   //Alignment of summary cell content:left/center/right 
                        type: 'sum',
                        render: function (e) 
                        {  // Summary Renderer，returns html loaded into cell
                           // Summary Object(include sum,max,min,avg,count)
                            return  Math.round(e.sum*100)/100;
                        }
                    }
                 
                 },
                 { display: 'Amount Written Off This Time', name: 'priceCheckNowSum', width: 270, align: 'right',
                 
                     totalSummary:
                    {
                         align: 'right',    //Alignment of summary cell content:left/center/right 
                        type: 'sum',
                        render: function (e) 
                        {  // Summary Renderer，returns html loaded into cell
                           // Summary Object(include sum,max,min,avg,count)
                            return  Math.round(e.sum*100)/100;
                        }
                    }
                 
                 },
                 { display: 'Overall Discount', name: 'disPrice', width: 150, align: 'right',
                 
                     totalSummary:
                    {
                         align: 'right',   //Alignment of summary cell content:left/center/right 
                        type: 'sum',
                        render: function (e) 
                        {  // Summary Renderer，returns html loaded into cell
                           // Summary Object(include sum,max,min,avg,count)
                            return  Math.round(e.sum*100)/100;
                        }
                    }
                 
                 },
                 
                 { display: 'Advance Payment Received This Time', name: 'payPriceNowMore', width: 270, align: 'right',
                 
                     totalSummary:
                    {
                        align: 'right',  //Alignment of summary cell content:left/center/right 
                        type: 'sum',
                        render: function (e) 
                        {  // Summary Renderer，returns html loaded into cell
                           // Summary Object(include sum,max,min,avg,count)
                            return Math.round(e.sum * 100) / 100;
                        }
                    }
                 
                 },
                 
                 { display: 'Status', name: 'flag', width: 80, align: 'center'},
                
                 { display: 'Created By', name: 'makeName', width: 90, align: 'center' },
                 { display: 'Reviewed By', name: 'checkName', width: 90, align: 'center' },
                 { display: 'Remarks', name: 'remarks', width: 100, align: 'left' }
                
            
                ], width: '98%', 
                  //pageSizeOptions: [5, 10, 15, 20],
                  height:'98%',
                 // pageSize: 15,
                  dataAction: 'local', //Local sorting
                usePager: false,
                //url: "ReceivableList.aspx?Action=GetDataList", 
                alternatingRow: false,
                onDblClickRow: function(data, rowindex, rowobj) {
                    // $.ligerDialog.alert('The selected one is' + data.id);
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
        

      


        function search() {

            var keys = document.getElementById("txtKeys").value;
            if (keys == "Please Enter Receipt No./Client/Remarks") {

                keys = "";

            }
            var start = document.getElementById("txtDateStart").value;
            var end = document.getElementById("txtDateEnd").value;
         

            manager.changePage("first");
            manager._setUrl("ReceivableList.aspx?Action=GetDataListSearch&keys=" + keys + "&start=" +start + "&end=" + end);
        }


       
        function deleteRow() {

            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('Please select the rows you want to delete'); return; }
            
            var idString = checkedCustomer.join(',');// Get the selected ID string, separated by commas, and pass it to the backend

            $.ligerDialog.confirm('Deletion cannot be restored. Confirm deletion?', function(type) {

              
                if (type) {
                
                    $.ajax({
                        type: "GET",
                        url: "ReceivableList.aspx",
                        data: "Action=delete&id=" + idString + " &ranid=" + Math.random(),
                        success: function(resultString) {
  
                           $.ligerDialog.alert(resultString, 'Notification');
                          
                            reload();
                        },
                        error: function(msg) {
                           
                            $.ligerDialog.alert("Network error, please contact the administrator", 'Notification');
                        }
                    });

                }

            });
             
           
        }
        
        
        function checkRow() {

            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('Please select the row to operate on'); return; }
            
            var idString = checkedCustomer.join(',');// Get the selected ID string, separated by commas, and pass it to the backend

           $.ajax({
                type: "GET",
                url: "ReceivableList.aspx",
                data: "Action=checkRow&idString=" + idString + "&ranid=" + Math.random(), //encodeURI
                success: function(resultString) {
                    $.ligerDialog.alert(resultString, 'Notification');
                    reload();

                },
                error: function(msg) {

                    $.ligerDialog.alert("Network error, please contact the administrator", 'Notification');
                }
            });

                    
             
           
        }
        
        
         function checkNoRow() {

            var row = manager.getSelectedRow();
             if (!row) { $.ligerDialog.warn('Please select the row to operate on'); return; }

            var idString = checkedCustomer.join(','); // Get the selected ID string, separated by commas, and pass it to the backend

            $.ajax({
                type: "GET",
                url: "ReceivableList.aspx",
                data: "Action=checkNoRow&idString=" + idString + "&ranid=" + Math.random(), //encodeURI
                success: function(resultString) {
                    $.ligerDialog.alert(resultString, 'Notification');
                    reload();

                },
                error: function(msg) {

                    $.ligerDialog.alert("Network error, please contact the administrator", 'Notification');
                }
            });


        }
        
      
      function add()
      {
      
          parent.f_addTab('ReceivableListAdd','Sales Payment - Add New','pay/ReceivableListAdd.aspx');
           
          top.topManager.openPage({
            id : 'ReceivableListAdd',
            href : 'pay/ReceivableListAdd.aspx',
            title : 'Sales Payment - Add New'
          });
  
  
      }
      
      

      function editRow()
      {
          var row = manager.getSelectedRow();
         
          parent.f_addTab('ReceivableListEdit','Sales Collection - Edit','pay/ReceivableListEdit.aspx?id='+row.id);
         
         
          top.topManager.openPage({
            id : 'ReceivableListEdit',
            href : 'pay/ReceivableListEdit.aspx?id='+row.id,
              title: 'Sales Collection - Edit'
          });
  
  
      }
   
     function viewRow()
      {
          var row = manager.getSelectedRow();
          
           parent.f_addTab('ReceivableListEdit','Sales Payment - Details','pay/ReceivableListEdit.aspx?id='+row.id);
         
          
         
  
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
        This example implements form pagination with multi-selection, by using onCheckRow to remember the selected rows, 
        and using isChecked to initialize the previously remembered rows as selected
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
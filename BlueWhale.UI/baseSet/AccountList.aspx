<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountList.aspx.cs" Inherits="BlueWhale.UI.baseSet.AccountList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Account Management</title>
    
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
    
    <script type="text/javascript">

      
       var manager;
        $(function ()
        {
            manager = $("#maingrid").ligerGrid({
                columns: [
                
                { display: 'Account Number', name: 'code', width: 180, type: 'text', align: 'left' },
                
                { display: 'Account Name', name: 'names', width: 250, align: 'left',type:'text' },
                { display: 'Account Balance', name: 'yuePrice', width: 150, align: 'right',type:'float' },
                { display: 'Balance Date', name: 'yueDate', width: 100, align: 'center',type:'date',format:"yyyy-MM-dd"},
                { display: 'Account Type', name: 'types', width: 150, align: 'center',type:'text' }
            
                ], width: '99%', pageSizeOptions: [5, 10, 15, 20],height: '99%',
                url: 'AccountList.aspx?Action=GetDataList', 
                alternatingRow: false,               
                rownumbers: true, // Show serial number
                
                 onDblClickRow: function(data, rowindex, rowobj) {
                     // $.ligerDialog.alert('Selected ID is ' + data.id);
                     editRow();
                },
                
                toolbar: { items: [
               
                { text: 'Refresh', click: reload, img: '../lib/ligerUI/skins/icons/refresh.png'},
                { line: true },
               
                { text: "Add", click: addRow,img: '../lib/ligerUI/skins/icons/add.gif'},
                { line: true },
               
                { text: "Edit", click:editRow,img: '../lib/ligerUI/skins/icons/modify.gif'},
                { line: true },
                
               { text: "Delete", click: deleteRow,img: '../lib/ligerUI/skins/icons/delete.gif'}

                ]
                } 
            }
            );
        });                             
        
        //Only allow editing the first 3 rows
        function f_onBeforeEdit(e)
        { 
            if(e.rowindex<=2) return true;
            return false;
        }
        // Limit age
        function f_onBeforeSubmitEdit(e)
        { 
            if (e.column.name == "dis")
            {
                if (e.value < 0 || e.value > 100) return false;
            }
            return true;
        }

       function editRow(id)
       {
          //alert("The selected value is："+id);
          
          f_open(id);
          
          
       }

         function reload()
        {
            manager.reload();
        }
        
        function deleteRow()
        {
           
             var row = manager.getSelectedRow();
            if (!row) {  $.ligerDialog.warn('Please select the row to delete'); return; }


              $.ligerDialog.confirm('Deletion cannot be undone, are you sure you want to delete?', function(type) {

                if (type) {

                    $.ajax({
                        type: "GET",
                        url: "AccountList.aspx",
                        data: "Action=delete&id=" + row.id + "&ranid=" + Math.random(), //encodeURI
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
        
         function editRow()
        {
            var row = manager.getSelectedRow();
            if (!row) {  $.ligerDialog.warn('Please select the row to edit!'); return; }
            
            var title="Edit Account";
           
            $.ligerDialog.open({ 
                title : title,
                url: "AccountListAdd.aspx?id="+row.id+"&names="+row.names+"&code="+row.code,
                 height:350,
                width:400,
                modal:true
            });
            


        }
        
        function deleteRow()
        {
           
             var row = manager.getSelectedRow();
            if (!row) {  $.ligerDialog.warn('Please select the row to delete'); return; }


              $.ligerDialog.confirm('Deletion cannot be undone, are you sure you want to delete?', function(type) {

                if (type) {

                    $.ajax({
                        type: "GET",
                        url: "AccountList.aspx",
                        data: "Action=delete&id=" + row.id + "&ranid=" + Math.random(), //encodeURI
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
          function addRow()
        {
             
            var title="Add New Account";
         
            
            $.ligerDialog.open({ 
                title : title,
                url: 'AccountListAdd.aspx',
                height:350,
                width:400,
                modal:true
            });
          
        } 
        
    </script>
    
</head>
<body style="padding-left:10px; padding-top:10px;">
    <form id="form1" runat="server">
    
    
       <div id="maingrid"></div>  
            <div style="display:none;"></div>
    
    
    </form>
</body>
</html>

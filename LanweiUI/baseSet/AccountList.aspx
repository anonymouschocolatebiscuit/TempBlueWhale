<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountList.aspx.cs" Inherits="Lanwei.Weixin.UI.baseSet.AccountList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>账户管理</title>
    
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
                
                
                { display: '账户编号', name: 'code', width: 180, type: 'text', align: 'left' },
                
                { display: '账户名称', name: 'names', width: 250, align: 'left',type:'text' },
                { display: '账户余额', name: 'yuePrice', width: 150, align: 'right',type:'float' },
                { display: '余额日期', name: 'yueDate', width: 100, align: 'center',type:'date',format:"yyyy-MM-dd"},
                { display: '账户类别', name: 'types', width: 150, align: 'center',type:'text' }
            
                ], width: '99%', pageSizeOptions: [5, 10, 15, 20],height: '99%',
               
                url: 'AccountList.aspx?Action=GetDataList', 
                alternatingRow: false,               
                rownumbers: true, //显示序号
                
                 onDblClickRow: function(data, rowindex, rowobj) {
                    // $.ligerDialog.alert('选择的是' + data.id);
                     editRow();
                },
                
                toolbar: { items: [
               
                { text: '刷新', click: reload, img: '../lib/ligerUI/skins/icons/refresh.png'},
                { line: true },
               
                { text: "添加", click: addRow,img: '../lib/ligerUI/skins/icons/add.gif'},
                { line: true },
               
                { text: "修改", click:editRow,img: '../lib/ligerUI/skins/icons/modify.gif'},
                { line: true },
                
              
               
                { text: "删除", click: deleteRow,img: '../lib/ligerUI/skins/icons/delete.gif'}

                ]
                }

                
                
            }
            );
        });
        
                                          
     

        
        //只允许编辑前3行
        function f_onBeforeEdit(e)
        { 
            if(e.rowindex<=2) return true;
            return false;
        }
        //限制年龄
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
          //alert("选择的值是："+id);
          
          f_open(id);
          
          
       }

      
         function reload()
        {
            manager.reload();
        }
        
        function deleteRow()
        {
           
             var row = manager.getSelectedRow();
            if (!row) {  $.ligerDialog.warn('请选要删除的择行'); return; }


              $.ligerDialog.confirm('删除后不能恢复，确认删除？', function(type) {

                if (type) {

                    $.ajax({
                        type: "GET",
                        url: "AccountList.aspx",
                        data: "Action=delete&id=" + row.id + "&ranid=" + Math.random(), //encodeURI
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
        
         function editRow()
        {
            var row = manager.getSelectedRow();
            if (!row) {  $.ligerDialog.warn('请选择修改的行！'); return; }
            
            var title="修改账户";
           
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
            if (!row) {  $.ligerDialog.warn('请选要删除的择行'); return; }


              $.ligerDialog.confirm('删除后不能恢复，确认删除？', function(type) {

                if (type) {

                    $.ajax({
                        type: "GET",
                        url: "AccountList.aspx",
                        data: "Action=delete&id=" + row.id + "&ranid=" + Math.random(), //encodeURI
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
          function addRow()
        {
             
            var title="新增账户";
         
            
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

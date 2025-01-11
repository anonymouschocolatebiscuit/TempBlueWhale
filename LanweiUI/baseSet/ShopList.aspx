<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopList.aspx.cs" Inherits="Lanwei.Weixin.UI.baseSet.ShopList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>分店管理</title>
     <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
    
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script> 
    <script src="../lib/ligerUI/js/core/base.js" type="text/javascript"></script> 
     
      <script src="../lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>

   <script src="../lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>

   

    
       <script type="text/javascript">
      
        var manager;
        $(function ()
        {
            manager = $("#maingrid").ligerGrid({
                columns: [


               
                 { display: '编号', name: 'code', width: 100, align: 'center' },
                 { display: '名称', name: 'names', width: 200, align: 'center' },
                 { display: '地址', name: 'address', width: 300, align: 'left'},
                  { display: '电话', name: 'tel', width: 120, align: 'center' },
                  { display: '传真', name: 'fax', width: 120, align: 'center'},
                  { display: '状态', name: 'flag', width: 120, align: 'center'},
                  
                  { display: '添加日期', name: 'makeDate', width: 80, align: 'center', type: "date" }
            
                
               
            
                ], width: '99%', pageSizeOptions: [5, 10, 15, 20],height: '100%',
               
                url: 'ShopList.aspx?Action=GetDataList', 
                alternatingRow: false, 
               onDblClickRow: function(data, rowindex, rowobj) {
                    // $.ligerDialog.alert('选择的是' + data.id);
                     editRow();
                },
           
                toolbar: { items: [
               
                { text: '刷新', click: reload, img: '../lib/ligerUI/skins/icons/refresh.png'},
                { line: true },
               
                { text: "新增分店", click: addRowTop,img: '../lib/ligerUI/skins/icons/add.gif'},
                { line: true },
               
                { text: "修改分店", click:editRow,img: '../lib/ligerUI/skins/icons/modify.gif'},
                { line: true },
               
                { text: "删除分店", click: deleteRow,img: '../lib/ligerUI/skins/icons/delete.gif'},
                { line: true },

                { text: "启用", click:startRow,img: '../lib/ligerUI/skins/icons/ok.gif'},
                { line: true },
                
                 { text: "禁用", click:stopRow,img: '../lib/ligerUI/skins/icons/busy.gif'}
               
             
                
                ]
                }

                
                
            }
            );
        });


        function getSelected() {
            var row = manager.getSelectedRow();
            if (!row) { alert('请选择行'); return; }
            alert(JSON.stringify(row));
        }
        function getData() {
            var data = manager.getData();
            alert(JSON.stringify(data));
        } 
        
        function stopRow()
        {
           
             var row = manager.getSelectedRow();
            if (!row) {  $.ligerDialog.warn('请选要操作的择行'); return; }
            if(row.flag=="禁用")
            {
                $.ligerDialog.warn('当前分店已是禁用状态');
                return;
            }


             $.ajax({
                        type: "GET",
                        url: "ShopList.aspx",
                        data: "Action=stop&id=" + row.id + "&ranid=" + Math.random(), //encodeURI
                        success: function(resultString) {
                            $.ligerDialog.alert(resultString, '提示信息');
                            reload();

                        },
                        error: function(msg) {

                            $.ligerDialog.alert("网络异常，请联系管理员", '提示信息');
                        }
                    });
           
        }
        
        function startRow()
        {
           
             var row = manager.getSelectedRow();
            if (!row) {  $.ligerDialog.warn('请选要操作的择行'); return; }
            
            if(row.flag=="启用")
            {
                $.ligerDialog.warn('当前分店已是启用状态');
                return;
            }


             $.ajax({
                        type: "GET",
                        url: "ShopList.aspx",
                        data: "Action=stop&id=" + row.id + "&ranid=" + Math.random(), //encodeURI
                        success: function(resultString) {
                            $.ligerDialog.alert(resultString, '提示信息');
                            reload();

                        },
                        error: function(msg) {

                            $.ligerDialog.alert("网络异常，请联系管理员", '提示信息');
                        }
                    });
           
        }
        

        function editRow()
        {
            var row = manager.getSelectedRow();
            if (!row) {  $.ligerDialog.warn('请选择修改的行！'); return; }

          
            
            var title="修改分店";
           
            $.ligerDialog.open({ 
                title : title,
                url: 'ShopListAdd.aspx?id='+row.id,
                height:400,
                width:650,
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
                        url: "ShopList.aspx",
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

     
     
    
       
        
      

    
       
         function reload() {
             manager.changePage("first");
            manager.reload();
        }
        
     
        
        function addRowTop()
        {
             
            var title="新增分店";
         
            
            $.ligerDialog.open({ 
                title : title,
                url: 'ShopListAdd.aspx',
                height:400,
                width:650,
                modal:false
            });
          
        } 



         
    </script>
    <style type="text/css">
    .l-button{width: 120px; float: left; margin-left: 10px; margin-bottom:2px; margin-top:2px;}
    </style>


    
</head>
<body style="padding-left:10px; padding-top:10px;">
    <form id="form1" runat="server">
  
   
    <div id="maingrid">
    </div>
  

    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsBrandList.aspx.cs" Inherits="Lanwei.Weixin.UI.baseSet.GoodsBrandList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>物料品牌设置</title>
     <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>

    <script src="../jsData/TreeDeptData.js" type="text/javascript"></script>

    
       <script type="text/javascript">
      
        var manager;
        $(function ()
        {
            manager = $("#maingrid").ligerGrid({
                columns: [
                
                 
                 { display: '品牌名称', name: 'names',id: 'levelName', width: 250, align: 'left' },
                 { display: '商品数量', name: 'num', width: 100, type: 'int', align: 'center' },             
                 { display: '显示顺序', name: 'flag', width: 60, align: 'center' }
            
                ], width: '99%', pageSizeOptions: [5, 10, 15, 20],height: '99%',
               
                url: 'GoodsBrandList.aspx?Action=GetDataList', 
                alternatingRow: false,               
                rownumbers: true, //显示序号
                onDblClickRow: function (data, rowindex, rowobj) {
                    // $.ligerDialog.alert('选择的是' + data.id);
                    editRow();
                },
                toolbar: { items: [
               
                { text: '刷新', click: reload, img: '../lib/ligerUI/skins/icons/refresh.png'},
                { line: true },
               
                { text: "添加", click: addRowTop,img: '../lib/ligerUI/skins/icons/add.gif'},
                { line: true },
               
                { text: "修改", click:editRow,img: '../lib/ligerUI/skins/icons/modify.gif'},
                { line: true },
               
                { text: "删除", click: deleteRow,img: '../lib/ligerUI/skins/icons/delete.gif'}

                ]
                }

                
                
            }
            );
        });
        
        function editRow()
        {
            var row = manager.getSelectedRow();
            if (!row) {  $.ligerDialog.warn('请选择修改的行！'); return; }
            
            var title="修改物料品牌";
           
            $.ligerDialog.open({ 
                title : title,
                url: "GoodsBrandListAdd.aspx?id="+row.id+"&names="+row.names+"&flag="+row.flag,
                height:250,
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
                        url: "GoodsBrandList.aspx",
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

     
        function getSelected()
        {
            var row = manager.getSelectedRow();
            if (!row) {  $.ligerDialog.warn('请选择行'); return; }
            alert(JSON.stringify(row));
        }
        function getData()
        {
            var data = manager.getData();
            alert(JSON.stringify(data));
        }

         function reload()
        {
            manager.reload();
        }
        
       
        function addRowTop()
        {
             
            var title="新增物料品牌";
         
            
            $.ligerDialog.open({ 
                title : title,
                url: 'GoodsBrandListAdd.aspx',
                height:250,
                width:400,
                modal:true
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

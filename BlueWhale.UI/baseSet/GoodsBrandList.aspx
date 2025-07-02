<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsBrandList.aspx.cs" Inherits="BlueWhale.UI.baseSet.GoodsBrandList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Material Brand Settings</title>
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
                    { display: 'Brand Name', name: 'names',id: 'levelName', width: 110, align: 'left' },
                    { display: 'Item Quantity', name: 'num', width: 110, type: 'int', align: 'center' },             
                    { display: 'Display Order', name: 'flag', width: 110, align: 'center' }
                ], width: '99%', pageSizeOptions: [5, 10, 15, 20],height: '99%',
                url: 'GoodsBrandList.aspx?Action=GetDataList', 
                alternatingRow: false,               
                rownumbers: true, 
                onDblClickRow: function (data, rowindex, rowobj) {
                    editRow();
                },
                toolbar: { items: [
                        { text: 'Refresh', click: reload, img: '../lib/ligerUI/skins/icons/refresh.png'},
                        { line: true },
                        { text: "Add", click: addRowTop,img: '../lib/ligerUI/skins/icons/add.gif'},
                        { line: true },
                        { text: "Modify", click:editRow,img: '../lib/ligerUI/skins/icons/modify.gif'},
                        { line: true },
                        { text: "Delete", click: deleteRow,img: '../lib/ligerUI/skins/icons/delete.gif'}
                    ]
                }
            }
            );
        });
        
        function editRow()
        {
            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('Please select the row to modify!'); return; }
            
            var title ="Edit Item Brand";
           
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
            if (!row) { $.ligerDialog.warn('Please select the rows you want to delete'); return; }
            $.ligerDialog.confirm('Deletion cannot be restored. Confirm deletion?', function(type) {
                if (type) {
                    $.ajax({
                        type: "GET",
                        url: "GoodsBrandList.aspx",
                        data: "Action=delete&id=" + row.id + "&ranid=" + Math.random(), //encodeURI
                        success: function(resultString) {
                            $.ligerDialog.alert(resultString, 'Notification');
                            reload();
                        },
                        error: function(msg) {
                            $.ligerDialog.alert("Network abnormality, please contact the administrator", 'Notification');
                        }
                    });
                }
            });
        }

        function getSelected()
        {
            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('Please select a row'); return; }
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
            var title ="Add Item Brand";

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

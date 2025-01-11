<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsTypeList.aspx.cs" Inherits="Lanwei.Weixin.UI.baseSet.GoodsTypeList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>商品类别设置</title>
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

                 {
                     display: '图片', name: 'picUrl', id: 'picUrl', width: 50, align: 'center'
                     , render: function(rowdata, rowindex, value) {
                         var h = "";
                         if (!rowdata._editing) {
                             h += "<img src='"+value+"' style='width:50px;height:50px;padding:5px;' /> ";
                         }
                         

                         return h;
                     }

                 },
                 { display: '类别名称', name: 'names', id: 'names', width: 250, align: 'left' },
                 { display: '商品数量', name: 'num', width: 100, type: 'int', align: 'center' },
                 { display: '显示顺序', name: 'seq', width: 60, align: 'center' },
                 {
                     display: '小程序显示', name: 'isShowXCX', id: 'isShowXCX', width: 80, align: 'center'
                     , render: function (rowdata, rowindex, value) {
                         var h = "";
                         if (!rowdata._editing) {

                             if (value == "1")
                             {
                                 h = "是";
                             }
                             else
                             {
                                 h = "否";
                             }
                            
                         }


                         return h;
                     }

                 },

                  {
                      display: '公众号显示', name: 'isShowGZH', id: 'isShowGZH', width: 80, align: 'center'
                     , render: function (rowdata, rowindex, value) {
                         var h = "";
                         if (!rowdata._editing) {

                             if (value == "1") {
                                 h = "是";
                             }
                             else {
                                 h = "否";
                             }

                         }


                         return h;
                     }

                  },

                 { display: '上级编号', name: 'parentId', width: 60, align: 'center',hide:true},
                  { display: '上级名称', name: 'parentName', width: 60, align: 'center',hide:true},
            
                
                 { display: '操作', isSort: false, width: 120,align:'center', render: function (rowdata, rowindex, value)
                 {
                    var h = "";
                    if (!rowdata._editing)
                    {
                        h += "<a href='javascript:addSubRow()' title='添加小类' >添加小类</a> ";
                    
                    }
                   
                    return h;
                }
                }
                ], width: '99%', pageSizeOptions: [5, 10, 15, 20],height: '100%',

                url: 'GoodsTypeList.aspx?Action=GetDataList', 
                alternatingRow: false, 
                rownumbers: true, //显示序号
                tree: {
                    columnId: 'names',
                    idField: 'id',
                    parentIDField: 'parentId'
                },
                
                 onDblClickRow: function(data, rowindex, rowobj) {
                    // $.ligerDialog.alert('选择的是' + data.id);
                     editRow();
                },
           
                toolbar: { items: [
               
                { text: '刷新', click: reload, img: '../lib/ligerUI/skins/icons/refresh.png'},
                { line: true },
               
                { text: "添加大类", click: addRowTop,img: '../lib/ligerUI/skins/icons/add.gif'},
                { line: true },
               
                { text: "修改类别", click:editRow,img: '../lib/ligerUI/skins/icons/modify.gif'},
                { line: true },


                { text: '修改图片', click: editPic, img: '../lib/ligerUI/skins/icons/photograph.gif' },
                { line: true },

               
                { text: "删除类别", click: deleteRow,img: '../lib/ligerUI/skins/icons/delete.gif'},
                { line: true },
               
              
               
                { text: '全部展开', click: expandAll, img: '../lib/ligerUI/skins/icons/expand.png' },
              
              
                { line: true },
                { text: '全部合并', click: collapseAll, img: '../lib/ligerUI/skins/icons/collapse.png' }
                
                ]
                }

                
                
            }
            );
        });
        
        function editRow()
        {
            var row = manager.getSelectedRow();
            if (!row) {  $.ligerDialog.warn('请选择修改的行！'); return; }
            
           
            
            var title="修改类别-"+row.names;
           
            $.ligerDialog.open({ 
                title : title,
                url: 'GoodsTypeListAdd.aspx?id='+row.id+"&names="+row.names+"&parentId="+row.parentId+"&parentName="+row.parentName+"&seq="+row.seq,
                height:250,
                width:400,
                modal:false
                
            });
            
            
         

        }
        

        function editPic() {
            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('请选择修改的行！'); return; }



            var title = "修改类别图片-" + row.names;

            $.ligerDialog.open({
                title: title,
                url: 'GoodsTypeListPic.aspx?id=' + row.id + "&names=" + row.names + "&picUrl=" + row.picUrl + "&parentName=" + row.parentName + "&seq=" + row.seq,
                height: 350,
                width: 400,
                modal: false

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
                        url: "GoodsTypeList.aspx",
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
    
       
        //全部合并
        function collapseAll()
        {
            var row = manager.getSelected();
            manager.collapseAll();
        }
        //全部展开
        function expandAll()
        {
            var row = manager.getSelected();
            manager.expandAll();
        }
     
         function reload()
        {
            manager.reload();
        }
        
        //新增子区域，ID为ParentId
        function addSubRow()
        {
             
             var row = manager.getSelectedRow();
                       
            var title="添加小类-"+row.names;
           
            $.ligerDialog.open({ 
                title : title,
                url: "GoodsTypeListAdd.aspx?id=0&names=&parentId="+row.id+"&parentName="+row.names+"&seq=0",
                height:250,
                width:400,
                modal:false
            });
          
        } 
        
        function addRowTop()
        {
             
            var title="新增顶级类别";
         
            
            $.ligerDialog.open({ 
                title : title,
                url: "GoodsTypeListAdd.aspx?id=0&names=&parentId=0&parentName=顶级类别&seq=0",
                height:250,
                width:400,
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeptList.aspx.cs" Inherits="Lanwei.Weixin.UI.baseSet.DeptList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>部门设置</title>
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

    <script src="../jsData/TreeDeptData.js" type="text/javascript"></script>

    
       <script type="text/javascript">
      
        var manager;
        $(function ()
        {
            manager = $("#maingrid").ligerGrid({
                columns: [


                 { display: '部门名称', name: 'deptName', id: 'deptName', width: 250, align: 'left' },
                 { display: '员工数量', name: 'num', width: 100, type: 'int', align: 'center' },
                 { display: '显示顺序', name: 'flag', width: 60, align: 'center' },
                 { display: '上级编号', name: 'parentId', width: 60, align: 'center',hide:true},
                  { display: '上级名称', name: 'parentName', width: 60, align: 'center',hide:true},
            
                
                 { display: '操作', isSort: false, width: 120,align:'center', render: function (rowdata, rowindex, value)
                 {
                    var h = "";
                    if (!rowdata._editing)
                    {
                        h += "<a href='javascript:addSubRow()' title='添加下级' >添加下级</a> ";
                    
                    }
                   
                    return h;
                }
                }
                ], width: '99%', pageSizeOptions: [5, 10, 15, 20],height: '100%',

                url: 'DeptList.aspx?Action=GetDataList', 
                alternatingRow: false, 
                rownumbers: true, //显示序号
                tree: {
                    columnId: 'deptName',
                    idField: 'deptId',
                    parentIDField: 'parentId'
                },
                
                 onDblClickRow: function(data, rowindex, rowobj) {
                    // $.ligerDialog.alert('选择的是' + data.id);
                     editRow();
                },
                
           
                toolbar: { items: [
               
                { text: '刷新', click: reload, img: '../lib/ligerUI/skins/icons/refresh.png'},
                { line: true },
               
                { text: "添加部门", click: addRowTop,img: '../lib/ligerUI/skins/icons/add.gif'},
                { line: true },
               
                { text: "修改部门", click:editRow,img: '../lib/ligerUI/skins/icons/modify.gif'},
                { line: true },
               
                { text: "删除部门", click: deleteRow,img: '../lib/ligerUI/skins/icons/delete.gif'},
                { line: true },
               
              
               
                { text: '全部展开', click: expandAll, img: '../lib/ligerUI/skins/icons/expand.png' },
              
              
                { line: true },
                { text: '全部合并', click: collapseAll, img: '../lib/ligerUI/skins/icons/collapse.png' },

                 { line: true },
                { text: '企业号同步', click: downQY, img: '../lib/ligerUI/skins/icons/down.gif' }

                
                ]
                }

                
                
            }
            );
        });

        function downQY() {

          
            $.ligerDialog.confirm('此操作将删除现有部门数据后再从企业号同步，确认操作？', function (type) {

                if (type) {

                    $.ajax({
                        type: "GET",
                        url: "DeptList.aspx",
                        data: "Action=downQY&ranid=" + Math.random(), //encodeURI
                        success: function (resultString) {
                            $.ligerDialog.alert(resultString, '提示信息');
                            reload();

                        },
                        error: function (msg) {

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
            
           
            
            var title="修改部门-"+row.deptName;
           
            $.ligerDialog.open({ 
                title : title,
                url: 'DeptListAdd.aspx?deptId='+row.deptId+"&deptName="+row.deptName+"&parentId="+row.parentId+"&parentName="+row.parentName+"&flag="+row.flag,
                height:250,
                width:400,
                modal:false
                
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
                        url: "DeptList.aspx",
                        data: "Action=delete&id=" + row.deptId + "&ranid=" + Math.random(), //encodeURI
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
                       
            var title="添加子部门-"+row.deptName;
           
            $.ligerDialog.open({ 
                title : title,
                url: "DeptListAdd.aspx?deptId=0&deptName=&parentId="+row.deptId+"&parentName="+row.deptName+"&flag=0",
                height:250,
                width:400,
                modal:false
            });
          
        } 
        
        function addRowTop()
        {
             
            var title="新增顶级部门";
         
            
            $.ligerDialog.open({ 
                title : title,
                url: "DeptListAdd.aspx?deptId=0&deptName=&parentId=0&parentName=顶级部门&flag=0",
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
<body style="padding-left:10px;">
    <form id="form1" runat="server">
  
   
    <div id="maingrid">
    </div>
  

    </form>
</body>
</html>

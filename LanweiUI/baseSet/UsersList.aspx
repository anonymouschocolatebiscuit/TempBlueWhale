<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsersList.aspx.cs" Inherits="Lanwei.Weixin.UI.baseSet.UsersList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>用户管理</title>

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


               
                 { display: '手机', name: 'phone', width: 150, align: 'center' },
                 { display: '姓名', name: 'names', width: 120, align: 'center' },
                 //{ display: '分店', name: 'shopName', width: 100, align: 'center'},
                 //{ display: '部门', name: 'deptName', width: 70, align: 'center'},
                 // { display: '角色', name: 'roleName', width: 70, align: 'center' },
                  { display: '电话', name: 'tel', width: 120, align: 'center' },
                  
                  { display: '邮箱', name: 'email', width: 120, align: 'center'},
                  { display: 'QQ', name: 'qq', width: 100, align: 'center'},
                  { display: '地址', name: 'address', width: 100, align: 'center'},
                  { display: '生日', name: 'brithDay', width: 80, align: 'center',type:"date"},
                  { display: '入职日期', name: 'comeDate', width: 80, align: 'center', type: "date" },
                  { display: '状态', name: 'flag', width: 80, align: 'center'},
            
                
               
            
                ], width: '99%', pageSizeOptions: [20, 50, 100, 200], height: '100%',
               
                url: 'UsersList.aspx?Action=GetDataList',

                rownumbers: true,
                pageSize: 50,
                dataAction: 'local', //本地排序
                usePager: true,

                alternatingRow: false, 
              
                 onDblClickRow: function(data, rowindex, rowobj) {
                    // $.ligerDialog.alert('选择的是' + data.id);
                     editRow();
                },
           
                toolbar: { items: [
               
                { text: '刷新', click: reload, img: '../lib/ligerUI/skins/icons/refresh.png'},
                { line: true },
               
                { text: "新增用户", click: addRowTop,img: '../lib/ligerUI/skins/icons/add.gif'},
                { line: true },
               
                { text: "修改用户", click:editRow,img: '../lib/ligerUI/skins/icons/modify.gif'},
                { line: true },
               
                { text: "删除用户", click: deleteRow,img: '../lib/ligerUI/skins/icons/delete.gif'},
                { line: true },



                { text: '重置密码', click: setPwd, img: '../lib/ligerUI/skins/icons/settings.gif' },
                { line: true },
                { text: '权限设置', click: setRight, img: '../lib/ligerUI/skins/icons/lock.gif' },

                 { line: true },
                { text: '同步到企业微信', click: downQY, img: '../lib/ligerUI/skins/icons/communication.gif' }
                
                ]
                }

                
                
            }
            );
        });

        function downQY() {


            $.ligerDialog.confirm('此操作将现有员工数据全部同步到企业号，确认操作？', function (type) {

                if (type) {

                    $.ajax({
                        type: "GET",
                        url: "UsersList.aspx",
                        data: "Action=downQY&ranid=" + Math.random(), //encodeURI
                        success: function (resultString) {
                            $.ligerDialog.alert(resultString, '提示信息');
                            reload();

                        },
                        error: function (msg) {
                           //"网络异常，请联系管理员"
                            $.ligerDialog.alert("操作异常，请确认企业微信-管理工具>通讯录同步>权限编辑权限已开启！", '提示信息');
                        }
                    });

                }

            });


        }


        function getSelected() {
            var row = manager.getSelectedRow();
            if (!row) { alert('请选择行'); return; }
            alert(JSON.stringify(row));
        }
        function getData() {
            var data = manager.getData();
            alert(JSON.stringify(data));
        } 

        function editRow()
        {
            var row = manager.getSelectedRow();
            if (!row) {  $.ligerDialog.warn('请选择修改的行！'); return; }

          
            
            var title="修改用户";
           
            $.ligerDialog.open({ 
                title : title,
                url: 'UsersListAdd.aspx?id='+row.id,
                height:450,
                width:650,
                modal:true
               
            });
            
            
        

        }
        
        function deleteRow()
        {
           
             var row = manager.getSelectedRow();
            if (!row) {  $.ligerDialog.warn('请选要删除的择行'); return; }
            
            if(row.flag=="启用")
            {
               $.ligerDialog.warn('启用状态不能删除！');
                return;
            }
           
              $.ligerDialog.confirm('删除后不能恢复，确认删除？', function(type) {

                if (type) {

                    $.ajax({
                        type: "GET",
                        url: "UsersList.aspx",
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

     
     
    
       
        
        function setPwd()
        {
            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('请选要设置的用户'); return; }

            $.ligerDialog.confirm('初始密码为123456，确认重置？', function(type) {

                //alert(type); 
                if (type) {
                    // alert("删除ID："+row.id);
                    $.ajax({
                        type: "GET",
                        url: "UsersList.aspx",
                        data: "Action=setPwd&id=" + row.id + " &ranid=" + Math.random(), //encodeURI
                        success: function(resultString) {

                            $.ligerDialog.alert(resultString, '提示信息');
                            reload();
                        },
                        error: function(msg) {
                            //alert('网络异常，请联系管理员！');
                            $.ligerDialog.alert("网络异常，请联系管理员", '提示信息');
                        }
                    });

                }

            });
            
            
            
        }


        function setRight() {
            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('请选要设置的用户'); return; }

           // alert(row.CName);
            
            var title = "权限设置--" + row.names;
            $.ligerDialog.open({
            title: title,
                url: 'UsersListRight.aspx?id='+row.id,
                height: 550,
                width: 400,
                modal: true
            });
            
        }
      
       
         function reload() {
             manager.changePage("first");
            manager.reload();
        }
        
     
        
        function addRowTop()
        {
             
            var title="新增用户";
         
            
            $.ligerDialog.open({ 
                title : title,
                url: 'UsersListAdd.aspx',
                height:450,
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientTypeListDis.aspx.cs" Inherits="Lanwei.Weixin.UI.baseSet.ClientTypeListDis" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>客户等级设置</title>
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
                
                 
                 { display: '客户分类名称', name: 'Names',id: 'Names', width: 350, align: 'left',frozen:true},
                 
                 { display: '优惠折扣%', name: 'Dis', width:160, align: 'center',editor: { type: 'float' }}
            
                ], width: '99%', pageSizeOptions: [5, 10, 15, 20],height: '98%',
               
                url: 'ClientTypeListDis.aspx?Action=GetDataList', 
                alternatingRow: false,               
                rownumbers: true, //显示序号
                enabledEdit: true,//控制能否编辑的
                onBeforeSubmitEdit: f_onBeforeSubmitEdit,
                onAfterEdit: f_onAfterEdit

              
            }
            );
        });
        
       //限制折扣类别
        function f_onBeforeSubmitEdit(e)
        { 
            if (e.column.name == "Dis")
            {
                if (e.value < 0 || e.value > 100)
                {
                
                  $.ligerDialog.alert("请输入正确优惠折扣", '提示信息');
               
                 return false;
               
                }
            
            }
            
            return true;
        }
        //编辑后事件 
        function f_onAfterEdit(e)
        {
            if (e.column.name == "Dis")
            {
                var typeId = Number(e.record.id);
                var dis=e.value;
                
             
                
               $.ajax({
               type: "GET",
               url: "ClientTypeListDis.aspx?Action=edit&id="+typeId+"&dis="+e.value+"&ranid=" + Math.random(),
              // data:dataJson,

                success: function(resultString) {
                
                // $.ligerDialog.alert(resultString, '提示信息');
                 reload();
               
                   
                   
               },
               error: function(msg) {
                   //alert('网络异常，请联系管理员！');
                   $.ligerDialog.alert("网络异常，请联系管理员", '提示信息');
               }
           });
    
              
            }
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
        
        function saveData()
        {
            manager.reload();
        }
        
       
        function addRowTop()
        {
             
            var title="新增客户分类";
         
            
            $.ligerDialog.open({ 
                title : title,
                url: 'ClientTypeListAdd.aspx',
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
  
   
       <div id="maingrid">  </div>
            <div style="display:none;">
   
   
  
  

    </form>
</body>
</html>

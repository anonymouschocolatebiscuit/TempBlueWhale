<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsListTypeList.aspx.cs" Inherits="Lanwei.Weixin.UI.baseSet.GoodsListTypeList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>商品导航设置</title>
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
                checkbox: true,
                columns: [
                
                 
                 { display: '导航分类名称', name: 'categoryName', id: 'categoryName', width: 350, align: 'left', frozen: true },
                 
                
            
                ] ,  toolbar: { items: [
               
              
                 
                   { text: '批量保存', click: checkRow, img: '../lib/ligerUI/skins/icons/true.gif' },
                
                 { line: true }
          
                
                ]
            }


                , width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '98%',
               
                url: 'GoodsListTypeList.aspx?Action=GetDataList',
                isChecked: f_isChecked, onCheckRow: f_onCheckRow, onCheckAllRow: f_onCheckAllRow,

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


        function checkRow() {


            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('请选要操作的择行'); return; }

            var idString = checkedCustomer.join(',');//获取选中的ID字符串，用‘，’隔开，传递到后台即可
            

            $.ajax({
                type: "GET",
                url: "GoodsListTypeList.aspx",
                data: "Action=checkRow&idString=" + idString +"&goodsId="+goodsId+ "&ranid=" + Math.random(), //encodeURI
                success: function (resultString) {
                    $.ligerDialog.alert(resultString, '提示信息');
                    reload();

                },
                error: function (msg) {

                    $.ligerDialog.alert("网络异常，请联系管理员", '提示信息');
                }
            });




        }


        var goodsId = getUrlParam("id");



        function getUrlParam(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");

            var r = window.location.search.substr(1).match(reg);

            if (r != null) return unescape(r[2]); return null;
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
        
       
       
        function f_onCheckAllRow(checked) {
            for (var rowid in this.records) {
                if (checked)
                    addCheckedCustomer(this.records[rowid]['id']);
                else
                    removeCheckedCustomer(this.records[rowid]['id']);
            }
        }

        /*
        该例子实现 表单分页多选
        即利用onCheckRow将选中的行记忆下来，并利用isChecked将记忆下来的行初始化选中
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



         
    </script>
    <style type="text/css">
    .l-button{width: 120px; float: left; margin-left: 10px; margin-bottom:2px; margin-top:2px;}
    </style>


    
</head>
<body style="padding-left:10px; padding-top:10px;">
    <form id="form1" runat="server">
  
          <div id="maingrid">  </div>
         

        

   
       

   
  
  

    </form>
</body>
</html>

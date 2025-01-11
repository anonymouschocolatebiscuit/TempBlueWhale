<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="welcomeGoodsList.aspx.cs" Inherits="Lanwei.Weixin.UI.baseSet.welcomeGoodsList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>商品管理</title>
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
    <script src="../lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    

   

    
     
    
  <script type="text/javascript">
      
        var manager;
        $(function() {
        
       
        
            manager = $("#maingrid").ligerGrid({
            checkbox:false,
                columns: [


               
               
                 { display: '商品类别', name: 'typeName', width: 60, type: 'int', align: 'center' },
                 { display: '商品编号', name: 'code', width: 70, align: 'center' },
                 { display: '商品名称', name: 'names', width: 100, align: 'left'},
                  { display: '规格', name: 'spec', width: 150, align: 'center' },
                  
                  { display: '单位', name: 'unitName', width: 50, align: 'center' },
                  { display: '品牌', name: 'brandName', width: 60, align: 'center' },
                  
                  { display: '封装', name: 'packages', width: 70, align: 'center' },
                  
                  { display: '最小包装', name: 'mpq', width: 60, align: 'center' },
                  
                  { display: '批号', name: 'batchs', width: 70, align: 'center' }
                  
               
            
            
            
            
                ], width: '99%', pageSizeOptions: [5, 10, 15, 20],height: '99%',
                  pageSize: 15,
                  dataAction: 'local', //本地排序
                  usePager: true,
                url: 'welcomeGoodsList.aspx?Action=GetDataListSearch&keys='+param, 
                alternatingRow: false,
                isChecked: f_isChecked, onCheckRow: f_onCheckRow, onCheckAllRow: f_onCheckAllRow,
                
                
                 rownumbers: true //显示序号
           
              

                
                
            }
            );

        });
        
   
     

       
      
        
        function reload() {
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
        
        
        
        var param=getUrlParam("keys");


 
function getUrlParam(name)
{
   var reg = new RegExp("(^|&)"+ name +"=([^&]*)(&|$)");

   var r = window.location.search.substr(1).match(reg);

   if (r!=null) return unescape(r[2]); return null;
}

         
    </script>  
    
    
    <style type="text/css">
    .l-button{width: 120px; float: left; margin-left: 10px; margin-bottom:2px; margin-top:2px;}
    </style>


    
</head>
<body style="padding-left:10px;padding-top:10px;">
    <form id="form1" runat="server">
  
       <div id="maingrid"></div>  
            <div style="display:none;"></div>
            
   
  

    </form>
</body>
</html>

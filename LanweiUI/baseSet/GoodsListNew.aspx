<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsListNew.aspx.cs" Inherits="Lanwei.Weixin.UI.baseSet.GoodsListNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>商品列表</title>
   
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
     
<script type="text/javascript">
        
        
    document.onkeydown=keyDownSearch;  
      
    function keyDownSearch(e) {    
        // 兼容FF和IE和Opera    
        var theEvent = e || window.event;    
        var code = theEvent.keyCode || theEvent.which || theEvent.charCode;    
        if (code == 13) {    
             
            
             
            $("#btnSearch").click();
                
            return false;    
        }    
        return true;    
    }  
        
        
        
        var manager = null;
        
        
        
        
        
        $(function () {
        
        
        
        
       
        
        
        
        
        
             var form = $("#formTB").ligerForm();
             
          
         
          var txtKeys =  $.ligerui.get("txtKeys");
         txtKeys.set("Width", 300);
            
        
           manager = $("#maingrid4").ligerGrid({
            
               checkbox:true,
                columns: [
               
              
                 { display: '商品编号', name: 'code', width: 100, align: 'center' },
                 { display: '商品名称', name: 'names', width: 150, align: 'left'},
                  { display: '规格', name: 'spec', width: 70, align: 'center' },
                  { display: '单位', name: 'unitName', width: 70, align: 'center' },
                  { display: '首选仓库', name: 'ckName', width: 60, align: 'center'},
                  { display: '单位成本', name: 'costUnit', width: 70, align: 'center'},
                  
                  { display: '期初总价', name: 'sumNumStart', width: 100, align: 'center'},
                  { display: '预计采购价', name: 'priceCost', width: 80, align: 'center',type:"date"},
                  { display: '预计销售价', name: 'priceSales', width: 80, align: 'center'},
                 
                  { display: '条码', name: 'barcode', width: 100, align: 'center' },
                  
                  { display: '最低库存', name: 'numMin', width: 60, align: 'center' },
                  
                  { display: '最高库存', name: 'numMax', width: 60, align: 'center' },
                  
                  { display: '称重', name: 'isWeight', width: 60, align: 'center' },
                  
                  { display: '状态', name: 'flag', width: 80, align: 'center' },
                 
                  { display: '期初库存', name: 'sumNumStart', width: 80, align: 'center', type: "date" }
                  
              
            
               
               
                ],  pageSize:10,
                
                usePager: false,
                 url: 'GoodsListSelect.aspx?Action=GetDataList', 
                width: '690',height:'600',
                 isChecked: f_isChecked, onCheckRow: f_onCheckRow, onCheckAllRow: f_onCheckAllRow,
                
                
                 rownumbers: true, //显示序号
           
                toolbar: { items: [
               
                { text: '刷新', click: reload, img: '../lib/ligerUI/skins/icons/refresh.png'},
                { line: true },
                
               
               
                { text: "新增商品", click: addRowTop,img: '../lib/ligerUI/skins/icons/add.gif'},
                { line: true },
               
                { text: "修改商品", click:editRow,img: '../lib/ligerUI/skins/icons/modify.gif'},
                { line: true },
               
                { text: "删除商品", click: deleteRow,img: '../lib/ligerUI/skins/icons/delete.gif'},
                { line: true },



                { text: '期初库存管理', click: linkManForm, img: '../lib/ligerUI/skins/icons/view.gif' },
                
                 { line: true },
                 
                 { text: '批量导入', click: excel, img: '../lib/ligerUI/skins/icons/xls.gif' }
          
                
                ]
                }

                
                
                
            }); 
            $("#pageloading").hide();
        });
        function f_select()
        {
            return manager.getSelectedRows();
            
            
            
        }
        
          function search() {

           
                var keys = document.getElementById("txtKeys").value;
                if (keys == "请输入编号/名称/规格") {

                    keys = "";

                }
                
              
                
                manager.changePage("first");
                manager._setUrl("GoodsListSelect.aspx?Action=GetDataListSearch&keys="+keys+"&typeId="+typeId); 
           
              
        }
        
        
        
        
         var typeId=0;
         
         $(function ()
        {   
           

            $("#tree1").ligerTree({  
            url:"GoodsTypeList.aspx?Action=GetTreeList",
      
            onSelect: onSelect,
            parentIcon:null,
            childIcon:null,
            checkbox:false,
            slide : false,
            treeLine:true,
            idFieldName :'id',
            
            
            parentIDFieldName :'pid'
            
            }
            
            );
        });

        
        
      
        function onSelect(note)
        {
          
          //  alert('onSelect:' + note.data.id);
            
            typeId=note.data.id;
            
            search();
            
            
        }
        
                 function excel()
        {
            var title = "导入商品";

            $.ligerDialog.open({
                title: title,
                url: 'GoodsListExcel.aspx',
                height: 450,
                width: 550,
                modal: true
            });
            
            

        }
        

         function addRowTop()
        {
             
            var title="新增商品";
         
            
            $.ligerDialog.open({ 
                title : title,
                url: 'GoodsListAdd.aspx?id=0',
                height:500,
                width:650,
                modal:false
            });
          
        } 
        
        function editRow()
        {
            var row = manager.getSelectedRow();
            if (!row) {  $.ligerDialog.warn('请选择修改的行！'); return; }

          
            
            var title="修改商品-"+row.names;
           
            $.ligerDialog.open({ 
                title : title,
                url: 'GoodsListAdd.aspx?id='+row.id,
                height:500,
                width:650,
                modal:true
               
            });
            
            
        }
        
         function linkManForm()
        {
            var row = manager.getSelectedRow();
            if (!row) {  $.ligerDialog.warn('请选择商品！'); return; }

          
            
            var title="商品期初库存-"+row.names;
           
            $.ligerDialog.open({ 
                title : title,
                url: 'GoodsListNumStart.aspx?id='+row.id,
                height:450,
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
                        url: "GoodsList.aspx",
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
    
    
</head>
<body>
    <form id="form1" runat="server">
   
   
      <table id="formTB" border="0" style="width:100%; line-height:40px;">
   
   <tr>
   <td>
       <asp:TextBox ID="txtKeys" runat="server" nullText="请输入编号/名称/规格"></asp:TextBox>
       </td>
   
   <td style="width:200px;">
       <input id="btnSearch" type="button" value="查询" class="ui-btn" onclick="search()"  /></td>
   
   </tr>
   
   
   <tr>
   <td>
   
   
    <div id="maingrid4" style="margin:0; padding:0"></div>
   
   



   
   
   
   
   </td>
   
   <td valign="top">
   
   
   
    <div style="width:200px; position:relative; height:600px; display:block; margin:10px; background:white; border:1px solid #ccc; overflow:auto;">
    <ul id="tree1">
     
    </ul>
    </div> 
 
        <div style="display:none">
     
    </div>



   
   
   
   </td>
   
   </tr>
   
   
   </table>
  
   
   
    </form>
</body>
</html>

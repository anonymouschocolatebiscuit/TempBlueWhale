<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsList.aspx.cs" Inherits="Lanwei.Weixin.UI.baseSet.GoodsList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>商品管理</title>
  
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
               
                 

                 
              
                 { display: '商品编码', name: 'code', width: 100, align: 'center' },
                
                 { display: '商品名称', name: 'names', width: 150, align: 'left'},
                  
                  { display: '条码', name: 'barcode', width: 100, align: 'center' },
                  
                  { display: '商品类别', name: 'typeName', width: 70, align: 'center' },
                  
                 { display: '品牌', name: 'brandName', width: 70, align: 'center' },
                  
                 { display: '规格', name: 'spec', width: 120, align: 'center' },
                  { display: '单位', name: 'unitName', width: 70, align: 'center' },
                  { display: '首选仓库', name: 'ckName', width: 60, align: 'center'},
                  { display: '产地', name: 'place', width: 70, align: 'center' },
                  
                 
                  { display: '采购价', name: 'priceCost', width: 80, align: 'center',type:"date"},
                  { display: '批发价', name: 'priceSalesWhole', width: 80, align: 'center'},
                  { display: '零售价', name: 'priceSalesRetail', width: 80, align: 'center'},
                  
                  
                  { display: '最低库存', name: 'numMin', width: 60, align: 'center' },
                  
                  { display: '最高库存', name: 'numMax', width: 60, align: 'center' },
                  
                  
//                  { display: '保质期(天)', name: 'bzDays', width: 70, align: 'center' },
//                  { display: '称重', name: 'isWeight', width: 60, align: 'center' },
                  
                  { display: '状态', name: 'isShow', width: 80, align: 'center' }
                 
//                  { display: '期初库存', name: 'sumNumStart', width: 80, align: 'center', type: "date" },
//                  
//                   { display: '单位成本', name: 'costUnit', width: 70, align: 'center'},
//                  
//                  { display: '期初总价', name: 'sumPriceStart', width: 100, align: 'center'},
                  
              
            
               
               
                ],

                usePager: false,
               

                 url: 'GoodsList.aspx?Action=GetDataList', 
                width: '690',height:'98%',
                 isChecked: f_isChecked, onCheckRow: f_onCheckRow, onCheckAllRow: f_onCheckAllRow,
                
                  onDblClickRow: function(data, rowindex, rowobj) {
                    // $.ligerDialog.alert('选择的是' + data.id);
                     editRow();
                },
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



                { text: '期初库存', click: linkManForm, img: '../lib/ligerUI/skins/icons/view.gif' },
                
                 { line: true },
                 
                 
                 // { text: '等级售价', click: f_showPrice, img: '../lib/ligerUI/skins/icons/view.gif' },
                
                 //{ line: true },
                 
                  { text: '导航分类', click: f_showTypeList, img: '../lib/ligerUI/skins/icons/view.gif' },
                
                 { line: true },
                 
                 
                  { text: '图片管理', click: f_showImage, img: '../lib/ligerUI/skins/icons/photograph.gif' },
                
                 { line: true },
                 
                  { text: '商品详情', click: f_hideImage, img: '../lib/ligerUI/skins/icons/view.gif' },
                  
                  { line: true },
                  
                  
                  { text: '在售商品', click: searchOnLine, img: '../lib/ligerUI/skins/icons/miniicons/date_new.gif' },
                
                 { line: true },
                 
                 
                   { text: '批量下架', click: checkNoRow, img: '../lib/ligerUI/skins/icons/outbox.gif' },
                
                 { line: true },
                 
                 
                  { text: '待售商品', click: searchOffLine, img: '../lib/ligerUI/skins/icons/miniicons/date_delete.gif' },
                
                 { line: true },
                 
                   { text: '批量上架', click: checkRow, img: '../lib/ligerUI/skins/icons/true.gif' },
                
                 { line: true },
                 
                 
                 //{ text: '批量导入', click: excel, img: '../lib/ligerUI/skins/icons/xls.gif' }
          
                
                ]
                }

                
                
                
            }); 
            $("#pageloading").hide();
        });
        function f_select()
        {
            return manager.getSelectedRows();
            
            
            
        }
        
        
        function f_hideImage()
        { 
                       
            var row = manager.getSelectedRow();
            if (!row) {  $.ligerDialog.warn('请选择修改的行！'); return; }

            
            var title="商品详情-"+row.names;
           
            $.ligerDialog.open({ 
                title : title,
                url: 'GoodsListDetail.aspx?id='+row.id,
                height:600,
                width:650,
                modal:true
               
            });
            
            
        }
        function f_showImage()
        {
        
            var row = manager.getSelectedRow();
            if (!row) {  $.ligerDialog.warn('请选择修改的行！'); return; }

            
            var title="商品图片-"+row.names;
           
            $.ligerDialog.open({ 
                title : title,
                url: 'GoodsListImages.aspx?id='+row.id,
                height:650,
                width:850,
                modal:true
               
            });

        }
        
        function f_showTypeList() {

            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('请选择修改的行！'); return; }


            var title = "商品导航分类-" + row.names;

            $.ligerDialog.open({
                title: title,
                url: 'GoodsListTypeList.aspx?id=' + row.id,
                height: 450,
                width: 450,
                modal: true

            });

        }

        
          function f_showPrice()
        {
        
            var row = manager.getSelectedRow();
            if (!row) {  $.ligerDialog.warn('请选择行！'); return; }

            
            var title="商品等级价格-"+row.names;
           
            $.ligerDialog.open({ 
                title : title,
                url: 'GoodsPriceClientType.aspx?id='+row.id,
                height:450,
                width:550,
                modal:true
               
            });

        }


          function search() {

           
                var keys = document.getElementById("txtKeys").value;
                if (keys == "请输入编码/条码/名称/规格") {

                    keys = "";

                }

                manager.changePage("first");
                manager._setUrl("GoodsList.aspx?Action=GetDataListSearch&keys="+keys+"&typeId="+typeId+"&isShow="+isShow); 
           
              
        }
        
        
       function searchOnLine() {

           
                isShow=1;
               search();
           
              
        }
        
      function searchOffLine() {

           
               isShow=0;
               search();
           
              
        }
        
        
        
        
         var typeId=0;
         
         var isShow=-1;
         
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
         
           //  parent.f_addTab('GoodsListAdd','新增商品','baseSet/GoodsListAdd.aspx?id=0');
             
             
            $.ligerDialog.open({ 
                title : title,
                url: 'GoodsListAdd.aspx?id=0',
                height:680,
                width:650,
                modal:false
            });
          
        } 
        
        function editRow()
        {
            var row = manager.getSelectedRow();
            if (!row) {  $.ligerDialog.warn('请选择修改的行！'); return; }

          //  parent.f_addTab('GoodsListAdd','修改商品','baseSet/GoodsListAdd.aspx?id='+row.id);
            
            var title="修改商品-"+row.names;
           
            $.ligerDialog.open({ 
                title : title,
                url: 'GoodsListAdd.aspx?id='+row.id,
                height:680,
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
            
             var idString = checkedCustomer.join(',');//获取选中的ID字符串，用‘，’隔开，传递到后台即可
             
              $.ligerDialog.confirm('删除后不能恢复，确认删除？', function(type) {


                if (type) {

                    window.open("GoodsList.aspx?Action=delete&idString=" + idString + " &ranid=" + Math.random());

                    $.ajax({
                        type: "GET",
                        url: "GoodsList.aspx",
                        data: "Action=delete&idString=" + idString + " &ranid=" + Math.random(),
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
        
        
        
            function checkRow() {

            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('请选要操作的择行'); return; }
            
            var idString = checkedCustomer.join(',');//获取选中的ID字符串，用‘，’隔开，传递到后台即可

           $.ajax({
                type: "GET",
                url: "GoodsList.aspx",
                data: "Action=checkRow&idString=" + idString + "&ranid=" + Math.random(), //encodeURI
                success: function(resultString) {
                    $.ligerDialog.alert(resultString, '提示信息');
                    reload();

                },
                error: function(msg) {

                    $.ligerDialog.alert("网络异常，请联系管理员", '提示信息');
                }
            });

                    
             
           
        }
        
        
         function checkNoRow() {

            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('请选要操作的择行'); return; }

            var idString = checkedCustomer.join(','); //获取选中的ID字符串，用‘，’隔开，传递到后台即可

            $.ajax({
                type: "GET",
                url: "GoodsList.aspx",
                data: "Action=checkNoRow&idString=" + idString + "&ranid=" + Math.random(), //encodeURI
                success: function(resultString) {
                    $.ligerDialog.alert(resultString, '提示信息');
                    reload();

                },
                error: function(msg) {

                    $.ligerDialog.alert("网络异常，请联系管理员", '提示信息');
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
       <asp:TextBox ID="txtKeys" runat="server" nullText="请输入编码/条码/名称/规格"></asp:TextBox>
       </td>
   
   <td style="width:200px;">
       
       <input id="btnSearch" type="button" value="查询" class="ui-btn" onclick="search()"  />
       
      
       
       </td>
   
   </tr>
   
   
   <tr>
   <td valign="top">
   
   
    <div id="maingrid4" style="margin:0; padding:0"></div>
   
   



   
   
   
   
   </td>
   
   <td valign="top">
   
   
   
    <div style="width:200px; position:relative; height:520px; display:block; margin:10px; background:white; border:1px solid #ccc; overflow:auto;">
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VenderList.aspx.cs" Inherits="Lanwei.Weixin.UI.baseSet.VenderList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>供应商管理</title>
       <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>

   

    
     
    
  <script type="text/javascript">
      
        var manager;
        $(function() {
        
       
        
            manager = $("#maingrid").ligerGrid({
            checkbox: true,
                columns: [


                { display: '操作', isSort: false, width: 50, align: 'center', render: function(rowdata, rowindex, value) {
                     var h = "";
                     if (!rowdata._editing) {
                         h += "<a href='javascript:editRow()' title='编辑行' style='float:left;'><div class='ui-icon ui-icon-pencil'></div></a> ";
                         h += "<a href='javascript:deleteRow()' title='删除行' style='float:right;'><div class='ui-icon ui-icon-trash'></div></a> ";
                     }
                    
                     return h;
                 }
                 },
               
                 { display: '供应商类别', name: 'typeName', width: 100, type: 'int', align: 'center' },
                 { display: '供应商编号', name: 'code', width: 100, align: 'center' },
                 { display: '供应商名称', name: 'names', width: 230, align: 'left'},
                  { display: '余额日期', name: 'yueDate', width: 80, align: 'center' },
                  { display: '期初余额', name: 'balance', width: 70, align: 'center' },
                  { display: '税率%', name: 'tax', width: 60, align: 'center' },

                  { display: '税号', name: 'taxNumber', width: 100, align: 'center' },
                  { display: '开户银行', name: 'bankName', width: 100, align: 'center' },
                  { display: '银行账号', name: 'bankNumber', width: 100, align: 'center' },
                  { display: '地址', name: 'dizhi', width: 160, align: 'center' },

                  { display: '首要联系人', name: 'linkMan', width: 70, align: 'center'},
                  
                  { display: '手机', name: 'phone', width: 100, align: 'center'},
                  { display: '座机', name: 'tel', width: 110, align: 'center',type:"date"},
                  { display: 'QQ', name: 'qq', width: 80, align: 'center'},
                  { display: '状态', name: 'flag', width: 80, align: 'center' },
                  { display: '录入日期', name: 'makeDate', width: 80, align: 'center', type: "date" }
            
            
            
            
                ], width: '99%',
                //pageSizeOptions: [5, 10, 15, 20],
                height: '99%',
                 // pageSize: 15,
                  dataAction: 'local', //本地排序
                // usePager: true,
                  usePager: false,
                url: 'VenderList.aspx?Action=GetDataList', 
                alternatingRow: false,
                isChecked: f_isChecked, onCheckRow: f_onCheckRow, onCheckAllRow: f_onCheckAllRow,
                
                
                 rownumbers: true, //显示序号
                 
                  onDblClickRow: function(data, rowindex, rowobj) {
                    // $.ligerDialog.alert('选择的是' + data.id);
                     editRow();
                },
           
                toolbar: { items: [
               
                 
               
                  { text: '刷新', click: reload, img: '../lib/ligerUI/skins/icons/refresh.png'},
                  { line: true },
                
                    { text: '筛选查询', click: search, img: '../lib/ligerUI/skins/icons/search.gif' },
                  { line: true },
               
                  { text: "新增供应商", click: addRowTop,img: '../lib/ligerUI/skins/icons/add.gif'},
                  { line: true },
               
                  { text: "修改供应商", click:editRow,img: '../lib/ligerUI/skins/icons/modify.gif'},
                  { line: true },
                
                  { text: "管理联系人", click: linkManForm, img: '../lib/ligerUI/skins/icons/customers.gif' },
                  { line: true },
                
                  { text: "审核", click:checkRow,img: '../lib/ligerUI/skins/icons/true.gif'},
                  { line: true },
                
                  { text: "反审核", click:checkNoRow,img: '../lib/ligerUI/skins/icons/refresh.gif'},
                  { line: true },
                
                
                  { text: "删除供应商", click: deleteRow,img: '../lib/ligerUI/skins/icons/delete.gif'},
                  { line: true },

                 
                    { text: '批量导入', click: excel, img: '../lib/ligerUI/skins/icons/xls.gif' }
          
                
                    ]
                    }

                
                
            }
            );

        });
        
        
           function excel()
        {
            var title = "导入供应商";

            $.ligerDialog.open({
                title: title,
                url: 'VenderListExcel.aspx',
                height: 450,
                width: 550,
                modal: true
            });
            
            

        }
        

         function addRowTop()
        {
             
            var title="新增供应商";
         
            
            $.ligerDialog.open({ 
                title : title,
                url: 'VenderListAdd.aspx?id=0',
                height:500,
                width:650,
                modal:false
            });
          
        } 
        
        function editRow()
        {
            var row = manager.getSelectedRow();
            if (!row) {  $.ligerDialog.warn('请选择修改的行！'); return; }

          
            
            var title="修改供应商-"+row.names;
           
            $.ligerDialog.open({ 
                title : title,
                url: 'VenderListAdd.aspx?id='+row.id,
                height:500,
                width:650,
                modal:true
               
            });
            
            
        }
        
         function linkManForm()
        {
            var row = manager.getSelectedRow();
            if (!row) {  $.ligerDialog.warn('请选择供应商！'); return; }

          
            
            var title="联系人管理-"+row.names;
           
            $.ligerDialog.open({ 
                title : title,
                url: 'VenderLinkMan.aspx?id='+row.id,
                height:400,
                width:650,
                modal:true
               
            });
            
            
        }


       function search() {

            
            $.ligerDialog.prompt('可按照名称、电话、联系人查询','', function (yes,value) {
            
             if(yes) 
             {

              
                var key = value;
                manager.changePage("first");
                manager._setUrl("VenderList.aspx?Action=GetDataListSearch&keys="+key);
            }
             
             });
           
        }


       
      function deleteRow()
        {
           
             var row = manager.getSelectedRow();
            if (!row) {  $.ligerDialog.warn('请选要删除的择行'); return; }
            
            var idString = checkedCustomer.join(',');//获取选中的ID字符串，用‘，’隔开，传递到后台即可

              $.ligerDialog.confirm('删除后不能恢复，确认删除？', function(type) {

                if (type) {

                    $.ajax({
                        type: "GET",
                        url: "VenderList.aspx",
                        data: "Action=delete&idString=" + idString + "&ranid=" + Math.random(), //encodeURI
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
              url: "VenderList.aspx",
              data: "Action=checkRow&idString=" + idString + "&ranid=" + Math.random(), //encodeURI
              success: function (resultString) {
                  $.ligerDialog.alert(resultString, '提示信息');
                  reload();

              },
              error: function (msg) {

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
              url: "VenderList.aspx",
              data: "Action=checkNoRow&idString=" + idString + "&ranid=" + Math.random(), //encodeURI
              success: function (resultString) {
                  $.ligerDialog.alert(resultString, '提示信息');
                  reload();

              },
              error: function (msg) {

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

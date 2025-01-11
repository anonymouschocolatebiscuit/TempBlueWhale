<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="goodsBomList.aspx.cs" Inherits="Lanwei.Weixin.UI.produce.goodsBomList" %>
<%@ Import namespace="Lanwei.Weixin.Common" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
   <title>组织架构</title>
   <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
     

   <script type="text/javascript">

       $(function () {

           $("#layout1").ligerLayout({ leftWidth: 180, allowLeftCollapse: false });
       });

         </script> 
        <style type="text/css"> 

            body{ padding:5px; margin:0; padding-bottom:15px;}
            #layout1{  width:100%;margin:0; padding:0;  }  
            .l-page-top{ height:80px; background:#f8f8f8; margin-bottom:3px;}
            h4{ margin:20px;}
                </style>



    
       <script type="text/javascript">

         


           $(function () {

               //创建表单结构
               var form = $("#form").ligerForm();
           });

        


           $(function () {

             


             var tree=  $("#tree1").ligerTree({
                 url: "goodsBomList.aspx?Action=GetTreeList",

                   checkbox:false,
                   parentIcon: 'folder',
                   childIcon: 'leaf',

                   slide: false,
                   treeLine: true,
                   idFieldName: 'id',
                   isExpand: 3,

                   parentIDFieldName: 'pid',
                   onSelect: onSelect



               }

               );
           });


           function onSelect(note) {
              

               manager._setUrl("goodsBomList.aspx?Action=GetDataList&typeId=" + note.data.id);

            
           }

       
           function reloadTree() {
          
               location.reload();
           }

           //清单开始

           var manager;

           $(function () {
             


               manager = $("#maingrid").ligerGrid({
                  

                   columns: [


                    { display: 'BOM分组', name: 'typeName', width: 70, align: 'center' },

                    { display: 'BOM单编号', name: 'number', width: 160, align: 'center' },
                    { display: '版本', name: 'edition', width: 70, align: 'center' },
                    { display: '图纸号', name: 'tuhao', width: 70, align: 'center' },
                    
                    { display: '状态', name: 'flagCheck', width: 50, align: 'center' },
                    { display: '物料代码', name: 'code', width: 100, align: 'center' },

                    { display: '物料名称', name: 'goodsName', width: 180, align: 'center' },
                    { display: '规格型号', name: 'spec', width: 100, align: 'center' },
                    { display: '单位', name: 'unitName', width: 70, align: 'center' },
                    { display: '数量', name: 'num', width: 70, align: 'center' },
                    { display: '成品率%', name: 'rate', width: 70, align: 'center' },

                    { display: '制单', name: 'makeName', width: 70, align: 'center' },
                    { display: '制单日期', name: 'makeDate', width: 80, align: 'center' },
                    { display: '审核', name: 'checkName', width: 70, align: 'center' },
                    { display: '审核日期', name: 'checkDate', width: 80, align: 'center' },

                    { display: '备注', name: 'remarks', width: 180, align: 'center' },




                   ], width: '99%', height: '50%',

                   url: 'goodsBomList.aspx?Action=GetDataList&typeId=0',
                   alternatingRow: false,
                   rownumbers: true,
                   usePager: false,
                 

                   onDblClickRow: function (data, rowindex, rowobj) {                      

                       editRow();

                   },

                   onSelectRow: function (data, rowindex, rowobj) {
                       //$.ligerDialog.alert('1选择的是' + data.CustomerID);
                       viewRow();
                   },


                   toolbar: {
                       items: [

                       { text: '刷新', click: reload, img: '../lib/ligerUI/skins/icons/refresh.png' },
                       { line: true },

                       { text: "新增", click: addRow, img: '../lib/ligerUI/skins/icons/add.gif' },
                       { line: true },

                       { text: "修改", click: editRow, img: '../lib/ligerUI/skins/icons/modify.gif' },
                       { line: true },

                       { text: "审核", click: checkRow, img: '../lib/ligerUI/skins/icons/ok.gif' },
                       { line: true },

                       { text: "反审", click: checkNoRow, img: '../lib/ligerUI/skins/icons/back.gif' },
                       { line: true },

                       { text: "删除", click: deleteRow, img: '../lib/ligerUI/skins/icons/delete.gif' }

                       ]
                   }

                   

               }
               );
           });

           function editRow() {
               var row = manager.getSelectedRow();
               if (!row) { $.ligerDialog.warn('请选择修改的行！'); return; }

               var id = row.id;

               if (row.flagCheck == "已审核") {
                   $.ligerDialog.warn('已审核的单据不能修改！');
                   return;
               }

               parent.f_addTab('goodsBomListEdit', 'BOM清单-修改', 'produce/goodsBomListEdit.aspx?id=' + id);

           }

           function deleteRow() {

               var row = manager.getSelectedRow();
               if (!row) { $.ligerDialog.warn('请选要删除的择行'); return; }

               var id = row.id;//获取选中的ID字符串，用‘，’隔开，传递到后台即可

               if (row.flagCheck == "已审核")
               {
                   $.ligerDialog.warn('已审核的单据不能删除！');
                   return;
               }
               //alert(idString);
               $.ligerDialog.confirm('删除后不能恢复，确认删除？', function (type) {


                   if (type) {

                       $.ajax({
                           type: "GET",
                           url: "goodsBomList.aspx",
                           data: "Action=delete&id=" + id + " &ranid=" + Math.random(),
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

           function addRow() {

               var title = "新增BOM清单";

               parent.f_addTab('goodsBomListAdd', 'BOM清单-新增', 'produce/goodsBomListAdd.aspx');


             



           }


           function checkRow() {

               var row = manager.getSelectedRow();
               if (!row) { $.ligerDialog.warn('请选要操作的择行'); return; }

               var id = row.id;//获取选中的ID字符串，用‘，’隔开，传递到后台即可
               
              // alert("id:" + id);
              // window.open("goodsBomList.aspx?Action=checkRow&id=" + id + "&ranid=" + Math.random());


               $.ajax({
                   type: "GET",
                   url: "goodsBomList.aspx",
                   data: "Action=checkRow&id=" + id + "&ranid=" + Math.random(), //encodeURI
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

               var id = row.id; //获取选中的ID字符串，用‘，’隔开，传递到后台即可

               $.ajax({
                   type: "GET",
                   url: "goodsBomList.aspx",
                   data: "Action=checkNoRow&id=" + id + "&ranid=" + Math.random(), //encodeURI
                   success: function (resultString) {
                       $.ligerDialog.alert(resultString, '提示信息');
                       reload();

                   },
                   error: function (msg) {

                       $.ligerDialog.alert("网络异常，请联系管理员", '提示信息');
                   }
               });


           }


           function viewRow() {

               var row = manager.getSelectedRow();
               if (!row) { alert('请选择行'); return; }



               var id = row.id;

               maingridItem._setUrl("goodsBomList.aspx?Action=GetDataListSub&pId=" + id);




           }

           function reload() {
               manager.reload();
           }

           //明细开始

           var maingridItem;
           $(function () {



               maingridItem = $("#maingridItem").ligerGrid({
                   columns: [

                   
                    { display: '物料代码', name: 'code', width: 80, align: 'center' },
                    { display: '物料名称', name: 'goodsName', width: 150, align: 'center' },
                    { display: '规格型号', name: 'spec', width: 100, align: 'center' },
                    { display: '单位', name: 'unitName', width: 100, align: 'center' },
                    { display: '数量', name: 'num', width: 100, align: 'left' },
                    { display: '损耗率', name: 'rate', width: 100, type: 'int', align: 'center' },
                    { display: '备注', name: 'remarks', width: 150, align: 'center' }

                   ], width: '99%', height: '50%',

                   rownumbers: true,
                   alternatingRow: false,
                   usePager: false


               }
               );
           });


           function reloadItem() {
               maingridItem.reload();
           }



    </script>

       
    <style type="text/css">
    .l-button{width: 120px; float: left; margin-left: 10px; margin-bottom:2px; margin-top:2px;}
    </style>


    
</head>
<body style="padding-left:10px;">
    <form id="form1" runat="server">
  
   
   

     <div id="layout1">
            <div id="showTitle" position="left" title="BOM分组">
         

             <div id="Div1" style="OVERFLOW:auto; WIDTH:99%;HEIGHT: 430px; text-align:left;"> 
             
                
                        
      <ul id="tree1">
     
    </ul>
                 
              </div>
            
            
            </div>
            <div id="rigthTitle" position="center" title="BOM清单">
          

 
                 <div id="maingrid"></div>


                 <div id="maingridItem"></div>
                
            </div>
          
        </div> 
  


    </form>
</body>
</html>

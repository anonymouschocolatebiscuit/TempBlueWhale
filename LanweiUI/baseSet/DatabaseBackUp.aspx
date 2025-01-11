<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DatabaseBackUp.aspx.cs" Inherits="Lanwei.Weixin.UI.baseSet.DatabaseBackUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>数据库备份</title>
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
                
                
                   { display: '备份名称', name: 'backName', id: 'levelName', width: 300, align: 'left' },
              
                 { display: '操作员', name: 'makeName', width: 200, type: 'int', align: 'center' },
                 { display: '备份日期', name: 'makeDate', width: 250, align: 'center' },
              

                  { display: '操作', isSort: false, width: 100, render: function(rowdata, rowindex, value) {
                      var h = "";
                      if (!rowdata._editing) {
                          h += "<a href='javascript:pdfDown()'>下载文件</a> ";

                      }

                      return h;
                  }
                  }


                
                ], width: '90%', pageSizeOptions: [5, 10, 15, 20],height: '300',

                url: 'DatabaseBackUp.aspx?Action=GetDataList', 
                alternatingRow: false, 
                 usePager: false,
                rownumbers: true, //显示序号
                toolbar: { items: [
               
                { text: '刷新', click: reload, img: '../lib/ligerUI/skins/icons/refresh.png'},
                { line: true },
               
             
               
                { text: "删除", click: deleteRow,img: '../lib/ligerUI/skins/icons/delete.gif'}
              
              
               
             
                
                ]
                }

                
                
            }
            );
        });
        
        
          function backUp()
          {
          
                var database = document.getElementById("txtNames").value;
                var names = document.getElementById("txtFileName").value;
                var path = document.getElementById("txtPath").value;
                var paths=path+names;
                
//                alert("database："+database);
//                alert("paths："+paths);
//                alert("names："+names);
                
             //   return ;
                
                
               $.ligerDialog.confirm('确认备份数据库？', function(type) {

                if (type) {

                    $.ajax({
                        type: "GET",
                        url: "DatabaseBackUp.aspx",
                        data: "Action=DataBackup&database=" + database + "&paths="+paths+"&names="+names+"&ranid=" + Math.random(), //encodeURI
                        success: function(resultString) {
                           
                           //判断数据库备份状态
                           checkOk();
                           
                          // $.ligerDialog.alert(resultString, '提示信息');
                           
                          

                        },
                        error: function(msg) {

                            $.ligerDialog.alert("备份网络异常！", '提示信息');
                            
                            return;
                        }
                    });

                }

            });
             
              
             
          }
        
        function checkOk()
        {
                var database = document.getElementById("txtNames").value;
                var names = document.getElementById("txtFileName").value;
                var path = document.getElementById("txtPath").value;
                var paths=path+names;
                
        
                var manager = $.ligerDialog.waitting('数据正在备份中,请稍候……'); 
                setTimeout(function () { 
                
                   manager.close();
                   
                    $.ajax({
                        type: "GET",
                        url: "DatabaseBackUp.aspx",
                        data: "Action=DataBackupCom&paths=" + paths +"&ranid=" + Math.random(), //encodeURI
                        success: function(resultString) {
                            
                            $.ligerDialog.alert(resultString);
                            reload();

                        },
                        error: function(msg) {

                            $.ligerDialog.alert("网络异常，请联系管理员", '提示信息');
                        }
                    });
                   
                   
                
                }, 16000);
                  
                  
        }
        
        
          function pdfDown() {

            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('请选要下载的行！'); return; }

            // alert(JSON.stringify(row));

            var pdfPath = row.backName;

            // alert(pdfPath);

            // return;

            if (pdfPath == "") {

                $.ligerDialog.warn('无附件！'); return;

            }
            else {

                //window.parent.parent.location.href = 'pdf/' + pdfPath;
                window.open('database/' + pdfPath);
            }


        }

        
        function deleteRow()
        {
           
             var row = manager.getSelectedRow();
            if (!row) {  $.ligerDialog.warn('请选择要删除的行'); return; }


              $.ligerDialog.confirm('删除后不能恢复，确认删除？', function(type) {

                if (type) {

                    $.ajax({
                        type: "GET",
                        url: "DatabaseBackUp.aspx",
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
        
     


         
    </script>
    <style type="text/css">
    .l-button{width: 120px; float: left; margin-left: 10px; margin-bottom:2px; margin-top:2px;}
    </style>

</head>
<body>
    <form id="form1" runat="server">
    
    <table id="sss" style=" line-height:40px; width:100%;" >
            <tr>
                <td style="width:100px;text-align:right;">
                    数据库名：</td>
                <td>
                    <asp:TextBox ID="txtNames"  runat="server" 
          Width="526px" ReadOnly="True"></asp:TextBox>
         
                </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    备份名称：</td>
                <td>
                    <asp:TextBox ID="txtFileName"  runat="server" 
          Width="526px" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    备份路径：</td>
                <td>
                    <asp:TextBox ID="txtPath"  runat="server" Width="527px" 
          ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    &nbsp;</td>
                <td>
                    <input id="btnBack" type="button" value="备 份……" class="ui-btn ui-btn-sp mrb" onclick="backUp()" /></td>
            </tr>
            <tr>
                <td style="text-align:center; padding-left:10px;" colspan="2">
            
            
    
      <div id="maingrid">
      </div>
    
                </td>
            </tr>
            </table>
            
            
    
    </form>
</body>
</html>

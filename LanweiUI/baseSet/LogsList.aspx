<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogsList.aspx.cs" Inherits="Lanwei.Weixin.UI.baseSet.LogsList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>日志管理</title>
     <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>


    
       <script type="text/javascript">
      
        var manager;
        $(function() {
        
        var form = $("#form").ligerForm();

        
            manager = $("#maingrid").ligerGrid({
            checkbox: true,
                columns: [


                 { display: '编号', name: 'id', id: 'id', width: 50, align: 'center' },
                 { display: '用户', name: 'users', width: 280, type: 'int', align: 'left' },
                 { display: '内容', name: 'events', width: 370, align: 'left' },
                 { display: 'IP', name: 'ip', width: 170, align: 'center'},
                  { display: '时间', name: 'date', width: 170, align: 'center' }
                
            
                ], width: '99%', pageSizeOptions: [5, 10, 15, 20],height: '99%',
                  pageSize: 15,
                  dataAction: 'local', //本地排序
                  usePager: true,
                url: "LogsList.aspx?Action=GetDataList", 
                alternatingRow: false,
                isChecked: f_isChecked, onCheckRow: f_onCheckRow, onCheckAllRow: f_onCheckAllRow

                
                
            }
            );

        });
        

        function f_set() {

            
            form.setData({
            
            keys:"",
            dateStart: new Date("<% =start%>"),
            dateEnd: new Date("<% =end%>")
        });


        }


        function search() {

            var keys = document.getElementById("txtKeys").value;
            if (keys == "请输入用户/事件/IP地址") {

                keys = "";

            }
            var start = document.getElementById("txtDateStart").value;
            var end = document.getElementById("txtDateEnd").value;

            manager.changePage("first");
            manager._setUrl("LogsList.aspx?Action=GetDataListSearch&keys=" + keys + "&start=" +start + "&end=" + end);
        }


       
        function deleteRow() {

            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('请选要删除的择行'); return; }
            
            var idString = checkedCustomer.join(',');//获取选中的ID字符串，用‘，’隔开，传递到后台即可


          


            $.ligerDialog.confirm('删除后不能恢复，确认删除？', function(type) {

              
                if (type) {
                
                    $.ajax({
                        type: "GET",
                        url: "LogsList.aspx",
                        data: "Action=delete&id=" + idString + " &ranid=" + Math.random(),
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
    <style type="text/css">
    .l-button{width: 120px; float: left; margin-left: 10px; margin-bottom:2px; margin-top:2px;}
    </style>


    
</head>
<body style="padding-left:10px; padding-top:10px;">
    <form id="form1" runat="server">
  
    <table id="form" border="0" cellpadding="0" cellspacing="0" style="width:100%; line-height:40px;">
           <tr>
           <td style="text-align:right; width:50px;">
            
            关键字： 
            
            
            </td>
           <td style="text-align:left; width:180px;">
            
             
            
               <asp:TextBox ID="txtKeys" runat="server" nullText="请输入用户/事件/IP地址"></asp:TextBox>
            
             
            
             
            
             
            
            </td>
           <td style="text-align:right; width:70px;">
            
               开始日期：
            
              </td>
           <td style="text-align:left; width:180px;">
            
           <asp:TextBox ID="txtDateStart" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
            
            
           </td>
           <td style="text-align:right; width:70px;">
            
            
               结束日期：
             
             
            
           
           </td>
           <td style="text-align:left;">
            
            <asp:TextBox ID="txtDateEnd" runat="server" ltype="date" validate="{required:true}"></asp:TextBox> 
             
            
            </td>
           <td style="text-align:left;width:220px;">
           
           
          
           
           
               <input id="btnAdd" type="button" value="查询" class="ui-btn ui-btn-sp mrb" onclick="search()" />
               
               <input 
                   id="btnReload" class="ui-btn" type="button" value="删除" onclick="deleteRow()" />
            
            
            
            
                   
            </td>
           </tr>
           <tr>
           <td style="text-align:left; height:300px;" colspan="7">
            
            <div id="maingrid"></div>  
            <div style="display:none;">
   
</div>
            
            </td>
           </tr>
           </table>
    
  

    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsList.aspx.cs" Inherits="Lanwei.Weixin.UI.baseSet.NewsList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>新闻管理</title>
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
    <script src="../lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
 
   

       <script type="text/javascript">

          
        var manager;
        $(function() {

        var form = $("#form").ligerForm();
        
           var dateStart =  $.ligerui.get("txtDateStart");
         dateStart.set("Width", 110);
         
         var  dateEnd=  $.ligerui.get("txtDateEnd");
         dateEnd.set("Width", 110);
         
   
         
       

         var  txtFlagList=  $.ligerui.get("txtFlagList");
         txtFlagList.set("Width", 100);

        manager = $("#maingrid").ligerGrid({
            checkbox:true,
                columns: [
                
               {
                    display: '操作', isAllowHide: false,width: 50,
                    render: function (row)
                    {
                        var html = '<a href="#" onclick="replay()">修改</a>';
                        return html;
                    }
                },
                
                 { display: '新闻类别', name: 'typeName', width: 150, align: 'center' },

                 { display: '发布日期', name: 'makeDate', width: 150, align: 'center'},
                  
                 { display: '标题', name: 'title', width: 480, align: 'left' },
               
                  { display: '浏览次数', name: 'hot', width: 100, align: 'center' }
              
             
                ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '99%',

                 url: 'NewsList.aspx?Action=GetDataList',
                alternatingRow: false,
                rownumbers: true, //显示序号
                pageSize: 15,
                dataAction: 'local', //本地排序
                usePager: true,
                isChecked: f_isChecked, onCheckRow: f_onCheckRow, onCheckAllRow: f_onCheckAllRow
                   

            }
            );
        });


        function deleteRow() {

            var row = manager.getSelectedRow();
            if (!row) { $.ligerDialog.warn('请选要删除的择行'); return; }
            
            var idString = checkedCustomer.join(','); //获取选中的ID字符串，用‘，’隔开，传递到后台即可
            $.ligerDialog.confirm('删除后不能恢复，确认删除？', function(type) {

                if (type) {

                    $.ajax({
                        type: "GET",
                        url: "NewsList.aspx",
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

        function search() {

           
           var flag=  $("#txtFlagList").val();



            var keys = document.getElementById("txtKeys").value;
            if (keys == "请输入新闻类别/新闻标题") {

                keys = "";

            }
            var start = document.getElementById("txtDateStart").value;
            var end = document.getElementById("txtDateEnd").value;

            manager.changePage("first");
            manager._setUrl("NewsList.aspx?Action=GetDataListSearch&keys=" + keys + "&start=" + start + "&end=" + end+'&flag='+flag);
        }


     
     
        function reload() {
            manager.reload();
        
        }

       function add() {
          
           $.ligerDialog.open({
                height:650,
                width: 640,
                title : '发布新闻',
                url: 'NewsListAdd.aspx?id=0', 
                showMax: false,
                showToggle: true,
                showMin: false,
                isResize: true,
                slide: false
                
            });



          
        }

       function replay() {
          
            var row = manager.getSelectedRow();
            if (!row) {  $.ligerDialog.warn('请选择修改的行！'); return; }
            
            
              $.ligerDialog.open({
                height:650,
                width: 640,
                title : '修改新闻',
                url: 'NewsListAdd.aspx?id='+row.id, 
                showMax: false,
                showToggle: true,
                showMin: false,
                isResize: true,
                slide: false
                
            });



          
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
<body style="background:#f5f5f5 url(../images/index_bg.png) 0 bottom no-repeat; background-attachment:fixed;padding-left:10px;">
    <form id="form1" runat="server">
  
    <table id="form" border="0" cellpadding="0" cellspacing="0" style="width:100%; line-height:40px;">
           <tr>
           <td style="text-align:right; width:60px;">
            
               关键字： 
            
            
            </td>
           <td style="text-align:left; width:180px;">
            
             
            
               <asp:TextBox ID="txtKeys" runat="server" nullText="请输入新闻类别/新闻标题"></asp:TextBox>
            
             
            
             
            
             
            
            </td>
           <td style="text-align:right; width:50px;">
            
               日期：
            
              </td>
           <td style="text-align:left; width:110px;">
            
           <asp:TextBox ID="txtDateStart" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
            
            
           </td>
           <td style="text-align:center; width:40px;">
            
            
             至

           </td>
           <td style="text-align:left; width:130px;">
            
              <asp:TextBox ID="txtDateEnd" runat="server" ltype="date" validate="{required:true}"></asp:TextBox> 
           
           </td>
           <td style="text-align:center; width:80px;">
            
           新闻类别 
           
           </td>
           <td style="text-align:left; width:110px;">
            
          
            <input type="text" id="txtFlagList"/>  
             
             
             </td>
           <td style="text-align:left;">
           
           
          
           
           
               <input id="btnSearch" type="button" value="查询" class="ui-btn ui-btn-sp mrb" onclick="search()" />
               
                <input id="btnAdd" type="button" value="发布" class="ui-btn ui-btn-sp mrb" onclick="add()" />
                
               
               <input  id="btnReload" class="ui-btn" type="button" value="删除" onclick="deleteRow()" />
            
            
            
            
                   
            </td>
           </tr>
           <tr>
           <td style="text-align:left; height:300px;" colspan="9">
            
            <div id="maingrid"></div>  
           
            
            </td>
           </tr>
           </table>
    
  
  <script type="text/javascript">
  
    $("#txtFlagList").ligerComboBox({  
                data: [
                    { text: '全部', id: '0' },
                    { text: '企业动态', id: '1' },
                    { text: '最新政策', id: '2' }
                ], valueFieldID: 'flag'
            }); 
            
      $("#txtFlagList").ligerGetComboBoxManager().setValue(0);

  
  </script>

    </form>
</body>
</html>

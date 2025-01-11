<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VenderListSelect.aspx.cs" Inherits="Lanwei.Weixin.UI.baseSet.VenderListSelect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>供应商列表</title>
    
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
             
             var ddlTypeList =  $.ligerui.get("ddlTypeList");
         ddlTypeList.set("Width", 130);
         
          var txtKeys =  $.ligerui.get("txtKeys");
         txtKeys.set("Width", 300);
            
        
           manager = $("#maingrid4").ligerGrid({
            
               //checkbox:true,
                columns: [
               
                 { display: '供应商类别', name: 'typeName', width: 100, type: 'int', align: 'center' },
                 { display: '供应商编号', name: 'code', width: 100, align: 'center' },
                 { display: '供应商名称', name: 'names', width: 230, align: 'left' },
                  { display: '余额日期', name: 'yueDate', width: 80, align: 'center' },
                  { display: '期初余额', name: 'balance', width: 70, align: 'center' },
                  { display: '税率%', name: 'tax', width: 60, align: 'center' },
                  { display: '首要联系人', name: 'linkMan', width: 70, align: 'center' },

                  { display: '手机', name: 'phone', width: 100, align: 'center' },
                  { display: '座机', name: 'tel', width: 110, align: 'center', type: "date" },
                  { display: 'QQ', name: 'qq', width: 80, align: 'center' },
                  { display: '状态', name: 'flag', width: 80, align: 'center' },
                  { display: '录入日期', name: 'makeDate', width: 80, align: 'center', type: "date" }
            
               
               
                ],  pageSize:10,
                rownumbers:true,//序号
                usePager: false,
                 url: 'VenderListSelect.aspx?Action=GetDataList', 
                width: '850',height:'420'
            }); 
            $("#pageloading").hide();
        });
        function f_select()
        {
            return manager.getSelectedRow();
        }
        
          function search() {

           
                var keys = document.getElementById("txtKeys").value;
                if (keys == "请输入名称/手机/姓名/备注/地址") {

                    keys = "";

                }
                
                var typeId=$("#ddlTypeList").val();
                
            
                
                manager.changePage("first");
                manager._setUrl("VenderListSelect.aspx?Action=GetDataListSearch&keys="+keys+"&typeId="+typeId); 
           
              
        }


        
        
    </script>



    
</head>
<body>
    <form id="form1" runat="server">
   
   <table id="formTB" border="0" style="width:780px; line-height:40px;">
   
   <tr>
   <td style="width:120px;">
       <asp:DropDownList ID="ddlTypeList" runat="server">
       </asp:DropDownList>
       </td>
   <td style="width:300px;">
       <asp:TextBox ID="txtKeys" runat="server" nullText="请输入名称/手机/姓名/备注/地址"></asp:TextBox>
       </td>
   
   <td>
       <input id="btnSearch" type="button" value="查询" class="ui-btn" onclick="search()"  /></td>
   
   </tr>
   
   
   <tr>
   <td colspan="3">
   
   
    <div id="maingrid4" style="margin:0; padding:0"></div>



   
   
   </td>
   
   </tr>
   
   
   </table>
   
   
   

   
   
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessListAdd.aspx.cs" Inherits="Lanwei.Weixin.UI.baseSet.ProcessListAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">

      <title>工序管理</title>

    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>

       <script type="text/javascript">

         var dialog = frameElement.dialog; //调用页面的dialog对象(ligerui对象)
        
         $(function() {

         //创建表单结构
         var form = $("#form").ligerForm();
       
         });


     
         function closeDialog() {
             var dialog = frameElement.dialog;
            
             dialog.close(); //关闭dialog

         

         }
        
    </script> 
    
    
    
    



</head>
<body>
    <form id="form1" runat="server">
    
    
    <table id="form" border="0" cellpadding="0" cellspacing="10" style="width:380px; line-height:40px;">
    <tr>
    <td style="width:80px; text-align:right;">工序类别：</td>
    <td>
    
    
                                   <asp:DropDownList ID="ddlTypeList" Height="40px" runat="server" Width="100%">
                </asp:DropDownList>


    </td>
    
    </tr>
    
    
    
    <tr>
    <td style="width:80px; text-align:right;">工序名称：</td>
    <td>
    
    
    
        <asp:TextBox ID="txtNames" runat="server"></asp:TextBox>
    
    
    
    </td>
    
    </tr>
    
    
    
    <tr>
    <td style="width:80px; text-align:right;">单位：</td>
    <td>
    
        <asp:DropDownList ID="ddlUnitList" Height="40px" runat="server" Width="100%">
                </asp:DropDownList>

    
    </td>
    
    </tr>
    
    
    
    <tr>
    <td style="width:80px; text-align:right;">单价：</td>
    <td>
    
     <asp:TextBox ID="txtPrice"    runat="server"  placeholder="请输入默认工序单价"></asp:TextBox>

    
    </td>
    
    </tr>
    
    
    
    <tr>
    <td style="width:80px; text-align:right;">显示顺序：</td>
    <td>
    
    
    
          <asp:TextBox ID="txtSortId" runat="server"  ltype='spinner' 
            ligerui="{type:'int'}" value="1"></asp:TextBox>

    
    </td>
    
    </tr>
    
    
    
    <tr>
    <td style="text-align:right;">&nbsp;</td>
    <td style="text-align:right; padding-right:30px;">
    
    
        <asp:Button ID="btnSave" runat="server" Text="保 存"  class="ui_state_highlight" 
            onclick="btnSave_Click"/>
    
   
         <input id="btnCancel" class="ui-btn" type="button" value="关闭" onclick="closeDialog()" />
           
    
    </td>
    
    </tr>
    
    
    
    </table>
    
    
        <asp:HiddenField ID="hfId" runat="server" />
    
    
    </form>
</body>
</html>

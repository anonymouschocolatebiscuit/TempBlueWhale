<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsersListAdd.aspx.cs" Inherits="Lanwei.Weixin.UI.baseSet.UsersListAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>用户信息</title>
    
     <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
    
     <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>

   <script src="../lib/ligerUI/js/core/base.js" type="text/javascript"></script>
   
   
    <script src="../lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>  
    <script src="../lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>  
 


    <script src="../lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../lib/jquery-validation/jquery.validate.min.js" type="text/javascript"></script> 
    <script src="../lib/jquery-validation/jquery.metadata.js" type="text/javascript"></script>
    <script src="../lib/jquery-validation/messages_cn.js" type="text/javascript"></script>
    
  
    
     <script type="text/javascript"> 
     
        var dialog = frameElement.dialog; //调用页面的dialog对象(ligerui对象)
       
         
         $(function() {

         //创建表单结构
         var form = $("#form").ligerForm();

        
         
         });

      

      
        function closeDialog()
        {
            var dialog = frameElement.dialog; //调用页面的dialog对象(ligerui对象)
            dialog.close();//关闭dialog 
        }
        
    </script> 
 


  
 
    
</head>
<body>
    <form id="form1" runat="server">
    

    <table id="form" border="0" cellpadding="0" cellspacing="20" style="width:600px; line-height:45px;">
    <tr>
    <td style="width:90px; text-align:right;">手机：</td>
    <td style="width:170px;">
    
        <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
    
    
    </td>
    
     <td style="width:90px; text-align:right;">姓名：</td>
    <td style="width:170px;">
        <asp:TextBox ID="txtNames" runat="server"></asp:TextBox>
        </td>
    
    </tr>
    
    
    <tr>
    <td style="text-align:right;">电话：</td>
    <td>
    
    
        <asp:TextBox ID="txtTel" runat="server"></asp:TextBox>
    
    
    
    </td>
    
     <td style="text-align:right;">状态：</td>
    <td>
    
    
        <asp:DropDownList ID="ddlFlagList" runat="server">
            <asp:ListItem>启用</asp:ListItem>
            <asp:ListItem>禁用</asp:ListItem>
        </asp:DropDownList>
    
    
    
    </td>
    
    </tr>
    
    
    <tr>
    <td style="text-align:right;">邮箱：</td>
    <td>
    <asp:TextBox ID="txtEmail" runat="server"  validate="{required:true}"></asp:TextBox>
    
    </td>
    
     <td style="text-align:right;">QQ：</td>
    <td>
    
    <asp:TextBox ID="txtQQ" runat="server"  validate="{required:true}"></asp:TextBox>
    
    </td>
    
    </tr>
    
    
    <tr>
    <td style="text-align:right;">地址：</td>
    <td colspan="3">
     
        <textarea id="txtAddress" cols="78" name="S1" rows="2" runat="server"></textarea></td>
    
    </tr>
    
    
    
    <tr>
    <td style="text-align:right;">出生：</td>
    <td>
    <asp:TextBox ID="txtBrithDay" runat="server" ltype="date" 
            validate="{required:true}"></asp:TextBox>
    
    </td>
    
    <td align="right">
        入职时间：</td>
    
    <td>
    
    <asp:TextBox ID="txtComeDate" runat="server" ltype="date" 
            validate="{required:true}"></asp:TextBox>
    
    </td>
    
    </tr>
    
    
    
    <tr>
    <td style="text-align:right;">&nbsp;</td>
    <td colspan="3" style="text-align:right; padding-right:30px;">
    
    
        <asp:HiddenField ID="hfId" runat="server" />
    
    
        <asp:Button ID="btnSave" runat="server" Text="保 存"  class="ui_state_highlight" 
            onclick="btnSave_Click"/>
    
         <input id="btnCancel" class="ui-btn" type="button" value="关闭" onclick="closeDialog()" />
           
    
    </td>
    
    </tr>
    
    
    
    </table>
    
    
    
   
   
   
    </form>
    
   
</body>
</html>

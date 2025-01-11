<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsAttributeListAdd.aspx.cs" Inherits="Lanwei.Weixin.UI.baseSet.GoodsAttributeListAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">

      <title>客户提醒信息</title>
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
         liger.get("ddlRepeatWeek").set('disabled', true);
         liger.get("txtRepeatDays").set('disabled', true);   
         
         });


     
         function closeDialog() {
             var dialog = frameElement.dialog;
            
             dialog.close(); //关闭dialog

         

         }
        
    </script> 
    
    
    
    <script type="text/javascript">
        $(function() {
            $("#cbRepeat").change(function() {


            if (this.checked) {
                liger.get("ddlRepeatWeek").set('disabled', false);            

                }
                else {

                    liger.get("ddlRepeatWeek").set('disabled', true);
                 
                }

            });


            $("#ddlRepeatWeek").change(function() {

            var checkValue = $("#ddlRepeatWeek").val();
            if (checkValue == "自定义") {

               
                liger.get("txtRepeatDays").set('disabled', false);   
                $("#txtRepeatDays").val("0");
            }
            else {
              
                liger.get("txtRepeatDays").set('disabled', true);   
                
                if(checkValue=="每天")
                {
                    $("#txtRepeatDays").val("1");
                   
                }
                if(checkValue=="每周")
                {
                    $("#txtRepeatDays").val("7");
                   
                }
                if(checkValue=="每月")
                {
                    $("#txtRepeatDays").val("30");
                   
                }
                if(checkValue=="每季度")
                {
                    $("#txtRepeatDays").val("90");
                   
                }
                if(checkValue=="每半年")
                {
                    $("#txtRepeatDays").val("180");
                   
                }
                if(checkValue=="每年")
                {
                    $("#txtRepeatDays").val("365");
                   
                }
                
                
                
               
            }
            
            });
            
        });
         
 
    </script>



</head>
<body>
    <form id="form1" runat="server">
    
    
    <table id="form" border="0" cellpadding="0" cellspacing="10" style="width:380px; line-height:40px;">
    <tr>
    <td style="width:80px; text-align:right;">分类名称：</td>
    <td>
    
    
    
        <asp:TextBox ID="txtNames" runat="server"></asp:TextBox>
    
    
    
    </td>
    
    </tr>
    
    
    <tr>
    <td style="text-align:right;">显示顺序：</td>
    <td>
    
    
        <asp:TextBox ID="txtFlag" runat="server"  ltype='spinner' 
            ligerui="{type:'int'}" value="0"></asp:TextBox>
            
            
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

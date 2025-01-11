<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientListAdd.aspx.cs" Inherits="Lanwei.Weixin.UI.BaseSet.ClientListAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>新增客户</title>
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

      

      
        function closeDialog()
        {
            var dialog = frameElement.dialog; //调用页面的dialog对象(ligerui对象)
            dialog.close();//关闭dialog 
        }
        
    </script>
     
     
    <style type="text/css">
           body{ font-size:12px;}
        .l-table-edit {}
        .l-table-edit-td{ padding:4px;}
        .l-button-submit,.l-button-test{width:80px; float:left; margin-left:10px; padding-bottom:2px;}
        .l-verify-tip{ left:230px; top:120px;}
    </style>
    
    
    
    
</head>
<body style="font-size:10pt;">
    <form id="form1" runat="server">
  <table id="form" border="0" cellpadding="0" cellspacing="20" style="width:600px; line-height:45px;">
            <tr>
                <td style="width:100px;text-align:right;">
                    客户编号：</td>
                <td style="width:180px;">
                    <asp:TextBox ID="txtCode" runat="server" validate="{required:true}"></asp:TextBox>
                    </td>
                <td style="width:100px; text-align:right;">
                    客户名称：</td>
                <td style="width:180px;">
                    <asp:TextBox ID="txtNames" runat="server" validate="{required:true}"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    客户类别：</td>
                <td>
                    <asp:DropDownList ID="ddlVenderTypeList" runat="server">
                    </asp:DropDownList>
                    </td>
                <td align="right">
                    余额日期：</td>
                <td>
                    <asp:TextBox ID="txtYueDate" runat="server"  ltype="date" validate="{required:true}" ></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    期初应收款：</td>
                <td>
                    <asp:TextBox ID="txtPayNeed" runat="server"></asp:TextBox>
                    </td>
                <td align="right">
                    期初预收款：</td>
                <td>
                    <asp:TextBox ID="txtPayReady" runat="server"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    税率：</td>
                <td>
                    <asp:TextBox ID="txtTax" runat="server"></asp:TextBox>
                    </td>
                <td align="right">
                    税号：</td>
                <td>
                    <asp:TextBox ID="txtTaxNumber" runat="server"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    开户银行：</td>
                <td>
                    <asp:TextBox ID="txtBankName" runat="server"></asp:TextBox>
                    </td>
                <td align="right">
                    银行账号：</td>
                <td>
                    <asp:TextBox ID="txtBankNumber" runat="server"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    地址：</td>
                <td colspan="3">
                    <asp:TextBox ID="txtDizhi" runat="server" TextMode="MultiLine" 
                        Width="438px"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    备注信息：</td>
                <td colspan="3">
                    <asp:TextBox ID="txtRemarks" runat="server" Height="40px" TextMode="MultiLine" 
                        Width="438px"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    &nbsp;</td>
                <td colspan="3" style=" text-align:right;">
                    
                
                
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

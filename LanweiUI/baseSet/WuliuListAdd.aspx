<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WuliuListAdd.aspx.cs" Inherits="Lanwei.Weixin.UI.baseSet.WuliuListAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>新增物流公司</title>
         <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    
         <script type="text/javascript">
       var dialog = frameElement.dialog; //调用页面的dialog对象(ligerui对象)
        
         $(function() {

         //创建表单结构
         var form = $("#form").ligerForm();
         
          var g =  $.ligerui.get("txtAddress");
            g.set("Width", 520);
      
         
         });


     
         function closeDialog() {
             var dialog = frameElement.dialog;
            
             dialog.close(); //关闭dialog

         

         }
        
        
         
    </script>
    <style type="text/css">
           body{ font-size:12px;}
        .l-table-edit {}
        .l-table-edit-td{ padding:4px;}
        .l-button-submit,.l-button-test{width:80px; float:left; margin-left:10px; padding-bottom:2px;}
        .l-verify-tip{ left:230px; top:120px;}
    </style>
    
    
    
    
    <script src="../lib/json2.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table id="form" style="height:280px;" >
            <tr>
                <td style="width:100px;text-align:right;">
                    公司编号：</td>
                <td style="width:250px;">
                    <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>

                    </td>
                <td style="width:100px; text-align:right;">
                    公司名称：</td>
                <td style="width:250px;">
                    <asp:TextBox ID="txtNames" runat="server"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    联系人：</td>
                <td>
                    <asp:TextBox ID="txtLinkMan" runat="server"></asp:TextBox>
                    </td>
                <td align="right">
                    电话：</td>
                <td>
                    <asp:TextBox ID="txtTel" runat="server"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    手机：</td>
                <td>
                    <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                    </td>
                <td align="right">
                    传真：</td>
                <td>
                    <asp:TextBox ID="txtFax" runat="server"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    地址：</td>
                <td colspan="3">
                    <asp:TextBox ID="txtAddress" runat="server" 
                        Width="438px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    电商物流：</td>
                <td>
                    <asp:DropDownList ID="ddlMallList" runat="server">
                        <asp:ListItem Value="0">(空)</asp:ListItem>
                        <asp:ListItem>中国邮政平邮</asp:ListItem>
                        <asp:ListItem>圆通速递</asp:ListItem>
                        <asp:ListItem>顺丰速运</asp:ListItem>
                        <asp:ListItem>中通速递</asp:ListItem>
                        <asp:ListItem>申通E物流</asp:ListItem>
                        <asp:ListItem>EMS</asp:ListItem>
                        <asp:ListItem>EMS经济快递</asp:ListItem>
                        <asp:ListItem>广东EMS</asp:ListItem>
                        <asp:ListItem>韵达快运</asp:ListItem>
                        <asp:ListItem>宅急送</asp:ListItem>
                        <asp:ListItem>联邦快递</asp:ListItem>
                        <asp:ListItem>德邦物流</asp:ListItem>
                        <asp:ListItem>华强物流</asp:ListItem>
                        <asp:ListItem>天天快递</asp:ListItem>
                        <asp:ListItem>国通快递</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right">
                    打印模板：</td>
                <td>
                    <asp:DropDownList ID="ddlPrintModel" runat="server">
                        <asp:ListItem Value="0">(空)</asp:ListItem>
                        <asp:ListItem>中国邮政平邮</asp:ListItem>
                        <asp:ListItem>圆通速递</asp:ListItem>
                        <asp:ListItem>顺丰速运</asp:ListItem>
                        <asp:ListItem>中通速递</asp:ListItem>
                        <asp:ListItem>申通E物流</asp:ListItem>
                        <asp:ListItem>EMS</asp:ListItem>
                        <asp:ListItem>EMS经济快递</asp:ListItem>
                        <asp:ListItem>广东EMS</asp:ListItem>
                        <asp:ListItem>韵达快运</asp:ListItem>
                        <asp:ListItem>宅急送</asp:ListItem>
                        <asp:ListItem>联邦快递</asp:ListItem>
                        <asp:ListItem>德邦物流</asp:ListItem>
                        <asp:ListItem>华强物流</asp:ListItem>
                        <asp:ListItem>天天快递</asp:ListItem>
                        <asp:ListItem>国通快递</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    &nbsp;</td>
               <td align="center" colspan="3">
                    <asp:Button ID="btnSave" runat="server" class="ui_state_highlight" 
               Text="保 存" onclick="btnSave_Click" />
                             &nbsp;
                      <input id="btnCancel" class="ui-btn" type="button" value="关闭" onclick="closeDialog()" />
                    
                    
                    
                    
                    </td>
            </tr>
            </table>
                    <asp:HiddenField ID="hf" runat="server" />
    </form>
</body>
</html>

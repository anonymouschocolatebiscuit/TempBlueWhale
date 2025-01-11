<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsListNumStart.aspx.cs" Inherits="Lanwei.Weixin.UI.BaseSet.GoodsListNumStart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>商品期初库存</title>
        
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
    <script src="../lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../lib/jquery-validation/jquery.validate.min.js" type="text/javascript"></script> 
    <script src="../lib/jquery-validation/jquery.metadata.js" type="text/javascript"></script>
    <script src="../lib/jquery-validation/messages_cn.js" type="text/javascript"></script>
 
     <script src="../lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script> 
    <script src="../lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>

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
                    商品编码：</td>
                <td style="width:180px;">
                    <asp:Label ID="lbCode" runat="server" Text="Label"></asp:Label>
                    </td>
                <td style="width:100px; text-align:right;">
                    商品名称：</td>
                <td style="width:180px;">
                    <asp:Label ID="lbNames" runat="server" Text="Label"></asp:Label>
                    </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    仓库：</td>
                <td>
                    <asp:DropDownList ID="ddlCangkuList" runat="server">
                    </asp:DropDownList>
                    </td>
                <td align="right">
                    期初数量：</td>
                <td>
                    <asp:TextBox ID="txtNum" runat="server"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    单位成本：</td>
                <td>
                    <asp:TextBox ID="txtPriceCost" runat="server"></asp:TextBox>
                    </td>
                <td align="right">
                     <asp:Button ID="btnSave" runat="server" class="ui_state_highlight" 
               Text="保 存" onclick="btnSave_Click" />
               
                             </td>
                <td>
                    &nbsp;
                    
                      <input id="btnCancel" class="ui-btn" type="button" value="关闭" onclick="closeDialog()" />
                    
                    
                    </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
             
            <asp:GridView ID="gvLevel" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="id" 
                onrowdatabound="gvLevel_RowDataBound" onrowdeleting="gvLevel_RowDeleting"  Width="100%"
                PageSize="15" ShowFooter="True">
                <Columns>
                    <asp:BoundField HeaderText="序号">
                        <ItemStyle Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ckName" HeaderText="仓库">
                        <ItemStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="num" HeaderText="期初数量">
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="priceCost" HeaderText="单位成本">
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="sumPrice" HeaderText="期初总价">
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    
                    <asp:CommandField HeaderText="删除" ShowDeleteButton="True">
                        <ItemStyle Width="80px" />
                    </asp:CommandField>
                </Columns>
                <EmptyDataTemplate>
                    <div>
                        信息暂无………………</div>
                </EmptyDataTemplate>
                <PagerSettings FirstPageImageUrl="~/images/main_54.gif" FirstPageText="首页" 
                    LastPageImageUrl="~/images/main_60.gif" LastPageText="尾页" 
                    Mode="NextPreviousFirstLast" NextPageImageUrl="~/images/main_58.gif" 
                    NextPageText="下一页" PreviousPageImageUrl="~/images/main_56.gif" 
                    PreviousPageText="上一页" />
                <FooterStyle CssClass="datagrid_footerstyle_normal" Height="20px" />
                <RowStyle Height="30px" HorizontalAlign="Center" />
            </asp:GridView>
                </td>
            </tr>
            </table>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsListImages.aspx.cs" Inherits="Lanwei.Weixin.UI.baseSet.GoodsListImages" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    
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
<body>
    <form id="form1" runat="server">
    
    
    
        
        
    <table border="0" cellpadding="0" width="800px">
    
    <tr>
    
    <td align="left" style="line-height:40px;"> 
    
    <asp:FileUpload ID="fload" runat="server" />
&nbsp; 默认显示：<asp:DropDownList ID="ddlMoren" runat="server">
                        <asp:ListItem Selected="True" Value="1">是</asp:ListItem>
                        <asp:ListItem Value="0">否</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp; <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="上 传" />


 &nbsp; &nbsp;    <input id="btnCancel" class="ui-btn" type="button" value="关闭" onclick="closeDialog()" />
           
        
    
     </td>
    
    </tr>
    
    
    <tr>
    
    <td>
    
    
    <asp:DataList ID="DataList1" runat="server" RepeatColumns="4" 
            RepeatDirection="Horizontal" onitemcommand="DataList1_ItemCommand" 
            onitemdatabound="DataList1_ItemDataBound">
            <ItemTemplate>
                
                <table border="0" width="80px" style=" text-align:center;">
                <tr>
                <td>
                   
                   
                    <asp:Image ID="Image2" runat="server" Height="200px"   Width="200px"
                      ImageUrl='<%#  "../goodsPicSmall/" + DataBinder.Eval(Container.DataItem,"imagesPath") %>' />
                
                
                </td>
                
                </tr>
                
                  <tr>
                      
                <td>
                
                    <asp:Label ID="lbMoren" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"moren") %>' ></asp:Label>
                
                 <asp:Button ID="btnMoren" runat="server" Text="设为默认"   CommandArgument='<%# DataBinder.Eval(Container.DataItem,"id") %>' 
                          CommandName="moren"  />
                          
                          &nbsp; 
                
                <asp:Button ID="btnDelete" runat="server" Text="删除"   CommandArgument='<%# DataBinder.Eval(Container.DataItem,"id") %>' 
                          CommandName="del"  />
                
                </td>
                
                </tr>
                
                </table>
                
                
                
            </ItemTemplate>
        </asp:DataList>
    
    
    
    </td>
    
    </tr>
    
    
     
    
    
    
    </table>    
        
    
   
   
    </form>
</body>
</html>

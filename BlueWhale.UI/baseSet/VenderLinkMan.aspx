<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VenderLinkMan.aspx.cs" Inherits="BlueWhale.UI.BaseSet.VenderLinkMan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Supplier Contact</title>
   <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
  
    
    <script src="../lib.1.3.1/Source/lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
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
                    Name：</td>
                <td style="width:180px;">
                    <asp:TextBox ID="txtNames" runat="server"></asp:TextBox>
                    </td>
                <td style="width:100px; text-align:right;">
                    Mobile Phone：</td>
                <td style="width:180px;">
                    <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    Landline Phone：</td>
                <td>
                    <asp:TextBox ID="txtTel" runat="server"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    Primary Contact Person：</td>
                <td>
                    <asp:DropDownList ID="ddlMoren" runat="server">
                        <asp:ListItem Selected="True" Value="1">True</asp:ListItem>
                        <asp:ListItem Value="0">False</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                <td align="right">
                    &nbsp;</td>
                <td>
                   
                         
                     <asp:Button ID="btnSave" runat="server" class="ui_state_highlight" 
               Text="Save" onclick="btnSave_Click" />
               
               &nbsp;
                <input id="btnCancel" class="ui-btn" type="button" value="Close" onclick="closeDialog()" />
                
                   
                   </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
             
            <asp:GridView ID="gvLevel" runat="server" AutoGenerateColumns="False"  Width="600px"
                DataKeyNames="id" onrowcancelingedit="gvLevel_RowCancelingEdit" 
                onrowdatabound="gvLevel_RowDataBound" onrowdeleting="gvLevel_RowDeleting" 
                onrowediting="gvLevel_RowEditing" onrowupdating="gvLevel_RowUpdating" 
                PageSize="15">
                <Columns>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label ID="lbNames" runat="server" 
                                Text='<%# DataBinder.Eval(Container.DataItem,"Names") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtNames0" runat="server" 
                                Text='<%# DataBinder.Eval(Container.DataItem,"Names") %>' Width="60px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="80px" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Mobile Phone">
                        <ItemTemplate>
                            <asp:Label ID="lbPhone" runat="server" 
                                Text='<%# DataBinder.Eval(Container.DataItem,"Phone") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPhone0" runat="server" 
                                Text='<%# DataBinder.Eval(Container.DataItem,"Phone") %>' Width="60px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="90px" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Landline Phone">
                        <ItemTemplate>
                            <asp:Label ID="lbTel" runat="server" 
                                Text='<%# DataBinder.Eval(Container.DataItem,"Tel") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtTel0" runat="server" 
                                Text='<%# DataBinder.Eval(Container.DataItem,"Tel") %>' Width="60px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="90px" />
                    </asp:TemplateField>
                    
<%--                      <asp:TemplateField HeaderText="QQ">
                        <ItemTemplate>
                            <asp:Label ID="lbQQ" runat="server" 
                                Text='<%# DataBinder.Eval(Container.DataItem,"QQ") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtQQ0" runat="server" 
                                Text='<%# DataBinder.Eval(Container.DataItem,"QQ") %>' Width="60px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="80px" />
                    </asp:TemplateField>--%>
                    
                      <asp:TemplateField HeaderText="Primary Contact Person">
                        <ItemTemplate>
                            <asp:Label ID="lbMoren" runat="server" 
                                Text='<%# DataBinder.Eval(Container.DataItem,"moren") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                           
                             <asp:DropDownList ID="ddlMoren0" runat="server" Width="50px">
                        <asp:ListItem Selected="True" Value="1">True</asp:ListItem>
                        <asp:ListItem Value="0">False</asp:ListItem>
                    </asp:DropDownList>
                           
                        </EditItemTemplate>
                        <ItemStyle Width="60px" />
                    </asp:TemplateField>
                    
                    <asp:CommandField HeaderText="Delete" ShowDeleteButton="True">
                        <ItemStyle Width="50px" />
                    </asp:CommandField>
                </Columns>
                <EmptyDataTemplate>
                    <div>
                        No Message………………</div>
                </EmptyDataTemplate>
                <PagerSettings FirstPageImageUrl="~/images/main_54.gif" FirstPageText="First" 
                    LastPageImageUrl="~/images/main_60.gif" LastPageText="Last" 
                    Mode="NextPreviousFirstLast" NextPageImageUrl="~/images/main_58.gif" 
                    NextPageText="Next" PreviousPageImageUrl="~/images/main_56.gif" 
                    PreviousPageText="Back" />
                <FooterStyle CssClass="datagrid_footerstyle_normal" Height="20px" />
                <RowStyle Height="30px" HorizontalAlign="Center" />
            </asp:GridView>
                </td>
            </tr>
            </table>
    </form>
</body>
</html>

﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsersListRight.aspx.cs" Inherits="Lanwei.Weixin.UI.baseSet.UsersListRight" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
      <title>权限设置</title>

       <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>


    
    <script type="text/javascript" src="../js/TreeViewCheckBoxSelected.js"></script>
    
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
    
    <div id="Div1" style="OVERFLOW:auto; WIDTH:99%;HEIGHT: 430px; text-align:left;"> 
             
                
                        <asp:TreeView ID="TreeViewRight" runat="server" ShowLines="True" onclick="client_OnTreeNodeChecked(event);"  
              ShowCheckBoxes="All" ExpandDepth="2">
                <DataBindings>
                      <asp:TreeNodeBinding DataMember="siteMapNode"   NavigateUrlField="url"  ToolTipField="seq"
                            TextField="title"  ValueField="id"  />
                  </DataBindings>
    </asp:TreeView>
    
                 
              </div>
     <asp:HiddenField ID="hf" runat="server" />
     
      <div style="height:10px; width:100%;"></div>
      
      
      
 <div class="footerDiv">
       
      
        
    
        <asp:Button ID="btnTongbu" runat="server" class="ui_state_highlight"  Text="同步权限菜单" Width="180px"
                   onclick="btnTongbu_Click" />
         
         
         &nbsp;<asp:Button ID="btnSave" runat="server"  class="ui_state_highlight"  Text="保存" 
            onclick="btnSave_Click" />
         
         
         <input id="btnCancel" class="ui-btn" type="button" value="关闭" onclick="closeDialog()" />
                    
                    
        
        </div>
        
        
        <div style="display:none;">
           
           
               <asp:TreeView ID="tvListXML" runat="server" DataSourceID="xmlDS">
                             
                              <DataBindings>
                      <asp:TreeNodeBinding DataMember="siteMapNode"  NavigateUrlField="url"   ToolTipField="seq"
                            TextField="title"  ValueField="id"  />
                  </DataBindings>
                  
                  
               </asp:TreeView>

               <asp:XmlDataSource ID="xmlDS" runat="server"></asp:XmlDataSource>

              </div>
              
              
        
    </form>
</body>
</html>

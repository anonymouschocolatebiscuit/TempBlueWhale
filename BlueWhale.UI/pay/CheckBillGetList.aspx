﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckBillGetList.aspx.cs" Inherits="BlueWhale.UI.pay.CheckBillGetList" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Collection Write-off - View</title>
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
    <script src="js/CheckBillGetList.js" type="text/javascript"></script>
            <style type="text/css">
.l-grid-header { height: 28px !important; /* Increase height */}
</style>



</head>
<body style="padding-left:10px; padding-top:10px;">
    <form id="form1" runat="server">
  
    <table id="form" border="0" cellpadding="0" cellspacing="0" style="width:100%; line-height:40px;">
           <tr>
           <td style="text-align:right; width:50px;">
            
            Keyword： 
            
            
            </td>
           <td style="text-align:left; width:180px;">
            
             
            
               <asp:TextBox ID="txtKeys" runat="server" placeholder="Search Receipt No./Client/Remarks"></asp:TextBox>
            
             
            
             
            
             
            
            </td>
           <td style="text-align:right; width:80px;">
            
               Start Date：
            
              </td>
           <td style="text-align:left; width:180px;">
            
           <asp:TextBox ID="txtDateStart" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
            
            
           </td>
           <td style="text-align:right; width:80px;">
            
            
               End Date：
             
             
            
           
           </td>
            <td style="text-align:left; width:180px;">
            
            <asp:TextBox ID="txtDateEnd" runat="server" ltype="date" validate="{required:true}"></asp:TextBox> 
            
            
            
             
            
            </td>
           <td style="text-align:center;width:100px;">
           
           
           <input id="btnSearch" type="button" value="Search" class="ui-btn ui-btn-sp mrb" onclick="search()" />
           
            
            </td>
           <td style="text-align:right; padding-right:20px;">
           
          
           
           <input id="btnAdd" type="button" value="Create" class="ui-btn" onclick="add()" />
           
          
           
           
           
           
           
           
               <input id="btnCheck" type="button" value="Approve" class="ui-btn" onclick="checkRow()" />
                
                  <input id="btnCheckNo" type="button" value="Reject" class="ui-btn" onclick="checkNoRow()" />
                
               
             
               
               <input id="btnReload" class="ui-btn" type="button" value="Delete" onclick="deleteRow()" />
            
            
            
            
                   
            </td>
           </tr>
           <tr>
           <td style="text-align:left; height:300px;" colspan="8">
            
            <div id="maingrid"></div>  
            <div style="display:none;">
   
</div>
            
            </td>
           </tr>
           </table>
    
  

    </form>
</body>
</html>

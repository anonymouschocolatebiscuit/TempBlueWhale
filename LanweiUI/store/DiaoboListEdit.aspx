<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DiaoboListEdit.aspx.cs" Inherits="Lanwei.Weixin.UI.store.DiaoboListEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>新增调拨单</title>
   
   <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <%--  <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>--%>
    
    
    <script src="../lib.1.3.1/Source/lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>


     <script src="js/DiaoboListEdit.js" type="text/javascript"></script>


    
    
</head>
<body style=" padding-top:10px; padding-left:10px;">


    <form id="form1" runat="server">
   
 <table id="form" border="0" cellpadding="0" cellspacing="0" style="width:99%; line-height:40px;">
           <tr>
           <td style="width:80px; text-align:center;">
               调拨日期：</td>
           <td style="text-align:left; width:250px;">
            
             <asp:TextBox ID="txtBizDate" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
            
             
                   </td>
           <td style="text-align:right; width:80px;">
                                                  &nbsp;</td>
           <td style="text-align:left; width:180px;">
                                                  &nbsp;</td>
           <td style="text-align:right; width:80px;">
                                                  &nbsp;</td>
           <td style="text-align:left; width:180px;" >
                                                  &nbsp;</td>
           <td style="text-align:right; width:80px;">
                                                  &nbsp;</td>
           <td style="text-align:left;">
                                                  &nbsp;</td>
           </tr>
           </table>
 
 <div id="maingrid"></div>
  
 
 <table id="tbFooter" border="0" cellpadding="0" cellspacing="0" style="width:99%; line-height:50px;">
           <tr>
           <td style="width:80px; text-align:right;">
               备注信息：</td>
           <td style="text-align:left; ">
            
               <asp:TextBox ID="txtRemarks" runat="server" Width="540px" TextMode="MultiLine"></asp:TextBox>
              
                   
                   </td>
           <td style="text-align:right; padding-right:30px; ">
              
              
                   
               <input id="btnSave" class="ui-btn ui-btn-sp mrb" type="button" value="保 存"  runat="server"
                   onclick="save()"  />
                      
            
              
                   
               </td>
           </tr>
           </table>
           
           
           
         
           
           
    </form>
</body>
</html>

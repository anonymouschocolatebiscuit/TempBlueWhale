<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProduceProcessListDetailReport.aspx.cs" Inherits="Lanwei.Weixin.UI.report.ProduceProcessListDetailReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    
<title>生成工序明细表</title>
    
     <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
     

    <script src="js/ProduceProcessListDetailReport.js" type="text/javascript"></script>




</head>
<body style="padding-left:10px; padding-top:10px;">
    <form id="form1" runat="server">
  
    <table id="form" border="0" cellpadding="0" cellspacing="0" style="width:99%; line-height:40px;">
           <tr>
           <td style="text-align:right; width:70px;">
            
               生产日期： 
            
            
            </td>
           <td style="text-align:left; width:120px;">
            
             
            
           <asp:TextBox ID="txtDateStart" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
            
            
                   </td>
           <td style="text-align:center; width:30px;">
            
            
               至
            
              </td>
           <td style="text-align:left; width:120px;">
            
            <asp:TextBox ID="txtDateEnd" runat="server" ltype="date" validate="{required:true}"></asp:TextBox> 
            
            
            
             
            
           </td>
         
                 <td style="text-align:right;width:80px;">
           
               工序类别：</td>
           <td style="text-align:left;width:80px;">
           
           
              <asp:DropDownList ID="ddlTypeList" runat="server">
               </asp:DropDownList>
             
             
          
             
             </td>
           
           <td style="text-align:right;width:80px;">
           
               工序名称：</td>
           
           <td style="text-align:left;width:150px;">
           
               <asp:TextBox ID="txtItemList" runat="server"></asp:TextBox>
                   </td>
           
           <td style="text-align:right;width:80px;">
           
               生产员工：</td>
           
           <td style="text-align:left;width:80px;">
           
               <asp:DropDownList ID="ddlUserList" runat="server">
               </asp:DropDownList>

                   </td>
           
           <td style="text-align:right; padding-right:20px;">
           
              <input id="btnSearch" type="button" value="查询" class="ui-btn ui-btn-sp mrb" onclick="search()" />&nbsp;
           
           </td>
           </tr>
           
           <tr>
           <td style="text-align:left; " colspan="11">
            
           
            <div id="maingrid"> </div>
  
           
           </td>
           </tr>
           
           </table>
    
  

    </form>
</body>
</html>

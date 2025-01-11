<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OtherGetPayFlowReport.aspx.cs" Inherits="Lanwei.Weixin.UI.report.OtherGetPayFlowReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
     <title>其他收支报表</title>
    
     <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
     

   

    <script src="js/OtherGetPayFlowReport.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <table id="form" border="0" cellpadding="0" cellspacing="0" style="width:99%; line-height:40px;">
           <tr>
           <td style="text-align:right; width:70px;">
            
               业务日期： 
            
            
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
         
                 <td style="text-align:right;width:50px;">
           
               类别：</td>
           <td style="text-align:left;width:80px;">
           
           
          
             
             
             <asp:TextBox ID="ddlTypeList" runat="server"></asp:TextBox>
             
             
             </td>
           
           <td style="text-align:right;width:80px;">
           
               收支项目：</td>
           
           <td style="text-align:left;width:80px;">
           
               <asp:TextBox ID="txtItemList" runat="server"></asp:TextBox>
                   </td>
           
           <td style="text-align:right;width:60px;">
           
               经手人：</td>
           
           <td style="text-align:left;width:80px;">
           
               <asp:DropDownList ID="ddlYWYList" runat="server">

               </asp:DropDownList>


                   </td>
           
           <td style="text-align:right; padding-right:20px;">
           
              <input id="btnSearch" type="button" value="查询" class="ui-btn ui-btn-sp mrb" onclick="search(0)" />
           
            <input id="btnToExcel" type="button" value="导出Excel" class="ui-btn ui-btn-sp mrb" onclick="search(1)" />
              

           
           </td>
           </tr>
           
           <tr>
           <td style="text-align:left; " colspan="11">
            
           
            <div id="maingrid"> </div>


               <asp:HiddenField ID="hfPath" Value="" runat="server" />
  
           
           </td>
           </tr>
           
           </table>
    </form>
</body>
</html>

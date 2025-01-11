﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="produceListEdit.aspx.cs" Inherits="Lanwei.Weixin.UI.produce.produceListEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <title>修改生产计划</title>
   
   <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <%--  <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>--%>
    
    
    <script src="../lib.1.3.1/Source/lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    
    <script src="../lib/json2.js" type="text/javascript"></script>


     <script src="js/produceListEdit.js" type="text/javascript"></script>


    
    
</head>
<body style=" padding-top:10px; padding-left:10px;">


    <form id="form1" runat="server">
   
 <table id="form" border="0" cellpadding="0" cellspacing="0" style="width:99%; line-height:40px;">
           <tr>
           <td style="width:80px; text-align:center;">
               订单编号：</td>
           <td style="text-align:left; width:250px;">
           
                 <asp:TextBox ID="txtOrderNumber" runat="server"></asp:TextBox>
             
                 <input type="hidden" id="hfOrderNumber" runat="server" value="" />
          
           
           </td>
           <td style="text-align:right; width:80px;">
                                                  计划类别：</td>
           <td style="text-align:left; width:180px;">
                                                  <asp:DropDownList ID="ddlTypeName" runat="server">
                                                      <asp:ListItem>订单生产</asp:ListItem>
                                                      <asp:ListItem>样品生产</asp:ListItem>
                                                  </asp:DropDownList>
                   </td>
           <td style="text-align:right; width:80px;">
                                                  &nbsp;</td>
           <td style="text-align:left;">
                                                  &nbsp;</td>
           </tr>
           <tr>
           <td style="width:80px; text-align:center;">
               商品名称：</td>
           <td style="text-align:left; width:250px;">
            
            <asp:TextBox ID="txtGoodsName" runat="server"></asp:TextBox>
            <input type="hidden" id="hfGoodsId" runat="server" value="" /> 
            <input type="hidden" id="hfGoodsName" runat="server" value="" />
        
           
           </td>
           <td style="text-align:right; width:80px;">
                      商品规格：</td>
           <td style="text-align:left; width:180px;">
           
               <asp:TextBox ID="txtSpec" runat="server"></asp:TextBox>                                      
           
           </td>
           <td style="text-align:right; width:80px;">
                                                  单位：</td>
           <td style="text-align:left;">
           
               <asp:TextBox ID="txtUnitName" runat="server"></asp:TextBox>    
                                
           </td>
           </tr>
           <tr>
           <td style="width:80px; text-align:center;">
               生产数量：</td>
           <td style="text-align:left; width:250px;">
           
               <asp:TextBox ID="txtNum" runat="server"></asp:TextBox>    
               
           </td>
           <td style="text-align:right; width:80px;">
                                                  开始日期：</td>
           <td style="text-align:left; width:180px;">
            
            <asp:TextBox ID="txtDateStart" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
            
             
                   </td>
           <td style="text-align:right; width:80px;">
                                                  结束日期：</td>
           <td style="text-align:left;">
                                                  <asp:TextBox ID="txtDateEnd" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                   </td>
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
              
              
                   
               <input id="btnSave" class="ui-btn ui-btn-sp mrb" runat="server" type="button" value="保 存" onclick="save()"  />
                      
            
              
                   
               </td>
           </tr>
           </table>
           
           
           
      
           
    </form>
</body>
</html>

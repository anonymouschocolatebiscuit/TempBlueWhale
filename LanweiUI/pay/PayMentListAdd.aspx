﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayMentListAdd.aspx.cs" Inherits="Lanwei.Weixin.UI.pay.PayMentListAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>采购付款新增</title>
   
   <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>


     <script src="js/PayMentListAdd.js" type="text/javascript"></script>


    
    
</head>
<body style=" padding-top:10px; padding-left:10px;">


    <form id="form1" runat="server">
   
 <table id="form" border="0" cellpadding="0" cellspacing="0" style="width:99%; line-height:40px;">
           <tr>
           <td style="width:80px; text-align:right;">
               采购单位：</td>
           <td style="text-align:left; width:250px;">
            
          <asp:DropDownList ID="ddlVenderList" runat="server" Width="250px">
    </asp:DropDownList>
            
          
          
          </td>
           <td style="text-align:right; width:80px;">
                                                  付款日期：</td>
           <td style="text-align:left; width:180px;">
            
                                                  <asp:TextBox ID="txtBizDate" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
            
             
               </td>
           <td style="text-align:right; width:80px;">
                                                  &nbsp;</td>
           <td style="text-align:left;">
                                                  &nbsp;</td>
           </tr>
           <tr>
           <td align="center" colspan="6">
            
             <div id="maingrid"></div>
 
            
            </td>
           </tr>
           <tr>
           <td align="left" colspan="6">
           
           
           
           <table id="Table1" border="0" cellpadding="0" cellspacing="0" style="line-height:40px;">
           <tr>
           <td style="text-align:right; width:80px;">
            
            源单编号：</td>
           <td style="text-align:left; width:140px;">
            
             
            
               <asp:TextBox ID="txtKeys" runat="server" nullText="请输入单据号"></asp:TextBox>
 
            
            </td>
           <td style="text-align:right; width:70px;">
            
               日期：
            
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
           <td style="text-align:left;">
           
           
           &nbsp;<input id="btnSelectBill" type="button" value="查 询" class="ui-btn" 
                   onclick="selectBill()" /></td>
           </tr>
           </table>
           
           
           
           
           </td>
           </tr>
           <tr>
           <td align="center" colspan="6">
               
                 <div id="maingridsub"></div>
               
               </td>
           </tr>
           <tr>
           <td align="right">
               整单折扣：</td>
           <td style="text-align:left; width:250px;">
            
            <asp:TextBox ID="txtDisPrice" runat="server">0</asp:TextBox>
              
                   
                   </td>
           <td style="text-align:right; width:80px;">
                                                  本次预付：</td>
           <td style="text-align:left; width:180px;">
            
           <asp:TextBox ID="txtPayPriceNowMore" runat="server" BackColor="#FFFFCC" 
                   ToolTip="自动计算">0</asp:TextBox>
              
                   
                   </td>
           <td style="text-align:right; width:80px;">
                                                  &nbsp;</td>
           <td style="text-align:left;">
                                                  
           
               &nbsp;</td>
           </tr>
           <tr>
           <td align="right">
               备注信息：</td>
           <td style="text-align:left; " colspan="3">
            
               <asp:TextBox ID="txtRemarks" runat="server" Width="510px" TextMode="MultiLine"></asp:TextBox>
              
                   
                   </td>
           <td style="text-align:left; ">
            
               &nbsp;</td>
           <td style="text-align:left;">
                                                  
              
                   
               <input id="Button1" class="ui-btn ui-btn-sp mrb" type="button" value="新增" onclick="save()"  /></td>
           </tr>
           </table>
 


  
 
           
    </form>
</body>
</html>

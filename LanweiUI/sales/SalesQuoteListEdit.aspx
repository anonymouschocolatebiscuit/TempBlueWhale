<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalesQuoteListEdit.aspx.cs" Inherits="Lanwei.Weixin.UI.sales.SalesQuoteListEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>修改销售报价单</title>
   
   <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>


     <script src="js/SalesQuoteListEdit.js" type="text/javascript"></script>


    
    
</head>
<body style=" padding-top:10px; padding-left:10px;">


    <form id="form1" runat="server">
    
    
    <div id="isCheck" runat="server" style="position:absolute;  width:150px;height:74px;background: url(../images/billChecked.png) 0 0 no-repeat;left:800px;top:30px; z-index:1000; ">

</div>
    
   
 <table id="form" border="0" cellpadding="0" cellspacing="0" style="width:99%; line-height:40px;">
           <tr>
           <td style="width:80px; text-align:center;">
               销货单位：</td>
           <td style="text-align:left; width:250px;">
            
    <asp:DropDownList ID="ddlVenderList" runat="server" Width="250px">
    </asp:DropDownList>
            
             
                   </td>
           <td style="text-align:right; width:80px;">
                                                  报价日期：</td>
           <td style="text-align:left; width:180px;">
                                                  <asp:TextBox ID="txtBizDate" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                   </td>
           <td style="text-align:right; width:80px;">
                                                  付款时间：</td>
           <td style="text-align:left; width:180px;" >
                                                  <asp:DropDownList ID="ddlPayDateList" runat="server">
                                                      <asp:ListItem>预付款</asp:ListItem>
                                                      <asp:ListItem>货到付款</asp:ListItem>
                                                      <asp:ListItem>月结30天</asp:ListItem>
                                                      <asp:ListItem>月结45天</asp:ListItem>
                                                      <asp:ListItem>月结60天</asp:ListItem>
                                                      <asp:ListItem>月结90天</asp:ListItem>
                                                      <asp:ListItem>月结120天</asp:ListItem>
                                                      <asp:ListItem>月结180天</asp:ListItem>
                                                  </asp:DropDownList>
               </td>
           <td style="text-align:right; width:80px;">
                                                  付款方式：</td>
           <td style="text-align:left;">
                                                  <asp:DropDownList ID="ddlPayWayList" runat="server">
                                                      <asp:ListItem>现金</asp:ListItem>
                                                      <asp:ListItem>电汇</asp:ListItem>
                                                      <asp:ListItem>转账</asp:ListItem>
                                                      <asp:ListItem>支票</asp:ListItem>
                                                      <asp:ListItem>承兑</asp:ListItem>
                                                  </asp:DropDownList>
                   </td>
           </tr>
           <tr>
           <td style="width:80px; text-align:center;">
               是否含税：</td>
           <td style="text-align:left; width:250px;">
            
               <asp:DropDownList ID="ddlIsTaxList" runat="server">
                   <asp:ListItem>不含税</asp:ListItem>
                   <asp:ListItem>含税</asp:ListItem>
                
               </asp:DropDownList>
            
             
                   </td>
           <td style="text-align:right; width:80px;">
                                                  运费承担：</td>
           <td style="text-align:left; width:180px;">
                                                  <asp:DropDownList ID="ddlIsFreight" runat="server">
                                                      <asp:ListItem>买方承担</asp:ListItem>
                                                      <asp:ListItem>卖方承担</asp:ListItem>
                                                     
                                                  </asp:DropDownList>
                   </td>
           <td style="text-align:right; width:80px;">
                                                  运输方式：</td>
           <td style="text-align:left; width:180px;" >
                                                  <asp:DropDownList ID="ddlFreightWayList" runat="server">
                                                      <asp:ListItem>物流</asp:ListItem>
                                                      <asp:ListItem>快递</asp:ListItem>
                                                      <asp:ListItem>航空</asp:ListItem>
                                                      <asp:ListItem>海运</asp:ListItem>
                                                      <asp:ListItem>自提</asp:ListItem>
                                                  </asp:DropDownList>
                   </td>
           <td style="text-align:right; width:80px;">
                                                  交货地点：</td>
           <td style="text-align:left;">
                                                  <asp:TextBox ID="txtSendPlace" runat="server"></asp:TextBox>
               </td>
           </tr>
           <tr>
           <td style="width:80px; text-align:center;">
               交货周期：</td>
           <td style="text-align:left; width:250px;">
            
               <asp:TextBox ID="txtSendDate" runat="server"></asp:TextBox>
            
             
                   </td>
           <td style="text-align:right; width:80px;">
                                                  包装方式：</td>
           <td style="text-align:left; width:180px;">
                                                  <asp:DropDownList ID="ddlPackageList" runat="server">
                                                      <asp:ListItem>纸箱</asp:ListItem>
                                                      <asp:ListItem>袋装</asp:ListItem>
                                                      <asp:ListItem>卷装</asp:ListItem>
                                                      <asp:ListItem>盒装</asp:ListItem>
                                                      <asp:ListItem>散装</asp:ListItem>
                                                  </asp:DropDownList>
                   </td>
           <td style="text-align:right; width:80px;">
                                                  有效日期：</td>
           <td style="text-align:left; width:180px;" >
                                                  <asp:TextBox ID="txtDeadLine" runat="server" ltype="date" 
                                                      validate="{required:true}"></asp:TextBox>
                   </td>
           <td style="text-align:right; width:80px;">
                                                  报价人员：</td>
           <td style="text-align:left;">
                                                  <asp:DropDownList ID="ddlBizId" runat="server">
                                                  </asp:DropDownList>
               </td>
           </tr>
           </table>
 
 <div id="maingrid"></div>
  
 
 <table id="tbFooter" border="0" cellpadding="0" cellspacing="0" style="width:99%; line-height:50px;">
           <tr>
           <td style="width:80px; text-align:right;">
               备注信息：</td>
           <td style="text-align:left; ">
            
               <asp:TextBox ID="txtRemarks" runat="server" Width="540px" TextMode="MultiLine" 
                   Height="80px"></asp:TextBox>
              
                   
                   </td>
           <td style="text-align:right; padding-right:30px; ">
              
              
                   
               <input id="btnSave" class="ui-btn ui-btn-sp mrb" type="button" value="保存" onclick="save()" runat="server"  />&nbsp;
                                     
                  
              
              
                   
               </td>
           </tr>
           </table>
           
           
           
           
           
           
           <asp:HiddenField ID="hfNumber" runat="server" />
           
           
           
           
           
           
    </form>
</body>
</html>

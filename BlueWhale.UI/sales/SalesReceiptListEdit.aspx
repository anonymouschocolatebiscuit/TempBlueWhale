<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalesReceiptListEdit.aspx.cs" Inherits="BlueWhale.UI.sales.SalesReceiptListEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>修改销售出库</title>
   
   <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <%--  <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>--%>
    <script src="../lib.1.3.1/Source/lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
    <script src="js/SalesReceiptListEdit.js" type="text/javascript"></script>
</head>
<body style=" padding-top:10px; padding-left:10px;">

    <form id="form1" runat="server">
   
        <table id="form" border="0" cellpadding="0" cellspacing="0" style="width:99%; line-height:40px;">
            <tr>
                <td style="width:80px; text-align:center;">销货单位：</td>
                <td style="text-align:left; width:250px;">  
                    <asp:TextBox ID="clientName" runat="server"></asp:TextBox>
                    <input type="hidden" id="clientId" runat="server" value="" />
                    <input type="hidden" id="txtClientName" runat="server" value="" />
                </td>
                <td style="text-align:right; width:80px;">出库日期：</td>
                <td style="text-align:left; width:180px;">
                    <asp:TextBox ID="txtBizDate" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                </td>
                <td style="text-align:right; width:80px;">销售人：</td>
                <td style="text-align:left; width:180px;" >
                    <asp:DropDownList ID="ddlYWYList" runat="server"></asp:DropDownList>
                </td>
                <td style="text-align:right; width:80px;">&nbsp;</td>
                <td style="text-align:left;">&nbsp;</td>
            </tr>
         </table>
 
 <div id="maingrid"></div>
  
 
            <table id="tbFooter" border="0" cellpadding="0" cellspacing="0" style="width:99%; line-height:50px;">
               <tr>
                   <td style="width:80px; text-align:right;">本次收款：</td>
                   <td style="text-align:left; width:100px;">
                       <asp:TextBox ID="txtPayNow" runat="server">0</asp:TextBox>
                   </td>
                   <td style="text-align:right; width:80px;">本次欠款：</td>
                   <td style="text-align:left; width:100px;">
                       <asp:TextBox ID="txtPayNo" runat="server" BackColor="#FFFFCC" ToolTip="自动计算">0</asp:TextBox>
                   </td>
                   <td style="text-align:right; width:80px;">结算账户：</td>
                   <td style="text-align:left; width:200px;">
                       <asp:DropDownList ID="ddlBankList" runat="server"></asp:DropDownList>
                   </td>
                   <td style="text-align:right; width:80px;">折扣%：</td>
                   <td style="text-align:left; padding-right:30px; ">             
                       <asp:TextBox ID="txtDis" runat="server" Text="0"></asp:TextBox>
                   </td>
               </tr>
               <tr>
                   <td style="width:80px; text-align:right;">物流公司：</td>
                   <td style="text-align:left; width:100px;">
                       <asp:DropDownList ID="ddlSendCompanyList" runat="server"></asp:DropDownList>
                    </td>
                   <td style="text-align:right; width:80px;">运单号：</td>
                   <td style="text-align:left; width:100px;">
                       <asp:TextBox ID="txtSendNumber" runat="server"></asp:TextBox>
                   </td>
                   <td style="text-align:right; width:80px;">运费方式：</td>
                   <td style="text-align:left; ">
                       <asp:DropDownList ID="ddlSendPayTypeList" runat="server">
                           <asp:ListItem>寄付</asp:ListItem>
                           <asp:ListItem>到付</asp:ListItem>
                       </asp:DropDownList>
                   </td>
                   <td style="text-align:right; ">折扣金额：</td>
                   <td style="text-align:left; padding-right:30px; ">
                       <asp:TextBox ID="txtDisPrice" BackColor="#FFFFCC" ToolTip="自动计算" Text="0" runat="server"></asp:TextBox>
                   </td>
               </tr>
               <tr>
                   <td style="width:80px; text-align:right;">收货人：</td>
                   <td style="text-align:left; width:100px;">
                       <asp:TextBox ID="txtGetName" runat="server"></asp:TextBox>
                   </td>
                   <td style="text-align:right; width:80px;">电话：</td>
                   <td style="text-align:left; width:100px;">
                       <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                   </td>
                   <td style="text-align:right; width:80px;">收货地址：</td>
                   <td style="text-align:left; ">
                       <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
                   </td>
                   <td style="text-align:right; ">运费：</td>
                   <td style="text-align:left; padding-right:30px; "> 
                       <asp:TextBox ID="txtSendPrice" runat="server" Text="0"></asp:TextBox>
                   </td>
                   </tr>
               <tr>
                   <td style="width:80px; text-align:right;">备注信息：</td>
                   <td style="text-align:left; " colspan="5">
                       <asp:TextBox ID="txtRemarks" runat="server" Width="540px" TextMode="MultiLine"></asp:TextBox>
                   </td>
                   <td style="text-align:left; ">&nbsp;</td>
                   <td style="text-align:right; padding-right:30px; ">
                       <input id="btnSave" class="ui-btn ui-btn-sp mrb" type="button" value="保 存" onclick="save()"  runat="server" />
                       <asp:HiddenField ID="hf" runat="server" />&nbsp;
                   </td>
               </tr>
           </table>
    </form>
</body>
</html>

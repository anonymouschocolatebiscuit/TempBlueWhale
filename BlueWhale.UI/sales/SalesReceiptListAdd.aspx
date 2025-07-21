<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalesReceiptListAdd.aspx.cs" Inherits="BlueWhale.UI.sales.SalesReceiptListAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Sales Outbound - Create</title>
   
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <%--<script src="../lib.1.3.1/Source/lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>--%>
    <script src="../lib/json2.js" type="text/javascript"></script>
    <script src="js/SalesReceiptListAdd.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function ()
        {
            $("#clientName").val("<%= wlName %>");
            $("#clientId").val("<%= wlId %>");
        });
    </script>
</head>
<body style=" padding-top:10px; padding-left:10px;">

    <form id="form1" runat="server">
        <table id="form" border="0" cellpadding="0" cellspacing="0" style="width:99%; line-height:40px;">
           <tr>
               <td style="width:100px; text-align:center;">Item Code: </td>
               <td style="text-align:left; width:250px;">
                   <asp:TextBox ID="txtKeys" runat="server" placeholder="Please use a scanner or enter and press Enter……"></asp:TextBox>
               </td>
               <td style="text-align:center; width:80px;">Sales Unit: </td>
               <td style="text-align:left; width:250px;">
                   <asp:TextBox ID="clientName" runat="server"></asp:TextBox>
                   <input type="hidden" id="hfClientName" runat="server" value="" />
                   <input type="hidden" id="clientId" runat="server" value="" />
               </td>
               <td style="text-align:right; width:110px;">Outbound Date: </td>
               <td style="text-align:left; width:180px;" >
                   <asp:TextBox ID="txtBizDate" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
               </td>
               <td style="text-align:right; width:90px;">Salesperson: </td>
               <td style="text-align:left; width:180px;" >
                    <asp:DropDownList ID="ddlYWYList" runat="server"></asp:DropDownList>
               </td>
               <td style="text-align:left;">&nbsp;</td>
           </tr>
         </table>
 
 <div id="maingrid"></div>
  
 
        <table id="tbFooter" border="0" cellpadding="0" cellspacing="0" style="width:99%; line-height:50px;">
           <tr>
               <td style="width:150px; text-align:right;">Payment Received: </td>
               <td style="text-align:left; width:100px;">
                   <asp:TextBox ID="txtPayNow" runat="server">0</asp:TextBox>
               </td>
               <td style="text-align:right; width:150px;">Debt: </td>
               <td style="text-align:left; width:100px;">
                   <asp:TextBox ID="txtPayNo" runat="server" BackColor="#FFFFCC" ToolTip="Automatic calculation">0</asp:TextBox>
               </td>
               <td style="text-align:right; width:150px;">Settlement Account: </td>
               <td style="text-align:left; width:200px;">
                   <asp:DropDownList ID="ddlBankList" runat="server"></asp:DropDownList>
               </td>
               <td style="text-align:right; width:120px;">Discount%: </td>
               <td style="text-align:left; padding-right:30px; ">  
                   <asp:TextBox ID="txtDis" runat="server" Text="0"></asp:TextBox>   
               </td>
           </tr>
           <tr>
               <td style="width:150px; text-align:right;">Logistics Company: </td>
               <td style="text-align:left; width:100px;">
                   <asp:DropDownList ID="ddlSendCompanyList" runat="server">
                   </asp:DropDownList>
               </td>
               <td style="text-align:right; width:150px;">Shipping Bill Number: </td>
               <td style="text-align:left; width:100px;">
                   <asp:TextBox ID="txtSendNumber" runat="server"></asp:TextBox>
               </td>
               <td style="text-align:right; width:150px;">Shipping Method: </td>
               <td style="text-align:left; ">
                   <asp:DropDownList ID="ddlSendPayTypeList" runat="server">
                       <asp:ListItem>(Please Select)</asp:ListItem>
                       <asp:ListItem>Pay before ship</asp:ListItem>
                       <asp:ListItem>Pay when receive</asp:ListItem>
                   </asp:DropDownList>
               </td>
               <td style="text-align:right; width:120px">Discount Amount: </td>
               <td style="text-align:left; padding-right:30px; ">
                   <asp:TextBox ID="txtDisPrice" runat="server" BackColor="#FFFFCC" ToolTip="Auto calculate" Text="0"></asp:TextBox>
               </td>
           </tr>
           <tr>
               <td style="width:150px; text-align:right;">Recipient: </td>
               <td style="text-align:left; width:100px;">
                   <asp:TextBox ID="txtGetName" runat="server"></asp:TextBox>
                </td>
               <td style="text-align:right; width:150px;">Phone:</td>
               <td style="text-align:left; width:100px;">
                   <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
               </td>
              <td style="text-align:right; width:150px;">Delivery Address: </td>
               <td style="text-align:left; ">
                   <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
                </td>
               <td style="text-align:right; width:120px;">Shipping Fee: </td>
               <td style="text-align:left; padding-right:30px; ">
                   <asp:TextBox ID="txtSendPrice" runat="server" Text="0"></asp:TextBox>
               </td>
           </tr>
           <tr>
               <td style="width:150px; text-align:right;">Remarks: </td>
               <td style="text-align:left; " colspan="5">
                   <asp:TextBox ID="txtRemarks" runat="server" Width="540px" TextMode="MultiLine"></asp:TextBox>
               </td>
               <td style="text-align:left; ">&nbsp;</td>
               <td style="text-align:right; padding-right:30px; "> 
                   <asp:HiddenField ID="hf" runat="server" />
                   <input id="Button1" class="ui-btn ui-btn-sp mrb" type="button" value="Add" onclick="save()"  />
               </td>
           </tr>
        </table>
    </form>
</body>
</html>
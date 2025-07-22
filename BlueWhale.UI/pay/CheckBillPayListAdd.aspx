<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckBillPayListAdd.aspx.cs" Inherits="BlueWhale.UI.pay.CheckBillPayListAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Payment Verification</title>
   
   <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>

    <script src="js/CheckBillPayListAdd.js" type="text/javascript"></script>
</head>
<body style=" padding-top:10px; padding-left:10px;">

    <form id="form1" runat="server">
   
 <table id="form" border="0" cellpadding="0" cellspacing="0" style="width:99%; line-height:40px;">
           <tr>
           <td style="width:130px; text-align:right; padding-right: 0.5rem;">
               Purchasing Unit:</td>
           <td style="text-align:left; width:250px;">
            
          <asp:DropDownList ID="ddlVenderList" runat="server" Width="250px">
          </asp:DropDownList>
          
          </td>
           <td style="text-align:right; width:130px; padding-right: 0.5rem;">
                                                  Write-off Date:</td>
           <td style="text-align:left; width:180px;">
            
                                                  <asp:TextBox ID="txtBizDate" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
            
               </td>
           <td style="text-align:right; width:80px;">
                                                  &nbsp;</td>
           <td style="text-align:left;">
                                                  &nbsp;</td>
           </tr>
           <tr>
           <td style="text-align:left;" colspan="6">
            
            <table id="Table2" border="0" cellpadding="0" cellspacing="0" style="line-height:40px;">
           <tr>
           <td style="text-align:right; width:120px; padding-right: 0.5rem;">
            
               Payment Number:</td>
           <td style="text-align:left; width:140px;">
            
               <asp:TextBox ID="txtKeysGet" runat="server" placeholder="Please enter the document number"></asp:TextBox>
            </td>
           <td style="text-align:right; width:80px; padding-right: 0.5rem;">
            
               Date:
            
              </td>
           <td style="text-align:left; width:120px;">
            
               <asp:TextBox ID="txtDateStar1" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
           </td>
           <td style="text-align:center; width:30px;">            
            
               To

           </td>
            <td style="text-align:left; width:120px; padding-right: 0.5rem;">
            
                <asp:TextBox ID="txtDateEnd1" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
            </td>
           <td style="text-align:left;">
           <input id="btnSelectPay" type="button" value="Search" class="ui-btn" 
                   onclick="selectGet()" /></td>
           </tr>
           </table>
            </td>
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
           <td style="text-align:right; width:130px; padding-right: 0.5rem;">
            
               Payable Number:</td>
           <td style="text-align:left; width:140px;">
            
               <asp:TextBox ID="txtKeys" runat="server" placeholder="Please enter the document number"></asp:TextBox>
            </td>
           <td style="text-align:right; width:90px; padding-right: 0.5rem;">
            
               Date:
            
              </td>
           <td style="text-align:left; width:120px;">
            
               <asp:TextBox ID="txtDateStart" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
           </td>
           <td style="text-align:center; width:30px;">            
            
               To

           </td>
            <td style="text-align:left; width:120px; padding-right: 0.5rem;">
            
                <asp:TextBox ID="txtDateEnd" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
            </td>
           <td style="text-align:left;">
           
           <input id="btnSelectBill" type="button" value="Search" class="ui-btn" 
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
               Remarks:</td>
           <td style="text-align:left; " colspan="3">
            
               <asp:TextBox ID="txtRemarks" runat="server" Width="510px" TextMode="MultiLine"></asp:TextBox>
               </td>
           <td style="text-align:left; ">
            
               &nbsp;</td>
           <td style="text-align:left;">
                                                  
               <input id="Button1" class="ui-btn ui-btn-sp mrb" type="button" value="Create" onclick="save()"  /></td>
           </tr>
           </table>
           
    </form>
</body>
</html>

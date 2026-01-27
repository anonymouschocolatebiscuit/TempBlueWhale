<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="produceListAdd.aspx.cs" Inherits="BlueWhale.UI.produce.produceListAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Create Produce Plan</title>
   
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <%--  <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>--%>
    <script src="../lib.1.3.1/Source/lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
    <script src="js/produceListAdd.js" type="text/javascript"></script>

</head>
<body style=" padding-top:10px; padding-left:10px;">
    <form id="form1" runat="server">
        <table id="form" border="0" cellpadding="0" cellspacing="0" style="width:99%; line-height:40px;">
            <tr>
                <td style="width:135px; text-align:right;">
                    Order no：
                </td>
                <td style="text-align:left; width:250px;">
                    <asp:TextBox ID="txtOrderNumber" runat="server"></asp:TextBox>
                    <input type="hidden" id="hfOrderNumber" runat="server" value="" />
                </td>
                <td style="text-align:right; width:150px;">
                    Program Type：
                </td>
                <td style="text-align:left; width:180px;">
                    <asp:DropDownList ID="ddlTypeName" runat="server">
                        <asp:ListItem>Order production</asp:ListItem>
                        <asp:ListItem>Sample production</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align:right; width:80px;">&nbsp;</td>
                <td style="text-align:left;">&nbsp;</td>
            </tr>
            <tr>
                <td style="width:135px; text-align:right;">Product name：</td>
                <td style="text-align:left; width:250px;">
                    <asp:TextBox ID="txtGoodsName" runat="server"></asp:TextBox>
                    <input type="hidden" id="hfGoodsId" runat="server" value="" /> 
                </td>
                <td style="text-align:right; width:150px;">
                          Product Specifications：</td>
                <td style="text-align:left; width:180px;">
                    <asp:TextBox ID="txtSpec" runat="server"></asp:TextBox>                                      
                </td>
                <td style="text-align:right; width:80px;">Unit：</td>
                <td style="text-align:left;">
                    <asp:TextBox ID="txtUnitName" runat="server"></asp:TextBox>    
                </td>
            </tr>
            <tr>
                <td style="width:135px; text-align:right;">Production quantity：</td>
                <td style="text-align:left; width:250px;">
                    <asp:TextBox ID="txtNum" runat="server"></asp:TextBox>    
                </td>
                <td style="text-align:right; width:150px;">Start Date：</td>
                <td style="text-align:left; width:180px;">
                    <asp:TextBox ID="txtDateStart" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                </td>
                <td style="text-align:right; width:80px;">End Date：</td>
                <td style="text-align:left;">
                    <asp:TextBox ID="txtDateEnd" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                </td>
            </tr>
        </table>

        <div id="maingrid"></div>
        <table id="tbFooter" border="0" cellpadding="0" cellspacing="0" style="width:99%; margin-top: 10px">
            <tr>
                <td style="width:80px; text-align:right;">Remark：</td>
                <td style="text-align:left">
                    <asp:TextBox ID="txtRemarks" runat="server" Width="540px" TextMode="MultiLine"></asp:TextBox>
                </td>
                <td style="text-align:right; padding-right:30px; ">
                    <input id="Button1" class="ui-btn ui-btn-sp mrb" type="button" value="Add" onclick="save()"  />
                </td>
            </tr>
        </table>    
    </form>
</body>
</html>

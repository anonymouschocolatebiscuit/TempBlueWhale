<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsListAdd.aspx.cs" Inherits="BlueWhale.UI.BaseSet.GoodsListAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Details</title>

    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />

    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>

    <script type="text/javascript"> 
     
        var dialog = frameElement.dialog;
                
         $(function() {

         var form = $("#form").ligerForm();

          var g =  $.ligerui.get("txtNames");
            g.set("Width", 550);
         });

        function closeDialog()
        {
            var dialog = frameElement.dialog;
            dialog.close(); 
        }
        
    </script>

    <style type="text/css">
         body{ font-size:12px;}
        .l-table-edit {}
        .l-table-edit-td{ padding:4px;}
        .l-button-submit,.l-button-test{width:80px; float:left; margin-left:10px; padding-bottom:2px;}
        .l-verify-tip{ left:230px; top:120px;}
        .l-radio-wrapper { width: 80px; padding-top:20px}
    </style>

</head>
<body style="font-size: 10pt;">
    <form id="form1" runat="server">

        <table id="form" border="0" cellpadding="0" cellspacing="20" style="line-height: 45px;">
            <tr>
                <td style="width: 150px; text-align: left;">Product Code:</td>
                <td style="width: 130px;">
                    <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
                </td>
                <td style="width: 150px; text-align: left;">Barcode:</td>
                <td style="width: 130px;">
                    <asp:TextBox ID="txtBarcode" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">Product Name:</td>
                <td colspan="3">
                    <asp:TextBox ID="txtNames" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align:left;">Product Type:</td>
                <td>
                    <asp:DropDownList ID="ddlVenderTypeList" runat="server">
                    </asp:DropDownList>
                </td>
                <td align="left">Brand:</td>
                <td>
                    <asp:DropDownList ID="ddlBrandList" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">Specification:</td>
                <td>
                    <asp:TextBox ID="txtSpec" runat="server"></asp:TextBox>
                </td>
                <td align="left">Measurement Unit:</td>
                <td>
                    <asp:DropDownList ID="ddlUnitList" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">Default Warehouse:</td>
                <td>
                    <asp:DropDownList ID="ddlCangkuList" runat="server">
                    </asp:DropDownList>
                </td>
                <td align="left">Place of Origin:</td>
                <td>
                    <asp:TextBox ID="txtPlace" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    <asp:Label ID="lbFieldA" runat="server" Text="Label"></asp:Label>
                    ：</td>
                <td>
                    <asp:TextBox ID="txtFieldA" runat="server"></asp:TextBox>
                </td>
                <td align="left">
                    <asp:Label ID="lbFieldB" runat="server" Text="Label"></asp:Label>
                    ：</td>
                <td>
                    <asp:TextBox ID="txtFieldB" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">
                    <asp:Label ID="lbFieldC" runat="server" Text="Label"></asp:Label>
                    ：</td>
                <td>
                    <asp:TextBox ID="txtFieldC" runat="server"></asp:TextBox>
                </td>
                <td align="left">
                    <asp:Label ID="lbFieldD" runat="server" Text="Label"></asp:Label>
                    ：</td>
                <td>
                    <asp:TextBox ID="txtFieldD" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">Purchase Price:</td>
                <td>
                    <asp:TextBox ID="txtPriceCost" runat="server"></asp:TextBox>
                </td>
                <td align="left">Commission Rate:</td>
                <td>
                    <asp:TextBox ID="txtTichengRate" runat="server"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="text-align: left;">Wholesale Price:</td>
                <td>
                    <asp:TextBox ID="txtPriceSalesWhole" runat="server"></asp:TextBox>
                </td>
                <td align="left">Retail Price:</td>
                <td>
                    <asp:TextBox ID="txtPriceSalesRetail" runat="server"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="text-align: left;">Minimum Stock:</td>
                <td>
                    <asp:TextBox ID="txtNumMin" runat="server"></asp:TextBox>
                </td>
                <td align="left">Maximum Stock:</td>
                <td>
                    <asp:TextBox ID="txtNumMax" runat="server"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="text-align: left;">Type:</td>
                <td colspan="3">
                <asp:RadioButtonList ID="rbNoteList" runat="server" 
                    RepeatDirection="Horizontal" >
                    <asp:ListItem Text="Default" Value="Default" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Recommend" Value="Recommend"></asp:ListItem>
                    <asp:ListItem Text="Special Offer" Value="Special Offer"></asp:ListItem>
                    <asp:ListItem Text="New Arrival" Value="New Arrival"></asp:ListItem>
                    <asp:ListItem Text="Best Seller" Value="Best Seller"></asp:ListItem>
                    <asp:ListItem Text="Free Gift" Value="Free Gift"></asp:ListItem>
                    <asp:ListItem Text="Out of Stock" Value="Out of Stock"></asp:ListItem>
                </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td align="left">List Now:</td>
                <td colspan="3">
                    <asp:CheckBox ID="cbShow" runat="server" Text="Show at platform after checked" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left;">&nbsp;</td>
                <td style="display:flex">
                    <asp:Button ID="btnSave" runat="server" class="ui_state_highlight"
                        Text="Save" OnClick="btnSave_Click" />
                    &nbsp;
                   <input id="btnCancel" class="ui-btn" type="button" value="Close" onclick="closeDialog()" />
                </td>
                <td>
                    <asp:HiddenField ID="hfImagePath" runat="server" />
                </td>
                <td>
                    <asp:HiddenField ID="hf" runat="server" />
                </td>
            </tr>
        </table>

    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsListAddNew.aspx.cs" Inherits="BlueWhale.UI.BaseSet.GoodsListAddNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Good Details</title>

    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />

    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>



    <script type="text/javascript"> 

        var dialog = frameElement.dialog;


        $(function () {

            var form = $("#form").ligerForm();

            var g = $.ligerui.get("txtNames");
            g.set("Width", 780);


        });




        function closeDialog() {
            var dialog = frameElement.dialog;
            dialog.close();
        }

    </script>


    <style type="text/css">
        body {
            font-size: 12px;
        }

        .l-table-edit {
        }

        .l-table-edit-td {
            padding: 4px;
        }

        .l-button-submit, .l-button-test {
            width: 80px;
            float: left;
            margin-left: 10px;
            padding-bottom: 2px;
        }

        .l-verify-tip {
            left: 230px;
            top: 120px;
        }
    </style>






</head>
<body style="font-size: 10pt;">
    <form id="form1" runat="server">
        <table id="form" border="1" cellpadding="0" cellspacing="20" style="line-height: 45px;">
            <tr>
                <td style="width: 200px; text-align: right;">Good Code:</td>
                <td style="width: 450px;">
                    <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
                </td>
                <td style="width: 150px; text-align: right;">Barcode:</td>
                <td style="width: 250px;">
                    <asp:TextBox ID="txtBarcode" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">Good Name:</td>
                <td colspan="3">
                    <asp:TextBox ID="txtNames" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">Good Type:</td>
                <td>
                    <asp:DropDownList ID="ddlVenderTypeList" runat="server">
                    </asp:DropDownList>
                </td>
                <td align="right">Brand:</td>
                <td>
                    <asp:DropDownList ID="ddlBrandList" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">Specification:</td>
                <td>
                    <asp:TextBox ID="txtSpec" runat="server"></asp:TextBox>
                </td>
                <td align="right">Measurement Unit:</td>
                <td>
                    <asp:DropDownList ID="ddlUnitList" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">Default Warehouse:</td>
                <td>
                    <asp:DropDownList ID="ddlCangkuList" runat="server">
                    </asp:DropDownList>
                </td>
                <td align="right">Place of Origin:</td>
                <td>
                    <asp:TextBox ID="txtPlace" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <asp:Label ID="lbFieldA" runat="server" Text="Label"></asp:Label>
                    : </td>
                <td>
                    <asp:TextBox ID="txtFieldA" runat="server"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lbFieldB" runat="server" Text="Label"></asp:Label>
                    : </td>
                <td>
                    <asp:TextBox ID="txtFieldB" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <asp:Label ID="lbFieldC" runat="server" Text="Label"></asp:Label>
                    : </td>
                <td>
                    <asp:TextBox ID="txtFieldC" runat="server"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lbFieldD" runat="server" Text="Label"></asp:Label>
                    : </td>
                <td>
                    <asp:TextBox ID="txtFieldD" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">Purchase Price:</td>
                <td>
                    <asp:TextBox ID="txtPriceCost" runat="server"></asp:TextBox>
                </td>
                <td align="right">Commision Rate:</td>
                <td>
                    <asp:TextBox ID="txtTichengRate" runat="server"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="text-align: right;">Wholesale Price:</td>
                <td>
                    <asp:TextBox ID="txtPriceSalesWhole" runat="server"></asp:TextBox>
                </td>
                <td align="right">Retail Price:</td>
                <td>
                    <asp:TextBox ID="txtPriceSalesRetail" runat="server"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="text-align: right;">Minimum Stock:</td>
                <td>
                    <asp:TextBox ID="txtNumMin" runat="server"></asp:TextBox>
                </td>
                <td align="right">Maximum Stock:</td>
                <td>
                    <asp:TextBox ID="txtNumMax" runat="server"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="text-align: right;">Type:</td>
                <td>
                    <asp:RadioButton ID="rb1" runat="server" GroupName="aa" Text="Default" />
                    <asp:RadioButton ID="rb2" runat="server" GroupName="aa" Text="Recommend" />
                    <asp:RadioButton ID="rb3" runat="server" GroupName="aa" Text="Special Offer" />
                    <asp:RadioButton ID="rb4" runat="server" GroupName="aa" Text="New Arrival" />
                    <asp:RadioButton ID="rb5" runat="server" GroupName="aa" Text="Best Seller" />
                    <asp:RadioButton ID="rb6" runat="server" GroupName="aa" Text="Free Gift" />
                    <asp:RadioButton ID="rb7" runat="server" GroupName="aa" Text="Out of Stock" />
                </td>
                <td align="right">List Now:</td>
                <td>
                    <asp:CheckBox ID="cbShow" runat="server" Text="Show at platform after checked" />
                </td>
            </tr>

            <tr>
                <td style="text-align: right;">&nbsp;</td>
                <td>
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




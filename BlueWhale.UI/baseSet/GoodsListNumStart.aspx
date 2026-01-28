<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsListNumStart.aspx.cs" Inherits="BlueWhale.UI.BaseSet.GoodsListNumStart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Product Opening Stock</title>

    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../lib/jquery-validation/jquery.validate.min.js" type="text/javascript"></script>
    <script src="../lib/jquery-validation/jquery.metadata.js" type="text/javascript"></script>
    <script src="../lib/jquery-validation/messages_cn.js" type="text/javascript"></script>

    <script src="../lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>

    <script type="text/javascript">
        var dialog = frameElement.dialog;

        $(function () {
            var form = $("#form").ligerForm();
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

        .l-table-edit-td {
            padding: 4px;
        }

        .l-button-submit,
        .l-button-test {
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
        <table id="form" border="0" cellpadding="0" cellspacing="20" style="width: 600px; line-height: 45px;">
            <tr>
                <td style="width: 100px; text-align: right;">Product Code：</td>
                <td style="width: 180px;">
                    <asp:Label ID="lbCode" runat="server" Text="Label"></asp:Label>
                </td>
                <td style="width: 100px; text-align: right;">Product Name：</td>
                <td style="width: 180px;">
                    <asp:Label ID="lbNames" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">Warehouse：</td>
                <td>
                    <asp:DropDownList ID="ddlInventoryList" runat="server">
                    </asp:DropDownList>
                </td>
                <td align="right">Initial Quantity：</td>
                <td>
                    <asp:TextBox ID="txtNum" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">Unit Cost：</td>
                <td>
                    <asp:TextBox ID="txtPriceCost" runat="server"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Button ID="btnSave" runat="server" class="ui_state_highlight" Text="Save"
                        OnClick="btnSave_Click" />
                </td>
                <td align="center">
                    <input id="btnCancel" class="ui-btn" type="button" value="Close" onclick="closeDialog()" />
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:GridView ID="gvLevel" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
                        OnRowDataBound="gvLevel_RowDataBound" OnRowDeleting="gvLevel_RowDeleting" Width="100%"
                        PageSize="15" ShowFooter="True">
                        <Columns>
                            <asp:BoundField HeaderText="Serial Number">
                                <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ckName" HeaderText="Warehouse">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="num" HeaderText="Initial Quantity">
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="priceCost" HeaderText="Unit Cost">
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="sumPrice" HeaderText="Initial Total Value">
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" DeleteText="Delete">
                                <ItemStyle Width="80px" />
                            </asp:CommandField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div>
                                No Information………………
                            </div>
                        </EmptyDataTemplate>
                        <PagerSettings FirstPageImageUrl="~/images/main_54.gif" FirstPageText="First Page"
                            LastPageImageUrl="~/images/main_60.gif" LastPageText="Last Page"
                            Mode="NextPreviousFirstLast" NextPageImageUrl="~/images/main_58.gif" NextPageText="Next"
                            PreviousPageImageUrl="~/images/main_56.gif" PreviousPageText="Previous" />
                        <FooterStyle CssClass="datagrid_footerstyle_normal" Height="20px" />
                        <RowStyle Height="30px" HorizontalAlign="Center" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </form>
</body>

</html>

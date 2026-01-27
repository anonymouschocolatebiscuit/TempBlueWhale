<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsListImages.aspx.cs" Inherits="BlueWhale.UI.baseSet.GoodsListImages" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>No Title</title>
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
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
        body, 
        body * {
            font-family: 'Nunito Sans', sans-serif !important;
        }

        input,
        button,
        select,
        textarea {
            font-family: 'Nunito Sans', sans-serif !important;
        }

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

        .hidden-uploader {
            display: none;
        }

        .upload-panel {
            height: 50px;
            color: gray;
            text-align: center;
            line-height: 50px;
            border-radius: 5px;
            cursor: pointer;
            border: 1px black dashed;
        }

        .my-1 {
            margin-top: 1rem;
            margin-bottom: 1rem;
        }

        .ui_state_highlight{
            width: fit-content;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" width="800px">
            <tr>
                <td align="left" style="line-height: 40px;">
                    <asp:FileUpload ID="fload" runat="server" CssClass="hidden-uploader" />
                    <div id="customUploader" class="upload-panel" runat="server">
                        Upload
                    </div>
                    &nbsp; Default Display：<asp:DropDownList ID="ddlDefault" runat="server">
                        <asp:ListItem Selected="True" Value="1">Yes</asp:ListItem>
                        <asp:ListItem Value="0">No</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Upload"  CssClass="ui_state_highlight"/>
                    &nbsp; &nbsp;   
                    <input id="btnCancel" class="ui-btn" type="button" value="Close" onclick="closeDialog()" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataList ID="DataList1" runat="server" RepeatColumns="4"
                        RepeatDirection="Horizontal" OnItemCommand="DataList1_ItemCommand"
                        OnItemDataBound="DataList1_ItemDataBound">
                        <ItemTemplate>
                            <table border="0" width="80px" style="text-align: center;">
                                <tr>
                                    <td>
                                        <asp:Image ID="Image2" runat="server" Height="200px" Width="200px"
                                            ImageUrl='<%#  "../goodsPicSmall/" + DataBinder.Eval(Container.DataItem,"imagesPath") %>' />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbDefault" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"moren") %>'></asp:Label>
                                        <asp:Button ID="btnDefault" runat="server" Text="Set To Default" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"id") %>'
                                            CommandName="moren"/>
                                        &nbsp;          
                <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"id") %>'
                    CommandName="del" />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

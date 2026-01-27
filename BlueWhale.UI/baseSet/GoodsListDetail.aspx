<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="GoodsListDetail.aspx.cs" Inherits="BlueWhale.UI.baseSet.GoodsListDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<script type="text/javascript">

        function closeDialog() {
            var dialog = frameElement.dialog;
            dialog.close();
        }
    </script>
        
<head runat="server">
        <style type="text/css">
            body, 
            body * {
                font-family: 'Nunito Sans', sans-serif !important;
            }

            input,
            button,
            select,
            textarea  {
                font-family: 'Nunito Sans', sans-serif !important;
            }
    </style>
    <title>Product Title</title>

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<body>
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px; line-height: 40px;">
            <tr>
                <td align="center" valign="middle" style="height: 50px;">
                    <asp:TextBox ID="txtNeirong" runat="server" Width="590px" Rows="10"
                        TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" valign="middle" style="height: 50px;">
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save"   CssClass="ui_state_highlight"/>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

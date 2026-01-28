<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemSet.aspx.cs" Inherits="BlueWhale.UI.baseSet.SystemSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>System Settings</title>
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />

    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
    <script type="text/javascript">

    

        $(function ()
        {
            var form = $("#form").ligerForm();
          
            var TextBox1 = $.ligerui.get("TextBox1");
            TextBox1.set("Width", 350);
            
            var TextBox2 = $.ligerui.get("TextBox2");
            TextBox2.set("Width", 350);
            
            var TextBox3 = $.ligerui.get("TextBox3");
            TextBox3.set("Width", 350);
            
            var TextBox4 = $.ligerui.get("TextBox4");
            TextBox4.set("Width", 350);
            
            var TextBox5 = $.ligerui.get("TextBox5");
            TextBox5.set("Width", 350);

            var txtUserSecret = $.ligerui.get("txtUserSecret");
            txtUserSecret.set("Width", 350);

            //-------------------------------------------------
            var txtSecretBuy = $.ligerui.get("txtSecretBuy");
            txtSecretBuy.set("Width", 350);

            var txtSecretSales = $.ligerui.get("txtSecretSales");
            txtSecretSales.set("Width", 350);

            var txtSecretStore = $.ligerui.get("txtSecretStore");
            txtSecretStore.set("Width", 350);

            var txtSecretFee = $.ligerui.get("txtSecretFee");
            txtSecretFee.set("Width", 350);

            var txtSecretReport = $.ligerui.get("txtSecretReport");
            txtSecretReport.set("Width", 350);
        });
    </script>
</head>
<body style="padding-top:10px;">
    <form id="form1" runat="server">
        <table id="form" style=" line-height:35px;" >
            <tr>
                <td style="width:200px;text-align:right;">
                    Company Name：
                </td>
                <td style="width:550px;">
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:200px;text-align:right;">
                    Company Address：
                </td>
                <td style="width:550px;">
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:200px;text-align:right;">
                    Contact No.：
                </td>
                <td style="width:550px;">
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:200px;text-align:right;">
                    Fax No.：
                </td>
                <td style="width:550px;">
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:200px;text-align:right;">
                    Postcode：
                </td>
                <td style="width:550px;">
                    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:200px;text-align:right;">
                    Activate Review：
                </td>
                <td style="width:550px;">
                    <asp:CheckBox ID="cbBillCheckList" runat="server" 
                        Text="Yes (If checked, changes on report data will only be made after review/approved）" />
                </td>
            </tr>
            <tr>
                <td style="width:200px;text-align:right;">
                    Check Negative Warehouse：
                </td>
                <td style="width:550px;">
                    <asp:CheckBox ID="cbStoreNum" runat="server" Text="Yes（If checked, stock cannot be moved if negative Warehouse is detected）" />
                </td>
            </tr>
            <tr>
                <td style="width:200px;text-align:right;">
                    Print Logo：
                </td>
                <td style="width:550px;">
                    <asp:CheckBox ID="cbPrintLogo" runat="server" 
                        Text="Yes（If checked, company logo will be shown on receipt）" />
                </td>
            </tr>
            <tr>
                <td style="width:200px;text-align:right;">
                    Print Digital Stamp：</td>
                <td style="width:550px;">
                    <asp:CheckBox ID="cbPrintStamp" runat="server" 
                        Text="Yes（If checked, digital stamp will be shown on receipt）" />
                </td>
            </tr>
            <tr>
                <td style="width:200px;text-align:right;">
                    Product Field
                </td>
                <td style="width:550px;"></td>
            </tr>
            <tr>
                <td style="width:200px;text-align:right;">
                    Customized Field 1：
                </td>
                <td style="width:550px;">
                    <asp:TextBox ID="txtFieldA" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:200px;text-align:right;">
                    Customized Field 2：
                </td>
                <td style="width:550px;">
                    <asp:TextBox ID="txtFieldB" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:200px;text-align:right;">
                    Customized Field 3：
                </td>
                <td style="width:550px;">
                    <asp:TextBox ID="txtFieldC" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:200px;text-align:right;">
                    Customized Field 4：
                </td>
                <td style="width:550px;">
                    <asp:TextBox ID="txtFieldD" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align:right;" colspan="2">
                    <hr />
                </td>
            </tr>
            <tr>
                <td style="width:200px;text-align:right;">
                    Attendance Secret：
                </td>
                <td style="width:550px;">
                    <asp:TextBox ID="txtSecretAttendance" runat="server"></asp:TextBox>  
                </td>
            </tr>
            <tr>
                <td style="width:200px;text-align:right;">
                    Review Secret：
                </td>
                <td style="width:550px;">
                    <asp:TextBox ID="txtSecretReview" runat="server"></asp:TextBox>  
                </td>
            </tr>
            <tr>
                <td style="width:200px;text-align:right;">
                    Purchase Secret：
                </td>
                <td style="width:550px;">
                    <asp:TextBox ID="txtSecretBuy" runat="server"></asp:TextBox>  
                </td>
            </tr>
            <tr>
                <td style="width:200px;text-align:right;">
                    Sales Secret：
                </td>
                <td style="width:550px;">
                    <asp:TextBox ID="txtSecretSales" runat="server"></asp:TextBox>  
                </td>
            </tr>
            <tr>
                <td style="width:200px;text-align:right;">
                    Warehouse Secret：
                </td>
                <td style="width:550px;">
                    <asp:TextBox ID="txtSecretStore" runat="server"></asp:TextBox>  
                </td>
            </tr>
            <tr>
                <td style="width:200px;text-align:right;">
                    Fee Secret：
                </td>
                <td style="width:550px;">
                    <asp:TextBox ID="txtSecretFee" runat="server"></asp:TextBox>  
                </td>
            </tr>
            <tr>
                <td style="width:200px;text-align:right;">
                    Report Secret：
                </td>
                <td style="width:550px;">
                    <asp:TextBox ID="txtSecretReport" runat="server"></asp:TextBox>  
                </td>
            </tr>
            <tr>
                <td style="width:150px;text-align:right;"></td>
                <td style="width:550px;">
                    <asp:Button ID="btnSave" runat="server" CssClass="ui-btn ui-btn-sp mrb"
                        onclick="btnSave_Click" Text="Save" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
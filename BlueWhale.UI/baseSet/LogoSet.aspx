<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogoSet.aspx.cs" Inherits="BlueWhale.UI.baseSet.LogoSet" Async="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Logo & Stamp</title>
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(document).ready(function () {
            $('#customUploader').bind('click', function () {
                $('#<%= ProductImg.ClientID %>').click();
             });
         });
    </script>
    <style type="text/css">
        form {
            display: flex;
            justify-content: start;
            align-items: flex-start;
            padding-top: 2rem;
            padding-left: 2rem;
        }

        .card {
            background: #ffffff;
            border-radius: 12px;
            box-shadow: 0 4px 10px rgba(0,0,0,0.08);
            width: 500px;
            padding: 40px;
        }

        .card table {
            width: 100%;
            border-collapse: collapse;
        }

        .card td {
            padding: 12px 8px;
            vertical-align: middle;
        }

        .card td[colspan="2"] {
            text-align: center;
        }

        .card td:first-child {
            color: #555;
            font-weight: 600;
            text-align: right;
            width: 150px;
        }

        .page-title {
            font-size: 20px;
            font-weight: 700;
            color: #333;
            margin-bottom: 20px;
        }

        .radio-group {
            display: flex;
            align-items: center;
        }

        .radio-group input[type="radio"] {
            vertical-align: middle;
            margin-right: 6px;
        }

        .radio-btn-label {
            margin-right: 14px;
        }

        .aspnet-upload {
            display: inline-block;
            padding: 6px;
        }

        .card img {
            border: 1px solid #ddd;
            border-radius: 8px;
            padding: 4px;
            background: #fff;
        }

        .footer-note {
            font-size: 12px;
            color: #888;
            margin-top: 8px;
            text-align: center;
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="card">
            <div class="page-title">Edit LogoSet Picture</div>
            <table border="0" cellpadding="2" cellspacing="1">
                <tr>
                    <td>Change Image:</td>
                    <td>
                        <div class="radio-group">
                            <asp:RadioButton ID="RB1" runat="server" Checked="True" GroupName="id" Text="Logo" CssClass="radio-btn-label" />
                            &nbsp;
                            <asp:RadioButton ID="RB2" runat="server" GroupName="id" Text="Stamp" CssClass="radio-btn-label" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>Image:</td>
                    <td>
                        <span class="aspnet-upload">
                            <asp:FileUpload ID="ProductImg" runat="server" Width="200px" CssClass="hidden-uploader" />
                        </span>
                            <div id="customUploader" class="upload-panel my-1" runat="server">
                                Select Image
                            </div>
                        <span class="aspnet-btn">
                            <asp:Button ID="ButtonUpload" runat="server" onclick="ButtonUpload_Click" Text="Upload" CssClass="ui-btn ui-btn-sp mrb" />
                        </span>
                    </td>
                </tr>
                <tr>
                    <td>Logo (60×60)</td>
                    <td>
                        <asp:Image ID="Image1" runat="server" Height="60px" Width="60px"
                            ImageUrl="../sales/pdf/logo100.jpg" />
                    </td>
                </tr>
                <tr>
                    <td>Stamp (180×180)</td>
                    <td>
                        <asp:Image ID="Image2" runat="server" Height="180px" Width="180px"
                            ImageUrl="../sales/pdf/zhang.jpg" />
                    </td>
                </tr>
            </table>
            <div class="footer-note">Recommended image sizes: Logo 60×60, Stamp 180×180</div>
        </div>
    </form>
</body>
</html>

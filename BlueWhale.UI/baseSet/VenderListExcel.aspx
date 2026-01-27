<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VenderListExcel.aspx.cs" Inherits="BlueWhaleUI.baseSet.VenderListExcel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
    <head runat="server">
        <title>Batch Import</title>
        
        <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
        <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />

        <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
        <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
        <script src="../lib/json2.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                $('#customUploader').bind('click', function () {
                    $('#<%= fload.ClientID %>').click();
                });
            });

            $(function () {
                var form = $("#form").ligerForm();
            });
        </script>
        <style type="text/css">
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

<body style=" padding:30px;">
        <form id="form1" runat="server">    
            <table id="form" border="0" cellpadding="0" cellspacing="10" style="width:480px; line-height:40px;">
                <tr>
                    <td style="text-align:left;" colspan="2"><b>Batch import vender details and initial balance</b></td>
                </tr>
                <tr>
                <td style="width:80px; text-align:right; padding-right: 10px">Choose File: </td>
                    <td>
                        <asp:FileUpload ID="fload" runat="server" CssClass="hidden-uploader"/>
                        <div id="customUploader" class="upload-panel" runat="server">
                            Upload
                        </div>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnExcelTo" runat="server" class="ui_state_highlight my-1"  Width="76px"  Text="Import" OnClick="btnExcelTo_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="width:80px; text-align:right;">&nbsp;</td>
                    <td>
                        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
                        <asp:Label ID="Label1" runat="server" Text="Label" Visible="False" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="white-space: nowrap; vertical-align: top;">
                        <b>Remind:</b>
                    </td>
                    <td style="white-space: normal;">
                        Format of imported model cannot be edited, please refer to the demo for the correct input format.
                    </td>
                </tr>
                <tr>
                    <td style="width:80px; text-align:right;">&nbsp;</td>
                    <td>
                        <asp:LinkButton ID="lbtnDownExcel" runat="server" onclick="lbtnDownExcel_Click">Download Import Model</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="width:80px; text-align:right;">&nbsp;</td>
                    <td>
                        <asp:LinkButton ID="lbtnDownExample" runat="server" onclick="lbtnDownExample_Click">Download Demo</asp:LinkButton>
                    </td>
                </tr>
            </table>
            &nbsp;
        </form>
    </body>
</html>
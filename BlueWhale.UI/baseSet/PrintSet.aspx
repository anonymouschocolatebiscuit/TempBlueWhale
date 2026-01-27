<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintSet.aspx.cs" Inherits="BlueWhale.UI.baseSet.PrintSet" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Print Setting</title>
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
    <style>
        body, body * {
            font-family: 'Nunito Sans', 'Segoe UI', sans-serif !important;
        }

        input,
        button,
        select,
        textarea {
            font-family: 'Nunito Sans', 'Segoe UI', sans-serif !important;
        }

        form {
            display: flex;
            justify-content: start;
            align-items: flex-start;
            padding: 20px;
        }

        .card {
            background: #ffffff;
            border-radius: 12px;
            box-shadow: 0 4px 10px rgba(0,0,0,0.08);
            width: 80%;
            max-width: 1100px;
            padding: 40px;
        }

        .page-title {
            font-size: 22px;
            font-weight: 700;
            color: #333;
            margin-bottom: 30px;
            text-align: center;
        }

        table {
            width: 100%;
            border-collapse: collapse;
        }

        td {
            padding: 12px 10px;
            vertical-align: top;
        }

        td:first-child {
            width: 180px;
            font-weight: 600;
            color: #555;
            text-align: right;
            padding-top: 10px;
        }

        td:last-child {
            text-align: left;
        }

        textarea,
        input[type="text"],
        .aspnet-textbox {
            width: 100%;
            box-sizing: border-box;
            font-size: 14px;
            line-height: 1.5;
            color: #333;
            padding: 10px;
            border: 1px solid #d1d5db;
            border-radius: 8px;
            background-color: #fff;
            resize: vertical;
            transition: border-color 0.2s ease, box-shadow 0.2s ease;
        }

        textarea:focus,
        input[type="text"]:focus,
        .aspnet-textbox:focus {
            border-color: #4b5563;
            box-shadow: 0 0 0 3px rgba(75, 85, 99, 0.15);
            outline: none;
        }

        .section {
            margin-bottom: 40px;
        }

        .section-title {
            font-size: 16px;
            font-weight: 600;
            color: #444;
            margin-bottom: 8px;
        }

        .button-row {
            text-align: right;
            margin-top: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="card">
            <div class="page-title">Print Settings</div>

            <div class="section">
                <div class="section-title">Purchase Order Remark</div>
                <asp:TextBox 
                    ID="txtRemarksPurOrder" 
                    runat="server" 
                    Width="100%" 
                    Rows="6" 
                    TextMode="MultiLine"
                    CssClass="aspnet-textbox">
                </asp:TextBox>
                <div class="button-row aspnet-btn">
                    <asp:Button 
                        ID="btnSave" 
                        runat="server" 
                        CssClass="ui-btn ui-btn-sp mrb ui_state_highlight"
                        OnClick="BtnSave_Click" 
                        Text="Save" />
                </div>
            </div>

            <div class="section">
                <div class="section-title">Sales Order Remark</div>
                <asp:TextBox 
                    ID="txtRemarksSalesOrder" 
                    runat="server" 
                    Width="100%" 
                    Rows="6" 
                    TextMode="MultiLine"
                    CssClass="aspnet-textbox">
                </asp:TextBox>
                <div class="button-row aspnet-btn">
                    <asp:Button 
                        ID="btnSaveSalesOrderRemarks" 
                        runat="server" 
                        CssClass="ui-btn ui-btn-sp mrb ui_state_highlight"
                        OnClick="BtnSaveSalesOrderRemarks_Click" 
                        Text="Save" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>

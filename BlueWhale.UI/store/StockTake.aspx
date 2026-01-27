<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockTake.aspx.cs" Inherits="BlueWhale.UI.store.StockTake" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>仓库盘点</title>

    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
    <script src="js/StockTake.js" type="text/javascript"></script>
</head>
<body style="padding-left: 5px;">
    <form id="form1" runat="server">
        <table id="form" border="0" cellpadding="0" cellspacing="0" style="width: 99%; line-height: 40px;">
            <tr>
                <td style="text-align: right; width: 120px;">Select Warehouse：
                </td>
                <td style="text-align: left; width: 120px;">
                    <input type="text" id="txtFlagList" />
                </td>
                <td style="text-align: right; width: 50px;">Type：</td>
                <td style="text-align: left; width: 150px;">
                    <input type="text" id="txtTypeList" />
                </td>
                <td style="text-align: right; width: 70px;">Product：
                </td>
                <td style="text-align: left; width: 100px;">
                    <input type="text" id="txtGoodsList" />
                </td>
                <td style="text-align: right; padding-right: 25px;">
                    <input id="btnSearch" type="button" value="Search" class="ui-btn ui-btn-sp mrb" onclick="search()" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left; height: 300px;" colspan="7">
                    <div id="maingrid"></div>
                    <div style="display: none;">
                    </div>
                </td>
            </tr>
        </table>

        <table id="tbFooter" border="0" cellpadding="0" cellspacing="0" style="width: 99%; line-height: 50px;">
            <tr>
                <td style="width: 80px; text-align: right;">Remarks：</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtRemarks" runat="server" Width="540px" TextMode="MultiLine"></asp:TextBox>
                </td>
                <td style="text-align: right; padding-right: 30px;">
                    <input id="Button1" class="ui-btn ui-btn-sp mrb" type="button" value="Generate Inventory Surplus and Shortage Report" onclick="save()" style="margin:0px;"/>
                </td>
            </tr>
        </table>

        <script type="text/javascript">

            $("#btn1").ligerButton({
                text: 'Get',
                click: function () {
                    var value = $.ligerui.get("popTxt").getValue();
                    alert(value);
                }
            });

            $("#txtGoodsList").ligerPopupEdit({
                condition: {
                    prefixID: 'condtion_',
                    fields: [{ name: 'names', type: 'text', label: 'Product Name' }]
                },
                grid: getGridGoodsList(true),
                valueField: 'code',
                textField: 'code',
                width: 600
            });

            function getGridGoodsList(checkbox) {
                var options = {
                    columns: [
                        { display: 'Product No', name: 'code', width: 100, align: 'center' },
                        { display: 'Product Name', name: 'names', width: 230, align: 'left' },
                        { display: 'Specifications', name: 'spec', width: 120, align: 'center' },
                        { display: 'Unit', name: 'unitName', width: 70, align: 'center' }
                    ], switchPageSizeApplyComboBox: false,

                    url: '../baseSet/GoodsList.aspx?Action=GetDataList',
                    pageSize: 15,
                    dataAction: 'local',
                    usePager: true,


                    checkbox: checkbox
                };
                return options;
            }

            $("#txtFlagList").ligerComboBox({
                isShowCheckBox: true,
                isMultiSelect: true,
                url: "../baseSet/InventoryList.aspx?Action=GetDDLList&r=" + Math.random(),
                valueField: 'ckId',
                textField: 'ckName'
                , valueFieldID: 'ckId'
            });

            $("#txtTypeList").ligerComboBox({
                isShowCheckBox: true,
                isMultiSelect: true,
                url: "../baseSet/GoodsTypeList.aspx?Action=GetDDLList&r=" + Math.random(),
                valueField: 'id',
                textField: 'names'
                , valueFieldID: 'id'
            });
        </script>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsOutInSumReport.aspx.cs" Inherits="BlueWhale.UI.report.GoodsOutInSumReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Goods Out In Summary Report</title>

    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />

    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>

    <script src="js/GoodsOutInSumReport.js" type="text/javascript"></script>
</head>

<body>
    <form id="form1" runat="server">
        <table id="form" border="0" cellpadding="0" cellspacing="0" style="width:99%; line-height:40px;">
            <tr>
                <td style="text-align:right; width:40px;">
                    Date：
                </td>
                <td style="text-align:left; width:120px;">
                    <asp:TextBox ID="txtDateStart" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                </td>
                <td style="text-align:center; width:40px;">
                    Until
                </td>
                <td style="text-align:left; width:120px;">
                    <asp:TextBox ID="txtDateEnd" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                </td>
                <td style="text-align:right; width:60px;">
                    Inventory：
                </td>
                <td style="text-align:left; width:120px;">
                    <input type="text" id="txtFlagList" />
                </td>
                <td style="text-align:right; width:50px;">
                    Item：
                </td>
                <td style="text-align:right; width:170px;">
                    <input type="text" id="txtGoodsList" />
                </td>
                <td style="text-align:right; padding-right:20px;">
                    <input id="btnSearch" type="button" value="Search" class="ui-btn ui-btn-sp mrb" onclick="search()" />
                </td>
            </tr>
            <tr>
                <td style="text-align:left; height:300px;" colspan="9">
                    <div id="maingrid"></div>
                    <div style="display:none;"></div>
                </td>
            </tr>
        </table>

        <script type="text/javascript">
            $("#btn1").ligerButton({
                text: 'Get Value',
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
                        { display: 'Item Code', name: 'code', width: 110, align: 'center' },
                        { display: 'Item Name', name: 'names', width: 230, align: 'left' },
                        { display: 'Specification', name: 'spec', width: 140, align: 'center' },
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
        </script>
    </form>
</body>

</html>
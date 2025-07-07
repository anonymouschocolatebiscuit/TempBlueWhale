<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProduceListReport.aspx.cs" Inherits="BlueWhale.UI.report.ProduceListReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Produce Order Tracking Report</title>

    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />

    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerFilter.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>

    <!-- Inline script -->
    <script type="text/javascript">
        var manager;

        function InitializeManager() {
            var menu = $.ligerMenu({
                width: 120, items: [
                    { text: 'Check order detail', click: viewRow },
                    { line: true },
                    { text: 'Check warehouse detail', click: viewRow }
                ]
            });

            var dateStart = $.ligerui.get("<%=txtDateStart.ClientID%>");
        dateStart.set("Width", 110);

        var dateEnd = $.ligerui.get("<%=txtDateEnd.ClientID%>");
        dateEnd.set("Width", 110);

        var txtFlagList = $.ligerui.get("txtFlagList");
        txtFlagList.set("Width", 100);

        manager = $("#maingrid").ligerGrid({
            columns: [
                {
                    display: 'Plan date', name: 'makeDate', width: 80, align: 'center', valign: 'center',
                    totalSummary:
                    {
                        type: 'count',
                        render: function (e) {
                            return 'Sum：';
                        }
                    }
                },

                { display: 'Receipt no', name: 'number', width: 150, align: 'center' },
                { display: 'Plan Category', name: 'typeName', width: 80, align: 'center' },
                { display: 'Order no', name: 'orderNumber', width: 150, align: 'center' },
                { display: 'Client', name: 'wlName', width: 170, align: 'left' },
                { display: 'Item No', name: 'code', width: 100, align: 'center' },
                { display: 'Item Name', name: 'goodsName', width: 120, align: 'center' },
                { display: 'Specification', name: 'spec', width: 100, align: 'center' },
                { display: 'Unit', name: 'unitName', width: 70, align: 'center' },
                {
                    display: 'Production Planning Quantity', name: 'num', width: 80, align: 'center',
                    totalSummary:
                    {
                        align: 'right',
                        type: 'sum',
                        render: function (e) {
                            return Math.round(e.sum * 100) / 100;
                        }
                    }
                },
                {
                    display: 'Completed Quantity', name: 'finishNum', width: 80, align: 'right',
                    totalSummary:
                    {
                        align: 'right',
                        type: 'sum',
                        render: function (e) {
                            return Math.round(e.sum * 100) / 100;
                        }
                    }
                },
                {
                    display: 'Remaining Quantity', name: 'finishNumNo', width: 80, align: 'right',
                    totalSummary:
                    {
                        align: 'right',   //left/center/right 
                        type: 'sum',    //(sum,max,min,avg,count) 
                        render: function (e) {
                            return Math.round(e.sum * 100) / 100;
                        }
                    }
                },

                { display: 'Progress', name: 'sendFlag', width: 60, align: 'center' },
                { display: 'Plan start date', name: 'dateStart', width: 80, align: 'center', valign: 'center' },
                { display: 'Plan end date', name: 'dateEnd', width: 80, align: 'center', valign: 'center' },
                { display: 'Send Date', name: 'sendDate', width: 80, align: 'center' },
                { display: 'Order status', name: 'flag', width: 80, align: 'center' },
                { display: 'Created by', name: 'makeName', width: 80, align: 'center' },
                { display: 'Reviewed By', name: 'checkName', width: 80, align: 'center' },
                { display: 'Remarks', name: 'remarks', width: 100, align: 'left' }

            ],

            width: '98%',
            height: '98%',
            dataAction: 'server',
            usePager: false,
            rownumbers: true,
            alternatingRow: false,
            onDblClickRow: function (data, rowindex, rowobj) {
                viewRow();
            },
            allowUnSelectRow: true,
            onRClickToSelect: true,
            onContextmenu: function (parm, e) {
                actionCustomerID = parm.data.id;
                menu.show({ top: e.pageY, left: e.pageX });
                return false;
            }
        });
    }

    function viewRow() {
        var row = manager.getSelectedRow();
        // implement your detail view logic here
    }

        function search() {
            var start = $("#txtDateStart").val();
            var end = $("#txtDateEnd").val();
            var wlId = $("#clientId").val();
            var goodsList = $("#txtGoodsId").val();
            var typeId = $("#txtFlagList").val();
            var wlIdString = wlId.split(";");
            var goodsIdString = goodsList.split(";");
            var typeIdString = typeId.split(";");

            if (wlIdString != "") {
                wlId = "";
                for (var i = 0; i < wlIdString.length; i++) {
                    wlId += "'" + wlIdString[i] + "'" + ",";
                }
                wlId = wlId.substring(0, wlId.length - 1);
            }

            if (goodsIdString != "") {
                goodsList = "";
                for (var i = 0; i < goodsIdString.length; i++) {
                    goodsList += "'" + goodsIdString[i] + "'" + ",";
                }
                goodsList = goodsList.substring(0, goodsList.length - 1);
            }

            if (typeIdString != "") {
                typeId = "";
                for (var i = 0; i < typeIdString.length; i++) {
                    typeId += "'" + typeIdString[i] + "'" + ",";
                }
                typeId = typeId.substring(0, typeId.length - 1);
            }

            var keys = $("#txtKeys").val();
            if (keys == "Please enter order no/remark") {
                keys = "";
            }

            manager.setOptions({
                url: "ProduceListReport.aspx?Action=GetDataList&keys=" + keys + "&start=" + start + "&end=" + end + "&wlId=" + wlId + "&goodsId=" + goodsList + "&typeId=" + typeId
            });
        }

    $(function () {
        // Initialize controls first
        $("#<%=txtDateStart.ClientID%>").ligerDateEditor({ width: 110 });
        $("#<%=txtDateEnd.ClientID%>").ligerDateEditor({ width: 110 });

        $("#txtFlagList").ligerComboBox({
            isShowCheckBox: true,
            isMultiSelect: true,
            width: 100,
            data: [
                { text: 'Unproduce', id: '1' },
                { text: 'Progressing', id: '2' },
                { text: 'Completed', id: '44' }
            ],
            valueFieldID: 'flag'
        });

        InitializeManager();

        search();
    });
    </script>

</head>
<body onload="search()">
    <form id="form1" runat="server">
        <table id="form" border="0" cellpadding="0" cellspacing="0" style="width: 99%; line-height: 40px;">
            <tr>
                <td style="text-align: right; width: 70px;">Schedule date： 
                </td>
                <td style="text-align: left; width: 120px;">
                    <asp:TextBox ID="txtDateStart" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                </td>
                <td style="text-align: center; width: 30px;">To
                </td>
                <td style="text-align: left; width: 120px;">
                    <asp:TextBox ID="txtDateEnd" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 80px;">Order no：</td>
                <td style="text-align: left; width: 180px;">
                    <asp:TextBox ID="txtKeys" runat="server" placeholder="Please enter oder no/remark"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 60px;">Vendor：</td>
                <td style="text-align: left; width: 120px;">
                    <input type="text" id="clientName" runat="server" value="" />
                    <input type="hidden" id="clientId" runat="server" value="" />
                </td>
                <td style="text-align: right; width: 50px;">Product：
                </td>
                <td style="text-align: left; width: 100px;">
                    <input type="text" id="txtGoodsName" runat="server" value="" />
                    <input type="hidden" id="txtGoodsId" runat="server" value="" />
                </td>
                <td style="text-align: right; width: 60px;">Status：
                </td>
                <td style="text-align: left; width: 80px;">
                    <input type="text" id="txtFlagList" />
                </td>
                <td style="text-align: right; padding-right: 20px;">
                    <input id="btnSearch" type="button" value="Search" class="ui-btn ui-btn-sp mrb" onclick="search()" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left; height: 300px;" colspan="13">
                    <div id="maingrid"></div>
                    <div style="display: none;">
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientListSelect.aspx.cs" Inherits="BlueWhale.UI.baseSet.ClientListSelect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Client List</title>

    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />

    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
    <script type="text/javascript">

        document.onkeydown = keyDownSearch;

        function keyDownSearch(e) {
            var theEvent = e || window.event;
            var code = theEvent.keyCode || theEvent.which || theEvent.charCode;
            if (code == 13) {
                $("#btnSearch").click();
                return false;
            }
            return true;
        }

        var manager = null;

        $(function () {
            var form = $("#formTB").ligerForm();

            var ddlTypeList = $.ligerui.get("ddlTypeList");
            ddlTypeList.set("Width", 130);

            var txtKeys = $.ligerui.get("txtKeys");
            txtKeys.set("Width", 300);

            manager = $("#maingrid4").ligerGrid({
                columns: [

                    { display: 'Client Category', name: 'typeName', width: 100, type: 'int', align: 'center' },
                    { display: 'Customer No', name: 'code', width: 100, align: 'center' },
                    { display: 'Client Name', name: 'names', width: 230, align: 'left' },
                    { display: 'Bal Date', name: 'yueDate', width: 80, align: 'center' },
                    { display: 'Opn Bal', name: 'balance', width: 70, align: 'center' },
                    { display: 'Tax Rate', name: 'tax', width: 60, align: 'center' },
                    { display: 'P. Contact', name: 'linkMan', width: 70, align: 'center' },

                    { display: 'Mobile Phone', name: 'phone', width: 100, align: 'center' },
                    { display: 'Landline Phone', name: 'tel', width: 110, align: 'center', type: "date" },
                    { display: 'Status', name: 'flag', width: 80, align: 'center' },
                    { display: 'Entry Date', name: 'makeDate', width: 80, align: 'center', type: "date" }
                ], pageSize: 10,
                rownumbers: true,
                usePager: false,
                url: 'ClientListSelect.aspx?Action=GetDataList',
                width: '690', height: '420'
            });
            $("#pageloading").hide();
        });

        function f_select() {
            return manager.getSelectedRow();
            if (!row) return null;
            return {
                names: row.names,
                code: row.code
            };
        }

        function search() {
            var keys = document.getElementById("txtKeys").value;
            if (keys == "Please enter Phone Number/Name/Contact") {
                keys = "";
            }
            var reg = /^[a-zA-Z0-9\s]*$/;
            if (!reg.test(keys)) {
                keys = "";
            }
            var typeId = $("#ddlTypeList").val();

            manager.changePage("first");
            manager._setUrl("ClientListSelect.aspx?Action=GetDataListSearch&keys=" + encodeURIComponent(keys) + "&typeId=" + typeId);
        }

        function filterKeys(input) {
            input.value = input.value.replace(/[^a-zA-Z0-9\s]/g, "");
        }
    </script>
</head>

<body>
    <form id="form1" runat="server">
        <table id="formTB" border="0" style="width: 770px; line-height: 40px;">
            <tr>
                <td style="width: 120px;">
                    <asp:DropDownList ID="ddlTypeList" runat="server">
                    </asp:DropDownList>
                </td>
                <td style="width: 300px;">
                    <asp:TextBox ID="txtKeys" runat="server" placeholder="Please enter Phone Number/Name/Contact" oninput="filterKeys(this)"></asp:TextBox>
                </td>

                <td>
                    <input id="btnSearch" type="button" value="Search" class="ui-btn" onclick="search()" />
                </td>
            </tr>

            <tr>
                <td colspan="3">
                    <div id="maingrid4" style="margin: 0; padding: 0"></div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VenderListSelect.aspx.cs" Inherits="BlueWhale.UI.baseSet.VenderListSelect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Supplier List</title>

    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />

    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>

    <script type="text/javascript">
        document.onkeydown = keyDownSearch;

        function keyDownSearch(e) {
            // FF,IE,Opera Compatibility
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
            ddlTypeList.set("Width", 140);

            var txtKeys = $.ligerui.get("txtKeys");
            txtKeys.set("Width", 330);

            manager = $("#maingrid4").ligerGrid({

                //checkbox:true,
                columns: [
                    { display: 'Supplier Type', name: 'typeName', width: 100, type: 'int', align: 'center' },
                    { display: 'Supplier Code', name: 'code', width: 100, align: 'center' },
                    { display: 'Supplier Name', name: 'names', width: 100, align: 'left' },
                    { display: 'Balance Date', name: 'yueDate', width: 110, align: 'center' },
                    { display: 'Balance', name: 'balance', width: 70, align: 'center' },
                    { display: 'Tax%', name: 'tax', width: 60, align: 'center' },
                    { display: 'Contact', name: 'linkMan', width: 120, align: 'center' },

                    { display: 'Mobile', name: 'phone', width: 70, align: 'center' },
                    { display: 'Tel', name: 'tel', width: 50, align: 'center', type: "date" },
                    { display: 'Status', name: 'flag', width: 70, align: 'center' },
                    { display: 'Create Date', name: 'makeDate', width: 100, align: 'center', type: "date" }
                ], pageSize: 10,
                rownumbers: true,//序号
                usePager: false,
                url: 'VenderListSelect.aspx?Action=GetDataList',
                width: '850', height: '420'
            });
            $("#pageloading").hide();
        });
        function f_select() {
            return manager.getSelectedRow();
        }

        function search() {
            var keys = document.getElementById("txtKeys").value;
            if (keys == "Please Enter Name/Mobile/Contact/Remarks/Address") {

                keys = "";
            }
            var typeId = $("#ddlTypeList").val();

            manager.changePage("first");
            manager._setUrl("VenderListSelect.aspx?Action=GetDataListSearch&keys=" + keys + "&typeId=" + typeId);

        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table id="formTB" border="0" style="width: 780px; line-height: 40px;">

            <tr>
                <td style="width: 120px;">
                    <asp:DropDownList ID="ddlTypeList" runat="server">
                    </asp:DropDownList>
                </td>
                <td style="width: 300px;">
                    <asp:TextBox ID="txtKeys" runat="server" placeholder="Please Enter Name/Mobile/Contact/Remarks/Address"></asp:TextBox>
                </td>
                <td>
                    <input id="btnSearch" type="button" value="Search" class="ui-btn" onclick="search()" /></td>
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

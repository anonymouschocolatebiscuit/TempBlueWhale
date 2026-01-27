<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsListSelect.aspx.cs" Inherits="BlueWhale.UI.baseSet.GoodsListSelect" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Good List</title>

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
            var txtKeys = $.ligerui.get("txtKeys");
            txtKeys.set("Width", 300);
            manager = $("#maingrid4").ligerGrid({
                checkbox: true,
                columns: [
                    { display: 'Good Code', name: 'code', width: 100, align: 'center' },
                    { display: 'Good Name', name: 'names', width: 150, align: 'left' },
                    { display: 'Specification', name: 'spec', width: 114, align: 'center' },
                    { display: 'Unit', name: 'unitName', width: 50, align: 'center' },
                    { display: 'Barcode', name: 'barcode', width: 100, align: 'center' }
                ], pageSize: 10,
                rownumbers: true,
                usePager: false,
                url: 'GoodsListSelect.aspx?Action=GetDataList',
                width: '690', height: '400'
            });
            $("#pageloading").hide();
        });

        function f_select() {
            return manager.getSelectedRows();
        }

        function search() {
            var keys = document.getElementById("txtKeys").value;
            if (keys == "Please enter code/name/specification") {
                keys = "";
            }
            var reg = /^[a-zA-Z0-9\s]*$/;
            if (!reg.test(keys)) {
                keys = "";
            }

            manager.changePage("first");
            manager._setUrl("GoodsListSelect.aspx?Action=GetDataListSearch&keys=" + encodeURIComponent(keys) + "&typeId=" + typeId);
        }

        var typeId = 0;

        $(function () {
            $("#tree1").ligerTree({
                url: "GoodsTypeList.aspx?Action=GetTreeList",
                onSelect: onSelect,
                parentIcon: null,
                childIcon: null,
                checkbox: false,
                slide: false,
                treeLine: true,
                idFieldName: 'id',
                parentIDFieldName: 'pid'
            });
        });

        function onSelect(note) {
            typeId = note.data.id;
            search();
        }

        function filterKeys(input) {
            input.value = input.value.replace(/[^a-zA-Z0-9\s]/g, "");
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table id="formTB" border="0" style="width: 800px; line-height: 40px;">
            <tr>
                <td>
                    <asp:TextBox ID="txtKeys" runat="server" placeholder="Please enter code/name/specification" oninput="filterKeys(this)"></asp:TextBox>
                </td>

                <td style="width: 200px;">
                    <input id="btnSearch" type="button" value="Search" class="ui-btn" onclick="search()" /></td>
            </tr>
            <tr>
                <td>
                    <div id="maingrid4" style="margin: 0; padding: 0"></div>
                </td>
                <td valign="top">
                    <div style="width: 200px; position: relative; height: 400px; display: block; margin: 10px; background: white; border: 1px solid #ccc; overflow: auto;">
                        <ul id="tree1">
                        </ul>
                    </div>
                    <div style="display: none">
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

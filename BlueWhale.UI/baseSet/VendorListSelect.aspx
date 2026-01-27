<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VendorListSelect.aspx.cs" Inherits="BlueWhale.UI.baseSet.VendorListSelect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <title>Vender List</title>

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
                ddlTypeList.set("Width", 160);

                var txtKeys = $.ligerui.get("txtKeys");
                txtKeys.set("Width", 400);

                manager = $("#maingrid4").ligerGrid({
                    columns: [
                        { display: 'Vender Category', name: 'typeName', width: 120, type: 'int', align: 'center' },
                        { display: 'Vender Number', name: 'code', width: 110, align: 'center' },
                        { display: 'Vender Name', name: 'names', width: 230, align: 'left' },
                        { display: 'Balance Date', name: 'yueDate', width: 80, align: 'center' },
                        { display: 'Opening Balance', name: 'balance', width: 120, align: 'center' },
                        { display: 'Tax rate%', name: 'tax', width: 70, align: 'center' },
                        { display: 'Primary contact person', name: 'linkMan', width: 180, align: 'center' },
                        { display: 'Mobile Phone', name: 'phone', width: 110, align: 'center' },
                        { display: 'Landline Phone', name: 'tel', width: 120, align: 'center', type: "date" },
                        { display: 'Status', name: 'flag', width: 80, align: 'center' },
                        { display: 'Entry Date', name: 'makeDate', width: 80, align: 'center', type: "date" }
                    ], pageSize: 10,
                    rownumbers: true,
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
                if (keys == "Please Enter Name/Mobile Phone/Username/Remark/Address") {
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
            <table id="formTB" border="0" style="width:780px; line-height:40px;">
                <tr>
                    <td style="width:120px;">
                        <asp:DropDownList ID="ddlTypeList" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td style="width:300px;">
                        <asp:TextBox ID="txtKeys" runat="server" placeholder="Please Enter Name/Mobile Phone/Username/Remark/Address"></asp:TextBox>
                    </td>
                    <td>
                        <input id="btnSearch" type="button" value="Search" class="ui-btn" onclick="search()" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <div id="maingrid4" style="margin:0; padding:0"></div>
                    </td>
                </tr>
            </table>
        </form>
    </body>

</html>
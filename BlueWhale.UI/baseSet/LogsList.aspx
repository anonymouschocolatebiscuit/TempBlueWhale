<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogsList.aspx.cs" Inherits="BlueWhale.UI.baseSet.LogsList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
    <head runat="server">
        <title>Log Management</title>

        <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
        <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
        <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
        <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
        <script src="../lib/json2.js" type="text/javascript"></script>
        <script type="text/javascript">
            var manager;

            $(function() {
            var form = $("#form").ligerForm();
                manager = $("#maingrid").ligerGrid({
                    checkbox: true,
                    columns: [
                        { display: 'Id', name: 'id', id: 'id', align: 'center', width: '10%' },
                        { display: 'User', name: 'users', type: 'int', align: 'left', width: '20%' },
                        { display: 'Event', name: 'events', align: 'left', width: '50%' },
                        { display: 'IP Address', name: 'ip', align: 'center', width: '10%'},
                        { display: 'Date', name: 'date', align: 'center', width: '10%' }  
                    ],
                    width: 'auto',
                    pageSizeOptions: [20, 50, 100, 200],
                    height: 'auto',
                    pageSize: 20,
                    dataAction: 'local', // Local arrange
                    usePager: true,
                    url: "LogsList.aspx?Action=GetDataList", 
                    alternatingRow: false,
                    isChecked: f_isChecked, onCheckRow: f_onCheckRow, onCheckAllRow: f_onCheckAllRow             
                });
            });   

            function f_set() {     
                form.setData({        
                    keys:"",
                    dateStart: new Date("<% =start%>"),
                    dateEnd: new Date("<% =end%>")
                });
            }

            function search() {
                var keys = document.getElementById("txtKeys").value;
                if (keys == "Enter user/event/IP address") {
                    keys = "";
                }

                var start = document.getElementById("txtDateStart").value;
                var end = document.getElementById("txtDateEnd").value;

                manager.changePage("first");
                manager._setUrl("LogsList.aspx?Action=GetDataListSearch&keys=" + keys + "&start=" +start + "&end=" + end);
            }
       
            function deleteRow() {
                var row = manager.getSelectedRow();
                if (!row) { $.ligerDialog.warn('Please select the row to delete'); return; }
           
                var idString = checkedCustomer.join(','); // Use "," to seperate all the customer and pass to backoffice

                $.ligerDialog.confirm('Cannot recover after delete, confirm to delete?', function(type) {        
                    if (type) {    
                        $.ajax({
                            type: "GET",
                            url: "LogsList.aspx",
                            data: "Action=delete&id=" + idString + " &ranid=" + Math.random(),
                            success: function(resultString) {
                                $.ligerDialog.alert(resultString, 'Prompt Message');
  
                                reload();
                            },
                            error: function(msg) {  
                                $.ligerDialog.alert("Network abnormality, please contact the administrator", 'Prompt Message');
                            }
                        });
                    }
                });
            }

            function reload() {
                manager.reload();
            }

            function f_onCheckAllRow(checked) {
                for (var rowid in this.records) {
                    if (checked)
                        addCheckedCustomer(this.records[rowid]['id']);
                    else
                        removeCheckedCustomer(this.records[rowid]['id']);
                }
            }

            /*
                This example implements form paging and multiple selections
                That is use for onCheckRow to memorize the selected row, and use isChecked to initialize the memorized row.
            */
            var checkedCustomer = [];
            function findCheckedCustomer(id) {
                for (var i = 0; i < checkedCustomer.length; i++) {
                    if (checkedCustomer[i] == id)
                        return i;
                }
                return -1;
            }

            function addCheckedCustomer(id) {
                if (findCheckedCustomer(id) == -1)
                    checkedCustomer.push(id);
            }

            function removeCheckedCustomer(id) {
                var i = findCheckedCustomer(id);
                if (i == -1)
                    return;
                checkedCustomer.splice(i, 1);
            }

            function f_isChecked(rowdata) {
                if (findCheckedCustomer(rowdata.id) == -1)
                    return false;
                return true;
            }

            function f_onCheckRow(checked, data) {
                if (checked) addCheckedCustomer(data.id);
                else removeCheckedCustomer(data.id);
            }
            function f_getChecked() {
                alert(checkedCustomer.join(','));
            }
        </script>

        <style type="text/css">
            .l-button{
                width: 120px; 
                float: left; 
                margin-left: 10px; 
                margin-bottom:2px; 
                margin-top:2px;
            }
        </style> 
    </head>

    <body style="padding-left:10px; padding-top:10px;">
        <form id="form1" runat="server">
            <table id="form" border="0" cellpadding="0" cellspacing="0" style="width:100%; line-height:40px;">
                <tr>
                    <td style="text-align:left; width:70px;">Keyword:</td>
                    <td style="text-align:center; width:180px;">
                        <asp:TextBox ID="txtKeys" runat="server" placeholder="Enter user/event/IP address"></asp:TextBox>
                    </td>
                    <td style="text-align:center; width:70px;">Start Date:</td>
                    <td style="text-align:right; width:180px;">
                        <asp:TextBox ID="txtDateStart" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
                    </td>
                    <td style="width:15px;"></td>
                    <td style="text-align:center; width:70px;">End Date:</td>
                    <td style="text-align:right;">
                        <asp:TextBox ID="txtDateEnd" runat="server" ltype="date" validate="{required:true}"></asp:TextBox> 
                    </td>
                    <td style="text-align:center; width:220px;">
                        <input id="btnAdd" type="button" value="Search" class="ui-btn ui-btn-sp mrb" onclick="search()"/>
                        <input id="btnReload" class="ui-btn" type="button" value="Delete" onclick="deleteRow()"/>
                    </td>
                </tr>

                <tr>
                    <td style="text-align:left; height:300px;" colspan="7">
                        <div id="maingrid" style="margin-right: 20px;"></div>
                    </td>
                </tr>
            </table>
        </form>
    </body>
</html>
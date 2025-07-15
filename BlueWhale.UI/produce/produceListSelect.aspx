<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="produceListSelect.aspx.cs" Inherits="BlueWhale.UI.produce.produceListSelect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Sales Order Search</title>
    
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

            var form = $("#form").ligerForm();
            var txtKeys = $.ligerui.get("txtKeys");
            txtKeys.set("Width", 200);

            var txtDateStart = $.ligerui.get("txtDateStart");
            txtDateStart.set("Width", 120);

            var txtDateEnd = $.ligerui.get("txtDateEnd");
            txtDateEnd.set("Width", 120);


            manager = $("#maingrid").ligerGrid({

                checkbox: false,
                columns: [

                    { display: 'Plan No.', name: 'number', width: 150, align: 'center' },

                    { display: 'Client', name: 'wlName', width: 170, align: 'left' },

                    { display: 'Item No.', name: 'code', width: 80, align: 'center' },
                    { display: 'Item Name', name: 'goodsName', width: 150, align: 'left' },
                    { display: 'Specification', name: 'spec', width: 100, align: 'center' },
                    { display: 'Unit', name: 'unitName', width: 70, align: 'center' },
                    { display: 'Produce Quantity', name: 'num', width: 120, align: 'center' },
                    { display: 'Quantity Produced', name: 'finishNum', width: 120, align: 'center' },
                    { display: 'Quantity UnProduced', name: 'finishNumNo', width: 120, align: 'center' },
                    { display: 'Start Date', name: 'dateStart', width: 120, align: 'center' }





                ], pageSize: 10,
                rownumbers: true,
                usePager: false,
                url: 'produceListSelect.aspx?Action=GetDataList',
                width: '900', height: '400'
            });
            $("#pageloading").hide();
        });



        function f_select() {
            return manager.getSelectedRows();
        }

        function search() {
            var keys = document.getElementById("txtKeys").value;
            if (keys == "Please Enter Plan Order No./Item/Client/Remarks") {

                keys = "";

            }
            var start = document.getElementById("txtDateStart").value;
            var end = document.getElementById("txtDateEnd").value;


            manager.changePage("first");
            manager._setUrl("produceListSelect.aspx?Action=GetDataListSearch&keys=" + keys + "&start=" + start + "&end=" + end);
        }
    </script>




</head>
<body>
    <form id="form1" runat="server">

         <table id="form" border="0" cellpadding="0" cellspacing="0" style="line-height:40px;">
           <tr>
           <td style="text-align:right; width:50px;">
            
            Keyword:  
            
            
            </td>
           <td style="text-align:left; width:220px;">
               <asp:TextBox ID="txtKeys" runat="server" placeholder="Please Enter Plan Order No./Item/Client/Remarks"></asp:TextBox>
            </td>
           <td style="text-align:right; width:70px;">
            
               Start Date: 
            
              </td>
           <td style="text-align:left; width:150px;">
            
           <asp:TextBox ID="txtDateStart" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
           </td>
           <td style="text-align:right; width:70px;">
            
               End Date: 
           
           </td>
            <td style="text-align:left; width:150px;">
            
            <asp:TextBox ID="txtDateEnd" runat="server" ltype="date" validate="{required:true}"></asp:TextBox> 
            
            </td>
           <td style="text-align:center;width:100px;">
           
           
           <input id="btnSearch" type="button" value="Search" class="ui-btn ui-btn-sp mrb" onclick="search()" />
            
            </td>
           </tr>
           </table>

         <div id="maingrid"></div>  
            <div style="display:none;">
   
      </div>
   
    </form>
</body>
</html>

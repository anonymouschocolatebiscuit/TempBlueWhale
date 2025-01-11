<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalesOrderListSelect.aspx.cs" Inherits="Lanwei.Weixin.UI.sales.SalesOrderListSelect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>销售订单查询</title>
    
     <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
     

    <script type="text/javascript">


        document.onkeydown = keyDownSearch;

        function keyDownSearch(e) {
            // 兼容FF和IE和Opera    
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

               { display: '订单编号', name: 'number', width: 150, align: 'center' },

                 { display: '客户', name: 'wlName', width: 170, align: 'left' },

                 { display: '商品编号', name: 'code', width: 80, align: 'center' },
                 { display: '商品名称', name: 'goodsName', width: 150, align: 'left' },
                  { display: '规格', name: 'spec', width: 100, align: 'center' },
                  { display: '单位', name: 'unitName', width: 70, align: 'center' },          
                  { display: '数量', name: 'num', width: 80, align: 'center' },
                  { display: '交货日期', name: 'sendDate', width: 80, align: 'center' }





                ], pageSize: 10,
                rownumbers: true,//序号
                usePager: false,
                url: 'SalesOrderListSelect.aspx?Action=GetDataList',
                width: '900', height: '400'
            });
            $("#pageloading").hide();
        });



        function f_select() {
            return manager.getSelectedRows();



        }

        function search() {


            var keys = document.getElementById("txtKeys").value;
            if (keys == "请输入订单号/商品/客户/备注") {

                keys = "";

            }
            var start = document.getElementById("txtDateStart").value;
            var end = document.getElementById("txtDateEnd").value;


            manager.changePage("first");
            manager._setUrl("SalesOrderListSelect.aspx?Action=GetDataListSearch&keys=" + keys + "&start=" + start + "&end=" + end);


           

        }




    


      




    </script>




</head>
<body>
    <form id="form1" runat="server">
   
   

         <table id="form" border="0" cellpadding="0" cellspacing="0" style="line-height:40px;">
           <tr>
           <td style="text-align:right; width:50px;">
            
            关键字： 
            
            
            </td>
           <td style="text-align:left; width:220px;">
            
             
            
               <asp:TextBox ID="txtKeys" runat="server" nullText="请输入订单号/商品/客户/备注"></asp:TextBox>
            
             
            
             
            
             
            
            </td>
           <td style="text-align:right; width:70px;">
            
               开始日期：
            
              </td>
           <td style="text-align:left; width:150px;">
            
           <asp:TextBox ID="txtDateStart" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
            
            
           </td>
           <td style="text-align:right; width:70px;">
            
            
               结束日期：
             
             
            
           
           </td>
            <td style="text-align:left; width:150px;">
            
            <asp:TextBox ID="txtDateEnd" runat="server" ltype="date" validate="{required:true}"></asp:TextBox> 
            
            
            
             
            
            </td>
           <td style="text-align:center;width:100px;">
           
           
           <input id="btnSearch" type="button" value="查询" class="ui-btn ui-btn-sp mrb" onclick="search()" />
           
            
            </td>

           </tr>
           </table>
    
   

         <div id="maingrid"></div>  
            <div style="display:none;">
   
      </div>

   
   
    </form>
</body>
</html>

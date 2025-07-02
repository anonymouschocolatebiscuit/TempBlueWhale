<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsListSet.aspx.cs" Inherits="BlueWhale.UI.baseSet.GoodsListSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Good Management</title>
     <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
     
  <script type="text/javascript">

      var manager;
      $(function () {

          var form = $("#form").ligerForm();

          var txtKeys = $.ligerui.get("txtKeys");
          txtKeys.set("Width", 260);

          manager = $("#maingrid").ligerGrid({
              checkbox: false,
              columns: [

                  {
                      display: 'Operation', isSort: false, width: 50, align: 'center',
                      render: function (rowdata, rowindex, value) {
                          var h = "";
                          if (!rowdata._editing) {
                              h += "<a href='javascript:editDetail()' title='Edit Good Description' style='float:left;'><div class='ui-icon ui-icon-pencil'></div></a> ";
                              h += "<a href='javascript:editImages()' title='Manage Good Images' style='float:right;'><div class='ui-icon ui-icon-pic'></div></a> ";
                          }
                          return h;
                      }
                  },

                  { display: 'Good Category', name: 'typeName', width: 100, type: 'int', align: 'center', valign: "center" },
                  { display: 'Good Barcode', name: 'barcode', width: 100, align: 'center' },
                  { display: 'Good Code', name: 'code', width: 100, align: 'center' },
                  { display: 'Good Name', name: 'names', width: 230, align: 'left' },
                  { display: 'Specification', name: 'spec', width: 150, align: 'center' },
                  { display: 'Unit', name: 'unitName', width: 70, align: 'center' },

                  { display: 'Estimated Purchase Price', name: 'priceCost', width: 80, align: 'center', type: "date" },
                  { display: 'Estimated Selling Price', name: 'priceSales', width: 80, align: 'center' },

                  {
                      display: 'Recommended Good', isSort: false, width: 80, align: 'center',
                      render: function (row) {
                          var html = '<input type ="checkbox" onclick="EditRow(1,this)"  ';
                          if (row.tj == 1) {
                              html += ' checked="checked" ';
                          }
                          html += ' style="margin-top:6px;" /> ';
                          return html;
                      }
                  },

                  {
                      display: 'Newest Good', width: 80, isAllowHide: false, name: 'checkbox', isSort: false,
                      render: function (row) {
                          var html = '<input type ="checkbox" onclick="EditRow(2,this)"  ';
                          if (row.xp == 1) {
                              html += ' checked="checked" ';
                          }
                          html += ' style="margin-top:6px;" /> ';
                          return html;
                      }
                  },

                  {
                      display: 'Promotional Good', isSort: false, width: 80, align: 'center',
                      render: function (row) {
                          var html = '<input type ="checkbox" onclick="EditRow(3,this)"  ';
                          if (row.cx == 1) {
                              html += ' checked="checked" ';
                          }
                          html += ' style="margin-top:6px;" /> ';
                          return html;
                      }
                  }


              ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '99%',
              pageSize: 15,
              dataAction: 'local', //Local sorting
              usePager: true,
              url: 'GoodsListSet.aspx?Action=GetDataList&typeId=&goodsId=',
              alternatingRow: false,
              isChecked: f_isChecked, onCheckRow: f_onCheckRow, onCheckAllRow: f_onCheckAllRow,


              rownumbers: true //Display serial number



          }
          );

      });



      function EditRow(showType, obj) {
          var row = manager.getSelectedRow();
          if (!row) {
              return;

          }

          var cloumn = "tj";

          if (showType == 1) {
              cloumn = "tj";
          }
          if (showType == 2) {
              cloumn = "xp";
          }
          if (showType == 3) {
              cloumn = "cx";
          }




          var selectYes = 0;
          if (obj.checked == true) {
              selectYes = 1;
          }
          else {
              selectYes = 0;
          }

          $.ajax({
              type: "GET",
              url: "GoodsListSet.aspx",
              data: "Action=SetShow&goodsId=" + row.id + "&showType=" + cloumn + "&selectYes=" + selectYes + "&ranid=" + Math.random(), //encodeURI
              success: function (resultString) {
                  search();

              },
              error: function (msg) {

                  $.ligerDialog.alert("Network error, please contact the administrator", 'Notification');
              }
          });




      }


      function search() {




          var typeList = $("#txtTypeList").val();

          var typeIdString = typeList.split(";");
          var goodsList = $("#txtKeys").val();
          if (goodsList == "Please enter good code/name") {
              goodsList = "";
          }

          if (typeIdString != "") {
              typeList = "";
              for (var i = 0; i < typeIdString.length; i++) {
                  typeList += "'" + typeIdString[i] + "'" + ",";
              }
              typeList = typeList.substring(0, typeList.length - 1);

          }



          manager._setUrl("GoodsListSet.aspx?Action=GetDataList&goodsId=" + goodsList + "&typeId=" + typeList);

      }



      function editDetail() {

          var row = manager.getSelectedRow();
          if (!row) { $.ligerDialog.warn('Please select a row to modify'); return; }





      }

      function editImages() {

          var row = manager.getSelectedRow();
          if (!row) { $.ligerDialog.warn('Please select a row to modify'); return; }





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

      var checkedCustomer = [];
      function findCheckedCustomer(id) {
          for (var i = 0; i < checkedCustomer.length; i++) {
              if (checkedCustomer[i] == id) return i;
          }
          return -1;
      }
      function addCheckedCustomer(id) {
          if (findCheckedCustomer(id) == -1)
              checkedCustomer.push(id);
      }
      function removeCheckedCustomer(id) {
          var i = findCheckedCustomer(id);
          if (i == -1) return;
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
    .l-button{width: 120px; float: left; margin-left: 10px; margin-bottom:2px; margin-top:2px;}
    </style>


    
</head>
<body style="padding-left:10px;padding-top:10px;">
    <form id="form1" runat="server">
  
  <table id="form" border="0" cellpadding="0" cellspacing="0" style="width:99%; line-height:40px;">
           <tr>
           <td style="text-align:right; width:70px;">
            
               Good Type：
                       
            
            </td>
           <td style="text-align:left; width:120px;">
            
           
            <input type="text" id="txtTypeList"/></td>
           <td style="text-align:right;width:80px;">
           
               Keyword：</td>
           <td style="text-align:left; width:200px;">
           
               <asp:TextBox ID="txtKeys" runat="server" placeholder="Please enter good code/name"></asp:TextBox>
          
           
           
           </td>
           <td style="text-align:right; padding-right:20px;">
           
              <input id="btnSearch" type="button" value="Search" class="ui-btn ui-btn-sp mrb" onclick="search()" /> </td>
           </tr>
           <tr>
           <td style="text-align:left; height:300px;" colspan="5">
            
             <div id="maingrid"></div>  
            <div style="display:none;"></div>
            
            </td>
           </tr>
           </table>
  
    </form>
    <p>
        <input id="Checkbox1" type="checkbox" /></p>
       
            <script type="text/javascript">

                $("#txtTypeList").ligerComboBox({
                    isShowCheckBox: true,
                    isMultiSelect: true,
                    url: "GoodsTypeList.aspx?Action=GetDDLList&r=" + Math.random(),
                    valueField: 'id',
                    textField: 'names'
                    , valueFieldID: 'id'
                });

            </script>
</body>
</html>

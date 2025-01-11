<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsOutInSumReport.aspx.cs" Inherits="Lanwei.Weixin.UI.report.GoodsOutInSumReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>商品收发汇总表</title>
    
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
            
              日期： 
            
            
            </td>
           <td style="text-align:left; width:120px;">
            
             
            
           <asp:TextBox ID="txtDateStart" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
            
            
                   </td>
           <td style="text-align:center; width:30px;">
            
            
               至
            
              </td>
           <td style="text-align:left; width:120px;">
            
            <asp:TextBox ID="txtDateEnd" runat="server" ltype="date" validate="{required:true}"></asp:TextBox> 
            
            
            
             
            
           </td>
           <td style="text-align:right; width:60px;">
            
            
               仓库：</td>
            <td style="text-align:left; width:120px;">
           
             <input type="text" id="txtFlagList"/>  
           
           
           </td>
           <td style="text-align:right;width:50px;">
           
           商品：
       
           
            
            </td>
           <td style="text-align:left;width:170px;">
           
            <input type="text" id="txtGoodsList"/> 
           
           </td>
           <td style="text-align:right; padding-right:20px;">
           
              <input id="btnSearch" type="button" value="查询" class="ui-btn ui-btn-sp mrb" onclick="search()" /></td>
           </tr>
           <tr>
           <td style="text-align:left; height:300px;" colspan="9">
            
            <div id="maingrid"></div>  
            <div style="display:none;">
   
</div>
            
            </td>
           </tr>
           </table>
      
      
      <script type="text/javascript">
            
            $("#btn1").ligerButton({
                text: '获取值',
                click: function () {
                    var value = $.ligerui.get("popTxt").getValue();
                    alert(value);
                }
            });


         
            
            
             $("#txtGoodsList").ligerPopupEdit({
                condition: {
                    prefixID: 'condtion_',
                    fields: [{ name: 'names', type: 'text', label: '商品名称' }]
                },
                grid: getGridGoodsList(true),
                valueField: 'code',
                textField: 'code',
                width: 600
            });
            
             function getGridGoodsList(checkbox) {
                var options = {
                    columns: [
                      { display: '商品编号', name: 'code', width: 100, align: 'center' },
                 { display: '商品名称', name: 'names', width: 230, align: 'left'},
                  { display: '规格', name: 'spec', width: 70, align: 'center' },
                  { display: '单位', name: 'unitName', width: 70, align: 'center' }
                    ], switchPageSizeApplyComboBox: false,
                    
                  url: '../baseSet/GoodsList.aspx?Action=GetDataList', 
                  pageSize: 15,
                  dataAction: 'local', //本地排序
                  usePager: true,
                    
                   
                    checkbox: checkbox
                };
                return options;
            }
            
              $("#txtFlagList").ligerComboBox({ 
                isShowCheckBox: true, 
                isMultiSelect: true,
                url:"../baseSet/CangkuList.aspx?Action=GetDDLList&r=" + Math.random(), 
                valueField: 'ckId',
                textField:'ckName'
                ,valueFieldID: 'ckId'
            }); 


            
            
        </script>






      
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="produceList.aspx.cs" Inherits="Lanwei.Weixin.UI.produce.produceList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    
<title>生成工单查询</title>
    
     <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
     

    <script src="js/produceList.js" type="text/javascript"></script>




</head>
<body style="padding-left:10px; padding-top:10px;">
    <form id="form1" runat="server">
  
    <table id="form" border="0" cellpadding="0" cellspacing="0" style="width:100%; line-height:40px;">
           <tr>
           <td style="text-align:right; width:50px;">
            
            关键字： 
            
            
            </td>
           <td style="text-align:left; width:180px;">
            
             
            
               <asp:TextBox ID="txtKeys" runat="server" nullText="请输入单据号/客户/商品/备注"></asp:TextBox>
            
             
            
             
            
             
            
            </td>
           <td style="text-align:right; width:70px;">
            
               开始日期：
            
              </td>
           <td style="text-align:left; width:180px;">
            
           <asp:TextBox ID="txtDateStart" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
            
            
           </td>
           <td style="text-align:right; width:70px;">
            
            
               结束日期：
             
             
            
           
           </td>
            <td style="text-align:left; width:180px;">
            
            <asp:TextBox ID="txtDateEnd" runat="server" ltype="date" validate="{required:true}"></asp:TextBox> 
            
            
            
             
            
            </td>
           <td style="text-align:center;width:100px;">
           
           
           <input id="btnSearch" type="button" value="查询" class="ui-btn ui-btn-sp mrb" onclick="search()" />
           
            
            </td>
           <td style="text-align:right; padding-right:20px;">
           
          
           
           <input id="btnAdd" type="button" value="新增" class="ui-btn" onclick="add()" />
           
          
           
           
           
           
           
           
               <input id="btnCheck" type="button" value="审核" class="ui-btn" onclick="checkRow()" />
                
                  <input id="btnCheckNo" type="button" value="反审核" class="ui-btn" onclick="checkNoRow()" />



                <input id="btnProduceIn" type="button" value="入库" class="ui-btn" onclick="inRow()" />
                
               
             
               
               <input id="btnReload" class="ui-btn" type="button" value="删除" onclick="deleteRow()" />
            
            
            
            
                   
            </td>
           </tr>
           <tr>
           <td style="text-align:left; height:300px;" colspan="8">
            
            <div id="maingrid"></div>  
            <div style="display:none;">
   
</div>
            
            </td>
           </tr>
           </table>
    
  

    </form>
</body>
</html>

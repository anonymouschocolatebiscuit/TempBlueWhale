<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssembleList.aspx.cs" Inherits="Lanwei.Weixin.UI.store.AssembleList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>查询商品组装单</title>
   
   <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>


     <script src="js/AssembleList.js" type="text/javascript"></script>


    
    
</head>
<body style=" padding-top:10px; padding-left:10px;">


    <form id="form1" runat="server">
   
 <table id="form" border="0" cellpadding="0" cellspacing="0" style="width:99%; line-height:30px;">
           <tr>
           <td style="width:70px; text-align:center;">
               关键字：</td>
           <td style="text-align:left; width:180px;">
            
             
            
               <asp:TextBox ID="txtKeys" runat="server" nullText="请输单据号/组装商品/备注"></asp:TextBox>
            
             
            
             
            
             
            
                   </td>
           <td style="text-align:right; width:70px;">
                                                  开始日期：</td>
           <td style="text-align:left; width:180px;">
            
           <asp:TextBox ID="txtDateStart" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
            
            
               </td>
           <td style="text-align:right; width:70px;">
                                                  结束日期：</td>
           <td style="text-align:left; width:180px;">
            
            <asp:TextBox ID="txtDateEnd" runat="server" ltype="date" validate="{required:true}"></asp:TextBox> 
            
            
            
             
            
               </td>
           <td style="text-align:right; width:80px;">
           
           
           <input id="btnSearch" type="button" value="查询" class="ui-btn ui-btn-sp mrb" onclick="search()" /></td>
           <td style="text-align:right; padding-right:20px;">
           
          
           
           <input id="btnAdd" type="button" value="新增" class="ui-btn" onclick="add()" />
           
          
           
           
           
           
           
           
               <input id="btnCheck" type="button" value="审核" class="ui-btn" onclick="checkRow()" />
                
                  <input id="btnCheckNo" type="button" value="反审核" class="ui-btn" onclick="checkNoRow()" />
                
               
             
               
               <input id="btnReload" class="ui-btn" type="button" value="删除" onclick="deleteRow()" />
            
            
            
            
                   
            </td>
           </tr>
           <tr>
           <td align="center" style="font-weight:bold; height:30px;">
               组装后商品</td>
           <td style="text-align:left; " colspan="7">
            
               &nbsp;</td>
           </tr>
           <tr>
           <td align="center" colspan="8">
            
             <div id="maingrid"></div>
 
            
            </td>
           </tr>
           <tr>
           <td align="center" style="font-weight:bold;">
               被组装商品</td>
           <td style="text-align:left; " colspan="7">
            
               &nbsp;</td>
           </tr>
           <tr>
           <td align="center" colspan="8">
            
              <div id="maingridsub"></div>
            
            </td>
           </tr>
           </table>
 


  
 
           
           
           
    </form>
</body>
</html>

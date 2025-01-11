<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="kaoqinList.aspx.cs" Inherits="Lanwei.Weixin.UI.kaoqin.kaoqinList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <title>考勤记录查询</title>

       <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
     

    <script src="js/kaoqinList.js" type="text/javascript"></script>


</head>
<body style="padding-left:10px; padding-top:10px;">
    <form id="form1" runat="server">
   <table id="form" border="0" cellpadding="0" cellspacing="0" style="width:100%; line-height:40px;">
           <tr>
           <td style="text-align:center; width:80px;">
            
            选择员工： 
            
            
            </td>
           <td style="text-align:left; width:250px;">
            
             
               <asp:TextBox ID="txtUserList" runat="server"></asp:TextBox>
            
             
            
             
            
            </td>
           <td style="text-align:right; width:70px;">
            
               开始日期：
            
              </td>
           <td style="text-align:left; width:110px;">
            
           <asp:TextBox ID="txtDateStart" runat="server" ltype="date" validate="{required:true}"></asp:TextBox>
            
            
           </td>
           <td style="text-align:right; width:70px;">
            
            
               结束日期：
             
             
            
           
           </td>
            <td style="text-align:left; width:110px;">
            
            <asp:TextBox ID="txtDateEnd" runat="server" ltype="date" validate="{required:true}"></asp:TextBox> 
            
            
            
             
            
            </td>
           <td style="text-align:center;width:50px;">
           
             类别
            
            </td>

                <td style="text-align:center;width:100px;">
           
                    <asp:DropDownList ID="ddlTypeList" runat="server">

                          <asp:ListItem Text="全部" Value="全部"></asp:ListItem>
                <asp:ListItem Text="上班打卡" Value="上班打卡"></asp:ListItem>
                <asp:ListItem Text="下班打卡" Value="下班打卡"></asp:ListItem>
                <asp:ListItem Text="外出打卡" Value="外出打卡"></asp:ListItem>

                    </asp:DropDownList>
            
            </td>

                <td style="text-align:center;width:50px;">
           
             状态
            
            </td>

                <td style="text-align:center;width:100px;">
           
             <asp:DropDownList ID="ddlState" runat="server" >
                <asp:ListItem Text="全部" Value="全部"></asp:ListItem>
                <asp:ListItem Text="正常" Value="正常"></asp:ListItem>
                <asp:ListItem Text="异常" Value="异常"></asp:ListItem>

            </asp:DropDownList>
            
            </td>
           <td style="text-align:right; padding-right:20px;">
           
          
           
               
           <input id="btnSearch" type="button" value="查询" class="ui-btn ui-btn-sp mrb" onclick="search()" />
       
            
            
            
                   
            </td>
           </tr>
           <tr>
           <td style="text-align:left; height:300px;" colspan="11">
            
            <div id="maingrid"></div>  
            <div style="display:none;">
   
</div>
            
            </td>
           </tr>
           </table>

    </form>
</body>
</html>

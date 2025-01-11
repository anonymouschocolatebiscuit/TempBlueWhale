<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeixinAPP.aspx.cs" Inherits="LanweiWeb.BaseSet.WeixinAPP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>系统设置</title>
  <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
    <script type="text/javascript">
    
 
      $(function ()
        {
        
          var form = $("#form").ligerForm();
          
          var txtAppURL = $.ligerui.get("txtAppURL");
          txtAppURL.set("Width", 500);
            
          var txtAdminURL = $.ligerui.get("txtAdminURL");
          txtAdminURL.set("Width", 500);


            var txtAppName =  $.ligerui.get("txtAppName");
            txtAppName.set("Width", 500);
            
            
            var txtMchId =  $.ligerui.get("txtMchId");
            txtMchId.set("Width", 500);

            var txtWeixinId = $.ligerui.get("txtWeixinId");
            txtWeixinId.set("Width", 500);
            
            
            var txtAppID =  $.ligerui.get("txtAppID");
            txtAppID.set("Width", 500);
            
             var txtAppSecret =  $.ligerui.get("txtAppSecret");
            txtAppSecret.set("Width", 500);
            
            var txtAppKeys =  $.ligerui.get("txtAppKeys");
            txtAppKeys.set("Width", 500);
            
            
              var txtSendUrl =  $.ligerui.get("txtSendUrl");
            txtSendUrl.set("Width", 500);
            
             var txtPayUrl =  $.ligerui.get("txtPayUrl");
            txtPayUrl.set("Width", 500);
            
            var txtNotifyUrl =  $.ligerui.get("txtNotifyUrl");
            txtNotifyUrl.set("Width", 500);
            
            
            
          
          });
          
        </script>
    
  
</head>
<body style="padding-top:10px;">
   
    <form id="form1" runat="server">
   
   
   
   <table id="form" style=" line-height:35px;" >

         
           <tr>
                <td style="width:100px;text-align:right;">
                    订货端接口：</td>
                <td style="width:600px;">
                    <asp:TextBox ID="txtAppURL" runat="server"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="width:100px;text-align:right;">
                  管理端接口：</td>
                <td style="width:600px;">
                
                    <asp:TextBox ID="txtAdminURL" runat="server"></asp:TextBox>  
                
                </td>
            </tr>

         
            <tr>
                <td style="width:100px;text-align:right;">
                    平台名称：</td>
                <td style="width:600px;">
                    <asp:TextBox ID="txtAppName" runat="server"></asp:TextBox>
                </td>
            </tr>


         

        <tr>
                <td style="width:100px;text-align:right;">
                    原始Id：</td>
                <td style="width:600px;">
                    <asp:TextBox ID="txtWeixinId" runat="server"></asp:TextBox>
                </td>
            </tr>
            

            <tr>
                <td style="text-align:right;">
                    AppID：</td>
                <td>
                    <asp:TextBox ID="txtAppID" runat="server"></asp:TextBox>
                                                   </td>
            </tr>
            <tr>
               <td style="text-align:right;">
                    AppSecret：</td>
               <td>
                    <asp:TextBox ID="txtAppSecret" runat="server"></asp:TextBox>
                                                   </td>
            </tr>

          <tr>
                <td style="width:100px;text-align:right;">
                    MchId：</td>
                <td style="width:600px;">
                    <asp:TextBox ID="txtMchId" runat="server"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="text-align:right;">
                    AppKey：</td>
                <td>
                    <asp:TextBox ID="txtAppKeys" runat="server"></asp:TextBox>
                                                   </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    SendUrl：</td>
                <td>
                
                
                   <asp:TextBox ID="txtSendUrl" runat="server"></asp:TextBox>
                    
                    
                    </td>
            </tr>
            <tr>
               <td style="text-align:right;">
                    PayUrl：</td>
                <td>
                 
                 <asp:TextBox ID="txtPayUrl" runat="server"></asp:TextBox>
                 
                 </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    NotifyUrl：</td>
               <td>
                   
                   
                    <asp:TextBox ID="txtNotifyUrl" runat="server"></asp:TextBox>
                   
                   </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnSave" runat="server" CssClass="ui-btn ui-btn-sp mrb"
                        onclick="btnSave_Click" Text="保 存" />
                                                   </td>
            </tr>
            </table>
   
   
   
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DingdingSet.aspx.cs" Inherits="LanweiWeb.BaseSet.DingdingSet" %>

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
          
            
         
            
            
            var txtAppID =  $.ligerui.get("txtAppID");
            txtAppID.set("Width", 500);
            
             var txtAppSecret =  $.ligerui.get("txtAppSecret");
            txtAppSecret.set("Width", 500);
            
   
            
          
          });
          
        </script>
    
  
</head>
<body style="padding-top:10px;">
   
    <form id="form1" runat="server">
   
   
   
   <table id="form" style=" line-height:55px;" >
            <tr>
                <td style="width:100px;text-align:right;">
                    关联应用：</td>
                <td style="width:600px;">
               
                         <asp:DataList ID="dlList" runat="server" RepeatDirection="Horizontal" RepeatColumns="10">

                        

                        <ItemTemplate>

                            <table border="0" width="150px" style="text-align:center; line-height:30px;">
                                <tr>
                                    <td align="center">   
                                         <asp:Image ID="Image1" runat="server" ImageUrl='<%#Eval("round_logo_url")%>' Width="60px" Height="60px"/>

                                    </td>
                                </tr>
                                 <tr>
                                    <td>ID:<%# Eval("agentid") %>  

                                         <asp:HiddenField ID="hidId" Value='<%#Eval("agentid")%>' runat="server" />

                                         <asp:HiddenField ID="hidName" Value='<%#Eval("name")%>' runat="server" />

                                    </td>
                                </tr>
                                 <tr>
                                    <td>名称：<%# Eval("name") %> </td>
                                </tr>
                                 <tr>
                                    <td><asp:RadioButton ID="appId" runat="server" GroupName="sss" /> </td>
                                </tr>


                            </table>

  
    
                        </ItemTemplate>



                    </asp:DataList>
                </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    corpId：</td>
                <td>
                    <asp:TextBox ID="txtAppID" runat="server"></asp:TextBox>
                                                   </td>
            </tr>
            <tr>
               <td style="text-align:right;">
                    corpSecret：</td>
               <td>
                    <asp:TextBox ID="txtAppSecret" runat="server"></asp:TextBox>
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

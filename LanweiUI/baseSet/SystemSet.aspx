<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemSet.aspx.cs" Inherits="LanweiWeb.BaseSet.SystemSet" %>

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
          
            var TextBox1 =  $.ligerui.get("TextBox1");
            TextBox1.set("Width", 350);
            
             var TextBox2 =  $.ligerui.get("TextBox2");
            TextBox2.set("Width", 350);
            
             var TextBox3 =  $.ligerui.get("TextBox3");
            TextBox3.set("Width", 350);
            
             var TextBox4 =  $.ligerui.get("TextBox4");
            TextBox4.set("Width", 350);
            
             var TextBox5 =  $.ligerui.get("TextBox5");
            TextBox5.set("Width", 350);
            
            var txtCorpIdQY = $.ligerui.get("txtCorpIdQY");
            txtCorpIdQY.set("Width", 350);
            var txtCorpSecretQY = $.ligerui.get("txtCorpSecretQY");
            txtCorpSecretQY.set("Width", 350);
            var txtCorpIdDD = $.ligerui.get("txtCorpIdDD");
            txtCorpIdDD.set("Width", 350);
            var txtCorpSecretDD = $.ligerui.get("txtCorpSecretDD");
            txtCorpSecretDD.set("Width", 350);

            var txtUserSecret = $.ligerui.get("txtUserSecret");
            txtUserSecret.set("Width", 350);

            var txtCheckInSecret = $.ligerui.get("txtCheckInSecret");
            txtCheckInSecret.set("Width", 350);

            var txtApplySecret = $.ligerui.get("txtApplySecret");
            txtApplySecret.set("Width", 350);

            //-------------------------------------------------
            var txtSecretBuy = $.ligerui.get("txtSecretBuy");
            txtSecretBuy.set("Width", 350);

            var txtSecretSales = $.ligerui.get("txtSecretSales");
            txtSecretSales.set("Width", 350);

            var txtSecretStore = $.ligerui.get("txtSecretStore");
            txtSecretStore.set("Width", 350);

            var txtSecretFee = $.ligerui.get("txtSecretFee");
            txtSecretFee.set("Width", 350);

            var txtSecretReport = $.ligerui.get("txtSecretReport");
            txtSecretReport.set("Width", 350);

            

            var txtSecretCheckIn = $.ligerui.get("txtSecretCheckIn");
            txtSecretCheckIn.set("Width", 350);

            var txtSecretApply = $.ligerui.get("txtSecretApply");
            txtSecretApply.set("Width", 350);
          
          });
          
        </script>
    
  
</head>
<body style="padding-top:10px;">
   
    <form id="form1" runat="server">
   
   
   
   <table id="form" style=" line-height:35px;" >
            <tr>
                <td style="width:100px;text-align:right;">
                    公司名称：</td>
                <td style="width:400px;">
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                   </td>
            </tr>
            <tr>
                <td style="width:100px;text-align:right;">
                    公司地址：</td>
                <td style="width:400px;">
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                   </td>
            </tr>
            <tr>
                <td style="width:100px;text-align:right;">
                    联系电话：</td>
                <td style="width:400px;">
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                   </td>
            </tr>
            <tr>
                <td style="width:100px;text-align:right;">
                    联系传真：</td>
                <td style="width:400px;">
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                                   </td>
            </tr>
            <tr>
                <td style="width:100px;text-align:right;">
                    邮政编码：</td>
                <td style="width:400px;">
                    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                                                   </td>
            </tr>
            <tr>
                <td style="width:100px;text-align:right;">
                    是否启用审核：</td>
                <td style="width:400px;">
                    <asp:CheckBox ID="cbBillCheckList" runat="server" 
                        Text="是（启用后、单据审核后报表数据才发生变化）" />
                                                   </td>
            </tr>
            <tr>
                <td style="width:100px;text-align:right;">
                    是否检查负库存：</td>
                <td style="width:400px;">
                    <asp:CheckBox ID="cbStoreNum" runat="server" Text="是（勾选后、负库存不能进行出库）" />
                                                   </td>
            </tr>
            <tr>
                <td style="width:100px;text-align:right;">
                    打印LOGO：</td>
                <td style="width:400px;">
                    <asp:CheckBox ID="cbPrintLogo" runat="server" 
                        Text="是（勾选后、单据打印显示公司LOGO）" />
                                                   </td>
            </tr>
            <tr>
                <td style="width:100px;text-align:right;">
                    打印电子印章：</td>
                <td style="width:400px;">
                    <asp:CheckBox ID="cbPrintZhang" runat="server" 
                        Text="是（勾选后、单据打印显示电子印章）" />
                                                   </td>
            </tr>
            <tr>
                <td style="width:100px;text-align:right;">
                    商品字段</td>
                <td style="width:400px;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width:100px;text-align:right;">
                    自定义字段1：</td>
                <td style="width:400px;">
                    <asp:TextBox ID="txtFieldA" runat="server"></asp:TextBox>
                                                   </td>
            </tr>
            <tr>
                <td style="width:100px;text-align:right;">
                    自定义字段2：</td>
                <td style="width:400px;">
                    <asp:TextBox ID="txtFieldB" runat="server"></asp:TextBox>
                                                   </td>
            </tr>
            <tr>
                <td style="width:100px;text-align:right;">
                    自定义字段3：</td>
                <td style="width:400px;">
                    <asp:TextBox ID="txtFieldC" runat="server"></asp:TextBox>
                                                   </td>
            </tr>
            <tr>
                <td style="width:100px;text-align:right;">
                    自定义字段4：</td>
                <td style="width:400px;">
                    <asp:TextBox ID="txtFieldD" runat="server"></asp:TextBox>
                                                   </td>
            </tr>
            <tr>
                <td style="width:100px;text-align:right;">
                    企业微信参数</td>
                <td style="width:400px;">
                 
                  
                 
                </td>
            </tr>
            <tr>
                <td style="width:100px;text-align:right;">
                    CorpId：</td>
                <td style="width:400px;">
                     <asp:TextBox ID="txtCorpIdQY" runat="server"></asp:TextBox>   
                
                </td>
            </tr>
            <tr>
                <td style="width:100px;text-align:right;">
                    管理组Secret：</td>
                <td style="width:400px;">
                    <asp:TextBox ID="txtCorpSecretQY" runat="server"></asp:TextBox>       
                   
                </td>
            </tr>
            <tr>
                <td style="width:100px;text-align:right;">
                    通讯录Secret：</td>
                <td style="width:400px;">

                   <asp:TextBox ID="txtUserSecret" runat="server"></asp:TextBox>  



                </td>
            </tr>
            <tr>
                <td style="width:100px;text-align:right;">
                    打卡Secret：</td>
                <td style="width:400px;">
                
                     <asp:TextBox ID="txtCheckInSecret" runat="server"></asp:TextBox>      
                
                </td>
            </tr>
            <tr>
                <td style="width:100px;text-align:right;">
                    审批Secret：</td>
                <td style="width:400px;">

                    <asp:TextBox ID="txtApplySecret" runat="server"></asp:TextBox>  


                </td>
            </tr>
          
       
            <tr>
                <td style="text-align:right;" colspan="2">
                    <hr />
                </td>
            </tr>
          
         <tr>
                <td style="width:100px;text-align:right;">
                    考勤Secret：</td>
                <td style="width:400px;">

                    <asp:TextBox ID="txtSecretCheckIn" runat="server"></asp:TextBox>  


                </td>
            </tr>

         <tr>
                <td style="width:100px;text-align:right;">
                    审批Secret：</td>
                <td style="width:400px;">

                    <asp:TextBox ID="txtSecretApply" runat="server"></asp:TextBox>  


                </td>
            </tr>
       
            <tr>
                <td style="width:100px;text-align:right;">
                    采购Secret：</td>
                <td style="width:400px;">

                    <asp:TextBox ID="txtSecretBuy" runat="server"></asp:TextBox>  


                </td>
            </tr>


                 <tr>
                <td style="width:100px;text-align:right;">
                    销售Secret：</td>
                <td style="width:400px;">

                    <asp:TextBox ID="txtSecretSales" runat="server"></asp:TextBox>  


                     </td>
            </tr>
        <tr>
                <td style="width:100px;text-align:right;">
                    库存Secret：</td>
                <td style="width:400px;">

                    <asp:TextBox ID="txtSecretStore" runat="server"></asp:TextBox>  


                </td>
            </tr>
        <tr>
                <td style="width:100px;text-align:right;">
                    财务Secret：</td>
                <td style="width:400px;">

                    <asp:TextBox ID="txtSecretFee" runat="server"></asp:TextBox>  


                </td>
            </tr>


       <tr>
                <td style="width:100px;text-align:right;">
                    报表Secret：</td>
                <td style="width:400px;">

                    <asp:TextBox ID="txtSecretReport" runat="server"></asp:TextBox>  


                </td>
            </tr>


        <tr>
                <td style="text-align:right;" colspan="2">
                    <hr />
                </td>
            </tr>


            <tr>
                <td style="width:100px;text-align:right;">
                    阿里钉钉参数</td>
                <td style="width:400px;">
                   
                
                </td>
            </tr>
            <tr>
                <td style="width:100px;text-align:right;">
                    CorpId：</td>
                <td style="width:400px;">
                     <asp:TextBox ID="txtCorpIdDD" runat="server"></asp:TextBox>    
                
                </td>
            </tr>
            <tr>
                <td style="width:100px;text-align:right;">
                    CorpSecret：</td>
                <td style="width:400px;">
                    
                     <asp:TextBox ID="txtCorpSecretDD" runat="server"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td style="width:150px;text-align:right;">
                    &nbsp;</td>
                <td style="width:400px;">
                    <asp:Button ID="btnSave" runat="server" CssClass="ui-btn ui-btn-sp mrb"
                        onclick="btnSave_Click" Text="保 存" />
                                                   </td>
            </tr>
            </table>
   
   
   
    </form>
</body>
</html>

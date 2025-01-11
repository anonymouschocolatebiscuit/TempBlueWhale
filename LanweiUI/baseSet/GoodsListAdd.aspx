<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsListAdd.aspx.cs" Inherits="Lanwei.Weixin.UI.BaseSet.GoodsListAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>商品信息</title>
      
      <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
     

     
     <script type="text/javascript"> 
     
        var dialog = frameElement.dialog; //调用页面的dialog对象(ligerui对象)
       
         
         $(function() {

         //创建表单结构
         var form = $("#form").ligerForm();

          var g =  $.ligerui.get("txtNames");
            g.set("Width", 490);
        
         
         });

      

      
        function closeDialog()
        {
            var dialog = frameElement.dialog; //调用页面的dialog对象(ligerui对象)
            dialog.close();//关闭dialog 
        }
        
    </script>
     
     
    <style type="text/css">
           body{ font-size:12px;}
        .l-table-edit {}
        .l-table-edit-td{ padding:4px;}
        .l-button-submit,.l-button-test{width:80px; float:left; margin-left:10px; padding-bottom:2px;}
        .l-verify-tip{ left:230px; top:120px;}
    </style>
    
    
      
    
    
</head>
<body style="font-size:10pt;">
    <form id="form1" runat="server">
    
    <table id="form" border="0" cellpadding="0" cellspacing="20" style="line-height:45px;">
            <tr>
                <td style="width:80px;text-align:right;">
                    商品编码：</td>
                <td style="width:150px;">
                    <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
                    </td>
                <td style="width:80px; text-align:right;">
                    商品条码：</td>
                <td style="width:150px;">
                    <asp:TextBox ID="txtBarcode" runat="server"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    商品名称：</td>
                <td colspan="3">
                    <asp:TextBox ID="txtNames" runat="server"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    商品类别：</td>
                <td>
                    <asp:DropDownList ID="ddlVenderTypeList" runat="server">
                    </asp:DropDownList>
                    </td>
                <td align="right">
                    品牌：</td>
                <td>
                    <asp:DropDownList ID="ddlBrandList" runat="server">
                    </asp:DropDownList>
                    </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    规格：</td>
                <td>
                    <asp:TextBox ID="txtSpec" runat="server"></asp:TextBox>
                    </td>
                <td align="right">
                    计量单位：</td>
                <td>
                    <asp:DropDownList ID="ddlUnitList" runat="server">
                    </asp:DropDownList>
                    </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    首选仓库：</td>
                <td>
                    <asp:DropDownList ID="ddlCangkuList" runat="server">
                    </asp:DropDownList>
                    </td>
                <td align="right">
                    产地：</td>
                <td>
                    <asp:TextBox ID="txtPlace" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    <asp:Label ID="lbFieldA" runat="server" Text="Label"></asp:Label>
                    ：</td>
                <td>
                    <asp:TextBox ID="txtFieldA" runat="server"></asp:TextBox>
                    </td>
                <td align="right">
                    <asp:Label ID="lbFieldB" runat="server" Text="Label"></asp:Label>
                    ：</td>
                <td>
                    <asp:TextBox ID="txtFieldB" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    <asp:Label ID="lbFieldC" runat="server" Text="Label"></asp:Label>
                    ：</td>
                <td>
                    <asp:TextBox ID="txtFieldC" runat="server"></asp:TextBox>
                    </td>
                <td align="right">
                    <asp:Label ID="lbFieldD" runat="server" Text="Label"></asp:Label>
                    ：</td>
                <td>
                    <asp:TextBox ID="txtFieldD" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    采购价：</td>
                <td>
                    <asp:TextBox ID="txtPriceCost" runat="server"></asp:TextBox>
                    </td>
                <td align="right">
                    提成比例：</td>
                <td>
                    <asp:TextBox ID="txtTichengRate" runat="server"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td style="text-align:right;">
                    批发价：</td>
                <td>
                    <asp:TextBox ID="txtPriceSalesWhole" runat="server"></asp:TextBox>
                    </td>
                <td align="right">
                    零售价：</td>
                <td>
                    <asp:TextBox ID="txtPriceSalesRetail" runat="server"></asp:TextBox>
                    </td>
            </tr>
            
            <tr>
                <td style="text-align:right;">
                    最低库存：</td>
                <td>
                    <asp:TextBox ID="txtNumMin" runat="server"></asp:TextBox>
                    </td>
                <td align="right">
                    最高库存：</td>
                <td>
                    <asp:TextBox ID="txtNumMax" runat="server"></asp:TextBox>
                    </td>
            </tr>
            
            <tr>
                <td style="text-align:right;">
                    显示类别：</td>
                <td>
                  
                    
                    <asp:RadioButtonList ID="rbNoteList" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="默认" Value="默认" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="推荐" Value="推荐"></asp:ListItem>
                    <asp:ListItem Text="特价" Value="特价"></asp:ListItem>
                    <asp:ListItem Text="新款" Value="新款"></asp:ListItem>
                    <asp:ListItem Text="热销" Value="热销"></asp:ListItem>
                    <asp:ListItem Text="赠品" Value="赠品"></asp:ListItem>
                    <asp:ListItem Text="缺货" Value="缺货"></asp:ListItem>
                    
                    </asp:RadioButtonList>
                    
                    
                    
                </td>
                <td align="right">
                    立即上架：</td>
                <td>
                    <asp:CheckBox ID="cbShow" runat="server" Text="勾选后显示在微信订货平台" />
                    </td>
            </tr>
            
            <tr>
                <td style="text-align:right;">
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnSave" runat="server" class="ui_state_highlight" 
               Text="保 存" onclick="btnSave_Click" />
                             &nbsp;
                   <input id="btnCancel" class="ui-btn" type="button" value="关闭" onclick="closeDialog()" />
                    
                    
                    </td>
                <td>
                    <asp:HiddenField ID="hfImagePath" runat="server" />
                </td>
                <td>
                    <asp:HiddenField ID="hf" runat="server" />
                </td>
            </tr>
            </table>
     
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogoSet.aspx.cs" Inherits="Lanwei.Weixin.UI.baseSet.LogoSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>修改LOGO印章</title>
    
        
</head>
<body>
    <form id="form1" runat="server">
   <div align="center">
		
                                <table border="0" cellpadding="2" cellspacing="1" style="width:100%;" >
						
						
                                                       
							<tr>
								<td align="center" height="30" 
                                    colspan="2">修改LOGO印章图片</td>
							</tr>
							<tr>
								<td  align="center" 
                                     colspan="2">
    
        替换图片：<asp:RadioButton ID="RB1" runat="server" Checked="True" GroupName="id" 
            Text="LOGO" />
&nbsp;
        <asp:RadioButton ID="RB2" runat="server" GroupName="id" Text="印章" />
&nbsp;
        </td>
							</tr>
						
							<tr>
								<td align="center" colspan="2">
    
   
        选择图片：<asp:FileUpload ID="ProductImg" runat="server" Width="194px" />
&nbsp;<asp:Button ID="ButtonUpload" runat="server" onclick="ButtonUpload_Click" 
            Text="上传" />
                                </td>
							</tr>
						
							<tr>
								<td style="width:100px;">
                                    LOGO（60*60）</td>
								<td>
    <asp:Image ID="Image1" runat="server"  Height="60px" Width="60px" ImageUrl="../sales/pdf/logo100.jpg" />
                                </td>
							</tr>
						
							<tr>
								<td style="width:100px;">
                                    印章（180*180）</td>
								<td>
    <asp:Image ID="Image2" runat="server"  Height="180px" Width="180px" ImageUrl="../sales/pdf/zhang.jpg" />
                                            </td>
							</tr>
						
							<tr>
								<td colspan="2">
                                    &nbsp;</td>
							</tr>
						
					</table>
                        </div>
    </form>
</body>
</html>

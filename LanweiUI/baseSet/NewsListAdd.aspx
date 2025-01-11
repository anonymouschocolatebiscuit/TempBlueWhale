<%@ Page Language="C#" AutoEventWireup="true" validateRequest="false" CodeBehind="NewsListAdd.aspx.cs" Inherits="Lanwei.Weixin.UI.baseSet.NewsListAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>新闻发布</title>
    
   
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script src="../UEditor/editor_config.js" type="text/javascript"></script>

    <script src="../UEditor/editor_all_min.js" type="text/javascript"></script>

    <link href="../UEditor/themes/default/ueditor.css" rel="stylesheet" type="text/css" />
    
     
    
    
    
</head>
<body>
    <form id="form1" runat="server">
    

    <table border="0" cellpadding="0" cellspacing="0" style="width:640px; line-height:40px;">
        
    
    <tr>
    <td>
    
                      <script type="text/javascript">
    var editor = new baidu.editor.ui.Editor({ id: 'editor', minFrameHeight: 600 }); ;editor.render("txtNeirong");
    </script>
    
    
                      新闻标题：<asp:TextBox ID="txtTitle" runat="server" Width="389px"></asp:TextBox>
                      （标题必填）</td>
    
    </tr>
    
    
    <tr>
    <td>
    
        新闻类别：<asp:DropDownList ID="ddlTypeName" runat="server">
            <asp:ListItem>促销信息</asp:ListItem>
            <asp:ListItem>新品资讯</asp:ListItem>
            <asp:ListItem>信息公告</asp:ListItem>
        </asp:DropDownList>
    
    
    </td>
    
    </tr>
    
    
    <tr>
    <td align="center" valign="middle" style="height:50px;">
    
     <asp:TextBox ID="txtNeirong" runat="server" Width="640px" Rows="10" 
                    TextMode="MultiLine"></asp:TextBox>
    
    
    </td>
    
    </tr>
    
    
    <tr>
    <td align="center" valign="middle" style="height:50px;">
    
        <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="保 存"  />
    
    
    </td>
    
    </tr>
    
    
    </table>
    
   
<script type="text/javascript">
    //实例化编辑器
    var um = UM.getEditor('myEditor');
    um.addListener('blur',function(){
        $('#focush2').html('编辑器失去焦点了')
    });
    um.addListener('focus',function(){
        $('#focush2').html('')
    });
    //按钮的操作
    function insertHtml() {
        var value = prompt('插入html代码', '');
        um.execCommand('insertHtml', value)
    }
    function isFocus(){
        alert(um.isFocus())
    }
    
    
    function getValue()
    {
        //UM.getEditor('myEditor').getPlainTxt()
        //alert(UM.getEditor('myEditor').getPlainTxt());
        
        document.getElementById("hfValue").value=document.getElementById("txtNeirong").value; //UM.getEditor('myEditor').getContent();
        
        alert(document.getElementById("txtNeirong").value);
        //alert(document.getElementById("hfValue").value);
        
        if(document.getElementById("hfValue").value=="")
        {
           alert("请输入内容！");
           setFocus();
           return false;
        }
        else
        {
           return true;
        }
        
    }
    
    function doBlur(){
        um.blur()
    }
    function createEditor() {
        enableBtn();
        um = UM.getEditor('myEditor');
    }
    function getAllHtml() {
        alert(UM.getEditor('myEditor').getAllHtml())
    }
    function getContent() {
        var arr = [];
        arr.push("使用editor.getContent()方法可以获得编辑器的内容");
        arr.push("内容为：");
        arr.push(UM.getEditor('myEditor').getContent());
        alert(arr.join("\n"));
    }
    function getPlainTxt() {
        var arr = [];
        arr.push("使用editor.getPlainTxt()方法可以获得编辑器的带格式的纯文本内容");
        arr.push("内容为：");
        arr.push(UM.getEditor('myEditor').getPlainTxt());
        alert(arr.join('\n'))
    }
    function setContent(isAppendTo) {
        var arr = [];
        arr.push("使用editor.setContent('欢迎使用umeditor')方法可以设置编辑器的内容");
        UM.getEditor('myEditor').setContent('欢迎使用umeditor', isAppendTo);
        alert(arr.join("\n"));
    }
    function setDisabled() {
        UM.getEditor('myEditor').setDisabled('fullscreen');
        disableBtn("enable");
    }

    function setEnabled() {
        UM.getEditor('myEditor').setEnabled();
        enableBtn();
    }

    function getText() {
        //当你点击按钮时编辑区域已经失去了焦点，如果直接用getText将不会得到内容，所以要在选回来，然后取得内容
        var range = UM.getEditor('myEditor').selection.getRange();
        range.select();
        var txt = UM.getEditor('myEditor').selection.getText();
        alert(txt)
    }

    function getContentTxt() {
        var arr = [];
        arr.push("使用editor.getContentTxt()方法可以获得编辑器的纯文本内容");
        arr.push("编辑器的纯文本内容为：");
        arr.push(UM.getEditor('myEditor').getContentTxt());
        alert(arr.join("\n"));
    }
    function hasContent() {
        var arr = [];
        arr.push("使用editor.hasContents()方法判断编辑器里是否有内容");
        arr.push("判断结果为：");
        arr.push(UM.getEditor('myEditor').hasContents());
        alert(arr.join("\n"));
    }
    function setFocus() {
        UM.getEditor('myEditor').focus();
    }
    function deleteEditor() {
        disableBtn();
        UM.getEditor('myEditor').destroy();
    }
    function disableBtn(str) {
        var div = document.getElementById('btns');
        var btns = domUtils.getElementsByTagName(div, "button");
        for (var i = 0, btn; btn = btns[i++];) {
            if (btn.id == str) {
                domUtils.removeAttributes(btn, ["disabled"]);
            } else {
                btn.setAttribute("disabled", "true");
            }
        }
    }
    function enableBtn() {
        var div = document.getElementById('btns');
        var btns = domUtils.getElementsByTagName(div, "button");
        for (var i = 0, btn; btn = btns[i++];) {
            domUtils.removeAttributes(btn, ["disabled"]);
        }
    }
</script>


    
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsPriceClientType.aspx.cs" Inherits="Lanwei.Weixin.UI.baseSet.GoodsPriceClientType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>客户等级设置</title>
  <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
    <script src="../lib/json2.js" type="text/javascript"></script>
     
    
       <script type="text/javascript">
      
        var manager;
        $(function ()
        {
            manager = $("#maingrid").ligerGrid({
                columns: [
                
                 
                 { display: '等级名称', name: 'names',id: 'names', width: 250, align: 'left' },
                 
                { display: '单价', name: 'price', width: 150, type: 'float', align: 'right', editor: { type: 'float'}
                
                }
                
            
                ], width: '550', pageSizeOptions: [5, 10, 15, 20],height: '300',
               
                url: 'GoodsPriceClientType.aspx?Action=GetDataList&id='+getUrlParam("id"), 
                alternatingRow: false,  
                enabledEdit: true,//控制能否编辑的
                usePager:false,             
                rownumbers: true

                
                
            }
            );
        });

        function save()
        {
               
             var goodsId=getUrlParam("id");
              
              
             var data = manager.getData();
     
     
                
                 var headJson={goodsId:goodsId};
      
    
        
        var dataNew = [];
        dataNew.push(headJson);
        
   
        
        var list=JSON.stringify(headJson);
        
        
        var goodsList=[];
        
        
   
        
        list=list.substring(0,list.length-1);//去掉最后一个花括号
        
        list+=",\"Rows\":";
        list+=JSON.stringify(data);      
        list+="}";
       
      
        
        var postData=JSON.parse(list);//最终的json
        
        
                
          $.ajax({
            type: "POST",
            url: 'ashx/GoodsPriceClientType.ashx',
            contentType: "application/json", //必须有
            //dataType: "json", //表示返回值类型，不必须
            data:JSON.stringify(postData),  //相当于 //data: "{'str1':'foovalue', 'str2':'barvalue'}",
            success: function (jsonResult) {
                
                if(jsonResult=="操作成功！")
                {
                
                    $.ligerDialog.waitting('操作成功！'); setTimeout(function () { $.ligerDialog.closeWaitting();location.reload();}, 2000);
                    
                }
                else
                {
                   $.ligerDialog.warn(jsonResult);
                   
                }
            },
            error: function (xhr) {
                alert("出现错误，请稍后再试:" + xhr.responseText);
            }
        });
        
        
           
        }


        function deleteRow() {

         
            var goodsId = getUrlParam("id");

            $.ligerDialog.confirm('清除后不能恢复，确认删除？', function (type) {

                if (type) {

                    $.ajax({
                        type: "GET",
                        url: "GoodsPriceClientType.aspx",
                        data: "Action=clear&id=" + goodsId + "&ranid=" + Math.random(), //encodeURI
                        success: function (resultString) {
                            $.ligerDialog.alert(resultString, '提示信息');
                            reload();

                        },
                        error: function (msg) {

                            $.ligerDialog.alert("网络异常，请联系管理员", '提示信息');
                        }
                    });

                }

            });


        }

        
      var dialog = frameElement.dialog; //调用页面的dialog对象(ligerui对象)
       
         
       

      

      
        function closeDialog()
        {
            var dialog = frameElement.dialog; //调用页面的dialog对象(ligerui对象)
            dialog.close();//关闭dialog 
        }
        
        
     
        function getSelected()
        {
            var row = manager.getSelectedRow();
            if (!row) {  $.ligerDialog.warn('请选择行'); return; }
            alert(JSON.stringify(row));
        }
        function getData()
        {
            var data = manager.getData();
            alert(JSON.stringify(data));
        }

         function reload()
        {
            manager.reload();
        }
        
       
       function getUrlParam(name)
{
   var reg = new RegExp("(^|&)"+ name +"=([^&]*)(&|$)");

   var r = window.location.search.substr(1).match(reg);

   if (r!=null) return unescape(r[2]); return null;
}



         
    </script>
    <style type="text/css">
    .l-button{width: 120px; float: left; margin-left: 10px; margin-bottom:2px; margin-top:2px;}
    </style>


    
</head>
<body style="padding-left:10px; padding-top:10px;">
    <form id="form1" runat="server">
  
   <table border="0" width="550px">
   
   <tr>
   
   <td>
   
     <div id="maingrid">
    </div>
    
    
   
   </td>
   </tr>
   
   
    <tr>
   
   <td style="line-height:40px; text-align:center;padding-right:20px;">
   
    <input id="Button1" class="ui-btn ui-btn-sp mrb" type="button" value="保 存" onclick="save()"  />
    
    
     <input id="Button2" class="ui-btn ui-btn-sp mrb" type="button" value="清 除" onclick="deleteRow()"  />
    
    
   
    <input id="btnCancel" class="ui-btn" type="button" value="关 闭" onclick="closeDialog()" />
    
    
    
   
   </td>
   </tr>
   
   
   </table>
   
  
  

    </form>
</body>
</html>

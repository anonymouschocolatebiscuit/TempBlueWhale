<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsListSelect.aspx.cs" Inherits="Lanwei.Weixin.UI.baseSet.GoodsListSelect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>商品列表</title>
    
      <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
  
    <script src="../lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script>
   
    <script src="../lib/json2.js" type="text/javascript"></script>

    
    
     <script type="text/javascript">
        
        
        document.onkeydown=keyDownSearch;  
      
        function keyDownSearch(e) {    
        // 兼容FF和IE和Opera    
        var theEvent = e || window.event;    
        var code = theEvent.keyCode || theEvent.which || theEvent.charCode;    
        if (code == 13) {    
             
            
             
            $("#btnSearch").click();
                
            return false;    
        }    
        return true;    
    }  
        
        
        
        var manager = null;
        
        
        
        
        
        $(function () {
        
  
             var form = $("#formTB").ligerForm();
             
              
          var txtKeys =  $.ligerui.get("txtKeys");
         txtKeys.set("Width", 300);
            
        
           manager = $("#maingrid4").ligerGrid({
            
               checkbox:true,
                columns: [
               
              
                 { display: '商品编号', name: 'code', width: 100, align: 'center' },
                 { display: '商品名称', name: 'names', width: 150, align: 'left'},
                  { display: '规格', name: 'spec', width: 70, align: 'center' },
                  { display: '单位', name: 'unitName', width: 70, align: 'center' },
                 
            
                // { display: '保质期(天)', name: 'bzDays', width: 70, align: 'center'},
                 
                  { display: '条码', name: 'barcode', width: 100, align: 'center' }
                  
              
            
               
               
                ],  pageSize:10,
                rownumbers:true,//序号
                usePager: false,
                 url: 'GoodsListSelect.aspx?Action=GetDataList', 
                width: '690',height:'400'
            }); 
            $("#pageloading").hide();
        });
        function f_select()
        {
            return manager.getSelectedRows();
            
            
            
        }
        
          function search() {

           
                var keys = document.getElementById("txtKeys").value;
                if (keys == "请输入编号/名称/规格") {

                    keys = "";

                }
                
                //alert("GoodsListSelect.aspx?Action=GetDataListSearch&keys=" + keys + "&typeId=" + typeId);
                
                manager.changePage("first");
                manager._setUrl("GoodsListSelect.aspx?Action=GetDataListSearch&keys="+keys+"&typeId="+typeId); 
           
              
        }
        
        
        
        
         var typeId=0;
         
         $(function ()
        {   
           

            $("#tree1").ligerTree({  
            url:"GoodsTypeList.aspx?Action=GetTreeList",
      
            onSelect: onSelect,
            parentIcon:null,
            childIcon:null,
            checkbox:false,
            slide : false,
            treeLine:true,
            idFieldName :'id',
            
            
            parentIDFieldName :'pid'
            
            }
            
            );
        });

        
        
      
        function onSelect(note)
        {
          
          //  alert('onSelect:' + note.data.id);
            
            typeId=note.data.id;
            
            search();
            
            
        }
   


        
        
    </script>
    
    
    
        



    
</head>
<body>
    <form id="form1" runat="server">
   
   <table id="formTB" border="0" style="width:800px; line-height:40px;">
   
   <tr>
   <td>
       <asp:TextBox ID="txtKeys" runat="server" nullText="请输入编号/名称/规格"></asp:TextBox>
       </td>
   
   <td style="width:200px;">
       <input id="btnSearch" type="button" value="查询" class="ui-btn" onclick="search()"  /></td>
   
   </tr>
   
   
   <tr>
   <td>
   
   
    <div id="maingrid4" style="margin:0; padding:0"></div>
   
   



   
   
   
   
   </td>
   
   <td valign="top">
   
   
   
    <div style="width:200px; position:relative; height:400px; display:block; margin:10px; background:white; border:1px solid #ccc; overflow:auto;">
    <ul id="tree1">
     
    </ul>
    </div> 
 
        <div style="display:none">
     
    </div>



   
   
   
   </td>
   
   </tr>
   
   
   </table>
   
   
      

   
   
    </form>
</body>
</html>

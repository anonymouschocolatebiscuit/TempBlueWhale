    function formatCurrency(x)
        {
              
            var f_x = parseFloat(x);
            if (isNaN(f_x)) {
               // alert('function:changeTwoDecimal->parameter error');
                return "0.00";
            }
            var f_x = Math.round(x * 100) / 100;
            var s_x = f_x.toString();
            var pos_decimal = s_x.indexOf('.');
            if (pos_decimal < 0) {
                pos_decimal = s_x.length;
                s_x += '.';
            }
            while (s_x.length <= pos_decimal + 2) {
                s_x += '0';
            }
            return s_x;
            
            
        }
        
  
    
            $(document).bind('keydown.grid', function (event)
            {
                if (event.keyCode == 13 || event.keyCode == 39 || event.keyCode == 9) //enter,right arrow,tap
                { 
                   manager.endEditToNext();
                }
            });
    
        
        
        
        function f_selectContact()
        {
            $.ligerDialog.open({ title: 'Select Product', name:'winselector',width: 840, height:540, url: '../baseSet/GoodsListSelect.aspx', buttons: [
                { text: 'OK', onclick: f_selectContactOK },
                { text: 'Cancel', onclick: f_selectContactCancel }
            ]
            });
            return false;
        }        function f_selectContactOK(item, dialog)
        {
			var fn = dialog.frame.f_select || dialog.frame.window.f_select; 
            var data = fn(); 
            if (!data)
            {
                alert('Please select a row!');
                return;
            }
            
            f_onGoodsChanged(data);
                       
            dialog.close();
            
        }
        
        
        function f_selectContactCancel(item, dialog)
        {
            dialog.close();
        }
        
       
       
      
        $.ligerDefaults.Grid.formatters['numberbox'] = function (value, column) {
            var precision = column.editor.precision;
            return value.toFixed(precision);
        };
     
                    
    var manager;
        $(function ()
        {
        
          var form = $("#form").ligerForm();
          
            var g =  $.ligerui.get("ddlVenderList");
            g.set("Width", 250);
            
          
            window['g'] = 
            manager = $("#maingrid").ligerGrid({
                columns: [
                
                 { display: '', isSort: false, width: 60,align:'center',frozen:true, render: function (rowdata, rowindex, value)
                 {
                    var h = "";
                    if (!rowdata._editing)
                    {
                        h += "<a href='javascript:addNewRow()' title='Add Row' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                        h += "<a href='javascript:deleteRow()' title='Delete Row' style='float:left;'><div class='ui-icon ui-icon-trash'></div></a> "; 
                        h += "<a href='javascript:f_selectContact()' title='Select Product' style='float:left;'><div class='ui-icon ui-icon-search'></div></a> ";
                    }
                  
                    return h;
                }
                }
                ,
               
                { display: 'Product Name', name: 'goodsName', width: 250, align: 'left',
                
                   totalSummary:
                    {
                        type: 'count',
                        render: function (e) 
                        {  
                            return 'Total：';
                        }
                    }
                  
                
                },
                
                { display: 'Specification', name: 'spec',width: 120, align: 'center' },
                { display: 'Unit', name: 'unitName',width: 100, align: 'center'},
                
                { display: 'Quantity', name: 'num', width: 100, type: 'float', align: 'right',editor: { type: 'float' },
                
                   totalSummary:
                    {
                        align: 'right',   //left/center/right 
                        type: 'sum',
                        render: function (e) 
                        {  
                            return  Math.round(e.sum*100)/100;
                        }
                    }
                
                },
                
                { display: 'Inbound Unit Price', name: 'price', width: 100, type: 'float', align: 'right', editor: { type: 'float', precision: 4 }
                
                },

             
                
                
                { display: 'Amount', name: 'sumPrice', width: 100, type: 'float', align: 'right',editor: { type: 'float' },
                
                 
                
                  totalSummary:
                    {
                        align: 'center',   //left/center/right 
                        type: 'sum',
                        render: function (e) 
                        {  
                         
                            
                             var itemSumPrice=e.sum;
                            
                        
                          
                        
                          　return "<span id='sumPriceItem'>"+Math.round(itemSumPrice*10000)/10000+"</span>";//formatCurrency(suminf.sum)
                            
                            
                        }
                    }
                
                
                },
                
                
             
                { display: 'Warehouse', name: 'ckId', width: 100, isSort: false,textField:'ckName',
                    editor: { type: 'select',
                              url:"../baseSet/StorageList.aspx?Action=GetDDLList&r=" + Math.random(), 
                              valueField: 'ckId',textField:'ckName'}

                },
                
                { display: 'Remarks', name: 'remarks', width: 150, align: 'left',type:'text',editor: { type: 'text' } }
                ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '450',
               url: 'OtherInListEdit.aspx?Action=GetData&id='+getUrlParam("id"),//param
               rownumbers:true,
               frozenRownumbers:true,
                dataAction: 'local',
                usePager:false,
                alternatingRow: false,
                
                totalSummary:true,
                enabledEdit: true, 
               // onBeforeEdit: f_onBeforeEdit,
               // onBeforeSubmitEdit: f_onBeforeSubmitEdit,
                
                //totalRender:f_totalRender,
                
                onAfterEdit: f_onAfterEdit 
            }
            );
        });
 
        var rowNumber=9;
        
       // alert(getUrlParam("id"));
 
        function f_totalRender(data, currentPageData)
        {
             //return "Total number of warehouses: "+data.sumPriceAll; 
        }

        function setStorage()
        {
        
            $.ligerDialog.open({ target: $("#target1") });
            
            // $.ligerDialog.open({ url: '../../welcome.htm', height: 250,width:null, buttons: [ { text: 'OK', onclick: function (item, dialog) { alert(item.text); } }, { text: 'Cancel', onclick: function (item, dialog) { dialog.close(); } } ] });
        }
        
        function selectStorage()
        {
              var ckName=$("#ddlStorageList").find("option:selected").text();  //Get selected text
              var ckId=$("#ddlStorageList").val();  //Get selected value
              
              alert(ckName);
              alert(ckId);
              
               var grid = liger.get("maingrid");
               var data = manager.getData();
               
               alert(data.length);
               
               
               for(var i=0;i<data.length;i++)
               {
                   alert(data[i].goodsId);
                   
                   grid.updateCell("ckId",ckId,i);
                   grid.updateCell("ckName",ckName,i);
                   
               }
                            
               
               $(".l-dialog,.l-window-mask").remove();
               
//             var dialog = frameElement.dialog;
//            
//             dialog.close(); //close dialog

             $.ligerDialog.close(); //close dialog
        }
        
      
      function updateTotal()
      {
      
          
            var data = manager.getData();//getData            var sumPriceItem=0;//
         
            //1、delete empty space first
            for(var i=data.length-1;i>=0;i--)
            {
                 if(data[i].goodsId==0 || data[i].goodsId=="" || data[i].goodsName=="")
                 {
                     data.splice(i,1);
                    
                 }
                
             }

           for(var i=0;i<data.length;i++)
           {
                    
               sumPriceItem+=Number(data[i].num)*Number(data[i].price); 
               
           }
                        
           $("#sumPriceItem").html(formatCurrency(sumPriceItem));
           
          
           
      }
      
       
        //Product change event: obtain unit, unit price and other information
        function f_onGoodsChanged(e)
        { 
                    
                   if (!e || !e.length) return;
            
            //1、Update the subsequent data of the current row first
            
            var grid = liger.get("maingrid");

           var selected =e[0];// e.data[0]; 
            
          // alert(selected.names);
                     var selectedRow = manager.getSelected();
           
            grid.updateRow(selectedRow, {
                
                goodsId: selected.id,
                goodsName: selected.names,
                unitName: selected.unitName,
                num:1,
                price: selected.priceCost,
                spec:selected.spec,
                sumPrice:selected.priceCost,
                ckId:selected.ckId,
                ckName:selected.ckName,
                remarks:""
                
            });

            if(e.length>1) //If there are multiple lines, delete the blank lines first and then insert the following
            {

             var data = manager.getData();
             for(var i=data.length-1;i>=0;i--)
             {
                 if(data[i].goodsId==0 || data[i].goodsName=="")
                 {
                     manager.deleteRow(i);
                    // alert("Delete rows: "+i);
                 }
                
             }

               for(var i=1;i<e.length;i++)
               {
                   grid.addRow({
                        id:rowNumber,
                        goodsId: e[i].id,
                        goodsName:e[i].names,
                        unitName:e[i].unitName,
                        num:1,
                        price : e[i].priceCost,
                        spec:e[i].spec,
                        sumPrice:e[i].priceCost,
                   
                        ckId:e[i].ckId,
                        ckName:e[i].ckName,
                        remarks:""
                        
                       });
                    
                    rowNumber=rowNumber+1;         
                 
               }
  
           }

        }
        
      
        
        
        //City drop-down box data initialization, this can also be changed to change server parameters (parms, url)
        function f_createCityData(e)
        {
            var Country = e.record.Country;
            var options =  {
                data: getCityData(Country)
            }; 
            return options;
        }  

       function f_onSelected(e) { 
            if (!e.data || !e.data.length) return;
 
            var grid = liger.get("maingrid");
 
            var selected = e.data[0]; 
            grid.updateRow(grid.lastEditRow, {
                CustomerID: selected.CustomerID,
                CompanyName: selected.CompanyName
            });
 
            var out = JSON.stringify(selected);
            $("#message").html('最后选择:'+out);
        }

       
     
       
        //After Edit
        function f_onAfterEdit(e)
        {
            var num,price,sumPrice;
            
            
            num=Number(e.record.num);
            
            price=Number(e.record.price);
                
       
            sumPrice=Number(e.record.sumPrice);    
            
         
           var goodsId,goodsName;
            
             goodsId=e.record.goodsId;
             goodsName=e.record.goodsName;
              
             if(goodsId=="" || goodsName=="")
             {
                 return;
             }
          
            if (e.column.name == "num") //Quantity change---start
            {
                 //Quantity change: [Discount rate, tax rate] Calculate [discount amount, amount, tax amount, price and tax total]
                 num=Number(e.value);
             
                  //2、Amount = quantity * unit price - discount amount
                  sumPrice=Number(num)* Number(price);

                  num=Math.round(num*100)/100;
                  price=Math.round(price*100)/100;
                
                  sumPrice=Math.round(sumPrice*100)/100;
               
                 
                  
                  
                 
                 manager.updateCell("num",num, e.record);
                
             
                 
                 manager.updateCell('sumPrice',sumPrice, e.record);
                 
            
                 
               
                 
                
                 

            } //Quantity change---end
            
            if (e.column.name == "price") 
            {
                
                price=Number(e.value);
                
              
                 
            
                 sumPrice=Number(num)* Number(price);
                                    
            
          
                 
                  num=Math.round(num*100)/100;
                  price=Math.round(price*100)/100;
               
                  sumPrice=Math.round(sumPrice*100)/100;
             
                  
                 
                
               
                
                 manager.updateCell("price",price, e.record);
                
            
              
                 manager.updateCell('sumPrice',sumPrice, e.record);
                 
           

            } 
            
         
            
            
             if (e.column.name == "sumPrice") 
            {
                 
               
                sumPrice=Number(e.value);

              
                
                if(num!=0)
                {
                    price=(sumPrice)/num;
                }
                else
                {
                    price=0;
                }
                
              
                 
                 num=Math.round(num*100)/100;
                  price=Math.round(price*100)/100;
               
                  sumPrice=Math.round(sumPrice*100)/100;
             
                  
                
                
                
                 manager.updateCell("price",price,e.record);
              
         
                 
                 manager.updateCell('sumPrice',sumPrice, e.record);
                 
             

            } 
            
          
          
           
          
 
           
           
         updateTotal();

        
          
         
        }



        
     
        function f_onBeforeEdit(e)
        { 
        
//            if(e.data.goodsId!="" && e.data.goodsName!="") return true;
//            return false;
//            
//            if(e.rowindex<=2) return true;
//            return false;

            
            
        }
    
        
        function f_onBeforeSubmitEdit(e)
        { 
            if (e.column.name == "dis")
            {
                if (e.value < 0 || e.value > 100) return false;
            }
            
            if (e.column.name == "tax")
            {
                if (e.value < 0 || e.value > 100) return false;
            }
            
            
            return true;
        }
       

       function beginEdit() {
            var row = manager.getSelectedRow();
            if (!row) { alert('Please select a row'); return; }
            manager.beginEdit(row);
        }
        function cancelEdit() {
            var row = manager.getSelectedRow();
            if (!row) { alert('Please select a row'); return; }
            manager.cancelEdit(row);
        }
        function cancelAllEdit()
        {
            manager.cancelEdit();
        }
        function endEdit()
        {
            var row = manager.getSelectedRow();
            if (!row) { alert('Please select a row'); return; }
            manager.endEdit(row);
        }
        function endAllEdit()
        {
            manager.endEdit();
        }
        function deleteRow()
        { 
           
            if(manager.rows.length==1)
            {
                $.ligerDialog.warn('Keep at least one line!')
                
            }
            else
            {
               manager.deleteSelectedRow();
            }
            
        }
        var newrowid = 100;
        
        function addNewRow()
        {
             var gridData = manager.getData();
             var rowNum=gridData.length;
             
           
               manager.addRow({ 
                id: rowNum+1,
                    id : rowNum+1,
                    goodsId :"",
                    goodsName : "",
                    unitName : "",
                    num : "",
                    spec:"",
                    sumPrice : "",

              
                    ckId : "",
                    ckName : "",

                    remarks : ""
            });
             
        } 
         
        function updateRow()
        {
            var selected = manager.getSelected();
            if (!selected) { alert('Please select a row'); return; }
          
        }

        function getSelected()
        { 
            var row = manager.getSelectedRow();
            if (!row) { alert('Please select a row'); return; }
            alert(JSON.stringify(row));
        }
        function getData()
        { 
            var data = manager.getData();
            alert(JSON.stringify(data));
        } 
        


function save()
{

     
     //Delete the blank lines first


     var data = manager.getData();
     
     
    //1、Delete the blank lines first
      for(var i=data.length-1;i>=0;i--)
     {
         if(data[i].goodsId==0 || data[i].goodsName=="")
         {
             data.splice(i,1);
            
         }
        
     }
     
     
     //2、Check whether to select a product
      if(data.length==0)
     {
          $.ligerDialog.warn('Please select product!');
          
          return;
          alert("I won't execute it!");
     }     
     
     
     
     //3、Check whether the product quantity has been entered.
      for(var i=0;i<data.length;i++)
     {
         if(data[i].num<=0 || data[i].num=="" || data[i].num=="0" || data[i].num=="0.00")
         {
             
             $.ligerDialog.warn("Please enter the product quantity in the "+(i+1)+" row!");
             
             return;
             alert("I won't execute it!");
         }
         
          if(data[i].ckId==0 || data[i].ckId=="" || data[i].ckId=="0" || data[i].ckName=="")
         {
             
             $.ligerDialog.warn("Please enter the warehouse at line "+(i+1)+"!");
             
             return;
             alert("I won't execute it!");
         }
       
        
     }
     
      
      var typeId=1;
      if ($("#rb1").attr("checked")) {
           //alert("Purchase selected");
           typeId=1;
      }
      if ($("#rb2").attr("checked")) {
           //alert("Return selected");
     
         typeId=-1;
     
      }
             
 
//    var checkText=$("#ddlVenderList").find("option:selected").text();  //Get selected text
     var venderId=$("#ddlVenderList").val();  //Get selected value


            
      var bizDate=$("#txtBizDate").val();
      if(bizDate=="")
      {
          $.ligerDialog.warn("Please enter the library date!");
          return;
          
      }

           
         
      
            
            var remarks=$("#txtRemarks").val();
            
           
        
           var headJson={id:getUrlParam("id"),venderId:venderId,bizDate:bizDate,remarks:remarks,typeId:typeId};
      
    
        
        var dataNew = [];
        dataNew.push(headJson);
        
   
        
        var list=JSON.stringify(headJson);
        
        
        var goodsList=[];
        
        
   
        
        list=list.substring(0,list.length-1);//Remove the last curly brace
        
        list+=",\"Rows\":";
        list+=JSON.stringify(data);      
        list+="}";
       
      
        
        var postData=JSON.parse(list);//Final json
        
//        alert(postData.Rows[0].id);
//        
//        alert(postData.bizDate);
//        
//        alert(postData.Rows[0].goodsName);

//       alert(JSON.stringify(postData));

//       $("#txtRemarks").val(JSON.stringify(postData));
       
    
         
         $.ajax({
            type: "POST",
            url: 'ashx/OtherInListEdit.ashx',
            contentType: "application/json", 
            //dataType: "json", 
            data:JSON.stringify(postData),  //data: "{'str1':'foovalue', 'str2':'barvalue'}",
            success: function (jsonResult) {
                
                if(jsonResult=="Operation successful!")
                {
                
                    $.ligerDialog.waitting('Operation successful!'); setTimeout(function () { $.ligerDialog.closeWaitting();location.reload();}, 2000);
                    
                }
                else
                {
                   $.ligerDialog.warn(jsonResult);
                   
                }
            },
            error: function (xhr) {
                alert("An error occurred, please try again later:" + xhr.responseText);
            }
        });
            
           
}

function checkBill(){

   var data = manager.getData();
   if(data.length==0)
   {
       $.ligerDialog.warn('Please select product information');
       return false;
   }
   else
   {
      for(var i=0;i<data.length;i++)
      {
          if(data.Rows[i].goodsName=="" || data.Rows[i].goodsId==0)
          {
              
              $.ligerDialog.warn('The product information in the row: ' + i +" is empty!");
              return false;
             
          }
          
          if(data.Rows[i].num==0)
          {
              
              $.ligerDialog.warn('Please fill in the quantity of product in row ' + i +"!");
              return false;
             
          }
          
    
      }
   }
   

};

function makeBill()
{
    var row = manager.getSelectedRow();
    
    //alert(row.id);
   // return;
    //window.open("PurReceiptListAdd.aspx?id="+row.id);    
    //return;
   // alert('buy/PurReceiptListAdd.aspx?id='+row.id);
   
     top.topManager.openPage({
            id : 'PurReceiptListAdds',
            href : 'buy/PurReceiptListAdd.aspx?id='+row.id,
            title : 'Add New Purchase To Warehouse'
          });
  
}


function getUrlParam(name)
{
   var reg = new RegExp("(^|&)"+ name +"=([^&]*)(&|$)");

   var r = window.location.search.substr(1).match(reg);

   if (r!=null) return unescape(r[2]); return null;
}


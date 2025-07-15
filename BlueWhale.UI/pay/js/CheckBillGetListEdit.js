        var data = [{
            UnitPrice: 10,
            Quantity: 2,
            Price: 20
        }];
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
        
        
        function selectGet()
        {
        
             
            
            var start = $("#txtDateStar1").val();
            var end = $("#txtDateEnd1").val();
            
          
            var wlId = $("#ddlVenderList").val();
    
             var keys = document.getElementById("txtKeysGet").value;
            if (keys == "Please Enter Receipt No.") {

                keys = "";

            }
            
    
           
           manager._setUrl("CheckBillGetListEdit.aspx?Action=GetDataListSearchGet&keys=" + keys + "&start=" +start + "&end=" + end+"&wlId="+wlId+"&id="+getUrlParam("id"));
              
        }
        

        function selectBill()
        {
            var start = $("#txtDateStart").val();
            var end = $("#txtDateEnd").val();
            
          
            var wlId = $("#ddlVenderList").val();
    
             var keys = document.getElementById("txtKeys").value;
            if (keys == "Please Enter Receipt No.") {

                keys = "";

            }
           
           managersub._setUrl("CheckBillGetListEdit.aspx?Action=GetDataListSearch&keys=" + keys + "&start=" +start + "&end=" + end+"&wlId="+wlId+"&id="+getUrlParam("id"));
              
        }

         function addNewRow()
        {
            
             manager.addRow({ 
              
                    bkId :"",
                    payPrice :"",
                    payTypeId : "",
                    payNumber : "",
                   
                    remarks : ""
            });
            
             f_onAfterEdit();//Recalculate

             
        } 
         

  
    var manager;
        $(function ()
        {
        
          var form = $("#form").ligerForm();
          var target1 = $("#target1").ligerForm();
          
            var g =  $.ligerui.get("ddlVenderList");
            g.set("Width", 250);
            
            var txtDateStart =  $.ligerui.get("txtDateStart");
            txtDateStart.set("Width", 120);
            
            var txtDateEnd =  $.ligerui.get("txtDateEnd");
            txtDateEnd.set("Width", 120);
            
            
            
            var txtDateStar1 =  $.ligerui.get("txtDateStar1");
            txtDateStar1.set("Width", 120);
            
            var txtDateEnd1 =  $.ligerui.get("txtDateEnd1");
            txtDateEnd1.set("Width", 120);
            
            
            
            
            window['g'] = 
            manager = $("#maingrid").ligerGrid({
                columns: [
                
                 { display: '', isSort: false, width: 40,align:'center',frozen:true, render: function (rowdata, rowindex, value)
                 {
                    var h = "";
                    if (!rowdata._editing)
                    {
                        h += "<a href='javascript:deleteRow()' title='Delete Row' style='text-align:center;'><div class='ui-icon ui-icon-trash'></div></a> "; 
                    }
                    return h;
                }
                }
                ,
               
               
                
                    {
                        display: 'Collection Number', name: 'sourceNumber',width: 220, align: 'center',
                
                     totalSummary:
                    {
                        type: 'count',
                        render: function (e) 
                        {  // Summary renderer, returns HTML to be loaded into the cell
                            // e: Summary Object (including sum, max, min, avg, count)
                            return 'Total: ';
                        }
                    }
                
                
                },
                
                    { display: 'Business Type', name: 'bizType',width: 120, align: 'center' },
                    { display: 'Invoice Date', name: 'bizDate',width: 80, align: 'center' },
                    { display: 'Invoice Amount', name: 'sumPriceAll',width: 120, align: 'right' },
                { display: 'Amount Written Off', name: 'priceCheckNowSum',width: 120, align: 'right' },
                { display: 'Unsettled Amount', name: 'priceCheckNo',width: 120, align: 'right' },
                
                    {
                        display: 'Current Write-Off Amount', name: 'priceCheckNow', width: 120, type: 'float', align: 'right', editor: { type: 'float', precision: 4 },
                
                   totalSummary:
                    {
                       align: 'right',   // Summary cell content alignment: left/center/right                        
                       type: 'sum',
                        render: function (e) 
                        {  // Summary renderer, returns HTML to be loaded into the cell
                            // e: Summary Object (including sum, max, min, avg, count)
                            return Math.round(e.sum * 100) / 100;
                        }
                    }
                
                
               }
                
                ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '190',
                url: 'CheckBillGetListEdit.aspx?Action=GetData&id='+getUrlParam("id"),//Get Params
                rownumbers: true, // Show row numbers
                frozenRownumbers: true, // Whether row numbers are fixed in the columns
                dataAction: 'local', // Local sorting
                usePager: false,
                alternatingRow: false, 

                totalSummary: false, 
                enabledEdit: true, // Control whether editing is allowed
                onBeforeSubmitEdit: f_onBeforeSubmitEdit, // Action before submitting edit
                onAfterEdit: f_onAfterEdit // Action after updating the cell
            }
            );
        });
//

// After edit event -------- Payment Amount
     
        
        function f_onAfterEdit()
        {
            var sumPrice = 0;// Total payment amount for this transaction
           var data = manager.getData();
           for(var i=0;i<data.length;i++)
           {
               sumPrice+=Number(data[i].payPrice);
               
           }
          
            var sumPayPrice = 0;// Total write-off amount for this transaction
            var datasub = managersub.getData();
           for(var i=0;i<datasub.length;i++)
           {
               sumPayPrice+=Number(datasub[i].priceCheckNow);
               
           }
           
           if(Number(sumPayPrice)>Number(sumPrice))
           {
               $.ligerDialog.warn("The total redemption amount cannot exceed the total payment amount!");
              return;
           }
           
//           alert("sumPrice: "+sumPrice);
//           alert("sumPayPrice: "+sumPayPrice);
//          
            var disPrice = $("#txtDisPrice").val();// Overall discount for the order
           
      
           
           var payPriceNowMore=Number(sumPrice)-Number(sumPayPrice)-Number(disPrice);
           
           $("#txtPayPriceNowMore").val(payPriceNowMore);
         
        }
        
        
        
        
 
       var managersub;
        $(function ()
        {
         
          
          
            window['gsub'] = 
            managersub = $("#maingridsub").ligerGrid({
                columns: [
                 { display: '', isSort: false, width: 40,align:'center',frozen:true, render: function (rowdata, rowindex, value)
                 {
                    var h = "";
                    if (!rowdata._editing)
                    {
                        h += "<a href='javascript:deleteRowSub()' title='Delete Row' style='text-align:center;'><div class='ui-icon ui-icon-trash'></div></a> "; 
                    }
                    return h;
                }
                }
                ,
               
               
                
                    {
                        display: 'Accounts Receivable No. (AR No.).', name: 'sourceNumber',width: 220, align: 'center',
                
                     totalSummary:
                    {
                        type: 'count',
                        render: function (e) 
                        {  // Summary renderer, returns HTML to be loaded into the cell
                            // e: Summary Object (including sum, max, min, avg, count)
                            return 'Total: ';
                        }
                    }
                
                
                },
                
                    { display: 'Business Type', name: 'bizType', width: 120, align: 'center' },
                    { display: 'Invoice Date', name: 'bizDate', width: 80, align: 'center' },
                    { display: 'Invoice Amount', name: 'sumPriceAll', width: 120, align: 'right' },
                    { display: 'Amount Written Off', name: 'priceCheckNowSum', width: 120, align: 'right' },
                    { display: 'Unsettled Amount', name: 'priceCheckNo', width: 120, align: 'right' },

                
                    { display: 'Current Write-Off Amount', name: 'priceCheckNow', width: 120, type: 'float', align: 'right', editor: { type: 'float', precision: 4 },
                
                   totalSummary:
                    {
                       align: 'right',   // Summary cell content alignment: left/center/right                          type: 'sum',
                        render: function (e) 
                        {   // Summary renderer, returns HTML to be loaded into the cell
                            // e: Summary Object (including sum, max, min, avg, count)
                            return  Math.round(e.sum*100)/100;
                        }
                    }
                
                
               }

               
                ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '190',
               url: 'CheckBillGetListEdit.aspx?Action=GetDataSub&id='+getUrlParam("id"),//Get params
               rownumbers:true,//Display serial number
               frozenRownumbers:true,//Is the row number in a fixed column
                dataAction: 'local',//Local sorting
                usePager:false,
                alternatingRow: false,
                
                totalSummary:true,
                enabledEdit: true, //Control whether editing is allowed
               // onBeforeEdit: f_onBeforeEdit,
                onBeforeSubmitEdit: f_onBeforeSubmitEditSub,
                onAfterEdit: f_onAfterEdit //Actions after updating the cell
            }
            );
        });
 

         //Only the first 3 rows are allowed to be edited.
        function f_onBeforeEdit(e)
        { 
            if(e.rowindex<=2) return true;
            return false;
        }
        //Age restriction
        
        function f_onBeforeSubmitEdit(e)
        {     
             var data = manager.getData();
           
             if (Number(e.value) > Number(data[e.rowindex].priceCheckNo))//The current write-off is greater than the unapplied amount. 
             {
                $.ligerDialog.warn("The write-off amount cannot be greater than the unapplied amount!");
               return false;
             }
             return true;
        }
        
        
        
        function f_onBeforeSubmitEditSub(e)
        {     
             var data = managersub.getData();
           
             if (Number(e.value) > Number(data[e.rowindex].priceCheckNo))//The current write-off is greater than the unapplied amount. 
             {
                $.ligerDialog.warn("The write-off amount cannot be greater than the unapplied amount!");
               return false;
             }
             return true;
        }

 
        var rowNumber=9;
 
     
    
    
      

        function deleteRow()
        { 
           
             manager.deleteSelectedRow();
            
        }
        
        function deleteRowSub()
        { 
            
            managersub.deleteSelectedRow();
            
       
            
            
            
        }
        
        
        var newrowid = 100;
        
       
      
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
        


function save() {
    var data = manager.getData();
    // alert(JSON.stringify(data));

    var datasub = managersub.getData();
    // alert(JSON.stringify(datasub));

    // return ;

    // 1, First delete the blank rows
    for (var i = data.length - 1; i >= 0; i--) {
        if (data[i].sourceNumber == "") {
            data.splice(i, 1);
        }
    }

    // 2, Check if a product is selected
    if (data.length == 0) {
        $.ligerDialog.warn('Please select payment information!');

        return;
        alert("Execution skipped!");
    }

    // 3, Check if the quantity for all products has been entered
    for (var i = 0; i < data.length; i++) {
        if (Number(data[i].priceCheckNow) <= 0 && data[i].sourceNumber != "") {

            $.ligerDialog.warn("Please enter the write-off amount for payment information line " + (i + 1) + "!");

            return;
            alert("Execution skipped!");
        }
    }

    // 1, First delete the blank rows
    for (var i = datasub.length - 1; i >= 0; i--) {
        if (datasub[i].sourceNumber == "") {
            datasub.splice(i, 1);
        }
    }

    if (datasub.length == 0) {
        $.ligerDialog.warn('Please select Accounts Receivable invoice information!');

        return;
        alert("Execution skipped!");
    }

    // 3, Check if the quantity for all products has been entered
    for (var i = 0; i < datasub.length; i++) {
        if (Number(datasub[i].priceCheckNow) <= 0 && datasub[i].sourceNumber != "") {

            $.ligerDialog.warn("Please enter the write-off amount for the Accounts Receivable information on line " + (i + 1) + "!");

            return;
            alert("Execution skipped!");
        }
    }

    var bizDate = $("#txtBizDate").val();
    if (bizDate == "") {
        $.ligerDialog.warn("Please enter the write-off date!");
        return;
    }

    var remarks = $("#txtRemarks").val();

    var wlId = $("#ddlVenderList").val();  // Get the selected value from the dropdown

    var CheckPrice = 0;
    // Calculate the total write-off amount
    for (var i = 0; i < data.length; i++) {
        CheckPrice += Number(data[i].priceCheckNow);
    }
    var sumPayPrice = 0;
    for (var i = 0; i < datasub.length; i++) {
        sumPayPrice += Number(datasub[i].priceCheckNow);
    }

    if (CheckPrice != sumPayPrice) {
        $.ligerDialog.warn("The total write-off amount does not match, please adjust!");
        return;
    }

    var headJson = { Id: getUrlParam("id"), ClientId: wlId, BizDate: bizDate, Remarks: remarks, CheckPrice: CheckPrice };

    // alert(JSON.stringify(headJson));

    var dataNew = [];
    dataNew.push(headJson);

    var list = JSON.stringify(headJson); // Serialize into string, header

    list = list.substring(0, list.length - 1); // Remove the last curly bracket

    list += ",\"Rows\":";
    list += JSON.stringify(data); // Insert account information

    list += ",\"RowsBill\":";
    list += JSON.stringify(datasub); // Insert write-off information

    list += "}";

    var postData = JSON.parse(list); // Final JSON

    // alert(JSON.stringify(postData));

    // return;
    // alert(postData.Rows[0].id);
    //
    // alert(postData.bizDate);
    //
    // alert(postData.Rows[0].goodsName);

    // alert(JSON.stringify(postData));

    // $("#txtRemarks").val(JSON.stringify(postData));

    // return;

    $.ajax({
        type: "POST",
        url: 'ashx/CheckBillGetListEdit.ashx',
        contentType: "application/json", // Must have
        //dataType: "json", // Indicates return value type, not necessary
        data: JSON.stringify(postData),  // Equivalent to //data: "{'str1':'foovalue', 'str2':'barvalue'}",
        success: function (jsonResult) {

            if (jsonResult == "Operation successful!") {

                $.ligerDialog.waitting('Operation successful!'); setTimeout(function () { $.ligerDialog.closeWaitting(); location.reload(); }, 2000);

            } else {
                $.ligerDialog.warn(jsonResult);

            }
        },
        error: function (xhr) {
            alert("An error occurred, please try again later:" + xhr.responseText);
        }
    });
}



function getUrlParam(name)
{
   var reg = new RegExp("(^|&)"+ name +"=([^&]*)(&|$)");

   var r = window.location.search.substr(1).match(reg);

   if (r!=null) return unescape(r[2]); return null;
}

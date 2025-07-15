function formatCurrency(x) {

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



$(document).bind('keydown.grid', function (event) {
    if (event.keyCode == 13 || event.keyCode == 39 || event.keyCode == 9) //enter,right arrow,tap
    {
        manager.endEditToNext();
    }
});




//New style import line

function f_selectContact() {
    $.ligerDialog.open({
        title: 'Select Item', name: 'winselector', width: 840, height: 540, url: '../baseSet/GoodsListSelect.aspx', buttons: [
            { text: 'Confirm', onclick: f_selectContactOK },
            { text: 'Close', onclick: f_selectContactCancel }
        ]
    });
    return false;
}
function f_selectContactOK(item, dialog) {
    var fn = dialog.frame.f_select || dialog.frame.window.f_select;
    var data = fn();
    if (!data) {
        alert('Please select a row!');
        return;
    }

    f_onGoodsChanged(data);

    dialog.close();

}


function f_selectContactCancel(item, dialog) {
    dialog.close();
}



//New style import line end


//Extend the formatting function of the NumberBox type
$.ligerDefaults.Grid.formatters['numberbox'] = function (value, column) {
    var precision = column.editor.precision;
    return value.toFixed(precision);
};



var manager;
$(function () {

    var form = $("#form").ligerForm();

    var g = $.ligerui.get("ddlVenderList");
    g.set("Width", 250);


    window['g'] =
        manager = $("#maingrid").ligerGrid({
            columns: [

                {
                    display: '', isSort: false, width: '6%', minWidth: 60, align: 'center', frozen: true, render: function (rowdata, rowindex, value) {
                        var h = "";
                        if (!rowdata._editing) {
                            h += "<a href='javascript:addNewRow()' title='Add Row' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                            h += "<a href='javascript:deleteRow()' title='Delete Row' style='float:left;'><div class='ui-icon ui-icon-trash'></div></a> ";
                            h += "<a href='javascript:f_selectContact()' title='Select Item' style='float:left;'><div class='ui-icon ui-icon-search'></div></a> ";
                        }

                        return h;
                    }
                }
                ,

                {
                    display: 'Item Name', name: 'goodsName', width: '16%', minWidth: 250, align: 'left',

                    totalSummary:
                    {
                        type: 'count',
                        render: function (e) {
                            return 'Total: ';
                        }
                    }


                },

                { display: 'Specification', name: 'spec', width: '8%', minWidth: 120, align: 'center' },
                { display: 'Unit', name: 'unitName', width: '8%', minWidth: 100, align: 'center' },

                {
                    display: 'Quantity', name: 'num', width: '8%', minWidth: 100, type: 'float', align: 'right', editor: { type: 'float' },

                    totalSummary:
                    {
                        align: 'right', 
                        type: 'sum',
                        render: function (e) {
                            return Math.round(e.sum * 100) / 100;
                        }
                    }

                },

                {
                    display: 'Stock In Unit Price', name: 'price', width: '10%', minWidth: 130, type: 'float', align: 'right', editor: { type: 'float', precision: 4 }

                },




                {
                    display: 'Amount', name: 'sumPrice', width: '8%', minWidth: 100, type: 'float', align: 'right', editor: { type: 'float' },



                    totalSummary:
                    {
                        align: 'center',
                        type: 'sum',
                        render: function (e) {  
                            var itemSumPrice = e.sum;




                            return "<span id='sumPriceItem'>" + Math.round(itemSumPrice * 10000) / 10000 + "</span>";//formatCurrency(suminf.sum)


                        }
                    }


                },



                {
                    display: 'Warehouse', name: 'ckId', width: '8%', minWidth: 100, isSort: false, textField: 'ckName',
                    editor: {
                        type: 'select',
                        url: "../baseSet/InventoryList.aspx?Action=GetDDLList&r=" + Math.random(),
                        valueField: 'ckId', textField: 'ckName'
                    }

                },

                { display: 'Remarks', name: 'remarks', width: '12%', minWidth: 150, align: 'left', type: 'text', editor: { type: 'text' } }
            ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '450',
            url: 'OtherInListAdd.aspx?Action=GetData',
            rownumbers: true,
            frozenRownumbers: true,
            dataAction: 'local',
            usePager: false,
            alternatingRow: false,

            totalSummary: true,
            enabledEdit: true,
            // onBeforeEdit: f_onBeforeEdit,
            // onBeforeSubmitEdit: f_onBeforeSubmitEdit,

            //totalRender:f_totalRender,

            onAfterEdit: f_onAfterEdit
        }
        );
});

var rowNumber = 9;

function f_totalRender(data, currentPageData) {
    //return "Total number of warehouses:"+data.sumPriceAll;
}

function setInventory() {

    $.ligerDialog.open({ target: $("#target1") });


    // $.ligerDialog.open({ url: '../../welcome.htm', height: 250,width:null, buttons: [ { text: 'Confirm', onclick: function (item, dialog) { alert(item.text); } }, { text: 'Cancel', onclick: function (item, dialog) { dialog.close(); } } ] });
}

function updateTotal() {


    var data = manager.getData();//getData
    var sumPriceItem = 0;//

    for (var i = data.length - 1; i >= 0; i--) {
        if (data[i].goodsId == 0 || data[i].goodsId == "" || data[i].goodsName == "") {
            data.splice(i, 1);

        }

    }

    for (var i = 0; i < data.length; i++) {

        sumPriceItem += Number(data[i].num) * Number(data[i].price);

    }

    $("#sumPriceItem").html(formatCurrency(sumPriceItem));



}



function f_onGoodsChanged(e) {


    if (!e || !e.length) return;


    var grid = liger.get("maingrid");

    var selected = e[0];// e.data[0]; 

    // alert(selected.names);

    var selectedRow = manager.getSelected();

    grid.updateRow(selectedRow, {

        goodsId: selected.id,
        goodsName: selected.names,
        unitName: selected.unitName,
        num: 1,
        price: selected.priceCost,
        spec: selected.spec,
        sumPrice: selected.priceCost,
        ckId: selected.ckId,
        ckName: selected.ckName,
        remarks: ""

    });

    if (e.length > 1)
    {

        var data = manager.getData();
        for (var i = data.length - 1; i >= 0; i--) {
            if (data[i].goodsId == 0 || data[i].goodsName == "") {
                manager.deleteRow(i);
                // alert("DeleteRow: "+i);
            }

        }

        for (var i = 1; i < e.length; i++) {
            grid.addRow({
                id: rowNumber,
                goodsId: e[i].id,
                goodsName: e[i].names,
                unitName: e[i].unitName,
                num: 1,
                price: e[i].priceCost,
                spec: e[i].spec,
                sumPrice: e[i].priceCost,

                ckId: e[i].ckId,
                ckName: e[i].ckName,
                remarks: ""

            });

            rowNumber = rowNumber + 1;

        }

    }

}




function f_createCityData(e) {
    var Country = e.record.Country;
    var options = {
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
    $("#message").html('Final Selection: ' + out);
}




function f_onAfterEdit(e) {
    var num, price, sumPrice;


    num = Number(e.record.num);

    price = Number(e.record.price);


    sumPrice = Number(e.record.sumPrice);

    var goodsId, goodsName;

    goodsId = e.record.goodsId;
    goodsName = e.record.goodsName;

    if (goodsId == "" || goodsName == "") {
        return;
    }


    if (e.column.name == "num")
    {
        num = Number(e.value);

        sumPrice = Number(num) * Number(price);

        num = Math.round(num * 100) / 100;
        price = Math.round(price * 100) / 100;

        sumPrice = Math.round(sumPrice * 100) / 100;





        manager.updateCell("num", num, e.record);



        manager.updateCell('sumPrice', sumPrice, e.record);








    }

    if (e.column.name == "price")
    {
        price = Number(e.value);



        sumPrice = Number(num) * Number(price);




        num = Math.round(num * 100) / 100;
        price = Math.round(price * 100) / 100;

        sumPrice = Math.round(sumPrice * 100) / 100;



        manager.updateCell("price", price, e.record);


        manager.updateCell('sumPrice', sumPrice, e.record);



    }




    if (e.column.name == "sumPrice")
    { 

        sumPrice = Number(e.value);


        if (num != 0) {
            price = (sumPrice) / num;
        }
        else {
            price = 0;
        }



        num = Math.round(num * 100) / 100;
        price = Math.round(price * 100) / 100;

        sumPrice = Math.round(sumPrice * 100) / 100;



        manager.updateCell("price", price, e.record);



        manager.updateCell('sumPrice', sumPrice, e.record);



    }








    updateTotal();




}



function f_onBeforeEdit(e) {

    //            if(e.data.goodsId!="" && e.data.goodsName!="") return true;
    //            return false;
    //            
    //            if(e.rowindex<=2) return true;
    //            return false;




}

function f_onBeforeSubmitEdit(e) {
    if (e.column.name == "dis") {
        if (e.value < 0 || e.value > 100) return false;
    }

    if (e.column.name == "tax") {
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
function cancelAllEdit() {
    manager.cancelEdit();
}
function endEdit() {
    var row = manager.getSelectedRow();
    if (!row) { alert('Please select a row'); return; }
    manager.endEdit(row);
}
function endAllEdit() {
    manager.endEdit();
}
function deleteRow() {

    if (manager.rows.length == 1) {
        $.ligerDialog.warn('At least keep one row!')

    }
    else {
        manager.deleteSelectedRow();
    }

}
var newrowid = 100;

function addNewRow() {
    var gridData = manager.getData();
    var rowNum = gridData.length;


    manager.addRow({
        id: rowNum + 1,
        id: rowNum + 1,
        goodsId: "",
        goodsName: "",
        unitName: "",
        num: "",
        spec: "",
        sumPrice: "",


        ckId: "",
        ckName: "",

        remarks: ""
    });


}

function updateRow() {
    var selected = manager.getSelected();
    if (!selected) { alert('Please select a row'); return; }

}


function getSelected() {
    var row = manager.getSelectedRow();
    if (!row) { alert('Please select a row'); return; }
    alert(JSON.stringify(row));
}
function getData() {
    var data = manager.getData();
    alert(JSON.stringify(data));
}



function save() {




    var data = manager.getData();


    for (var i = data.length - 1; i >= 0; i--) {
        if (data[i].goodsId == 0 || data[i].goodsName == "") {
            data.splice(i, 1);

        }

    }


    if (data.length == 0) {
        $.ligerDialog.warn('Please select an item!');

        return;
        alert("Won't execute it!");
    }



    for (var i = 0; i < data.length; i++) {
        if (data[i].num <= 0 || data[i].num == "" || data[i].num == "0" || data[i].num == "0.00") {

            $.ligerDialog.warn("Please enter the item quantity in the row" + (i + 1) + "!");

            return;
            alert("Won't execute it!");
        }

        if (data[i].ckId == 0 || data[i].ckId == "" || data[i].ckId == "0" || data[i].ckName == "") {

            $.ligerDialog.warn("Please enter the warehouse at line" + (i + 1) + "!");

            return;
            alert("Won't execute it!");
        }


    }


    var typeId = 1;
    if ($("#rb1").attr("checked")) {
        //alert("Selected Purchase");
        typeId = 1;
    }
    if ($("#rb2").attr("checked")) {
        //alert("Selected Return");

        typeId = -1;

    }


    //    var checkText=$("#ddlVenderList").find("option:selected").text(); 
    var venderId = $("#ddlVenderList").val();



    var bizDate = $("#txtBizDate").val();
    if (bizDate == "") {
        $.ligerDialog.warn("Please enter stock in date!");
        return;

    }



    var remarks = $("#txtRemarks").val();



    var headJson = { venderId: venderId, bizDate: bizDate, remarks: remarks, typeId: typeId };



    var dataNew = [];
    dataNew.push(headJson);



    var list = JSON.stringify(headJson);


    var goodsList = [];




    list = list.substring(0, list.length - 1);

    list += ",\"Rows\":";
    list += JSON.stringify(data);
    list += "}";



    var postData = JSON.parse(list);

    //        alert(postData.Rows[0].id);
    //        
    //        alert(postData.bizDate);
    //        
    //        alert(postData.Rows[0].goodsName);

    //        alert(JSON.stringify(postData));

    //       $("#txtRemarks").val(JSON.stringify(postData));



    $.ajax({
        type: "POST",
        url: 'ashx/OtherInListAdd.ashx',
        contentType: "application/json", 
        //dataType: "json",
        data: JSON.stringify(postData),  //Equivalent to //data: "{'str1':'foovalue', 'str2':'barvalue'}",
        success: function (jsonResult) {

            if (jsonResult == "Operation successful!") {

                $.ligerDialog.waitting('Operation successful!'); setTimeout(function () { $.ligerDialog.closeWaitting(); location.reload(); }, 2000);

            }
            else {
                $.ligerDialog.warn(jsonResult);

            }
        },
        error: function (xhr) {
            alert("An error occurred, Please try again later:" + xhr.responseText);
        }
    });


}

function checkBill() {

    var data = manager.getData();
    if (data.length == 0) {
        $.ligerDialog.warn('Please select item information');
        return false;
    }
    else {
        for (var i = 0; i < data.length; i++) {
            if (data.Rows[i].goodsName == "" || data.Rows[i].goodsId == 0) {

                $.ligerDialog.warn('Row: ' + i + " item information in this row is empty!");
                return false;

            }

            if (data.Rows[i].num == 0) {

                $.ligerDialog.warn('Please fill in row ' + i + "quantity for the item!");
                return false;

            }


        }
    }


};

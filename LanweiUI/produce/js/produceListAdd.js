var data = [{
    UnitPrice: 10,
    Quantity: 2,
    Price: 20
}];
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

function addNewRow() {

    manager.addRow({

        processId: "",
        num: "",
        price: "",
        sumPrice: "",
        remarks: ""

    });

    f_onAfterEdit();//从新计算


}


//新样式引入行

function f_selectContact() {
    $.ligerDialog.open({
        title: '选择工序', name: 'winselector', width: 840, height: 540, url: '../baseSet/ProcessListSelect.aspx', buttons: [
            { text: '确定', onclick: f_selectContactOK },
            { text: '关闭', onclick: f_selectContactCancel }
        ]
    });
    return false;
}
function f_selectContactOK(item, dialog) {
    var fn = dialog.frame.f_select || dialog.frame.window.f_select;
    var data = fn();
    if (!data) {
        alert('请选择行!');
        return;
    }

    f_onGoodsChanged(data);

    dialog.close();

}


function f_selectContactCancel(item, dialog) {
    dialog.close();
}



//订单开始

$(function () {
    $("#txtOrderNumber").ligerComboBox({
        onBeforeOpen: f_selectOrder, valueFieldID: 'hfOrderNumber', width: 300
    });
});


function f_selectOrder() {
    $.ligerDialog.open({
        title: '选择订单', name: 'winselector', width: 950, height: 600, url: '../sales/SalesOrderListSelect.aspx', buttons: [
            { text: '确定', onclick: f_selectOrderOK },
            { text: '关闭', onclick: f_selectOrderCancel }
        ]
    });
    return false;
}


function f_selectOrderOK(item, dialog) {
    var fn = dialog.frame.f_select || dialog.frame.window.f_select;
    var data = fn();
    if (!data) {
        alert('请选择行!');
        return;
    }

    $("#txtOrderNumber").val(data[0].number);
    $("#hfOrderNumber").val(data[0].hfOrderNumber);
    $("#txtGoodsName").val(data[0].goodsName);
    $("#hfGoodsId").val(data[0].goodsId);
    $("#txtSpec").val(data[0].spec);
    $("#txtNum").val(data[0].num);
    $("#txtUnitName").val(data[0].unitName);


    var goodsId = data[0].goodsId;

    var num = data[0].num;

    manager.changePage("first");
    manager._setUrl("produceListAdd.aspx?Action=GetDataBom&goodsId=" + goodsId + "&num=" + num);


    dialog.close();

}


function f_selectOrderCancel(item, dialog) {
    dialog.close();
}

//订单结束


//商品开始

$(function () {
    $("#txtGoodsName").ligerComboBox({
        onBeforeOpen: f_selectGoods, valueFieldID: 'hfGoodsId', width: 300
    });
});


function f_selectGoods() {
    $.ligerDialog.open({
        title: '选择商品', name: 'winselector', width: 800, height: 540, url: '../baseSet/GoodsListSelect.aspx', buttons: [
            { text: '确定', onclick: f_selectGoodsOK },
            { text: '关闭', onclick: f_selectGoodsCancel }
        ]
    });
    return false;
}


function f_selectGoodsOK(item, dialog) {
    var fn = dialog.frame.f_select || dialog.frame.window.f_select;
    var data = fn();
    if (!data) {
        alert('请选择行!');
        return;
    }

    $("#txtOrderNumber").val("");
    $("#hfOrderNumber").val("");
    $("#txtGoodsName").val(data[0].names);
    $("#hfGoodsId").val(data[0].id);
    $("#txtSpec").val(data[0].spec);
    $("#txtUnitName").val(data[0].unitName);

    $("#txtNum").val("1");

    //start---------添加Bom明细

    var goodsId = data[0].id;


    manager.changePage("first");
    manager._setUrl("produceListAdd.aspx?Action=GetDataBom&goodsId=" + goodsId + "&num=1");



    //end

    dialog.close();

}


function f_selectGoodsCancel(item, dialog) {
    dialog.close();
}

//商品结束





//商品 改变事件：获取单位、单价等信息
function f_onGoodsChanged(e) {


    if (!e || !e.length) return;

    //1、先更新当前行的后续数据

    var grid = liger.get("maingrid");

    var selected = e[0];// e.data[0]; 

    // alert(selected.names);

    var selectedRow = manager.getSelected();

    grid.updateRow(selectedRow, {

        processId: selected.id,
        processName: selected.names,
        unitId: selected.unitId,
        unitName: selected.unitName,
        num: 1,
        price: selected.price,        
        sumPrice: selected.price,       
        remarks: ""

    });

    if (e.length > 1) //如果有多行的、先删除空白行，然后插入下面
    {

        var data = manager.getData();
        for (var i = data.length - 1; i >= 0; i--) {
            if (data[i].processId == 0 || data[i].processName == "") {
                manager.deleteRow(i);
                // alert("删除行："+i);
            }

        }

        for (var i = 1; i < e.length; i++) {
            grid.addRow({
                id: rowNumber,
                processId: e[i].id,
                processName: e[i].names,
                unitId: e[i].unitId,
                unitName: e[i].unitName,
                num: 1,
                price: e[i].price,
           
                sumPrice: e[i].price,

        
                remarks: ""

            });

            rowNumber = rowNumber + 1;

        }

    }

}



//新样式引入行end


var managerProcess;
$(function () {

    var form = $("#form").ligerForm();
  
  
    var txtGoodsName = $.ligerui.get("txtGoodsName");
    txtGoodsName.set("Width", 250);

    var txtOrderNumber = $.ligerui.get("txtOrderNumber");
    txtOrderNumber.set("Width", 250);

    var txtNum = $.ligerui.get("txtNum");
    txtNum.set("Width", 250);



    window['g'] =
    managerProcess = $("#maingrid9999").ligerGrid({
        columns: [

        {
            display: '', isSort: false, width: 60, align: 'center', frozen: true, render: function (rowdata, rowindex, value) {
                var h = "";
                if (!rowdata._editing) {
                    h += "<a href='javascript:addNewRow()' title='新增行' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                    h += "<a href='javascript:deleteRow()' title='删除行' style='float:left;'><div class='ui-icon ui-icon-trash'></div></a> ";
                    h += "<a href='javascript:f_selectContact()' title='选择工序' style='float:right;'><div class='ui-icon ui-icon-search'></div></a> ";
                }
                else {
                    //                        h += "<a href='javascript:endEdit(" + rowindex + ")'>提交</a> ";
                    //                        h += "<a href='javascript:cancelEdit(" + rowindex + ")'>取消</a> "; 
                }
                return h;
            }
        }
        ,

        {
            display: '工序名称', name: 'processName', width: 250, align: 'left',

            totalSummary:
             {
                 type: 'count',
                 render: function (e) {  //汇总渲染器，返回html加载到单元格
                     //e 汇总Object(包括sum,max,min,avg,count) 
                     return '合计：';
                 }
             }


        },



        { display: '单位', name: 'unitName', width: 100, align: 'center' },

        {
            display: '单品工序数量', name: 'num', width: 120, type: 'float', align: 'right', editor: { type: 'float' },

            totalSummary:
             {
                 align: 'right',   //汇总单元格内容对齐方式:left/center/right 
                 type: 'sum',
                 render: function (e) {  //汇总渲染器，返回html加载到单元格
                     //e 汇总Object(包括sum,max,min,avg,count) 
                     return Math.round(e.sum * 100) / 100;
                 }
             }


        },

        { display: '工序单价', name: 'price', width: 120, align: 'right', type: 'float', editor: { type: 'float' } },
        {
            display: '单品工序金额', name: 'sumPrice', width: 120, align: 'right', type: 'float', 
            totalSummary:
             {
                 align: 'right',   //汇总单元格内容对齐方式:left/center/right 
                 type: 'sum',
                 render: function (e) {  //汇总渲染器，返回html加载到单元格
                     //e 汇总Object(包括sum,max,min,avg,count) 
                     var itemSumPrice = e.sum;
                     return "<span id='sumPriceItem'>" + Math.round(itemSumPrice * 10000) / 10000 + "</span>";//formatCurrency(suminf.sum)


                 }
             }

        },


        { display: '备注', name: 'remarks', width: 220, align: 'left', type: 'text', editor: { type: 'text' } }
        ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '350',
        url: 'produceListAdd.aspx?Action=GetData',
        rownumbers: true,//显示序号
        frozenRownumbers: true,//行序号是否在固定列中
        dataAction: 'local',//本地排序
        usePager: false,
        alternatingRow: false,

        totalSummary: false,
        enabledEdit: true, //控制能否编辑的

        onAfterEdit: f_onAfterEdit //更新单元格后的操作
    }
    );
});
// 

//编辑后事件---------付款金额


function f_onAfterEdit(e) {


    
    var num, price, sumPrice;


    num = Number(e.record.num);

    price = Number(e.record.price);


   

    var processId, processName;

    processId = e.record.processId;
    processName = e.record.processName;

    if (processId == "" || processName == "") {
        return;
    }


  

    if (e.column.name == "num") //数量改变---开始
    {
        //数量改变：【折扣率、税率】 计算【折扣额、金额、税额、价税合计】
        num = Number(e.value);

      
        num = Math.round(num * 100) / 100;
        price = Math.round(price * 100) / 100;

        sumPrice = Math.round(sumPrice * 100) / 100;




        //开始赋值

        manager.updateCell("num", num, e.record);



        //2、金额
        manager.updateCell('sumPrice', sumPrice, e.record);








    } //数量改变---结束

    if (e.column.name == "price") //单价改变---开始、计算金额、折扣额、税额、价税合计
    {
        //单价改变：【数量、折扣率、税率】 计算【折扣额、金额、税额、价税合计】; 
        price = Number(e.value);



        //2、金额=数量*单价-折扣额
        sumPrice = Number(num) * Number(price);




        num = Math.round(num * 100) / 100;
        price = Math.round(price * 100) / 100;

        sumPrice = Math.round(sumPrice * 100) / 100;


        //开始赋值

        //1、折扣额

        manager.updateCell("price", price, e.record);


        //2、金额
        manager.updateCell('sumPrice', sumPrice, e.record);



    } //单价改变---结束

    //最后改变汇总行的值


    updateTotal();
  

}

function updateTotal() {


    var data = manager.getData();//getData
    var sumPriceItem = 0;//

    //1、先删掉空白行

    for (var i = data.length - 1; i >= 0; i--) {
        if (data[i].processId == 0 || data[i].processId == "" || data[i].processName == "") {
            data.splice(i, 1);

        }

    }

    for (var i = 0; i < data.length; i++) {

        sumPriceItem += Number(data[i].num) * Number(data[i].price);

    }

    $("#sumPriceItem").html(formatCurrency(sumPriceItem));



}



var rowNumber = 9;






function deleteRow() {

    if (manager.rows.length == 1) {
        $.ligerDialog.warn('至少保留一行！')

    }
    else {
        manager.deleteSelectedRow();


    }

}


var newrowid = 100;



function getSelected() {
    var row = manager.getSelectedRow();
    if (!row) { alert('请选择行'); return; }
    alert(JSON.stringify(row));
}
function getData() {
    var data = manager.getData();
    alert(JSON.stringify(data));
}


var manager;
$(function () {

    
    window['gBom'] =
    manager = $("#maingrid").ligerGrid({
        columns: [

        {
            display: '', isSort: false, width: 60, align: 'center', frozen: true, render: function (rowdata, rowindex, value) {
                var h = "";
                if (!rowdata._editing) {
                   // h += "<a href='javascript:addNewRow()' title='新增行' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
                    h += "<a href='javascript:deleteRow()' title='删除行' style='float:left;'><div class='ui-icon ui-icon-trash'></div></a> ";
                    //h += "<a href='javascript:f_selectContact()' title='选择物料' style='float:right;'><div class='ui-icon ui-icon-search'></div></a> ";
                }
                else {
                    //                        h += "<a href='javascript:endEdit(" + rowindex + ")'>提交</a> ";
                    //                        h += "<a href='javascript:cancelEdit(" + rowindex + ")'>取消</a> "; 
                }
                return h;
            }
        }
        ,
        { display: '商品编码', name: 'code', width: 100, align: 'center' },
        {
            display: '商品名称', name: 'goodsName', width: 250, align: 'left',

            totalSummary:
             {
                 type: 'count',
                 render: function (e) {  //汇总渲染器，返回html加载到单元格
                     //e 汇总Object(包括sum,max,min,avg,count) 
                     return '合计：';
                 }
             }


        },

        { display: '规格型号', name: 'spec', width: 100, align: 'center' },

        { display: '单位', name: 'unitName', width: 70, align: 'center' },

        {
            display: '标准用量', name: 'numBom', width: 80, type: 'float', align: 'right',

            totalSummary:
             {
                 align: 'right',   //汇总单元格内容对齐方式:left/center/right 
                 type: 'sum',
                 render: function (e) {  //汇总渲染器，返回html加载到单元格
                     //e 汇总Object(包括sum,max,min,avg,count) 
                     return Math.round(e.sum * 100) / 100;
                 }
             }


        },

        { display: '损耗率', name: 'rate', width: 60, align: 'right', type: 'float' },
        {
            display: '计划用量', name: 'num', width: 80, align: 'right', type: 'float',editor: { type: 'float' },
            totalSummary:
             {
                 align: 'right',   //汇总单元格内容对齐方式:left/center/right 
                 type: 'sum',
                 render: function (e) {  //汇总渲染器，返回html加载到单元格
                     //e 汇总Object(包括sum,max,min,avg,count) 
                     var itemSumPrice = e.sum;
                     return "<span id='sumPriceItem'>" + Math.round(itemSumPrice * 10000) / 10000 + "</span>";//formatCurrency(suminf.sum)


                 }
             }

        },

        //{ display: '仓库', name: 'ckName', width: 100, align: 'center', type: 'text' },

        { display: '备注', name: 'remarks', width: 220, align: 'left', type: 'text', editor: { type: 'text' } }
        ], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: '350',
        url: "produceListAdd.aspx?Action=GetData",
        rownumbers: true,//显示序号
        frozenRownumbers: true,//行序号是否在固定列中
        dataAction: 'local',//本地排序
        usePager: false,
        alternatingRow: false,

        totalSummary: false,
        enabledEdit: true, //控制能否编辑的

        onAfterEdit: f_onAfterEdit //更新单元格后的操作
    }
    );
});

function save() {


    var hfGoodsId = $("#hfGoodsId").val();
    if (hfGoodsId == "" || hfGoodsId==0) {
        $.ligerDialog.warn("请选择要生产的商品！");
        return;

    }

    var txtNum = $("#txtNum").val();
    if (txtNum == "" || txtNum == 0) {
        $.ligerDialog.warn("请输入生产数量！");
        return;

    }


    //先删掉空白行


    var data = manager.getData();
    // alert(JSON.stringify(data));

  

    //1、先删掉空白行
    for (var i = data.length - 1; i >= 0; i--) {
        if (data[i].goodsId == 0 || data[i].goodsName == "" || data[i].goodsId == "") {
            data.splice(i, 1);

        }

    }

    var isGongxu = false;

    //2、判断是否选择商品
    if (data.length == 0) {

        if (confirm("确认不添加用料计划？")) {
            isGongxu = false;
        }
        else {

            isGongxu = true;
 
        }
    }

    if (isGongxu)
    {
        if (data.length == 0)
        {
            $.ligerDialog.warn("请选择原料商品！");

            return;
            alert("我就不执行了！");

        }
        for (var i = 0; i < data.length; i++) {

            if (data[i].num <= 0 || data[i].num == "") {

                $.ligerDialog.warn("请输入第" + (i + 1) + "行的计划用量！");

                return;
                alert("我就不执行了！");
            }


        }

    }

    
    




    var dateStart = $("#txtDateStart").val();
    if (dateStart == "") {
        $.ligerDialog.warn("请输入开始日期！");
        return;

    }

    var dateEnd = $("#txtDateEnd").val();
    if (dateStart == "") {
        $.ligerDialog.warn("请输入结束日期！");
        return;

    }


    var typeName = $("#ddlTypeName").val();
  
    var orderNumber = $("#txtOrderNumber").val();

    var remarks = $("#txtRemarks").val();

   
    var headJson = {
        orderNumber: orderNumber,
        typeName: typeName,
        goodsId: hfGoodsId,
        num: txtNum,
        dateStart: dateStart,
        dateEnd: dateEnd,
        remarks: remarks
    };

    // alert(JSON.stringify(headJson));

    var dataNew = [];

    dataNew.push(headJson);



    var list = JSON.stringify(headJson);//返序列化成字符串、表头


    list = list.substring(0, list.length - 1);//去掉最后一个花括号

    list += ",\"Rows\":";
    list += JSON.stringify(data);//插入账户信息   

    list += "}";



    var postData = JSON.parse(list);//最终的json

    //   alert(JSON.stringify(postData));

    //  return;
    //        alert(postData.Rows[0].id);
    //        
    //        alert(postData.bizDate);
    //        
    //        alert(postData.Rows[0].goodsName);

     //   alert(JSON.stringify(postData));

    //       $("#txtRemarks").val(JSON.stringify(postData));

    // return;

    $.ajax({
        type: "POST",
        url: 'ashx/produceListAdd.ashx',
        contentType: "application/json", //必须有
        //dataType: "json", //表示返回值类型，不必须
        data: JSON.stringify(postData),  //相当于 //data: "{'str1':'foovalue', 'str2':'barvalue'}",
        success: function (jsonResult) {

            if (jsonResult == "操作成功！") {

                $.ligerDialog.waitting('操作成功！'); setTimeout(function () { $.ligerDialog.closeWaitting(); location.reload(); }, 2000);

            }
            else {
                $.ligerDialog.warn(jsonResult);

            }
        },
        error: function (xhr) {
            alert("出现错误，请稍后再试:" + xhr.responseText);
        }
    });


}

function getBomList()
{

    var goodsId = $("#hfGoodsId").val();
    var num= $("#txtNum").val();

    //alert("goodsId:" + goodsId + " num:" + num);

    if (goodsId > 0 && num > 0) {
        manager.changePage("first");
        manager._setUrl("produceListAdd.aspx?Action=GetDataBom&goodsId=" + goodsId + "&num=" + num);
    }


}
$(function () {
    // Client
    $("#clientName").ligerComboBox({
        onBeforeOpen: f_selectClient, valueFieldID: 'clientId', width: 250
    });

    $("#txtFlagList").ligerComboBox({
        isShowCheckBox: true, isMultiSelect: true,
        data: [
            { text: 'Unproduce', id: '1' },
            { text: 'Progressing', id: '2' },
            { text: 'Completed', id: '44' }
        ], valueFieldID: 'flag'
    });

    // Products
    $("#txtGoodsName").ligerComboBox({
        onBeforeOpen: f_selectGoods, valueFieldID: 'hfGoodsId', width: 300
    });

    // Manager
    InitializeManager();
});

function f_selectClient() {
    $.ligerDialog.open({
        title: 'Choose vendor', name: 'winselector', width: 800, height: 540, url: '../baseSet/ClientListSelect.aspx', buttons: [
            { text: 'Confirm', onclick: f_selectClientOK },
            { text: 'Close', onclick: f_selectClientCancel }
        ]
    });
    return false;
}

function f_selectClientOK(item, dialog) {
    var fn = dialog.frame.f_select || dialog.frame.window.f_select;
    var data = fn();
    if (!data) {
        alert('Please select row!');
        return;
    }

    $("#clientName").val(data.names);
    $("#clientId").val(data.code);
    dialog.close();
}

function f_selectClientCancel(item, dialog) {
    dialog.close();
}

function f_selectGoods() {
    $.ligerDialog.open({
        title: 'Select Goods', name: 'winselector', width: 800, height: 540, url: '../baseSet/GoodsListSelect.aspx?type=1', buttons: [
            { text: 'Confirm', onclick: f_selectGoodsOK },
            { text: 'Close', onclick: f_selectGoodsCancel }
        ]
    });
    return false;
}

function f_selectGoodsOK(item, dialog) {
    var fn = dialog.frame.f_select || dialog.frame.window.f_select;
    var data = fn();
    if (data.length == 0) {
        $.ligerDialog.warn("Please select product！");

        return;
    }

    if (data.length > 1) {
        $.ligerDialog.warn("Please select only one product！");
        return;

    }

    $("#txtGoodsName").val(data[0].names);
    $("#txtGoodsId").val(data[0].code);
    dialog.close();
}

function f_selectGoodsCancel(item, dialog) {
    dialog.close();
}

var manager;
function InitializeManager() {
    var menu = $.ligerMenu({
        width: 120, items:
            [
                { text: 'Check order detail', click: viewRow },
                { line: true },
                { text: 'Check warehouse detail', click: viewRow },
            ]
    });

    $("#form").ligerForm();

    var dateStart = $.ligerui.get("txtDateStart");
    dateStart.set("Width", 110);

    var dateEnd = $.ligerui.get("txtDateEnd");
    dateEnd.set("Width", 110);

    var txtFlagList = $.ligerui.get("txtFlagList");
    txtFlagList.set("Width", 100);

    manager = $("#maingrid").ligerGrid({
        columns: [
            {
                display: 'Schedule Date', name: 'makeDate', width: 150, align: 'center', valign: 'center',
                totalSummary:
                {
                    type: 'count',
                    render: function (e) { 
                        return 'Sum：';
                    }
                }
            },
            { display: 'Invoice No.', name: 'number', width: 150, align: 'center' },
            { display: 'Schedule Type', name: 'typeName', width: 150, align: 'center' },
            { display: 'Order No.', name: 'orderNumber', width: 150, align: 'center' },
            { display: 'Vendor', name: 'wlName', width: 170, align: 'left' },
            { display: 'Product Id', name: 'code', width: 100, align: 'center' },
            { display: 'Product Name', name: 'goodsName', width: 120, align: 'center' },
            { display: 'Specification', name: 'spec', width: 100, align: 'center' },
            { display: 'Unit', name: 'unitName', width: 70, align: 'center' },
            {
                display: 'Planned Quantity', name: 'num', width: 120, align: 'center',
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
                display: 'Produced Quantity', name: 'finishNum', width: 120, align: 'right',
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
                display: 'Remaining Quantity', name: 'finishNumNo', width: 150, align: 'right',
                totalSummary:
                {
                    align: 'right',   //left/center/right 
                    type: 'sum',    //(sum,max,min,avg,count) 
                    render: function (e) {  
                        return Math.round(e.sum * 100) / 100;
                    }
                }
            },
            { display: 'Progress', name: 'sendFlag', width: 60, align: 'center' },
            { display: 'Plan Start Date', name: 'dateStart', width: 120, align: 'center', valign: 'center' },
            { display: 'Plan End Date', name: 'dateEnd', width:120, align: 'center', valign: 'center' },
            { display: 'Delivery Date', name: 'sendDate', width: 120, align: 'center' },
            { display: 'Invoice Status', name: 'flag', width: 120, align: 'center' },
            { display: 'Created By', name: 'makeName', width: 90, align: 'center' },
            { display: 'Reviewed By', name: 'checkName', width: 90, align: 'center' },
            { display: 'Remarks', name: 'remarks', width: 100, align: 'left' }

        ],
        width: '98%',
        //pageSizeOptions: [5, 10, 15, 20],
        height: '98%',
        // pageSize: 15,
        dataAction: 'local', //Order
        usePager: false,
        rownumbers: true,//Display index
        alternatingRow: false,
        onDblClickRow: function (data, rowindex, rowobj) {
            viewRow();
        },
        allowUnSelectRow: true,
        onRClickToSelect: true,
        onContextmenu: function (parm, e) {
            actionCustomerID = parm.data.id;
            menu.show({ top: e.pageY, left: e.pageX });
            return false;
        }
    });
}

function search() {
    var start = $("#txtDateStart").val();
    var end = $("#txtDateEnd").val();
    var wlId = $("#clientId").val();
    var goodsList = $("#txtGoodsId").val();
    var typeId = $("#txtFlagList").val();
    var wlIdString = wlId.split(";");
    var goodsIdString = goodsList.split(";");
    var typeIdString = typeId.split(";");

    if (wlIdString != "") {
        wlId = "";
        for (var i = 0; i < wlIdString.length; i++) {
            wlId += "'" + wlIdString[i] + "'" + ",";
        }
        wlId = wlId.substring(0, wlId.length - 1);
    }

    if (goodsIdString != "") {
        goodsList = "";
        for (var i = 0; i < goodsIdString.length; i++) {
            goodsList += "'" + goodsIdString[i] + "'" + ",";
        }
        goodsList = goodsList.substring(0, goodsList.length - 1);
    }

    if (typeIdString != "") {
        typeId = "";
        for (var i = 0; i < typeIdString.length; i++) {
            typeId += "'" + typeIdString[i] + "'" + ",";
        }
        typeId = typeId.substring(0, typeId.length - 1);
    }

    var keys = $("#txtKeys").val();
    if (keys == "Please enter order no/remark") {
        keys = "";
    }
  
    manager._setUrl("ProduceListReport.aspx?Action=GetDataList&keys=" + keys + "&start=" + start + "&end=" + end + "&wlId=" + wlId + "&goodsId=" + goodsList + "&typeId=" + typeId);
}

function viewRow() {
    var row = manager.getSelectedRow();

    //          top.topManager.openPage({
    //            id : 'purOrderListView',
    //            href : 'buy/purOrderListView.aspx?id='+row.id,
    //            title : 'Purchase order detail'
    //          });
}

function reload() {
    manager.reload();
}
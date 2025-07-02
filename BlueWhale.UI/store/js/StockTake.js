var manager;
$(function () {
    var form = $("#form").ligerForm();
    var txtFlagList = $.ligerui.get("txtFlagList");

    txtFlagList.set("Width", 160);

    manager = $("#maingrid").ligerGrid({
        columns: [
            { display: 'Store', name: 'ckName', width: 120, align: 'center', frozen: true },
            { display: 'Product No', name: 'code', width: 120, align: 'center' },
            { display: 'Product Name', name: 'goodsName', width: 220, align: 'left' },
            { display: 'Specification', name: 'spec', width: 120, align: 'center' },
            { display: 'Unit', name: 'unitName', width: 120, align: 'center' },
            { display: 'System Inventory', name: 'sumNum', width: 130, align: 'right' },
            { display: 'Stocktake', name: 'sumNumPD', width: 100, align: 'right', editor: { type: 'float' } },
            {
                display: 'Surplus and Shortage', name: 'sumNumPK', width: 140, align: 'right',
                render: function (row) {
                    var html = row.sumNumPK > 0 ? "<span style='color:red'>" + row.sumNumPK + "</span>" : "<span style='color:green'>" + row.sumNumPK + "</span>";
                    return html;
                }
            }
        ], width: '98%',
        //pageSizeOptions: [5, 10, 15, 20],
        height: '450',
        // pageSize: 15,
        dataAction: 'local', 
        usePager: false,
        rownumbers: true,
        enabledEdit: true, 
        alternatingRow: false,
        onAfterEdit: f_onAfterEdit 
    }
    );
});

function search() {
    var cangkuId = $("#txtFlagList").val();
    var typeList = $("#txtTypeList").val();
    var goodsList = $("#txtGoodsList").val();
    var cangkuIdString = cangkuId.split(";");
    var typeIdString = typeList.split(";");
    var goodsIdString = goodsList.split(";");

    if (cangkuIdString != "") {
        cangkuId = "";
        for (var i = 0; i < cangkuIdString.length; i++) {
            cangkuId += "'" + cangkuIdString[i] + "'" + ",";
        }
        cangkuId = cangkuId.substring(0, cangkuId.length - 1);
    }

    if (typeIdString != "") {
        typeList = "";
        for (var i = 0; i < typeIdString.length; i++) {
            typeList += "'" + typeIdString[i] + "'" + ",";
        }
        typeList = typeList.substring(0, typeList.length - 1);
    }


    if (goodsIdString != "") {
        goodsList = "";
        for (var i = 0; i < goodsIdString.length; i++) {
            goodsList += "'" + goodsIdString[i] + "'" + ",";
        }
        goodsList = goodsList.substring(0, goodsList.length - 1);
    }
    //return ;

    manager._setUrl("StockTake.aspx?Action=GetDataList&goodsId=" + goodsList + "&typeId=" + typeList + "&ckId=" + cangkuId);
}


function f_onAfterEdit(e) {
    var sumNum, sumNumPD, sumPK;

    sumNumPD = Number(e.record.num);

    if (e.column.name == "sumNumPD") 
    {
    
        sumNumPD = Number(e.value);
        sumNumPK = Number(e.record.sumNum) - sumNumPD;
      
        manager.updateCell("sumNumPK", sumNumPK, e.record);
    } 
    //manager.reRender();
    //  manager.totalRender();
}
function reload() {
    manager.reload();
}

function save() {
    var data = manager.getData();

    for (var i = data.length - 1; i >= 0; i--) {
        if (data[i].goodsId == 0 || data[i].goodsName == "") {
            data.splice(i, 1);
        }
    }

    if (data.length == 0) {
        $.ligerDialog.warn('Please Select Product！');

        return;
    }

    for (var i = 0; i < data.length; i++) {
        //|| data[i].sumNumPD=="0" || data[i].sumNumPD=="0.00"

        if (data[i].sumNumPD < 0 || data[i].sumNumPD == "") {
            $.ligerDialog.warn("请输入第" + (i + 1) + "行的商品数量！");

            return;
            alert("我就不执行了！");
        }
    }

    var remarks = $("#txtRemarks").val();
    var headJson = { remarks: remarks };
    var dataNew = [];

    dataNew.push(headJson);

    var list = JSON.stringify(headJson);
    var goodsList = [];

    list = list.substring(0, list.length - 1);
    list += ",\"Rows\":";
    list += JSON.stringify(data);
    list += "}";

    var postData = JSON.parse(list);//last json

    //        alert(postData.Rows[0].id);
    //        
    //        alert(postData.bizDate);
    //        
    //        alert(postData.Rows[0].goodsName);

    //        alert(JSON.stringify(postData));

    //       $("#txtRemarks").val(JSON.stringify(postData));

    $.ajax({
        type: "POST",
        url: 'ashx/StockTake.ashx',
        contentType: "application/json", 
        //dataType: "json", 
        data: JSON.stringify(postData),  //aka //data: "{'str1':'foovalue', 'str2':'barvalue'}",
        success: function (jsonResult) {

            if (jsonResult == "Success！") {
                $.ligerDialog.waitting('Success！'); setTimeout(function () { $.ligerDialog.closeWaitting(); location.reload(); }, 2000);
            }
            else {
                $.ligerDialog.warn(jsonResult);
            }
        },
        error: function (xhr) {
            alert("Error，Please Try Again Later:" + xhr.responseText);
        }
    });
}

var manager;
$(function () {
    var bizTypeData =
        [
            { id: 0, text: 'All Category' },
            { id: 1, text: 'Other Income' },
            { id: 2, text: 'Other Expenses' }
        ];
    $("#txtItemList").ligerComboBox({ data: null, isMultiSelect: true, isShowCheckBox: true, valueFieldID: 'itemId' });

    $("#ddlTypeList").ligerComboBox({
        data: bizTypeData, isMultiSelect: false, valueFieldID: 'bizTypeId',
        onSelected: function (newvalue) {
            var typeName = "ALL";
            if (newvalue == "1") {
                typeName = "Income";
            }
            if (newvalue == "2") {
                typeName = "Expenses";

            }
            liger.get("txtItemList")._setUrl("../baseSet/PayGetList.aspx?Action=GetDDLList&type=" + typeName + "&r=" + Math.random());
        }
    });
    var form = $("#form").ligerForm();
    var dateStart = $.ligerui.get("txtDateStart");
    dateStart.set("Width", 110);
    var dateEnd = $.ligerui.get("txtDateEnd");
    dateEnd.set("Width", 110);
    var txtItemList = $.ligerui.get("txtItemList");
    txtItemList.set("Width", 220);
    manager = $("#maingrid").ligerGrid({
        columns: [
            { display: 'Bussiness Date', name: 'bizDate', width: 120, align: 'center' },
            { display: 'Account Number', name: 'code', width: 120, align: 'center' },
            { display: 'Account Name', name: 'bkName', width: 120, align: 'center' },
            { display: 'Receipt Number', name: 'number', width: 150, align: 'center' },
            { display: 'Business Category', name: 'bizType', width: 130, align: 'center' },
            { display: 'Business Partner', name: 'wlName', width: 170, align: 'left' },
            {
                display: 'Expenses Category', name: 'typeName', width: 130, align: 'center',
                totalSummary:
                {
                    type: 'count',
                    render: function (e) { 
                        return 'Total：';
                    }
                }
            },
            {
                display: 'Income', name: 'priceIn', width: 80, align: 'right',
                totalSummary:
                {
                    align: 'center',
                    type: 'sum',
                    render: function (e) {                     
                        return e.sum.toFixed(2);
                    }
                }
            },
            {
                display: 'Expenses', name: 'priceOut', width: 80, align: 'right',

                totalSummary:
                {
                    align: 'center',
                    type: 'sum',
                    render: function (e) { 
                        return e.sum.toFixed(2)
                    }
                }
            },
            { display: 'Handled By', name: 'bizName', width: 100, align: 'center' },

            { display: 'Summary', name: 'remarks', width: 170, align: 'left' },

            { display: 'Remark', name: 'remarksMain', width: 170, align: 'left' },

            { display: 'Status', name: 'flag', width: 70, align: 'center' }
        ], width: '98%',
        height: '98%',
        dataAction: 'local',
        usePager: false,
        rownumbers: true,
        alternatingRow: false,
        onDblClickRow: function (data, rowindex, rowobj) {
            viewRow();
        }
    }
    );

});
function search(down) {
    var start = $("#txtDateStart").val();
    var end = $("#txtDateEnd").val();
    var bizType = $("#bizTypeId").val();
    if (bizType == "0" || bizType == "") {
        bizType = "ALL";

    }
    if (bizType == "1") {
        bizType = 'Other Income';

    }
    if (bizType == "2") {
        bizType = 'Other Expenses';

    }
    var typeId = $("#itemId").val();

    var bizId = $("#ddlYWYList").val();

    var typeIdString = typeId.split(";");

    if (typeIdString != "") {
        typeId = "";
        for (var i = 0; i < typeIdString.length; i++) {
            typeId += typeIdString[i] + ",";
        }
        typeId = typeId.substring(0, typeId.length - 1);

    }

    var path = new Date().getTime();

    var url = "OtherGetPayFlowReport.aspx?Action=GetDataList&start=" + start
        + "&end=" + end
        + "&bizType=" + bizType
        + "&typeIdString=" + typeIdString
        + "&bizId=" + bizId
        + "&down=" + down + "&path=" + path;
    manager._setUrl(url);

    if (down == 1) {
        setTimeout(function () {

            window.open("../excel/OtherGetPayFlowReport" + path + ".xls");

        }, 3000);
    }
}
function viewRow() {
    var row = manager.getSelectedRow();
}

function reload() {
    manager.reload();
}
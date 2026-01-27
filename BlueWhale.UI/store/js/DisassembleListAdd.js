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

var itemType = "main";
function f_selectContact(type) {
	if (type == 0) {
		itemType = "main";
	}
	if (type == 1) {
		itemType = "sub";
	}

	$.ligerDialog.open({
		title: 'Select goods', name: 'winselector', width: 840, height: 540, url: '../baseSet/GoodsListSelect.aspx', buttons: [
			{ text: 'OK', onclick: f_selectContactOK },
			{ text: 'Cancel', onclick: f_selectContactCancel }
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

	if (itemType == "main") {
		f_onGoodsChanged(data);
	}
	if (itemType == "sub") {
		f_onGoodsChangedSub(data);
	}
	dialog.close();
}
function f_selectContactCancel(item, dialog) {
	dialog.close();
}

$.ligerDefaults.Grid.formatters['numberbox'] = function (value, column) {
	var precision = column.editor.precision;
	return value.toFixed(precision);
};
var manager;
$(function () {
	var form = $("#form").ligerForm();

	window['g'] =
		manager = $("#maingrid").ligerGrid({
			columns: [
				{
					display: '', isSort: false, width: 60, align: 'center', frozen: true, render: function (rowdata, rowindex, value) {
						var h = "";
						if (!rowdata._editing) {
							h += "<a href='javascript:f_selectContact(0)' title='Select goods'><div class='ui-icon ui-icon-search' style='margin:0 auto;'></div></a> ";
						}
						return h;
					}
				},
				{
					display: 'Item Name', name: 'goodsName', width: 250, align: 'left',
					totalSummary:
					{
						type: 'count',
						render: function (e) {  
							return 'Total：';
						}
					}
				},
				{ display: 'Specification', name: 'spec', width: 100, align: 'center' },
				{ display: 'Unit Name', name: 'unitName', width: 80, align: 'center' },
				{ display: 'Outbound quantity', name: 'num', width: 180, type: 'float', align: 'right', editor: { type: 'float' } },
				{
					display: 'Outbound unit price', name: 'price', width: 180, type: 'float', align: 'right', editor: { type: 'float', precision: 4 }
				},
				{ display: 'Total Price', name: 'sumPrice', width: 80, type: 'float', align: 'right', editor: { type: 'float' } },
				{
					display: 'Outbound warehouse', name: 'ckId', width: 180, isSort: false, textField: 'ckName',
					editor: {
						type: 'select',
						url: "../baseSet/InventoryList.aspx?Action=GetDDLList&r=" + Math.random(),
						valueField: 'ckId', textField: 'ckName'
					}
				},
				{ display: 'Remarks', name: 'remarks', width: 150, align: 'left', type: 'text', editor: { type: 'text' } }
			], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: 'auto',
			url: 'DisassembleListAdd.aspx?Action=GetData',
			rownumbers: true,
			frozenRownumbers: true,
			dataAction: 'local',
			usePager: false,
			alternatingRow: false,
			totalSummary: false,
			enabledEdit: true, 
			onAfterEdit: f_onAfterEdit
		}
		);
});

var managersub;
$(function () {
	window['gsub'] =
		managersub = $("#maingridsub").ligerGrid({
			columns: [
				{
					display: '', isSort: false, width: 60, align: 'center', frozen: true, render: function (rowdata, rowindex, value) {
						var h = "";
						if (!rowdata._editing) {
							h += "<a href='javascript:addNewRow()' title='NewRow' style='float:left;'><div class='ui-icon ui-icon-plus'></div></a> ";
							h += "<a href='javascript:deleteRow()' title='DeleteRow' style='float:left;'><div class='ui-icon ui-icon-trash'></div></a> ";
							h += "<a href='javascript:f_selectContact(1)' title='SelectGood' style='float:left;'><div class='ui-icon ui-icon-search'></div></a> ";
						}
						return h;
					}
				},
				{
					display: 'Item Name', name: 'goodsName', width: 250, align: 'left',
					totalSummary:
					{
						type: 'count',
						render: function (e) {  
							return 'Total：';
						}
					}
				},
				{ display: 'Specification', name: 'spec', width: 100, align: 'center' },
				{ display: 'Unit', name: 'unitName', width: 80, align: 'center' },
				{
					display: 'Quantity in stock', name: 'num', width: 180, type: 'float', align: 'right', editor: { type: 'float' },
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
					display: 'Unit Price', name: 'price', width: 70, type: 'float', align: 'right', editor: { type: 'float', precision: 4 }
				},
				{
					display: 'Total Price', name: 'sumPrice', width: 80, type: 'float', align: 'right', editor: { type: 'float' },
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
					display: 'Inbound warehouse', name: 'ckId', width: 180, isSort: false, textField: 'ckName',
					editor: {
						type: 'select',
						url: "../baseSet/InventoryList.aspx?Action=GetDDLList&r=" + Math.random(),
						valueField: 'ckId', textField: 'ckName'
					}
				},
				{ display: 'Remarks', name: 'remarks', width: 150, align: 'left', type: 'text', editor: { type: 'text' } }
			], width: '99%', pageSizeOptions: [5, 10, 15, 20], height: 'auto',
			url: 'DisassembleListAdd.aspx?Action=GetDataSub',
			rownumbers: true,
			frozenRownumbers: true,
			dataAction: 'local',
			usePager: false,
			alternatingRow: false,
			totalSummary: true,
			enabledEdit: true, 
			onAfterEdit: f_onAfterEditSub
		}
		);
});

function updateTotal() {
	var data = managersub.getData();//getData
	var sumPriceItem = 0;
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

var rowNumber = 9;

function f_totalRender(data, currentPageData) {
}

function setInventory() {
	$.ligerDialog.open({ target: $("#target1") });
}

function selectInventory() {
	var ckName = $("#ddlInventoryList").find("option:selected").text();  
	var ckId = $("#ddlInventoryList").val();  
	alert(ckName);
	alert(ckId);
	var grid = liger.get("maingrid");
	var data = manager.getData();
	alert(data.length);
	for (var i = 0; i < data.length; i++) {
		alert(data[i].goodsId);
		grid.updateCell("ckId", ckId, i);
		grid.updateCell("ckName", ckName, i);
	}
	$(".l-dialog,.l-window-mask").remove();
	//             var dialog = frameElement.dialog;
	//             dialog.close(); 
	$.ligerDialog.close(); 
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
}

function f_onGoodsChangedSub(e) {
	if (!e || !e.length) return;
	var grid = liger.get("maingridsub");
	var selected = e[0];// e.data[0]; 
	// alert(selected.names);

	var selectedRow = managersub.getSelected();
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
		var data = managersub.getData();
		for (var i = data.length - 1; i >= 0; i--) {
			if (data[i].goodsId == 0 || data[i].goodsName == "") {
				managersub.deleteRow(i);
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
	updateTotal();
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
	$("#message").html('Selected:' + out);
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
}

function f_onAfterEditSub(e) {
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
		managersub.updateCell("num", num, e.record);
		managersub.updateCell('sumPrice', sumPrice, e.record);
	}

	if (e.column.name == "price") 
	{
		price = Number(e.value);
		sumPrice = Number(num) * Number(price);
		num = Math.round(num * 100) / 100;
		price = Math.round(price * 100) / 100;
		sumPrice = Math.round(sumPrice * 100) / 100;
		managersub.updateCell("price", price, e.record);
		managersub.updateCell('sumPrice', sumPrice, e.record);
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
		managersub.updateCell("price", price, e.record);
		managersub.updateCell('sumPrice', sumPrice, e.record);
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

	if (managersub.rows.length == 1) {
		$.ligerDialog.warn('Keep at least one row！')

	}
	else {
		managersub.deleteSelectedRow();
	}

}
var newrowid = 100;

function addNewRow() {
	var gridData = managersub.getData();
	var rowNum = gridData.length;

	managersub.addRow({
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
		$.ligerDialog.warn('Please select Disassemble Goods！');
		return;
		alert("Execution skipped！");
	}

	for (var i = 0; i < data.length; i++) {
		if (data[i].num <= 0 || data[i].num == "" || data[i].num == "0" || data[i].num == "0.00") {
			$.ligerDialog.warn("Please enter the " + (i + 1) + " number of disassembled items in the row！");
			return;
			alert("Execution skipped！");
		}

		if (data[i].ckId == 0 || data[i].ckId == "" || data[i].ckId == "0" || data[i].ckName == "") {
			$.ligerDialog.warn("Please enter the " + (i + 1) + " number of disassembled items in the row！");
			return;
			alert("Execution skipped！");
		}
	}

	var goodsId = data[0].goodsId;
	var num = data[0].num;
	var price = data[0].price;
	var ckId = data[0].ckId;
	var remarksItem = data[0].remarks;
	var datasub = managersub.getData();

	for (var i = datasub.length - 1; i >= 0; i--) {
		if (datasub[i].goodsId == 0 || datasub[i].goodsName == "") {
			datasub.splice(i, 1);
		}
	}
	if (datasub.length == 0) {
		$.ligerDialog.warn('Please Select Disassembled items！');
		return;
		alert("Execution skipped！");
	}

	for (var i = 0; i < datasub.length; i++) {
		if (datasub[i].num <= 0 || datasub[i].num == "" || datasub[i].num == "0" || datasub[i].num == "0.00") {
			$.ligerDialog.warn("Please enter the " + (i + 1) + " number of disassembled items in the ro");
			return;
			alert("Execution skipped！");
		}

		if (datasub[i].ckId == 0 || datasub[i].ckId == "" || datasub[i].ckId == "0" || datasub[i].ckName == "") {
			$.ligerDialog.warn("Please enter the " + (i + 1) + " number of disassembled items in the ro");
			return;
			alert("Execution skipped！");
		}
	}

	var bizDate = $("#txtBizDate").val();
	if (bizDate == "") {
		$.ligerDialog.warn("Please enter Disassemble Date！");
		return;
	}

	var remarks = $("#txtRemarks").val();
	var fee = $("#txtFee").val() == "" ? 0 : $("#txtFee").val();
	var headJson = { fee: fee, bizDate: bizDate, remarks: remarks, goodsId: goodsId, num: num, price: price, ckId: ckId, remarksItem: remarksItem };
	var dataNew = [];
	dataNew.push(headJson);
	var list = JSON.stringify(headJson);
	var goodsList = [];
	list = list.substring(0, list.length - 1);
	list += ",\"Rows\":";
	list += JSON.stringify(datasub);
	list += "}";

	var postData = JSON.parse(list);
	//        alert(postData.Rows[0].id);
	//        
	//        alert(postData.bizDate);
	//        
	//        alert(postData.Rows[0].goodsName);

	//    alert(JSON.stringify(postData));

	//       $("#txtRemarks").val(JSON.stringify(postData));

	// return;

	$.ajax({
		type: "POST",
		url: 'ashx/DisassembleListAdd.ashx',
		contentType: "application/json", 
		//dataType: "json", 
		data: JSON.stringify(postData),  
		success: function (jsonResult) {

			if (jsonResult == "Execution successful！") {
				$.ligerDialog.waitting('Execution successful！'); setTimeout(function () { $.ligerDialog.closeWaitting(); location.reload(); }, 2000);
			}
			else {
				$.ligerDialog.warn(jsonResult);
			}
		},
		error: function (xhr) {
			alert("Execution An error occurred, please try again later: " + xhr.responseText);
		}
	});
}
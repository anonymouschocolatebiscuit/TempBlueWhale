var manager;
$(function ()
{
    var form = $("#form").ligerForm();

    var menu = $.ligerMenu({ width: 120, items:
        [
            { text: 'Add', click: add, icon: 'add' },
            { text: 'Edit', click: editRow },
            { line: true },
            { text: 'View', click: viewRow }
        ]
    }); 
        
    manager = $("#maingrid").ligerGrid({
    checkbox: true,
        columns: [
            {
                display: 'Action', isSort: false, width: 60, align: 'center', render: function (rowdata, rowindex, value) {
                    var h = "";
                    if (!rowdata._editing) {
                        h += "<a href='javascript:editRow()' title='Edit Row' style='float:left;'><div class='ui-icon ui-icon-pencil'></div></a> ";
                        h += "<a href='javascript:deleteRow()' title='Delete row' style='float:right;'><div class='ui-icon ui-icon-trash'></div></a> ";
                    }
                    else {
                        h += "<a href='javascript:endEdit(" + rowindex + ")'>Submit</a> ";
                        h += "<a href='javascript:cancelEdit(" + rowindex + ")'>Cancel</a> ";
                    }
                    return h;
                }
            },
            {
                display: 'Inbound Date', name: 'bizDate', width: 130, align: 'center', valign: 'center',
                totalSummary:
                {
                    type: 'count',
                    render: function (e) 
                    {  //Summary renderer, returns HTML to load into the cell
                        //e Summary Object (including sum, max, min, avg, count)
                        return 'Total:';
                    }
                }
            },
            { display: 'Receipt Number', name: 'number', width: 150, align: 'center' },
            {
                display: 'Business Type', name: 'types', width: 130, align: 'center',
                render: function (row) {  
                    var html = row.types == 1 ? "Other Inbound" : "<span style='color:green'>Overage Inbound</span>";  
                    return html;
                }   
            },
            { display: 'Supplier', name: 'wlName', width: 170, align: 'left'},
            { display: 'Inbound Amount', name: 'sumPriceAll', width: 160, align: 'right',
                totalSummary:
                {
                    align: 'right',   //Summary cell content alignment: left/center/right
                    type: 'sum',
                    render: function (e) 
                    {  //Summary renderer, returns HTML to load into the cell
                        //e Summary Object (including sum, max, min, avg, count)
                        return Math.round(e.sum * 100) / 100;
                    }
                }
            },
            {
                display: 'Quantity', name: 'sumNum', width: 100, align: 'center',
                totalSummary:
                {
                    align: 'right',   //Summary cell content alignment: left/center/right
                    type: 'sum',
                    render: function (e) 
                    {  //Summary renderer, returns HTML to load into the cell
                        //e Summary Object (including sum, max, min, avg, count)
                        return Math.round(e.sum * 100) / 100;
                    }
                }
            },
            { display: 'Status', name: 'flag', width: 80, align: 'center'},
            { display: 'Created By', name: 'makeName', width: 120, align: 'center' },
            { display: 'Reviewed By', name: 'checkName', width: 130, align: 'center' },
            { display: 'Remark', name: 'remarks', width: 100, align: 'left' }
        ],
        width: '98%', 
        //pageSizeOptions: [5, 10, 15, 20],
        height:'98%',
        // pageSize: 15,
        dataAction: 'local', //Local sorting
        usePager: false,
        url: "OtherInList.aspx?Action=GetDataList", 
        alternatingRow: false,
        onDblClickRow: function(data, rowindex, rowobj) {
            viewRow();
        },
        onRClickToSelect:true,
        onContextmenu : function (parm,e)
        {
            actionCustomerID = parm.data.id;
            menu.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        isChecked: f_isChecked, onCheckRow: f_onCheckRow, onCheckAllRow: f_onCheckAllRow
    });
});

function f_set()
{
    form.setData({
        keys:"",
        dateStart: new Date("<% =start%>"),
        dateEnd: new Date("<% =end%>")
    });
}

function search()
{
    var keys = document.getElementById("txtKeys").value;
    if (keys == "Please enter Receipt No./Supplier/Remarks") {
        keys = "";
    }
    var start = document.getElementById("txtDateStart").value;
    var end = document.getElementById("txtDateEnd").value;

    if (start && end) {
        var startDate = new Date(start);
        var endDate = new Date(end);

        if (isNaN(startDate.getTime()) || isNaN(endDate.getTime())) {
            $.ligerDialog.alert("Invalid date format. Please check your input.", "Notification");
            return;
        }

        if (startDate > endDate) {
            $.ligerDialog.alert("Start date cannot be later than end date.", "Notification");
            return;
        }
    }

    manager.setOptions({
        url: "OtherInList.aspx",
        parms: {
            Action: "GetDataListSearch",
            types: 0,
            keys: keys.trim(),
            start: start,
            end: end
        }
    });

    manager.loadData();
}
       
function deleteRow()
{
    var row = manager.getSelectedRow();
    if (!row)
    {
        $.ligerDialog.warn('Please select the row(s) to delete');
        return;
    }
            
    var idString = checkedCustomer.join(',');

    $.ligerDialog.confirm('Cannot be recovered after clearing. Confirm deletion？', function(type) {
        if (type) {
            $.ajax({
                type: "GET",
                url: "OtherInList.aspx",
                data: "Action=delete&id=" + idString + " &ranid=" + Math.random(),
                success: function(resultString) {
                    $.ligerDialog.alert(resultString, 'Notification');
                    reload();
                },
                error: function(msg) {
                    $.ligerDialog.alert("Network error, please contact the administrator", 'Notification');
                }
            });
        }
    });
}
        
function checkRow()
{
    var row = manager.getSelectedRow();
    if (!row)
    {
        $.ligerDialog.warn('Please select the row to operate on');
        return;
    }
            
    var idString = checkedCustomer.join(',');

    $.ajax({
        type: "GET",
        url: "OtherInList.aspx",
        data: "Action=checkRow&idString=" + idString + "&ranid=" + Math.random(), //encodeURI
        success: function(resultString) {
            $.ligerDialog.alert(resultString, 'Notification');
            reload();
        },
        error: function(msg) {
            $.ligerDialog.alert("Network error, please contact the administrator", 'Notification');
        }
    });
}
        
function checkNoRow()
{
    var row = manager.getSelectedRow();
    if (!row)
    {
        $.ligerDialog.warn('Please select the row to operate on');
        return;
    }

    var idString = checkedCustomer.join(',');

    $.ajax({
        type: "GET",
        url: "OtherInList.aspx",
        data: "Action=checkNoRow&idString=" + idString + "&ranid=" + Math.random(), //encodeURI
        success: function(resultString) {
            $.ligerDialog.alert(resultString, 'Notification');
            reload();
        },
        error: function(msg) {
            $.ligerDialog.alert("Network error, please contact the administrator", 'Notification');
        }
    });
}
      
function add()
{
    parent.f_addTab('OtherInListAdd','Other Inbound - Create', 'store/OtherInListAdd.aspx');
            
    top.topManager.openPage({
        id : 'OtherInListAdd',
        href : 'store/OtherInListAdd.aspx',
        title: 'Other Inbound - Create'
    });
}

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
        title : 'Purchase Inbound - Create'
    });
}

function editRow()
{
    var row = manager.getSelectedRow();
          
    parent.f_addTab('OtherInListEdit','Other Inbound - Edit', 'store/OtherInListEdit.aspx?id=' + row.id);
          
    top.topManager.openPage({
        id : 'OtherInListEdit',
        href : 'store/OtherInListEdit.aspx?id=' + row.id,
        title : 'Other Inbound - Edit'
    });
}
      
function viewRow()
{
    var row = manager.getSelectedRow();
          
    parent.f_addTab('OtherInListEdit','Other Inbound - Details', 'store/OtherInListEdit.aspx?id=' + row.id);
          
    top.topManager.openPage({
        id : 'OtherInListView',
        href : 'store/OtherInListEdit.aspx?id=' + row.id,
        title: 'Other Inbound - Details'
    });
}
        
function reload()
{
    manager.reload();
}

function f_onCheckAllRow(checked)
{
    for (var rowid in this.records)
    {
        if (checked)
            addCheckedCustomer(this.records[rowid]['id']);
        else
            removeCheckedCustomer(this.records[rowid]['id']);
    }
}

var checkedCustomer = [];
function findCheckedCustomer(id)
{
    for (var i = 0; i < checkedCustomer.length; i++)
    {
        if (checkedCustomer[i] == id) return i;
    }
    return -1;
}
function addCheckedCustomer(id)
{
    if (findCheckedCustomer(id) == -1)
        checkedCustomer.push(id);
}

function removeCheckedCustomer(id)
{
    var i = findCheckedCustomer(id);
    if (i == -1) return;
    checkedCustomer.splice(i, 1);
}

function f_isChecked(rowdata)
{
    if (findCheckedCustomer(rowdata.id) == -1)
        return false;
    return true;
}

function f_onCheckRow(checked, data)
{
    if (checked) addCheckedCustomer(data.id);
    else removeCheckedCustomer(data.id);
}

function f_getChecked()
{
    alert(checkedCustomer.join(','));
}
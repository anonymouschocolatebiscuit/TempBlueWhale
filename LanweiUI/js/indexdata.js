var indexdata = 
[
    { text: '图标分析',isexpand:false, children: [ 
		{url:"deskView.aspx",text:"管理驾驶舱"}
		
	]
    },
    { text: '客户订货管理', isexpand: false, children: [
		{ url: "../baseSet/GoodsListSet.aspx", text: "商品显示设置" },
		{ url: "../order/OrderList.aspx", text: "客户订单查询" },
		{ url: "../baseSet/ClientTypeListDis.aspx", text: "客户折扣设置" },
		{ url: "../baseSet/WuliuList.aspx", text: "物流公司设置" } ,
		{ url: "../baseSet/NoticeInfo.aspx", text: "系统公告设置" } ,
		{ url: "../pay/ReceivableList.aspx", text: "客户订单收款" } 
	]
    }, 
	{ text: '采购业务管理',isexpand:false, children: [ 
		{ url: "buy/PurOrderListAdd.aspx", text: "采购订单-新增" },
        { url: "demos/dialog/dialogParent.htm", text: "采购订单-查询" },
		{url:"demos/dialog/dialogTarget.htm",text:"采购入库-新增"},
		{url:"demos/dialog/dialogUrl.htm",text:"采购入库-查询"}
		
	]},
	{ text: '销售业务管理',isexpand:false, children: [  
		{ url: "demos/dialog/dialogAll.htm", text: "销售订单-新增" },
        { url: "demos/dialog/dialogParent.htm", text: "销售订单-查询" },
		{url:"demos/dialog/dialogTarget.htm",text:"销售出库-新增"},
		{url:"demos/dialog/dialogUrl.htm",text:"销售出库-查询"}
	]},
	{ text: '仓库业务管理',isexpand:false, children: [  
		{ url: "demos/comboBox/comboBoxSelect.htm", text: "其他入库-新增" },
        { url: "demos/comboBox/comboBoxUnSelect.htm", text: "其他入库-查询" },
        { url: "demos/comboBox/comboBoxAuto.htm", text: "其他出库-新增" },
		{url:"demos/comboBox/comboBoxSingle.htm",text:"其他出库-查询"},
		{url:"demos/comboBox/comboBoxSingleCheckBox.htm",text:"调拨单-新增"},
		{url:"demos/comboBox/comboBoxMul.htm",text:"调拨单-查询"},
		{url:"demos/comboBox/comboBoxTable.htm",text:"成本调整单-新增"},
		{url:"demos/comboBox/comboBoxTableMul.htm",text:"成本调整单-查询"},
		{url:"demos/comboBox/comboBoxInterface.htm",text:"库存盘点"},
		{url:"demos/comboBox/comboBoxEven.htm",text:"库存查询"}
		
	]},
	{
	    text: '发票信息管理', isexpand: false, children: [
            { url: "demos/tree/bigDataTest.htm", text: "采购发票-新增" },
            { url: "demos/tree/treeIsExpand.htm", text: "采购发票-查询" },
            { url: "demos/tree/treeDelay.htm", text: "销售发票-新增" },
            { url: "demos/tree/treeDelayUrl.htm", text: "销售发票-查询" }
	]
	},

    {
        text: "财务资金管理", isexpand: "false", children: [
             { url: "demos/tree/bigDataTest.htm", text: "收款单-新增" },
            { url: "demos/tree/treeIsExpand.htm", text: "收款单-查询" },
            { url: "demos/tree/treeDelay.htm", text: "付款单-新增" },
            { url: "demos/tree/treeDelayUrl.htm", text: "付款单-查询" },
             { url: "demos/tree/treeDelay.htm", text: "核销单-新增" },
            { url: "demos/tree/treeDelayUrl.htm", text: "核销单-查询" }
        ]
    },

	{
	    text: '分析统计报表', isexpand: false, children: [
         {
             text: '采购业务报表', isexpand: false, children: [
                { url: "demos/form/v125/afterContent.htm", text: "采购订单跟踪表" },
                { url: "demos/form/v125/afterContent.htm", text: "采购明细表" },
                { url: "demos/form/v125/enabled.htm", text: "采购汇总表-按商品" },
                { url: "demos/form/v125/fieldError.htm", text: "采购汇总表-按供应商" }
           
             ]
             
         },
        
         {
             text: '销售业务报表', isexpand: false, children: [
                { url: "demos/form/v125/afterContent.htm", text: "销售订单跟踪表" },
                { url: "demos/form/v125/afterContent.htm", text: "销售明细表" },
                { url: "demos/form/v125/enabled.htm", text: "销售汇总表-按商品" },
                { url: "demos/form/v125/fieldError.htm", text: "销售汇总表-客户" }
           
             ]
             
         },
         
         {
             text: '仓库业务报表', isexpand: false, children: [
                { url: "demos/form/v125/afterContent.htm", text: "商品库存余额表" },
                { url: "demos/form/v125/afterContent.htm", text: "商品收发明细表" },
                { url: "demos/form/v125/enabled.htm", text: "商品收发汇总表" }
           
             ]
             
         },
         {
             text: '资金报表', isexpand: false, children: [
                { url: "demos/form/v125/afterContent.htm", text: "现金银行报表" },
                { url: "demos/form/v125/afterContent.htm", text: "应付账款明细表" },
                { url: "demos/form/v125/enabled.htm", text: "应收账款明细表" },
                { url: "demos/form/v125/afterContent.htm", text: "对账单-客户" },
                { url: "demos/form/v125/enabled.htm", text: "对账单-供应商" }
           
             ]
             
         }
         
		
	]},
	{ isexpand: "false", text: "基础信息管理", children: [
        { isexpand: "false", text: "基础资料", children: [
		    { url: "demos/filter/grid.htm", text: "高级自定义查询" },
             { url: "demos/grid/search/autoFilter.htm", text: "自动过滤" },
            { url: "demos/grid/search/search.htm", text: "查询 表格" }
	    ] 
        }, 
	    {isexpand:"false",text:"辅助资料",children:[ 
		    {url:"demos/grid/frozen/frozengrid.htm",text:"固定列"}, 
		    {url:"demos/grid/frozen/treefrozengrid.htm",text:"兼容树"}, 
		    {url:"demos/grid/frozen/mulheaders.htm",text:"兼容多表头"}
	    ]}, 
	    {isexpand:"false",text:"高级设置",children:[ 
		    {url:"demos/grid/expandable/method.htm",text:"方法"}, 
		    { url: "demos/grid/expandable/editor_numberbox.htm", text: "编辑器" }, 
		    {url:"demos/grid/expandable/formatter.htm",text:"格式化器"},
		    {url:"demos/grid/expandable/sorter.htm",text:"自定义排序"}
	    ]}
    ]}
];


var indexdata2 =
[
    { isexpand: "true", text: "基础信息管理", children: [
        { isexpand: "true", text: "可排序", children: [
		    { url: "dotnetdemos/grid/sortable/client.aspx", text: "客户端" },
            { url: "dotnetdemos/grid/sortable/server.aspx", text: "服务器" }
	    ]
        },
        { isexpand: "true", text: "可分页", children: [
		    { url: "dotnetdemos/grid/pager/client.aspx", text: "客户端" },
            { url: "dotnetdemos/grid/pager/server.aspx", text: "服务器" }
	    ]
        },
        { isexpand: "true", text: "树表格", children: [
		    { url: "dotnetdemos/grid/treegrid/tree.aspx", text: "树表格" }, 
		    { url: "dotnetdemos/grid/treegrid/tree2.aspx", text: "树表格2" }
	    ]
        }
    ]
    }
];

var navs = [{
	"title": "订单信息管理",
	"icon": "fa-file-text",
	"spread": true,
	"children": [
	{
	    "title": "待付款订单查询",
	    "icon": "fa-list",
	    "href": "client/clientList.html"
    }, 
    {
        "title": "待发货订单查询",
	    "icon": "fa-list",
	    "href": "client/clientList.html"
	}, {
	    "title": "待收货订单查询",
	    "icon": "fa-list",
	    "href": "client/clientList.html"
	}, {
	    "title": "已完成订单查询",
	    "icon": "fa-list",
	    "href": "client/clientList.html"
	}
	]
},
{
    "title": "统计分析报表",
    "icon": "fa-pie-chart",
    "spread": true,
    "children": [ {
        "title": "销售明细报表",
        "icon": "fa-list",
        "href": "client/report_salesListView.html"
    }, {
        "title": "销售汇总-按客户",
        "icon": "fa-list",
        "href": "client/report_salesListSumClient.html"
    }, {
        "title": "销售汇总-按商品",
        "icon": "fa-list",
        "href": "client/report_salesListSumClient.html"
    }]
},
{
    "title": "小程序公众号管理",
    "icon": "fa-weixin",
    "spread": true,
    "children": [
        {

        "title": "轮播图片管理",
        "icon": "fa-picture-o",
        "href": "admin/wxopen/bannerList.html"
        },
         {

                    "title": "小程序参数设置",
                    "icon": "fa-list",
                    "href": "admin/wxopen/wxOpenSet.html"
         }
         ,
         {

             "title": "公众号参数设置",
             "icon": "fa-list",
             "href": "admin/wxopen/wxOpenSet.html"
         }



    ]
}
,


{
	"title": "基础信息设置",
	"icon": "fa-cogs",
	"spread": true,
	"children": [
         {
            "title": "商品信息管理",
            "icon": "fa-th-large",
            "href": "admin/goods/service_manage_list.html"
        },
 {
     "title": "商品分类管理",
     "icon": "fa-table",
     "href": "admin/goods/service_category_list.html"
 }
	]
}];
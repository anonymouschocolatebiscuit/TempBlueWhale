<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Lanweiyun.com.aspx.cs" Inherits="Lanwei.Weixin.UI.Lanweiyun" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
     <title>欢迎使用蓝微·云ERP系统</title>
     
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="renderer" content="webkit|ie-stand">
  
  <!--左侧导航和Top 开始-->
    
     <link rel="stylesheet" type="text/css" href="appCss/app.css">
 
   <script src="appCss/jquery-1.10.2.min.js" type="text/javascript"></script>
   
   <style type="text/css">
 .default #appHeader {
height: 46px;
position: fixed;
top: 0;
left: 0px;
z-index: 99;
width: 100%;
font-family: '微软雅黑', '宋体';
}

.default .menuList1 {

/*Logo背景颜色 background-color:Green;background: #3b4857;*/

width: 63px;
height: 46px;
}

.default .headMessage {
height: 45px;
margin-right: 25px;
}

.default .mainLogo {
float: left;
display: block;
width: 300px;
height: 25px;
margin: 10px 0 0 14px;
font-size: 18px;
color: #fff;
font-family: '微软雅黑', '宋体';
position: relative;
}


.default .head-ask {
height: 45px;
line-height: 45px;
color: #f2f2f2;
font-size: 14px;
text-decoration: none;
max-width: 30em;
white-space: nowrap;
text-overflow: ellipsis;
overflow: hidden;
text-align: right;
}


.default #appLeftSide {
position: fixed;
top: 46px;
left: 0;
width: 63px;
background: #393d48;
height: 100%;
z-index: 201;
}
       
       
       
       
       
       
       
       
   
#nav { z-index:1; /*border-bottom:1px solid #28688b;border-color:rgba(0,0,0,0.15);*/ }
#nav .item { position: relative; margin-bottom: 1px; zoom: 1; float: left; width: 100%; }
.v-standard #nav .item{ margin-bottom:0; }
#nav .main-nav { position: relative; display: block; height: 80px; width: 100%; float: left; font-family:"新宋体", "宋体";}
.v-standard #nav .item{ height: 84px; }
#nav .on .main-nav { background-position: 0 -80px;}
#nav .current .main-nav { background-position: 0 -160px;} 
#nav .sales { background-position: -98px 0; }
#nav .on .sales { background-position: -98px -80px; }
#nav .current .sales { background-position: -98px -160px; }
#nav .storage { background-position: -196px 0; }
#nav .on .storage { background-position: -196px -80px; }
#nav .current .storage { background-position: -196px -160px; }
#nav .money { background-position: -294px 0; }
#nav .on .money { background-position: -294px -80px; }
#nav .current .money { background-position: -294px -160px; }
#nav .report { background-position: -392px 0; }
#nav .on .report { background-position: -392px -80px; }
#nav .current .report { background-position: -392px -160px; }
#nav .setting { background-position: -490px 0; }
#nav .on .setting { background-position: -490px -80px; }
#nav .current .setting { background-position: -490px -160px; }
#nav .vip { background-position: -582px 0; }
#nav .on .vip { background-position: -582px -80px; }
#nav .current .vip { background-position: -582px -160px; }
#nav .arrow { position: absolute; left: 75px; top: 55px; display: block; text-indent: 0; font-weight: bold; }
#nav .on .arrow { text-indent:-99999px; width: 0;height: 0;border-style: solid;border-width: 8px;border-color: transparent;border-right-color: white;border-left: none;left: 87px;top: 50%;margin-top: -8px; }
#nav.static .current .arrow { text-indent:-99999px; width: 0;height: 0;border-style: solid;border-width: 8px;border-color: transparent;border-right-color: white;border-left: none;left: 87px;top: 50%;margin-top: -8px;}
.v-standard #nav .on .arrow { text-indent:-99999px; width: 0;height: 0;border-style: solid;border-width: 8px;border-color: transparent;border-right-color: white;border-left: none;left: 87px;top: 50%;margin-top: -8px; }
.v-standard #nav.static .current .arrow { text-indent:-99999px; width: 0;height: 0;border-style: solid;border-width: 8px;border-color: transparent;border-right-color: white;border-left: none;left: 87px;top: 50%;margin-top: -8px; }
#nav .sub-nav-wrap { display: none; position: absolute; left:60px; width: 120px; border: 4px solid #c7c7c7; border-color: rgba(0,0,0,0.2); border-left: 0; border-top-right-radius: 4px; border-bottom-right-radius: 4px; z-index: 9999; background-color: #fff; }
#nav .sub-nav-wrap b { position: absolute; left: -8px;  margin-top: -8px; display: block; width: 0; height: 0; font-size: 0; overflow: hidden; border-style: solid;border-width: 8px;border-color: transparent;border-right-color: white;border-left: none;}
#nav .single-nav b{top: 50%;}
#nav .sub-nav { background-color: #fff; padding: 10px 0 5px; line-height: 24px; }
#nav .sub-nav li { padding-bottom: 8px;font-size:10px;}
#nav .sub-nav a { padding-left: 15px; display: block; color: #888; zoom: 1;}
#nav .sub-nav a:hover { background-color: #eee; color: #555; }

#nav .group-nav{width:221px;padding:15px 0;background-color:#fff;}
#nav .group-nav .nav-item{float:left;width:130px;border-right:1px dashed #ccc;}
#nav .group-nav .sub-nav{padding-bottom:0;}
#nav .group-nav h3{margin-left:15px;font-size:12px;}
#nav .group-nav .last{border-right:0;}
#nav .group-nav b{bottom:28px;top:inherit}
#nav .group-nav-t0{top:0;}
#nav .group-nav-t0 b{top:36px;}
#nav .group-nav-b0{bottom:0;}

.v-standard #nav .group-nav b{bottom:30px;top:inherit}

#nav .vip-nav{width:246px;}
#nav .vip-nav .nav-onlineStore{width:120px;}
#nav .vip-nav .nav-JDstore{width:125px;}

#nav .report-nav{width:900px;}/*报表总款、原来是674*/

#nav .report-nav .nav-pur{width:180px;}/*报表列、采购报表*/

#nav .report-nav .nav-sales{width:180px;}/*报表列、销售报表*/

#nav .report-nav .nav-fund{width:150px;}/*报表列、仓存报表*/

#nav .setting-nav{ width:425px; }


#nav .produce-nav{ width:525px; } /**生产*/

#nav .store-nav{ width:425px; } /**财务*/

#nav .setting-nav .last { width:auto; }
#nav .setting-nav .last ul{ width:120px;float:left; }
#navScroll span{ display:block; width:47px; height:23px; float:left; border-radius:3px; cursor:pointer; }

/*左侧导航图标begin*/

.moduleimg {
background: url(images/tubiao.png) no-repeat scroll 0 0 transparent;
height: 32px;
width: 32px;
margin: 0 auto;
}


/*左侧导航图标end*/

   </style>
   
   
   <script type="text/javascript">
//左侧导航
function subindexMenu(){
	$("#nav").find("li").hover(function(){
		$(this).addClass("on");
		
		$(this).find("div").show();
		
	
		
		
	},function(){

		$(this).removeClass("on");
		$(this).find("div").hide();
		
	});
	
	
	
		$("#userList").find("a").hover(function(){
		//$(this).addClass("on");
		//alert("sss");
		$(this).find("div").show();
		
	
		
		
	},function(){

		//$(this).removeClass("on");
		$(this).find("div").hide();
		
	});
	
	
	
}
$(function(){
	subindexMenu();
});

function moveMenu()
{
   $("#nav").find("li")(function(){
		$(this).removeClass("on");
		$(this).find("div").hide();
		
	});
}
function weixin()
{
    $.ligerDialog.open({ title:'扫描蓝微公众平台',height: 550,width:550,url: 'weixin.htm' });
}
 
</script>
   
    <!--左侧导航和Top 结束-->

   <!--内容区、Tab 开始-->
   
 
<link href="lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
   
    <link href="lib/ligerUI/skins/Gray2014/css/all.css" rel="stylesheet" type="text/css" />
<script src="lib/ligerUI/js/ligerui.all.js" type="text/javascript"></script> 
    <script src="lib/ligerUI/js/plugins/ligerTab.js" type="text/javascript"></script>
    <script src="lib/jquery.cookie.js" type="text/javascript"></script>
    <script src="lib/json2.js" type="text/javascript"></script>
    <script src="js/indexdata.js" type="text/javascript"></script>


  <script src="lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script> 
  <script src="lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>

<script type="text/javascript">
 
            var
             tab = null;
            var accordion = null;
            var tree = null;
            var tabItems = [];
            $(function ()
            {

                //布局 heightDiff -34
                $("#layout1").ligerLayout({ leftWidth: 190, height: '100%',heightDiff:0,space:4, onHeightChanged: f_heightChanged });

                var height = $(".l-layout-center").height();

                //Tab
                $("#framecenter").ligerTab({
                    height: height,
                    showSwitchInTab : true,
                    showSwitch: true,
                    onAfterAddTabItem: function (tabdata)
                    {
                        tabItems.push(tabdata);
                        saveTabStatus();
                    },
                    onAfterRemoveTabItem: function (tabid)
                    { 
                        for (var i = 0; i < tabItems.length; i++)
                        {
                            var o = tabItems[i];
                            if (o.tabid == tabid)
                            {
                                tabItems.splice(i, 1);
                                saveTabStatus();
                                break;
                            }
                        }
                    },
                    onReload: function (tabdata)
                    {
                        var tabid = tabdata.tabid;
                        addFrameSkinLink(tabid);
                    }
                });

                //面板
                $("#accordion1").ligerAccordion({
                    height: height - 24, speed: null
                });

                $(".l-link").hover(function ()
                {
                    $(this).addClass("l-link-over");
                }, function ()
                {
                    $(this).removeClass("l-link-over");
                });
                //树
                $("#tree1").ligerTree({
                    data : indexdata,
                    checkbox: false,
                    slide: false,
                    nodeWidth: 120,
                    attribute: ['nodename', 'url'],
                    onSelect: function (node)
                    {
                        if (!node.data.url) return;
                        var tabid = $(node.target).attr("tabid");
                        if (!tabid)
                        {
                            tabid = new Date().getTime();
                            $(node.target).attr("tabid", tabid)
                        } 
                        f_addTab(tabid, node.data.text, node.data.url);
                    }
                });

                tab = liger.get("framecenter");
                accordion = liger.get("accordion1");
                tree = liger.get("tree1");
                $("#pageloading").hide();

                css_init();
               // pages_init();//是否记忆打开的页签，重新登陆系统还显示。
            });
            
            
            function f_heightChanged(options)
            {  
                if (tab)
                    tab.addHeight(options.diff);
                if (accordion && options.middleHeight - 24 > 0)
                    accordion.setHeight(options.middleHeight - 24);
            }
            
            //新增右侧标签
            function f_addTab(tabid, text, url)
            {
                tab.addTabItem({
                    tabid: tabid,
                    text: text,
                    url: url,
                    callback: function ()
                    {
                       // addShowCodeBtn(tabid); 
                        addFrameSkinLink(tabid); 
                    }
                });
            }
            function addShowCodeBtn(tabid)
            {
                var viewSourceBtn = $('<a class="viewsourcelink" href="javascript:void(0)">查看源码</a>');
                var jiframe = $("#" + tabid);
                viewSourceBtn.insertBefore(jiframe);
                viewSourceBtn.click(function ()
                {
                    showCodeView(jiframe.attr("src"));
                }).hover(function ()
                {
                    viewSourceBtn.addClass("viewsourcelink-over");
                }, function ()
                {
                    viewSourceBtn.removeClass("viewsourcelink-over");
                });
            }
            function showCodeView(src)
            {
                $.ligerDialog.open({
                    title : '源码预览',
                    url: 'dotnetdemos/codeView.aspx?src=' + src,
                    width: $(window).width() *0.9,
                    height: $(window).height() * 0.9
                });

            }
            function addFrameSkinLink(tabid)
            {
                var prevHref = getLinkPrevHref(tabid) || "";
                var skin = getQueryString("skin");
                if (!skin) return;
                skin = skin.toLowerCase();
                attachLinkToFrame(tabid, prevHref + skin_links[skin]);
            }
            var skin_links = {
                "aqua": "lib/ligerUI/skins/Aqua/css/ligerui-all.css",
                "gray": "lib/ligerUI/skins/Gray/css/all.css",
                "silvery": "lib/ligerUI/skins/Silvery/css/style.css",
                "gray2014": "lib/ligerUI/skins/gray2014/css/all.css"
            };
            function pages_init()
            {
                var tabJson = $.cookie('liger-home-tab'); 
                if (tabJson)
                { 
                    var tabitems = JSON2.parse(tabJson);
                    for (var i = 0; tabitems && tabitems[i];i++)
                    { 
                        f_addTab(tabitems[i].tabid, tabitems[i].text, tabitems[i].url);
                    } 
                }
            }
            function saveTabStatus()
            { 
                $.cookie('liger-home-tab', JSON2.stringify(tabItems));
            }
            function css_init()
            {
                var css = $("#mylink").get(0), skin = getQueryString("skin");
                $("#skinSelect").val(skin);
                $("#skinSelect").change(function ()
                { 
                    if (this.value)
                    {
                        location.href = "lanweigroup.htm?skin=" + this.value;
                    } else
                    {
                        location.href = "lanweigroup.htm";
                    }
                });

               
                if (!css || !skin) return;
                skin = skin.toLowerCase();
                $('body').addClass("body-" + skin); 
                $(css).attr("href", skin_links[skin]); 
            }
            function getQueryString(name)
            {
                var now_url = document.location.search.slice(1), q_array = now_url.split('&');
                for (var i = 0; i < q_array.length; i++)
                {
                    var v_array = q_array[i].split('=');
                    if (v_array[0] == name)
                    {
                        return v_array[1];
                    }
                }
                return false;
            }
            function attachLinkToFrame(iframeId, filename)
            { 
                if(!window.frames[iframeId]) return;
                var head = window.frames[iframeId].document.getElementsByTagName('head').item(0);
                var fileref = window.frames[iframeId].document.createElement("link");
                if (!fileref) return;
                fileref.setAttribute("rel", "stylesheet");
                fileref.setAttribute("type", "text/css");
                fileref.setAttribute("href", filename);
                head.appendChild(fileref);
            }
            function getLinkPrevHref(iframeId)
            {
                if (!window.frames[iframeId]) return;
                var head = window.frames[iframeId].document.getElementsByTagName('head').item(0);
                var links = $("link:first", head);
                for (var i = 0; links[i]; i++)
                {
                    var href = $(links[i]).attr("href");
                    if (href && href.toLowerCase().indexOf("ligerui") > 0)
                    {
                        return href.substring(0, href.toLowerCase().indexOf("lib") );
                    }
                }
            }
            
            function logout() {
           
                
                 $.ligerDialog.confirm('确定退出蓝微·云ERP系统？', function (yes) { 
            
                if(yes)
                {
                   
                      if(dbIdString!=null)
                    {
                        window.parent.location.href = "loginOut.aspx?dbid="+dbIdString;
                    }
                    else
                    {
                       window.parent.location.href = "loginOut.aspx";
                    }
                }

            });   
                
            }
            function pwd()
      {
          top.topManager.openPage({
            id : 'pwd',
            href : 'pwd.aspx',
            title : '用户密码-修改'
          });
  
  
      }
               
               
var dbIdString=getUrlParam("dbid");


 
function getUrlParam(name)
{
   var reg = new RegExp("(^|&)"+ name +"=([^&]*)(&|$)");

   var r = window.location.search.substr(1).match(reg);

   if (r!=null) return unescape(r[2]); return null;
}   
            
     </script>
   
     <!--内容区、Tab 结束-->
     
     
     
     <!--弹窗提示-->
     
     <script type="text/javascript"> 
        function f_tip() {
            $.ligerDialog.tip({  title: '提示信息',content:'记录已经删除！'+num++ });
        }
        var tip;
        var num = 0;
        function f_tip2() {
            if (!tip) {
                tip = $.ligerDialog.tip({ title: '提示信息', content: '记录已经删除！' });
            }
            else {
                var visabled = tip.get('visible');
                if (!visabled) tip.show();
                tip.set('content', '有新订单需要审核<a>' + num++);
            }
        } 
        
        
        
    </script>
     
 
    
     
</head>
<body class="default">
    <form id="form1" runat="server">
  
  	<!--头部-->
 	<!--头部-->
<div id="appHeader">
    
    
    <div id="app/vm/Header_0">
 
    <div class="header_menu_black menuList1 fl">
        <a href="http://www.lanweiyun.com" target="_blank" title="获取更多资讯、访问蓝微云官网">
        
        <img src="appCss/lwLogoNew.png" width="60px" alt="获取更多资讯、访问蓝微云官网" />
        
        
        </a>
    </div>
    <div><a title="回到首页" class="mainLogo">
         <% =companyName %>
        <span>

           

                                          </span></a></div>
    
    
    <div class="fr headMessage" id="userList">
      
      <a class="clearfix" onclick="logout()"><img src="images/logout_24.png" style="border:0px;"><span>退出系统</span><%--<span data-dojo-attach-point="iconNode" class="moreBtn"></span>--%>

      </a>
        
    
        
    </div>
    
 
   <div class="fr headMessage" id="Div2">
      
      <a class="clearfix" onclick="f_addTab('Pwd','修改密码','Pwd.aspx')"><img src="images/key_24.png" style="border:0px;"><span>修改密码</span>
      
    
      </a>
        
 
    </div>
 
    <div class="fr headMessage" id="Div7">
     
       
     </div>
     
     
     
     
   
     <div class="fr headMessage" id="Div1">
      
      <a class="clearfix"><img src="images/kefu.png">
      
   <span>

             <% =userName %>

                                          </span>
     
    
        
         <span> &nbsp;&nbsp;&nbsp;&nbsp;</span>
         
      
    
    
      </a>
      
      
     
     
      
       
    </div>
    
   
    
   
  <%-- <div class="fr headMessage" id="Div3">
   
     <a class="clearfix" title="切换至商城系统" href="lanweiyun.aspx">
   <img src="images/weichat.jpg" /> 
   <span>切换至商城系统</span>
   
   
   </a>
   
   </div>--%>
    

    
     <div class="fr headMessage" id="Div4">
     <a class="clearfix" title="有问题请点此QQ咨询" href="tencent://message/?uin=516294911&Site=www.lanweiyun.com.cn&Menu=yes" target="_blank">
    <img src="images/qqServer.jpg" /> 
   <span>在线咨询</span>
   
   
   </a>

    </div>
    
      <div class="fr headMessage" id="Div8" style="display:none;">
     <a class="clearfix" title="点击关注蓝微公众号">
    <img src="images/weichat.png" /> 
   <span>微信公众平台</span>
   
   
   </a>

    </div>
    
    

   </div>

</div>

 <!-- 导航区 -->
    
    <div id="appLeftSide" class="app-menu app-left-menu admin">
    <span></span>
    <ul id="nav">
    
   <%--class= menuCur--%>
    
        <li class="a1" style="display:none;"><a href="javascript:void(0);"><span class="moduleimg" style="background-position: -36px -32px;"></span><span>电商</span></a>
        
        
        
        
        <div class="sub-nav-wrap group-nav group-nav-t0 vip-nav cf" style="display:none;">
          <div class="nav-item nav-onlineStore">
            <h3 style="color:Black;">微信订单</h3>
          
            <ul class="sub-nav" id="Ul1">

          
            <li><a onclick="f_addTab('SalesOrderList','小程序订单','wxopen/SalesOrderList.aspx?id=0')">小程序订单</a></li>
         
            <li><a onclick="f_addTab('SalesOrderList','公众号订单','wxopen/SalesOrderList.aspx')">公众号订单</a></li>
          
        

          	
          
          </ul>
          
          </div>
          <div class="nav-item nav-JDstore last">
            <h3 style="color:Black;">参数设置</h3>
          
            <ul class="sub-nav" id="Ul2">
          	
                <li><a onclick="f_addTab('bannerList','轮播图片设置','admin/wxopen/bannerList.html')">轮播图片设置</a></li>
          
          <li><a onclick="f_addTab('service_category_list','分类导航设置','admin/goods/service_category_list.html')">分类导航设置</a></li>
          

          <li><a onclick="f_addTab('wxOpenSet','小程序参数设置','admin/wxopen/wxOpenSet.html')">小程序参数设置</a></li>
          <li><a onclick="f_addTab('wxOpenSet','公众号参数设置','admin/wxopen/wxOpenSet.html')">公众号参数设置</a></li>
          	
          	</ul>
          
          </div>
          </div>
        
        
        
        </li>
        
        
      
        
        
      
  
        <li class="a2"><a href="javascript:void(0);" page="workrecord" ><span class="moduleimg" style="background-position: -36px -96px;"></span><span>销售</span></a>
        
        
        
        <div class="sub-nav-wrap group-nav group-nav-t0 vip-nav cf" style="display:none;">
          <div class="nav-item nav-onlineStore">
            <h3 style="color:Black;">销售订单</h3>
          
            <ul class="sub-nav" id="Ul15">
          
            <li><a onclick="f_addTab('SalesOrderListAdd','销售订单-新增','Sales/SalesOrderListAdd.aspx?id=0')">销售订单-新增</a></li>
          <li><a onclick="f_addTab('SalesOrderList','销售订单-查询','Sales/SalesOrderList.aspx')">销售订单-查询</a></li>
          
         <%-- <li><a onclick="f_addTab('SalesOrderListItem','销售订单-处理','Sales/SalesOrderListItem.aspx')">销售订单-处理</a></li>--%>
          
       <%--   <li><a onclick="f_addTab('ClientAsksListAdd','客户询价-新增','Sales/ClientAsksListAdd.aspx')">客户询价-新增</a></li>
          <li><a onclick="f_addTab('ClientAsksList','客户询价-查询','Sales/ClientAsksList.aspx')">客户询价-查询</a></li>
          --%>
         <%-- <li><a onclick="f_addTab('SalesQuoteListAdd','销售报价-新增','Sales/SalesQuoteListAdd.aspx')">销售报价-新增</a></li>
          <li><a onclick="f_addTab('SalesQuoteList','销售报价-查询','Sales/SalesQuoteList.aspx')">销售报价-查询</a></li>
         --%>
          	
          
          </ul>
          
          </div>
          <div class="nav-item nav-JDstore last">
            <h3 style="color:Black;">销售出库</h3>
          
            <ul class="sub-nav" id="Ul16">
          	
         
          
          <li><a onclick="f_addTab('SalesReceiptListAdd','销售出库-新增','Sales/SalesReceiptListAdd.aspx?id=0')">销售出库-新增</a></li>
          <li><a onclick="f_addTab('SalesReceiptList','销售出库-查询','Sales/SalesReceiptList.aspx')">销售出库-查询</a></li>
          	
          	</ul>
          
          </div>
          </div>
          
          
        
        
      
        
        
        </li>
      

            <li class="a1"><a href="javascript:void(0);">
                
                <span class="moduleimg" style="background-position: -36px -160px;"></span><span>生产</span></a>
        
        
        
        <div class="sub-nav-wrap group-nav group-nav-t0 produce-nav cf" style="display:none;">
         

             <div class="nav-item nav-onlineStore">
            <h3 style="color:Black;">基础资料</h3>
            <ul class="sub-nav" id="Ul18">
          

            <li><a onclick="f_addTab('goodsBomListType','BOM分组','produce/goodsBomListType.aspx')">BOM分组</a></li>
          	
          	<li><a onclick="f_addTab('goodsBomList','BOM清单','produce/goodsBomList.aspx')">BOM清单</a></li>
          	
           <%-- <li><a onclick="f_addTab('produceList','工序管理','produce/produceList.aspx')">工序管理</a></li>--%>


          	
         
          	
          
          </ul>
          </div>
            
             <div class="nav-item nav-onlineStore">
            <h3 style="color:Black;">生产计划</h3>
            <ul class="sub-nav" id="Ul11">
          
          
          	
          	<li><a onclick="f_addTab('produceListAdd','生产计划-新增','produce/produceListAdd.aspx')">生产计划-新增</a></li>
          	
            <li><a onclick="f_addTab('produceList','生产计划-查询','produce/produceList.aspx')">生产计划-查询</a></li>


          	
         
          	
          
          </ul>
          </div>


            <div class="nav-item nav-onlineStore">
            <h3 style="color:Black;">生产领料</h3>
            <ul class="sub-nav" id="Ul10">
          
          
          	
          
           <%-- <li><a onclick="f_addTab('produceProcessList','工序生产记录-查询','produce/produceProcessList.aspx')">生产工序-查询</a></li>
 --%>
                
            <li><a onclick="f_addTab('produceGetListAdd','生产领料-新增','produce/produceGetListAdd.aspx')">生产领料-新增</a></li>

            <li><a onclick="f_addTab('produceGetList','生产领料-查询','produce/produceGetList.aspx')">生产领料-查询</a></li>

          	
         
          	
          
          </ul>
          </div>


          
          <div class="nav-item nav-JDstore last">
            <h3 style="color:Black;">生产入库</h3>
            <ul class="sub-nav" id="Ul12">
            
          		<li><a onclick="f_addTab('produceInListAdd','生产入库-新增','produce/produceInListAdd.aspx?id=0')">生产入库-新增</a></li>
          	
         
            	<li><a onclick="f_addTab('produceInList','生产入库-查询','produce/produceInList.aspx')">生产入库-查询</a></li>
            	
            	
          	
          	</ul>
          </div>
          
          
          </div>
        
        
        
        
        </li>

          <li class="">
            
            <a href="javascript:void(0);" page="customer_list" ><span class="moduleimg" style="background-position: -36px -64px;"></span><span>采购</span></a>
        
        
        
        <div class="sub-nav-wrap group-nav group-nav-t0 vip-nav cf" style="display:none;">
          <div class="nav-item nav-onlineStore">
            <h3 style="color:Black;">采购订单</h3>
          
            <ul class="sub-nav" id="Ul13">
            
            
              <li><a onclick="f_addTab('PurOrderListAdd','采购订单-新增','buy/PurOrderListAdd.aspx?id=0')">采购订单-新增</a></li>
          <li><a onclick="f_addTab('PurOrderList','采购订单-查询','buy/PurOrderList.aspx')">采购订单-查询</a></li>
          
         <%-- <li><a onclick="f_addTab('PurAsksListAdd','采购询价-新增','buy/PurAsksListAdd.aspx')">采购询价-新增</a></li>
          <li><a onclick="f_addTab('PurAsksList','采购询价-查询','buy/PurAsksList.aspx')">采购询价-查询</a></li>--%>
          	
       <%--   <li><a onclick="f_addTab('PurAsksListAdd','采购回价-新增','buy/PurAsksListAdd.aspx')">采购回价-新增</a></li>
          <li><a onclick="f_addTab('PurAsksList','采购回价-查询','buy/PurAsksList.aspx')">采购回价-查询</a></li>
         
          	--%>
          
          </ul>
          
          </div>
          <div class="nav-item nav-JDstore last">
            <h3 style="color:Black;">采购入库</h3>
          
            <ul class="sub-nav" id="Ul14">
          	
          	 
          <li><a onclick="f_addTab('PurReceiptListAdd','采购入库-新增','buy/PurReceiptListAdd.aspx?id=0')">采购入库-新增</a></li>
          <li><a onclick="f_addTab('PurReceiptList','采购入库-查询','buy/PurReceiptList.aspx')">采购入库-查询</a></li>
          	
          	</ul>
          
          </div>
          </div>
          
          
        
        
        
       
        
     
        </li>
      
        <li class="a3"><a href="javascript:void(0);" page="todo" ><span class="moduleimg" style="background-position: -36px -0px;"></span><span>库存</span></a>
        
        
        
        
        <div class="sub-nav-wrap group-nav group-nav-b0 store-nav cf">
          <div class="nav-item nav-pur">
            <h3>入库出库</h3>
            <ul class="sub-nav" id="Ul4">
           
          <li><a onclick="f_addTab('PurReceiptListCheck','采购入库-审核','buy/PurReceiptListCheck.aspx')">采购入库-审核</a></li>
          <li><a onclick="f_addTab('SalesReceiptListCheck','销售出库-审核','sales/SalesReceiptListCheck.aspx')">销售出库-审核</a></li>

          <li><a onclick="f_addTab('OtherInListAdd','其他入库-新增','store/OtherInListAdd.aspx')">其他入库-新增</a></li>
          <li><a onclick="f_addTab('OtherInList','其他入库-查询','store/OtherInList.aspx')">其他入库-查询</a></li>
          
          <li><a onclick="f_addTab('OtherOutListAdd','其他出库-新增','store/OtherOutListAdd.aspx')">其他出库-新增</a></li>
          <li><a onclick="f_addTab('OtherOutList','其他出库-查询','store/OtherOutList.aspx')">其他出库-查询</a></li>
          
           
           </ul>
          </div>
          <div class="nav-item nav-sales">
            <h3>组装拆卸</h3>
            <ul class="sub-nav" id="Ul5">
           
          <li><a onclick="f_addTab('AssembleListAdd','商品组装-新增','store/AssembleListAdd.aspx')">商品组装-新增</a></li>
          <li><a onclick="f_addTab('AssembleList','商品组装-查询','store/AssembleList.aspx')">商品组装-查询</a></li>
          
          <li><a onclick="f_addTab('DisassembleListAdd','商品拆卸-新增','store/DisassembleListAdd.aspx')">商品拆卸-新增</a></li>
          <li><a onclick="f_addTab('DisassembleList','商品拆卸-查询','store/DisassembleList.aspx')">商品拆卸-查询</a></li>
          
           </ul>
          </div>
          
          <div class="nav-item nav-fund last">
            <h3>库存管理</h3>
            <ul class="sub-nav" id="Ul7">
             
             <li><a onclick="f_addTab('DiaoboListAdd','库存调拨-新增','store/DiaoboListAdd.aspx')">库存调拨-新增</a></li>
             <li><a onclick="f_addTab('DiaoboList','库存调拨-查询','store/DiaoboList.aspx')">库存调拨-查询</a></li>
          
               <li><a onclick="f_addTab('Pandian','库存盘点-新增','store/Pandian.aspx')">库存盘点-新增</a></li>
             <li><a onclick="f_addTab('SumNumGoodsReport','库存查询','report/SumNumGoodsReport.aspx')">库存查询</a></li>
              
            </ul>
          </div>
          
       </div>
        
        
        </li>
     
        <li style="display:none;">
            
            <a href="javascript:void(0);">
            <span class="moduleimg" style="background-position: -36px -192px;"></span>

                <span>考勤审批</span>

            </a>
        
        <div class="sub-nav-wrap single-nav" style="top: -0px;">
           <ul class="sub-nav" id="Ul3">
         
                <li><a onclick="f_addTab('kaoqinList','考勤记录-查询','Kaoqin/kaoqinList.aspx')">考勤记录-查询</a></li>

               <li><a onclick="f_addTab('applyList','审批记录-查询','Kaoqin/applyList.aspx')">审批记录-查询</a></li>
         
         
          </ul>
        
        </div>
        
        
        </li>
     
        <li class="a4"><a href="javascript:void(0);"><span  class="moduleimg" style="background-position: -36px -220px;"></span><span>财务</span></a>
        
       
        
        <div class="sub-nav-wrap group-nav group-nav-b0 store-nav cf" style="display:none;">
          
          <div class="nav-item nav-onlineStore">
            <h3 style="color:Black;">收款</h3>
            <ul class="sub-nav" id="Ul6">
            
             <li><a onclick="f_addTab('ReceivableListAdd','销售收款-新增','pay/ReceivableListAdd.aspx')">销售收款-新增</a></li>
             <li><a onclick="f_addTab('ReceivableList','销售收款-查询','pay/ReceivableList.aspx')">销售收款-查询</a></li>

             <li><a onclick="f_addTab('OtherGetListAdd','其他收款-新增','pay/OtherGetListAdd.aspx')">其他收款-新增</a></li>
             <li><a onclick="f_addTab('OtherGetList','其他收款-查询','pay/OtherGetList.aspx')">其他收款-查询</a></li>


      
          	
          
          </ul>
          </div>
          
          <div class="nav-item nav-onlineStore">
            <h3 style="color:Black;">付款</h3>
            <ul class="sub-nav" id="Ul9">
                
           	 <li><a onclick="f_addTab('PayMentListAdd','采购付款-新增','pay/PayMentListAdd.aspx')">采购付款-新增</a></li>
             <li><a onclick="f_addTab('PayMentList','采购付款-查询','pay/PayMentList.aspx')">采购付款-查询</a></li>

           
          	 <li><a onclick="f_addTab('OtherPayListAdd','其他付款-新增','pay/OtherPayListAdd.aspx')">其他付款-新增</a></li>
             <li><a onclick="f_addTab('OtherPayList','其他付款-查询','pay/OtherPayList.aspx')">其他付款-查询</a></li>

          	
          
          </ul>
          </div>
          
          <div class="nav-item nav-JDstore last">
            <h3 style="color:Black;">核销</h3>
            <ul class="sub-nav" id="Ul8">
          	
          	  <li><a onclick="f_addTab('CheckBillGetListAdd','收款核销-新增','pay/CheckBillGetListAdd.aspx')">收款核销-新增</a></li>
             <li><a onclick="f_addTab('CheckBillGetList','收款核销-查询','pay/CheckBillGetList.aspx')">收款核销-查询</a></li>

          	 <li><a onclick="f_addTab('CheckBillPayListAdd','付款核销-新增','pay/CheckBillPayListAdd.aspx')">付款核销-新增</a></li>
             <li><a onclick="f_addTab('PayMentList','付款核销-查询','pay/PayMentList.aspx')">付款核销-查询</a></li>

          	
          	</ul>
          </div>
          </div>
        
        </li>
        
         <li class="a4">
             <a href="javascript:void(0);" page="baidu"  class="baidu ">
                 <span class="moduleimg" style="background-position: -36px -256px;"></span>
                 <span>报表</span>

             </a>
         
         
         <div class="sub-nav-wrap group-nav group-nav-b0 report-nav cf">
        
             
         <div class="nav-item nav-pur">
            <h3>采购报表</h3>
            <ul class="sub-nav" id="report-purchase">
           
           <%-- <li><a onclick="f_addTab('PurAsksListReport','采购询价记录表','Report/PurAsksListReport.aspx')">采购询价记录表</a></li>--%>
            <li><a onclick="f_addTab('PurOrderListReport','采购订单跟踪表','Report/PurOrderListReport.aspx')">采购订单跟踪表</a></li>
            <li><a onclick="f_addTab('PurOrderListDetailReport','采购明细表','Report/PurOrderListDetailReport.aspx')">采购明细表</a></li>
            <li><a onclick="f_addTab('PurOrderListSumGoodsReport','采购汇总表（按商品）','Report/PurOrderListSumGoodsReport.aspx')">采购汇总表（按商品）</a></li>
            <li><a onclick="f_addTab('PurOrderListSumVenderReport','采购汇总表（按供应商）','Report/PurOrderListSumVenderReport.aspx')">采购汇总表（按供应商）</a></li>
           
           </ul>
          </div>


          <div class="nav-item nav-sales">
            <h3>销售报表</h3>
            <ul class="sub-nav" id="report-sales">
           
            <%--<li><a onclick="f_addTab('SalesQuoteListReport','销售报价记录表','Report/SalesQuoteListReport.aspx')">销售报价记录表</a></li>--%>
            <li><a onclick="f_addTab('SalesOrderListReport','销售订单跟踪表','Report/SalesOrderListReport.aspx')">销售订单跟踪表</a></li>
            <li><a onclick="f_addTab('SalesOrderListDetailReport','销售明细表','Report/SalesOrderListDetailReport.aspx')">销售明细表</a></li>
            <li><a onclick="f_addTab('SalesOrderListSumGoodsReport','销售汇总表（按商品）','Report/SalesOrderListSumGoodsReport.aspx')">销售汇总表（按商品）</a></li>
            <li><a onclick="f_addTab('SalesOrderListSumVenderReport','销售汇总表（按客户）','Report/SalesOrderListSumClientReport.aspx')">销售汇总表（按客户）</a></li>
           
                <%-- <li><a onclick="f_addTab('SalesOrderListSumSalersReport','销售汇总表（按员工）','Report/SalesOrderListSumSalersReport.aspx')">销售汇总表（按员工）</a></li>
           --%>
           </ul>
          </div>


        <div class="nav-item nav-sales">

            <h3>生产报表</h3>
            <ul class="sub-nav" id="Ul17">
           
          
            <li><a onclick="f_addTab('ProduceListReport','生产计划跟踪表','Report/ProduceListReport.aspx')">生产计划跟踪表</a></li>
         
            <li><a onclick="f_addTab('ProduceGetListReportDetail','生产领料明细表','Report/ProduceGetListReportDetail.aspx')">生产领料明细表</a></li>
            <li><a onclick="f_addTab('ProduceGetListReportSum','生产领料汇总表','Report/ProduceGetListReportSum.aspx')">生产领料汇总表</a></li>
           
            <li><a onclick="f_addTab('ProduceListDetailReport','生产入库明细表','Report/ProduceListDetailReport.aspx')">生产入库明细表</a></li>
            <li><a onclick="f_addTab('ProduceListSumGoodsReport','生产入库汇总表','Report/ProduceListSumGoodsReport.aspx')">生产入库汇总表</a></li>
           
           
          <%--  <li><a onclick="f_addTab('ProduceProcessListDetailReport','工序生产明细表','Report/ProduceProcessListDetailReport.aspx')">工序生产明细表</a></li>
            <li><a onclick="f_addTab('ProduceProcessListSumReport','工序生产汇总表','Report/ProduceProcessListSumReport.aspx')">工序生产汇总表</a></li>
           --%>

           </ul>
          </div>


          <div class="nav-item nav-fund">
            <h3>仓存报表</h3>
            <ul class="sub-nav" id="report-storage">
                
                <li><a onclick="f_addTab('SumNumGoodsReport','商品库存余额表','Report/SumNumGoodsReport.aspx')">商品库存余额表</a></li>
                <li><a onclick="f_addTab('GoodsOutInDetailReport','商品收发明细表','Report/GoodsOutInDetailReport.aspx')">商品收发明细表</a></li>
                <li><a onclick="f_addTab('GoodsOutInSumReport','商品收发汇总表','Report/GoodsOutInSumReport.aspx')">商品收发汇总表</a></li>
            </ul>
          </div>
          
          <div class="nav-item nav-fund last">
            <h3>资金报表</h3>
            <ul class="sub-nav" id="report-money">
            
            <li><a onclick="f_addTab('AccountFlowReport','现金银行报表','Report/AccountFlowReport.aspx')">现金银行报表</a></li>
            <li><a onclick="f_addTab('VenderNeedPayReport','应付账款明细表','Report/VenderNeedPayReport.aspx')">应付账款明细表</a></li>
         
            <li><a onclick="f_addTab('ClientNeedPayReport','应收账款明细表','Report/ClientNeedPayReport.aspx')">应收账款明细表</a></li>
            <li><a onclick="f_addTab('StatementClient','客户对账单','Report/StatementClient.aspx')">客户对账单</a></li>
            <li><a onclick="f_addTab('StatementVender','供应商对账单','Report/StatementVender.aspx')">供应商对账单</a></li>

            <li><a onclick="f_addTab('OtherGetPayFlowReport','其他收支明细','Report/OtherGetPayFlowReport.aspx')">其他收支明细</a></li>
            
            </ul>
          </div>
          
       </div>
         
         
         </li>
        
         <li class="a4"><a href="javascript:void(0);" page="baidu"  class="baidu "><span class="moduleimg" style="background-position: -36px -288px;"></span><span>设置</span></a>
         
         <div class="sub-nav-wrap cf group-nav group-nav-b0 setting-nav">
          <div class="nav-item">
            <h3>基础资料</h3>
            <ul class="sub-nav" id="setting-base">
           
              <li><a onclick="f_addTab('ClientList','客户管理','BaseSet/ClientList.aspx')">客户管理</a></li>
              <li><a onclick="f_addTab('VenderList','供应商管理','BaseSet/VenderList.aspx')">供应商管理</a></li>
              <li><a onclick="f_addTab('GoodsList','商品管理','BaseSet/GoodsList.aspx')">商品管理</a></li>
              <li><a onclick="f_addTab('CangkuList','仓库管理','BaseSet/CangkuList.aspx')">仓库管理</a></li>
              <li><a onclick="f_addTab('AccountList','账户管理','BaseSet/AccountList.aspx')">账户管理</a></li>
              <li><a onclick="f_addTab('UsersList','员工管理','BaseSet/UsersList.aspx')">员工管理</a></li>

            

              <li><a onclick="f_addTab('processList','工序管理','BaseSet/processList.aspx')">工序管理</a></li>
           
           </ul>
          </div>
          <div class="nav-item">
            <h3>辅助资料</h3>
            <ul class="sub-nav" id="setting-auxiliary">
             
              <li><a onclick="f_addTab('ClientTypeList','客户类别','BaseSet/ClientTypeList.aspx')">客户类别</a></li>
              <li><a onclick="f_addTab('VenderTypeList','供应商类别','BaseSet/VenderTypeList.aspx')">供应商类别</a></li>
              <li><a onclick="f_addTab('GoodsTypeList','商品类别','BaseSet/GoodsTypeList.aspx')">商品类别</a></li>
              <li><a onclick="f_addTab('GoodsBrandList','商品品牌','BaseSet/GoodsBrandList.aspx')">商品品牌</a></li>
              <li><a onclick="f_addTab('PayGetList','收支类别','BaseSet/PayGetList.aspx')">收支类别</a></li>
              
              <li><a onclick="f_addTab('UnitList','计量单位','BaseSet/UnitList.aspx')">计量单位</a></li>
              <li><a onclick="f_addTab('PayTypeList','结算方式','BaseSet/PayTypeList.aspx')">结算方式</a></li>

              <li><a onclick="f_addTab('processTypeList','工序类别','BaseSet/processTypeList.aspx')">工序类别</a></li>
             
             </ul>
          </div>
          
          
          <%--   <div class="nav-item">
            <h3>系统管理</h3>
            <ul class="sub-nav" id="Ul10">
             
           <li><a onclick="f_addTab('ShopList','分店管理','BaseSet/ShopList.aspx')">分店管理</a></li>
              <li><a onclick="f_addTab('appList','应用管理','weixinQY/appList.aspx')">应用管理</a></li>
             
              <li><a onclick="f_addTab('deptList','组织架构','weixinQY/deptList.aspx')">组织架构</a></li>
              <li><a onclick="f_addTab('tagList','标签管理','weixinQY/tagList.aspx')">标签管理</a></li>
              <li><a onclick="f_addTab('RolesList','角色管理','BaseSet/RolesList.aspx')">角色管理</a></li>
             
            
             </ul>
          </div>--%>
          
          <div class="nav-item cf last">
            <h3>高级设置</h3>
            <ul class="sub-nav" id="setting-advancedSetting">
           
                
             
                
                 <li><a onclick="f_addTab('LogsList','操作日志','BaseSet/LogsList.aspx')">操作日志</a></li>
           
                 <li><a onclick="f_addTab('SystemSet','参数设置','BaseSet/SystemSet.aspx')">参数设置</a></li>

                 <li><a onclick="f_addTab('PrintSet','打印设置','BaseSet/PrintSet.aspx')">打印设置</a></li>
               
                 <li><a onclick="f_addTab('LogoSet','Logo印章','BaseSet/LogoSet.aspx')">Logo印章</a></li>
                
              <%--  <li><a onclick="f_addTab('WeixinAPP','微信公众号','BaseSet/WeixinAPP.aspx')">微信公众号</a></li>--%>

              <%--  <li><a onclick="f_addTab('WeixinQY','微信企业号','BaseSet/WeixinQY.aspx')">微信企业号</a></li>

                 <li><a onclick="f_addTab('WeixinAPP','企业微信','BaseSet/WeixinAPP.aspx')">企业微信</a></li>
                
                <li><a onclick="f_addTab('DingdingSet','阿里钉钉','BaseSet/DingdingSet.aspx')">阿里钉钉</a></li>

                 <li><a onclick="f_addTab('DeptListDD','钉钉通讯录','Dingding/DeptList.aspx')">钉钉通讯录</a></li>
                
                 <li><a onclick="f_addTab('License','使用许可','BaseSet/License.aspx')">使用许可</a></li>--%>
                
                
                
             
           
            </ul>
            <ul class="sub-nav" id="setting-advancedSetting-right" style="display:none;">
            
               <li><a >增值服务</a></li>
            
            </ul>
          </div>
        </div>
         
         </li>
        
    </ul>
</div>
   
    
    <!--内容区-->

    <div id="appContainer" class="wide-container" style="padding-left:40px;margin-right:30px;">
    
      <div id="layout1" style="width:100%; margin:0 auto; margin-top:4px;margin-left:20px;"> 
      
        
        
        
        <div position="center" id="framecenter"> 
        
            <div tabid="home" title="销售图表" style="height:400px" >
                <iframe frameborder="0" name="home" id="home" src="baiduCharts.aspx"></iframe>
            </div>
            
           
        </div> 
        
    </div>

</div>





  
    </form>
</body>
</html>

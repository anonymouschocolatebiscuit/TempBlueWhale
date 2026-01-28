<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="BlueWhale.UI.Dashboard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Welcome to BlueWhale ERP</title>

    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="renderer" content="webkit|ie-stand" />

    <!--Left Navigation & Top Begin-->

        <link rel="stylesheet" type="text/css" href="appCss/app.css" />
        <link href="https://fonts.googleapis.com/css2?family=Nunito+Sans&display=swap" rel="stylesheet"/>

    <script src="appCss/jquery-1.10.2.min.js" type="text/javascript"></script>

        <style type="text/css">
            .default #appHeader {
                height: 46px;
                position: fixed;
                top: 0;
                left: 0px;
                z-index: 99;
                width: 100%;
                font-family: 'Nunito Sans', sans-serif !important;
            }

    .default .menuList1 {
        width: 63px;
        height: 46px;
    }

    .default .headMessage {
        height: 45px;
        margin-right: 10px;
    }

            .default .mainLogo {
                float: left;
                display: block;
                width: 300px;
                height: 25px;
                margin: 10px 0 0 14px;
                font-size: 18px;
                color: #fff;
                font-family: 'Nunito Sans', sans-serif;
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

    #nav {
        z-index: 1;
        /*border-bottom:1px solid #28688b;border-color:rgba(0,0,0,0.15);*/
    }

        #nav .item {
            position: relative;
            margin-bottom: 1px;
            zoom: 1;
            float: left;
            width: 100%;
        }

    .v-standard #nav .item {
        margin-bottom: 0;
    }

            #nav .main-nav {
                position: relative;
                display: block;
                height: 80px;
                width: 100%;
                float: left;
                font-family: 'Nunito Sans', sans-serif;
            }

    .v-standard #nav .item {
        height: 84px;
    }

    #nav .on .main-nav {
        background-position: 0 -80px;
    }

    #nav .current .main-nav {
        background-position: 0 -160px;
    }

    #nav .sales {
        background-position: -98px 0;
    }

    #nav .on .sales {
        background-position: -98px -80px;
    }

    #nav .current .sales {
        background-position: -98px -160px;
    }

    #nav .storage {
        background-position: -196px 0;
    }

    #nav .on .storage {
        background-position: -196px -80px;
    }

    #nav .current .storage {
        background-position: -196px -160px;
    }

    #nav .money {
        background-position: -294px 0;
    }

    #nav .on .money {
        background-position: -294px -80px;
    }

    #nav .current .money {
        background-position: -294px -160px;
    }

    #nav .report {
        background-position: -392px 0;
    }

    #nav .on .report {
        background-position: -392px -80px;
    }

    #nav .current .report {
        background-position: -392px -160px;
    }

    #nav .setting {
        background-position: -490px 0;
    }

    #nav .on .setting {
        background-position: -490px -80px;
    }

    #nav .current .setting {
        background-position: -490px -160px;
    }

    #nav .vip {
        background-position: -582px 0;
    }

    #nav .on .vip {
        background-position: -582px -80px;
    }

    #nav .current .vip {
        background-position: -582px -160px;
    }

    #nav .arrow {
        position: absolute;
        left: 75px;
        top: 55px;
        display: block;
        text-indent: 0;
        font-weight: bold;
    }

    #nav .on .arrow {
        text-indent: -99999px;
        width: 0;
        height: 0;
        border-style: solid;
        border-width: 8px;
        border-color: transparent;
        border-right-color: white;
        border-left: none;
        left: 87px;
        top: 50%;
        margin-top: -8px;
    }

    #nav.static .current .arrow {
        text-indent: -99999px;
        width: 0;
        height: 0;
        border-style: solid;
        border-width: 8px;
        border-color: transparent;
        border-right-color: white;
        border-left: none;
        left: 87px;
        top: 50%;
        margin-top: -8px;
    }

    .v-standard #nav .on .arrow {
        text-indent: -99999px;
        width: 0;
        height: 0;
        border-style: solid;
        border-width: 8px;
        border-color: transparent;
        border-right-color: white;
        border-left: none;
        left: 87px;
        top: 50%;
        margin-top: -8px;
    }

    .v-standard #nav.static .current .arrow {
        text-indent: -99999px;
        width: 0;
        height: 0;
        border-style: solid;
        border-width: 8px;
        border-color: transparent;
        border-right-color: white;
        border-left: none;
        left: 87px;
        top: 50%;
        margin-top: -8px;
    }

    #nav .sub-nav-wrap {
        display: none;
        position: absolute;
        left: 60px;
        width: 120px;
        border: 4px solid #c7c7c7;
        border-color: rgba(0, 0, 0, 0.2);
        border-left: 0;
        border-top-right-radius: 4px;
        border-bottom-right-radius: 4px;
        z-index: 9999;
        background-color: #fff;
    }

        #nav .sub-nav-wrap b {
            position: absolute;
            left: -8px;
            margin-top: -8px;
            display: block;
            width: 0;
            height: 0;
            font-size: 0;
            overflow: hidden;
            border-style: solid;
            border-width: 8px;
            border-color: transparent;
            border-right-color: white;
            border-left: none;
        }

    #nav .single-nav b {
        top: 50%;
    }

    #nav .sub-nav {
        background-color: #fff;
        padding: 10px 0 5px;
        line-height: 24px;
    }

        #nav .sub-nav li {
            padding-bottom: 8px;
            font-size: 10px;
        }

        #nav .sub-nav a {
            padding-left: 15px;
            display: block;
            color: #888;
            zoom: 1;
        }

            #nav .sub-nav a:hover {
                background-color: #eee;
                color: #555;
            }

    #nav .group-nav {
        width: 221px;
        padding: 15px 0;
        background-color: #fff;
    }

        #nav .group-nav .nav-item {
            float: left;
            width: 230px;
            border-right: 1px dashed #ccc;
        }

        #nav .group-nav .sub-nav {
            padding-bottom: 0;
        }

        #nav .group-nav h3 {
            margin-left: 15px;
            font-size: 15px;
        }

        #nav .group-nav .last {
            border-right: 0;
        }

        #nav .group-nav b {
            bottom: 28px;
            top: inherit
        }

    #nav .group-nav-t0 {
        top: 0;
    }

        #nav .group-nav-t0 b {
            top: 36px;
        }

    #nav .group-nav-b0 {
        bottom: 0;
    }

    .v-standard #nav .group-nav b {
        bottom: 30px;
        top: inherit
    }

    #nav .vip-nav {
        width: auto;
        display: flex;
    }

        #nav .vip-nav .nav-onlineStore {
            width: 200px;
        }

        #nav .vip-nav .nav-JDstore {
            width: 220px;
        }

    #nav .report-nav {
        width: auto;
        display: flex;
    }

        #nav .report-nav .nav-pur {
            width: 300px;
        }

        /*Report List, Purchase Report*/

        #nav .report-nav .nav-sales {
            width: 280px;
        }

        /*Report List、Sales Report*/

        #nav .report-nav .nav-fund {
            width: 350px;
        }

    /*Report List、Warehouse Stock Report*/

    #nav .setting-nav {
        width: auto;
        display: flex;
    }

    #nav .produce-nav {
        width: auto;
        display: flex;
    }

    /*Production*/
    #nav .store-nav {
        width: auto;
        display: flex;
    }

    #nav .setting-nav .nav-basic-setting {
        width: 250px;
    }

    /*Finnance */
    #navScroll span {
        display: block;
        width: 47px;
        height: 23px;
        float: left;
        border-radius: 3px;
        cursor: pointer;
    }

    /*Left Navigation icon begin*/
    .moduleimg {
        background: url(images/tubiao.png) no-repeat scroll 0 0 transparent;
        height: 32px;
        width: 32px;
        margin: 0 auto;
    }
    /* Left Navigation icon end */

    .LogoutBtn {
      width: 40px;
      height: 40px;
      top: 2.5px;
      border-radius: 50%;
      background-color: rgb(57, 61, 72);
      border: none;
      font-weight: 600;
      display: flex;
      align-items: center;
      justify-content: center;
      box-shadow: 0px 0px 20px rgba(0, 0, 0, 0.164);
      cursor: pointer;
      transition-duration: .3s;
      overflow: hidden;
      position: relative;
    }

    .svgIcon {
      width: 14px;
      transition-duration: .3s;
    }

    .svgIcon path {
      fill: white;
    }

    .LogoutBtn:hover {
      width: 140px;
      border-radius: 50px;
      transition-duration: .3s;
      background-color: rgb(255, 69, 69);
      align-items: center;
    }

    .Btn:hover .svgIcon {
      width: 50px;
      transition-duration: .3s;
      opacity: 0;
    }

    .LogoutBtn::before {
      position: absolute;
      top: -20px;
      content: "Logout";
      color: white;
      transition-duration: .3s;
      font-size: 2px;
    }

    .Btn:hover::before {
      font-size: 13px;
      opacity: 1;
      transform: translateY(30px);
      transition-duration: .3s;
    }

    .ChangePasswordBtn {
      width: 40px;
      height: 40px;
      top: 2.5px;
      border-radius: 50%;
      background-color: rgb(57, 61, 72);
      border: none;
      font-weight: 600;
      display: flex;
      align-items: center;
      justify-content: center;
      box-shadow: 0px 0px 20px rgba(0, 0, 0, 0.164);
      cursor: pointer;
      transition-duration: .3s;
      overflow: hidden;
      position: relative;
    }
    
    .ChangePasswordBtn:hover {
      width: 140px;
      border-radius: 50px;
      transition-duration: .3s;
      background-color: rgb(14, 209, 24);
      align-items: center;
    }

    .ChangePasswordBtn::before {
      position: absolute;
      top: -20px;
      content: "Change Password";
      color: white;
      transition-duration: .3s;
      font-size: 2px;
    }
    </style>

    <script type="text/javascript">
        // Left Navigation
        function subindexMenu() {
            $("#nav").find("li").hover(function () {
                $(this).addClass("on");
                $(this).find("div").show();
            }, function () {
                $(this).removeClass("on");
                $(this).find("div").hide();
            });
            $("#userList").find("a").hover(function () {
                //$(this).addClass("on");
                //alert("sss");
                $(this).find("div").show();
            }, function () {
                $(this).find("div").hide();
            });
        }
        $(function () {
            subindexMenu();
        });

            function moveMenu() {
                $("#nav").find("li")(function () {
                    $(this).removeClass("on");
                    $(this).find("div").hide();
                });
            }
    </script>

    <!--Left Navigation and Top End-->

    <!--Content Area, Tab Begin-->
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
        var tab = null;
        var accordion = null;
        var tree = null;
        var tabItems = [];
        $(function () {
            // Layout heightDiff -34
            $("#layout1").ligerLayout({ leftWidth: 190, height: '100%', heightDiff: 0, space: 4, onHeightChanged: f_heightChanged });

            var height = $(".l-layout-center").height();
            // Tab
            $("#framecenter").ligerTab({
                height: height,
                showSwitchInTab: true,
                showSwitch: true,
                onAfterAddTabItem: function (tabdata) {
                    tabItems.push(tabdata);
                    saveTabStatus();
                },
                onAfterRemoveTabItem: function (tabid) {
                    for (var i = 0; i < tabItems.length; i++) {
                        var o = tabItems[i];
                        if (o.tabid == tabid) {
                            tabItems.splice(i, 1);
                            saveTabStatus();
                            break;
                        }
                    }
                },
                onReload: function (tabdata) {
                    var tabid = tabdata.tabid;
                    addFrameSkinLink(tabid);
                }
            });
            //accordion
            $("#accordion1").ligerAccordion({
                height: height - 24, speed: null
            });
            $(".l-link").hover(function () {
                $(this).addClass("l-link-over");
            }, function () {
                $(this).removeClass("l-link-over");
            });
            // tree
            $("#tree1").ligerTree({
                data: indexdata,
                checkbox: false,
                slide: false,
                nodeWidth: 120,
                attribute: ['nodename', 'url'],
                onSelect: function (node) {
                    if (!node.data.url) return;
                    var tabid = $(node.target).attr("tabid");
                    if (!tabid) {
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
        });


        function f_heightChanged(options) {
            if (tab)
                tab.addHeight(options.diff);
            if (accordion && options.middleHeight - 24 > 0)
                accordion.setHeight(options.middleHeight - 24);
        }

        // Add new right side tab
        function f_addTab(tabid, text, url) {
            tab.addTabItem({
                tabid: tabid,
                text: text,
                url: url,
                callback: function () {
                    // addShowCodeBtn(tabid); 
                    addFrameSkinLink(tabid);
                }
            });
        }
        function addShowCodeBtn(tabid) {
            var viewSourceBtn = $('<a class="viewsourcelink" href="javascript:void(0)">View Code</a>');
            var jiframe = $("#" + tabid);
            viewSourceBtn.insertBefore(jiframe);
            viewSourceBtn.click(function () {
                showCodeView(jiframe.attr("src"));
            }).hover(function () {
                viewSourceBtn.addClass("viewsourcelink-over");
            }, function () {
                viewSourceBtn.removeClass("viewsourcelink-over");
            });
        }
        function showCodeView(src) {
            $.ligerDialog.open({
                title: 'Code View',
                url: 'dotnetdemos/codeView.aspx?src=' + src,
                width: $(window).width() * 0.9,
                height: $(window).height() * 0.9
            });

        }
        function addFrameSkinLink(tabid) {
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
        function pages_init() {
            var tabJson = $.cookie('liger-home-tab');
            if (tabJson) {
                var tabitems = JSON2.parse(tabJson);
                for (var i = 0; tabitems && tabitems[i]; i++) {
                    f_addTab(tabitems[i].tabid, tabitems[i].text, tabitems[i].url);
                }
            }
        }
        function saveTabStatus() {
            $.cookie('liger-home-tab', JSON2.stringify(tabItems));
        }
        function css_init() {
            var css = $("#mylink").get(0), skin = getQueryString("skin");
            $("#skinSelect").val(skin);
            if (!css || !skin) return;
            skin = skin.toLowerCase();
            $('body').addClass("body-" + skin);
            $(css).attr("href", skin_links[skin]);
        }
        function getQueryString(name) {
            var now_url = document.location.search.slice(1), q_array = now_url.split('&');
            for (var i = 0; i < q_array.length; i++) {
                var v_array = q_array[i].split('=');
                if (v_array[0] == name) {
                    return v_array[1];
                }
            }
            return false;
        }
        function attachLinkToFrame(iframeId, filename) {
            if (!window.frames[iframeId]) return;
            var head = window.frames[iframeId].document.getElementsByTagName('head').item(0);
            var fileref = window.frames[iframeId].document.createElement("link");
            if (!fileref) return;
            fileref.setAttribute("rel", "stylesheet");
            fileref.setAttribute("type", "text/css");
            fileref.setAttribute("href", filename);
            head.appendChild(fileref);
        }
        function getLinkPrevHref(iframeId) {
            if (!window.frames[iframeId]) return;
            var head = window.frames[iframeId].document.getElementsByTagName('head').item(0);
            var links = $("link:first", head);
            for (var i = 0; links[i]; i++) {
                var href = $(links[i]).attr("href");
                if (href && href.toLowerCase().indexOf("ligerui") > 0) {
                    return href.substring(0, href.toLowerCase().indexOf("lib"));
                }
            }
        }

        function logout() {
            $.ligerDialog.confirm('Proceed to logout ?', function (yes) {
                if (yes) {

                    if (dbIdString != null) {
                        window.parent.location.href = "loginOut.aspx?dbid=" + dbIdString;
                    }
                    else {
                        window.parent.location.href = "loginOut.aspx";
                    }
                }
            });
        }
        function pwd() {
            top.topManager.openPage({
                id: 'pwd',
                href: 'pwd.aspx',
                title: 'User password-Edit'
            });
        }

        var dbIdString = getUrlParam("dbid");

        function getUrlParam(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }
    </script>

    <!--Content Area, Tab End-->

    <!--PopOut Box-->
    <script type="text/javascript">
        function f_tip() {
            $.ligerDialog.tip({ title: 'Alert', content: 'Record Deleted' + num++ });
        }
        var tip;
        var num = 0;
        function f_tip2() {
            if (!tip) {
                tip = $.ligerDialog.tip({ title: 'Alert', content: 'Record Deleted' });
            }
            else {
                var visabled = tip.get('visible');
                if (!visabled) tip.show();
                tip.set('content', 'New order require to review<a>' + num++);
            }
        }
    </script>
</head>
<body class="default">
    <form id="form1" runat="server">
        <!--HEADER-->
        <div id="appHeader">
            <div id="app/vm/Header_0">
                <div>
                    <a title="Return to Index" class="mainLogo">BlueWhale ERP
                    </a>
                </div>
                <div class="fr headMessage" id="userList">
                    <button type="button" class="Btn LogoutBtn" onclick="logout()">
                      <svg viewBox="0 0 512 512" class="svgIcon">
                        <path d="M377.9 105.9L500.7 228.7c7.2 7.2 11.3 17.1 11.3 27.3s-4.1 20.1-11.3 27.3L377.9 406.1c-6.4 6.4-15 9.9-24 9.9c-18.7 0-33.9-15.2-33.9-33.9l0-62.1-128 0c-17.7 0-32-14.3-32-32l0-64c0-17.7 14.3-32 32-32l128 0 0-62.1c0-18.7 15.2-33.9 33.9-33.9c9 0 17.6 3.6 24 9.9zM160 96L96 96c-17.7 0-32 14.3-32 32l0 256c0 17.7 14.3 32 32 32l64 0c17.7 0 32 14.3 32 32s-14.3 32-32 32l-64 0c-53 0-96-43-96-96L0 128C0 75 43 32 96 32l64 0c17.7 0 32 14.3 32 32s-14.3 32-32 32z"></path>
                      </svg>
                    </button>
                </div>
                <div class="fr headMessage" id="Div2">
                    <button type="button" class="Btn ChangePasswordBtn" onclick="f_addTab('Pwd','Change Password','Pwd.aspx')">
                      <svg viewBox="0 0 128 128" class="svgIcon">
                        <path d="M92.6 16.8c-19.5 0-35.4 15.8-35.4 35.4 0 1.3.2 4 .2 4L6.7 87.6 0 104.5l16.8 6.7 3.4-13.5 13.5 6.7 3.4-13.5 13.5 6.7L54 84.1l10 3.5s4-2 9.6-5.6c5.5 3.5 12 5.6 19 5.6 19.5 0 35.4-15.8 35.4-35.4 0-19.5-15.8-35.4-35.4-35.4zm5.1 43.8c-5.6 0-10.1-4.5-10.1-10.1s4.5-10.1 10.1-10.1 10.1 4.5 10.1 10.1-4.5 10.1-10.1 10.1z"/>
                      </svg>
                    </button>
                </div>
            </div>
        </div>

        <!-- Navigation Bar -->
        <div id="appLeftSide" class="app-menu app-left-menu admin">
            <span></span>
            <ul id="nav">
                <li class="a2">
                    <a href="javascript:void(0);" page="workrecord">
                        <span class="moduleimg" style="background-position: -36px -96px;"></span>
                    </a>
                    <div class="sub-nav-wrap group-nav group-nav-t0 vip-nav cf" style="display: none;">
                        <div class="nav-item nav-onlineStore">
                            <h3>Sales Order</h3>
                            <ul class="sub-nav" id="Ul15">
                                <li><a onclick="f_addTab('SalesOrderListAdd','Sales Order - Create','Sales/SalesOrderListAdd.aspx?id=0')">Sales Order - Create</a></li>
                                <li><a onclick="f_addTab('SalesOrderList','Sales Order- View','Sales/SalesOrderList.aspx')">Sales Order- View</a></li>
                            </ul>
                        </div>
                        <div class="nav-item nav-JDstore last">
                            <h3>Sales Outbound</h3>
                            <ul class="sub-nav" id="Ul16">
                                <li><a onclick="f_addTab('SalesReceiptListAdd','Sales Outbound  - Create','Sales/SalesReceiptListAdd.aspx?id=0')">Sales Outbound  - Create</a></li>
                                <li><a onclick="f_addTab('SalesReceiptList','Sales Outbound - View','Sales/SalesReceiptList.aspx')">Sales Outbound - View</a> </li>
                            </ul>
                        </div>
                    </div>
                </li>
                <li class="a1">
                    <a href="javascript:void(0);">
                        <span class="moduleimg" style="background-position: -36px -160px;"></span>
                    </a>

                    <div class="sub-nav-wrap group-nav group-nav-t0 produce-nav cf" style="display: none;">
                        <div class="nav-item nav-onlineStore" style="width: 130px">
                            <h3>Master Data</h3>
                            <ul class="sub-nav" id="Ul18">
                                <li><a onclick="f_addTab('goodsBomListType','BOM Grouping','produce/goodsBomListType.aspx')">BOM Grouping</a></li>
                                <li><a onclick="f_addTab('goodsBomList','BOM List','produce/goodsBomList.aspx')">BOM List</a></li>
                            </ul>
                        </div>
                        <div class="nav-item nav-onlineStore">
                            <h3>Production Planning</h3>
                            <ul class="sub-nav" id="Ul11">
                                <li><a onclick="f_addTab('produceListAdd','Production Planning - Create','produce/produceListAdd.aspx')">Production Planning - Create</a></li>
                                <li><a onclick="f_addTab('produceList','Production Planning - View','produce/produceList.aspx')">Production Planning - View</a></li>
                            </ul>
                        </div>
                        <div class="nav-item nav-onlineStore">
                            <h3>Material Requistion</h3>
                            <ul class="sub-nav" id="Ul10">
                                <li><a onclick="f_addTab('produceGetListAdd','Material Requisition - Create','produce/produceGetListAdd.aspx')">Material Requisition - Create</a></li>
                                <li><a onclick="f_addTab('produceGetList','Material Requisition- View','produce/produceGetList.aspx')">Material Requisition- View</a></li>
                            </ul>
                        </div>
                        <div class="nav-item nav-JDstore last">
                            <h3>Production Inbound</h3>
                            <ul class="sub-nav" id="Ul12">
                                <li><a onclick="f_addTab('produceInListAdd','Production Inbound - Create','produce/produceInListAdd.aspx?id=0')">Production Inbound - Create</a></li>
                                <li><a onclick="f_addTab('produceInList','Production Inbound- View','produce/produceInList.aspx')">Production Inbound- View</a></li>
                            </ul>
                        </div>
                    </div>
                </li>
                <li class="">
                    <a href="javascript:void(0);" page="customer_list">
                        <span class="moduleimg" style="background-position: -36px -64px;"></span>
                    </a>
                    <div class="sub-nav-wrap group-nav group-nav-t0 vip-nav cf" style="display: none;">
                        <div class="nav-item nav-onlineStore">
                            <h3>Purchase Order</h3>
                            <ul class="sub-nav" id="Ul13">
                                <li><a onclick="f_addTab('PurOrderListAdd','Purchase Order - Create','buy/PurOrderListAdd.aspx?id=0')">Purchase Order - Create</a></li>
                                <li><a onclick="f_addTab('PurOrderList','Purchase Order- View','buy/PurOrderList.aspx')">Purchase Order- View</a></li>
                            </ul>
                        </div>
                        <div class="nav-item nav-JDstore last">
                            <h3>Purchase Inbound - Create</h3>
                            <ul class="sub-nav" id="Ul14">
                                <li><a onclick="f_addTab('PurReceiptListAdd','Purchase Inbound  - Create','buy/PurReceiptListAdd.aspx?id=0')">Purchase Inbound  - Create</a></li>
                                <li><a onclick="f_addTab('PurReceiptList','Purchase Inbound - View','buy/PurReceiptList.aspx')">Purchase Inbound - View</a></li>
                            </ul>
                        </div>
                    </div>
                </li>
                <li class="a3">
                    <a href="javascript:void(0);" page="todo">
                        <span class="moduleimg" style="background-position: -36px -0px;"></span>
                    </a>
                    <div class="sub-nav-wrap group-nav group-nav-t0 store-nav cf" style="display: none;">
                        <div class="nav-item nav-pur">
                            <h3>Inbounds & Outbounds</h3>
                            <ul class="sub-nav" id="Ul4">
                                <li><a onclick="f_addTab('PurReceiptListCheck','Purchase Inbound(Approval)','buy/PurReceiptListCheck.aspx')">Purchase Inbound (Approval)</a></li>
                                <li><a onclick="f_addTab('SalesReceiptListCheck','Sales Outbound(Approval)','sales/SalesReceiptListCheck.aspx')">Sales Outbound (Approval)</a> </li>
                                <li><a onclick="f_addTab('OtherInListAdd','Other Inbound - Create','store/OtherInListAdd.aspx')">Other Inbound - Create</a> </li>
                                <li><a onclick="f_addTab('OtherInList','Other Inbound- View','store/OtherInList.aspx')">Other Inbound- View</a></li>
                                <li><a onclick="f_addTab('OtherOutListAdd','Other Outbound - Create','store/OtherOutListAdd.aspx')">Other Outbound - Create</a></li>
                                <li><a onclick="f_addTab('OtherOutList','Other Outbound- View','store/OtherOutList.aspx')">Other Outbound- View</a></li>
                            </ul>
                        </div>
                        <div class="nav-item nav-sales">
                            <h3>Product Assembly & Disassembly</h3>
                            <ul class="sub-nav" id="Ul5">
                                <li><a onclick="f_addTab('AssembleListAdd','Product Assembly - Create','store/AssembleListAdd.aspx')">Product Assembly - Create</a></li>
                                <li><a onclick="f_addTab('AssembleList','Product Assembly- View','store/AssembleList.aspx')">Product Assembly- View</a></li>
                                <li><a onclick="f_addTab('DisassembleListAdd','Product Disassembly - Create','store/DisassembleListAdd.aspx')">Product Disassembly - Create</a></li>
                                <li><a onclick="f_addTab('DisassembleList','Product Disassembly- View','store/DisassembleList.aspx')">Product Disassembly- View</a></li>
                            </ul>
                        </div>
                        <div class="nav-item nav-fund last">
                            <h3>Warehouse Management</h3>
                            <ul class="sub-nav" id="Ul7">
                                <li><a onclick="f_addTab('InventoryTransferListAdd','Warehouse Transfer - Create','store/InventoryTransferListAdd.aspx')">Warehouse Transfer - Create</a></li>
                                <li><a onclick="f_addTab('InventoryTransferList','Warehouse Transfer- View','store/InventoryTransferList.aspx')">Warehouse Transfer- View</a></li>
                                <li><a onclick="f_addTab('Stocktake','Stocktake - Create','store/Stocktake.aspx')">Stocktake - Create</a></li>
                                <li><a onclick="f_addTab('SumNumGoodsReport','Warehouse - View','report/SumNumGoodsReport.aspx')">Warehouse - View</a> </li>
                            </ul>
                        </div>
                    </div>
                </li>
                <li class="a4">
                    <a href="javascript:void(0);">
                        <span class="moduleimg" style="background-position: -36px -220px;"></span>
                    </a>
                    <div class="sub-nav-wrap group-nav group-nav-t0 store-nav cf" style="display: none;">
                        <div class="nav-item nav-onlineStore">
                            <h3>Collection</h3>
                            <ul class="sub-nav" id="Ul6">
                                <li><a onclick="f_addTab('ReceivableListAdd','Sales Collection - Create','pay/ReceivableListAdd.aspx')">Sales Collection - Create</a></li>
                                <li><a onclick="f_addTab('ReceivableList','Sales Collection - View','pay/ReceivableList.aspx')">Sales Collection - View</a></li>
                                <li><a onclick="f_addTab('OtherGetListAdd','Other Collection - Create','pay/OtherGetListAdd.aspx')">Other Collection - Create</a></li>
                                <li><a onclick="f_addTab('OtherGetList','Other Collection - View','pay/OtherGetList.aspx')">Other Collection - View</a></li>
                            </ul>
                        </div>
                        <div class="nav-item nav-onlineStore">
                            <h3>Payment</h3>
                            <ul class="sub-nav" id="Ul9">
                                <li><a onclick="f_addTab('PayMentListAdd','Purchase Payment - Create','pay/PayMentListAdd.aspx')">Purchase Payment - Create</a></li>
                                <li><a onclick="f_addTab('PayMentList','Purchase Payment - View','pay/PayMentList.aspx')">Purchase Payment - View</a></li>
                                <li><a onclick="f_addTab('OtherPayListAdd','Other Payment - Create','pay/OtherPayListAdd.aspx')">Other Payment - Create</a></li>
                                <li><a onclick="f_addTab('OtherPayList','Other Payment - View','pay/OtherPayList.aspx')">Other Payment - View</a></li>
                            </ul>
                        </div>
                        <div class="nav-item nav-JDstore last">
                            <h3>Settlement</h3>
                            <ul class="sub-nav" id="Ul8">
                                <li><a onclick="f_addTab('CheckBillGetListAdd',' Collection Write-off - Create','pay/CheckBillGetListAdd.aspx')">Collection Write-off - Create</a></li>
                                <li><a onclick="f_addTab('CheckBillGetList','Collection Write-off - View','pay/CheckBillGetList.aspx')">Collection Write-off - View</a></li>
                                <li><a onclick="f_addTab('CheckBillPayListAdd','Payment Write-off - Create','pay/CheckBillPayListAdd.aspx')">Payment Write-off - Create</a></li>
                                <li><a onclick="f_addTab('PayMentList',' Payment Write-off - View','pay/PayMentList.aspx')"> Payment Write-off - View</a></li>
                            </ul>
                        </div>
                    </div>
                </li>
                <li class="a4">
                    <a href="javascript:void(0);" page="baidu" class="baidu ">
                        <span class="moduleimg" style="background-position: -36px -256px;"></span>
                    </a>
                    <div class="sub-nav-wrap group-nav group-nav-t0 report-nav cf" style="display: none;">

                        <div class="nav-item nav-pur">
                            <h3>Purchase Reports</h3>
                            <ul class="sub-nav" id="report-purchase">
                                <li><a onclick="f_addTab('PurOrderListReport','Purchase Order Tracking Report','Report/PurOrderListReport.aspx')">Purchase Order Tracking Report</a></li>
                                <li><a onclick="f_addTab('PurOrderListDetailReport','Purchase Detail Report','Report/PurOrderListDetailReport.aspx')">Purchase Detail Report</a></li>
                                <li><a onclick="f_addTab('PurOrderListSumGoodsReport','Purchase Summary Report (By Product)','Report/PurOrderListSumGoodsReport.aspx')">Purchase Summary Report (By Product)</a></li>
                                <li><a onclick="f_addTab('PurOrderListSumVenderReport','Purchase Summary Report (By Supplier)','Report/PurOrderListSumVenderReport.aspx')">Purchase Summary Report (By Supplier)</a></li>
                            </ul>
                        </div>
                        <div class="nav-item nav-sales">
                            <h3>Sales Reports</h3>
                            <ul class="sub-nav" id="report-sales">
                                <li><a onclick="f_addTab('SalesOrderListReport','Sales Order Tracking Report','Report/SalesOrderListReport.aspx')">Sales Order Tracking Report</a></li>
                                <li><a onclick="f_addTab('SalesOrderListDetailReport','Sales Detail Report','Report/SalesOrderListDetailReport.aspx')">Sales Detail Report</a></li>
                                <li><a onclick="f_addTab('SalesOrderListSumGoodsReport','Sales Summary Report (By Product)','Report/SalesOrderListSumGoodsReport.aspx')">Sales Summary Report (By Product)</a></li>
                                <li><a onclick="f_addTab('SalesOrderListSumVenderReport','Sales Summary Report (By Client)','Report/SalesOrderListSumClientReport.aspx')">Sales Summary Report (By Client)</a></li>
                            </ul>
                        </div>
                        <div class="nav-item nav-sales">
                            <h3>Production Reports</h3>
                            <ul class="sub-nav" id="Ul17">
                                <li><a onclick="f_addTab('ProduceListReport','Production Planning Tracking Report','Report/ProduceListReport.aspx')">Production Planning Tracking Report</a></li>
                                <li><a onclick="f_addTab('ProduceGetListReportDetail','Material Requisition Detail Report','Report/ProduceGetListReportDetail.aspx')">Material Requisition Detail Report</a></li>
                                <li><a onclick="f_addTab('ProduceGetListReportSum','Material Requisition Summary Report','Report/ProduceGetListReportSum.aspx')">Material Requisition Summary Report</a></li>
                                <li><a onclick="f_addTab('ProduceListDetailReport','Production Inbound Detail Report','Report/ProduceListDetailReport.aspx')">Production Inbound Detail Report</a></li>
                                <li><a onclick="f_addTab('ProduceListSumGoodsReport','Production Inbound Summary Report','Report/ProduceListSumGoodsReport.aspx')">Production Inbound Summary Report</a></li>
                            </ul>
                        </div>
                        <div class="nav-item nav-fund">
                            <h3>Warehouse Reports</h3>
                            <ul class="sub-nav" id="report-storage">
                                <li><a onclick="f_addTab('SumNumGoodsReport','Product Warehouse Balance Report','Report/SumNumGoodsReport.aspx')">Product Warehouse Balance Report</a></li>
                                <li><a onclick="f_addTab('GoodsOutInDetailReport','Product Inbound & Outbound Detail Report','Report/GoodsOutInDetailReport.aspx')">Product Inbound & Outbound Detail Report</a></li>
                                <li><a onclick="f_addTab('GoodsOutInSumReport','Product Inbound & Outbound Summary Report','Report/GoodsOutInSumReport.aspx')">Product Inbound & Outbound Summary Report</a></li>
                            </ul>
                        </div>
                        <div class="nav-item nav-fund last">
                            <h3>Financial Reports</h3>
                            <ul class="sub-nav" id="report-money">
                                <li><a onclick="f_addTab('AccountFlowReport','Cash & Bank Report','Report/AccountFlowReport.aspx')">Cash & Bank Report</a></li>
                                <li><a onclick="f_addTab('VendorNeedPayReport','Accounts Payable Detail Report','Report/VendorNeedPayReport.aspx')">Accounts Payable Detail Report</a></li>
                                <li><a onclick="f_addTab('ClientNeedPayReport','Accounts Receivable Detail Report','Report/ClientNeedPayReport.aspx')">Accounts Receivable Detail Report</a></li>
                                <li><a onclick="f_addTab('StatementClient','Client Statement','Report/StatementClient.aspx')">Client Statement</a></li>
                                <li><a onclick="f_addTab('StatementVender','Supplier Statement','Report/StatementVender.aspx')">Supplier Statement</a></li>
                                <li><a onclick="f_addTab('OtherGetPayFlowReport','Other Income & Expense Detail Report','Report/OtherGetPayFlowReport.aspx')">Other Income & Expense Detail Report</a></li>
                            </ul>
                        </div>
                    </div>
                </li>
                <li class="a4">
                    <a href="javascript:void(0);" page="baidu" class="baidu ">
                        <span class="moduleimg" style="background-position: -36px -288px;"></span>
                    </a>
                    <div class="sub-nav-wrap cf group-nav group-nav-t0 setting-nav" style="display: none;">
                        <div class="nav-item">
                            <h3>Basic Settings</h3>
                            <ul class="sub-nav" id="setting-base">
                                <li><a onclick="f_addTab('ClientList','Client Management','BaseSet/ClientList.aspx')">Client Management</a></li>
                                <li><a onclick="f_addTab('VenderList','Supplier Management','BaseSet/VenderList.aspx')">Supplier Management</a></li>
                                <li><a onclick="f_addTab('GoodsList','Product Management','BaseSet/GoodsList.aspx')">Product Management</a></li>
                                <li><a onclick="f_addTab('InventoryList','Warehouse Management','BaseSet/InventoryList.aspx')">Warehouse Management</a></li>
                                <li><a onclick="f_addTab('AccountList','Account Management','BaseSet/AccountList.aspx')">Account Management</a></li>
                                <li><a onclick="f_addTab('UsersList','User Management','BaseSet/UsersList.aspx')">User Management</a></li>
                                <li><a onclick="f_addTab('processList','Process Management','BaseSet/processList.aspx')">Process Management</a></li>
                            </ul>
                        </div>
                        <div class="nav-item">
                            <h3>Auxiliary Settings</h3>
                            <ul class="sub-nav" id="setting-auxiliary">
                                <li><a onclick="f_addTab('ClientTypeList','Client Category','BaseSet/ClientTypeList.aspx')">Client Category</a></li>
                                <li><a onclick="f_addTab('VenderTypeList','Supplier Category','BaseSet/VenderTypeList.aspx')">Supplier Category</a></li>
                                <li><a onclick="f_addTab('GoodsTypeList','Item Category','BaseSet/GoodsTypeList.aspx')">Item Category</a></li>
                                <li><a onclick="f_addTab('GoodsBrandList','Item Brand','BaseSet/GoodsBrandList.aspx')">Item Brand</a></li>
                                <li><a onclick="f_addTab('PayGetList','Income & Expense Category','BaseSet/PayGetList.aspx')">Income & Expense Category</a></li>
                                <li><a onclick="f_addTab('UnitList','Unit of Measurement','BaseSet/UnitList.aspx')">Unit of Measurement</a></li>
                                <li><a onclick="f_addTab('PayTypeList','Settlement Method','BaseSet/PayTypeList.aspx')">Settlement Method</a></li>
                                <li><a onclick="f_addTab('processTypeList','Process Category','BaseSet/ProcessTypeList.aspx')">Process Category</a></li>
                            </ul>
                        </div>
                        <div class="nav-item cf last">
                            <h3>Advanced Settings</h3>
                            <ul class="sub-nav" id="setting-advancedSetting">
                                <li><a onclick="f_addTab('LogsList','Operation Log','BaseSet/LogsList.aspx')">Operation Log</a></li>
                                <li><a onclick="f_addTab('SystemSet','Parameter Settings','BaseSet/SystemSet.aspx')">Parameter Settings</a></li>
                                <li><a onclick="f_addTab('PrintSet','Print Settings','BaseSet/PrintSet.aspx')">Print Settings</a></li>
                                <li><a onclick="f_addTab('LogoSet','Logo & Stamp','BaseSet/LogoSet.aspx')">Logo & Stamp</a></li>
                            </ul>
                        </div>
                    </div>
                </li>
            </ul>
        </div>
        <!--Content Area-->
        <div id="appContainer" class="wide-container" style="padding-left: 40px; margin-right: 30px;">
            <div id="layout1" style="width: 100%; margin: 0 auto; margin-top: 4px; margin-left: 20px;">
                <div position="center" id="framecenter">
                    <div tabid="home" title="Sales Report" style="height: 400px">
                        <iframe frameborder="0" id="home" src="saleCharts.aspx"></iframe>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

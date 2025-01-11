(function($){

    var hitch = function(scope , func){  //切换作用域
        return function(){
           return func.apply(scope , arguments);
        }
    };

    var CheckConstructor = function(){
        //debugger;
        //private define
        var _data = {};
        var orgAccount = '';
        var orgId;
        var orgFullName;

        var templates = [
            '<div class="list" style="display:none;">', //style="display:none;"
                '<div class="title">',
                    '<div class="t1">客户管家授权邀请</div>',
                '</div>',
                '<div class="content" id="roll" orgAccount>',
                    '<div class="inviteListTotal"></div>',
                    '<div class="orgListTotal"></div>',
                '</div>',
            '</div>',
            '<div class="mask opacity"></div>'//半透明遮罩层
        ].join('');


        //初始化
        this.init = function(params){
            console.log(params);
            var parentNode = params.parentNode || document.body;
            //console.log(parentNode);
            console.log(templates);
            $(parentNode).html(templates.replace("orgAccount","orgAccount='"+orgAccount+"'"));

            this.domNode = parentNode;

            //1.查列表数据
            hitch(this , _initData)(); //从windows上的作用域切换到this,匿名函数调用这个函数
            //2.绑定相关dom的事件,比如进入,开通,接受邀请等按钮的点击事件
            _initEvent();
        };

        //显示列表
        var _show = function(data){
            $('.list').show();
        };

        var _initData = function(){

            //调邀请列表和企业列表的接口
            $.ajax({
                url : 'index/inviteAndOrgList' ,
                dataType : 'json',
                type : 'GET',
                success : function (data) {
                    console.log(data);
                    if(data.result){
                        hitch(this , _queryDataSuccessHandler)(data);
                        $('.inviteListTotal').html(inviteHtml.join(''));
                        $('.orgListTotal').html(orgHtml.join(''));
                    }else{

                        //console.log('一个组件');
                    }
                },
                error : _queryDataFailedHandler
            });
        };

        var _queryDataSuccessHandler = function(data){
            console.log(data);
            if(!data.result){
                _queryDataFailedHandler();
                return ;
            }
            var inviteList = data.data.inviteList;
            var orgList = data.data.orgList;
            var appDefaultOrgId = data.data.appDefaultOrgId;

            var status = 'enter';
            //1.inviteList.length == 0 && orgList.length == 0           显示一个"开通应用"按钮
            //2.inviteList.length == 0 && orgList.length != 0           判断默认企业是否有权限?进入应用:显示列表
            //3.inviteList.length != 0 && orgList.length == 0           显示列表
            //4.inviteList.length != 0 && orgList.length != 0            显示列表
            //debugger;
                //判断几种情况
                if (inviteList.length != 0) {   //判断是否有邀请
                    status = 'showList';       //有邀请时，显示列表
                } else {
                    if(orgList.length == 0){    //判断企业列表是否为0
                        status = 'showBtn';    //企业列表为0时，显示“开通应用”按钮
                    }
                    //else{
                       //if(orgList.length==1 && !orgList[0].hasInstall){
                       //     status = 'showBtn';
                       //     orgAccount=orgList[0].orgAccount;
                       //     orgId=orgList[0].orgId;
                       //     console.log(orgId);
                       //}
                        else{
                            for (var i = 0, len = orgList.length ; i < len ; i++ ){
                                if((orgList[i].orgId == appDefaultOrgId)){  //判断是否是默认企业
                                    if(orgList[i].hasAuth){
                                        status = 'enter';  //有权限，进入应用
                                        orgAccount = orgList[i].orgAccount;
                                    }else{
                                        status = 'showList';  //无权限，显示列表
                                    }
                                    break;
                                }
                            }
                        }
                    //}
                }
            switch(status){
                case "enter":
                    //直接进入
                    //window.location.href = 'http://'+ orgAccount + ".chanapp.com/chanjet/customer/";
                    window.location.href = 'http://i.chanjet.com/wait?app=customer&org='+orgAccount;
                    console.log("直接进入");
                    break;

                case "showList":
                    $('.list').css({"display":"inline"});
                    //显示列表分2步：
                    //1.遍历数据,生成html , append到dom树中,默认是隐藏;
                    hitch(this , _generateHtml)(data);
                    //2 show
                    _show();
                    break;

                case "showBtn":
                    //console.log(data);

                    //显示"开通应用"按钮
                    $('.list').css({"display":"inline"});
                    $('.content').html('<div class="kt"><p>[未命名企业]</p><a class="ktyy" href="javascript:void(0);">开通应用</a></div>');
                    $('.content').delegate('.ktyy' , 'click' , function(){

                        $.ajax({
                            url : '/index/activeApp' ,
                            dataType : 'json',
                            type : 'POST',
                            success : function (data) {
                                console.log(data);
                                //debugger;
                                orgAccount = data.data.orgAccount;
                                if(data.result){

                                    $('.ktyy').html('进入');
                                    $('.ktyy').click(function () {
                                        window.location.href = 'http://i.chanjet.com/wait?app=customer&org='+orgAccount;
                                    });

                                }else {
                                    console.log('请求结果不正确');
                                }
                            },
                            error : _queryDataFailedHandler
                        });
                    //window.location.href = 'http://' + orgAccount + ".chanapp.com/chanjet/customer/";
                    });
                    break;
            }
        };

        //var _queryDataFailedHandler = function(){
        //    //reload , 提示用户错误
        //    console.log('load error')
        //    //改动
        //    $(".errorprompt").show();
        //    setTimeout("$('.errorprompt').fadeOut();",3000);
        //};

        //给列表绑定事件
        var _initEvent = function(){
            //代理, 原理是事件流
            //“进入”的点击事件
            //$('.orgListTotal').delegate('.enterbutton' , 'click' , function(){ //绑定.orgListTotal
            //    window.location.href = 'http://' + orgAccount + ".chanapp.com/chanjet/customer/";
            //});

            //“接受邀请”的点击事件
            $('.inviteListTotal').delegate('.acceptbutton' , 'click' , function(){

                //通过当前点击的元素获取orgid
                var orgId = $(this).closest('.invitePanel').attr('orgid');

                var orgAccount = $(this).closest('.invitePanel').attr('orgAccount');

                var orgFullName = $(this).closest('.invitePanel').attr('orgFullName');

                //调用接受邀请接口
                $.ajax({
                    url : 'index/acceptInvitation' ,
                    dataType : 'json',
                    type : 'POST',
                    data : {
                                orgPrinciple : orgId,
                                orgType : '1',
                                isAccept : '1'
                            },
                    success : function (data) {
                        console.log('wfy');
                        console.log(data);

                        if(data.result){
                            //加入企业成功
                            if(data.data.join){
                                var html = [
                                    '<div class="orgTxt">',
                                        orgFullName,
                                    '</div>'
                                 ];
                                //授权成功
                                if(data.data.auth){
                                    //html.push( '<a class="enterbutton" orgAccount="'+ orgAccount+'" href="http://'+orgAccount+'.chanapp.com/chanjet/customer/"> 进入</a>');
                                    html.push( '<a class="enterbutton" orgAccount="'+ orgAccount+'" href="http://i.chanjet.com/wait?app=customer&org='+orgAccount+'"> 进入</a>');
                                }else{
                                    html.push('<a class="noauth"> 无权限进入应用，请您尽快联系管理员。</a>');
                                }
                                $('[orgid='+orgId+'].invitePanel').html(html.join(''));

                            }else{
                                //console.log('该企业没有开通');
                            }
                        }else{
                            //console.log('existsUserAcceptJoinOrg');
                        }
                    },
                    error : _queryDataFailedHandler
                });
            });

            //开通
            $('.orgListTotal').delegate('.kaitongbutton' , 'click' , function(){
                var orgId = $(this).attr('orgId');
                var orgAccount = $(this).attr('orgAccount');
                var self = this;
                console.log(orgId);
                $.ajax({
                    url : 'index/activeApp' ,
                    dataType : 'json',
                    type : 'POST',
                    data : { orgId : orgId },
                    success : function (data) {
                        console.log(data);
                        if(data.result){
                            //$(self).html('进入').attr('href' , 'http://' + orgAccount + '.chanapp.com/chanjet/customer/')
                            $(self).html('进入').attr('href' , 'http://i.chanjet.com/wait?app=customer&org=' + orgAccount)

                        }else {
                            //console.log(data);
                        }
                    },
                    error : _queryDataFailedHandler
                });
            });

            //“开通应用”的点击事件
            /*$('.content').delegate('.kaitongbutton' , 'click' , function(){
                $.ajax({
                    url : 'index/activeApp' ,
                    dataType : 'json',
                    type : 'POST',
                    data : { orgId : orgId },
                    success : function (data) {
                        console.log(data);
                        if(data.result){
                            window.location.href = 'http://' + orgAccount + ".chanapp.com/chanjet/customer/";
                        }else {
                            console.log(data);
                        }
                    }
                    //error : _queryDataFailedHandler
                });
            });*/
        };

        var inviteHtml = [];
        var orgHtml = [];
        var _generateHtml = function(data){

            var inviteList = data.data.inviteList;
            var orgList = data.data.orgList;
            var appDefaultOrgId= data.data.appDefaultOrgId;

            //遍历邀请列表
            for(var i in inviteList){
                var txt1 = '【'+ inviteList[i].applyerName+'】'+'<span>邀请您加入</span>'+inviteList[i].orgFullName;  //邀请人名称 和 邀请加入的企业
                inviteHtml.push([  //拼接字段
                     '<div class="invitePanel" orgId="',inviteList[i].orgId,'"', ' ',
                        'orgFullName="',inviteList[i].orgFullName,'"',
                        'orgAccount="',inviteList[i].orgAccount,'">',
                        '<div class="inviteTxt">',
                            txt1,
                        '</div>',
                        '<a class="acceptbutton" href="javascript:void(0);">接受邀请</a>',
                    '</div>'
                 ].join('')); //以空连接
            }

            //遍历企业列表
            for(var i in orgList){
                var orgAccount = orgList[i].orgAccount;
                var txt2= orgList[i].orgFullName == "" ? "[未命名企业]" : orgList[i].orgFullName;
                var btn = '';

                if (orgList[i].hasAuth) {
                    //btn = '<a class="enterbutton" orgAccount="'+ orgList[i].orgAccount +'" href="http://'+orgAccount+'.chanapp.com/chanjet/customer/"> 进入</a>';
                    btn = '<a class="enterbutton" orgAccount="'+ orgList[i].orgAccount +'" href="http://i.chanjet.com/wait?app=customer&org='+orgAccount+'"> 进入</a>';
                    //$('.orgListTotal').delegate('.enterbutton' , 'click' , function(){
                    //    //动态获取orgAccount的值，负责一直进入i的最后一个值
                    //    window.location.href = 'http://' + $(this).attr('orgAccount') + ".chanapp.com/chanjet/customer/";
                    //});
                } else {
                    if (orgList[i].hasInstall) {
                        btn = '<a class="noauth"> 无权限进入应用，请您尽快联系管理员。</a>';
                    } else {
                        btn = '<a class="kaitongbutton" orgAccount="'+orgList[i].orgAccount+'" orgId="'+ orgList[i].orgId +'" href="javascript:void(0);"> 开通</a>';
                        //“开通”的点击事件
                        //debugger;


                    }
                }
                orgHtml.push([
                    '<div class="orgPanel">',
                        '<div class="orgTxt">',
                            txt2,
                        '</div>',
                        //'<a class="enterbutton" orgAccount="'+ orgAccount+'" href="javascript:void(0);"> 进入</a>',
                            btn,
                        '</a>',
                    '</div>'
                ].join(''));
            }

            //将邀请和企业列表生成的页面加载到html中
            $(this.domNode).find('.inviteListTotal').html(inviteHtml.join('')); //清空之前的数据html
            $(this.domNode).find('.orgListTotal').html(orgHtml.join('')); //清空之前的数据html
        }
    };

    window.CheckConstructor = CheckConstructor;
})(window.jQuery);

/** 给window绑定事件，滚动时，头悬浮显示*/
function windowScrollEvent(){
    $(window).on('scroll',function(){
        var s_top = $(document).scrollTop();
        if(s_top > 0){
            $("#header").removeClass("headerBlack").addClass("headerWhite");
        }else{
            //$("#header").removeClass("headerWhite").addClass("headerBlack");
        }
        //$("#header").css("top",s_top);
        
        var s_left = $(document).scrollLeft();
        console.log(s_left);
        if(s_left>=0){
            $('.headerbox .w1200').css("margin-left",-s_left);
        }

    }).trigger('scroll');
}

 _queryDataFailedHandler = function(){
    //改动
    $(".errorprompt").show();
    setTimeout("$('.errorprompt').fadeOut();",3000);
};

function checkLoginStatus(){
    $.ajax({
        url :'http://cia.chanapp.chanjet.com/internal_api/authorizeByJsonp?client_id=newapp&jsonp=true&_=1444704891844',
        dataType : 'jsonp',
        type : 'get',
        data: {jsonp: true},
        success : function(data){
            if (data.code) {
                codeLogin(data.code);
            }
        },
        error: _queryDataFailedHandler
    });
}

function codeLogin(code,Fn) {
    $.ajax({
        url: '/login/codeLogin',
        type: 'POST',
        data : {code :code},
        dataType : 'json',

        //判断后台是否登录成功
        success: function(loginResult){
            if(loginResult.result){
                renderUserInfo(loginResult.data);
                orgListShow();
            }else{
                console.log('false');
            }
        },
        error: _queryDataFailedHandler
    });
}

function renderUserInfo(res){
    //console.log(res);
    var nameTmp = res.name || res.nickName || res.mobile;
    if (nameTmp && nameTmp.length > 10) { //用户姓名
        nameTmp = nameTmp.substring(0, 10) + "...";
    }

    //头像加载失败时,显示默认头像
    var defaultImg = 'http://i.static.chanjet.com/chanjet/images/workbench/user.png';
    var headPictrue = res.headPicture;
    //console.log(headPictrue);
    if(!headPictrue){
        headPictrue = defaultImg;
    }

    $('#LoginUserInfo img').attr("src",headPictrue);
    $('#LoginUserInfo .username').html(nameTmp);
    $('#waitLogin').hide();
    $('#LoginUserInfo').show();
}

var orgListShow= function () {
    new CheckConstructor().init({
        parentNode : $('.inviteListWrapper')[0]
    });
    /*$("#roll").niceScroll({
        cursorcolor:"#aaaaab",
        cursoropacitymax:1,
        touchbehavior:false,
        cursorwidth:"10px",
        //cursortop:"70px",
        cursorborder:"0",
        cursorborderradius:"10px"
    });*/
};

/**登录页面*/
function login() {
    var params = {
        loginSuccess: function () {
            checkLoginStatus();
        }
    };
    var Clogin = ChanjetLogin.getInstance(params);
    Clogin.show();
}

windowScrollEvent(); //方法调用
checkLoginStatus();

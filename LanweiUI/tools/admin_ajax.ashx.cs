﻿using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.SessionState;
using Lanwei.Weixin.Web.UI;
using Lanwei.Weixin.Common;

//using Lanwei.Weixin.API.Payment.wxpay;

namespace Lanwei.Weixin.Web.tools
{
    /// <summary>
    /// admin_ajax 的摘要说明
    /// </summary>
    public class admin_ajax : IHttpHandler, IRequiresSessionState
    {


        public void ProcessRequest(HttpContext context)
        {
            //取得处事类型
            string action = MXRequest.GetQueryString("action");

            switch (action)
            {
                case "attribute_field_validate": //验证扩展字段是否重复
                    attribute_field_validate(context);
                    break;
                case "channel_category_validate": //验证频道分类目录是否重复
                    channel_category_validate(context);
                    break;
                case "channel_validate": //验证频道名称是否重复
                    channel_validate(context);
                    break;
                case "urlrewrite_name_validate": //验证URL调用名称是否重复
                    urlrewrite_name_validate(context);
                    break;
                case "navigation_validate": //验证导航菜单ID是否重复
                    navigation_validate(context);
                    break;
                case "manager_validate": //验证管理员用户名是否重复
                    manager_validate(context);
                    break;
                case "get_remote_fileinfo": //获取远程文件的信息
                    get_remote_fileinfo(context);
                    break;
                case "get_navigation_list": //获取后台导航字符串
                    get_navigation_list(context);
                    break;
                case "edit_order_status": //修改订单信息和状态
                    edit_order_status(context);
                    break;
                case "validate_username": //验证会员用户名是否重复
                    validate_username(context);
                    break;
                case "sms_message_post": //发送手机短信
                    sms_message_post(context);
                    break;
                case "get_builder_urls": //获取要生成静态的地址
                    get_builder_urls(context);
                    break;
                case "get_builder_html": //生成静态页面
                    get_builder_html(context);
                    break;
            }

        }

        #region 验证扩展字段是否重复============================
        private void attribute_field_validate(HttpContext context)
        {
            string column_name = MXRequest.GetString("param");
            if (string.IsNullOrEmpty(column_name))
            {
                context.Response.Write("{ \"info\":\"名称不可为空\", \"status\":\"n\" }");
                return;
            }
            BLL.article_attribute_field bll = new BLL.article_attribute_field();
            if (bll.Exists(column_name))
            {
                context.Response.Write("{ \"info\":\"该名称已被占用，请更换！\", \"status\":\"n\" }");
                return;
            }
            context.Response.Write("{ \"info\":\"该名称可使用\", \"status\":\"y\" }");
            return;
        }
        #endregion

        #region 验证频道分类生成目录名是否可用==================
        private void channel_category_validate(HttpContext context)
        {
            string build_path = MXRequest.GetString("param");
            string old_build_path = MXRequest.GetString("old_build_path");
            if (string.IsNullOrEmpty(build_path))
            {
                context.Response.Write("{ \"info\":\"该目录名不可为空！\", \"status\":\"n\" }");
                return;
            }
            if (build_path.ToLower() == old_build_path.ToLower())
            {
                context.Response.Write("{ \"info\":\"该目录名可使用\", \"status\":\"y\" }");
                return;
            }
            BLL.channel_category bll = new BLL.channel_category();
            if (bll.Exists(build_path))
            {
                context.Response.Write("{ \"info\":\"该目录名已被占用，请更换！\", \"status\":\"n\" }");
                return;
            }
            context.Response.Write("{ \"info\":\"该目录名可使用\", \"status\":\"y\" }");
            return;
        }
        #endregion

        #region 验证频道名称是否是否可用========================
        private void channel_validate(HttpContext context)
        {
            string channel_name = MXRequest.GetString("param");
            string old_channel_name = MXRequest.GetString("old_channel_name");
            if (string.IsNullOrEmpty(channel_name))
            {
                context.Response.Write("{ \"info\":\"频道名称不可为空！\", \"status\":\"n\" }");
                return;
            }
            if (channel_name.ToLower() == old_channel_name.ToLower())
            {
                context.Response.Write("{ \"info\":\"该名称可使用\", \"status\":\"y\" }");
                return;
            }
            BLL.channel bll = new BLL.channel();
            if (bll.Exists(channel_name))
            {
                context.Response.Write("{ \"info\":\"该名称已被占用，请更换！\", \"status\":\"n\" }");
                return;
            }
            context.Response.Write("{ \"info\":\"该名称可使用\", \"status\":\"y\" }");
            return;
        }
        #endregion

        #region 验证URL调用名称是否重复=========================
        private void urlrewrite_name_validate(HttpContext context)
        {
            string new_name = MXRequest.GetString("param");
            string old_name = MXRequest.GetString("old_name");
            if (string.IsNullOrEmpty(new_name))
            {
                context.Response.Write("{ \"info\":\"名称不可为空！\", \"status\":\"n\" }");
                return;
            }
            if (new_name.ToLower() == old_name.ToLower())
            {
                context.Response.Write("{ \"info\":\"该名称可使用\", \"status\":\"y\" }");
                return;
            }
            BLL.url_rewrite bll = new BLL.url_rewrite();
            if (bll.Exists(new_name))
            {
                context.Response.Write("{ \"info\":\"该名称已被使用，请更换！\", \"status\":\"n\" }");
                return;
            }
            context.Response.Write("{ \"info\":\"该名称可使用\", \"status\":\"y\" }");
            return;
        }
        #endregion

        #region 验证导航菜单ID是否重复==========================
        private void navigation_validate(HttpContext context)
        {
            string navname = MXRequest.GetString("param");
            string old_name = MXRequest.GetString("old_name");
            if (string.IsNullOrEmpty(navname))
            {
                context.Response.Write("{ \"info\":\"该导航菜单ID不可为空！\", \"status\":\"n\" }");
                return;
            }
            if (navname.ToLower() == old_name.ToLower())
            {
                context.Response.Write("{ \"info\":\"该导航菜单ID可使用\", \"status\":\"y\" }");
                return;
            }
            //检查保留的名称开头
            if (navname.ToLower().StartsWith("channel_"))
            {
                context.Response.Write("{ \"info\":\"该导航菜单ID系统保留，请更换！\", \"status\":\"n\" }");
                return;
            }
            BLL.navigation bll = new BLL.navigation();
            if (bll.Exists(navname))
            {
                context.Response.Write("{ \"info\":\"该导航菜单ID已被占用，请更换！\", \"status\":\"n\" }");
                return;
            }
            context.Response.Write("{ \"info\":\"该导航菜单ID可使用\", \"status\":\"y\" }");
            return;
        }
        #endregion

        #region 验证管理员用户名是否重复========================
        private void manager_validate(HttpContext context)
        {
            string user_name = MXRequest.GetString("param");
            if (string.IsNullOrEmpty(user_name))
            {
                context.Response.Write("{ \"info\":\"请输入用户名\", \"status\":\"n\" }");
                return;
            }
            BLL.manager bll = new BLL.manager();
            if (bll.Exists(user_name))
            {
                context.Response.Write("{ \"info\":\"用户名已被占用，请更换！\", \"status\":\"n\" }");
                return;
            }
            context.Response.Write("{ \"info\":\"用户名可使用\", \"status\":\"y\" }");
            return;
        }
        #endregion

        #region 获取远程文件的信息==============================
        private void get_remote_fileinfo(HttpContext context)
        {
            string filePath = MXRequest.GetFormString("remotepath");
            if (string.IsNullOrEmpty(filePath))
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"没有找到远程附件地址！\"}");
                return;
            }
            if (!filePath.ToLower().StartsWith("http://"))
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"不是远程附件地址！\"}");
                return;
            }
            try
            {
                HttpWebRequest _request = (HttpWebRequest)WebRequest.Create(filePath);
                HttpWebResponse _response = (HttpWebResponse)_request.GetResponse();
                int fileSize = (int)_response.ContentLength;
                string fileName = filePath.Substring(filePath.LastIndexOf("/") + 1);
                string fileExt = filePath.Substring(filePath.LastIndexOf(".") + 1).ToUpper();
                context.Response.Write("{\"status\": 1, \"msg\": \"获取远程文件成功！\", \"name\": \"" + fileName + "\", \"path\": \"" + filePath + "\", \"size\": " + fileSize + ", \"ext\": \"" + fileExt + "\"}");
            }
            catch
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"远程文件不存在！\"}");
                return;
            }
        }
        #endregion

        #region 获取后台导航字符串==============================
        private void get_navigation_list(HttpContext context)
        {
            Model.manager adminModel = new ManagePage().GetAdminInfo(); //获得当前登录管理员信息
            if (adminModel == null)
            {
                return;
            }
            Model.manager_role roleModel = new BLL.manager_role().GetModel(adminModel.role_id); //获得管理角色信息
            if (roleModel == null)
            {
                return;
            }
            DataTable dt = new BLL.navigation().GetDataList(0, MXEnums.NavigationEnum.System.ToString());
            this.get_navigation_childs(context, dt, 0, "", roleModel.role_type, roleModel.manager_role_values);

        }
        private void get_navigation_childs(HttpContext context, DataTable oldData, int parent_id, string parent_name, int role_type, List<Model.manager_role_value> ls)
        {
            DataRow[] dr = oldData.Select("parent_id=" + parent_id);
            bool isWrite = false;
            for (int i = 0; i < dr.Length; i++)
            {
                //检查是否显示在界面上====================
                bool isActionPass = true;
                if (int.Parse(dr[i]["is_lock"].ToString()) == 1)
                {
                    isActionPass = false;
                }
                //检查管理员权限==========================
                if (isActionPass && role_type > 1)
                {
                    string[] actionTypeArr = dr[i]["action_type"].ToString().Split(',');
                    foreach (string action_type_str in actionTypeArr)
                    {
                        //如果存在显示权限资源，则检查是否拥有该权限
                        if (action_type_str == "Show")
                        {
                            Model.manager_role_value modelt = ls.Find(p => p.nav_name == dr[i]["name"].ToString() && p.action_type == "Show");
                            if (modelt == null)
                            {
                                isActionPass = false;
                            }
                        }
                    }
                }
                //如果没有该权限则不显示
                if (!isActionPass)
                {
                    if (isWrite && i == (dr.Length - 1) && parent_id > 0 && parent_name != "sys_contents")
                    {
                        context.Response.Write("</ul>\n");
                    }
                    continue;
                }
                //输出开始标记
                if (i == 0 && parent_id > 0 && parent_name != "sys_contents")
                {
                    isWrite = true;
                    context.Response.Write("<ul>\n");
                }
                //以下是输出选项内容=======================
                if (int.Parse(dr[i]["class_layer"].ToString()) == 1)
                {
                    context.Response.Write("<div class=\"list-group\" name=\"" + dr[i]["sub_title"].ToString() + "\">\n");
                    if (dr[i]["name"].ToString() != "sys_contents")
                    {
                        context.Response.Write("<h2>" + dr[i]["title"].ToString() + "<i></i></h2>\n");
                    }
                    //调用自身迭代
                    this.get_navigation_childs(context, oldData, int.Parse(dr[i]["id"].ToString()), dr[i]["name"].ToString(), role_type, ls);
                    context.Response.Write("</div>\n");
                }
                else if (int.Parse(dr[i]["class_layer"].ToString()) == 2 && parent_name == "sys_contents")
                {
                    context.Response.Write("<h2>" + dr[i]["title"].ToString() + "<i></i></h2>\n");
                    //调用自身迭代
                    this.get_navigation_childs(context, oldData, int.Parse(dr[i]["id"].ToString()), dr[i]["name"].ToString(), role_type, ls);
                }
                else
                {
                    context.Response.Write("<li>\n");
                    context.Response.Write("<a navid=\"" + dr[i]["name"].ToString() + "\"");
                    if (!string.IsNullOrEmpty(dr[i]["link_url"].ToString()))
                    {
                        if (int.Parse(dr[i]["channel_id"].ToString()) > 0)
                        {
                            context.Response.Write(" href=\"" + dr[i]["link_url"].ToString() + "?channel_id=" + dr[i]["channel_id"].ToString() + "\" target=\"mainframe\"");
                        }
                        else
                        {
                            context.Response.Write(" href=\"" + dr[i]["link_url"].ToString() + "\" target=\"mainframe\"");
                        }
                    }
                    context.Response.Write(" class=\"item\">\n");
                    context.Response.Write("<span>" + dr[i]["title"].ToString() + "</span>\n");
                    context.Response.Write("</a>\n");
                    //调用自身迭代
                    this.get_navigation_childs(context, oldData, int.Parse(dr[i]["id"].ToString()), dr[i]["name"].ToString(), role_type, ls);
                    context.Response.Write("</li>\n");
                }
                //以上是输出选项内容=======================
                //输出结束标记
                if (i == (dr.Length - 1) && parent_id > 0 && parent_name != "sys_contents")
                {
                    context.Response.Write("</ul>\n");
                }
            }
        }
        #endregion

        #region 修改订单信息和状态==============================
        private void edit_order_status(HttpContext context)
        {
            //取得管理员登录信息
            Model.manager adminInfo = new Web.UI.ManagePage().GetAdminInfo();
            if (adminInfo == null)
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"未登录或已超时，请重新登录！\"}");
                return;
            }
            //取得站点配置信息
            Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig();
            //取得订单配置信息
            Model.orderconfig orderConfig = new BLL.orderconfig().loadConfig();

            string order_no = MXRequest.GetString("order_no");
            string edit_type = MXRequest.GetString("edit_type");
            if (order_no == "")
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"传输参数有误，无法获取订单号！\"}");
                return;
            }
            if (edit_type == "")
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"无法获取修改订单类型！\"}");
                return;
            }

            BLL.orders bll = new BLL.orders();
            Model.orders model = bll.GetModel(order_no);
            if (model == null)
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"订单号不存在或已被删除！\"}");
                return;
            }
            switch (edit_type.ToLower())
            {
                case "order_confirm": //确认订单
                    //检查权限
                    if (!new BLL.manager_role().Exists(adminInfo.role_id, "order_list", MXEnums.ActionEnum.Confirm.ToString()))
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"您没有确认订单的权限！\"}");
                        return;
                    }
                    if (model.status > 1)
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"订单已经确认，不能重复处理！\"}");
                        return;
                    }
                    model.status = 2;
                    model.confirm_time = DateTime.Now;
                    if (!bll.Update(model))
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"订单确认失败！\"}");
                        return;
                    }
                    new BLL.manager_log().Add(adminInfo.id, adminInfo.user_name, MXEnums.ActionEnum.Confirm.ToString(), "确认订单号:" + model.order_no); //记录日志
                    #region 发送短信或邮件
                    if (orderConfig.confirmmsg > 0)
                    {
                        switch (orderConfig.confirmmsg)
                        {
                            case 1: //短信通知
                                if (string.IsNullOrEmpty(model.mobile))
                                {
                                    context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送短信<br/ >对方未填写手机号码！\"}");
                                    return;
                                }
                                Model.sms_template smsModel = new BLL.sms_template().GetModel(orderConfig.confirmcallindex); //取得短信内容
                                if (smsModel == null)
                                {
                                    context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送短信<br/ >短信通知模板不存在！\"}");
                                    return;
                                }
                                //替换标签
                                string msgContent = smsModel.content;
                                msgContent = msgContent.Replace("{webname}", siteConfig.webname);
                                msgContent = msgContent.Replace("{username}", model.user_name);
                                msgContent = msgContent.Replace("{orderno}", model.order_no);
                                msgContent = msgContent.Replace("{amount}", model.order_amount.ToString());
                                //发送短信
                                string tipMsg = string.Empty;
                                bool sendStatus = new BLL.sms_message().Send(model.mobile, msgContent, 2, out tipMsg);
                                if (!sendStatus)
                                {
                                    context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送短信<br/ >" + tipMsg + "\"}");
                                    return;
                                }
                                break;
                            case 2: //邮件通知
                                //取得用户的邮箱地址
                                if (model.user_id > 0)
                                {
                                    Model.users userModel = new BLL.users().GetModel(model.user_id);
                                    if (userModel == null || string.IsNullOrEmpty(userModel.email))
                                    {
                                        context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送邮件<br/ >该用户不存在或没有填写邮箱地址。\"}");
                                        return;
                                    }
                                    //取得邮件模板内容
                                    Model.mail_template mailModel = new BLL.mail_template().GetModel(orderConfig.confirmcallindex);
                                    if (mailModel == null)
                                    {
                                        context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送邮件<br/ >邮件通知模板不存在。\"}");
                                        return;
                                    }
                                    //替换标签
                                    string mailTitle = mailModel.maill_title;
                                    mailTitle = mailTitle.Replace("{username}", model.user_name);
                                    string mailContent = mailModel.content;
                                    mailContent = mailContent.Replace("{webname}", siteConfig.webname);
                                    mailContent = mailContent.Replace("{weburl}", siteConfig.weburl);
                                    mailContent = mailContent.Replace("{webtel}", siteConfig.webtel);
                                    mailContent = mailContent.Replace("{username}", model.user_name);
                                    mailContent = mailContent.Replace("{orderno}", model.order_no);
                                    mailContent = mailContent.Replace("{amount}", model.order_amount.ToString());
                                    //发送邮件
                                    MXMail.sendMail(siteConfig.emailsmtp, siteConfig.emailusername, siteConfig.emailpassword, siteConfig.emailnickname,
                                        siteConfig.emailfrom, userModel.email, mailTitle, mailContent);
                                }
                                break;
                        }
                    }
                    #endregion
                    context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功！\"}");
                    break;
                case "order_payment": //确认付款
                    //检查权限
                    if (!new BLL.manager_role().Exists(adminInfo.role_id, "order_list", MXEnums.ActionEnum.Confirm.ToString()))
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"您没有确认付款的权限！\"}");
                        return;
                    }
                    if (model.status > 1 || model.payment_status == 2)
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"订单已确认，不能重复处理！\"}");
                        return;
                    }
                    model.payment_status = 2;
                    model.payment_time = DateTime.Now;
                    model.status = 2;
                    model.confirm_time = DateTime.Now;
                    if (!bll.Update(model))
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"订单确认付款失败！\"}");
                        return;
                    }
                    new BLL.manager_log().Add(adminInfo.id, adminInfo.user_name, MXEnums.ActionEnum.Confirm.ToString(), "确认付款订单号:" + model.order_no); //记录日志
                    #region 发送短信或邮件
                    if (orderConfig.confirmmsg > 0)
                    {
                        switch (orderConfig.confirmmsg)
                        {
                            case 1: //短信通知
                                if (string.IsNullOrEmpty(model.mobile))
                                {
                                    context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送短信<br/ >对方未填写手机号码！\"}");
                                    return;
                                }
                                Model.sms_template smsModel = new BLL.sms_template().GetModel(orderConfig.confirmcallindex); //取得短信内容
                                if (smsModel == null)
                                {
                                    context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送短信<br/ >短信通知模板不存在！\"}");
                                    return;
                                }
                                //替换标签
                                string msgContent = smsModel.content;
                                msgContent = msgContent.Replace("{webname}", siteConfig.webname);
                                msgContent = msgContent.Replace("{username}", model.user_name);
                                msgContent = msgContent.Replace("{orderno}", model.order_no);
                                msgContent = msgContent.Replace("{amount}", model.order_amount.ToString());
                                //发送短信
                                string tipMsg = string.Empty;
                                bool sendStatus = new BLL.sms_message().Send(model.mobile, msgContent, 2, out tipMsg);
                                if (!sendStatus)
                                {
                                    context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送短信<br/ >" + tipMsg + "\"}");
                                    return;
                                }
                                break;
                            case 2: //邮件通知
                                //取得用户的邮箱地址
                                if (model.user_id > 0)
                                {
                                    Model.users userModel = new BLL.users().GetModel(model.user_id);
                                    if (userModel == null || string.IsNullOrEmpty(userModel.email))
                                    {
                                        context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送邮件<br/ >该用户不存在或没有填写邮箱地址。\"}");
                                        return;
                                    }
                                    //取得邮件模板内容
                                    Model.mail_template mailModel = new BLL.mail_template().GetModel(orderConfig.confirmcallindex);
                                    if (mailModel == null)
                                    {
                                        context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送邮件<br/ >邮件通知模板不存在。\"}");
                                        return;
                                    }
                                    //替换标签
                                    string mailTitle = mailModel.maill_title;
                                    mailTitle = mailTitle.Replace("{username}", model.user_name);
                                    string mailContent = mailModel.content;
                                    mailContent = mailContent.Replace("{webname}", siteConfig.webname);
                                    mailContent = mailContent.Replace("{weburl}", siteConfig.weburl);
                                    mailContent = mailContent.Replace("{webtel}", siteConfig.webtel);
                                    mailContent = mailContent.Replace("{username}", model.user_name);
                                    mailContent = mailContent.Replace("{orderno}", model.order_no);
                                    mailContent = mailContent.Replace("{amount}", model.order_amount.ToString());
                                    //发送邮件
                                    MXMail.sendMail(siteConfig.emailsmtp, siteConfig.emailusername, siteConfig.emailpassword, siteConfig.emailnickname,
                                        siteConfig.emailfrom, userModel.email, mailTitle, mailContent);
                                }
                                break;
                        }
                    }
                    #endregion
                    context.Response.Write("{\"status\": 1, \"msg\": \"订单确认付款成功！\"}");
                    break;
                case "order_express": //确认发货
                    //检查权限
                    if (!new BLL.manager_role().Exists(adminInfo.role_id, "order_list", MXEnums.ActionEnum.Confirm.ToString()))
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"您没有确认发货的权限！\"}");
                        return;
                    }
                    if (model.status > 2 || model.express_status == 2)
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"订单已完成或已发货，不能重复处理！\"}");
                        return;
                    }
                    int express_id = MXRequest.GetFormInt("express_id");
                    string express_no = MXRequest.GetFormString("express_no");
                    if (express_id == 0)
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"请选择配送方式！\"}");
                        return;
                    }
                    model.express_id = express_id;
                    model.express_no = express_no;
                  
                    model.express_time = DateTime.Now;
                    string ret = "";
                    if (model.payment_id == 3 && model.express_status != 2)
                    { 
                        //通知微信支付，改变发货状态
                        //FaHuoProc fahuo = new FaHuoProc();

                        //BLL.wx_payment_wxpay payBll = new BLL.wx_payment_wxpay();
                        //Model.wx_payment_wxpay paymentInfo = payBll.GetModelByWid(model.wid.Value);
                        //Dictionary<string, object> fahuoDict = fahuo.fahuomgr(paymentInfo, model);
                        //string errcode = fahuoDict["errcode"].ToString();
                        //string errmsg = fahuoDict["errmsg"].ToString();
                        //model.fahuoCode = errcode;
                        //model.fahuoMsg = errmsg;
                        //if (errcode == "0")
                        //{
                        //    model.express_status = 2;
                        //}
                        //else
                        //{
                        //    ret = "通知微信支付，改变发货状态失败:"+errmsg;
                        //}
                    }
                    model.express_status = 2;
                    if (!bll.Update(model))
                    {
                        ret += "订单发货失败！";
                        context.Response.Write("{\"status\": 0, \"msg\": \"" + ret + "\"}");
                        return;
                    }
                    new BLL.manager_log().Add(adminInfo.id, adminInfo.user_name, MXEnums.ActionEnum.Confirm.ToString(), "确认发货订单号:" + model.order_no); //记录日志
                    #region 发送短信或邮件
                    if (orderConfig.expressmsg > 0)
                    {
                        switch (orderConfig.expressmsg)
                        {
                            case 1: //短信通知
                                if (string.IsNullOrEmpty(model.mobile))
                                {
                                    context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送短信<br/ >对方未填写手机号码！\"}");
                                    return;
                                }
                                Model.sms_template smsModel = new BLL.sms_template().GetModel(orderConfig.expresscallindex); //取得短信内容
                                if (smsModel == null)
                                {
                                    context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送短信<br/ >短信通知模板不存在！\"}");
                                    return;
                                }
                                //替换标签
                                string msgContent = smsModel.content;
                                msgContent = msgContent.Replace("{webname}", siteConfig.webname);
                                msgContent = msgContent.Replace("{username}", model.user_name);
                                msgContent = msgContent.Replace("{orderno}", model.order_no);
                                msgContent = msgContent.Replace("{amount}", model.order_amount.ToString());
                                //发送短信
                                string tipMsg = string.Empty;
                                bool sendStatus = new BLL.sms_message().Send(model.mobile, msgContent, 2, out tipMsg);
                                if (!sendStatus)
                                {
                                    context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送短信<br/ >" + tipMsg + "\"}");
                                    return;
                                }
                                break;
                            case 2: //邮件通知
                                //取得用户的邮箱地址
                                if (model.user_id > 0)
                                {
                                    Model.users userModel = new BLL.users().GetModel(model.user_id);
                                    if (userModel == null || string.IsNullOrEmpty(userModel.email))
                                    {
                                        context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送邮件<br/ >该用户不存在或没有填写邮箱地址。\"}");
                                        return;
                                    }
                                    //取得邮件模板内容
                                    Model.mail_template mailModel = new BLL.mail_template().GetModel(orderConfig.expresscallindex);
                                    if (mailModel == null)
                                    {
                                        context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送邮件<br/ >邮件通知模板不存在。\"}");
                                        return;
                                    }
                                    //替换标签
                                    string mailTitle = mailModel.maill_title;
                                    mailTitle = mailTitle.Replace("{username}", model.user_name);
                                    string mailContent = mailModel.content;
                                    mailContent = mailContent.Replace("{webname}", siteConfig.webname);
                                    mailContent = mailContent.Replace("{weburl}", siteConfig.weburl);
                                    mailContent = mailContent.Replace("{webtel}", siteConfig.webtel);
                                    mailContent = mailContent.Replace("{username}", model.user_name);
                                    mailContent = mailContent.Replace("{orderno}", model.order_no);
                                    mailContent = mailContent.Replace("{amount}", model.order_amount.ToString());
                                    //发送邮件
                                    MXMail.sendMail(siteConfig.emailsmtp, siteConfig.emailusername, siteConfig.emailpassword, siteConfig.emailnickname,
                                        siteConfig.emailfrom, userModel.email, mailTitle, mailContent);
                                }
                                break;
                        }
                    }
                    #endregion
                    context.Response.Write("{\"status\": 1, \"msg\": \"订单发货成功！\"}");
                    break;
                case "order_complete": //完成订单=========================================
                    //检查权限
                    if (!new BLL.manager_role().Exists(adminInfo.role_id, "order_list", MXEnums.ActionEnum.Confirm.ToString()))
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"您没有确认完成订单的权限！\"}");
                        return;
                    }
                    if (model.status > 2)
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"订单已经完成，不能重复处理！\"}");
                        return;
                    }
                    model.status = 3;
                    model.complete_time = DateTime.Now;
                    if (!bll.Update(model))
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"确认订单完成失败！\"}");
                        return;
                    }
                    //给会员增加积分检查升级
                    if (model.user_id > 0 && model.point > 0)
                    {
                        new BLL.user_point_log().Add(model.user_id, model.user_name, model.point, "购物获得积分，订单号：" + model.order_no, true);
                    }
                    new BLL.manager_log().Add(adminInfo.id, adminInfo.user_name, MXEnums.ActionEnum.Confirm.ToString(), "确认交易完成订单号:" + model.order_no); //记录日志
                    #region 发送短信或邮件
                    if (orderConfig.completemsg > 0)
                    {
                        switch (orderConfig.completemsg)
                        {
                            case 1: //短信通知
                                if (string.IsNullOrEmpty(model.mobile))
                                {
                                    context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送短信<br/ >对方未填写手机号码！\"}");
                                    return;
                                }
                                Model.sms_template smsModel = new BLL.sms_template().GetModel(orderConfig.completecallindex); //取得短信内容
                                if (smsModel == null)
                                {
                                    context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送短信<br/ >短信通知模板不存在！\"}");
                                    return;
                                }
                                //替换标签
                                string msgContent = smsModel.content;
                                msgContent = msgContent.Replace("{webname}", siteConfig.webname);
                                msgContent = msgContent.Replace("{username}", model.user_name);
                                msgContent = msgContent.Replace("{orderno}", model.order_no);
                                msgContent = msgContent.Replace("{amount}", model.order_amount.ToString());
                                //发送短信
                                string tipMsg = string.Empty;
                                bool sendStatus = new BLL.sms_message().Send(model.mobile, msgContent, 2, out tipMsg);
                                if (!sendStatus)
                                {
                                    context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送短信<br/ >" + tipMsg + "\"}");
                                    return;
                                }
                                break;
                            case 2: //邮件通知
                                //取得用户的邮箱地址
                                if (model.user_id > 0)
                                {
                                    Model.users userModel = new BLL.users().GetModel(model.user_id);
                                    if (userModel == null || string.IsNullOrEmpty(userModel.email))
                                    {
                                        context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送邮件<br/ >该用户不存在或没有填写邮箱地址。\"}");
                                        return;
                                    }
                                    //取得邮件模板内容
                                    Model.mail_template mailModel = new BLL.mail_template().GetModel(orderConfig.completecallindex);
                                    if (mailModel == null)
                                    {
                                        context.Response.Write("{\"status\": 1, \"msg\": \"订单确认成功，但无法发送邮件<br/ >邮件通知模板不存在。\"}");
                                        return;
                                    }
                                    //替换标签
                                    string mailTitle = mailModel.maill_title;
                                    mailTitle = mailTitle.Replace("{username}", model.user_name);
                                    string mailContent = mailModel.content;
                                    mailContent = mailContent.Replace("{webname}", siteConfig.webname);
                                    mailContent = mailContent.Replace("{weburl}", siteConfig.weburl);
                                    mailContent = mailContent.Replace("{webtel}", siteConfig.webtel);
                                    mailContent = mailContent.Replace("{username}", model.user_name);
                                    mailContent = mailContent.Replace("{orderno}", model.order_no);
                                    mailContent = mailContent.Replace("{amount}", model.order_amount.ToString());
                                    //发送邮件
                                    MXMail.sendMail(siteConfig.emailsmtp, siteConfig.emailusername, siteConfig.emailpassword, siteConfig.emailnickname,
                                        siteConfig.emailfrom, userModel.email, mailTitle, mailContent);
                                }
                                break;
                        }
                    }
                    #endregion
                    context.Response.Write("{\"status\": 1, \"msg\": \"确认订单完成成功！\"}");
                    break;
                case "order_cancel": //取消订单==========================================
                    //检查权限
                    if (!new BLL.manager_role().Exists(adminInfo.role_id, "order_list", MXEnums.ActionEnum.Cancel.ToString()))
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"您没有取消订单的权限！\"}");
                        return;
                    }
                    if (model.status > 2)
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"订单已经完成，不能取消订单！\"}");
                        return;
                    }
                    model.status = 4;
                    if (!bll.Update(model))
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"取消订单失败！\"}");
                        return;
                    }
                    int check_revert1 = MXRequest.GetFormInt("check_revert");
                    if (check_revert1 == 1)
                    {
                        //如果存在积分换购则返还会员积分
                        if (model.user_id > 0 && model.point < 0)
                        {
                            new BLL.user_point_log().Add(model.user_id, model.user_name, (model.point * -1), "取消订单返还积分，订单号：" + model.order_no, false);
                        }
                        //如果已支付则退还金额到会员账户
                        if (model.user_id > 0 && model.payment_status == 2 && model.order_amount > 0)
                        {
                            new BLL.user_amount_log().Add(model.user_id, model.user_name, MXEnums.AmountTypeEnum.BuyGoods.ToString(), model.order_amount, "取消订单退还金额，订单号：" + model.order_no);
                        }
                    }
                    new BLL.manager_log().Add(adminInfo.id, adminInfo.user_name, MXEnums.ActionEnum.Cancel.ToString(), "取消订单号:" + model.order_no); //记录日志
                    context.Response.Write("{\"status\": 1, \"msg\": \"取消订单成功！\"}");
                    break;
                case "order_invalid": //作废订单==========================================
                    //检查权限
                    if (!new BLL.manager_role().Exists(adminInfo.role_id, "order_list", MXEnums.ActionEnum.Invalid.ToString()))
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"您没有作废订单的权限！\"}");
                        return;
                    }
                    if (model.status != 3)
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"订单尚未完成，不能作废订单！\"}");
                        return;
                    }
                    model.status = 5;
                    if (!bll.Update(model))
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"作废订单失败！\"}");
                        return;
                    }
                    int check_revert2 = MXRequest.GetFormInt("check_revert");
                    if (check_revert2 == 1)
                    {
                        //扣除购物赠送的积分
                        if (model.user_id > 0 && model.point > 0)
                        {
                            new BLL.user_point_log().Add(model.user_id, model.user_name, (model.point * -1), "作废订单扣除积分，订单号：" + model.order_no, false);
                        }
                        //退还金额到会员账户
                        if (model.user_id > 0 && model.order_amount > 0)
                        {
                            new BLL.user_amount_log().Add(model.user_id, model.user_name, MXEnums.AmountTypeEnum.BuyGoods.ToString(), model.order_amount, "取消订单退还金额，订单号：" + model.order_no);
                        }
                    }
                    new BLL.manager_log().Add(adminInfo.id, adminInfo.user_name, MXEnums.ActionEnum.Invalid.ToString(), "作废订单号:" + model.order_no); //记录日志
                    context.Response.Write("{\"status\": 1, \"msg\": \"作废订单成功！\"}");
                    break;
                case "edit_accept_info": //修改收货信息====================================
                    //检查权限
                    if (!new BLL.manager_role().Exists(adminInfo.role_id, "order_list", MXEnums.ActionEnum.Edit.ToString()))
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"您没有修改收货信息的权限！\"}");
                        return;
                    }
                    if (model.express_status == 2)
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"订单已经发货，不能修改收货信息！\"}");
                        return;
                    }
                    string accept_name = MXRequest.GetFormString("accept_name");
                    string province = MXRequest.GetFormString("province");
                    string city = MXRequest.GetFormString("city");
                    string area = MXRequest.GetFormString("area");
                    string address = MXRequest.GetFormString("address");
                    string post_code = MXRequest.GetFormString("post_code");
                    string mobile = MXRequest.GetFormString("mobile");
                    string telphone = MXRequest.GetFormString("telphone");

                    if (accept_name == "")
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"请填写收货人姓名！\"}");
                        return;
                    }
                    if (area == "")
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"请选择所在地区！\"}");
                        return;
                    }
                    if (address == "")
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"请填写详细的送货地址！\"}");
                        return;
                    }
                    if (mobile == "" && telphone == "")
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"联系手机或电话至少填写一项！\"}");
                        return;
                    }

                    model.accept_name = accept_name;
                    model.area = province + "," + city + "," + area;
                    model.address = address;
                    model.post_code = post_code;
                    model.mobile = mobile;
                    model.telphone = telphone;
                    if (!bll.Update(model))
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"修改收货人信息失败！\"}");
                        return;
                    }
                    new BLL.manager_log().Add(adminInfo.id, adminInfo.user_name, MXEnums.ActionEnum.Edit.ToString(), "修改收货信息，订单号:" + model.order_no); //记录日志
                    context.Response.Write("{\"status\": 1, \"msg\": \"修改收货人信息成功！\"}");
                    break;
                case "edit_order_remark": //修改订单备注=================================
                    string remark = MXRequest.GetFormString("remark");
                    if (remark == "")
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"请填写订单备注内容！\"}");
                        return;
                    }
                    model.remark = remark;
                    if (!bll.Update(model))
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"修改订单备注失败！\"}");
                        return;
                    }
                    new BLL.manager_log().Add(adminInfo.id, adminInfo.user_name, MXEnums.ActionEnum.Edit.ToString(), "修改订单备注，订单号:" + model.order_no); //记录日志
                    context.Response.Write("{\"status\": 1, \"msg\": \"修改订单备注成功！\"}");
                    break;
                case "edit_real_amount": //修改商品总金额================================
                    //检查权限
                    if (!new BLL.manager_role().Exists(adminInfo.role_id, "order_list", MXEnums.ActionEnum.Edit.ToString()))
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"您没有修改商品金额的权限！\"}");
                        return;
                    }
                    if (model.status > 1)
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"订单已经确认，不能修改金额！\"}");
                        return;
                    }
                    decimal real_amount = MXRequest.GetFormDecimal("real_amount", 0);
                    model.real_amount = real_amount;
                    if (!bll.Update(model))
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"修改商品总金额失败！\"}");
                        return;
                    }
                    new BLL.manager_log().Add(adminInfo.id, adminInfo.user_name, MXEnums.ActionEnum.Edit.ToString(), "修改商品金额，订单号:" + model.order_no); //记录日志
                    context.Response.Write("{\"status\": 1, \"msg\": \"修改商品总金额成功！\"}");
                    break;
                case "edit_express_fee": //修改配送费用==================================
                    //检查权限
                    if (!new BLL.manager_role().Exists(adminInfo.role_id, "order_list", MXEnums.ActionEnum.Edit.ToString()))
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"您没有配送费用的权限！\"}");
                        return;
                    }
                    if (model.status > 1)
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"订单已经确认，不能修改金额！\"}");
                        return;
                    }
                    decimal express_fee = MXRequest.GetFormDecimal("express_fee", 0);
                    model.express_fee = express_fee;
                    if (!bll.Update(model))
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"修改配送费用失败！\"}");
                        return;
                    }
                    new BLL.manager_log().Add(adminInfo.id, adminInfo.user_name, MXEnums.ActionEnum.Edit.ToString(), "修改配送费用，订单号:" + model.order_no); //记录日志
                    context.Response.Write("{\"status\": 1, \"msg\": \"修改配送费用成功！\"}");
                    break;
                case "edit_payment_fee": //修改支付手续费=================================
                    //检查权限
                    if (!new BLL.manager_role().Exists(adminInfo.role_id, "order_list", MXEnums.ActionEnum.Edit.ToString()))
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"您没有修改支付手续费的权限！\"}");
                        return;
                    }
                    if (model.status > 1)
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"订单已经确认，不能修改金额！\"}");
                        return;
                    }
                    decimal payment_fee = MXRequest.GetFormDecimal("payment_fee", 0);
                    model.payment_fee = payment_fee;
                    if (!bll.Update(model))
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"修改支付手续费失败！\"}");
                        return;
                    }
                    new BLL.manager_log().Add(adminInfo.id, adminInfo.user_name, MXEnums.ActionEnum.Edit.ToString(), "修改支付手续费，订单号:" + model.order_no); //记录日志
                    context.Response.Write("{\"status\": 1, \"msg\": \"修改支付手续费成功！\"}");
                    break;
            }

        }
        #endregion

        #region 验证用户名是否可用==============================
        private void validate_username(HttpContext context)
        {
            string user_name = MXRequest.GetString("param");
            //如果为Null，退出
            if (string.IsNullOrEmpty(user_name))
            {
                context.Response.Write("{ \"info\":\"请输入用户名\", \"status\":\"n\" }");
                return;
            }
            Model.userconfig userConfig = new BLL.userconfig().loadConfig();
            //过滤注册用户名字符
            string[] strArray = userConfig.regkeywords.Split(',');
            foreach (string s in strArray)
            {
                if (s.ToLower() == user_name.ToLower())
                {
                    context.Response.Write("{ \"info\":\"用户名不可用\", \"status\":\"n\" }");
                    return;
                }
            }
            BLL.users bll = new BLL.users();
            //查询数据库
            if (bll.Exists(user_name.Trim()))
            {
                context.Response.Write("{ \"info\":\"用户名已重复\", \"status\":\"n\" }");
                return;
            }
            context.Response.Write("{ \"info\":\"用户名可用\", \"status\":\"y\" }");
            return;
        }
        #endregion

        #region 发送手机短信====================================
        private void sms_message_post(HttpContext context)
        {
            //检查管理员是否登录
            if (!new ManagePage().IsAdminLogin())
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"尚未登录或已超时，请登录后操作！\"}");
                return;
            }
            string mobiles = MXRequest.GetFormString("mobiles");
            string content = MXRequest.GetFormString("content");
            if (mobiles == "")
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"手机号码不能为空！\"}");
                return;
            }
            if (content == "")
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"短信内容不能为空！\"}");
                return;
            }
            //开始发送
            string msg = string.Empty;
            bool result = new BLL.sms_message().Send(mobiles, content, 2, out msg);
            if (result)
            {
                context.Response.Write("{\"status\": 1, \"msg\": \"" + msg + "\"}");
                return;
            }
            context.Response.Write("{\"status\": 0, \"msg\": \"" + msg + "\"}");
            return;
        }
        #endregion

        #region 获取要生成静态的地址============================
        private void get_builder_urls(HttpContext context)
        {
            int state = GetIsLoginAndIsStaticstatus();
            if (state == 1)
                new HtmlBuilder().getpublishsite(context);
            else
                context.Response.Write(state);
        }
        #endregion

        #region 生成静态页面====================================
        private void get_builder_html(HttpContext context)
        {
            int state = GetIsLoginAndIsStaticstatus();
            if (state == 1)
                new HtmlBuilder().handleHtml(context);
            else
                context.Response.Write(state);


        }
        #endregion

        #region 判断是否登陆以及是否开启静态====================
        private int GetIsLoginAndIsStaticstatus()
        {
            Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig();
            //取得管理员登录信息
            Model.manager adminInfo = new Web.UI.ManagePage().GetAdminInfo();
            if (adminInfo == null)
                return -1;
            else if (!new BLL.manager_role().Exists(adminInfo.role_id, "app_builder_html", MXEnums.ActionEnum.Build.ToString()))
                return -2;
            else if (siteConfig.staticstatus != 2)
                return -3;
            else
                return 1;
        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="attribute_edit.aspx.cs" Inherits="Lanwei.Weixin.Web.shopmgr.catalog.attribute_edit" %>


<%@ Import Namespace="Lanwei.Weixin.Common" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>编辑商品类型</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../../scripts/datepicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload/swfupload.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload/swfupload.queue.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload/swfupload.handlers.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../editor/kindeditor-min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../editor/lang/zh_CN.js"></script>
    <script type="text/javascript" src="../../admin/js/layout.js"></script>
    <link href="../../admin/skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../../admin/skin/mystyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            //初始化表单验证
            $("#form1").initValidform();

            //初始化上传控件
            $(".upload-img").each(function () {
                $(this).InitSWFUpload({ sendurl: "../../tools/upload_ajax.ashx", flashurl: "../../scripts/swfupload/swfupload.swf" });
            });
            $(".upload-album").each(function () {
                $(this).InitSWFUpload({ btntext: "批量上传", btnwidth: 66, single: false, water: true, thumbnail: true, filesize: "2048", sendurl: "../../tools/upload_ajax.ashx", flashurl: "../../scripts/swfupload/swfupload.swf", filetypes: "*.jpg;*.jpge;*.png;*.gif;" });
            });
            $(".attach-btn").click(function () {
                showAttachDialog();
            });


            $("input[name='radType']").click(function () {

                if ($(this).val() == "3") {
                    //文本
                 
                    $("#shuxingValue").hide();
                   
                }
                else  {
                    //语音
                 
                    $("#shuxingValue").show();
                   
                }

            });



        });
    </script>
</head>

<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="catalog_list.aspx" class="back"><i></i><span>返回商品类型列表</span></a>
            <a href="attribute_list.aspx?id=<%=catalogId %>" class="home"><i></i><span>商品属性管理</span></a>
            <i class="arrow"></i>
            <span>编辑商品属性</span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->

        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">编辑商品属性</a></li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="tab-content">
            <dl>
                <dt>商品属性名称</dt>
                <dd>
                    <asp:HiddenField ID="hidid" runat="server" Value="0" />
                    <asp:TextBox ID="txtaName" runat="server" CssClass="input normal" datatype="*1-50" sucmsg=" " Text="" />
                    <span class="Validform_checktip">*扩展属性的名称，长度在1至25个字符之间.</span>
                </dd>
            </dl>

            <dl>
                <dt>所属商品类型</dt>
                <dd>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlCatalog" runat="server"></asp:DropDownList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>属性的使用方式</dt>
                <dd>

                    <div class="rule-multi-radio">
                        <asp:RadioButtonList ID="radType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Text="供客户查看" Value="1"></asp:ListItem>
                            <asp:ListItem Text="客户可选规格" Value="2" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="供客户填写" Value="3"></asp:ListItem>

                        </asp:RadioButtonList>
                    </div>
                </dd>
            </dl>


            <dl id="shuxingValue">
                <dt>属性值</dt>
                <dd>
                    <table>
                        <tr>
                            <td style="width:350px;">
                                <textarea name="txtaValue" rows="5" cols="20" id="txtaValue" style="height: 100px;" class="input" runat="server" datatype="*0-500" sucmsg=" " nullmsg=" "></textarea>
                                <span class="Validform_checktip"></span>
                            </td>
                            <td style="padding-right:20px;">

                                <span>请预设属性值(一行代表一个属性值); 管理员在这里可预设多个属性值，以便于添加商品时从中选择一个。前台客户购物时,仅可以查看商品此项属性。 如为空，则可在添加商品时由管理员手工录入。</span>

                            </td>
                        </tr>
                    </table>

                </dd>
            </dl>



            <dl>
                <dt>排序</dt>
                <dd>
                    <asp:TextBox ID="txtsort_id" runat="server" CssClass="input small" datatype="n" sucmsg=" ">99</asp:TextBox>
                    <span class="Validform_checktip">*数字，越小越向前</span>
                </dd>
            </dl>




        </div>



        <!--/内容-->

        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
                <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" OnClick="btnSubmit_Click" />
                <a href="attribute_list.aspx"><span class="btn yellow">返回上一页</span></a>

            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->
    </form>
</body>
</html>

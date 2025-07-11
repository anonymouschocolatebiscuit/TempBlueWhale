﻿using BlueWhale.DAL;
using BlueWhale.Model;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.SessionState;//用Session必须引用

namespace BlueWhaleUI.pay.ashx
{
    /// <summary>
    /// ReceivableListAdd 的摘要说明
    /// </summary>
    public class ReceivableListAdd : IHttpHandler, IRequiresSessionState  //用Session必须引用IRequiresSessionState
    {

        public ReceivableDAL dal = new ReceivableDAL();

        public class OrderListModel<T>
        {
            #region 表头字段


            private int _wlId;
            public int wlId
            {
                get { return _wlId; }
                set { _wlId = value; }
            }

            private DateTime _bizDate;
            public DateTime bizDate
            {
                get { return _bizDate; }
                set { _bizDate = value; }
            }

            private decimal _disPrice;
            public decimal disPrice
            {
                get { return _disPrice; }
                set { _disPrice = value; }
            }

            private decimal _payPriceNowMore;
            public decimal payPriceNowMore
            {
                get { return _payPriceNowMore; }
                set { _payPriceNowMore = value; }
            }

            private string _remarks;
            public string remarks
            {
                get { return _remarks; }
                set { _remarks = value; }
            }



            #endregion

            /// <summary>
            /// 付款账户
            /// </summary>
            public List<OrderListItemModel> _Rows;
            public List<OrderListItemModel> Rows
            {
                get { return _Rows; }
                set { _Rows = value; }
            }

            /// <summary>
            /// 核销明细
            /// </summary>
            public List<OrderListItemModelBill> _RowsBill;
            public List<OrderListItemModelBill> RowsBill
            {
                get { return _RowsBill; }
                set { _RowsBill = value; }
            }
        }

        [Serializable]
        public class OrderListItemModel
        {
            private int bkId;
            public int BkId
            {
                get { return bkId; }
                set { bkId = value; }
            }

            private int payTypeId;
            public int PayTypeId
            {
                get { return payTypeId; }
                set { payTypeId = value; }
            }

            private string payNumber;
            public string PayNumber
            {
                get { return payNumber; }
                set { payNumber = value; }
            }


            private decimal payPrice;
            public decimal PayPrice
            {
                get { return payPrice; }
                set { payPrice = value; }
            }


            private string remarks;
            public string Remarks
            {
                get { return remarks; }
                set { remarks = value; }
            }
        }

        [Serializable]
        public class OrderListItemModelBill
        {
            private string sourceNumber;
            public string SourceNumber
            {
                get { return sourceNumber; }
                set { sourceNumber = value; }
            }

            private decimal priceCheckNow;
            public decimal PriceCheckNow
            {
                get { return priceCheckNow; }
                set { priceCheckNow = value; }
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";//返回格式

            if (context.Session["userInfo"] == null)
            {

                context.Response.Write("登陆超时，请重新登陆系统！");
                return;

            }
            BasePage basePage = new BasePage();
            if (!basePage.CheckPower("ReceivableListAdd"))
            {
                context.Response.Write("无此操作权限，请联系管理员！");
                return;
            }
            Users users = context.Session["userInfo"] as Users;


            StreamReader reader = new StreamReader(context.Request.InputStream);
            string strJson = HttpUtility.UrlDecode(reader.ReadToEnd());

            OrderListModel<OrderListItemModel> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderListModel<OrderListItemModel>>(strJson);


            OrderListModel<OrderListItemModelBill> objBill = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderListModel<OrderListItemModelBill>>(strJson);


            OrderListModel<OrderListItemModel> payList = obj;

            OrderListModel<OrderListItemModelBill> billList = objBill;

            #region 主表赋值

            dal.ShopId = users.ShopId;
            dal.Number = dal.GetBillNumberAuto(users.ShopId);

            dal.BizDate = obj.bizDate;
            dal.WlId = obj.wlId;
            dal.DisPrice = obj.disPrice;
            dal.Remarks = obj.remarks.ToString();
            dal.MakeId = users.Id;
            dal.MakeDate = DateTime.Now;

            dal.PayPriceNowMore = obj.payPriceNowMore;

            dal.Flag = "保存";
            dal.OrderNumber = "";//为了获取订货系统付款信息处理，这里不需要数据。

            #endregion


            int pId = dal.Add();


            #region 字表赋值

            if (pId > 0)
            {

                int check = 0;

                ReceivableAccountItemDAL item = new ReceivableAccountItemDAL();
                ReceivableSourceBillItemDAL billItem = new ReceivableSourceBillItemDAL();

                #region 账户分录


                for (int i = 0; i < payList.Rows.Count; i++)
                {

                    item.PId = pId;
                    item.BkId = payList.Rows[i].BkId;
                    item.PayTypeId = payList.Rows[i].PayTypeId;
                    item.PayNumber = payList.Rows[i].PayNumber;
                    item.PayPrice = payList.Rows[i].PayPrice;
                    item.Remarks = payList.Rows[i].Remarks;
                    item.Flag = "保存";

                    check = item.Add();

                }

                #endregion

                #region 核销分录

                for (int i = 0; i < billList.RowsBill.Count; i++)
                {

                    billItem.PId = pId;

                    billItem.PriceCheckNow = billList.RowsBill[i].PriceCheckNow;

                    billItem.SourceNumber = billList.RowsBill[i].SourceNumber;

                    billItem.Flag = "保存";


                    check = billItem.Add();



                }

                #endregion

                if (check > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.Users = users.Names;
                    logs.Events = "新增收款单：" + dal.Number;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    context.Response.Write("Operation Successful!");


                }
            }
            #endregion

        }




        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
﻿using BlueWhale.DAL;
using BlueWhale.Model;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.SessionState; // Required for using Session

namespace BlueWhale.UI.sales.ashx
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class SalesOrderListEdit : IHttpHandler, IRequiresSessionState // Required for using Session
    {
        public SalesOrderDAL dal = new SalesOrderDAL();

        public class OrderListModel<T>
        {
            #region Header fields

            private int id;
            public int Id
            {
                get { return id; }
                set { id = value; }
            }

            private int _venderId;
            public int venderId
            {
                get { return _venderId; }
                set { _venderId = value; }
            }

            private DateTime _bizDate;
            public DateTime bizDate
            {
                get { return _bizDate; }
                set { _bizDate = value; }
            }

            private DateTime _sendDate;
            public DateTime sendDate
            {
                get { return _sendDate; }
                set { _sendDate = value; }
            }

            private int _bizId;
            public int bizId
            {
                get { return _bizId; }
                set { _bizId = value; }
            }

            private string _remarks;
            public string remarks
            {
                get { return _remarks; }
                set { _remarks = value; }
            }

            #endregion

            /// <summary>
            /// Item details
            /// </summary>
            public List<OrderListItemModel> _Rows;
            public List<OrderListItemModel> Rows
            {
                get { return _Rows; }
                set { _Rows = value; }
            }
        }

        [Serializable]
        public class OrderListItemModel
        {
            private int id;
            public int Id
            {
                get { return id; }
                set { id = value; }
            }

            private int goodsId;
            public int GoodsId
            {
                get { return goodsId; }
                set { goodsId = value; }
            }

            private decimal num;
            public decimal Num
            {
                get { return num; }
                set { num = value; }
            }

            private decimal price;
            public decimal Price
            {
                get { return price; }
                set { price = value; }
            }

            private decimal dis;
            public decimal Dis
            {
                get { return dis; }
                set { dis = value; }
            }

            /// <summary>
            /// Discount amount
            /// </summary>
            private decimal sumPriceDis;
            public decimal SumPriceDis
            {
                get { return sumPriceDis; }
                set { sumPriceDis = value; }
            }

            private decimal priceNow;
            public decimal PriceNow
            {
                get { return priceNow; }
                set { priceNow = value; }
            }

            private decimal sumPriceNow;
            public decimal SumPriceNow
            {
                get { return sumPriceNow; }
                set { sumPriceNow = value; }
            }

            private int tax;
            public int Tax
            {
                get { return tax; }
                set { tax = value; }
            }

            private decimal priceTax;
            public decimal PriceTax
            {
                get { return priceTax; }
                set { priceTax = value; }
            }

            private decimal sumPriceTax;
            public decimal SumPriceTax
            {
                get { return sumPriceTax; }
                set { sumPriceTax = value; }
            }

            private decimal sumPriceAll;
            public decimal SumPriceAll
            {
                get { return sumPriceAll; }
                set { sumPriceAll = value; }
            }

            private int ckId;
            public int CkId
            {
                get { return ckId; }
                set { ckId = value; }
            }

            private string remarks;
            public string Remarks
            {
                get { return remarks; }
                set { remarks = value; }
            }

            private int itemId;
            public int ItemId
            {
                get { return itemId; }
                set { itemId = value; }
            }

            private string sourceNumber;
            public string SourceNumber
            {
                get { return sourceNumber; }
                set { sourceNumber = value; }
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain"; // Response content type

            if (context.Session["userInfo"] == null)
            {
                context.Response.Write("Login timed out, please log in again!");
                return;
            }

            BasePage basePage = new BasePage();
            if (!basePage.CheckPower("PurOrderListAdd"))
            {
                context.Response.Write("You do not have permission for this operation. Please contact the administrator.");
                return;
            }

            Users users = context.Session["userInfo"] as Users;

            StreamReader reader = new StreamReader(context.Request.InputStream);
            string strJson = HttpUtility.UrlDecode(reader.ReadToEnd());

            OrderListModel<OrderListItemModel> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderListModel<OrderListItemModel>>(strJson);
            OrderListModel<OrderListItemModel> itemList = obj;

            #region Assign header values

            dal.Id = obj.Id;
            dal.ShopId = users.ShopId;
            dal.Number = ""; // dal.GetBillNumberAuto();
            dal.WlId = obj.venderId;
            dal.BizDate = obj.bizDate;
            dal.SendDate = obj.sendDate;
            dal.BizId = obj.bizId;
            dal.Remarks = obj.remarks;
            dal.MakeId = users.Id;
            dal.MakeDate = DateTime.Now;
            dal.Flag = "Save";

            #endregion

            int pId = dal.Update();

            if (pId > 0)
            {
                #region Assign item details

                SalesOrderItemDAL item = new SalesOrderItemDAL();
                item.Delete(obj.Id);

                int check = 0;

                for (int i = 0; i < itemList.Rows.Count; i++)
                {
                    item.PId = obj.Id;
                    item.GoodsId = itemList.Rows[i].GoodsId;
                    item.Num = itemList.Rows[i].Num;
                    item.CkId = itemList.Rows[i].CkId;
                    item.Price = itemList.Rows[i].Price;
                    item.Dis = itemList.Rows[i].Dis;
                    item.SumPriceDis = itemList.Rows[i].SumPriceDis;
                    item.PriceNow = itemList.Rows[i].PriceNow;
                    item.SumPriceNow = itemList.Rows[i].SumPriceNow;
                    item.Tax = itemList.Rows[i].Tax;
                    item.PriceTax = itemList.Rows[i].PriceTax;
                    item.SumPriceTax = itemList.Rows[i].SumPriceTax;
                    item.SumPriceAll = itemList.Rows[i].SumPriceAll;
                    item.Remarks = itemList.Rows[i].Remarks;
                    item.ItemId = itemList.Rows[i].ItemId;
                    item.SourceNumber = itemList.Rows[i].SourceNumber;

                    check = item.Add();
                }

                #endregion

                if (check > 0)
                {
                    LogsDAL logs = new LogsDAL
                    {
                        ShopId = users.ShopId,
                        Users = users.Names,
                        Events = "Modified sales order: " + obj.Id,
                        Ip = HttpContext.Current.Request.UserHostAddress
                    };
                    logs.Add();

                    context.Response.Write("Operation successful!");
                }
            }
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}

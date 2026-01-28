using BlueWhale.DAL;
using BlueWhale.Model;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;//Session must be referenced

namespace BlueWhale.UI.sales.ashx
{
    /// <summary>
    /// $codebehindclassname$ 的Summary
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class SalesReceiptListAdd : IHttpHandler, IRequiresSessionState  //When using Session, you must reference IRequiresSessionState
    {
        public SalesReceiptDAL dal = new SalesReceiptDAL();

        public GoodsDAL goodsDAL = new GoodsDAL();

        public class OrderListModel<T>
        {
            #region Header Fields

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

            /// <summary>
            /// Guarantee deposit %
            /// </summary>
            private decimal _dis;
            public decimal dis
            {
                get { return _dis; }
                set { _dis = value; }
            }

            /// <summary>
            /// Warranty amount
            /// </summary>
            private decimal _disPrice;
            public decimal disPrice
            {
                get { return _disPrice; }
                set { _disPrice = value; }
            }

            private decimal _payNow;
            public decimal payNow
            {
                get { return _payNow; }
                set { _payNow = value; }
            }

            private decimal _payNowNo;
            public decimal payNowNo
            {
                get { return _payNowNo; }
                set { _payNowNo = value; }
            }

            private int _bkId;
            public int bkId
            {
                get { return _bkId; }
                set { _bkId = value; }
            }

            private int sendId;
            public int SendId
            {
                get { return sendId; }
                set { sendId = value; }
            }

            private string sendNumber;
            public string SendNumber
            {
                get { return sendNumber; }
                set { sendNumber = value; }
            }

            private string sendPayType;
            public string SendPayType
            {
                get { return sendPayType; }
                set { sendPayType = value; }
            }

            private decimal sendPrice;
            public decimal SendPrice
            {
                get { return sendPrice; }
                set { sendPrice = value; }
            }

            private string getName;
            public string GetName
            {
                get { return getName; }
                set { getName = value; }
            }

            private string phone;
            public string Phone
            {
                get { return phone; }
                set { phone = value; }
            }

            private string address;
            public string Address
            {
                get { return address; }
                set { address = value; }
            }

            #endregion

            /// <summary>
            /// Product Details
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
            context.Response.ContentType = "text/plain";//返回格式

            if (context.Session["userInfo"] == null)
            {

                context.Response.Write("Login timeout, please log in again！");
                return;

            }
            BasePage basePage = new BasePage();

            if (!basePage.CheckPower("SalesReceiptListAdd"))
            {
                context.Response.Write("You do not have this permission, please contact the administrator！");
                return;
            }

            Users users = context.Session["userInfo"] as Users;

            StreamReader reader = new StreamReader(context.Request.InputStream);

            string strJson = HttpUtility.UrlDecode(reader.ReadToEnd());

            OrderListModel<OrderListItemModel> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderListModel<OrderListItemModel>>(strJson);

            OrderListModel<OrderListItemModel> itemList = obj;

            #region Checking for negative Warehouse

            if (basePage.CheckStoreNum())//Whether to check negative Warehouse
            {
                for (int i = 0; i < itemList.Rows.Count; i++)
                {
                    int ckId = itemList.Rows[i].CkId;
                    int goodsId = itemList.Rows[i].GoodsId;
                    decimal Num = itemList.Rows[i].Num;
                    decimal storeNum = goodsDAL.GetGoodsStoreNumNow(goodsId, ckId);

                    if (Num > storeNum)//If it is greater than the stock quantity
                    {
                        context.Response.Write("No. " + (i + 1).ToString() + " row's product has insufficient remaining stock. The current stock is: " + storeNum.ToString("0.00"));
                        return;
                    }
                }
            }

            #endregion

            #region Main table assignment

            dal.Number = dal.GetBillNumberAuto(users.ShopId);
            dal.ShopId = users.ShopId;
            dal.WlId = obj.venderId;
            dal.BizDate = obj.bizDate;
            dal.BizId = obj.bizId;
            dal.Types = 1;
            dal.Remarks = obj.remarks.ToString();
            dal.Dis = obj.dis;
            dal.DisPrice = obj.disPrice;
            dal.PayNow = obj.payNow;
            dal.PayNowNo = obj.payNowNo;
            dal.BkId = obj.bkId;
            dal.MakeId = users.Id;
            dal.MakeDate = DateTime.Now;
            dal.SendId = obj.SendId;
            dal.SendPayType = obj.SendPayType;
            dal.SendNumber = obj.SendNumber;
            dal.SendPrice = obj.SendPrice;
            dal.GetName = obj.GetName;
            dal.Phone = obj.Phone;
            dal.Address = obj.Address;
            dal.Flag = "Save";

            #endregion

            int pId = dal.Add();

            #region Word table assignment

            if (pId > 0)
            {
                int check = 0;

                SalesReceiptItemDAL item = new SalesReceiptItemDAL();

                for (int i = 0; i < itemList.Rows.Count; i++)
                {
                    item.PId = pId;
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
                    item.Remarks = itemList.Rows[i].Remarks.ToString();
                    item.ItemId = itemList.Rows[i].ItemId;
                    item.SourceNumber = itemList.Rows[i].SourceNumber.ToString();

                    check = item.Add();
                }

                if (check > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = users.ShopId;
                    logs.Users = users.Names;
                    logs.Events = "New sales outbound:" + dal.Number;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    context.Response.Write("Operation successful！");
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
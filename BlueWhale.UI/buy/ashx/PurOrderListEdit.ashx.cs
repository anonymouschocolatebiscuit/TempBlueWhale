using BlueWhale.DAL;
using BlueWhale.Model;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;

namespace BlueWhale.UI.buy.ashx
{
    /// <summary>
    /// $codebehindclassname$ description
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class PurOrderListEdit : IHttpHandler, IRequiresSessionState
    {

        public PurOrderDAL dal = new PurOrderDAL();


        public class OrderListModel<T>
        {
            private int _id;
            public int id
            {
                get { return _id; }
                set { _id = value; }
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

            /// <summary>
            /// Goods Detail
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
            /// Discount price
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
            context.Response.ContentType = "text/plain";

            if (context.Session["userInfo"] == null)
            {
                context.Response.Write("Login overtime, please relogin system!");
                return;
            }
            BasePage basePage = new BasePage();

            if (!basePage.CheckPower("PurOrderListEdit"))
            {
                context.Response.Write("No permission for this action, please contact admin");
                return;
            }

            Users users = context.Session["userInfo"] as Users;

            StreamReader reader = new StreamReader(context.Request.InputStream);
            string strJson = HttpUtility.UrlDecode(reader.ReadToEnd());

            OrderListModel<OrderListItemModel> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderListModel<OrderListItemModel>>(strJson);
            OrderListModel<OrderListItemModel> itemList = obj;

            dal.Id = obj.id;
            dal.Number = "";
            dal.WlId = obj.venderId;
            dal.BizDate = obj.bizDate;
            dal.SendDate = obj.sendDate;
            dal.BizId = obj.bizId;
            dal.Remarks = obj.remarks.ToString();
            dal.MakeId = users.Id;
            dal.MakeDate = DateTime.Now;
            dal.Flag = "Save";

            int pId = dal.Update();

            if (pId > 0)
            {
                int check = 0;

                PurOrderItemDAL item = new PurOrderItemDAL();

                int delete = item.Delete(obj.id);

                for (int i = 0; i < itemList.Rows.Count; i++)
                {
                    item.PId = obj.id;
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
                    logs.Users = users.Phone + "-" + users.Names;
                    logs.Events = "Edit Purchase Order:" + obj.id.ToString();
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    context.Response.Write("Action Success!");
                }
            }
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
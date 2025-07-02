using BlueWhale.DAL;
using BlueWhale.DAL.produce;
using BlueWhale.Model;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;


namespace BlueWhale.UI.produce.ashx
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class produceInListEdit : IHttpHandler, IRequiresSessionState
    {

        public ProduceInDAL dal = new ProduceInDAL();


        public class OrderListModel<T>
        {

            #region Header Fields

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
            /// Discount Amount
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

                context.Response.Write("Login timeout, please log in again!");
                return;

            }
            BasePage basePage = new BasePage();

            Users users = context.Session["userInfo"] as Users;

            StreamReader reader = new StreamReader(context.Request.InputStream);
            string strJson = HttpUtility.UrlDecode(reader.ReadToEnd());

            OrderListModel<OrderListItemModel> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderListModel<OrderListItemModel>>(strJson);

            OrderListModel<OrderListItemModel> itemList = obj;



            #region Main Table Assignment

            dal.Id = obj.id;
            dal.ShopId = users.ShopId;
            dal.Number = "";
            dal.WlId = obj.venderId;
            dal.BizDate = obj.bizDate;

            dal.BizId = obj.bizId;
            dal.Remarks = obj.remarks.ToString();
            dal.MakeId = users.Id;
            dal.MakeDate = DateTime.Now;

            dal.Flag = "Save";


            #endregion


            int pId = dal.Update();


            #region Sub-table Assignment 

            if (pId > 0)
            {
                ProduceInItemDAL item = new ProduceInItemDAL();

                int check = 0;

                item.Delete(obj.id);

                for (int i = 0; i < itemList.Rows.Count; i++)
                {

                    item.PId = obj.id;
                    item.GoodsId = itemList.Rows[i].GoodsId;
                    item.Num = itemList.Rows[i].Num;
                    item.CkId = itemList.Rows[i].CkId;
                    item.Price = itemList.Rows[i].Price;

                    item.Dis = itemList.Rows[i].Dis;
                    item.SumPriceDis = itemList.Rows[i].SumPriceDis;

                    item.PriceNow = itemList.Rows[i].Price;
                    item.SumPriceNow = itemList.Rows[i].SumPriceNow;

                    item.Tax = itemList.Rows[i].Tax;
                    item.PriceTax = itemList.Rows[i].Price;
                    item.SumPriceTax = itemList.Rows[i].SumPriceTax;
                    item.SumPriceAll = itemList.Rows[i].SumPriceNow;

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
                    logs.Events = "Modify Production Stock In：" + dal.Number;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    context.Response.Write("Execution successful！");


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

using System;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using BlueWhale.Model;
using System.Collections.Generic;
using System.IO;


namespace BlueWhale.UI.ashx
{
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class produceInListAdd : IHttpHandler, IRequiresSessionState  
    {
        public ProduceInDAL dal = new ProduceInDAL();

        public class OrderListModel<T>
        {

            #region Header

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
            /// 商品明细
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
            /// 折扣金额
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
                context.Response.Write("登陆超时，请重新登陆系统！");
                return;
            }
            BasePage basePage = new BasePage();

            Users users = context.Session["userInfo"] as Users;

            StreamReader reader = new StreamReader(context.Request.InputStream);
            string strJson = HttpUtility.UrlDecode(reader.ReadToEnd());

            OrderListModel<OrderListItemModel> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderListModel<OrderListItemModel>>(strJson);

            OrderListModel<OrderListItemModel> itemList = obj;

            #region 主表赋值

            dal.ShopId = users.ShopId;
            dal.Number = dal.GetBillNumberAuto(users.ShopId);//"CGDD"+DateTime.Now.ToString("yyyyMMddhhmmssfff");//
            dal.WlId = obj.venderId;
            dal.BizDate = obj.bizDate;

            dal.BizId = obj.bizId;
            dal.Remarks = obj.remarks.ToString();
            dal.MakeId = users.Id;
            dal.MakeDate = DateTime.Now;

            dal.Flag = "保存";
            #endregion


            int pId = dal.Add();
            #region 字表赋值

            if (pId > 0)
            {
                ProduceInItemDAL item = new ProduceInItemDAL();

                int check = 0;
                for (int i = 0; i < itemList.Rows.Count; i++)
                {
                    item.PId = pId;
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
                    LogsDAL logs = new LogsDAL
                    {
                        ShopId = users.ShopId,
                        Users = users.Phone + "-" + users.Names,
                        Events = "新增生产入库：" + dal.Number,
                        Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString()
                    };
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
using BlueWhale.DAL;
using BlueWhale.Model;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.SessionState;

namespace BlueWhale.UI.store.ashx
{
    public class OtherOutListEdit : IHttpHandler, IRequiresSessionState
    {
        public OtherOutDAL dal = new OtherOutDAL();

        public class OrderListModel<T>
        {
            #region Property

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

            private int _typeId;
            public int typeId
            {
                get { return _typeId; }
                set { _typeId = value; }
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

            private decimal sumPrice;
            public decimal SumPrice
            {
                get { return sumPrice; }
                set { sumPrice = value; }
            }

            private int warehouseId;
            public int WarehouseId
            {
                get { return warehouseId; }
                set { warehouseId = value; }
            }

            private string remarks;
            public string Remarks
            {
                get { return remarks; }
                set { remarks = value; }
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Session["userInfo"] == null)
            {
                context.Response.Write("Login timeout，please login again！");
                return;
            }

            BasePage basePage = new BasePage();
            if (!basePage.CheckPower("OtherOutListEdit"))
            {
                context.Response.Write("You have no permission，please contact an administrator！");
                return;
            }

            Users users = context.Session["userInfo"] as Users;
            StreamReader reader = new StreamReader(context.Request.InputStream);
            string strJson = HttpUtility.UrlDecode(reader.ReadToEnd());
            OrderListModel<OrderListItemModel> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderListModel<OrderListItemModel>>(strJson);
            //string sendDate = obj.sendDate.ToShortDateString();
            OrderListModel<OrderListItemModel> itemList = obj;

            #region Table Value Assignment

            dal.Id = obj.id;
            dal.ShopId = users.ShopId;
            dal.WlId = obj.venderId;
            dal.BizDate = obj.bizDate;
            dal.Types = obj.typeId;
            dal.Remarks = obj.remarks.ToString();
            dal.MakeId = users.Id;
            dal.MakeDate = DateTime.Now;
            dal.Flag = "Pending";

            #endregion

            int pId = dal.Update();

            if (pId > 0)
            {
                int check = 0;
                OtherOutItemDAL item = new OtherOutItemDAL();
                int delete = item.Delete(obj.id);

                for (int i = 0; i < itemList.Rows.Count; i++)
                {
                    item.PId = obj.id;
                    item.GoodsId = itemList.Rows[i].GoodsId;
                    item.Num = itemList.Rows[i].Num;
                    item.Price = itemList.Rows[i].Price;
                    item.SumPrice = itemList.Rows[i].SumPrice;
                    item.CkId = itemList.Rows[i].WarehouseId;
                    item.Remarks = itemList.Rows[i].Remarks.ToString();

                    check = item.Add();
                }

                if (check > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = users.ShopId;
                    logs.Users = users.Names;
                    logs.Events = "Edit Other Inbound：" + obj.id.ToString();
                    logs.Ip = HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    context.Response.Write("Operation Successful！");
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

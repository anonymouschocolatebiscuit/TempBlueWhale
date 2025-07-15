using System;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using BlueWhale.Model;
using System.Collections.Generic;
using System.IO;

namespace BlueWhale.UI.pay.ashx
{
    /// <summary>
    /// OtherGetListEdit 的摘要说明
    /// </summary>
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class OtherGetListEdit : IHttpHandler, IRequiresSessionState  
    {
        public OtherGetDAL dal = new OtherGetDAL();

        public class OrderListModel<T>
        {

            #region Header Model
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

            private int _bkId;
            public int bkId
            {
                get { return _bkId; }
                set { _bkId = value; }
            }

            private DateTime _bizDate;
            public DateTime bizDate
            {
                get { return _bizDate; }
                set { _bizDate = value; }
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

            private int typeId;
            public int TypeId
            {
                get { return typeId; }
                set { typeId = value; }
            }


            private decimal price;
            public decimal Price
            {
                get { return price; }
                set { price = value; }
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

                context.Response.Write("Login timeout, please log in again!");
                return;

            }
            BasePage basePage = new BasePage();
            if (!basePage.CheckPower("OtherGetListEdit"))
            {
                context.Response.Write("You do not have permission to do this operation. Please contact the administrator!");
                return;
            }
            Users users = context.Session["userInfo"] as Users;


            StreamReader reader = new StreamReader(context.Request.InputStream);
            string strJson = HttpUtility.UrlDecode(reader.ReadToEnd());

            OrderListModel<OrderListItemModel> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderListModel<OrderListItemModel>>(strJson);

            OrderListModel<OrderListItemModel> itemList = obj;

            #region Initial Model

            dal.Id = obj.id;
            dal.ShopId = users.ShopId;
            dal.Number = "";
            dal.WlId = obj.venderId;
            dal.BizDate = obj.bizDate;
            dal.BkId = obj.bkId;

            dal.Remarks = obj.remarks.ToString();
            dal.MakeId = users.Id;
            dal.MakeDate = DateTime.Now;

            dal.Flag = "Save";
            #endregion


            int pId = dal.Update();

            #region 字表赋值

            if (pId > 0)
            {

                int check = 0;

                OtherGetItemDAL item = new OtherGetItemDAL();

                int delete = item.Delete(obj.id);


                for (int i = 0; i < itemList.Rows.Count; i++)
                {

                    item.PId = obj.id;

                    item.TypeId = itemList.Rows[i].TypeId;

                    item.Price = itemList.Rows[i].Price;

                    item.Remarks = itemList.Rows[i].Remarks.ToString();

                    check = item.Add();
                }
                if (check > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = users.ShopId;
                    logs.Users = users.Names;
                    logs.Events = "Modify other payment orders: " + obj.id.ToString();
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();
                    context.Response.Write("Succefull!");
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
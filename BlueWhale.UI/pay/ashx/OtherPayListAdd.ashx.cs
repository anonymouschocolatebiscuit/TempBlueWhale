using BlueWhale.DAL;
using BlueWhale.Model;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;

namespace BlueWhale.UI.pay.ashx
{
    /// <summary>
    /// $codebehindclassname$ Summary
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class OtherPayListAdd : IHttpHandler, IRequiresSessionState
    {
        public OtherPayDAL dal = new OtherPayDAL();

        public class OrderListModel<T>
        {
            #region Header Fields

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
            if (!basePage.CheckPower("OtherPayListAdd"))
            {
                context.Response.Write("You do not have this permission, please contact the administrator!");
                return;
            }
            Users users = context.Session["userInfo"] as Users;


            StreamReader reader = new StreamReader(context.Request.InputStream);
            string strJson = HttpUtility.UrlDecode(reader.ReadToEnd());

            OrderListModel<OrderListItemModel> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderListModel<OrderListItemModel>>(strJson);

            OrderListModel<OrderListItemModel> itemList = obj;

            #region Main table assignment

            dal.ShopId = users.ShopId;
            dal.Number = dal.GetBillNumberAuto(users.ShopId);
            dal.WlId = obj.venderId;
            dal.BizDate = obj.bizDate;
            dal.BkId = obj.bkId;
            dal.Remarks = obj.remarks.ToString();
            dal.MakeId = users.Id;
            dal.MakeDate = DateTime.Now;
            dal.Flag = "Save";

            #endregion

            int pId = dal.Add();

            #region Word table assignment

            if (pId > 0)
            {
                int check = 0;

                OtherPayItemDAL item = new OtherPayItemDAL();

                for (int i = 0; i < itemList.Rows.Count; i++)
                {
                    item.PId = pId;
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
                    logs.Events = "Add other payment slips: " + dal.Number;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();
                    context.Response.Write("Operation successful!");
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

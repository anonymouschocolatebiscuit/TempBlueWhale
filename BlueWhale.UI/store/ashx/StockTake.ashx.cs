using BlueWhale.DAL;
using BlueWhale.Model;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace BlueWhale.UI.store.ashx
{
    /// <summary>
    /// Summary description for StockTake
    /// </summary>
    public class StockTake : IHttpHandler, IRequiresSessionState
    {
        OtherInDAL inDAL = new OtherInDAL();
        OtherOutDAL outDAL = new OtherOutDAL();

        public class OrderListModel<T>
        {

            #region Header Fields

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

            private decimal sumNum;
            public decimal SumNum
            {
                get { return sumNum; }
                set { sumNum = value; }
            }

            private decimal sumNumPD;
            public decimal SumNumPD
            {
                get { return sumNumPD; }
                set { sumNumPD = value; }
            }

            private decimal sumNumPK;
            public decimal SumNumPK
            {
                get { return sumNumPK; }
                set { sumNumPK = value; }
            }


            private int ckId;
            public int CkId
            {
                get { return ckId; }
                set { ckId = value; }
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

            if (!basePage.CheckPower("Pandian"))
            {
                context.Response.Write("You do not have this permission, please contact the administrator！");
                return;
            }

            Users users = context.Session["userInfo"] as Users;
            StreamReader reader = new StreamReader(context.Request.InputStream);
            string strJson = HttpUtility.UrlDecode(reader.ReadToEnd());
            OrderListModel<OrderListItemModel> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderListModel<OrderListItemModel>>(strJson);
            OrderListModel<OrderListItemModel> itemList = obj;
            bool hasMore = false;
            bool hasDes = false;
            string result = "Execution successful！";

            #region Traverse the situation of Warehouse surplus and shortage

            for (int i = 0; i < itemList.Rows.Count; i++)
            {
                int goodsId = itemList.Rows[i].GoodsId;
                int ckId = itemList.Rows[i].CkId;

                decimal numPd = itemList.Rows[i].SumNumPD;
                decimal numSystem = itemList.Rows[i].SumNum;
                decimal disNum = numSystem - numPd;

                if (numSystem > numPd)
                {
                    hasDes = true;
                }

                if (numSystem < numPd)
                {
                    hasMore = true;
                }
            }

            #endregion

            #region Marked as Warehouse surplus 

            if (hasMore)
            {
                string numberIn = inDAL.GetBillNumberAuto(users.ShopId);

                result += " Generate surplus Warehouse receipt：" + numberIn;

                inDAL.ShopId = users.ShopId;
                inDAL.Number = numberIn;

                inDAL.BizDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                inDAL.BizId = users.Id;
                inDAL.MakeId = users.Id;
                inDAL.Types = -1;
                inDAL.WlId = 0;

                inDAL.Remarks = obj.remarks;
                inDAL.MakeDate = DateTime.Now;
                inDAL.Flag = "Save";

                int pId = inDAL.Add();

                if (pId > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.Users = users.Names;
                    logs.Events = result;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    for (int i = 0; i < itemList.Rows.Count; i++)
                    {
                        int goodsId = itemList.Rows[i].GoodsId;
                        int ckId = itemList.Rows[i].CkId;


                        decimal numPd = itemList.Rows[i].SumNumPD;

                        decimal numSystem = itemList.Rows[i].SumNum;

                        decimal disNum = numSystem - numPd;

                        if (numSystem < numPd)
                        {
                            OtherInItemDAL item = new OtherInItemDAL();
                            item.GoodsId = goodsId;
                            item.Num = disNum * (-1);
                            item.CkId = ckId;
                            item.Price = 0;
                            item.SumPrice = 0;
                            item.Remarks = "Put Warehouse surplus into stock";

                            item.PId = pId;
                            item.Add();
                        }
                    }
                }
            }

            #endregion

            #region Marked as Warehouse shortage

            if (hasDes)
            {
                string numberOut = outDAL.GetBillNumberAuto(users.ShopId);

                result += " Generate stock-out form for Warehouse shortage：" + numberOut;

                outDAL.ShopId = users.ShopId;
                outDAL.Number = numberOut;

                outDAL.BizDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                outDAL.BizId = users.Id;
                outDAL.MakeId = users.Id;
                outDAL.Types = -1;
                outDAL.WlId = 0;
                outDAL.Remarks = obj.remarks;
                outDAL.MakeDate = DateTime.Now;
                outDAL.Flag = "Save";

                int pId = outDAL.Add();
                if (pId > 0)
                {

                    LogsDAL logs = new LogsDAL();
                    logs.Users = users.Names;
                    logs.Events = result;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    for (int i = 0; i < itemList.Rows.Count; i++)
                    {
                        int goodsId = itemList.Rows[i].GoodsId;
                        int ckId = itemList.Rows[i].CkId;

                        decimal numPd = itemList.Rows[i].SumNumPD;
                        decimal numSystem = itemList.Rows[i].SumNum;
                        decimal disNum = numSystem - numPd;

                        if (numSystem > numPd)
                        {
                            OtherOutItemDAL item = new OtherOutItemDAL();
                            item.GoodsId = goodsId;
                            item.Num = disNum;
                            item.CkId = ckId;
                            item.Price = 0;
                            item.SumPrice = 0;
                            item.Remarks = "Stock out for Warehouse shortage";

                            item.PId = pId;
                            item.Add();

                        }

                    }
                }
            }
            #endregion

            context.Response.Write(result);
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
using System;
using System.Web;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using BlueWhale.Model;
using System.Collections.Generic;
using System.Web.SessionState;
using System.IO;
using System.Web.Services;

namespace BlueWhale.UI.store.ashx
{
    /// <summary>
    /// InventoryTransferListAdd 的摘要说明
    /// </summary>
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class InventoryTransferListAdd : IHttpHandler, IRequiresSessionState
    {
        public InventoryTransferDAL dal = new InventoryTransferDAL();


        public class OrderListModel<T>
        {

            #region 表头字段



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


            private int ckIdOut;
            public int CkIdOut
            {
                get { return ckIdOut; }
                set { ckIdOut = value; }
            }


            private int ckIdIn;
            public int CkIdIn
            {
                get { return ckIdIn; }
                set { ckIdIn = value; }
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
            context.Response.ContentType = "text/plain";//返回格式

            if (context.Session["userInfo"] == null)
            {

                context.Response.Write("Login timeout, please login again!");
                return;

            }
            BasePage basePage = new BasePage();
            if (!basePage.CheckPower("InventoryTransferListAdd"))
            {
                context.Response.Write("No operate permission, please contact admin");
                return;
            }
            Users users = context.Session["userInfo"] as Users;


            StreamReader reader = new StreamReader(context.Request.InputStream);
            string strJson = HttpUtility.UrlDecode(reader.ReadToEnd());

            OrderListModel<OrderListItemModel> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderListModel<OrderListItemModel>>(strJson);

            //string sendDate = obj.sendDate.ToShortDateString();

            OrderListModel<OrderListItemModel> itemList = obj;



            #region 主表赋值

            dal.ShopId = users.ShopId;
            dal.Number = dal.GetBillNumberAuto(users.ShopId);

            dal.BizDate = obj.bizDate;

            dal.Remarks = obj.remarks.ToString();
            dal.MakeId = users.Id;
            dal.MakeDate = DateTime.Now;

            dal.Flag = "Save";


            #endregion

            int pId = dal.Add();

            #region Initial InventoryTransferDetails

            if (pId > 0)
            {

                int check = 0;

                InventoryTransferDetailsDAL item = new InventoryTransferDetailsDAL();


                for (int i = 0; i < itemList.Rows.Count; i++)
                {
                    item.PId = pId;
                    item.GoodsId = itemList.Rows[i].GoodsId;
                    item.Num = itemList.Rows[i].Num;

                    item.CkIdIn = itemList.Rows[i].CkIdIn;
                    item.CkIdOut = itemList.Rows[i].CkIdOut;

                    item.Remarks = itemList.Rows[i].Remarks.ToString();

                    check = item.Add();
                }

                if (check > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.Users = users.Names;
                    logs.Events = "Add Warehouse transfer:" + dal.Number;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    context.Response.Write("Execute successful");
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
using BlueWhale.DAL;
using BlueWhale.Model;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;// Using Session requires referencing

namespace BlueWhale.UI.pay.ashx
{
    /// <summary>
    /// $codebehindclassname$ Summary description
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class CheckBillGetListAdd : IHttpHandler, IRequiresSessionState  // Using Session requires referencing IRequiresSessionState
    {

        public CheckBillDAL dal = new CheckBillDAL();


        public class OrderListModel<T>
        {
            #region Header Fields

            private int id;
            public int Id
            {
                get { return id; }
                set { id = value; }
            }

            private string number;
            public string Number
            {
                get { return number; }
                set { number = value; }
            }

            private int clientId;
            public int ClientId
            {
                get { return clientId; }
                set { clientId = value; }
            }

            private DateTime bizDate;
            public DateTime BizDate
            {
                get { return bizDate; }
                set { bizDate = value; }
            }

            private decimal checkPrice;
            public decimal CheckPrice
            {
                get { return checkPrice; }
                set { checkPrice = value; }
            }



            private string remarks;
            public string Remarks
            {
                get { return remarks; }
                set { remarks = value; }
            }


            #endregion

            /// <summary>
            /// Payment Account
            /// </summary>
            public List<OrderListItemModel> _Rows;
            public List<OrderListItemModel> Rows
            {
                get { return _Rows; }
                set { _Rows = value; }
            }

            /// <summary>
            /// Verification Details
            /// </summary>
            public List<OrderListItemModelBill> _RowsBill;
            public List<OrderListItemModelBill> RowsBill
            {
                get { return _RowsBill; }
                set { _RowsBill = value; }
            }
        }

        [Serializable]
        public class OrderListItemModel
        {
            private string sourceNumber;
            public string SourceNumber
            {
                get { return sourceNumber; }
                set { sourceNumber = value; }
            }

            private decimal priceCheckNow;
            public decimal PriceCheckNow
            {
                get { return priceCheckNow; }
                set { priceCheckNow = value; }
            }

        }


        [Serializable]
        public class OrderListItemModelBill
        {
            private string sourceNumber;
            public string SourceNumber
            {
                get { return sourceNumber; }
                set { sourceNumber = value; }
            }

            private decimal priceCheckNow;
            public decimal PriceCheckNow
            {
                get { return priceCheckNow; }
                set { priceCheckNow = value; }
            }


        }


        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain"; // Response format

            if (context.Session["userInfo"] == null)
            {
                context.Response.Write("Login timeout, please log in to the system again!");
                return;

            }
            BasePage basePage = new BasePage();
            if (!basePage.CheckPower("CheckBillGetListAdd"))
            {
                context.Response.Write("No permission for this operation, please contact the administrator!");
                return;
            }
            Users users = context.Session["userInfo"] as Users;


            StreamReader reader = new StreamReader(context.Request.InputStream);
            string strJson = HttpUtility.UrlDecode(reader.ReadToEnd());

            OrderListModel<OrderListItemModel> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderListModel<OrderListItemModel>>(strJson);


            OrderListModel<OrderListItemModelBill> objBill = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderListModel<OrderListItemModelBill>>(strJson);


            OrderListModel<OrderListItemModel> payList = obj;

            OrderListModel<OrderListItemModelBill> billList = objBill;

            #region Main Table Assignment ----------------- Prepaid to Accounts Receivable ------ typeId=1

            dal.ShopId = users.ShopId;
            dal.Number = dal.GetBillNumberAuto(users.ShopId);

            dal.BizType = 1; // Prepaid to Accounts Receivable ------ typeId=1
            dal.BizDate = obj.BizDate;
            dal.ClientIdA = obj.ClientId;
            dal.ClientIdB = 0;
            dal.VenderIdA = 0;
            dal.VenderIdB = 0;

            dal.CheckPrice = obj.CheckPrice;
            dal.Remarks = obj.Remarks.ToString();
            dal.MakeId = users.Id;
            dal.MakeDate = DateTime.Now;

            dal.Flag = "Save";

            #endregion


            int pId = dal.Add();

            #region Sub Table Assignment

            if (pId > 0)
            {

                int check = 0;

                #region Insert Entry -- Prepaid

                CheckBillItemReceivableDAL recei = new CheckBillItemReceivableDAL();


                for (int i = 0; i < payList.Rows.Count; i++)
                {
                    recei.PId = pId;
                    recei.PriceCheckNow = payList.Rows[i].PriceCheckNow;
                    recei.SourceNumber = payList.Rows[i].SourceNumber;
                    recei.Flag = "Save";
                    check = recei.Add();

                }

                #endregion

                #region Insert Entry -- Accounts Receivable

                CheckBillItemSalesDAL sales = new CheckBillItemSalesDAL();


                for (int i = 0; i < billList.RowsBill.Count; i++)
                {
                    sales.PId = pId;
                    sales.PriceCheckNow = billList.RowsBill[i].PriceCheckNow;
                    sales.SourceNumber = billList.RowsBill[i].SourceNumber;
                    sales.Flag = "Save";
                    check = sales.Add();

                }

                #endregion

                if (check > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = users.ShopId;
                    logs.Users = users.Names;
                    logs.Events = "New Collection Verification Order: " + dal.Number;
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

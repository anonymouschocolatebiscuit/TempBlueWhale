using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

using System.Web.SessionState;//用Session必须引用
using Lanwei.Weixin.DAL;
using Lanwei.Weixin.UI.src;
using Lanwei.Weixin.Common;
using Lanwei.Weixin.Model;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

using System.Runtime.Serialization;
using System.Text;


namespace Lanwei.Weixin.UI.pay.ashx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class CheckBillGetListEdit : IHttpHandler, IRequiresSessionState  //用Session必须引用IRequiresSessionState
    {

        public CheckBillDAL dal = new CheckBillDAL();


        public class OrderListModel<T>
        {

            #region 表头字段


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
            /// 付款账户
            /// </summary>
            public List<OrderListItemModel> _Rows;
            public List<OrderListItemModel> Rows
            {
                get { return _Rows; }
                set { _Rows = value; }
            }

            /// <summary>
            /// 核销明细
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
            context.Response.ContentType = "text/plain";//返回格式

            if (context.Session["userInfo"] == null)
            {

                context.Response.Write("登陆超时，请重新登陆系统！");
                return;

            }
            BasePage basePage = new BasePage();
            if (!basePage.CheckPower("CheckBillGetListEdit"))
            {
                context.Response.Write("无此操作权限，请联系管理员！");
                return;
            }
            Users users = context.Session["userInfo"] as Users;


            StreamReader reader = new StreamReader(context.Request.InputStream);
            string strJson = HttpUtility.UrlDecode(reader.ReadToEnd());

            OrderListModel<OrderListItemModel> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderListModel<OrderListItemModel>>(strJson);


            OrderListModel<OrderListItemModelBill> objBill = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderListModel<OrderListItemModelBill>>(strJson);


            OrderListModel<OrderListItemModel> payList = obj;

            OrderListModel<OrderListItemModelBill> billList = objBill;



            #region 主表赋值-----------------预收充应收------typeId=1

            dal.Id = obj.Id;
            dal.ShopId = users.ShopId;
            dal.Number = "";

            dal.BizType = 1;//预收充应收------typeId=1
            dal.BizDate = obj.BizDate;
            dal.ClientIdA = obj.ClientId;
            dal.ClientIdB = 0;
            dal.VenderIdA = 0;
            dal.VenderIdB = 0;

            dal.CheckPrice = obj.CheckPrice;
            dal.Remarks = obj.Remarks.ToString();
            dal.MakeId = users.Id;
            dal.MakeDate = DateTime.Now;


            dal.Flag = "保存";

            #endregion


            int pId = dal.Update();


            #region 字表赋值

            if (pId > 0)
            {

                int check = 0;



                #region 插入分录 --预收

                CheckBillItemReceivableDAL recei = new CheckBillItemReceivableDAL();

                int deleteItem = recei.Delete(obj.Id);

                for (int i = 0; i < payList.Rows.Count; i++)
                {
                    recei.PId = obj.Id;
                    recei.PriceCheckNow = payList.Rows[i].PriceCheckNow;
                    recei.SourceNumber = payList.Rows[i].SourceNumber;
                    recei.Flag = "保存";
                    check = recei.Add();

                }

                #endregion

                #region 插入分录 --应收


                CheckBillItemSalesDAL sales = new CheckBillItemSalesDAL();

                deleteItem = sales.Delete(obj.Id);

                for (int i = 0; i < billList.RowsBill.Count; i++)
                {
                    sales.PId = obj.Id;
                    sales.PriceCheckNow = billList.RowsBill[i].PriceCheckNow;
                    sales.SourceNumber = billList.RowsBill[i].SourceNumber;
                    sales.Flag = "保存";
                    check = sales.Add();

                }

                #endregion

                if (check > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = users.ShopId;
                    logs.Users = users.Names;
                    logs.Events = "修改收款核销单：" + obj.Id.ToString();
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    context.Response.Write("操作成功！");


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

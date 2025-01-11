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

using Lanwei.Weixin.UI.src;

namespace Lanwei.Weixin.UI.sales.ashx
{
    /// <summary>
    /// SalesQuoteListAdd 的摘要说明
    /// </summary>
    public class SalesQuoteListAdd : IHttpHandler, IRequiresSessionState  //用Session必须引用IRequiresSessionState
    {

        public SalesQuoteDAL dal = new SalesQuoteDAL();


        public class OrderListModel<T>
        {

            #region 表头字段


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


            private DateTime _deadLine;
            public DateTime deadLine
            {
                get { return _deadLine; }
                set { _deadLine = value; }
            }

            private string _sendPlace;
            public string sendPlace
            {
                get { return _sendPlace; }
                set { _sendPlace = value; }
            }


            private string _isTax;
            public string isTax
            {
                get { return _isTax; }
                set { _isTax = value; }
            }

            private string _isFreight;
            public string isFreight
            {
                get { return _isFreight; }
                set { _isFreight = value; }
            }

            private string _freightWay;
            public string freightWay
            {
                get { return _freightWay; }
                set { _freightWay = value; }
            }

            private string _packageWay;
            public string packageWay
            {
                get { return _packageWay; }
                set { _packageWay = value; }
            }

            private string _sendDate;
            public string sendDate
            {
                get { return _sendDate; }
                set { _sendDate = value; }
            }

            private string _payWay;
            public string payWay
            {
                get { return _payWay; }
                set { _payWay = value; }
            }

            private string _payDate;
            public string payDate
            {
                get { return _payDate; }
                set { _payDate = value; }
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



            private int pId;
            public int PId
            {
                get { return pId; }
                set { pId = value; }
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

                context.Response.Write("登陆超时，请重新登陆系统！");
                return;

            }
            BasePage basePage = new BasePage();
            if (!basePage.CheckPower("SalesQuoteListAdd"))
            {
                context.Response.Write("无此操作权限，请联系管理员！");
                return;
            }
            Users users = context.Session["userInfo"] as Users;


            StreamReader reader = new StreamReader(context.Request.InputStream);
            string strJson = HttpUtility.UrlDecode(reader.ReadToEnd());

            OrderListModel<OrderListItemModel> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderListModel<OrderListItemModel>>(strJson);



            OrderListModel<OrderListItemModel> itemList = obj;



            #region 主表赋值

            dal.ShopId = users.ShopId;
            dal.Number = dal.GetBillNumberAuto(users.ShopId);
            dal.WlId = obj.venderId;
            dal.BizId = obj.bizId;
            dal.SendPlace = obj.sendDate;
            dal.DeadLine = obj.deadLine;
            dal.BizDate = obj.bizDate;
            dal.IsTax = obj.isTax;
            dal.IsFreight = obj.isFreight;
            dal.FreightWay = obj.freightWay;
            dal.Package = obj.packageWay;
            dal.SendDate = obj.sendDate;
            dal.PayWay = obj.payWay;
            dal.PayDate = obj.payDate;
            dal.Remarks = obj.remarks.ToString();
            dal.MakeId = users.Id;
            dal.BizId = users.Id;
            dal.MakeDate = DateTime.Now;

            dal.Flag = "保存";


            #endregion


            int pId = dal.Add();


            #region 字表赋值

            if (pId > 0)
            {

                int check = 0;
                for (int i = 0; i < itemList.Rows.Count; i++)
                {




                    SalesQuoteItemDAL item = new SalesQuoteItemDAL();

                    item.PId = pId;
                    item.GoodsId = itemList.Rows[i].GoodsId;


                    item.Num = itemList.Rows[i].Num;

                    item.Price = itemList.Rows[i].Price;
                    item.SumPrice = itemList.Rows[i].SumPrice;

                    item.Tax = itemList.Rows[i].Tax;
                    item.PriceTax = itemList.Rows[i].PriceTax;
                    item.SumPriceTax = itemList.Rows[i].SumPriceTax;

                    item.SumPriceAll = itemList.Rows[i].SumPriceAll;

                    item.Remarks = itemList.Rows[i].Remarks.ToString();

                    check = item.Add();





                }
                if (check > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.Users = users.Names;
                    logs.Events = "新增销售报价单：" + dal.Number;
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
using BlueWhale.DAL;
using BlueWhale.Model;
using BlueWhale.Model.produce;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.SessionState;

namespace BlueWhale.UI.produce.ashx
{
    /// <summary>
    /// produceListAdd
    /// </summary>
    public class ProduceGetListAdd : IHttpHandler, IRequiresSessionState
    {
        public DAL.produce.ProduceGetList dal = new DAL.produce.ProduceGetList();

        public class OrderListModel<T>
        {

            #region Model
            private int _id;
            private int? _shopid;
            private string _number;
            private int? _deptid;
            private string _plannumber;
            private int? _goodsid;
            private decimal? _num;
            private int? _makeid;
            private DateTime? _makedate;
            private int? _bizid;
            private DateTime? _bizdate;
            private int? _checkid;
            private DateTime? _checkdate;
            private string _remarks;
            /// <summary>
            /// 
            /// </summary>
            public int id
            {
                set { _id = value; }
                get { return _id; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int? shopId
            {
                set { _shopid = value; }
                get { return _shopid; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string number
            {
                set { _number = value; }
                get { return _number; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int? deptId
            {
                set { _deptid = value; }
                get { return _deptid; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string planNumber
            {
                set { _plannumber = value; }
                get { return _plannumber; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int? goodsId
            {
                set { _goodsid = value; }
                get { return _goodsid; }
            }
            /// <summary>
            /// 
            /// </summary>
            public decimal? num
            {
                set { _num = value; }
                get { return _num; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int? makeId
            {
                set { _makeid = value; }
                get { return _makeid; }
            }
            /// <summary>
            /// 
            /// </summary>
            public DateTime? makeDate
            {
                set { _makedate = value; }
                get { return _makedate; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int? bizId
            {
                set { _bizid = value; }
                get { return _bizid; }
            }
            /// <summary>
            /// 
            /// </summary>
            public DateTime? bizDate
            {
                set { _bizdate = value; }
                get { return _bizdate; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int? checkId
            {
                set { _checkid = value; }
                get { return _checkid; }
            }
            /// <summary>
            /// 
            /// </summary>
            public DateTime? checkDate
            {
                set { _checkdate = value; }
                get { return _checkdate; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string remarks
            {
                set { _remarks = value; }
                get { return _remarks; }
            }
            #endregion Model

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
            #region Model
            private int _id;
            private int? _pid;
            private int? _goodsid;
            private int? _ckid;
            private string _pihao;
            private decimal? _numapply;
            private decimal? _num;
            private decimal? _price;
            private decimal? _sumprice;
            private string _remarks;
            /// <summary>
            /// 
            /// </summary>
            public int id
            {
                set { _id = value; }
                get { return _id; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int? pId
            {
                set { _pid = value; }
                get { return _pid; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int? goodsId
            {
                set { _goodsid = value; }
                get { return _goodsid; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int? ckId
            {
                set { _ckid = value; }
                get { return _ckid; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string pihao
            {
                set { _pihao = value; }
                get { return _pihao; }
            }
            /// <summary>
            /// 
            /// </summary>
            public decimal? numApply
            {
                set { _numapply = value; }
                get { return _numapply; }
            }
            /// <summary>
            /// 
            /// </summary>
            public decimal? num
            {
                set { _num = value; }
                get { return _num; }
            }
            /// <summary>
            /// 
            /// </summary>
            public decimal? price
            {
                set { _price = value; }
                get { return _price; }
            }
            /// <summary>
            /// 
            /// </summary>
            public decimal? sumPrice
            {
                set { _sumprice = value; }
                get { return _sumprice; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string remarks
            {
                set { _remarks = value; }
                get { return _remarks; }
            }
            #endregion Model
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

            Model.produce.ProduceGetList model = new Model.produce.ProduceGetList();

            #region Main table assignment

            model.number = dal.GetBillNumberAuto(users.ShopId);
            model.shopId = users.ShopId;
            model.deptId = 0;
            model.planNumber = obj.planNumber;
            model.bizId = obj.bizId;
            model.bizDate = obj.bizDate;
            model.goodsId = obj.goodsId;
            model.remarks = obj.remarks.ToString();
            model.num = obj.num;
            model.makeId = users.Id;
            model.makeDate = DateTime.Now;
            model.flag= "保存";

            #endregion

            int pId = dal.Add(model);

            #region Word table assignment

            if (pId > 0)
            {
                int check = 0;

                #region bom

                DAL.produce.ProduceGetListItem itemDAL = new DAL.produce.ProduceGetListItem();
                ProduceGetListItemModel item = new ProduceGetListItemModel();

                for (int i = 0; i < itemList.Rows.Count; i++)
                {

                    item.pId = pId;
                    item.goodsId = itemList.Rows[i].goodsId;
                    item.num = itemList.Rows[i].num;
                    item.pihao = itemList.Rows[i].pihao;
                    item.numApply = itemList.Rows[i].numApply;
                    item.price = itemList.Rows[i].price;
                    item.sumPrice = itemList.Rows[i].sumPrice;
                    item.ckId = itemList.Rows[i].ckId;
                    item.remarks = itemList.Rows[i].remarks.ToString();
                    check = itemDAL.Add(item);
                }

                #endregion

                LogsDAL logs = new LogsDAL();
                logs.ShopId = users.ShopId;
                logs.Users = users.Names;
                logs.Events = "Create Production Material Collection" + model.number;
                logs.Ip = HttpContext.Current.Request.UserHostAddress.ToString();
                logs.Add();

                context.Response.Write("Success！");
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

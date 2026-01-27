using BlueWhale.DAL;
using BlueWhale.DAL.produce;
using BlueWhale.Model;
using BlueWhale.UI.src;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;



namespace BlueWhale.UI.produce.ashx
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class goodsBomListAdd : IHttpHandler, IRequiresSessionState
    {

        public DAL.produce.goodsBomList dal = new DAL.produce.goodsBomList();
        
  
        public class ListModel<T>
        {

            #region Model
            private int _id;
            private int? _shopid;
            private int? _typeid;
            private string _number;
            private string _edition;
            private string _flaguse;
            private string _flagcheck;
            private string _tuhao;
            private int? _goodsid;
            private decimal? _num;
            private decimal? _rate;
            private string _remarks;
            private int? _makeid;
            private DateTime? _makedate;
            private int? _checkid;
            private DateTime? _checkdate;
            public int id
            {
                set { _id = value; }
                get { return _id; }
            }
            public int? shopId
            {
                set { _shopid = value; }
                get { return _shopid; }
            }
            public int? typeId
            {
                set { _typeid = value; }
                get { return _typeid; }
            }
            public string number
            {
                set { _number = value; }
                get { return _number; }
            }
            public string edition
            {
                set { _edition = value; }
                get { return _edition; }
            }
            public string flagUse
            {
                set { _flaguse = value; }
                get { return _flaguse; }
            }
            public string flagCheck
            {
                set { _flagcheck = value; }
                get { return _flagcheck; }
            }
            
            public string tuhao
            {
                set { _tuhao = value; }
                get { return _tuhao; }
            }
            public int? goodsId
            {
                set { _goodsid = value; }
                get { return _goodsid; }
            }
            public decimal? num
            {
                set { _num = value; }
                get { return _num; }
            }
            public decimal? rate
            {
                set { _rate = value; }
                get { return _rate; }
            }
            public string remarks
            {
                set { _remarks = value; }
                get { return _remarks; }
            }
            public int? makeId
            {
                set { _makeid = value; }
                get { return _makeid; }
            }
            public DateTime? makeDate
            {
                set { _makedate = value; }
                get { return _makedate; }
            }
            public int? checkId
            {
                set { _checkid = value; }
                get { return _checkid; }
            }
            public DateTime? checkDate
            {
                set { _checkdate = value; }
                get { return _checkdate; }
            }
            #endregion Model


            /// <summary>
            /// Product Detail
            /// </summary>
            public List<ListItemModel> _Rows;
            public List<ListItemModel> Rows
            {
                get { return _Rows; }
                set { _Rows = value; }
            }

         
        }
 
        [Serializable]
        public class ListItemModel
        {

            #region Model
            private int _id;
            private int? _pid;
            private int? _itemid;
            private int? _goodsid;
            private decimal? _num;
            private decimal? _rate;
            private string _remarks;
            public int id
            {
                set { _id = value; }
                get { return _id; }
            }
            public int? pId
            {
                set { _pid = value; }
                get { return _pid; }
            }
            public int? itemId
            {
                set { _itemid = value; }
                get { return _itemid; }
            }
            public int? goodsId
            {
                set { _goodsid = value; }
                get { return _goodsid; }
            }
            public decimal? num
            {
                set { _num = value; }
                get { return _num; }
            }
            public decimal? rate
            {
                set { _rate = value; }
                get { return _rate; }
            }
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
            //if (!basePage.CheckPower("goodsBomListAdd"))
            //{
            //    context.Response.Write("You do not have this permission, please contact the administrator");
            //    return;
            //}
            Users users = context.Session["userInfo"] as Users;

    
            StreamReader reader = new StreamReader(context.Request.InputStream);
            string strJson = HttpUtility.UrlDecode(reader.ReadToEnd());

         

            ListModel<ListItemModel> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<ListModel<ListItemModel>>(strJson);

            ListModel<ListItemModel> itemList = obj;


            if (dal.ExistsBomList(obj.goodsId))
            {
                context.Response.Write("Item is exist in BOM，Cannot duplicate add！");
                return;

            }

            #region 

            Model.produce.goodsBomList model = new Model.produce.goodsBomList();

            model.shopId = users.ShopId;
            model.typeId = obj.typeId;
            model.number = dal.GetBillNumberAuto(users.ShopId);
            model.edition = obj.edition;
            model.tuhao = obj.tuhao;
            model.goodsId = obj.goodsId;
            model.num = obj.num;
            model.remarks = obj.remarks;
            model.rate=obj.rate;
            model.makeId = users.Id;
            model.makeDate = DateTime.Now;

            model.flagCheck = "Pending";
            model.flagUse = "Available";


            #endregion


            int pId = dal.Add(model);


            #region 

            if (pId > 0)
            {

                int check = 0;


                Model.produce.goodsBomListItem modelItem = new Model.produce.goodsBomListItem();
                goodsBomListItem item = new goodsBomListItem();


                for (int i = 0; i < itemList.Rows.Count; i++)
                {

                    modelItem.pId = pId;

                    modelItem.itemId = i+1;
                    modelItem.goodsId = itemList.Rows[i].goodsId;
                    modelItem.num = itemList.Rows[i].num;
                    modelItem.rate = itemList.Rows[i].rate;

                    modelItem.remarks = itemList.Rows[i].remarks.ToString();

                    check = item.Add(modelItem);


                }
                if (check > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.ShopId=users.ShopId;
                    logs.Users = users.Names;
                    logs.Events = "Add new product BOM list：" + model.number;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
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

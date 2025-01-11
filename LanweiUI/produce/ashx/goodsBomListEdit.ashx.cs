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



namespace Lanwei.Weixin.UI.produce.ashx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class goodsBomListEdit : IHttpHandler, IRequiresSessionState  //用Session必须引用IRequiresSessionState
    {

        public DAL.goodsBomList dal = new DAL.goodsBomList();
        
  
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
            public int? typeId
            {
                set { _typeid = value; }
                get { return _typeid; }
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
            public string edition
            {
                set { _edition = value; }
                get { return _edition; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string flagUse
            {
                set { _flaguse = value; }
                get { return _flaguse; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string flagCheck
            {
                set { _flagcheck = value; }
                get { return _flagcheck; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string tuhao
            {
                set { _tuhao = value; }
                get { return _tuhao; }
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
            public decimal? rate
            {
                set { _rate = value; }
                get { return _rate; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string remarks
            {
                set { _remarks = value; }
                get { return _remarks; }
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
            #endregion Model


            /// <summary>
            /// 商品明细
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
            public int? itemId
            {
                set { _itemid = value; }
                get { return _itemid; }
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
            public decimal? rate
            {
                set { _rate = value; }
                get { return _rate; }
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
            context.Response.ContentType = "text/plain";//返回格式

            if (context.Session["userInfo"] == null)
            {

                context.Response.Write("登陆超时，请重新登陆系统！");
                return;
 
            }
            BasePage basePage = new BasePage();
            //if (!basePage.CheckPower("goodsBomListAdd"))
            //{
            //    context.Response.Write("无此操作权限，请联系管理员！");
            //    return;
            //}
            Users users = context.Session["userInfo"] as Users;

    
            StreamReader reader = new StreamReader(context.Request.InputStream);
            string strJson = HttpUtility.UrlDecode(reader.ReadToEnd());

         

            ListModel<ListItemModel> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<ListModel<ListItemModel>>(strJson);

            ListModel<ListItemModel> itemList = obj;

          

            #region 主表赋值

            Model.goodsBomList model = new Model.goodsBomList();

            model.id = obj.id;
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

            LogUtil.WriteLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + " Update");


            #endregion


            bool update = dal.Update(model);


            #region 字表赋值

            if (update)
            {

                int check = 0;


                Model.goodsBomListItem modelItem = new Model.goodsBomListItem();
                DAL.goodsBomListItem item = new DAL.goodsBomListItem();


                LogUtil.WriteLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + " 字表赋值");

                bool delete = item.DeleteList(obj.id);


                for (int i = 0; i < itemList.Rows.Count; i++)
                {

                    modelItem.pId = obj.id;

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
                    logs.Events = "修改商品BOM清单：" +model.number;
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

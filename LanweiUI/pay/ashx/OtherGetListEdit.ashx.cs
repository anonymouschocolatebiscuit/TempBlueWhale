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
    public class OtherGetListEdit : IHttpHandler, IRequiresSessionState  //用Session必须引用IRequiresSessionState
    {

        public OtherGetDAL dal = new OtherGetDAL();


        public class OrderListModel<T>
        {

            #region 表头字段


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
            context.Response.ContentType = "text/plain";//返回格式

            if (context.Session["userInfo"] == null)
            {

                context.Response.Write("登陆超时，请重新登陆系统！");
                return;

            }
            BasePage basePage = new BasePage();
            if (!basePage.CheckPower("OtherGetListEdit"))
            {
                context.Response.Write("无此操作权限，请联系管理员！");
                return;
            }
            Users users = context.Session["userInfo"] as Users;


            StreamReader reader = new StreamReader(context.Request.InputStream);
            string strJson = HttpUtility.UrlDecode(reader.ReadToEnd());

            OrderListModel<OrderListItemModel> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderListModel<OrderListItemModel>>(strJson);

            //string sendDate = obj.sendDate.ToShortDateString();

            OrderListModel<OrderListItemModel> itemList = obj;



            #region 主表赋值

            dal.Id = obj.id;
            dal.ShopId = users.ShopId;
            dal.Number = "";
            dal.WlId = obj.venderId;
            dal.BizDate = obj.bizDate;
            dal.BkId = obj.bkId;

            dal.Remarks = obj.remarks.ToString();
            dal.MakeId = users.Id;
            dal.MakeDate = DateTime.Now;

            dal.Flag = "保存";


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
                    logs.Events = "修改其他付款单：" + obj.id.ToString();
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

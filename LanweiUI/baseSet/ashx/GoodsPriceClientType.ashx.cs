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


namespace Lanwei.Weixin.UI.baseSet.ashx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GoodsPriceClientType : IHttpHandler, IRequiresSessionState  //用Session必须引用IRequiresSessionState
    {

        public GoodsDAL dal = new GoodsDAL();
        
  
        public class OrderListModel<T>
        {

            #region 表头字段

        
          
            private int _goodsId;
            public int goodsId
            {
                get { return _goodsId; }
                set { _goodsId = value; }
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


       
        }

      
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";//返回格式

            if (context.Session["userInfo"] == null)
            {

                context.Response.Write("登陆超时，请重新登陆系统！");
                return;
 
            }
           
            //BasePage basePage = new BasePage();
            //if (!basePage.CheckPower("DiaoboListAdd"))
            //{
            //    context.Response.Write("无此操作权限，请联系管理员！");
            //    return;
            //}


            Users users = context.Session["userInfo"] as Users;

    
            StreamReader reader = new StreamReader(context.Request.InputStream);
            string strJson = HttpUtility.UrlDecode(reader.ReadToEnd());

            OrderListModel<OrderListItemModel> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderListModel<OrderListItemModel>>(strJson);
           
            //string sendDate = obj.sendDate.ToShortDateString();

            OrderListModel<OrderListItemModel> itemList = obj;

          

            #region 主表赋值

            //dal.ShopId = users.CorpId;
            //dal.Number = dal.GetBillNumberAuto();
            
            //dal.BizDate = obj.bizDate;
          
            //dal.Remarks = obj.remarks.ToString();
            //dal.MakeId = users.Id;
            //dal.MakeDate = DateTime.Now;

            //dal.Flag = "保存";


            #endregion


            //int pId = dal.Add();


            #region 字表赋值

            if (obj.goodsId > 0)
            {

                int check = 0;

                for (int i = 0; i < itemList.Rows.Count; i++)
                {


                    check = dal.AddClientTypePrice(obj.goodsId, itemList.Rows[i].TypeId, itemList.Rows[i].Price);


                }
                if (check > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.Users = users.Names;
                    logs.Events = "设置商品等级价格："+obj.goodsId.ToString();
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

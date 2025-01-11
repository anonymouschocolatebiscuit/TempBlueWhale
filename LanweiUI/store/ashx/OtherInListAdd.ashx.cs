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



namespace Lanwei.Weixin.UI.store.ashx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class OtherInListAdd : IHttpHandler, IRequiresSessionState  //用Session必须引用IRequiresSessionState
    {

        public OtherInDAL dal = new OtherInDAL();
        
  
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

       
            private int _typeId;
            public int typeId
            {
                get { return _typeId; }
                set { _typeId = value; }
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

            private int ckId;
            public int CkId
            {
                get { return ckId; }
                set { ckId = value; }
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
            if (!basePage.CheckPower("OtherInListAdd"))
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
            dal.BizDate = obj.bizDate;
        
            dal.Types = obj.typeId;
            dal.Remarks = obj.remarks.ToString();
            dal.MakeId = users.Id;
            dal.MakeDate = DateTime.Now;

            dal.Flag = "保存";


            #endregion


            int pId = dal.Add();


            #region 字表赋值

            if (pId > 0)
            {

                int check = 0;



                OtherInItemDAL item = new OtherInItemDAL();
                
                
                for (int i = 0; i < itemList.Rows.Count; i++)
                {


                   

                  

                    item.PId = pId;
                    item.GoodsId = itemList.Rows[i].GoodsId;
                    item.Num = itemList.Rows[i].Num;
                    item.Price = itemList.Rows[i].Price;
                    item.SumPrice = itemList.Rows[i].SumPrice;
                   
                    item.CkId = itemList.Rows[i].CkId;
                    item.Remarks = itemList.Rows[i].Remarks.ToString();

                    check = item.Add();





                }
                if (check > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = users.ShopId;
                    logs.Users = users.Names;
                    logs.Events = "新增其他入库单：" + dal.Number;
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

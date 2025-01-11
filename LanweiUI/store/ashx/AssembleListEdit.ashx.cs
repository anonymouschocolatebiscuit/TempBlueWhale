﻿using System;
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
    public class AssembleListEdit : IHttpHandler, IRequiresSessionState  //用Session必须引用IRequiresSessionState
    {

        public AssembleDAL dal = new AssembleDAL();
        
  
        public class OrderListModel<T>
        {

            #region 表头字段

            private int _id;
            public int id
            {
                get { return _id; }
                set { _id = value; }
            }

           
            private DateTime _bizDate;
            public DateTime bizDate
            {
                get { return _bizDate; }
                set { _bizDate = value; }
            }

            private decimal _fee;
            public decimal fee
            {
                get { return _fee; }
                set { _fee = value; }
            }


            private int _goodsId;
            public int goodsId
            {
                get { return _goodsId; }
                set { _goodsId = value; }
            }

            private decimal _num;
            public decimal num
            {
                get { return _num; }
                set { _num = value; }
            }

            private decimal _price;
            public decimal price
            {
                get { return _price; }
                set { _price = value; }
            }

            private int _ckId;
            public int ckId
            {
                get { return _ckId; }
                set { _ckId = value; }
            }

            private string _remarks;
            public string remarks
            {
                get { return _remarks; }
                set { _remarks = value; }
            }

            private string _remarksItem;
            public string remarksItem
            {
                get { return _remarksItem; }
                set { _remarksItem = value; }
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
            if (!basePage.CheckPower("AssembleListEdit"))
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

            dal.Id = obj.id;

            dal.ShopId = users.ShopId;

            dal.Number = "";
          
            dal.BizDate = obj.bizDate;
        
            dal.Fee = obj.fee;
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

                

                AssembleItemDAL item = new AssembleItemDAL();

                int deleteItem = item.Delete(obj.id);


                #region 增加组合的商品


                item.PId = obj.id;
                item.Types = 1;//要组合的商品，所以是入库，数量增加。
                item.GoodsId = obj.goodsId;
                item.Num =obj.num;
                item.Price = obj.price;
                item.SumPrice = obj.price * obj.num;

                item.CkId = obj.ckId;
                item.Remarks = obj.remarksItem;

                check = item.Add();

                #endregion


                for (int i = 0; i < itemList.Rows.Count; i++)
                {

                    item.PId = obj.id;
                    item.Types = -1;//被组合的商品，所以是出库，数量减少。
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
                    logs.Users = users.Names;
                    logs.Events = "修改商品组合单：" + obj.id.ToString();
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

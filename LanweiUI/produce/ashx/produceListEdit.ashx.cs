using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.SessionState;//用Session必须引用
using Lanwei.Weixin.DAL;
using Lanwei.Weixin.UI.src;
using Lanwei.Weixin.Common;
using Lanwei.Weixin.Model;

using System.IO;
using System.Web.Script.Serialization;

using System.Runtime.Serialization;
using System.Text;

namespace Lanwei.Weixin.UI.produce.ashx
{
    /// <summary>
    /// produceListAdd 的摘要说明
    /// </summary>
    public class produceListEdit : IHttpHandler, IRequiresSessionState  //用Session必须引用IRequiresSessionState
    {

        public ProduceListDAL dal = new ProduceListDAL();

     
        public class OrderListModel<T>
        {

            #region 成员属性

            private int id;
            public int Id
            {
                get { return id; }
                set { id = value; }
            }

            private int shopId;
            public int ShopId
            {
                get { return shopId; }
                set { shopId = value; }
            }

            private string number;
            public string Number
            {
                get { return number; }
                set { number = value; }
            }

            private string typeName;
            public string TypeName
            {
                get { return typeName; }
                set { typeName = value; }
            }

            private DateTime dateStart;
            public DateTime DateStart
            {
                get { return dateStart; }
                set { dateStart = value; }
            }

            private DateTime dateEnd;
            public DateTime DateEnd
            {
                get { return dateEnd; }
                set { dateEnd = value; }
            }


            private string orderNumber;
            public string OrderNumber
            {
                get { return orderNumber; }
                set { orderNumber = value; }
            }

            private string remarks;
            public string Remarks
            {
                get { return remarks; }
                set { remarks = value; }
            }

            private decimal num;
            public decimal Num
            {
                get { return num; }
                set { num = value; }
            }





            private int goodsId;
            public int GoodsId
            {
                get { return goodsId; }
                set { goodsId = value; }
            }

            private int makeId;
            public int MakeId
            {
                get { return makeId; }
                set { makeId = value; }
            }

            private DateTime makeDate;
            public DateTime MakeDate
            {
                get { return makeDate; }
                set { makeDate = value; }
            }



            private string flag;
            public string Flag
            {
                get { return flag; }
                set { flag = value; }
            }




            #endregion

            /// <summary>
            /// 商品明细
            /// </summary>
            public List<OrderListItemModelBom> _Rows;
            public List<OrderListItemModelBom> Rows
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

            private int processId;
            public int ProcessId
            {
                get { return processId; }
                set { processId = value; }
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



            private string remarks;
            public string Remarks
            {
                get { return remarks; }
                set { remarks = value; }
            }

        }

        [Serializable]
        public class OrderListItemModelBom
        {
            #region 成员属性

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

            private int itemId;
            public int ItemId
            {
                get { return itemId; }
                set { itemId = value; }
            }

            private int goodsId;
            public int GoodsId
            {
                get { return goodsId; }
                set { goodsId = value; }
            }

            private float numBom;
            public float NumBom
            {
                get { return numBom; }
                set { numBom = value; }
            }

            private float rate;
            public float Rate
            {
                get { return rate; }
                set { rate = value; }
            }

            private float num;
            public float Num
            {
                get { return num; }
                set { num = value; }
            }


            //private int ckId;
            //public int CkId
            //{
            //    get { return ckId; }
            //    set { ckId = value; }
            //}

            private string remarks;
            public string Remarks
            {
                get { return remarks; }
                set { remarks = value; }
            }


            #endregion
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
          
            //if (!basePage.CheckPower("SalesReceiptListAdd"))
            //{
            //    context.Response.Write("无此操作权限，请联系管理员！");
            //    return;
            //}




            Users users = context.Session["userInfo"] as Users;


            StreamReader reader = new StreamReader(context.Request.InputStream);
            string strJson = HttpUtility.UrlDecode(reader.ReadToEnd());

            OrderListModel<OrderListItemModelBom> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderListModel<OrderListItemModelBom>>(strJson);



            OrderListModel<OrderListItemModelBom> itemList = obj;


            




            #region 主表赋值

          
            dal.ShopId = users.ShopId;
            dal.TypeName = obj.TypeName;

            dal.OrderNumber = obj.OrderNumber;
            dal.DateStart = obj.DateStart;
            dal.DateEnd = obj.DateEnd;
            dal.GoodsId = obj.GoodsId;
            dal.Remarks = obj.Remarks.ToString();
            dal.Num = obj.Num;
            dal.MakeId = users.Id;
            dal.MakeDate = DateTime.Now;

       


            dal.Flag = "保存";
            dal.Id = obj.Id;

            #endregion


            int pId = dal.Update();


            #region 字表赋值

            if (pId > 0)
            {

                int check = 0;

                #region 工序部分

                //ProduceListItemDAL item = new ProduceListItemDAL();

                //item.Delete(obj.Id);

                //for (int i = 0; i < itemList.Rows.Count; i++)
                //{





                //    item.PId = obj.Id;
                //    item.ProcessId = itemList.Rows[i].ProcessId;
                //    item.Num = itemList.Rows[i].Num;

                  
                //    item.Price = itemList.Rows[i].Price;


                //    item.SumPrice = itemList.Rows[i].SumPrice;
                  

                //    item.Remarks = itemList.Rows[i].Remarks.ToString();
                  

                //    check = item.Add();





                //}

                #endregion

                #region bom部分

                ProduceListItemBomDAL item = new ProduceListItemBomDAL();

                item.Delete(obj.Id);

                for (int i = 0; i < itemList.Rows.Count; i++)
                {

                    item.PId = obj.Id;
                    item.ItemId = i + 1;

                    item.GoodsId = itemList.Rows[i].GoodsId;

                    item.Num = itemList.Rows[i].Num;


                    item.NumBom = itemList.Rows[i].NumBom;


                    item.Rate = itemList.Rows[i].Rate;


                    item.CkId = 0;// itemList.Rows[i].CkId;


                    item.Remarks = itemList.Rows[i].Remarks.ToString();


                    check = item.Add();


                }


                #endregion

                LogsDAL logs = new LogsDAL();
                    logs.ShopId = users.ShopId;
                    logs.Users = users.Names;
                    logs.Events = "修改生产计划：" + dal.Number;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    context.Response.Write("操作成功！");

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
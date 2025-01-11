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

namespace Lanwei.Weixin.UI.store.ashx
{
    /// <summary>
    /// Pandian 的摘要说明
    /// </summary>
    public class Pandian : IHttpHandler, IRequiresSessionState  //用Session必须引用IRequiresSessionState
    {
        OtherInDAL inDAL = new OtherInDAL();
        OtherOutDAL outDAL = new OtherOutDAL();


        public class OrderListModel<T>
        {

            #region 表头字段


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

            private decimal sumNum;
            public decimal SumNum
            {
                get { return sumNum; }
                set { sumNum = value; }
            }

            private decimal sumNumPD;
            public decimal SumNumPD
            {
                get { return sumNumPD; }
                set { sumNumPD = value; }
            }

            private decimal sumNumPK;
            public decimal SumNumPK
            {
                get { return sumNumPK; }
                set { sumNumPK = value; }
            }


            private int ckId;
            public int CkId
            {
                get { return ckId; }
                set { ckId = value; }
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
            if (!basePage.CheckPower("Pandian"))
            {
                context.Response.Write("无此操作权限，请联系管理员！");
                return;
            }
            Users users = context.Session["userInfo"] as Users;


            StreamReader reader = new StreamReader(context.Request.InputStream);
            string strJson = HttpUtility.UrlDecode(reader.ReadToEnd());

            OrderListModel<OrderListItemModel> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderListModel<OrderListItemModel>>(strJson);



            OrderListModel<OrderListItemModel> itemList = obj;


            bool hasMore = false;//有盘赢标识
            bool hasDes = false;


            string result = "操作成功！";

            #region 遍历盘盈盘亏情况

            for (int i = 0; i < itemList.Rows.Count; i++)
            {
                int goodsId = itemList.Rows[i].GoodsId;
                int ckId = itemList.Rows[i].CkId;


                decimal numPd = itemList.Rows[i].SumNumPD;

                decimal numSystem = itemList.Rows[i].SumNum;

                decimal disNum = numSystem - numPd;//系统库存-盘点库存，若大于0，那证明是盘亏，小于0，那是盘赢

                if (numSystem > numPd)//盘亏
                {

                    hasDes = true;

                }

                if (numSystem < numPd)//盘盈
                {
                    hasMore = true;

                }

            }

            #endregion

            #region 有盘赢标识

            if (hasMore)//有盘赢标识
            {
                string numberIn = inDAL.GetBillNumberAuto(users.ShopId);
               
                result += " 生成盘盈入库单：" + numberIn;

                inDAL.ShopId = users.ShopId;
                inDAL.Number = numberIn;

                inDAL.BizDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                inDAL.BizId = users.Id;
                inDAL.MakeId = users.Id;
                inDAL.Types = -1;
                inDAL.WlId = 0;



                inDAL.Remarks = obj.remarks;

                inDAL.MakeDate = DateTime.Now;

                inDAL.Flag = "保存";

                int pId = inDAL.Add();
                if (pId > 0)
                {

                    LogsDAL logs = new LogsDAL();
                    logs.Users = users.Names;
                    logs.Events = result;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();


                    for (int i = 0; i < itemList.Rows.Count; i++)
                    {
                        int goodsId = itemList.Rows[i].GoodsId;
                        int ckId = itemList.Rows[i].CkId;


                        decimal numPd = itemList.Rows[i].SumNumPD;

                        decimal numSystem = itemList.Rows[i].SumNum;

                        decimal disNum = numSystem - numPd;//系统库存-盘点库存，若大于0，那证明是盘亏，小于0，那是盘赢

                        if (numSystem < numPd)//盘盈
                        {
                            OtherInItemDAL item = new OtherInItemDAL();
                            item.GoodsId = goodsId;
                            item.Num = disNum * (-1);
                            item.CkId = ckId;
                            item.Price = 0;
                            item.SumPrice = 0;
                            item.Remarks = "盘盈入库";

                            item.PId = pId;
                            item.Add();

                        }

                    }
                }
            }

            #endregion

            #region 有盘亏标识

            if (hasDes)//有盘亏标识
            {

                string numberOut = outDAL.GetBillNumberAuto(users.ShopId);

                result += " 生成盘亏出库单：" + numberOut;

                outDAL.ShopId = users.ShopId;
                outDAL.Number = numberOut;

                outDAL.BizDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                outDAL.BizId = users.Id;
                outDAL.MakeId = users.Id;
                outDAL.Types = -1;
                outDAL.WlId = 0;



                outDAL.Remarks = obj.remarks;

                outDAL.MakeDate = DateTime.Now;

                outDAL.Flag = "保存";

                int pId = outDAL.Add();
                if (pId > 0)
                {

                    LogsDAL logs = new LogsDAL();
                    logs.Users = users.Names;
                    logs.Events = result;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                    for (int i = 0; i < itemList.Rows.Count; i++)
                    {
                        int goodsId = itemList.Rows[i].GoodsId;
                        int ckId = itemList.Rows[i].CkId;


                        decimal numPd = itemList.Rows[i].SumNumPD;

                        decimal numSystem = itemList.Rows[i].SumNum;


                        decimal disNum = numSystem - numPd;//系统库存-盘点库存，若大于0，那证明是盘亏，小于0，那是盘赢

                        if (numSystem > numPd)//盘亏了
                        {
                            OtherOutItemDAL item = new OtherOutItemDAL();
                            item.GoodsId = goodsId;
                            item.Num = disNum;
                            item.CkId = ckId;
                            item.Price = 0;
                            item.SumPrice = 0;
                            item.Remarks = "盘亏出库";

                            item.PId = pId;
                            item.Add();

                        }

                    }
                }
            }

            #endregion

            context.Response.Write(result);







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
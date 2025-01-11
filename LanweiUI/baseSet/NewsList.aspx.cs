using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Linq;

using System.Web.Script.Serialization;
using Lanwei.Weixin.DAL;
using Lanwei.Weixin.Common;

using Lanwei.Weixin.UI.src;

namespace Lanwei.Weixin.UI.baseSet
{
    public partial class NewsList : BasePage
    {
        public NewsDAL dal = new NewsDAL();


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                //if (!CheckPower("QuestionList"))
                //{

                //    Response.Redirect("../OverPower.htm");

                   
                //}

              

                this.txtDateStart.Text = DateTime.Now.AddYears(-1).ToShortDateString();
                this.txtDateEnd.Text = DateTime.Now.ToShortDateString();
            }


            if (Request.Params["Action"] == "GetDataList")
            {
                string keys = "";

                DateTime start = DateTime.Now.AddDays(-7);

                DateTime end = DateTime.Now;

                GetDataList(keys, start, end,"全部");
                Response.End();
            }

            if (Request.Params["Action"] == "GetDataListSearch")
            {

                string keys = Request.Params["keys"].ToString();

                string flag = Request.Params["flag"].ToString();

                DateTime start = Convert.ToDateTime(Request.Params["start"].ToString());

                DateTime end = Convert.ToDateTime(Request.Params["end"].ToString());

                GetDataList(keys, start, end,flag);
                Response.End();
            }


            if (Request.Params["Action"] == "delete")
            {
                string idString = Request.Params["idString"].ToString();
                DeleteRow(idString);
                Response.End();
            }

          

        }
        void GetDataList(string key, DateTime start, DateTime end,string flag)
        {
            DataSet ds = dal.GetQuestionInfo(LoginUser.ShopId,key,start,end,flag);

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    title = ds.Tables[0].Rows[i]["title"].ToString(),
                    typeName = ds.Tables[0].Rows[i]["typeName"].ToString(),


                 
                    contents = ds.Tables[0].Rows[i]["contents"].ToString(),

             
                    makeDate = ds.Tables[0].Rows[i]["makeDate"].ToString(),

                    hot = ds.Tables[0].Rows[i]["hot"].ToString()

                });
            }
            var griddata = new { Rows = list };
            string s = new JavaScriptSerializer().Serialize(griddata);
            Response.Write(s);
        }

        void DeleteRow(string idString)
        {
            if (Session["userInfo"] != null)
            {

                //if (!CheckPower("QuestionListDelete"))
                //{
                //    Response.Write("无此操作权限！");

                //    return;
                //}


                int del = dal.Delete(idString);
                if (del > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;
                    logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "删除新闻-ID：" + idString.ToString();
                    logs.Ip = Request.UserHostAddress.ToString();
                    logs.Add();

                    Response.Write("删除成功！");

                }
                else
                {
                    Response.Write("删除失败！");
                }
            }
            else
            {
                Response.Write("登录超时，请重新登陆！");
            }

        }



    }
}

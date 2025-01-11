using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Lanwei.Weixin.DAL;
using Lanwei.Weixin.Common;
using Lanwei.Weixin.UI.src;
using System.IO;

using System.Collections.Generic;
using System.Web.Script.Serialization;


namespace Lanwei.Weixin.UI.baseSet
{
    public partial class DatabaseBackUp :BasePage// System.Web.UI.Page
    {
        public SystemSetDAL dal = new SystemSetDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!CheckPower("DatabaseBackUp"))
                {
                    Response.Redirect("../OverPower.htm");
                }


                string databaseName = dal.GetDatabaseName();

                this.txtNames.Text = databaseName;

                this.txtFileName.Text = databaseName + DateTime.Now.ToString("yyyyMMddhhmmss") + ".bak";

                this.txtPath.Text = Server.MapPath("database/");

            }

            if (Request.Params["Action"] == "GetDataList")
            {
                GetDataList();
                Response.End();
            }

            if (Request.Params["Action"] == "DataBackup")
            {
                string database = Request.Params["database"].ToString();
                string paths = Request.Params["paths"].ToString();

                string names = Request.Params["names"].ToString();

                this.DataBackup(database,paths,names);
                
                Response.End();
            }

            if (Request.Params["Action"] == "DataBackupCom")
            {
                string paths = Request.Params["paths"].ToString();

                this.DataBackupCom(paths);

                Response.End();
            }

            if (Request.Params["Action"] == "delete")
            {
                int id =ConvertTo.ConvertInt(Request.Params["id"].ToString());

                this.DeleteRow(id);

                Response.End();
            }




        }

        void GetDataList()
        {
            DataSet ds = dal.GetDataBackList();

            IList<object> list = new List<object>();
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                list.Add(new
                {
                    id = ds.Tables[0].Rows[i]["id"].ToString(),
                    backName = ds.Tables[0].Rows[i]["backName"].ToString(),
                    makeId = ds.Tables[0].Rows[i]["makeId"].ToString(),
                    makeName = ds.Tables[0].Rows[i]["makeName"].ToString(),
                    makeDate = ds.Tables[0].Rows[i]["makeDate"].ToString()
                  

                });

            }
            var griddata = new { Rows = list };

            string s = new JavaScriptSerializer().Serialize(griddata);//传给grid的时候才要
            Response.Write(s);
        }


        void DeleteRow(int id)
        {
            if (Session["userInfo"] != null)
            {
                int del = dal.Delete(id);
                if (del > 0)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = LoginUser.ShopId;logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                    logs.Events = "删除数据库备份记录-ID：" + id.ToString();
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



        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        void DataBackup(string database, string path,string names)
        {
            int back = this.dal.DataBackup(database, path);

            int list = dal.AddDataBackList(LoginUser.Id,names);
           

            LogsDAL logs = new LogsDAL();
            logs.ShopId = LoginUser.ShopId;logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
            logs.Events = "备份数据库：" + names;
            logs.Ip = Request.UserHostAddress.ToString();
            logs.Add();

            Response.Write("ok");

        }

        /// <summary>
        /// 判断备份成功与否
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        void DataBackupCom(string names)
        {
            int num = this.dal.DataBackupCom(names);

            if (num == 1)
               
               Response.Write("数据备份成功！");
            
            else
             
               Response.Write("数据库备份失败，请联系管理员！");


        }

 

    }
}

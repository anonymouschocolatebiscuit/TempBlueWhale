using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lanwei.Weixin.Common;
using Lanwei.Weixin.DAL;
using System.Data;
using Lanwei.Weixin.DBUtility;

using System.Web.Script.Serialization;

using System.Net;
using System.Xml;


namespace Lanwei.Weixin.UI
{
    public partial class index : System.Web.UI.Page
    {
     
        public UserDAL dal = new UserDAL();
        public LogsDAL logs = new LogsDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //

                //this.txtUserName.Text = "lanwei";
                //this.txtPassword.TextMode = TextBoxMode.SingleLine;
                //this.txtPassword.Text = "123456";

                string ipString = Request.UserHostAddress.ToString();

                //使用          


                if (ipString == "127.0.0.1" || ipString == "" || ipString == "::1")
                {
                    return;
                }

                DetailAddress detail = GetDetailAddressByBaiduAPI(ipString);



                string contents = "蓝微·云ERP有访客，IP地址：" + ipString + " " + detail.content.address;

              



            }
        }

        #region 获取IP地址


        public static DetailAddress GetDetailAddressByBaiduAPI(string IPAddress)
        {
            System.Net.HttpWebRequest request;
            System.Net.HttpWebResponse response;
            string url = string.Format("http://api.map.baidu.com/location/ip?ak=HApWIBI5q1in9bCuVHLBjWeGZLNqwlxL&coor=bd09ll&ip={0}", IPAddress);
            try
            {
                request = HttpWebRequest.Create(url) as System.Net.HttpWebRequest;
                response = request.GetResponse() as System.Net.HttpWebResponse;
                using (System.IO.Stream stream = response.GetResponseStream())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8))
                    {
                        string Data = sr.ReadToEnd();
                        System.Web.Script.Serialization.JavaScriptSerializer serializer = new JavaScriptSerializer();
                        DetailAddress detail = serializer.Deserialize<DetailAddress>(Data);
                        return detail;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        [Serializable]  //添加序列化特性
        public class DetailAddress
        {
            public string address { get; set; }
            public DetailContent content { get; set; }
            
        }

        [Serializable]
        public class DetailContent
        {

            public string address { get; set; }
            public Address_detail address_detail { get; set; }
            public int status { get; set; }
           
        }

        [Serializable]
        public class Address_detail
        {
            public string city { get; set; }
            public string city_code { get; set; }
            public string district { get; set; }
            public string province { get; set; }           
            public string street { get; set; }
            public string street_number { get; set; }
        }

        #endregion


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
           // this.DelCookeis();

            #region 合法性验证


            if (this.txtUserName.Text == "")
            {
                MessageBox.Show(this, "请输入手机号！");
                this.txtUserName.Focus();
                return;
            }

            if (this.txtUserName.Text != "lanwei" && this.txtUserName.Text.Length != 11)
            {
                MessageBox.Show(this, "请输入11位手机号！");

                this.txtUserName.Focus();
                return;
            }
            else
            {
                if (this.txtUserName.Text != "lanwei" && !Regexlib.IsValidMobile(this.txtUserName.Text))
                {
                    MessageBox.Show(this, "请输入正确的手机号！");
                    this.txtUserName.Focus();
                    return;
                }
            }


            if (this.txtPassword.Text == "")
            {
                MessageBox.Show(this, "请输入密码！");
                this.txtPassword.Focus();
                return;


            }


            string yzm = this.txtYZM.Text;

            if (yzm == "")
            {
                MessageBox.Show(this, "请输入验证码！");
                this.txtYZM.Focus();
                return;


            }



            if (yzm != Session["verify"].ToString())//
            {

                MessageBox.Show(this, "验证码输入错误，请重试！");
                return;

            }



            #endregion


            if (Page.IsValid)
            {
                string userName = StrHelper.ConvertSql(this.txtUserName.Text.ToString());
                string userPwd = StrHelper.ConvertSql(this.txtPassword.Text.ToString());

                Lanwei.Weixin.Model.Users user = dal.getByUserName(userName);

                if (user == null)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = 0;
                    logs.Users = this.txtUserName.Text;
                    logs.Events = "手机号码不存在,密码：" + this.txtPassword.Text + " 验证码：" + this.txtYZM.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    logs.Add();

                   


                    MessageBox.Show(this, "手机号码不存在，请重试！");
                    return;
                }

                if (user.Pwd != userPwd)
                {
                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = 0;
                    logs.Users = this.txtUserName.Text;
                    logs.Events = "手机号或密码错误,密码：" + this.txtPassword.Text + " 验证码：" + this.txtYZM.Text;
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();

                    logs.Add();


                   


                    MessageBox.Show(this, "手机号或密码错误，请重试！");
                    return;
                }

                if (user.Flag == "启用")
                {

                   // 


                    Session["userInfo"] = user;
                    Session["login"] = "员工";
                    Session.Timeout = 45;

                    LogsDAL logs = new LogsDAL();
                    logs.ShopId = user.ShopId;
                    logs.Users = user.Phone + "-" + user.Names;
                    logs.Events = "登陆系统--来index.aspx页面";
                    logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                  
                    logs.Add();


                   
                    //写入Cookies
                    Utils.WriteCookie("shopId", user.ShopId.ToString(), 14400);
                    Utils.WriteCookie("shopName", user.ShopName, 14400);
                    Utils.WriteCookie("userName", user.Names, 14400);
                    Utils.WriteCookie("roleName", user.RoleName, 14400);
                    Utils.WriteCookie("deptName", user.DeptName, 14400);
                    Utils.WriteCookie("phone", user.Phone, 14400);
                    Utils.WriteCookie("pwd", user.Pwd, 14400);

                    Utils.WriteCookie("WeixinId", user.WeixinId, 14400);


                    string contents = "蓝微·云ERP有访客，IP地址：" + Request.UserHostAddress.ToString();
                    contents += "登陆系统" + userName + "--来自index.aspx页面,密码：" + this.txtPassword.Text + " 验证码：" + this.txtYZM.Text;

                    contents += " shopName:" + user.ShopName + " userName:" + user.Names;


                  
                   



                    Response.Redirect("Lanweiyun.com.aspx");

                }
                if (user.Flag == "禁用")
                {
                    MessageBox.Show(this, "您的账号已被禁用，请联系管理员！");
                    Response.Redirect("index.aspx");

                }

            }


        }

        #region  ##删除cookies
        ///<summary>
        /// 删除cookies
        ///</summary>
        public void DelCookeis()
        {
            foreach (string cookiename in Request.Cookies.AllKeys)
            {
                HttpCookie cookies = Request.Cookies[cookiename];
                if (cookies != null)
                {
                    cookies.Expires = DateTime.Today.AddDays(-1);
                    Response.Cookies.Add(cookies);
                    Request.Cookies.Remove(cookiename);
                }
            }
        }
        #endregion
    }
}
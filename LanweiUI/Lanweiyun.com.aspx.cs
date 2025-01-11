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

using Lanwei.Weixin.UI.src;

using Lanwei.Weixin.DBUtility;
using Lanwei.Weixin.Common;

namespace Lanwei.Weixin.UI
{
    public partial class Lanweiyun : BasePage
    {
        public string companyName = "";
        public string userName = "";
        public string roleName = "";
        public string deptName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                companyName = Utils.GetCookie("shopName").ToString();

                userName = LoginUser.Names;
                roleName = LoginUser.RoleName;
                deptName = LoginUser.DeptName;

                LogUtil.WriteLog("Lanweiyun.aspx 来到Lanweiyun页面 userId=" + LoginUser.LoginName + " conStr:" + SQLHelper.ConStr);



            }
        }
    }
}

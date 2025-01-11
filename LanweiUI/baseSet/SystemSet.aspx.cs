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
using Microsoft.Win32;
using System.Text;
using System.Management;
using System.Xml;



namespace LanweiWeb.BaseSet
{
    public partial class SystemSet : BasePage
    {
        public SystemSetDAL dal = new SystemSetDAL();
      

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                //不存在权限、或者不是管理员
                //if (!CheckPower("SystemSet"))
                //{
                //    Response.Redirect("../OverPower.htm");
                //}
               

          
                this.Bind();
            }
        }

        public void Bind()
        {
            string isWhere = " shopId='" + LoginUser.ShopId + "' ";
            DataSet ds = dal.GetList(isWhere);

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.TextBox1.Text = ds.Tables[0].Rows[0]["company"].ToString();
                this.TextBox2.Text = ds.Tables[0].Rows[0]["address"].ToString();
                this.TextBox3.Text = ds.Tables[0].Rows[0]["tel"].ToString();
                this.TextBox4.Text = ds.Tables[0].Rows[0]["fax"].ToString();
                this.TextBox5.Text = ds.Tables[0].Rows[0]["postCode"].ToString();

                this.txtCorpIdQY.Text = ds.Tables[0].Rows[0]["CorpIdQY"].ToString();
                this.txtCorpSecretQY.Text = ds.Tables[0].Rows[0]["CorpSecretQY"].ToString();
                this.txtCorpIdDD.Text = ds.Tables[0].Rows[0]["CorpIdDD"].ToString();
                this.txtCorpSecretDD.Text = ds.Tables[0].Rows[0]["CorpSecretDD"].ToString();

                this.txtUserSecret.Text = ds.Tables[0].Rows[0]["UserSecret"].ToString();
                this.txtCheckInSecret.Text = ds.Tables[0].Rows[0]["CheckInSecret"].ToString();
                this.txtApplySecret.Text = ds.Tables[0].Rows[0]["ApplySecret"].ToString();


                this.txtSecretCheckIn.Text = ds.Tables[0].Rows[0]["SecretCheckIn"].ToString();
                this.txtSecretApply.Text = ds.Tables[0].Rows[0]["SecretApply"].ToString();
                this.txtSecretBuy.Text = ds.Tables[0].Rows[0]["SecretBuy"].ToString();
                this.txtSecretSales.Text = ds.Tables[0].Rows[0]["SecretSales"].ToString();
                this.txtSecretStore.Text = ds.Tables[0].Rows[0]["SecretStore"].ToString();
                this.txtSecretFee.Text = ds.Tables[0].Rows[0]["SecretFee"].ToString();
                this.txtSecretReport.Text = ds.Tables[0].Rows[0]["SecretReport"].ToString();




                string checkBill = ds.Tables[0].Rows[0]["useCheck"].ToString();
                if (checkBill == "1")
                {
                    this.cbBillCheckList.Checked = true;
                }
                else
                {
                    this.cbBillCheckList.Checked = false;
                }

                string checkNum = ds.Tables[0].Rows[0]["checkNum"].ToString();

                if (checkNum == "1")
                {
                    this.cbStoreNum.Checked = true;
                }
                else
                {
                    this.cbStoreNum.Checked = false;
                }


                this.txtFieldA.Text = ds.Tables[0].Rows[0]["FieldA"].ToString();
                this.txtFieldB.Text = ds.Tables[0].Rows[0]["FieldB"].ToString();
                this.txtFieldC.Text = ds.Tables[0].Rows[0]["FieldC"].ToString();
                this.txtFieldD.Text = ds.Tables[0].Rows[0]["FieldD"].ToString();

                string printLogo = ds.Tables[0].Rows[0]["printLogo"].ToString();

                if (printLogo == "是")
                {
                    this.cbPrintLogo.Checked = true;
                }
                else
                {
                    this.cbPrintLogo.Checked = false;
                }


                string printZhang = ds.Tables[0].Rows[0]["printZhang"].ToString();

                if (printZhang == "是")
                {
                    this.cbPrintZhang.Checked = true;
                }
                else
                {
                    this.cbPrintZhang.Checked = false;
                }
            }


          
        }


        

        protected void btnSave_Click(object sender, EventArgs e)
        {

            dal.ShopId = LoginUser.ShopId;

            dal.Company = this.TextBox1.Text;
            dal.Address = this.TextBox2.Text;
            dal.Tel = this.TextBox3.Text;
            dal.Fax = this.TextBox4.Text;
            dal.PostCode = this.TextBox5.Text;

            dal.FieldA = this.txtFieldA.Text;
            dal.FieldB = this.txtFieldB.Text;
            dal.FieldC = this.txtFieldC.Text;
            dal.FieldD = this.txtFieldD.Text;

            dal.CorpIdQY = this.txtCorpIdQY.Text;
            dal.CorpSecretQY = this.txtCorpSecretQY.Text;
            dal.CorpIdDD = this.txtCorpIdDD.Text;
            dal.CorpSecretDD = this.txtCorpSecretDD.Text;

            dal.UserSecret = this.txtUserSecret.Text;
            dal.CheckInSecret = this.txtCheckInSecret.Text;
            dal.ApplySecret = this.txtApplySecret.Text;

            dal.SecretCheckIn = this.txtSecretCheckIn.Text;
            dal.SecretApply = this.txtSecretApply.Text;
            dal.SecretBuy = this.txtSecretBuy.Text;
            dal.SecretSales = this.txtSecretSales.Text;
            dal.SecretStore = this.txtSecretStore.Text;
            dal.SecretFee = this.txtSecretFee.Text;
            dal.SecretReport = this.txtSecretReport.Text;



            if (this.cbBillCheckList.Checked == true)
            {
                dal.UseCheck = 1;
            }
            else
            {
                dal.UseCheck = 0;
            }

            if (this.cbStoreNum.Checked == true)
            {
                dal.CheckNum = 1;
            }
            else
            {
                dal.CheckNum = 0;
            }

            if (this.cbPrintLogo.Checked == true)
            {
                dal.PrintLogo = "是";
            }
            else
            {
                dal.PrintLogo = "否";
            }

            if (this.cbPrintZhang.Checked == true)
            {
                dal.PrintZhang = "是";
            }
            else
            {
                dal.PrintZhang = "否";
            }


            int add = dal.UpdateSample();
            if (add > 0)
            {
                MessageBox.Show(this.Page,"操作成功！");
            }


            this.Bind();

        }
    
    }
}

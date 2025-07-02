using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;
using System;
using System.Data;



namespace BlueWhale.UI.baseSet
{
    public partial class SystemSet : BasePage
    {
        public SystemSetDAL dal = new SystemSetDAL();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                // Unauthorized
                // Might need later, keeping the comment for the moment
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

                if (printLogo == "Yes")
                {
                    this.cbPrintLogo.Checked = true;
                }
                else
                {
                    this.cbPrintLogo.Checked = false;
                }

                string printStamp = ds.Tables[0].Rows[0]["printZhang"].ToString();

                if (printStamp == "Yes")
                {
                    this.cbPrintStamp.Checked = true;
                }
                else
                {
                    this.cbPrintStamp.Checked = false;
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
                dal.PrintLogo = "Yes";
            }
            else
            {
                dal.PrintLogo = "No";
            }

            if (this.cbPrintStamp.Checked == true)
            {
                dal.PrintStamp = "Yes";
            }
            else
            {
                dal.PrintStamp = "No";
            }

            int add = dal.UpdateSample();
            if (add > 0)
            {
                MessageBox.Show(this.Page, "Execute successfully!");
            }

            this.Bind();
        }
    }
}
using System;
using System.Data;
using System.Data.OleDb;
using BlueWhale.Common;
using BlueWhale.DAL;
using BlueWhale.UI.src;

namespace BlueWhale.UI.BaseSet
{
    public partial class GoodsListExcel : BasePage
    {
        public GoodsDAL goodsDAL = new GoodsDAL();
        public GoodsTypeDAL typeDAL = new GoodsTypeDAL();
        public UnitDAL unitDAL = new UnitDAL();
        public InventoryDAL ckDAL = new InventoryDAL();
        public GoodsBrandDAL brandDAL = new GoodsBrandDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!CheckPower("GoodsListExcel"))
                {
                    Response.Redirect("../OverPower.htm");
                }
            }
        }

        protected void lbtnDownExcel_Click(object sender, EventArgs e)
        {
            string path = "excel/excel.xls";

            try
            {
                System.IO.FileInfo file = new System.IO.FileInfo(Server.MapPath(path));

                Response.Clear();
                Response.Charset = "GB2312";
                Response.ContentEncoding = System.Text.Encoding.UTF8;

                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(file.Name));
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.WriteFile(file.FullName);
                Response.End();
            }
            catch (Exception) { }
        }

        protected void lbtnDownExample_Click(object sender, EventArgs e)
        {
            string path = "excel/excel.xls";

            try
            {
                System.IO.FileInfo file = new System.IO.FileInfo(Server.MapPath(path));

                Response.Clear();
                Response.Charset = "GB2312";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(file.Name));
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.WriteFile(file.FullName);
                Response.End();
            }
            catch (Exception) { }
        }

        public int TotalNumAll = 0;
        public int TotalNumOK = 0;
        public int TotalNumNo = 0;

        protected void btnExcelTo_Click(object sender, EventArgs e)
        {
            if (!CheckPower("GoodsListExcel"))
            {
                MessageBox.Show(this, "You do not have this permission!");
                return;
            }

            if (fload.HasFile)
            {
                string fileExt = System.IO.Path.GetExtension(fload.FileName);
                if (fileExt == ".xls" || fileExt == ".xlsx")
                {
                    string fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + fileExt;
                    string oPath = Server.MapPath("excel/" + fileName);

                    try
                    {
                        fload.PostedFile.SaveAs(oPath);

                        //ExcelHelper help = new ExcelHelper();
                        //DataSet ds = help.ExcelToDataSet(filepath);

                        DataSet ds = this.ExcelDataSource(fileName);
                        DataTable dt = ds.Tables[0];

                        if (dt != null && dt.Rows.Count > 0)
                        {

                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                if (dt.Columns[i].ColumnName.IndexOf('F') >= 0)
                                {
                                    if (dt.Columns[i].ColumnName.Trim() != "F1")
                                    {
                                        dt.Columns.Remove(dt.Columns[i]);
                                        i--;
                                    }
                                }
                            }

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (DBNull.Value == dt.Rows[i][0] || dt.Rows[0][0].ToString() == "")
                                {
                                    dt.Rows.Remove(dt.Rows[0]);
                                }
                            }
                            dt.AcceptChanges();
                        }

                        this.Label1.Text = this.DataTableToSql(dt);
                        this.Label1.Visible = true;
                        LogsDAL logs = new LogsDAL();
                        logs.ShopId = LoginUser.ShopId; logs.Users = LoginUser.Phone + "-" + LoginUser.Names;
                        logs.Events = "Import Good Details：" + this.Label1.Text;
                        logs.Ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                        logs.Add();

                        //this.GridView1.DataSource = dt;
                        //this.GridView1.DataBind();

                        System.IO.File.Delete(oPath);
                    }
                    catch (Exception ex)
                    {
                        this.Label1.Text = ex.Message;
                        this.Label1.Visible = true;
                    }
                }
                else
                {
                    this.Label1.Text = "Error!!!";
                }
            }
        }

        public DataSet ExcelDataSource(string fileName)
        {
            string oPath = Server.MapPath("excel/" + fileName);
            string strConn;
            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + oPath + ";Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(strConn);
            OleDbDataAdapter oada = new OleDbDataAdapter("select * from [商品$]", strConn);
            DataSet ds = new DataSet();
            oada.Fill(ds);
            conn.Close();
            return ds;
        }

        public string DataTableToSql(DataTable dt)
        {
            int goodsId = 0;
            int goodsNum = 0;
            int kcNum = 0;

            TotalNumAll = dt.Rows.Count;

            #region Loop DataTable

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string code = dt.Rows[i]["Good Code"].ToString();
                string names = dt.Rows[i]["Good Name"].ToString();
                string barcode = dt.Rows[i]["Good Barcode"].ToString();
                string typeName = dt.Rows[i]["Good Type"].ToString();
                string brandName = dt.Rows[i]["Brand"].ToString();
                string spec = dt.Rows[i]["Specification"].ToString();
                string unitName = dt.Rows[i]["Measurement Unit"].ToString();
                string ckName = dt.Rows[i]["Default Warehouse"].ToString();
                string place = dt.Rows[i]["Place of Origin"].ToString();
                decimal priceCost = ConvertTo.ConvertDec(dt.Rows[i]["Purchase Price"].ToString());
                decimal priceSalesWhole = ConvertTo.ConvertDec(dt.Rows[i]["Wholesale Price"].ToString());
                decimal priceSalesRetail = ConvertTo.ConvertDec(dt.Rows[i]["Retail Price"].ToString());
                int numMin = ConvertTo.ConvertInt(dt.Rows[i]["Minimum Stock"].ToString());
                int numMax = ConvertTo.ConvertInt(dt.Rows[i]["Maximum Stock"].ToString());
                int bzDays = ConvertTo.ConvertInt(dt.Rows[i]["Shelf Life (Days)"].ToString());
                string isWeight = dt.Rows[i]["Weighing Product"].ToString();
                string remarks = dt.Rows[i]["Remarks"].ToString();
                string ckNameNm = dt.Rows[i]["Warehouse"].ToString();
                decimal num = ConvertTo.ConvertDec(dt.Rows[i]["Initial Quantity"].ToString());
                decimal price = ConvertTo.ConvertDec(dt.Rows[i]["Unit Cost"].ToString());
                decimal sumPrice = num * price;
                string FieldA = dt.Rows[i]["Custom Field 1"].ToString();
                string FieldB = dt.Rows[i]["Custom Field 2"].ToString();
                string FieldC = dt.Rows[i]["Custom Field 3"].ToString();
                string FieldD = dt.Rows[i]["Custom Field 4"].ToString();
                int brandId = 0;
                int typeId = 0;
                int unitId = 0;
                int ckId = 0;
                int ckIdNum = 0;
                bool hasCode = false;
                hasCode = goodsDAL.isExistsAdd(LoginUser.ShopId, code, barcode, names, spec);

                if (code != "" && names != "")
                {
                    #region if this is first row

                    if (!hasCode)
                    {
                        brandId = brandDAL.GetIdByName(LoginUser.ShopId, brandName);
                        typeId = typeDAL.GetIdByName(LoginUser.ShopId, typeName);
                        unitId = unitDAL.GetIdByName(LoginUser.ShopId, unitName);
                        ckId = ckDAL.GetIdByName(LoginUser.ShopId, ckName);

                        goodsDAL.ShopId = LoginUser.ShopId;
                        goodsDAL.Code = code;
                        goodsDAL.Barcode = barcode;
                        goodsDAL.Names = names;
                        goodsDAL.TypeId = typeId;
                        goodsDAL.BrandId = brandId;
                        goodsDAL.Spec = spec;
                        goodsDAL.UnitId = unitId;
                        goodsDAL.CkId = ckId;
                        goodsDAL.Place = place;
                        goodsDAL.PriceCost = priceCost;
                        goodsDAL.PriceSalesWhole = priceSalesWhole;
                        goodsDAL.PriceSalesRetail = priceSalesRetail;
                        goodsDAL.NumMin = numMin;
                        goodsDAL.NumMax = numMax;
                        goodsDAL.BzDays = bzDays;
                        goodsDAL.Remarks = remarks;

                        if (isWeight == "Yes")
                        {
                            goodsDAL.IsWeight = 1;

                            if (barcode.Length != 5)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            goodsDAL.IsWeight = 0;
                        }

                        goodsDAL.MakeDate = DateTime.Now;
                        goodsDAL.ImagePath = "noPic.jpg";
                        goodsDAL.ShowType = "Default";
                        goodsDAL.FieldA = FieldA;
                        goodsDAL.FieldB = FieldB;
                        goodsDAL.FieldC = FieldC;
                        goodsDAL.FieldD = FieldD;
                        goodsDAL.Flag = "Save";

                        goodsId = goodsDAL.Add();

                        if (goodsId > 0)
                        {
                            goodsNum += 1;
                            TotalNumOK += 1;
                        }

                        if (ckNameNm != "" && num != 0)
                        {
                            ckIdNum = ckDAL.GetIdByName(LoginUser.ShopId, ckNameNm);

                            if (ckIdNum > 0)
                            {
                                int addNum = goodsDAL.AddGoodsNumStart(goodsId, ckIdNum, num, price, sumPrice);

                                if (addNum > 0)
                                {
                                    kcNum += 1;
                                }
                            }

                        }
                    }

                    #endregion
                }
                else
                {
                    #region if is empty, check next row

                    if (code != "" && names != "" && ckNameNm != "" && num != 0)
                    {
                        ckIdNum = ckDAL.GetIdByName(LoginUser.ShopId, ckNameNm);
                        int add = goodsDAL.AddGoodsNumStart(goodsId, ckIdNum, num, price, sumPrice);

                        if (add > 0)
                        {
                            kcNum += 1;
                        }
                    }

                    #endregion
                }
            }

            #endregion

            TotalNumNo = TotalNumAll - TotalNumOK;

            string result = "Total[" + TotalNumAll.ToString() + "】row of goods，success【" + goodsNum.ToString() + "】row，fail【" + TotalNumNo.ToString() + "】rows，import successful【" + kcNum.ToString() + "] row of stocks.";

            return result;
        }
    }
}

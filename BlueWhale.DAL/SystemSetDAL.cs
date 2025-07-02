using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using BlueWhale.DBUtility;

namespace BlueWhale.DAL
{
    public class SystemSetDAL
    {
        public SystemSetDAL()
        {

        }

        #region Member

        private int shopId;
        public int ShopId
        {
            get { return shopId; }
            set { shopId = value; }
        }


        private string company;
        public string Company
        {
            get { return company; }
            set { company = value; }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        private string tel;
        public string Tel
        {
            get { return tel; }
            set { tel = value; }
        }

        private string fax;
        public string Fax
        {
            get { return fax; }
            set { fax = value; }
        }

        private string postCode;
        public string PostCode
        {
            get { return postCode; }
            set { postCode = value; }
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

        private string bwb;
        public string Bwb
        {
            get { return bwb; }
            set { bwb = value; }
        }

        private int num;
        public int Num
        {
            get { return num; }
            set { num = value; }
        }

        private int price;
        public int Price
        {
            get { return price; }
            set { price = value; }
        }

        private int priceType;
        public int PriceType
        {
            get { return priceType; }
            set { priceType = value; }
        }

        private int checkNum;
        public int CheckNum
        {
            get { return checkNum; }
            set { checkNum = value; }
        }

        private int useCheck;
        public int UseCheck
        {
            get { return useCheck; }
            set { useCheck = value; }
        }

        private int useTax;
        public int UseTax
        {
            get { return useTax; }
            set { useTax = value; }
        }

        /// <summary>
        /// Tax Percentage
        /// </summary>
        private int tax;
        public int Tax
        {
            get { return tax; }
            set { tax = value; }
        }

        private string fieldA;
        public string FieldA
        {
            get { return fieldA; }
            set { fieldA = value; }
        }

        private string fieldB;
        public string FieldB
        {
            get { return fieldB; }
            set { fieldB = value; }
        }

        private string fieldC;
        public string FieldC
        {
            get { return fieldC; }
            set { fieldC = value; }
        }

        private string fieldD;
        public string FieldD
        {
            get { return fieldD; }
            set { fieldD = value; }
        }

        private string corpId;
        public string CorpId
        {
            get { return corpId; }
            set { corpId = value; }
        }

        private string corpSecret;
        public string CorpSecret
        {
            get { return corpSecret; }
            set { corpSecret = value; }
        }

        private string printLogo;
        public string PrintLogo
        {
            get { return printLogo; }
            set { printLogo = value; }
        }

        private string printStamp;
        public string PrintStamp
        {
            get { return printStamp; }
            set { printStamp = value; }
        }

        private string secretBuy;
        public string SecretBuy
        {
            get { return secretBuy; }
            set { secretBuy = value; }
        }

        private string secretSales;
        public string SecretSales
        {
            get { return secretSales; }
            set { secretSales = value; }
        }

        private string secretStore;
        public string SecretStore
        {
            get { return secretStore; }
            set { secretStore = value; }
        }

        private string secretFee;
        public string SecretFee
        {
            get { return secretFee; }
            set { secretFee = value; }
        }

        private string secretReport;
        public string SecretReport
        {
            get { return secretReport; }
            set { secretReport = value; }
        }

        #endregion

        #region GetAllModel

        /// <summary>
        /// GetAllModel, select top 1 * from systemSet 
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModel()
        {
            string sql = "select top 1 * from systemSet ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        /// <summary>
        /// GetList
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM systemSet ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion

        /// <summary>
        /// UpdateLocation
        /// </summary>
        public int UpdateLocation(int shopId, double Location_X, double Location_Y)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("update systemSet set ");

            strSql.Append(" Location_X=@Location_X,");
            strSql.Append(" Location_Y=@Location_Y");

            strSql.Append(" where shopId=@shopId");

            SqlParameter[] parameters = {
                    new SqlParameter("@Location_X",Location_X),
                    new SqlParameter("@shopId", shopId),
                    new SqlParameter("@Location_Y", Location_Y)
                                        };

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        #region GetAllModel

        /// <summary>
        /// GetAllModel 
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModel(string sql)
        {
            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region DeleteInfo--------ByShopId

        /// <summary>
        /// DeleteData--------ByShopId
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int DeleteByShopId(int shopId)
        {
            string sql = "delete from SystemSet where shopId='" + shopId + "' ";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        /// <summary>
        /// Add One Data
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SystemSet(");
            strSql.Append("shopId,Company,address,tel,fax,postCode,fieldA,fieldB,fieldC,fieldD,dateStart,dateEnd)");
            strSql.Append(" values (");
            strSql.Append("@shopId,@Company,@address,@tel,@fax,@postCode,@fieldA,@fieldB,@fieldC,@fieldD,@dateStart,@dateEnd)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@shopId", ShopId),
                    new SqlParameter("@Company",Company),
                    new SqlParameter("@address", Address),
                    new SqlParameter("@tel",Tel),
                    new SqlParameter("@fax", Fax),
                    new SqlParameter("@postCode",PostCode),
                    new SqlParameter("@fieldA",FieldA),
                    new SqlParameter("@fieldB", FieldB),
                    new SqlParameter("@fieldC", FieldC),
                    new SqlParameter("@fieldD", FieldD),
                    new SqlParameter("@DateStart", DateStart),
                    new SqlParameter("@DateEnd", DateEnd)
                    };

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        #region UpdateData

        /// <summary>
        /// Update Data
        /// </summary>
        /// <returns></returns>
        public int Update()
        {
            string sql = @"update SystemSet 
                   set company=@Company,
                       address=@Address,
                       Tel=@Tel,
                       Fax=@Fax,
                       PostCode=@PostCode,
                       num=@num,
                       price=@price,
                       priceType=@priceType,
                       checkNum=@checkNum,
                       useCheck=@useCheck,
                       useTax=@useTax,
                       Tax=@Tax
                      ,fieldA = @fieldA
                      ,fieldB = @fieldB
                      ,fieldC = @fieldC
                      ,fieldD = @fieldD
                      where shopId=@shopId
                       ";

            SqlParameter[] param = {
                                       new SqlParameter("@Company",Company),
                                       new SqlParameter("@Address",Address),
                                       new SqlParameter("@Tel",Tel),
                                       new SqlParameter("@Fax",Fax),
                                       new SqlParameter("@PostCode",PostCode),
                                       new SqlParameter("@Num",Num),
                                       new SqlParameter("@Price",Price),
                                       new SqlParameter("@PriceType",PriceType),
                                       new SqlParameter("@CheckNum",CheckNum),
                                       new SqlParameter("@UseCheck",UseCheck),
                                       new SqlParameter("@UseTax",UseTax),
                                       new SqlParameter("@Tax",Tax),
                                       new SqlParameter("@FieldA",FieldA),
                                       new SqlParameter("@FieldB",FieldB),
                                       new SqlParameter("@FieldC",FieldC),
                                       new SqlParameter("@FieldD",FieldD),
                                       new SqlParameter("@shopId",ShopId)
                                   };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);
        }
        #endregion

        #region UpdateSample
        /// <summary>
        /// Update Sample
        /// </summary>
        /// <returns></returns>
        public int UpdateSample()
        {
            string sql = @"update systemSet 
                   set company=@Company
                       ,address=@Address
                       ,Tel=@Tel
                       ,Fax=@Fax
                       ,PostCode=@PostCode
                       ,checkNum=@checkNum
                       ,useCheck=@useCheck
                       ,fieldA = @fieldA
                       ,fieldB = @fieldB
                       ,fieldC = @fieldC
                       ,fieldD = @fieldD
                       ,printLogo = @printLogo
                       ,printZhang = @printStamp
                       ,secretBuy = @SecretBuy
                       ,secretSales = @secretSales
                       ,secretStore = @secretStore
                       ,secretFee = @SecretFee
                       ,SecretReport = @SecretReport
                         where shopId=@shopId        
                       ";

            SqlParameter[] param = {
                                        new SqlParameter("@Company",Company),
                                        new SqlParameter("@Address",Address),
                                        new SqlParameter("@Tel",Tel),
                                        new SqlParameter("@Fax",Fax),
                                        new SqlParameter("@CheckNum",CheckNum),
                                        new SqlParameter("@UseCheck",UseCheck),
                                        new SqlParameter("@PostCode",PostCode),
                                        new SqlParameter("@FieldA",FieldA),
                                        new SqlParameter("@FieldB",FieldB),
                                        new SqlParameter("@FieldC",FieldC),
                                        new SqlParameter("@FieldD",FieldD),
                                        new SqlParameter("@PrintLogo",PrintLogo),
                                        new SqlParameter("@PrintStamp",PrintStamp),
                                        new SqlParameter("@SecretBuy",SecretBuy),
                                        new SqlParameter("@SecretSales",SecretSales),
                                        new SqlParameter("@SecretStore",SecretStore),
                                        new SqlParameter("@SecretFee",SecretFee),
                                        new SqlParameter("@SecretReport",SecretReport),
                                        new SqlParameter("@shopId",ShopId)
                                   };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);
        }
        #endregion

        #region UpdateDate

        /// <summary>
        /// Update Date
        /// </summary>
        /// <returns></returns>
        public int UpdateDate()
        {
            string sql = @"update systemSet 
                   set dateStart=@dateStart
                       ,dateEnd=@dateEnd
                       where shopId=@shopId        
                       ";

            SqlParameter[] param = {
                                        new SqlParameter("@DateStart",DateStart),
                                        new SqlParameter("@DateEnd",DateEnd),

                                        new SqlParameter("@shopId",ShopId)
                                   };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);

        }

        #endregion

        #region UpdateLogo

        /// <summary>
        /// Update Logo
        /// </summary>
        /// <returns></returns>
        public int UpdateLogoURL(int shopId, string logoURL)
        {
            string sql = @"update systemSet 
                   set logoURL=@logoURL
                       where shopId=@shopId        
                       ";

            SqlParameter[] param = {
                                        new SqlParameter("@logoURL",logoURL),

                                        new SqlParameter("@shopId",ShopId)
                                   };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);
        }
        #endregion

        #region UpdateStamp
        /// <summary>
        /// Update Stamp
        /// </summary>
        /// <returns></returns>
        public int UpdateStampURL(int shopId, string stampUrl)
        {
            string sql = @"update systemSet 
                   set zhangURL=@stampURL
                       where shopId=@shopId        
                       ";

            SqlParameter[] param = {
                                        new SqlParameter("@zhangURL",stampUrl),

                                        new SqlParameter("@shopId",ShopId)
                                   };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region Update----PrintSet---PurOrderRemarks

        /// <summary>
        /// Update----Print Settings---Purchase Order Remarks
        /// </summary>
        /// <returns></returns>
        public int UpdatePrintSetPurOrderRemarks(int shopId, string RemarksPurOrder)
        {
            string sql = @"update systemSet 
                        set RemarksPurOrder=@RemarksPurOrder
                       where shopId=@shopId        
                       ";

            SqlParameter[] param = {
                                        new SqlParameter("@RemarksPurOrder",RemarksPurOrder),

                                        new SqlParameter("@shopId",shopId)
                                   };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);

        }

        #endregion

        #region Update----PrintSet---SalesOrderRemarks

        /// <summary>
        /// Update----Print Settings---Sales Order Remarks
        /// </summary>
        /// <returns></returns>
        public int UpdatePrintSetSalesOrderRemarks(int shopId, string RemarksSalesOrder)
        {
            string sql = @"update systemSet 
                        set RemarksSalesOrder=@RemarksSalesOrder
                       where shopId=@shopId        
                       ";

            SqlParameter[] param = {
                                        new SqlParameter("@RemarksSalesOrder",RemarksSalesOrder),

                                        new SqlParameter("@shopId",shopId)
                                   };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);

        }
        #endregion

        #region UpdateApp

        /// <summary>
        /// Update App Settings
        /// </summary>
        /// <returns></returns>
        public int UpdateERPApp(int shopId, string appName, string erpId, string appId, string appSecret, string mchId, string appKey, string sendUrl, string payUrl, string notifyUrl)
        {
            string sql = @"update systemSet 
                   set  appName=@appName
                       ,erpId=@erpId
                       ,appId=@appId
                       ,appSecret=@appSecret
                       ,mchId=@mchId
                       ,appKey=@appKey
                       ,sendUrl=@sendUrl
                       ,payUrl=@payUrl
                       ,notifyUrl=@notifyUrl
                       where shopId=@shopId
                       ";

            SqlParameter[] param = {
                                       new SqlParameter("@shopId",shopId),
                                       new SqlParameter("@appName",appName),
                                       new SqlParameter("@erpId",erpId),
                                       new SqlParameter("@appId",appId),
                                       new SqlParameter("@appSecret",appSecret),
                                       new SqlParameter("@mchId",mchId),

                                       new SqlParameter("@appKey",appKey),

                                       new SqlParameter("@sendUrl",sendUrl),
                                       new SqlParameter("@payUrl",payUrl),
                                       new SqlParameter("@notifyUrl",notifyUrl)
                                   };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);

        }

        #endregion

        #region UpdateERPAppId

        /// <summary>
        /// Update ERP App Id
        /// </summary>
        /// <returns></returns>
        public int UpdateERPAppCorpDatabaseConnString(string appId, int corpId)
        {
            string sql = @"update corpDatabaseConnString 
                   set appId=@appId
                      where  id=@corpId
                       ";

            SqlParameter[] param = {
                                       new SqlParameter("@appId",appId),
                                       new SqlParameter("@corpId",corpId)
                                   };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);

        }
        #endregion

        #region UpdatePrice

        /// <summary>
        /// Update Price
        /// </summary>
        /// <returns></returns>
        public int UpdateSystem()
        {
            SqlParameter[] param = {
                                       new SqlParameter("@Num",Num),
                                       new SqlParameter("@Price",Price)
                                   };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.StoredProcedure, "procUpdatePriceNumDecimal", param);
        }
        #endregion

        #region GetAllModel--------NoticeInfo

        /// <summary>
        /// Get All Model--------select top 1 * from noticeInfo 
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModelContents()
        {
            string sql = "select top 1 * from noticeInfo ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region UpdateNoticeInfo

        /// <summary>
        /// Update Notice Info
        /// </summary>
        /// <returns></returns>
        public int UpdateNoticeInfo(string contents)
        {
            string sql = @"update noticeInfo 
                   set contents=@contents
                       ";

            SqlParameter[] param = {
                                       new SqlParameter("@contents",contents)
                                   };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);

        }
        #endregion

        #region GetDatabase

        /// <summary>
        /// Get Database
        /// </summary>
        /// <returns></returns>
        public string GetDatabaseName()
        {
            string names = "master";
            string sql = " Select Name From Master..SysDataBases Where DbId=(Select Dbid From Master..SysProcesses Where Spid = @@spid) ";
            DataSet ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                names = ds.Tables[0].Rows[0]["name"].ToString();
            }

            return names;
        }

        #endregion

        #region GetDatabase--------Connection String

        /// <summary>
        /// Get Database--------Connection String
        /// </summary>
        /// <returns></returns>
        public string GetDatabaseAccess()
        {
            return SQLHelper.ConStr.ToString();
        }

        #endregion

        #region GetDataBackup

        /// <summary>
        /// Get Data Backup
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public int DataBackup(string database, string path)
        {
            int num = 0;
            SqlParameter[] parameterArray = new SqlParameter[] {
                new SqlParameter("@names", database),
                new SqlParameter("@path", path)
            };
            DataSet set = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.StoredProcedure, "BackUpDatabase", parameterArray);
            if (set.Tables[0].Rows.Count > 0)
            {
                if (set.Tables[0].Rows[0][0].ToString() == path)
                {
                    num = 1;
                }
            }

            return num;
        }

        /// <summary>
        /// Restore Data
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public int DataRestore(string names, string path)
        {
            SqlParameter[] parameterArray = new SqlParameter[] {
                new SqlParameter("@names", names),
                new SqlParameter("@path", path)
            };
            //DataSet set = SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.StoredProcedure, "RestoreDatabase", parameterArray);
            //if (set.Tables[0].Rows.Count > 0)
            //{
            //    DataRow row = set.Tables[0].Rows[0];
            //    num = int.Parse(row[0].ToString());
            //}
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.StoredProcedure, "RestoreDatabase", parameterArray); ;
        }

        /// <summary>
        /// Check if data backup is successful
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public int DataBackupCom(string path)
        {
            int num = 0;
            string str = "select top 1 physical_device_name from msdb..backupmediafamily order by media_set_id desc";
            DataSet set = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, str, null);
            if (set.Tables[0].Rows.Count > 0)
            {
                DataRow row = set.Tables[0].Rows[0];
                if (row[0].ToString() == path)
                {
                    num = 1;
                }
            }
            return num;
        }

        #endregion

        #region InsertIntoDataBackList

        /// <summary>
        /// Insert Into Data Back List
        /// </summary>
        /// <param name="makeId"></param>
        /// <param name="backName"></param>
        /// <returns></returns>
        public int AddDataBackList(int makeId, string backName)
        {
            string sql = "insert into dataBackList(backName,makeId,makeDate) values('" + backName + "','" + makeId + "',getdate()) ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region GetDataBackList

        /// <summary>
        /// Get Data Back List
        /// </summary>
        /// <returns></returns>
        public DataSet GetDataBackList()
        {
            string sql = "select * from viewDataBackList order by id desc";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);

        }

        #endregion 

        #region DeleteDataBackList

        /// <summary>
        /// Delete Data Back List
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            string sql = "delete from dataBackList where id='" + id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region BlueWhaleUser

        #region AddUser

        /// <summary>
        /// 插入软件用户
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="corpSecret"></param>
        /// <param name="phone"></param>
        /// <param name="pwd"></param>
        /// <param name="typeId"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public int AddUserList(string corpId, string corpSecret, string phone, string pwd, int typeId, string typeName)
        {
            string sql = " if not exists(select * from userList where corpId=@corpId and  corpSecret=@corpSecret and typeId=@typeId) ";

            sql += " insert into userList(corpId,corpSecret,phone,pwd,typeId,typeName,dateStart,dateEnd,makeDate) ";
            sql += " values(@corpId,@corpSecret,@phone,@pwd,@typeId,@typeName,@dateStart,@dateEnd,@makeDate)";

            SqlParameter[] param ={

                                      new SqlParameter("@corpId",corpId),
                                      new SqlParameter("@corpSecret",corpSecret),
                                      new SqlParameter("@phone",phone),
                                      new SqlParameter("@pwd",pwd),
                                      new SqlParameter("@typeId",typeId),
                                      new SqlParameter("@typeName",typeName),
                                      new SqlParameter("@dateStart",DateTime.Now),
                                      new SqlParameter("@dateEnd",DateTime.Now.AddDays(30)),
                                      new SqlParameter("@makeDate",DateTime.Now)
            };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region GetUser

        /// <summary>
        /// GetUser---All
        /// </summary>
        /// <returns></returns>
        public DataSet GetUserList()
        {
            string sql = "select * from userList order by id desc";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        /// <summary>
        /// Get User by CorpId and TypeId 
        /// </summary>
        /// <param name="corpId">corpId</param>
        /// <param name="typeId">typeId  1 阿里钉钉  2 微信企业 </param>
        /// <returns></returns>
        public DataSet GetUserList(string corpId, int typeId)
        {
            string sql = " select * from userList  where corpId='" + corpId + "' and typeId='" + typeId + "' order by id desc ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        /// <summary>
        /// Check If User Is Expired ---true if expired、false if no record
        /// </summary>
        /// <param name="corpId">corpId</param>
        /// <param name="typeId">typeId 1 ERP </param>
        /// <returns></returns>
        public bool CheckUserListDateEnd(string corpId, int typeId)
        {
            bool result = false;

            string sql = "select * from userList where corpId=@corpId and typeId=@typeId and dateEnd<getdate() order by id desc ";

            SqlParameter[] param ={
                                      new SqlParameter("@corpId",corpId),
                                      new SqlParameter("@typeId",typeId)
                                   };

            DataSet ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, param);
            if (ds.Tables[0].Rows.Count > 0)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;

        }

        #endregion 

        #endregion

        #region ERPSet

        /// <summary>
        /// Get ERP Set select top 1 * from systemSet 
        /// </summary>
        /// <returns></returns>
        public DataSet GetERPCorpSet()
        {
            string sql = " select top 1 * from systemSet ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #region UpdateERPCorpSet
        /// <summary>
        /// Update ERP Corp Settings
        /// </summary>
        /// <returns></returns>
        public int UpdateERPCorpSet(int appId, string appName, string corpId, string corpSecret)
        {
            string sql = @"if exists(select * from weixinQY)
                               update erpCorp 
                                   set  appName=@appName

                                       ,appId=@appId
                                       ,corpId=@corpId
                                       ,corpSecret=@corpSecret
                                       ,makeDate=getdate()
                        else
                        insert into erpCorp(appId,appName,corpId,corpSecret) values(@appId,@appName,@corpId,@corpSecret)             
                       ";

            SqlParameter[] param = {
                                       new SqlParameter("@appId",appId),
                                       new SqlParameter("@appName",appName),
                                       new SqlParameter("@corpId",corpId),
                                       new SqlParameter("@corpSecret",corpSecret)
                                   };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);
        }
        #endregion


        #region UpdateCorpName---Authorized

        /// <summary>
        /// Update Corp Name--- When Authorized
        /// </summary>
        /// <returns></returns>
        public int UpdateCorpIdCorpNameQY(string corpId, string corpName, string permanentCode)
        {
            string sql = @" update systemSet set corpId=@corpId,corpName=@corpName,permanentCode=@permanentCode,company=@corpName  ";

            SqlParameter[] param = {
                                       new SqlParameter("@corpId",corpId),
                                       new SqlParameter("@corpName",corpName),
                                       new SqlParameter("@permanentCode",permanentCode)
                                   };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);

        }
        #endregion

        #endregion
    }
}

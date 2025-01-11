using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Lanwei.Weixin.Model;
using Lanwei.Weixin.DBUtility;

namespace Lanwei.Weixin.DAL
{
    public class SystemSetDAL
    {
        public SystemSetDAL()
        {

        }
        #region 成员字段

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
        /// 税率
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


        private string corpIdQY;
        public string CorpIdQY
        {
            get { return corpIdQY; }
            set { corpIdQY = value; }
        }

        private string corpSecretQY;
        public string CorpSecretQY
        {
            get { return corpSecretQY; }
            set { corpSecretQY = value; }
        }

        private string corpIdDD;
        public string CorpIdDD
        {
            get { return corpIdDD; }
            set { corpIdDD = value; }
        }

        private string corpSecretDD;
        public string CorpSecretDD
        {
            get { return corpSecretDD; }
            set { corpSecretDD = value; }
        }

        private string userSecret;
        public string UserSecret
        {
            get { return userSecret; }
            set { userSecret = value; }
        }

        private string checkInSecret;
        public string CheckInSecret
        {
            get { return checkInSecret; }
            set { checkInSecret = value; }
        }

        private string applySecret;
        public string ApplySecret
        {
            get { return applySecret; }
            set { applySecret = value; }
        }


        private string printLogo;
        public string PrintLogo
        {
            get { return printLogo; }
            set { printLogo = value; }
        }

        private string printZhang;
        public string PrintZhang
        {
            get { return printZhang; }
            set { printZhang = value; }
        }

        private string secretCheckIn;
        public string SecretCheckIn
        {
            get { return secretCheckIn; }
            set { secretCheckIn = value; }
        }

        private string secretApply;
        public string SecretApply
        {
            get { return secretApply; }
            set { secretApply = value; }
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


        #region 获取所有成员

        /// <summary>
        /// 获取所有成员 select top 1 * from systemSet 
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModel()
        {
            string sql = "select top 1 * from systemSet ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        /// <summary>
        /// 获得数据列表
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
        /// 更新地理位置
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



        #region 获取所有成员

        /// <summary>
        /// 获取所有成员 
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModel(string sql)
        {
           
            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #region 删除信息--------根据企业Id
        /// <summary>
        /// 删除信息--------根据企业Id
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
        /// 增加一条数据
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


        #region 修改成员信息
        /// <summary>
        /// 修改成员信息
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
                       bwb=@bwb,
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
                                      
                                       new SqlParameter("@Bwb",Bwb),
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


        #region 修改成员信息
        /// <summary>
        /// 修改成员信息
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

                       ,corpIdQY = @corpIdQY
                       ,corpSecretQY = @corpSecretQY
                       ,corpIdDD = @corpIdDD
                       ,corpSecretDD = @corpSecretDD
                       ,printLogo = @printLogo
                       ,printZhang = @printZhang
                       ,userSecret = @userSecret
                       ,checkInSecret = @checkInSecret
                       ,applySecret = @applySecret

                       ,SecretCheckIn = @SecretCheckIn
                       ,secretApply = @secretApply
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
                                    
                                    new SqlParameter("@CorpIdQY",CorpIdQY),
                                    new SqlParameter("@CorpSecretQY",CorpSecretQY),
                                    new SqlParameter("@CorpIdDD",CorpIdDD),
                                    new SqlParameter("@CorpSecretDD",CorpSecretDD),
                                    new SqlParameter("@PrintLogo",PrintLogo),
                                    new SqlParameter("@PrintZhang",PrintZhang),

                                    new SqlParameter("@UserSecret",UserSecret),
                                    new SqlParameter("@CheckInSecret",CheckInSecret),
                                    new SqlParameter("@ApplySecret",ApplySecret),

                                    new SqlParameter("@SecretCheckIn",SecretCheckIn),
                                    new SqlParameter("@SecretApply",SecretApply),
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

        #region 修改成员信息
        /// <summary>
        /// 修改成员信息
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

        #region 修改LOGO
        /// <summary>
        /// 修改LOGO
        /// </summary>
        /// <returns></returns>
        public int UpdateLogoURL(int shopId,string logoURL)
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

        #region 修改印章
        /// <summary>
        /// 修改印章
        /// </summary>
        /// <returns></returns>
        public int UpdateZhangURL(int shopId, string zhangURL)
        {
            string sql = @"update systemSet 
                   set zhangURL=@zhangURL
                     
                       where shopId=@shopId        
                       ";

            SqlParameter[] param = {
                                      
                                           new SqlParameter("@zhangURL",zhangURL),
                                          
                                           new SqlParameter("@shopId",ShopId)
                                       


                                   };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);

        }
        #endregion


        #region 修改----打印设置---采购订单打印
        /// <summary>
        /// 修改----打印设置---采购订单打印
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


        #region 修改----打印设置---销售出库打印
        /// <summary>
        /// 修改----打印设置---销售出库打印
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



        #region 修改成员信息
        /// <summary>
        /// 修改成员信息
        /// </summary>
        /// <returns></returns>
        public int UpdateWeixinApp(int shopId,string appName,string weixinId,string appId, string appSecret, string mchId, string appKey, string sendUrl, string payUrl, string notifyUrl)
        {
            string sql = @"update systemSet 

                   set  appName=@appName
                       ,weixinId=@weixinId
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
                                       new SqlParameter("@weixinId",weixinId),
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

        #region 修改蓝微网站的appId
        /// <summary>
        /// 修改蓝微网站的appId
        /// </summary>
        /// <returns></returns>
        public int UpdateWeixinAppCorpDatabaseConnString(string appId,int corpId)
        {
            string sql = @"update corpDatabaseConnString 

                   set 
                       appId=@appId
                     
                      where  id=@corpId
                      
                       ";

            SqlParameter[] param = {
                                     
                                       new SqlParameter("@appId",appId),
                                        new SqlParameter("@corpId",corpId)
                                    
                                       


                                   };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);

        }
        #endregion

        #region 修改单价，数量的精确小数位
        /// <summary>
        /// 修改单价，数量的精确小数位
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

        #region 获取所有成员--------系统通告

        /// <summary>
        /// 获取所有成员--------系统通告 select top 1 * from noticeInfo 
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModelContents()
        {
            string sql = "select top 1 * from noticeInfo ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #region 修改系统通告
        /// <summary>
        /// 修改系统通告
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



        #region 获取当前数据库

        /// <summary>
        /// 获取当前数据库
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


        #region 获取当前数据库--------连接字符串

        /// <summary>
        /// 获取当前数据库--------连接字符串
        /// </summary>
        /// <returns></returns>
        public string GetDatabaseAccess()
        {
            return SQLHelper.ConStr.ToString();
        }

        #endregion


        #region 数据库备份

        /// <summary>
        /// 数据库备份
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public int DataBackup(string database,string path)
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
        /// 数据库还原
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public int DataRestore(string names, string path)
        {
            int num = 0;
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
        /// 数据库备份判断是否成功？
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



        #region 插入数据库备份记录

        /// <summary>
        /// 插入数据库备份记录
        /// </summary>
        /// <param name="makeId"></param>
        /// <param name="backName"></param>
        /// <returns></returns>
        public int AddDataBackList(int makeId, string backName)
        {
            string sql = "insert into dataBackList(backName,makeId,makeDate) values('"+backName+"','"+makeId+"',getdate()) ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr,CommandType.Text,sql,null);

        }


        #endregion




        #region 获取数据库备份记录

        /// <summary>
        /// 获取数据库备份记录
        /// </summary>
        /// <returns></returns>
        public DataSet GetDataBackList()
        {
            string sql = "select * from viewDataBackList order by id desc";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr,CommandType.Text,sql,null);

        }

        #endregion 

        #region 删除数据库备份记录
        /// <summary>
        /// 删除数据库备份记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            string sql = "delete from dataBackList where id='" + id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion


        #region 蓝微软件用户

        #region 插入软件用户

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
        public int AddUserList(string corpId,string corpSecret,string phone,string pwd,int typeId,string typeName)
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

        #region 获取软件用户

        /// <summary>
        /// 获取软件用户---所有
        /// </summary>
        /// <returns></returns>
        public DataSet GetUserList()
        {
            string sql = "select * from userList order by id desc";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);

        }

        /// <summary>
        ///获取软件用户 
        /// </summary>
        /// <param name="corpId">corpId</param>
        /// <param name="typeId">typeId  1 阿里钉钉  2 微信企业 </param>
        /// <returns></returns>
        public DataSet GetUserList(string corpId,int typeId)
        {
            string sql = " select * from userList  where corpId='" + corpId + "' and typeId='" + typeId + "' order by id desc ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);

        }


        /// <summary>
        ///判断软件用户是否过期 ---返回true为已过期、false为没有记录
        /// </summary>
        /// <param name="corpId">corpId</param>
        /// <param name="typeId">typeId  1 阿里钉钉  2 微信企业 </param>
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


        #region 微信企业号

        /// <summary>
        /// 获取微信企业号 select top 1 * from systemSet 
        /// </summary>
        /// <returns></returns>
        public DataSet GetWeixinQYSet()
        {
            string sql = " select top 1 * from systemSet ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #region 修改微信企业号
        /// <summary>
        /// 修改微信企业号
        /// </summary>
        /// <returns></returns>
        public int UpdateWeixinQY(int appId,string appName, string corpId, string corpSecret)
        {
            string sql = @"
if exists(select * from weixinQY)

               update weixinQY 
                   set  appName=@appName

                       ,appId=@appId
                       ,corpId=@corpId
                       ,corpSecret=@corpSecret
                       ,makeDate=getdate()

    
else
insert into weixinQY(appId,appName,corpId,corpSecret) values(@appId,@appName,@corpId,@corpSecret)             
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


        #region 修改企业名称---授权后
        /// <summary>
        /// 修改企业名称---授权后
        /// </summary>
        /// <returns></returns>
        public int UpdateCorpIdCorpNameQY(string corpId, string corpName, string permanentCode)
        {
            string sql = @" update systemSet set corpIdQY=@corpId,corpNameQY=@corpName,permanentCodeQY=@permanentCode,company=@corpName  ";

            SqlParameter[] param = {
                                       new SqlParameter("@corpId",corpId),
                                       new SqlParameter("@corpName",corpName),
                                       new SqlParameter("@permanentCode",permanentCode)
                                      
                                   };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);

        }
        #endregion



        #endregion

        #region 阿里钉钉

        /// <summary>
        /// 获取阿里钉钉 select top 1 * from systemSet 
        /// </summary>
        /// <returns></returns>
        public DataSet GetDingdingSet()
        {
            string sql = "select top 1 * from dingdingSet ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #region 修改阿里钉钉
        /// <summary>
        /// 修改阿里钉钉
        /// </summary>
        /// <returns></returns>
        public int UpdateDingdingSet(int appId, string appName, string corpId, string corpSecret)
        {
            string sql = @"
            if exists(select * from dingdingSet)

               update dingdingSet 
                   set  appName=@appName

                       ,appId=@appId
                       ,corpId=@corpId
                       ,corpSecret=@corpSecret
                       ,makeDate=getdate()

    
else
insert into dingdingSet(appId,appName,corpId,corpSecret) values(@appId,@appName,@corpId,@corpSecret)             
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


        #endregion

    }
}

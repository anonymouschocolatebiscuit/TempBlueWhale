using BlueWhale.DBUtility;
using BlueWhale.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BlueWhale.DAL
{
    public class ClientDAL
    {
        public ClientDAL()
        {

        }

        #region Attribute

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private int shopId;
        public int ShopId
        {
            get { return shopId; }
            set { shopId = value; }
        }

        private string code;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        private string names;
        public string Names
        {
            get { return names; }
            set { names = value; }
        }

        private int typeId;
        public int TypeId
        {
            get { return typeId; }
            set { typeId = value; }
        }

        private DateTime yueDate;
        public DateTime YueDate
        {
            get { return yueDate; }
            set { yueDate = value; }
        }

        private decimal payNeed;
        public decimal PayNeed
        {
            get { return payNeed; }
            set { payNeed = value; }
        }

        private decimal payReady;
        public decimal PayReady
        {
            get { return payReady; }
            set { payReady = value; }
        }

        private int tax;
        public int Tax
        {
            get { return tax; }
            set { tax = value; }
        }

        private string remarks;
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }

        private DateTime makeDate;
        public DateTime MakeDate
        {
            get { return makeDate; }
            set { makeDate = value; }
        }

        private string taxNumber;
        public string TaxNumber
        {
            get { return taxNumber; }
            set { taxNumber = value; }
        }

        private string bankName;
        public string BankName
        {
            get { return bankName; }
            set { bankName = value; }
        }

        private string bankNumber;
        public string BankNumber
        {
            get { return bankNumber; }
            set { bankNumber = value; }
        }

        private string dizhi;
        public string Dizhi
        {
            get { return dizhi; }
            set { dizhi = value; }
        }

        private string flag;
        public string Flag
        {
            get { return flag; }
            set { flag = value; }
        }

        public string _openId;

        /// <summary>
        /// openId 
        /// </summary>
        public string openId
        {
            set { _openId = value; }
            get { return _openId; }
        }

        public string _nickname;

        /// <summary>
        /// user nickname 
        /// </summary>
        public string nickname
        {
            set { _nickname = value; }
            get { return _nickname; }
        }

        public string _province;

        /// <summary>
        /// province in personal info is filled in by user 
        /// </summary>
        public string province
        {
            set { _province = value; }
            get { return _province; }
        }

        public string _city;

        /// <summary>
        /// city in personal info is filled in by normal user 
        /// </summary>
        public string city
        {
            set { _city = value; }
            get { return _city; }
        }

        public string _country;

        /// <summary>
        /// country, example Malaysia is 'MY'
        /// </summary>
        public string country
        {
            set { _country = value; }
            get { return _country; }
        }

        public string _headimgurl;

        /// <summary>
        /// User Profile Image，Last value is the image size（Got 0、46、64、96、132 value can be choosen，0 is 640*640 square image），user without profile image will be 0
        /// </summary>
        public string headimgurl
        {
            set { _headimgurl = value; }
            get { return _headimgurl; }
        }

        #endregion

        #region Attribute

        /// <summary>
        /// Check code exists--When Add
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsCodeAdd(int shopId, string code)
        {
            bool flag = false;

            string sql = "select * from Client where code='" + code + "' and shopId='" + shopId + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }

            reader.Close();

            return flag;
        }

        /// <summary>
        /// Check code exists--When Edit
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsCodeEdit(int id, int shopId, string code)
        {
            bool flag = false;

            string sql = "select * from Client where code='" + code + "' and id<>'" + id + "' and shopId='" + shopId + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            reader.Close();
            return flag;
        }

        /// <summary>
        /// Check names exists--When Add
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsNamesAdd(int shopId, string names)
        {
            bool flag = false;

            string sql = "select * from Client where names='" + names + "' and shopId='" + shopId + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }

            reader.Close();
            return flag;
        }

        /// <summary>
        /// Check names exists--When Edit
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsNamesEdit(int shopId, string code, string names)
        {
            bool flag = false;

            string sql = "select * from Client where names='" + names + "' and code<>'" + code + "' and shopId='" + shopId + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }

            reader.Close();
            return flag;
        }

        #endregion

        #region Get All

        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModel()
        {
            string sql = "select *,code+ ' '+Names CodeName from viewClient order by code ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        /// <summary>
        /// Get List
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *,code+' '+names CodeName ");
            strSql.Append(" FROM viewClient ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by code  ");

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, strSql.ToString(), null);
        }

        #region GetAllModel--View

        /// <summary>
        /// GetAll--ModelView
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModelView(int id)
        {
            string sql = "select * from viewClient where id='" + id + "' order by code ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region GetAllModel--View

        /// <summary>
        /// GetAll--ModelView
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModelView()
        {
            string sql = "select * from viewClient order by code ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region GetAllModel--View

        /// <summary>
        /// GetAll--ModelView
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModelView(string key)
        {
            string sql = "select * from viewClient where 1=1 ";

            if (key != "")
            {
                sql += " and names like'%" + key + "%'" +
                    " or  code like'%" + key + "%' " +
                    " or  tel like'%" + key + "%' " +
                    " or  remarks like'%" + key + "%' " +
                    " or  address like'%" + key + "%' ";
            }
            sql += " order by code ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region GetAllModel--View

        /// <summary>
        /// GetAll--ModelView
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModelView(int typeId, string key)
        {
            string sql = "select * from viewClient where 1=1 ";
            if (key != "")
            {
                sql += " and ( names like'%" + key + "%'" +
                    " or  code like'%" + key + "%' " +
                    " or  tel like'%" + key + "%' " +
                    " or  remarks like'%" + key + "%' " +
                    " or  address like'%" + key + "%' )";
            }

            if (typeId != 0)
            {
                sql += " and typeId='" + typeId + "'";
            }

            sql += " order by code ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region GetModel-------By Id

        /// <summary>
        /// Get Model-------By Id
        /// </summary>
        /// <returns></returns>
        public DataSet GetModelById(int Id)
        {
            string sql = "select * from Client where id='" + Id + "' ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Add Client
        /// <summary>
        /// Add Client
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string id = "0";

            string sql = @" insert into client
            (
                shopId,
                code,
                names,
                TypeId,
                yueDate,
                payNeed,
                payReady,
                tax,
                remarks,
                makeDate,
                taxNumber,
                bankName,
                bankNumber,
                dizhi,
                flag,
                openId,
                nickname,
                headimgurl,
                city,
                country,
                province
            ) ";

            sql += @"  values
            (
                @shopId,
                @code,
                @names,
                @TypeId,
                @yueDate,
                @payNeed,
                @payReady,
                @tax,
                @remarks,
                @makeDate,
                @taxNumber,
                @bankName,
                @bankNumber,
                @dizhi,
                @flag,
                @openId,
                @nickname,
                @headimgurl,
                @city,
                @country,
                @province
            ) 
            select @@identity ";

            SqlParameter[] param = {
                new SqlParameter("@shopId",shopId),
                new SqlParameter("@code",code),
                new SqlParameter("@names",names),
                new SqlParameter("@TypeId",TypeId),
                new SqlParameter("@yueDate",YueDate),
                new SqlParameter("@payNeed",payNeed),
                new SqlParameter("@payReady",payReady),
                new SqlParameter("@tax",tax),
                new SqlParameter("@remarks",remarks),
                new SqlParameter("@makeDate",makeDate),
                new SqlParameter("@taxNumber",taxNumber),
                new SqlParameter("@bankName",bankName),
                new SqlParameter("@bankNumber",bankNumber),
                new SqlParameter("@dizhi",dizhi),
                new SqlParameter("@flag",flag),
                new SqlParameter("@openId",openId),
                new SqlParameter("@nickname",nickname),
                new SqlParameter("@headimgurl",headimgurl),
                new SqlParameter("@city",city),
                new SqlParameter("@country",country),
                new SqlParameter("@province",province)
            };

            SqlDataReader sdr = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, param);
            if (sdr.Read())
            {
                id = sdr[0].ToString();
            }
            sdr.Close();
            return int.Parse(id);
        }

        #endregion

        #region Edit Client
        /// <summary>
        /// Edit Client
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="names"></param>
        /// <returns></returns>

        public int Update()
        {
            string sql = "";
            if (!this.isExistsCodeEdit(Id, ShopId, Code) && !this.isExistsNamesEdit(ShopId, Code, Names))
            {
                sql = @" update client set
                            shopId=@shopId,
                            code=@code,
                            names=@names,
                            TypeId=@TypeId,
                            yueDate=@yueDate,
                            payNeed=@payNeed,
                            payReady=@payReady,
                            tax=@tax,
                            remarks=@remarks,
                            makeDate=@makeDate,
                            taxNumber=@taxNumber,
                            bankName=@bankName,
                            bankNumber=@bankNumber,
                            dizhi=@address
                        where Id=@id  ";

                SqlParameter[] param = {
                    new SqlParameter("@shopId",shopId),
                    new SqlParameter("@code",code),
                    new SqlParameter("@names",names),
                    new SqlParameter("@TypeId",TypeId),
                    new SqlParameter("@yueDate",yueDate),
                    new SqlParameter("@payNeed",payNeed),
                    new SqlParameter("@payReady",payReady),
                    new SqlParameter("@tax",tax),
                    new SqlParameter("@remarks",remarks),
                    new SqlParameter("@makeDate",makeDate),
                    new SqlParameter("@taxNumber",taxNumber),
                    new SqlParameter("@bankName",bankName),
                    new SqlParameter("@bankNumber",bankNumber),
                    new SqlParameter("@address",dizhi),
                    new SqlParameter("@Id",Id)
                };
                return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region Update Client Password
        /// <summary>
        /// Update Client Password
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public int UpdateClientPwd(int Id)
        {
            string sql = "update Client set pwd='123456',pwds='123456'  where Id='" + Id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }
        #endregion

        #region Delete Client
        /// <summary>
        /// Delete Client
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            string sql = "delete from Client where id='" + Id + "' and flag<>'审核' ";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Review

        /// <summary>
        /// Review
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="chekerId"></param>
        /// <param name="checker"></param>
        /// <param name="checkDate"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public int UpdateCheck(int Id, int chekerId, string checker, DateTime checkDate, string flag)
        {
            string sql = " if not exists(select * from client where flag='" + flag + "' and id='" + Id + "') ";
            sql += " begin ";

            sql += " update client set flag='" + flag + "' ";
            if (flag == "Review")
            {
                sql += " ,checkId='" + chekerId + "',checkDate='" + checkDate.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            }
            if (flag == "Save")
            {
                sql += " ,checkId=null,checkDate=null ";
            }

            sql += "  where id = '" + Id + "'";

            sql += " end ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Client Login
        /// <summary>
        /// Client Login
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public ClientInfo GetClientByIdPwd(string loginName, string pwd)
        {
            string sql = "select * from viewClient where code=@loginName and Pwds=@Pwd  ";//and flag='审核'

            ClientInfo userInfo = null;
            SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@loginName",loginName),
                new SqlParameter("@pwd", pwd)
            };

            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, parms))
            {
                if (dr.Read())
                {
                    userInfo = new ClientInfo();
                    userInfo.Id = int.Parse(dr["id"].ToString());
                    userInfo.CompanyName = dr["names"].ToString();
                    userInfo.Pwd = dr["pwd"].ToString();
                    userInfo.TypeId = int.Parse(dr["typeId"].ToString());
                    userInfo.TypeName = dr["TypeName"].ToString();
                    userInfo.Flag = dr["flag"].ToString();
                    userInfo.OpenId = dr["OpenId"].ToString();
                }
            }

            return userInfo;
        }

        #endregion

        #region Client Login
        /// <summary>
        /// Client Login
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>

        public ClientInfo GetClientByIdPwd(string fromUser)
        {
            string sql = "select * from viewClient where flag='审核' and id in (select top 1 clientId from ClientWeixin where fromUser=@fromUser order by id desc ) ";

            ClientInfo userInfo = null;

            SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@fromUser",fromUser)

            };

            using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, parms))
            {
                if (dr.Read())
                {
                    userInfo = new ClientInfo();
                    userInfo.Id = int.Parse(dr["id"].ToString());
                    userInfo.CompanyName = dr["names"].ToString();
                    userInfo.Pwd = dr["pwd"].ToString();
                    userInfo.TypeId = int.Parse(dr["typeId"].ToString());
                    userInfo.Dis = Convert.ToDecimal(dr["dis"].ToString());
                }
            }

            return userInfo;
        }

        #endregion

        #region Add Client Address
        /// <summary>
        /// Add Client Address
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public int AddClientAddress(int clientId, int proId, int ctId, int areaId, string address, string postCode, string names, string phone, string tel, int defaultStatus)
        {
            string sql = "";
            if (defaultStatus == 1)
            {
                sql += " update clientAddress set default=0 where clientId='" + clientId + "' ";
            }

            sql += " insert into  clientAddress(clientId,proId,ctId,areaId,address,postCode,names,phone,tel,default) ";
            sql += " values('" + clientId
                + "','" + proId
                + "','" + ctId
                + "','" + areaId
                + "','" + address
                + "','" + postCode
                + "','" + names
                + "','" + phone
                + "','" + tel
                + "','" + defaultStatus + "')  ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Add Client Address
        /// <summary>
        /// Add Client Address
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public int AddClientAddress(string openId, int proId, int ctId, int areaId, string address, string postCode, string names, string phone, string tel, int defaultStatus)
        {
            string sql = "";
            if (defaultStatus == 1)
            {
                sql += " update clientAddress set default=0 where openId='" + openId + "' ";
            }

            sql += " insert into  clientAddress(openId,proId,ctId,areaId,address,postCode,names,phone,tel,default) ";
            sql += " values('" + openId
                + "','" + proId
                + "','" + ctId
                + "','" + areaId
                + "','" + address
                + "','" + postCode
                + "','" + names
                + "','" + phone
                + "','" + tel
                + "','" + defaultStatus + "')  ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion


        #region Add Client Address
        /// <summary>
        /// Add Client Address
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public int AddClientAddress(int clientId, string openId, int proId, int ctId, int areaId, string address, string postCode, string names, string phone, string tel, int defaultStatus)
        {
            string sql = "";
            if (defaultStatus == 1)
            {
                sql += " update clientAddress set default=0 where openId='" + openId + "' and clientId='" + clientId + "' ";
            }

            sql += " insert into  clientAddress(clientId,openId,proId,ctId,areaId,address,postCode,names,phone,tel,default) ";
            sql += " values('" + clientId
                + "','" + openId
                + "','" + proId
                + "','" + ctId
                + "','" + areaId
                + "','" + address
                + "','" + postCode
                + "','" + names
                + "','" + phone
                + "','" + tel
                + "','" + defaultStatus + "')  ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Set Default Address

        /// <summary>
        /// Set Default Address
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int SetDefaultAddress(int id, int clientId)
        {
            string sql = " update clientAddress set moren=0 where clientId='" + clientId + "' update  clientAddress set moren=1 where id='" + id + "' ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Delete Address
        /// <summary>
        /// Delete Address
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int DeleteAddress(int id)
        {
            string sql = "delete from ClientAddress where id='" + id + "'  ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Get Client Address

        /// <summary>
        /// Get Client Address
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public DataSet GetClientAddress(int clientId)
        {
            string sql = "select *,'（'+names+' Collect）'+proName+','+ctName+','+areaName+','+address+' '+phone addressAll from viewClientAddress where clientId='" + clientId + "' ";

            sql += " order by default desc ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


        #region Get Client Address

        /// <summary>
        /// Get Client Address
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>

        public DataSet GetClientAddress(string openId)
        {
            string sql = "select *,'（'+names+' Collect）'+proName+','+ctName+','+areaName+','+address+' '+phone addressAll from viewClientAddress where openId='" + openId + "' ";

            sql += " order by default desc ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Get Client Address--By Id

        /// <summary>
        /// Get Client Address--By Id
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public DataSet GetClientAddressById(int id)
        {
            string sql = "select *,'（'+names+' Collect）'+proName+','+ctName+','+areaName+','+address+' '+phone addressAll from viewClientAddress where id='" + id + "' ";

            sql += " order by default desc ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Get All Client Type

        /// <summary>
        /// Get All Client Type--Return DataSet
        /// </summary>
        /// <returns></returns>

        public DataSet GetAllClientType()
        {
            string sql = @"select a.*,isnull(num,0) num
                            from ClientType a
                            left join
                            (
                            select TypeId,count(*) num
                            from client
                            group by TypeId
                            ) b
                            on a.id=b.TypeId  order by flag desc ";
            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region Delete Client Type

        /// <summary>
        /// Delete Client Type
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>

        public int DeleteClientType(int Id)
        {
            string sql = "delete from ClientType where id='" + Id + "' ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion
    }
}
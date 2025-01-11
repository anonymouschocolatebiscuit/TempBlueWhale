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
    public class ClientDAL
    {
        public ClientDAL()
        {

        }

        #region 成员字段

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
        /// 用户昵称 
        /// </summary>
        public string nickname
        {
            set { _nickname = value; }
            get { return _nickname; }
        }

        public string _province;

        /// <summary>
        /// 用户个人资料填写的省份
        /// </summary>
        public string province
        {
            set { _province = value; }
            get { return _province; }
        }

        public string _city;

        /// <summary>
        /// 普通用户个人资料填写的城市 
        /// </summary>
        public string city
        {
            set { _city = value; }
            get { return _city; }
        }

        public string _country;

        /// <summary>
        /// 国家，如中国为CN 
        /// </summary>
        public string country
        {
            set { _country = value; }
            get { return _country; }
        }


        public string _headimgurl;

        /// <summary>
        /// 用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空
        /// </summary>
        public string headimgurl
        {
            set { _headimgurl = value; }
            get { return _headimgurl; }
        }

        #endregion


        #region 成员方法

        /// <summary>
        /// 编号是否存在--新增的时候
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsCodeAdd(int shopId,string code)
        {
            bool flag = false;

            string sql = "select * from Client where code='" + code + "' and shopId='"+shopId+"' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }

            reader.Close();
            
            return flag;
        }

        /// <summary>
        /// 编号是否存在--修改的时候
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsCodeEdit(int id,int shopId, string code)
        {
            bool flag = false;

            string sql = "select * from Client where code='" + code + "' and id<>'" + id + "' and shopId='"+shopId+"' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            reader.Close();
            return flag;
        }

        /// <summary>
        /// 名称是否存在
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsNamesAdd(int shopId,string names)
        {
            bool flag = false;

            string sql = "select * from Client where names='" + names + "' and shopId='"+shopId+"' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }

            reader.Close();
            return flag;
        }

        /// <summary>
        /// 名称是否存在--修改的时候
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsNamesEdit(int shopId,string code, string names)
        {
            bool flag = false;

            string sql = "select * from Client where names='" + names + "' and code<>'" + code + "' and shopId='"+shopId+"' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }

            reader.Close();
            return flag;
        }

        #endregion



        #region 获取所有成员

        /// <summary>
        /// 获取所有成员
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModel()
        {
            string sql = "select *,code+ ' '+Names CodeName from viewClient order by code ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        /// <summary>
        /// 获得数据列表
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


        #region 获取所有成员-------视图的

        /// <summary>
        /// 获取所有成员-------视图的
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModelView(int id)
        {
            string sql = "select * from viewClient where id='"+id+"' order by code ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion
        
        
        #region 获取所有成员-------视图的

        /// <summary>
        /// 获取所有成员-------视图的
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModelView()
        {
            string sql = "select * from viewClient order by code ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


        #region 获取所有成员-------视图的

        /// <summary>
        /// 获取所有成员-------视图的
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModelView(string key)
        {
            string sql = "select * from viewClient where 1=1 ";

            if (key != "")
            {
                sql += " and names like'%" + key + "%'" +
                    " or  code like'%" + key + "%' "+
                    " or  tel like'%" + key + "%' " +
                    " or  remarks like'%" + key + "%' " +
                    " or  address like'%" + key + "%' ";
            }
            sql += " order by code ";


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


        #region 获取所有成员-------视图的

        /// <summary>
        /// 获取所有成员-------视图的
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModelView(int typeId,string key)
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
                sql += " and typeId='"+typeId+"'";
            }

            sql += " order by code ";


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion



        #region 获取成员-------通过编号

        /// <summary>
        /// 获取成员-------通过编号
        /// </summary>
        /// <returns></returns>
        public DataSet GetModelById(int Id)
        {
            string sql = "select * from Client where id='" + Id + "' ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion



        #region 新增成员信息
        /// <summary>
        /// 新增成员信息
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
            sql += @"  values(
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

                                )      select @@identity ";

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

        #region 修改成员信息
        /// <summary>
        /// 修改成员信息
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
                                                dizhi=@dizhi
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
                                       new SqlParameter("@dizhi",dizhi),
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

        #region 修改客户密码信息
        /// <summary>
        /// 修改客户密码信息
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public int UpdateClientPwd(int Id, string pwd,string pwds)
        {
            string sql = "update Client set pwd='" + pwd + "',pwds='" + pwds + "'  where Id='" + Id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);

        }
        #endregion

        #region 删除成员信息
        /// <summary>
        /// 删除成员信息
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            string sql = "delete from Client where id='" + Id + "' and flag<>'审核' ";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion


        #region 审核一条记录

        /// <summary>
        /// 审核一条记录
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
            if (flag == "审核")
            {
                sql += " ,checkId='" + chekerId + "',checkDate='" + checkDate + "'";
            }
            if (flag == "保存")
            {
                sql += " ,checkId=null,checkDate=null ";
            }

            sql += "  where id = '" + Id + "'";

            sql += " end ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);

        }

        #endregion

        #region 客户登陆系统
        /// <summary>
        /// 客户登陆系统
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

        #region 客户登陆系统
        /// <summary>
        /// 客户登陆系统
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

        #region 新增客户地址
        /// <summary>
        /// 新增客户地址
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public int AddClientAddress(int clientId, int proId,int ctId,int areaId,string address,string postCode,string names,string phone,string tel, int moren)
        {
            string sql = "";
            if (moren == 1)
            {
                sql += " update clientAddress set moren=0 where clientId='" + clientId + "' ";
            }

            sql += " insert into  clientAddress(clientId,proId,ctId,areaId,dizhi,postCode,names,phone,tel,moren) ";
            sql += " values('" + clientId 
                + "','" + proId
                + "','" + ctId 
                + "','" + areaId
                + "','" + address
                + "','" + postCode 
                + "','" + names
                + "','" + phone
                + "','" + tel
                + "','" + moren + "')  ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);



        }

        #endregion

        #region 新增客户地址
        /// <summary>
        /// 新增客户地址
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public int AddClientAddress(string openId, int proId, int ctId, int areaId, string address, string postCode, string names, string phone, string tel, int moren)
        {
            string sql = "";
            if (moren == 1)
            {
                sql += " update clientAddress set moren=0 where openId='" + openId + "' ";
            }

            sql += " insert into  clientAddress(openId,proId,ctId,areaId,dizhi,postCode,names,phone,tel,moren) ";
            sql += " values('" + openId
                + "','" + proId
                + "','" + ctId
                + "','" + areaId
                + "','" + address
                + "','" + postCode
                + "','" + names
                + "','" + phone
                + "','" + tel
                + "','" + moren + "')  ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);



        }

        #endregion


        #region 新增客户地址
        /// <summary>
        /// 新增客户地址
        /// </summary>
        /// <param name="goodsId"></param>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public int AddClientAddress(int clientId,string openId, int proId, int ctId, int areaId, string address, string postCode, string names, string phone, string tel, int moren)
        {
            string sql = "";
            if (moren == 1)
            {
                sql += " update clientAddress set moren=0 where openId='" + openId + "' and clientId='"+clientId+"' ";
            }

            sql += " insert into  clientAddress(clientId,openId,proId,ctId,areaId,dizhi,postCode,names,phone,tel,moren) ";
            sql += " values('"+clientId
                +"','" + openId
                + "','" + proId
                + "','" + ctId
                + "','" + areaId
                + "','" + address
                + "','" + postCode
                + "','" + names
                + "','" + phone
                + "','" + tel
                + "','" + moren + "')  ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);



        }

        #endregion


        #region 默认收货地址信息
        /// <summary>
        /// 默认收货地址信息
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int SetMorenAddress(int id, int clientId)
        {

            string sql = " update clientAddress set moren=0 where clientId='" + clientId + "' update  clientAddress set moren=1 where id='" + id + "' ";

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion

        #region 删除客户地址信息
        /// <summary>
        /// 删除客户地址信息
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int DeleteAddress(int id)
        {
            string sql = "delete from ClientAddress where id='" + id + "'  ";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion

        #region 查询送货地址

        /// <summary>
        /// 查询订单送货地址
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public DataSet GetClientAddress(int clientId)
        {
            string sql = "select *,'（'+names+' 收）'+proName+','+ctName+','+areaName+','+dizhi+' '+phone dizhiAll from viewClientAddress where clientId='" + clientId + "' ";
           
            sql += " order by moren desc ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


        #region 查询送货地址

        /// <summary>
        /// 查询订单送货地址
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public DataSet GetClientAddress(string openId)
        {
            string sql = "select *,'（'+names+' 收）'+proName+','+ctName+','+areaName+','+dizhi+' '+phone dizhiAll from viewClientAddress where openId='" + openId + "' ";

            sql += " order by moren desc ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion



        #region 查询送货地址

        /// <summary>
        /// 查询订单送货地址
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public DataSet GetClientAddressById(int id)
        {
            string sql = "select *,'（'+names+' 收）'+proName+','+ctName+','+areaName+','+dizhi+' '+phone dizhiAll from viewClientAddress where id='" + id + "' ";

            sql += " order by moren desc ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


        #region 获取成员-------通过微信号

        /// <summary>
        /// 获取成员-------通过微信号
        /// </summary>
        /// <returns></returns>
        public DataSet GetModelByWeixinOpenId(string openId)
        {
            string sql = " select * from viewClient where  openId='" + openId + "' ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


        

        #region 绑定微信消息
        /// <summary>
        /// 绑定微信消息
        /// </summary>
        /// <returns></returns>
        public int BindWeixin(int clientId,string fromUser,string weixin)
        {
            string id = "0";

            string sql = "insert into ClientWeixin(clientId,fromUser,weixin) values('" + clientId + "','" + fromUser + "','" + weixin + "')    select @@identity";

            SqlDataReader sdr = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            if (sdr.Read())
            {
                id = sdr[0].ToString();

            }

            return int.Parse(id);
        }

        #endregion


        #region 获取所有客户分类信息
        /// <summary>
        /// 获取所有客户分类信息--返回数据集
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

        #region 删除客户分类信息
        /// <summary>
        /// 删除客户分类信息
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int DeleteClientType(int Id)
        {
            string sql = "delete from ClientType where id='" + Id + "' ";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion



        //以下是微信平台系统


        #region 判断用户是否绑定

        /// <summary>
        /// 判断用户是否绑定、true 已绑定 false 未绑定
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public bool CheckWeixinBind(string openId)
        {
            bool isBind = false;

            string sql = "select id from client where openId='" + openId + "' ";
            DataSet ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                isBind = true;
            }
            return isBind;



        }



        #endregion


        #region 判断用户手机是否存在

        /// <summary>
        /// 判断用户手机是否存在
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckPhoneReg(string phone)
        {
            bool isBind = false;

            string sql = "select id from client where code='" + phone + "' ";
            DataSet ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                isBind = true;
            }
            return isBind;



        }



        #endregion


        #region 判断用户手机是否存在-----修改的时候

        /// <summary>
        /// 判断用户手机是否存在-----修改的时候
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckPhoneReg(int id, string phone)
        {
            bool isBind = false;

            string sql = "select id from client where code='" + phone + "' and id<>'" + id + "' ";
            DataSet ds = SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                isBind = true;
            }
            return isBind;



        }



        #endregion


        #region 热销排行

        /// <summary>
        /// 热销排行
        /// </summary>
        /// <param name="orderBy">排行：sumNum 按数量，sumPriceAll 按金额</param>
        /// <param name="desc">desc 畅销，asc 滞销</param>
        /// <returns></returns>
        public DataSet TopGoodsList(string orderBy, string desc)
        {
            string sql = @" select top 10  a.*,isnull(s.sumNum,0) sumNum,isnull(s.sumPriceAll,0) sumPriceAll
                                from viewClient a
                                left join
                                (
	                                select wlId,sum(sumNum) sumNum,sum(sumPriceAll) sumPriceAll
	                                from viewSalesOrderList
	                                group by wlId
                                )
                                s on a.id=s.wlId ";

            sql += " order by  " + orderBy + "   " + desc;





            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion


        #region 获取登录密码-------通过手机号

        /// <summary>
        /// 获取登录密码-------通过手机号
        /// </summary>
        /// <returns></returns>
        public DataSet GetPWDByPhone(string phone)
        {
            string sql = "select * from client where code='" + phone + "'";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


        #region 初始化密码
        /// <summary>
        /// 初始化密码
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public int ChangPassword(int id)
        {
            string sqlstr = "update client set pwd='123456',pwds='123456' where id='" + id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sqlstr, null);
        }

        #endregion


        #region 微信企业号---------方法

        #region 获取成员-------通过userid

        /// <summary>
        /// 获取成员-------通过userid
        /// </summary>
        /// <returns></returns>
        public DataSet GetModelByWeixinUserid(string userId)
        {
            string sql = " select * from client where  code='" + userId + "' ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #endregion




        #region 阿里钉钉---------方法

        #region 获取客户信息-------通过userid

        /// <summary>
        /// 获取成员-------通过userid
        /// </summary>
        /// <returns></returns>
        public DataSet GetModelByDingdingUserid(string userId)
        {
            string sql = " select * from client where  code='" + userId + "' ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #endregion


    }
}

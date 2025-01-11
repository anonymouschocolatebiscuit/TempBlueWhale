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
    public class VenderDAL
    {
        public VenderDAL()
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

            string sql = "select * from vender where code='" + code + "' and shopId='"+shopId+"' ";

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
        public bool isExistsCodeEdit(int id,int shopID, string code)
        {
            bool flag = false;

            string sql = "select * from vender where code='" + code + "' and id<>'" + id + "' and shopId='" + shopId + "'  ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
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

            string sql = "select * from vender where names='" + names + "' and shopId='" + shopId + "'  ";

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

            string sql = "select * from vender where names='" + names + "' and code<>'" + code + "' and shopId='" + shopId + "'  ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
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
            string sql = "select *,code+' '+names CodeName from viewVender order by code ";

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
            string sql = "select * from viewVender order by code ";

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
            string sql = "select * from viewVender where 1=1 ";
            if (key != "")
            {
                sql += " and names like'%" + key + "%' or  code like'%" + key + "%' or  linkMan like'%" + key + "%' ";
            }
            sql += " order by code ";


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #region 获取成员-------通过编号---ID

        /// <summary>
        /// 获取成员-------通过编号---ID
        /// </summary>
        /// <returns></returns>
        public DataSet GetModelByCode()
        {
            string sql = "select * from Vender where id='"+Id+"' ";

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

            //string sql = " insert into vender(shopId,code,names,TypeId,yueDate,payNeed,payReady,tax,remarks,makeDate,flag) ";
            //sql += "  values('" + ShopId + "','" + Code + "','" + Names + "','" + TypeId + "','" + YueDate + "','" + PayNeed + "','" + PayReady + "','" + Tax + "','" + Remarks + "',getdate(),'"+Flag+"')      select @@identity ";

            string sql = @" insert into vender
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
                                                flag
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
                                @flag
                                )      select @@identity ";

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
                                       new SqlParameter("@flag",flag)

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
            string sql="";
            if (!this.isExistsCodeEdit(Id,ShopId, Code))
            {
                //sql = "update vender set ShopId='" + ShopId + "',Names='" + Names + "',Code='" + Code + "',YueDate='" + YueDate + "',PayNeed='" + PayNeed + "',PayReady='" + PayReady + "',Tax='" + Tax + "',Remarks='" + Remarks + "',TypeId='" + TypeId + "' where Id='" + Id + "'";

                sql = @" update vender set
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

        #region 删除成员信息
        /// <summary>
        /// 删除成员信息
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            string sql = "delete from vender where id='" + Id + "' delete from venderLinkMan where pId='"+Id+"'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion


        #region 获取所有供应商分类信息
        /// <summary>
        /// 获取所有供应商分类信息--返回数据集
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllVenderType()
        {
            string sql = @"select a.*,isnull(num,0) num
                            from VenderType a
                            left join
                            (
                            select TypeId,count(*) num
                            from Vender
                            group by TypeId
                            ) b
                            on a.id=b.TypeId  order by flag desc ";
            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);

        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *,code+' '+names CodeName   ");
            strSql.Append(" FROM viewVender ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by code  ");

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, strSql.ToString(), null);
        }


        #endregion

        #region 删除供应商分类信息
        /// <summary>
        /// 删除供应商分类信息
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int DeleteVenderType(int Id)
        {
            string sql = "delete from VenderType where id='" + Id + "' ";
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
            string sql = " if not exists(select * from vender where flag='" + flag + "' and id='" + Id + "') ";
            sql += " begin ";

            sql += " update vender set flag='" + flag + "' ";
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

    }
}

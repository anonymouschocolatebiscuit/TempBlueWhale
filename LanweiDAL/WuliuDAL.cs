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
    public class WuliuDAL
    {
        public WuliuDAL()
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

        private string linkMan;
        public string LinkMan
        {
            get { return linkMan; }
            set { linkMan = value; }
        }

        private string tel;
        public string Tel
        {
            get { return tel; }
            set { tel = value; }
        }

        private string phone;
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        private string fax;
        public string Fax
        {
            get { return fax; }
            set { fax = value; }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        private string mall;
        public string Mall
        {
            get { return mall; }
            set { mall = value; }
        }

        private string printModel;
        public string PrintModel
        {
            get { return printModel; }
            set { printModel = value; }
        }

        private int makeId;
        public int MakeId
        {
            get { return makeId; }
            set { makeId = value; }
        }

        private DateTime makeDate;
        public DateTime MakeDate
        {
            get { return makeDate; }
            set { makeDate = value; }
        }


        #endregion


        #region  成员方法

        /// <summary>
        /// 编号是否存在--新增的时候
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsCodeAdd(int shopId,string code)
        {
            bool flag = false;

            string sql = "select * from Wuliu where code='" + code + "' and shopId='" + shopId + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr,CommandType.Text,sql,null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// 编号是否存在--修改的时候
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsCodeEdit(int id,int shopId,string code)
        {
            bool flag = false;

            string sql = "select * from Wuliu where code='" + code + "' and id<>'" + id + "' and shopId='" + shopId + "'  ";

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

            string sql = "select * from Wuliu where names='" + names + "' and shopId='" + shopId + "'  ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// 名称是否存在--修改的时候
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsNamesEdit(int id,int shopId, string names)
        {
            bool flag = false;

            string sql = "select * from Wuliu where names='" + names + "' and id<>'" + id + "' and shopId='" + shopId + "'  ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }

      
      


        #endregion

        #region 获取所有信息

        /// <summary>
        /// 获取所有信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetALLModelList()
        {
            string sql = "select * from viewWuliu order by code  ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *   ");
            strSql.Append(" FROM viewWuliu ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by code  ");

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, strSql.ToString(), null);
        }


        #endregion

        #region 获取所有信息

        /// <summary>
        /// 获取所有信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetWuliuCodeList()
        {
            string sql = "select * from WuliuCodeList order by code  ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion


        #region 新增一条信息
        /// <summary>
        /// 新增一条信息
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string sql = @"insert into Wuliu(shopId,code,names,linkMan,tel,phone,fax,address,mall,printModel,makeId,makeDate) 

                                      values(@shopId,@code,@names,@linkMan,@tel,@phone,@fax,@address,@mall,@printModel,@makeId,getdate()) ";
            SqlParameter[] param = {
                                       new SqlParameter("@ShopId",ShopId),
                                       new SqlParameter("@code",Code),
                                       new SqlParameter("@Names",Names),
                                       new SqlParameter("@LinkMan",LinkMan),
                                       new SqlParameter("@Tel",Tel),
                                       new SqlParameter("@Phone",Phone),
                                       new SqlParameter("@Fax",Fax),
                                       new SqlParameter("@Address",Address),
                                       new SqlParameter("@Mall",Mall),
                                       new SqlParameter("@PrintModel",PrintModel),
                                       new SqlParameter("@MakeId",MakeId)

                                   };


            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #region 修改一条信息
        /// <summary>
        /// 修改一条信息
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public int Update()
        {
            string sql="";

            if (!this.isExistsCodeEdit(Id,ShopId, Code) && !this.isExistsNamesEdit(Id,ShopId, Names))
            {
               
                sql = @"UPDATE wuliu
                                    SET code = @code
                                      ,shopId=@shopId
                                      ,names = @names
                                      ,linkMan = @linkMan
                                      ,tel = @tel
                                      ,phone = @phone
                                      ,fax = @fax
                                      ,address = @address
                                      ,mall = @mall
                                      ,printModel = @printModel
                                  
                                    WHERE id=@id ";


                SqlParameter[] param = {
                                           new SqlParameter("@ShopId",ShopId),
                                       new SqlParameter("@code",Code),
                                       new SqlParameter("@Names",Names),
                                       new SqlParameter("@LinkMan",LinkMan),
                                       new SqlParameter("@Tel",Tel),
                                       new SqlParameter("@Phone",Phone),
                                       new SqlParameter("@Fax",Fax),
                                       new SqlParameter("@Address",Address),
                                       new SqlParameter("@Mall",Mall),
                                       new SqlParameter("@PrintModel",PrintModel),

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

        #region 获取所有成员-------视图的

        /// <summary>
        /// 获取所有成员-------视图的
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllModelView(string key)
        {
            string sql = "select * from Wuliu where 1=1 ";
            if (key != "")
            {
                sql += " and names like'%" + key + "%' or  code like'%" + key + "%' ";
            }
            sql += " order by code ";


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #region 获取物流公司名称-------通过ID

        /// <summary>
        /// 获取物流公司名称-------通过ID
        /// </summary>
        /// <returns></returns>
        public DataSet GetModelById(int id)
        {
            string sql = "select * from wuliu where id='" + id + "' ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion
      

        #region 删除一条信息
        /// <summary>
        /// 删除一条信息
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            string sql = "delete from Wuliu where id='" + Id + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion

        #region 通过名称获取ID
        /// <summary>
        /// 通过名称获取ID
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public int GetIdByName(int shopId,string names)
        {
            int id = 0;
            string sql = "select * from Wuliu where names='" + names + "' and shopId='" + shopId + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                id = Convert.ToInt32(reader["id"].ToString());
            }
            return id;
        }
        #endregion
    }
}

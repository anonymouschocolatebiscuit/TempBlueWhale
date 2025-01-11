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
    public class ShopDAL
    {
        public ShopDAL()
        {

        }
        #region 成员字段



        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
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

    

        private string tel;
        public string Tel
        {
            get { return tel; }
            set { tel = value; }
        }

        private string fax;
        public string Fax
        {
            get { return fax ; }
            set { fax  = value; }
        }

    
        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

   
        private DateTime makeDate;
        public DateTime MakeDate
        {
            get { return makeDate; }
            set { makeDate = value; }
        }

        private string flag;
        public string Flag
        {
            get { return flag; }
            set { flag = value; }
        }

      

        #endregion


        #region  成员方法

        /// <summary>
        /// 名称是否存在
        /// </summary>
        /// <param name="ShopName"></param>
        /// <returns></returns>
        public bool isExistsNames(string ShopName)
        {
            bool flag = false;
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@names", SqlDbType.VarChar) };

            string sql = "select * from shop where names=@names ";

            parameters[0].Value = ShopName;
            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr,CommandType.Text,sql,parameters);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }


        #region  成员方法

        /// <summary>
        /// 编号是否存在--新增的时候
        /// </summary>
        /// <param name="ShopName"></param>
        /// <returns></returns>
        public bool isExistsCodeAdd(string code)
        {
            bool flag = false;

            string sql = "select * from shop where code='" + code + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// 编号是否存在--修改的时候
        /// </summary>
        /// <param name="ShopName"></param>
        /// <returns></returns>
        public bool isExistsCodeEdit(int id, string code)
        {
            bool flag = false;

            string sql = "select * from shop where code='" + code + "' and id<>'" + id + "' ";

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
        /// <param name="ShopName"></param>
        /// <returns></returns>
        public bool isExistsNamesAdd(string names)
        {
            bool flag = false;

            string sql = "select * from shop where names='" + names + "' ";

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
        /// <param name="ShopName"></param>
        /// <returns></returns>
        public bool isExistsNamesEdit(int id, string names)
        {
            bool flag = false;

            string sql = "select * from shop where names='" + names + "' and id<>'" + id + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }



        #endregion



        #endregion

        #region 获取所有分店

        /// <summary>
        /// 获取所有分店
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllShopList()
        {
            string sql = "select * from shop order by code  ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #region 获取所有分店

        /// <summary>
        /// 获取所有分店
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllShopList(int id)
        {
            string sql = "select * from shop where id='"+id+"' order by id desc ";

            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }


        #endregion

        #region 新增分店信息
        /// <summary>
        /// 新增分店信息
        /// </summary>
        /// <returns></returns>
        public int Add()
        {
            string sql = @"insert into Shop(code,names,address,tel,fax,makeDate,flag) 
                      values(@code,@names,@address,@tel,@fax,@makeDate,@flag)";
            SqlParameter[] param = {
                                       new SqlParameter("@Code",Code),
                                       new SqlParameter("@Names",Names),
                                       new SqlParameter("@Address",Address),
                                       new SqlParameter("@Tel",Tel),
                                       new SqlParameter("@Fax",Fax),
                                       new SqlParameter("@makeDate",MakeDate),
                                       new SqlParameter("@flag",Flag)
                                     
                                   };
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);


          
        }

        #endregion

        #region 修改分店信息
        /// <summary>
        /// 修改分店信息
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public int Update()
        {
            string sql = @"UPDATE Shop
                                   SET code = @code
                                      ,names = @names
                                    
                                      ,tel = @tel
                                      ,fax = @fax
                                   
                                      ,address = @address
                                      ,flag = @flag
                                 

                                 WHERE id=@id ";

            SqlParameter[] param = {
                                       new SqlParameter("@Code",Code),
                                       new SqlParameter("@Names",Names),
                                       new SqlParameter("@Address",Address),
                                       new SqlParameter("@Tel",Tel),
                                       new SqlParameter("@Fax",Fax),
                                      
                                       new SqlParameter("@flag",Flag),

                                       new SqlParameter("@Id",Id)

                                   };
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);

           

        }
        #endregion

   
        #region 修改分店状态
        /// <summary>
        /// 修改分店状态
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public int UpdateFlag(int Id)
        {
            string sqls = "if exists(select * from Shop where id='"+Id+"' and flag='启用') ";
            sqls += " update Shop set flag='禁用' where id='"+Id+"'";

            sqls += " else update Shop set flag='启用' where id='"+Id+"'";

         
            
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sqls, null);

        }
        #endregion

        #region 删除分店信息
        /// <summary>
        /// 删除分店信息
        /// </summary>
        /// <param name="fId"></param>
        /// <returns></returns>
        public int Delete(int Id)
        {
            string sql = "delete from Shop where id='" + Id + "' and flag='禁用' ";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion



    }
}

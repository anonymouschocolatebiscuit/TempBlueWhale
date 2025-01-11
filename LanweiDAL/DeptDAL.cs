using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lanwei.Weixin.DBUtility;
using System.Data;
using System.Data.SqlClient;
using Lanwei.Weixin.Model;

namespace Lanwei.Weixin.DAL
{
    public class DeptDAL
    {


        #region 成员字段



        private int deptId;
        public int DeptId
        {
            get { return deptId; }
            set { deptId = value; }
        }


        private int flag;
        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }


        private string deptName;
        public string DeptName
        {
            get { return deptName; }
            set { deptName = value; }
        }

        private int parentId;
        public int ParentId
        {
            get { return parentId; }
            set { parentId = value; }
        }


        #endregion


        #region  成员方法


        /// <summary>
        /// 名称是否存在
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool isExistsNamesAdd(string names)
        {
            bool flag = false;

            string sql = "select * from dept where deptName='" + names + "' ";

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
        public bool isExistsNamesEdit(int id, string names)
        {
            bool flag = false;

            string sql = "select * from dept where deptName='" + names + "' and deptId<>'" + id + "' ";

            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            while (reader.Read())
            {
                flag = true;
            }
            return flag;
        }





        #endregion



        #region 判断角色部门是否存在
        /// <summary>
        /// 判断部门是否存在
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public int CheckDeptExsits(string names)
        {
            string sql = "select deptId from dept where deptName='" + names + "'";
            int i = 0;
            SqlDataReader sdr = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            if (sdr.Read())
            {
                i = 1;

            }
            return i;
        }
        #endregion

        #region 获取所有部门信息 填充到TreeView
        /// <summary>
        /// 获取所有部门信息--填充到TreeView
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<DeptInfo> GetAllDeptTreeView(int parentId)
        {
            string sql = "select * from dept where parentId='" + parentId + "'";
            using (SqlDataReader read = SQLHelper.ExecuteReader(SQLHelper.ConStr, System.Data.CommandType.Text, sql, null))
            {
                List<DeptInfo> list = new List<DeptInfo>();
                while (read.Read())
                {
                    DeptInfo s = new DeptInfo();

                    s.DeptId = read.GetInt32(0);
                    s.DeptName = read.GetString(1);
                    s.ParentId = read.GetInt32(2);
                    s.Flag = read.GetInt32(3);
                    list.Add(s);
                }
                return list;
            }

        }
        #endregion

        #region 获取所有部门信息
        /// <summary>
        /// 获取所有部门信息--返回数据集
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllDept()
        {
           // string sql = "select * from dept";

            string sql = @"select a.*,isnull(num,0) num
                            from viewDept a
                            left join
                            (
                            select deptId,count(*) num
                            from users
                            group by deptId
                            ) b
                            on a.deptId=b.deptId  order by flag ";


            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);

        }

        #endregion

        #region 查询部门信息---通过部门编号
        /// <summary>
        /// 查询部门信息---通过部门编号
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public DataSet GetDeptInfo(int deptId)
        {
            string sql = "select * from Dept where deptId='" + deptId + "'";
            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region 新增部门信息
        /// <summary>
        /// 新增部门信息
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="deptName"></param>
        /// <returns></returns>
        public int AddDept(int parentId, string deptName,int flag)
        {
            string sql = "insert into dept(deptName,parentId,flag) values('" + deptName + "','" + parentId + "','"+flag+"') ";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion


        #region 新增部门信息
        /// <summary>
        /// 新增部门信息
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="deptName"></param>
        /// <returns></returns>
        public int Add()
        {
            string sql = "insert into dept(deptName,parentId,flag) values('" + DeptName + "','" + ParentId + "','" + Flag + "') ";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion



        #region 修改部门信息
        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="deptId"></param>
        /// <param name="deptName"></param>
        /// <param name="parentId"></param>     
        /// <returns></returns>
        public int Update()
        {
            string sql = "update dept set deptName='" + DeptName + "',parentId='" + ParentId + "',flag='" + Flag + "'  where deptId='" + DeptId + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);

        }
        #endregion

        #region 删除部门信息
        public int Delete(int deptId)
        {
            string sql = "delete from dept where deptId='" + deptId + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);


        }

        #endregion


        #region 微信企业号

        #region 初始化部门信息
        /// <summary>
        /// 初始化部门信息
        /// </summary>
        /// <returns></returns>
        public int TruncateDept()
        {
            string sql = @" truncate table dept ";          

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region 新增部门信息
        /// <summary>
        /// 新增部门信息
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="deptName"></param>
        /// <returns></returns>
        public int AddDeptQY(int deptId,int parentId, string deptName, int flag)
        {
            string sql = @" set IDENTITY_INSERT dept on
                            insert into dept(deptId,deptName,parentId,flag) values(@deptId,@deptName,@parentId,@flag) 
                           
                            set IDENTITY_INSERT dept  off  ";

            SqlParameter[] param = {
                                       new SqlParameter("@deptId",deptId),
                                       new SqlParameter("@parentId",parentId),
                                       new SqlParameter("@deptName",deptName),
                                       new SqlParameter("@flag",flag)
                                   };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion

        #endregion
    }
}

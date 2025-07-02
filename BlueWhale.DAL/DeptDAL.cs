using BlueWhale.DBUtility;
using BlueWhale.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BlueWhale.DAL
{
    public class DeptDAL
    {
        #region Fields and Properties

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

        #region Validation Methods

        public bool isExistsNamesAdd(string names)
        {
            string sql = "select * from dept where deptName='" + names + "'";
            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            return reader.Read();
        }

        public bool isExistsNamesEdit(int id, string names)
        {
            string sql = "select * from dept where deptName='" + names + "' and deptId!='" + id + "'";
            SqlDataReader reader = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            return reader.Read();
        }

        public int CheckDeptExsits(string names)
        {
            string sql = "select deptId from dept where deptName='" + names + "'";
            SqlDataReader sdr = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null);
            return sdr.Read() ? 1 : 0;
        }

        #endregion

        #region Retrieval Methods

        public List<DeptInfo> GetAllDeptTreeView(int parentId)
        {
            string sql = "select * from dept where parentId='" + parentId + "'";
            using (SqlDataReader read = SQLHelper.ExecuteReader(SQLHelper.ConStr, CommandType.Text, sql, null))
            {
                List<DeptInfo> list = new List<DeptInfo>();
                while (read.Read())
                {
                    DeptInfo s = new DeptInfo
                    {
                        DeptId = read.GetInt32(0),
                        DeptName = read.GetString(1),
                        ParentId = read.GetInt32(2),
                        Flag = read.GetInt32(3)
                    };
                    list.Add(s);
                }
                return list;
            }
        }

        public DataSet GetAllDept()
        {
            string sql = @"select a.*, isnull(num,0) num
                            from viewDept a
                            left join
                            (
                                select deptId, count(*) num
                                from users
                                group by deptId
                            ) b on a.deptId = b.deptId
                            order by flag";
            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        public DataSet GetDeptInfo(int deptId)
        {
            string sql = "select * from Dept where deptId='" + deptId + "'";
            return SQLHelper.SqlDataAdapter(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region CRUD Operations

        public int AddDept(int parentId, string deptName, int flag)
        {
            string sql = "insert into dept(deptName, parentId, flag) values('" + deptName + "', '" + parentId + "', '" + flag + "')";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        public int Add()
        {
            string sql = "insert into dept(deptName, parentId, flag) values('" + DeptName + "', '" + ParentId + "', '" + Flag + "')";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        public int Update()
        {
            string sql = "update dept set deptName='" + DeptName + "', parentId='" + ParentId + "', flag='" + Flag + "' where deptId='" + DeptId + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        public int Delete(int deptId)
        {
            string sql = "delete from dept where deptId='" + deptId + "'";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        #endregion

        #region WeChat Enterprise Features

        public int TruncateDept()
        {
            string sql = "truncate table dept";
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, null);
        }

        public int AddDeptQY(int deptId, int parentId, string deptName, int flag)
        {
            string sql = @"set IDENTITY_INSERT dept on
                            insert into dept(deptId, deptName, parentId, flag) values(@deptId, @deptName, @parentId, @flag)
                            set IDENTITY_INSERT dept off";

            SqlParameter[] param = {
                new SqlParameter("@deptId", deptId),
                new SqlParameter("@parentId", parentId),
                new SqlParameter("@deptName", deptName),
                new SqlParameter("@flag", flag)
            };

            return SQLHelper.ExecuteNonQuery(SQLHelper.ConStr, CommandType.Text, sql, param);
        }

        #endregion
    }
}

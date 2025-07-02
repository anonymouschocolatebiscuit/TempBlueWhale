using System;

namespace BlueWhale.Model
{
    public class Users
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string appId;
        public string AppId
        {
            get { return appId; }
            set { appId = value; }
        }

        private string appSecret;
        public string AppSecret
        {
            get { return appSecret; }
            set { appSecret = value; }
        }

        private string userSecret;
        public string UserSecret
        {
            get { return userSecret; }
            set { userSecret = value; }
        }

        private string weixinId;
        public string WeixinId
        {
            get { return weixinId; }
            set { weixinId = value; }
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

        private int shopId;
        public int ShopId
        {
            get { return shopId; }
            set { shopId = value; }
        }

        private string shopName;
        public string ShopName
        {
            get { return shopName; }
            set { shopName = value; }
        }

        private string loginName;
        public string LoginName
        {
            get { return loginName; }
            set { loginName = value; }
        }

        private int roleId;
        public int RoleId
        {
            get { return roleId; }
            set { roleId = value; }
        }

        private string roleName;
        public string RoleName
        {
            get { return roleName; }
            set { roleName = value; }
        }

        private string deptName;
        public string DeptName
        {
            get { return deptName; }
            set { deptName = value; }
        }

        private string pwd;
        public string Pwd
        {
            get { return pwd; }
            set { pwd = value; }
        }

        private string pwds;
        public string Pwds
        {
            get { return pwds; }
            set { pwds = value; }
        }

        private string names;
        public string Names
        {
            get { return names; }
            set { names = value; }
        }

        private string phone;
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
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
    }
}

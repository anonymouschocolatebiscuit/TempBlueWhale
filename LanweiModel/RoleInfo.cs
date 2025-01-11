using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanwei.Weixin.Model
{
    public class RoleInfo
    {
        private int roleId;
        public int RoleId
        {
            get { return roleId; }
            set { roleId = value; }
        }

        private int parentId;
        public int ParentId
        {
            get { return parentId; }
            set { parentId = value; }
        }

        private string roleName;
        public string RoleName
        {
            get { return roleName; }
            set { roleName = value; }
        }

        private int flag;
        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }
    }
}

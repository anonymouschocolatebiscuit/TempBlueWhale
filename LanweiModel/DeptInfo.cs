using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanwei.Weixin.Model
{
    public class DeptInfo
    {
        private int deptId;
        public int DeptId
        {
            get { return deptId; }
            set { deptId = value; }
        }

        private int parentId;
        public int ParentId
        {
            get { return parentId; }
            set { parentId = value; }
        }

        private string deptName;
        public string DeptName
        {
            get { return deptName; }
            set { deptName = value; }
        }

        private int flag;
        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }
    }
}


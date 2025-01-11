using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanwei.Weixin.Model
{
    public class GoodsTypeInfo
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

      
        private string typeName;
        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }

        private int parentId;
        public int ParentId
        {
            get { return parentId; }
            set { parentId = value; }
        }
        private int seq;
        public int Seq
        {
            get { return seq; }
            set { seq = value; }
        }

    }
}

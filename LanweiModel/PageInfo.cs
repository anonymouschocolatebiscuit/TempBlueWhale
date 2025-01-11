using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanwei.Weixin.Model
{
    public class PageInfo
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private int parentId;
        public int ParentId
        {
            get { return parentId; }
            set { parentId = value; }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string url;
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

       

        private int seq;
        public int Seq
        {
            get { return seq; }
            set { seq = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanwei.Weixin.Model
{
    public class LotteryGGK
    {
        
        
        private string _prize;
        private int _v;
        private int _num;
        private int _id;
        private int _groupId;

        /// <summary>
        /// 编号
        /// </summary>
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 编号
        /// </summary>
        public int groupId
        {
            get { return _groupId; }
            set { _groupId = value; }
        }


        /// <summary>
        /// 奖项名称
        /// </summary>
        public string prize
        {
            get { return _prize; }
            set { _prize = value; }
        }

        /// <summary>
        /// 中奖概率
        /// </summary>
        public int v
        {
            get { return _v; }
            set { _v = value; }
        }

        /// <summary>
        /// 奖品数量
        /// </summary>
        public int num
        {
            get { return _num; }
            set { _num = value; }
        }
    }
}

using System;
namespace Lanwei.Weixin.Model
{
	/// <summary>
	/// wx_wxmall_goods_order:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class wx_wxmall_goods_order
	{
		public wx_wxmall_goods_order()
		{}
		#region Model
		private long _id;
		private string _ordernum;
		private string _openId;
		private decimal? _totalmoney;
		private int? _ispay;
		private int? _isuserdone;
		private int? _iscomdone;

		private DateTime? _addtime= DateTime.Now;
		private DateTime? _paytime;
		private string _addressnum;
		private string _addresstext;
		private int? _payway;
		private int? _isexpress;
		private string _expressname;
		private string _expressnum;
        private string _remarks;

		private DateTime? _expresstime;
		private DateTime? _comdonetime;
		/// <summary>
		/// 
		/// </summary>
		public long id
		{
			set{ _id=value;}
			get{return _id;}
		}

		/// <summary>
		/// 
		/// </summary>
		public string orderNum
		{
			set{ _ordernum=value;}
			get{return _ordernum;}
		}

        private string _payNumber;

        /// <summary>
		/// 
		/// </summary>
        public string payNumber
		{
            set { _payNumber = value; }
            get { return _payNumber; }
		}

        


		/// <summary>
		/// 
		/// </summary>
        public string openId
		{
            set { _openId = value; }
            get { return _openId; }
		}


        private int _totalNum;

        public int totalNum
        {
            set { _totalNum = value; }
            get { return _totalNum; }
        }

		/// <summary>
		/// 
		/// </summary>
		public decimal? totalMoney
		{
			set{ _totalmoney=value;}
			get{return _totalmoney;}
		}

        /// <summary>
        /// 最终价格
        /// </summary>
        public decimal? totalMoneyAll { set; get;}


		/// <summary>
		/// 
		/// </summary>
		public int? isPay
		{
			set{ _ispay=value;}
			get{return _ispay;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? isUserDone
		{
			set{ _isuserdone=value;}
			get{return _isuserdone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? isComDone
		{
			set{ _iscomdone=value;}
			get{return _iscomdone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? addTime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? payTime
		{
			set{ _paytime=value;}
			get{return _paytime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string addressNum
		{
			set{ _addressnum=value;}
			get{return _addressnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string addressText
		{
			set{ _addresstext=value;}
			get{return _addresstext;}
		}

        /// <summary>
		/// 
		/// </summary>
        public string remarks
		{
            set { _remarks = value; }
            get { return _remarks; }
		}

        


		/// <summary>
		/// 
		/// </summary>
		public int? payWay
		{
			set{ _payway=value;}
			get{return _payway;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? isExpress
		{
			set{ _isexpress=value;}
			get{return _isexpress;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string expressName
		{
			set{ _expressname=value;}
			get{return _expressname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string expressNum
		{
			set{ _expressnum=value;}
			get{return _expressnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? expressTime
		{
			set{ _expresstime=value;}
			get{return _expresstime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? comDoneTime
		{
			set{ _comdonetime=value;}
			get{return _comdonetime;}
		}

        public DateTime? userDoneTime
		{
            set;
            get;
		}
        


        /// <summary>
        /// 
        /// </summary>
        public int isWrite
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime writeTime
        {
            set;
            get;
        }




        /// <summary>
        /// 
        /// </summary>
        public int isFinish
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime finishTime
        {
            set;
            get;
        }


        public int isKaipiao { set; get; }

        public int isYuejie { set; get; }

        public int yuejieDays { set; get; }


		#endregion Model

	}
}


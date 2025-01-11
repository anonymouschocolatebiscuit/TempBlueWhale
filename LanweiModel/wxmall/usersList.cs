using System;
namespace Lanwei.Weixin.Model
{
	/// <summary>
	/// usersList:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class usersList
	{
		public usersList()
		{}
		#region Model
		private int _id;
		private string _openid;
		private string _nickname;
		private int? _gender;
		private string _province;
		private string _country;
		private string _avatarurl;
		private DateTime? _makedate= DateTime.Now;
		private string _fromwhere;
		private string _location;
		private string _locationx;
		private string _locationy;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string openId
		{
			set{ _openid=value;}
			get{return _openid;}
		}

		/// <summary>
		/// 
		/// </summary>
		public string nickName
		{
			set{ _nickname=value;}
			get{return _nickname;}
		}

        public string cardNumber { set; get; }

        private string _names;

        /// <summary>
        /// 
        /// </summary>
        public string names
        {
            set { _names = value; }
            get { return _names; }
        }

        private string _phone;

        /// <summary>
        /// 
        /// </summary>
        public string phone
        {
            set { _phone = value; }
            get { return _phone; }
        }


		/// <summary>
		/// 
		/// </summary>
		public int? gender
		{
			set{ _gender=value;}
			get{return _gender;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string province
		{
			set{ _province=value;}
			get{return _province;}
		}


		/// <summary>
		/// 
		/// </summary>
		public string country
		{
			set{ _country=value;}
			get{return _country;}
		}

        public string _city;

        /// <summary>
        /// 
        /// </summary>
        public string city
        {
            set { _city = value; }
            get { return _city; }
        }


		/// <summary>
		/// 
		/// </summary>
		public string avatarUrl
		{
			set{ _avatarurl=value;}
			get{return _avatarurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? makeDate
		{
			set{ _makedate=value;}
			get{return _makedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string fromWhere
		{
			set{ _fromwhere=value;}
			get{return _fromwhere;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string location
		{
			set{ _location=value;}
			get{return _location;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string locationX
		{
			set{ _locationx=value;}
			get{return _locationx;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string locationY
		{
			set{ _locationy=value;}
			get{return _locationy;}
		}
		#endregion Model

	}
}


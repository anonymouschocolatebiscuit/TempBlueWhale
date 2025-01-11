using System;
namespace Lanwei.Weixin.Model
{
	/// <summary>
	/// wx_wxmall_users_address:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class wx_wxmall_users_address
	{
		public wx_wxmall_users_address()
		{}
		#region Model
		private int _id;
        private string _openId;
		private string _username;
		private string _phone;
		private string _province;
		private string _codeprovince;
		private string _city;
		private string _codecity;
		private string _country;
		private string _codecountry;
		private string _addressdetail;
		private int? _flag=0;
		private DateTime? _makedate= DateTime.Now;
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
            set { _openId = value; }
            get { return _openId; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string username
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string phone
		{
			set{ _phone=value;}
			get{return _phone;}
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
		public string codeProvince
		{
			set{ _codeprovince=value;}
			get{return _codeprovince;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string city
		{
			set{ _city=value;}
			get{return _city;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string codeCity
		{
			set{ _codecity=value;}
			get{return _codecity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string country
		{
			set{ _country=value;}
			get{return _country;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string codeCountry
		{
			set{ _codecountry=value;}
			get{return _codecountry;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string addressDetail
		{
			set{ _addressdetail=value;}
			get{return _addressdetail;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? flag
		{
			set{ _flag=value;}
			get{return _flag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? makeDate
		{
			set{ _makedate=value;}
			get{return _makedate;}
		}
		#endregion Model

	}
}


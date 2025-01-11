using System;
namespace Lanwei.Weixin.Model
{
	/// <summary>
	/// wxOpenList:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class wxOpenList
	{
		public wxOpenList()
		{}
		#region Model
		private int _id;
		private int? _shopid;
		private string _appname;
		private string _appid;
		private string _appsecret;
		private string _mchid;
		private string _appkey;
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
		public int? shopId
		{
			set{ _shopid=value;}
			get{return _shopid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string appName
		{
			set{ _appname=value;}
			get{return _appname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string appId
		{
			set{ _appid=value;}
			get{return _appid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string appSecret
		{
			set{ _appsecret=value;}
			get{return _appsecret;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string mchId
		{
			set{ _mchid=value;}
			get{return _mchid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string appKey
		{
			set{ _appkey=value;}
			get{return _appkey;}
		}
		#endregion Model

	}
}


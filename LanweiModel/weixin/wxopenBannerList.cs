using System;
namespace Lanwei.Weixin.Model
{
	/// <summary>
	/// wxopenBannerList:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class wxopenBannerList
	{
		public wxopenBannerList()
		{}
		#region Model
		private int _id;
		private int? _wid;
		private string _title;
		private string _picurl;
		private int? _sortid;
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
		public int? wId
		{
			set{ _wid=value;}
			get{return _wid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string picUrl
		{
			set{ _picurl=value;}
			get{return _picurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? sortId
		{
			set{ _sortid=value;}
			get{return _sortid;}
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


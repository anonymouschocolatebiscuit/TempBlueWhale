using System;
namespace Lanwei.Weixin.Model
{
	/// <summary>
	/// goodsBomListType:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class goodsBomListType
	{
		public goodsBomListType()
		{}
		#region Model
		private int _id;
		private int? _shopid;
		private string _names;
		private int? _sortid;
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
		public string names
		{
			set{ _names=value;}
			get{return _names;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? sortId
		{
			set{ _sortid=value;}
			get{return _sortid;}
		}
		#endregion Model

	}
}


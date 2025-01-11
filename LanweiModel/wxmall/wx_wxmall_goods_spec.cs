using System;
namespace Lanwei.Weixin.Model
{
	/// <summary>
	/// wx_wxmall_goods_spec:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class wx_wxmall_goods_spec
	{
		public wx_wxmall_goods_spec()
		{}
		#region Model
		private int _id;
		private int? _goodsid;
		private string _names;
		private string _picurl;
		private decimal? _price;
		private int? _isstop=0;
		private int? _sortid=0;
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
		public int? goodsId
		{
			set{ _goodsid=value;}
			get{return _goodsid;}
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
		public string picUrl
		{
			set{ _picurl=value;}
			get{return _picurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? isStop
		{
			set{ _isstop=value;}
			get{return _isstop;}
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


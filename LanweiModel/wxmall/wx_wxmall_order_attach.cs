using System;
namespace Lanwei.Weixin.Model
{
	/// <summary>
	/// wx_wxmall_order_attach:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class wx_wxmall_order_attach
	{
		public wx_wxmall_order_attach()
		{}
		#region Model
		private long _id;
		private int? _orderid;
		private int? _attachid;
		private string _filepath;
		private DateTime? _makedate= DateTime.Now;
		private string _flag="已通过";
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
		public int? orderId
		{
			set{ _orderid=value;}
			get{return _orderid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? attachId
		{
			set{ _attachid=value;}
			get{return _attachid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string filePath
		{
			set{ _filepath=value;}
			get{return _filepath;}
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
		public string flag
		{
			set{ _flag=value;}
			get{return _flag;}
		}
		#endregion Model

	}
}


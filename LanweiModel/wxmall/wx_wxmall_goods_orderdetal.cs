using System;
namespace Lanwei.Weixin.Model
{
	/// <summary>
	/// wx_wxmall_goods_orderdetal:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class wx_wxmall_goods_orderdetal
	{
		public wx_wxmall_goods_orderdetal()
		{}
		#region Model
		private long _id;
		private int _orderid;
		private int? _goodsid;
		private decimal? _price;
		private int? _numbers;
		private decimal? _totalmoney;
		private int? _amountid;
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
		public int orderId
		{
			set{ _orderid=value;}
			get{return _orderid;}
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
		public decimal? price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? numbers
		{
			set{ _numbers=value;}
			get{return _numbers;}
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
		/// 
		/// </summary>
		public int? amountId
		{
			set{ _amountid=value;}
			get{return _amountid;}
		}
		#endregion Model

	}
}


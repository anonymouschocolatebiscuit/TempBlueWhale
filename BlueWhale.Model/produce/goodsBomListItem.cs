using System;
namespace BlueWhale.Model.produce
{
    /// <summary>
    /// goodsBomListItem:Entity class (attribute description automatically extracts description information of database fields)
    /// </summary>
    [Serializable]
	public partial class goodsBomListItem
	{
		public goodsBomListItem()
		{}
		#region Model
		private int _id;
		private int? _pid;
		private int? _itemid;
		private int? _goodsid;
		private decimal? _num;
		private decimal? _rate;
		private string _remarks;
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
		public int? pId
		{
			set{ _pid=value;}
			get{return _pid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? itemId
		{
			set{ _itemid=value;}
			get{return _itemid;}
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
		public decimal? num
		{
			set{ _num=value;}
			get{return _num;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? rate
		{
			set{ _rate=value;}
			get{return _rate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string remarks
		{
			set{ _remarks=value;}
			get{return _remarks;}
		}
		#endregion Model

	}
}


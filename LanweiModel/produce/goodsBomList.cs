using System;
namespace Lanwei.Weixin.Model
{
	/// <summary>
	/// goodsBomList:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class goodsBomList
	{
		public goodsBomList()
		{}
		#region Model
		private int _id;
		private int? _shopid;
		private int? _typeid;
		private string _number;
		private string _edition;
		private string _flaguse;
		private string _flagcheck;
		private string _tuhao;
		private int? _goodsid;
		private decimal? _num;
		private decimal? _rate;
		private string _remarks;
		private int? _makeid;
		private DateTime? _makedate;
		private int? _checkid;
		private DateTime? _checkdate;
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
		public int? typeId
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string number
		{
			set{ _number=value;}
			get{return _number;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string edition
		{
			set{ _edition=value;}
			get{return _edition;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string flagUse
		{
			set{ _flaguse=value;}
			get{return _flaguse;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string flagCheck
		{
			set{ _flagcheck=value;}
			get{return _flagcheck;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string tuhao
		{
			set{ _tuhao=value;}
			get{return _tuhao;}
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
		/// <summary>
		/// 
		/// </summary>
		public int? makeId
		{
			set{ _makeid=value;}
			get{return _makeid;}
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
		public int? checkId
		{
			set{ _checkid=value;}
			get{return _checkid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? checkDate
		{
			set{ _checkdate=value;}
			get{return _checkdate;}
		}
		#endregion Model

	}
}


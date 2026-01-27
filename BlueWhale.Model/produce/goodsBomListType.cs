using System;
namespace BlueWhale.Model.produce
{
    /// <summary>
    /// goodsBomListType:Entity class (attribute description automatically extracts description information of database fields)
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


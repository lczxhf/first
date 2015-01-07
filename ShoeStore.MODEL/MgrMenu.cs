using System;
namespace ShoeStore.MODEL
{
    [Serializable]
    /// <summary>
    /// 作者: SunCoder
    /// 描述: 实体层 -- MgrMenu表映射类.
    /// 最后修改时间:2014-06-18 0:45:06
    /// </summary>
    public class MgrMenu
    {
        public MgrMenu()
        { }

        #region 字段们
        protected int _mgrId;
        protected int _mgrPId;
        protected string _mgrName;
        protected string _mgrLinkUrl;
        protected int _mgrSort;
   
        protected DateTime _mgrAddtime;
        #endregion

        #region 属性们
        /// <summary>
        /// 后台菜单表 id
        /// </summary>
        public int MgrId
        {
            set { _mgrId = value; }
            get { return _mgrId; }
        }

        /// <summary>
        /// 菜单父id
        /// </summary>
        public int MgrPId
        {
            set { _mgrPId = value; }
            get { return _mgrPId; }
        }

        /// <summary>
        /// 菜单名
        /// </summary>
        public string MgrName
        {
            set { _mgrName = value; }
            get { return _mgrName; }
        }

        /// <summary>
        /// 菜单连接地址
        /// </summary>
        public string MgrLinkUrl
        {
            set { _mgrLinkUrl = value; }
            get { return _mgrLinkUrl; }
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int MgrSort
        {
            set { _mgrSort = value; }
            get { return _mgrSort; }
        }



        /// <summary>
        /// 
        /// </summary>
        public DateTime MgrAddtime
        {
            set { _mgrAddtime = value; }
            get { return _mgrAddtime; }
        }
        #endregion
    }
}

using System;
namespace ShoeStore.MODEL
{
    [Serializable]
    /// <summary>
    /// 作者: SunCoder
    /// 描述: 实体层 -- Menu表映射类.
    /// 最后修改时间:2014-06-18 0:30:03
    /// </summary>
    public class Menu
    {
        public Menu()
        { }

        #region 字段们
        protected int _mId;
        protected string _mName;
        protected int _mSort;
        protected string _mUrl;
        
        protected DateTime _mAddtime;
        #endregion

        #region 属性们
        /// <summary>
        /// 
        /// </summary>
        public int MId
        {
            set { _mId = value; }
            get { return _mId; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string MName
        {
            set { _mName = value; }
            get { return _mName; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int MSort
        {
            set { _mSort = value; }
            get { return _mSort; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string MUrl
        {
            set { _mUrl = value; }
            get { return _mUrl; }
        }

  

        /// <summary>
        /// 
        /// </summary>
        public DateTime MAddtime
        {
            set { _mAddtime = value; }
            get { return _mAddtime; }
        }
        #endregion
    }
}

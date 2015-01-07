using System;

namespace ShoeStore.MODEL
{
    [Serializable]
    /// <summary>
    /// 作者: SunCoder
    /// 描述: 实体层 -- Users表映射类.
    /// 最后修改时间:2014-06-17 18:47:59
    /// </summary>
    public class Users
    {
        public Users()
        { }

        #region 字段们
        protected int _uId;
        protected string _uLoginName;
        protected string _uPwd;
        #endregion

        #region 属性们
        /// <summary>
        /// 
        /// </summary>
        public int UId
        {
            set { _uId = value; }
            get { return _uId; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ULoginName
        {
            set { _uLoginName = value; }
            get { return _uLoginName; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string UPwd
        {
            set { _uPwd = value; }
            get { return _uPwd; }
        }
        #endregion
    }
}

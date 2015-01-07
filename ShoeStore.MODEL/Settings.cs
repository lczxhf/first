using System;
namespace ShoeStore.MODEL
{
    [Serializable]
    /// <summary>
    /// 作者: SunCoder
    /// 描述: 实体层 -- T_Settings表映射类.
    /// 最后修改时间:2014-06-20 4:56:59
    /// </summary>
    public class T_Settings
    {
        public T_Settings()
        { }

        #region 字段们
        protected long _id;
        protected string _name;
        protected string _value;
        #endregion

        #region 属性们
        /// <summary>
        /// 
        /// </summary>
        public long Id
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            set { _value = value; }
            get { return _value; }
        }
        #endregion
    }
}

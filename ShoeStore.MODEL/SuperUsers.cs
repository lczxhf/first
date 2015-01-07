using System;
namespace ShoeStore.MODEL
{
    [Serializable]
    /// <summary>
    /// 作者: SunCoder
    /// 描述: 实体层 -- SuperUsers表映射类.
    /// 最后修改时间:2014-06-18 0:48:50
    /// </summary>
    public class SuperUsers
    {
        public SuperUsers()
        { }

        #region 字段们
        protected int _id;
        protected string _name;
        protected string _passWord;
        #endregion

        #region 属性们
        /// <summary>
        /// 
        /// </summary>
        public int Id
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
        public string PassWord
        {
            set { _passWord = value; }
            get { return _passWord; }
        }
        #endregion
    }
}

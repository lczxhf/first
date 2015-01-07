using System;
namespace ShoeStore.MODEL
{
    [Serializable]
    /// <summary>
    /// 作者: SunCoder
    /// 描述: 实体层 -- ContantUs表映射类.
    /// 最后修改时间:2014-06-20 18:29:02
    /// </summary>
    public class ContantUs
    {
        public ContantUs()
        { }

        #region 字段们
        protected int _id;
        protected int _userId;
        protected string _name;
        protected string _email;
        protected int _phone;
        protected string _message;
        protected DateTime _mTime;
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
        public int UserId
        {
            set { _userId = value; }
            get { return _userId; }
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
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Message
        {
            set { _message = value; }
            get { return _message; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime MTime
        {
            set { _mTime = value; }
            get { return _mTime; }
        }
        #endregion
    }
}

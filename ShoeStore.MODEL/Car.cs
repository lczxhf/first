using System;
using ShoeStore.MODEL;
using System.Collections.Generic;

namespace ShoeStore.MODEL
{
    [Serializable]
    /// <summary>
    /// 作者: SunCoder
    /// 描述: 实体层 -- Car表映射类.
    /// 最后修改时间:2014-06-17 18:57:19
    /// </summary>
    public class Car
    {
        public Car()
        { }

        #region 字段们
        protected int _carId;
        protected int _usrId;
        protected Users _usrIdMODEL;
        //购物车明细集合（购物车里的产品集合）
        List<MODEL.CarItems> itemList = new List<CarItems>();
        #endregion

        #region 属性们
        /// <summary>
        /// 
        /// </summary>
        public int CarId
        {
            set { _carId = value; }
            get { return _carId; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int UsrId
        {
            set { _usrId = value; }
            get { return _usrId; }
        }
        /// <summary>
        ///  -外键实体
        /// </summary>
        public Users UsrIdMODEL
        {
            set { _usrIdMODEL = value; }
            get { return _usrIdMODEL; }
        }
        #endregion

        /// <summary>
        /// 购物车明细集合（购物车里的产品集合）
        /// </summary>
        public List<MODEL.CarItems> ItemList
        {
            get { return itemList; }
            set { itemList = value; }
        }
    }
}
    

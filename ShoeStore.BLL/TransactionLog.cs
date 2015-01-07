using System;
using System.Collections.Generic;
using System.Text;

namespace ShoeStore.BLL
{
    /// <summary>
    /// 作者: SunCoder
    /// 描述: 业务层 --  TransactionLog表的业务操作类.
    /// 时间:2014-06-17 21:19:36
    /// </summary>
    public class TransactionLog
    {
        private readonly ShoeStore.DAL.TransactionLog dal = new ShoeStore.DAL.TransactionLog();

        #region 01.根据ID获得实体对象 +MODEL.TransactionLog GetModel(int intId)
        /// <summary>
        /// 根据ID获得实体对象
        /// </summary>
        /// <param name="intId">id值</param>
        /// <returns>实体对象</returns>
        public ShoeStore.MODEL.TransactionLog GetModel(int intId)
        {
            return dal.GetModel(intId);
        }
        #endregion

        #region GET DATA LIST
        /// <summary>
        /// GET DATA LIST
        /// </summary>
        public IList<ShoeStore.MODEL.TransactionLog> GetList()
        {
            return dal.GetList();
        }
        #endregion

        #region 根据根据where条件查询数据集合 

        /// <summary>
        /// 根据根据where条件查询数据集合 
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public IList<ShoeStore.MODEL.TransactionLog> GetListByWhere(string strWhere)
        {
            return dal.GetListByWhere(strWhere);
        } 
        #endregion

        #region 05.物理删除 +int Del(string ids)
        /// <summary>
        /// 物理删除（将删除标志设置为true）
        /// </summary>
        /// <param name="ids">要删除的 id们</param>
        /// <returns>删除成功与否</returns>
        public bool Del(string ids)
        {
            return dal.Del(ids) > 0;
        }
        #endregion

        #region 06.新增记录
        /// <summary>
        /// 新增记录
        /// </summary>
        /// <param name="model">数据实体对象</param>
        /// <returns>新增行的ID</returns>
        public int Add(ShoeStore.MODEL.TransactionLog model)
        {
            return dal.Add(model);
        }
        #endregion

        #region 07.修改记录
        /// <summary>
        /// 修改记录
        /// </summary>
        /// <param name="model">数据实体对象</param>
        /// <returns>受影响行数</returns>
        public bool Update(ShoeStore.MODEL.TransactionLog model)
        {
            return dal.Update(model) > 0;
        }
        #endregion

        public IList<MODEL.NewBuy> GetNewBuy(int day,int top)
        {
            return dal.GetNewBuy(day,top);
        }
        public IList<MODEL.TransactionLog> PageTLog(int pageIndex, int pageSize, out int pageCount, out int num)
        {
            return dal.PageTLog(pageIndex, pageSize, out pageCount, out num);
        }
    }
}

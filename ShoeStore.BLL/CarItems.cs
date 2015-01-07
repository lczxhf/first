using System;
using System.Collections.Generic;
using System.Text;

namespace ShoeStore.BLL
{
    /// <summary>
    /// 作者: SunCoder
    /// 描述: 业务层 --  CarItems表的业务操作类.
    /// 时间:2014-06-17 21:19:04
    /// </summary>
    public class CarItems
    {
        private readonly ShoeStore.DAL.CarItems dal = new ShoeStore.DAL.CarItems();

        #region 01.根据ID获得实体对象 +MODEL.CarItems GetModel(int intId)
        /// <summary>
        /// 根据ID获得实体对象
        /// </summary>
        /// <param name="intId">id值</param>
        /// <returns>实体对象</returns>
        public ShoeStore.MODEL.CarItems GetModel(int intId)
        {
            return dal.GetModel(intId);
        }
        #endregion

        #region GET DATA LIST
        /// <summary>
        /// GET DATA LIST
        /// </summary>
        public IList<ShoeStore.MODEL.CarItems> GetList()
        {
            return dal.GetList();
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
        public int Add(ShoeStore.MODEL.CarItems model)
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
        public bool Update(ShoeStore.MODEL.CarItems model)
        {
            return dal.Update(model) > 0;
        }
        #endregion

        public int UpdateItemById(int itemId, int count,DateTime time)
        {
            return dal.UpdateItemById(itemId, count,time);
        }

        public bool IsExistProduct(MODEL.CarItems carItem)
        {
            int cItemId=dal.IsExistProduct(carItem.CPId , carItem.CCarId);
            if (cItemId != 0)
            {
                if (dal.UpdateCount(carItem.CCount,cItemId,DateTime.Now) > 0)
                {
                    return true;
                }
            }
            else
            {
                if (dal.Add(carItem) > 0)
                {
                    return true;
                }

            }
            return false;
        }

        public IList<MODEL.CarItems> PageItems(int pageIndex, int pageSize, out int pageCount, out int num)
        {
            return dal.PageItems(pageIndex, pageSize, out pageCount, out num);
        }
    }
}

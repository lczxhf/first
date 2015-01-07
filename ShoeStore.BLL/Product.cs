using System;
using System.Collections.Generic;
using System.Text;

namespace ShoeStore.BLL
{
    /// <summary>
    /// 作者: SunCoder
    /// 描述: 业务层 --  Product表的业务操作类.
    /// 时间:2014-06-17 21:19:59
    /// </summary>
    public class Product
    {
        private readonly ShoeStore.DAL.Product dal = new ShoeStore.DAL.Product();

        #region 01.根据ID获得实体对象 +MODEL.Product GetModel(int intId)
        /// <summary>
        /// 根据ID获得实体对象
        /// </summary>
        /// <param name="intId">id值</param>
        /// <returns>实体对象</returns>
        public ShoeStore.MODEL.Product GetModel(int intId)
        {
            return dal.GetModel(intId);
        }
        #endregion

        #region GET DATA LIST
        /// <summary>
        /// GET DATA LIST
        /// </summary>
        public IList<ShoeStore.MODEL.Product> GetList()
        {
            return dal.GetList();
        }
        #endregion


        public IList<MODEL.Product> GetListByIsrec()
        {
            return dal.GetListByIsrec();
        }


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
        public int Add(ShoeStore.MODEL.Product model)
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
        public bool Update(ShoeStore.MODEL.Product model)
        {
            return dal.Update(model) > 0;
        }

        ////////////////////////////////////
        public IList<MODEL.Product> GetPageProduct(int pageIndex, int pageSize, out int pageCount, out int num)
        {
            return dal.GetPageProduct(pageIndex,pageSize,out pageCount,out num);
        }
        #endregion

        public IList<MODEL.Product> GetSearch( string word, int pageIndex, int pageSize, out int pageCount, out int num)
        {
            return dal.GetSearch( word, pageIndex, pageSize, out  pageCount, out  num);
        }
        public IList<MODEL.Product> GetProByCate(int id, int pageIndex, int pageSize, out int pageCount, out int num)
        {
            return dal.GetProByCate(id, pageIndex, pageSize, out  pageCount, out  num);
        }

        public List<MODEL.Product> GetRelated(int id,int Cateid)
        {
            IList<MODEL.Product> product = dal.GetRelated(Cateid);
            List<MODEL.Product> related = new List<MODEL.Product>();
            int i = 0;
            foreach (MODEL.Product pro in product) 
            {
                if (pro.PId == id)
                {
                    continue;
                }
                if (i < 3)
                {
                    related.Add(pro);
                    i++;
                }
                else
                {
                    break;
                }
            }
            return related;

        }

        public int GetProductId(string pName)
        {
            return dal.GetProductId(pName);
        }

        public int UpdateCount(int num, int userId, string pName, int itemId,int almost)
        {
            return dal.UpdateCount(num , userId,pName, itemId,almost);
        }

        public int GetProductCount(string pName)
        {
            return dal.GetProductCount(pName);        
        }
        public int UserMoney(int userId)
        {
            return dal.UserMoney(userId);
        }
        public bool ProductNameIsExist(string pName)
        {
            return dal.ProductNameIsExist(pName);
        }
    }
}

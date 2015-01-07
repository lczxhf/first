using System;
using System.Collections.Generic;
using System.Text;

namespace ShoeStore.BLL
{
    /// <summary>
    /// 作者: SunCoder
    /// 描述: 业务层 --  SuperUsers表的业务操作类.
    /// 时间:2014-06-18 0:49:28
    /// </summary>
    public class SuperUsers
    {
        private readonly ShoeStore.DAL.SuperUsers dal = new ShoeStore.DAL.SuperUsers();

        #region 01.根据ID获得实体对象 +MODEL.SuperUsers GetModel(int intId)
        /// <summary>
        /// 根据ID获得实体对象
        /// </summary>
        /// <param name="intId">id值</param>
        /// <returns>实体对象</returns>
        public ShoeStore.MODEL.SuperUsers GetModel(int intId)
        {
            return dal.GetModel(intId);
        }
        #endregion

        #region GET DATA LIST
        /// <summary>
        /// GET DATA LIST
        /// </summary>
        public IList<ShoeStore.MODEL.SuperUsers> GetList()
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
        public int Add(ShoeStore.MODEL.SuperUsers model)
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
        public bool Update(ShoeStore.MODEL.SuperUsers model)
        {
            return dal.Update(model) > 0;
        }
        #endregion

        public MODEL.SuperUsers GetUser(string name,string passWord,out bool isExist)
        {
            MODEL.SuperUsers suser = dal.GetUser(name);
            isExist = false;
            if (suser == null)
            {
                isExist = false;
                return null;
            }
            if (suser.PassWord == passWord)
            {
                isExist = true;
                return suser;
            }
            else
            {
                isExist = true; ;
                return null;
            }
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ShoeStore.BLL
{
    /// <summary>
    /// 作者: SunCoder
    /// 描述: 业务层 --  Users表的业务操作类.
    /// 时间:2014-06-17 21:19:47
    /// </summary>
    public class Users
    {
        private readonly ShoeStore.DAL.Users dal = new ShoeStore.DAL.Users();

        #region 01.根据ID获得实体对象 +MODEL.Users GetModel(int intId)
        /// <summary>
        /// 根据ID获得实体对象
        /// </summary>
        /// <param name="intId">id值</param>
        /// <returns>实体对象</returns>
        public ShoeStore.MODEL.Users GetModel(int intId)
        {
            return dal.GetModel(intId);
        }
        #endregion

        #region GET DATA LIST
        /// <summary>
        /// GET DATA LIST
        /// </summary>
        public IList<ShoeStore.MODEL.Users> GetList()
        {
            return dal.GetList();
        }
        #endregion
        public ShoeStore.MODEL.Users GetUser(string name, string passWord,out bool isExist)
        {

            ShoeStore.MODEL.Users suser = dal.GetUser(name);
            isExist = false;
            if (suser == null)
            {
                isExist = false;
                return null;
            }
            if (suser.UPwd == passWord)
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
        public int Add(ShoeStore.MODEL.Users model)
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
        public bool Update(ShoeStore.MODEL.Users model)
        {
            return dal.Update(model) > 0;
        }
        #endregion

        public bool DelAllAboutUser(int userId)
        {
            return dal.DelAllAboutUser(userId);
        }

        public int GetUserId(string userName)
        {
            return dal.GetUserId(userName);
        }

    }
}

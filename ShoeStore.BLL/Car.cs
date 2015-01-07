using System;
using System.Collections.Generic;
using System.Text;

namespace ShoeStore.BLL
{
    /// <summary>
    /// 作者: SunCoder
    /// 描述: 业务层 --  Car表的业务操作类.
    /// 时间:2014-06-17 21:18:50
    /// </summary>
    public class Car
    {
        private readonly ShoeStore.DAL.Car dal = new ShoeStore.DAL.Car();

        #region 01.根据ID获得实体对象 +MODEL.Car GetModel(int intId)
        /// <summary>
        /// 根据ID获得实体对象
        /// </summary>
        /// <param name="intId">id值</param>
        /// <returns>实体对象</returns>
        public ShoeStore.MODEL.Car GetModel(int intId)
        {
            return dal.GetModel(intId);
        }
        #endregion

        #region GET DATA LIST
        /// <summary>
        /// GET DATA LIST
        /// </summary>
        public IList<ShoeStore.MODEL.Car> GetList()
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
        public int Add(ShoeStore.MODEL.Car model)
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
        public bool Update(ShoeStore.MODEL.Car model)
        {
            return dal.Update(model) > 0;
        }
        #endregion

        #region 1.0 - 根据 用户 id 查询 购物车
        /// <summary>
        /// 根据 用户 id 查询 购物车
        /// </summary>
        /// <param name="usrId"></param>
        /// <returns></returns>
        public MODEL.Car GetUserCar(int usrId)
        {
            //看数据库中是否有 当前用户的购物车，如果有，则直接返回
            MODEL.Car car = dal.GetModelByUser(usrId);
            //如果没有，则创建并返回
            if (car == null)
            {
                //创建当前用户的 购物车
                car = new MODEL.Car() { UsrId = usrId };
                //添加到数据库
                int newCarId = dal.Add(car);
                car.CarId = newCarId;
            }
            return car;
        }
        #endregion


        #region 06.新增记录
        /// <summary>
        /// 新增记录
        /// </summary>
        /// <param name="model">数据实体对象</param>
        /// <returns>新增行的ID</returns>
        public int AddProductItem(int productId, MODEL.Car shopCar)
        {
            //新增的购物明细项 
            MODEL.CarItems itemNew = null;
            //在购物车中是否 存在此明细项
            bool isExsit = false;
            //判断 购物车中 是否已经有了产品
            foreach (MODEL.CarItems item in shopCar.ItemList)
            {
                //1.如果有，则 在原来基础上 + 1
                if (item.CPId == productId)
                {
                    //为已存在购物车里的 产品 数量 +1
                    item.CCount++;
                    itemNew = item;
                    //标识 新加的 产品 已存在购物车
                    isExsit = true;
                    break;
                }
            }
            //如果购物车中没有这个产品，则 新增到购物车
            if (!isExsit)
            {
                itemNew = new MODEL.CarItems() { CCarId = shopCar.CarId, CPId = productId, CCount = 1 };
                shopCar.ItemList.Add(itemNew);
            }
            //创建 购物车 明细表 操作对象
            DAL.CarItems dalItem = new DAL.CarItems();
            //调用数据库 更新/新增 执行
            if (isExsit)
            {
                dalItem.Update(itemNew);
            }
            else
            {
                dalItem.Add(itemNew);
            }

            return 1;
        }
        #endregion

        #region 2.0 清空 登录用户的 购物车
        /// <summary>
        /// 清空 登录用户的 购物车
        /// </summary>
        /// <param name="usrId"></param>
        /// <returns></returns>
        public bool ClearCar(int usrId)
        {
            return dal.ClearCar(usrId);
        }
        #endregion


        public bool ClearOne(int userId,int carItemId)
        {
            return dal.ClearOne(userId, carItemId);
        }
        public int GetCarId(string userName)
        {
            return dal.GetCarId(userName);
        }
    }
}

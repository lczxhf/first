using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ShoeStore.DAL
{
    /// <summary>
    /// Author: SunCoder
    /// Description: 数据层 -- Car的实体类.
    /// 创建时间:2014-06-17 21:09:53
    /// </summary>
    public class Car
    {
       

        #region 02.执行物理删除 +int Del(string ids)
        /// <summary>
        /// 执行物理删除
        /// </summary>
        /// <param name="ids">要删除的id号们(1,2,5)</param>
        /// <returns>受影响行数</returns>
        public int Del(string ids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("exec('delete Car where carId in ('+@ids+')')");
            SqlParameter para = new SqlParameter("@ids", ids);
            return DbHelperSQL.ExcuteNonQuery(strSql.ToString(), para);
        }
        #endregion

        #region 02.2单个物理删除
        /// <summary>
        /// 单个物理删除
        /// </summary>
        /// <param name="idInt">要删除的id号</param>
        /// <returns>受影响行数</returns>
        public int Del(int idInt)
        {
            string strSql = "delete Car where carId = @id";
            SqlParameter para = new SqlParameter("@id", idInt);
            return DbHelperSQL.ExcuteNonQuery(strSql, para);
        }
        #endregion

        #region 03.根据ID获得实体对象 +MODEL.Car GetModel(int intId)
        /// <summary>
        /// 根据ID获得实体对象
        /// </summary>
        /// <param name="intId">id值</param>
        /// <returns>实体对象</returns>
        public ShoeStore.MODEL.Car GetModel(int intId)
        {
            StringBuilder strSql = new StringBuilder("select carId,usrId from Car ");
            strSql.Append(" where carId=@intId ");
            ShoeStore.MODEL.Car model = new ShoeStore.MODEL.Car();
            DataTable dt = DbHelperSQL.GetDataTable(strSql.ToString(), new SqlParameter("@intId", intId));
            if (dt.Rows.Count > 0)
            {
                LoadEntityData(model, dt.Rows[0]);
                return model;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 04.查询数据集合 +IList<ShoeStore.MODEL.Car> GetList()
        /// <summary>
        /// 查询数据集合
        /// </summary>
        public IList<ShoeStore.MODEL.Car> GetList()
        {
            return GetListByWhere("");
        }
        #endregion

        #region 根据where条件查询数据集合 -IList<ShoeStore.MODEL.Car> GetListByWhere(string strWhere)
        /// <summary>
        /// 根据where条件查询数据集合
        /// </summary>
        internal IList<ShoeStore.MODEL.Car> GetListByWhere(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select carId,usrId ");
            strSql.Append(" FROM Car ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataTable dt = DbHelperSQL.GetDataTable(strSql.ToString());
            IList<ShoeStore.MODEL.Car> list = null;
            if (dt.Rows.Count > 0)
            {
                list = Table2List(dt);
            }
            return list;
        }
        #endregion

        #region a01.将数据表转换成泛型集合接口+ IList<MODEL.Car> Table2List(DataTable dt)
        /// <summary>
        /// a01.将数据表转换成泛型集合接口
        /// </summary>
        /// <param name="dt">数据表对象</param>
        /// <returns>泛型集合接口</returns>
        public IList<ShoeStore.MODEL.Car> Table2List(DataTable dt)
        {
            List<ShoeStore.MODEL.Car> list = null;
            if (dt.Rows.Count > 0)
            {
                list = new List<ShoeStore.MODEL.Car>();
                ShoeStore.MODEL.Car model = null;
                foreach (DataRow dr in dt.Rows)
                {
                    model = new ShoeStore.MODEL.Car();
                    LoadEntityData(model, dr);
                    list.Add(model);
                }
                return list;
            }
            return null;
        }
        #endregion
        DAL.CarItems dalItem = new CarItems();
        #region a04.加载实体数据(将行数据装入实体对象中)-void LoadEntityData(MODEL.BlogArticleCate model, DataRow dr)
        /// <summary>
        /// 加载实体数据(将行数据装入实体对象中)
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="dr">数据行</param>
        internal void LoadEntityData(ShoeStore.MODEL.Car model, DataRow dr)
        {
            if (dr.Table.Columns.Contains("carId") && !dr.IsNull("carId"))
            {
                model.CarId = int.Parse(dr["carId"].ToString());
            }
            if (dr.Table.Columns.Contains("usrId") && !dr.IsNull("usrId"))
            {
                model.UsrId = int.Parse(dr["usrId"].ToString());
            }

        }
        #endregion

        #region 07.新增数据 +int Add(MODEL.BlogArticleCate model)
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model">数据实体对象</param>
        /// <returns>新增成功的ID号</returns>
        public int Add(ShoeStore.MODEL.Car model)
        {
            int result = 0;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into Car(");
                strSql.Append("usrId)");
                strSql.Append(" values (");
                strSql.Append("@usrId)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
                    new SqlParameter("@usrId", SqlDbType.Int,4)};
                parameters[0].Value = model.UsrId;
                result = Convert.ToInt32(DbHelperSQL.ExcuteScalar(strSql.ToString(), parameters));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        #endregion

        #region 08.修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">数据实体对象</param>
        /// <returns>修改成功的行数</returns>
        public int Update(ShoeStore.MODEL.Car model)
        {
            int res = -2;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Car set ");
            strSql.Append("usrId=@usrId");
            strSql.Append(" where carId=@carId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@carId", SqlDbType.Int,4),
                    new SqlParameter("@usrId", SqlDbType.Int,4)};
            parameters[0].Value = model.CarId;
            parameters[1].Value = model.UsrId;

            try
            {
                res = DbHelperSQL.ExcuteNonQuery(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
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
            try
            {
                //根据用户 id 查询 购物车id
                int carId = Convert.ToInt32(DbHelperSQL.ExcuteScalar("select carId from car where usrId =" + usrId));
                //根据 购物车id 删除购物车 明细
                DbHelperSQL.ExcuteNonQuery("delete CarItems where cCarid =" + carId);
                //删除购物车
                DbHelperSQL.ExcuteNonQuery("delete Car where carid =" + carId);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
        #endregion

        #region 1.0 根据用户ID获得实体对象 +MODEL.Car GetModelByUser(int intId)
        /// <summary>
        /// 根据用户ID获得实体对象
        /// </summary>
        /// <param name="intId">id值</param>
        /// <returns>实体对象</returns>
        public MODEL.Car GetModelByUser(int usrId)
        {
            StringBuilder strSql = new StringBuilder("select * from Car c");
            strSql.AppendLine(" inner join CarItems ci on c.carId = ci.cCarId ");
            strSql.AppendLine(" inner join Product p on ci.cPId=p.pId ");
            strSql.Append(" where c.usrId=@usrId ");

            MODEL.Car modelCar = new MODEL.Car();

            DataTable dt = DbHelperSQL.GetDataTable(strSql.ToString(), new SqlParameter("@usrId", usrId));
            if (dt.Rows.Count > 0)
            {
                //创建购物车，并 为购物车的属性复制
                LoadEntityData(modelCar, dt.Rows[0]);

                //先将 购物车的 第一个产品 加入
                MODEL.CarItems carItem = null;

                //循环 追加 明细项
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //购物车明细项
                    carItem = new MODEL.CarItems();
                    //将后面的数据 也加入 集合
                    dalItem.LoadEntityData(carItem, dt.Rows[i]);
                    //加入到 购物车 的 购物项目 集合中
                    modelCar.ItemList.Add(carItem);
                }

                //返回购物车
                return modelCar;
            }
            else
            {
                return null;
            }
        }
        #endregion

        public bool ClearOne(int userId, int CarItemId)
        {
            try
            {
                //根据用户 id 查询 购物车id
                int carId = Convert.ToInt32(DbHelperSQL.ExcuteScalar("select carId from car where usrId =" + userId));
                //根据 购物车id 删除购物车 明细
                DbHelperSQL.ExcuteNonQuery("delete CarItems where cCarid =" + carId+"and cItemId ="+CarItemId);
                
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
 
        }

        public int GetCarId(string userName)
        {
            int userId = DbHelperSQL.ExcuteScalar("select uId from Users where uLoginName='" + userName + "'");
            int i = DbHelperSQL.ExcuteScalar("select carId from Car where usrId="+userId);
            return i;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ShoeStore.DAL
{
    /// <summary>
    /// Author: SunCoder
    /// Description: 数据层 -- CarItems的实体类.
    /// 创建时间:2014-06-17 21:10:14
    /// </summary>
    public class CarItems
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
            strSql.Append("exec('delete CarItems where cItemId in ('+@ids+')')");
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
            string strSql = "delete CarItems where cItemId = @id";
            SqlParameter para = new SqlParameter("@id", idInt);
            return DbHelperSQL.ExcuteNonQuery(strSql, para);
        }
        #endregion

        #region 03.根据ID获得实体对象 +MODEL.CarItems GetModel(int intId)
        /// <summary>
        /// 根据ID获得实体对象
        /// </summary>
        /// <param name="intId">id值</param>
        /// <returns>实体对象</returns>
        public ShoeStore.MODEL.CarItems GetModel(int intId)
        {
            StringBuilder strSql = new StringBuilder("select cItemId,cCarId,cPId,cCount,cTime from CarItems ");
            strSql.Append(" where cItemId=@intId ");
            IList<MODEL.CarItems> list=new List<MODEL.CarItems>();
            DataTable dt = DbHelperSQL.GetDataTable(strSql.ToString(), new SqlParameter("@intId", intId));
            if (dt.Rows.Count > 0)
            {
                SetUserAndProduct(dt, list);
                return list[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 04.查询数据集合 +IList<ShoeStore.MODEL.CarItems> GetList()
        /// <summary>
        /// 查询数据集合
        /// </summary>
        public IList<ShoeStore.MODEL.CarItems> GetList()
        {
            return GetListByWhere("");
        }
        #endregion

        #region 根据where条件查询数据集合 -IList<ShoeStore.MODEL.CarItems> GetListByWhere(string strWhere)
        /// <summary>
        /// 根据where条件查询数据集合
        /// </summary>
        internal IList<ShoeStore.MODEL.CarItems> GetListByWhere(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select cItemId,cCarId,cPId,cCount,cTime ");
            strSql.Append(" FROM CarItems ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataTable dt = DbHelperSQL.GetDataTable(strSql.ToString());
            IList<ShoeStore.MODEL.CarItems> list = null;
            if (dt.Rows.Count > 0)
            {
                list = Table2List(dt);
            }
            return list;
        }
        #endregion

        #region a01.将数据表转换成泛型集合接口+ IList<MODEL.CarItems> Table2List(DataTable dt)
        /// <summary>
        /// a01.将数据表转换成泛型集合接口
        /// </summary>
        /// <param name="dt">数据表对象</param>
        /// <returns>泛型集合接口</returns>
        public IList<ShoeStore.MODEL.CarItems> Table2List(DataTable dt)
        {
            List<ShoeStore.MODEL.CarItems> list = null;
            if (dt.Rows.Count > 0)
            {
                list = new List<ShoeStore.MODEL.CarItems>();
                ShoeStore.MODEL.CarItems model = null;
                foreach (DataRow dr in dt.Rows)
                {
                    model = new ShoeStore.MODEL.CarItems();
                    LoadEntityData(model, dr);
                    list.Add(model);
                }
                return list;
            }
            return null;
        }
        #endregion

        DAL.Product dal = new Product();
        #region a04.加载实体数据(将行数据装入实体对象中)-void LoadEntityData(MODEL.BlogArticleCate model, DataRow dr)
        /// <summary>
        /// 加载实体数据(将行数据装入实体对象中)
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="dr">数据行</param>
        internal void LoadEntityData(ShoeStore.MODEL.CarItems model, DataRow dr)
        {
            if (dr.Table.Columns.Contains("cItemId") && !dr.IsNull("cItemId"))
            {
                model.CItemId = int.Parse(dr["cItemId"].ToString());
            }
            if (dr.Table.Columns.Contains("cCarId") && !dr.IsNull("cCarId"))
            {
                model.CCarId = int.Parse(dr["cCarId"].ToString());
            }
            if (dr.Table.Columns.Contains("cPId") && !dr.IsNull("cPId"))
            {
                model.CPId = int.Parse(dr["cPId"].ToString());
                //将 购物车明细 项 里的 产品 创建出来，并赋值
                model.CPIdMODEL = new MODEL.Product();
                dal.LoadEntityData(model.CPIdMODEL, dr);    
            }
            if (dr.Table.Columns.Contains("cCount") && !dr.IsNull("cCount"))
            {
                model.CCount = int.Parse(dr["cCount"].ToString());
            }
            if (dr.Table.Columns.Contains("cTime") && !dr.IsNull("cTime"))
            {
                model.CTime = DateTime.Parse(dr["cTime"].ToString());
            }

        }
        #endregion

        #region 07.新增数据 +int Add(MODEL.BlogArticleCate model)
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model">数据实体对象</param>
        /// <returns>新增成功的ID号</returns>
        public int Add(ShoeStore.MODEL.CarItems model)
        {
            int result = 0;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into CarItems(");
                strSql.Append("cCarId,cPId,cCount,cTime)");
                strSql.Append(" values (");
                strSql.Append("@cCarId,@cPId,@cCount,@cTime)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
                    new SqlParameter("@cCarId", SqlDbType.Int,4),
                    new SqlParameter("@cPId", SqlDbType.Int,4),
                    new SqlParameter("@cCount", SqlDbType.Int,4),
                    new SqlParameter("@cTime", SqlDbType.DateTime,8)};
                parameters[0].Value = model.CCarId;
                parameters[1].Value = model.CPId;
                parameters[2].Value = model.CCount;
                parameters[3].Value = model.CTime;
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
        public int Update(ShoeStore.MODEL.CarItems model)
        {
            int res = -2;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CarItems set ");
            strSql.Append("cCarId=@cCarId,");
            strSql.Append("cPId=@cPId,");
            strSql.Append("cCount=@cCount,");
            strSql.Append("cTime=@cTime");
            strSql.Append(" where cItemId=@cItemId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@cItemId", SqlDbType.Int,4),
                    new SqlParameter("@cCarId", SqlDbType.Int,4),
                    new SqlParameter("@cPId", SqlDbType.Int,4),
                    new SqlParameter("@cCount", SqlDbType.Int,4),
                    new SqlParameter("@cTime", SqlDbType.DateTime,8)};
            parameters[0].Value = model.CItemId;
            parameters[1].Value = model.CCarId;
            parameters[2].Value = model.CPId;
            parameters[3].Value = model.CCount;
            parameters[4].Value = model.CTime;

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


        #region 08.根据 购物详细 id 更新
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">数据实体对象</param>
        /// <returns>修改成功的行数</returns>
        public int UpdateItemById(int ItemId,int count,DateTime time)
        {
            int res = -2;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CarItems set ");
            strSql.Append("cCount=@cCount,cTime=@cTime");
            strSql.Append(" where cItemId=@cItenId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@cItenId", SqlDbType.Int,4),
                    new SqlParameter("@cCount", SqlDbType.Int,4),
            new SqlParameter("@cTime", SqlDbType.DateTime,8)};
            parameters[0].Value = ItemId;
            parameters[1].Value = count;
            parameters[2].Value = time;

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

        public int UpdateCount(int count,int ItemId,DateTime time)
        {
            int i=DbHelperSQL.ExcuteNonQuery("update CarItems set cCount=cCount+@Count,cTime=@cTime where cItemId=@ItemId", new SqlParameter("@Count", count),new SqlParameter("@ItemId",ItemId),new SqlParameter("@cTime",time));
            if (i > 0)
            {
                return i;
            }
            else
            {
                return 0;
            }
        }
        public int IsExistProduct(int pId,int carId)
        {
           DataTable dt= DbHelperSQL.GetDataTable("select * from CarItems where cPId=@pId and cCarId=@cCarId",new SqlParameter("@PId",pId),new SqlParameter("@cCarId",carId));
            if(dt.Rows.Count>0)
            {
                return Convert.ToInt32( dt.Rows[0]["cItemId"]);
            }
            return 0;

        }

        public IList<MODEL.CarItems> PageItems(int pageIndex, int pageSize, out int pageCount, out int num)
        {
            DataTable dt = DbHelperSQL.Getproc("pageCar", pageIndex, pageSize, out pageCount, out num);
            IList<MODEL.CarItems> list = null;
            if (dt.Rows.Count > 0)
            {
                list = new List<MODEL.CarItems>();
                MODEL.CarItems cItem = null;
                DAL.Product product = null;
                DAL.Users user = null;
                foreach (DataRow dr in dt.Rows)
                {
                    cItem = new MODEL.CarItems();
                    LoadEntityData(cItem, dr);
                    DataTable dt1 = DbHelperSQL.GetDataTable("select * from Product where pId=" + dr["cPId"]);
                    product = new Product();
                    cItem.CPIdMODEL = new MODEL.Product();
                    product.LoadEntityData(cItem.CPIdMODEL, dt1.Rows[0]);
                    int a =Convert.ToInt32( DbHelperSQL.ExcuteScalar("select usrId from Car where carId=" + dr["cCarId"]));
                    DataTable dt2 = DbHelperSQL.GetDataTable("select * from Users where uId="+a);
                    user = new Users();
                    cItem.CUserMODEL = new MODEL.Users();
                    user.LoadEntityData(cItem.CUserMODEL, dt2.Rows[0]);
                    list.Add(cItem);
                }
            }
            return list;
        }

        internal void SetUserAndProduct(DataTable dt, IList<MODEL.CarItems> list)
        {

            MODEL.CarItems catitem = null;
            DAL.Product product = null;
            DAL.Users user = null;
            foreach (DataRow dr in dt.Rows)
            {
                catitem = new MODEL.CarItems();
                LoadEntityData(catitem, dr);
                DataTable dt1 = DbHelperSQL.GetDataTable("select * from Product where pId=" + dr["cPId"]);
                product = new Product();
                catitem.CPIdMODEL = new MODEL.Product();
                product.LoadEntityData(catitem.CPIdMODEL, dt1.Rows[0]);
                DataTable dt2 = DbHelperSQL.GetDataTable("select uLoginName from Users where uId=(select usrId from Car where carId=@carId)",new SqlParameter("@carId",dr["cCarId"]));
                user = new Users();
                catitem.CUserMODEL = new MODEL.Users();
                user.LoadEntityData(catitem.CUserMODEL, dt2.Rows[0]);
                list.Add(catitem);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ShoeStore.DAL
{
    /// <summary>
    /// Author: SunCoder
    /// Description: 数据层 -- TransactionLog的实体类.
    /// 创建时间:2014-06-17 21:10:42
    /// </summary>
    public class TransactionLog
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
            strSql.Append("exec('delete TransactionLog where Id in ('+@ids+')')");
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
            string strSql = "delete TransactionLog where Id = @id";
            SqlParameter para = new SqlParameter("@id", idInt);
            return DbHelperSQL.ExcuteNonQuery(strSql, para);
        }
        #endregion

        #region 03.根据ID获得实体对象 +MODEL.TransactionLog GetModel(int intId)
        /// <summary>
        /// 根据ID获得实体对象
        /// </summary>
        /// <param name="intId">id值</param>
        /// <returns>实体对象</returns>
        public ShoeStore.MODEL.TransactionLog GetModel(int intId)
        {
            StringBuilder strSql = new StringBuilder("select Id,pId,pCount,pTime,userId from TransactionLog ");
            strSql.Append(" where Id=@intId ");
            IList<MODEL.TransactionLog> list = new List<MODEL.TransactionLog>();
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

        #region 04.查询数据集合 +IList<ShoeStore.MODEL.TransactionLog> GetList()
        /// <summary>
        /// 查询数据集合
        /// </summary>
        public IList<ShoeStore.MODEL.TransactionLog> GetList()
        {
            return GetListByWhere("");
        }
        #endregion

        #region 根据where条件查询数据集合 -IList<ShoeStore.MODEL.TransactionLog> GetListByWhere(string strWhere)
        /// <summary>
        /// 根据where条件查询数据集合
        /// </summary>
        public  IList<ShoeStore.MODEL.TransactionLog> GetListByWhere(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,pId,pCount,pTime,userId ");
            strSql.Append(" FROM TransactionLog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataTable dt = DbHelperSQL.GetDataTable(strSql.ToString());
            IList<ShoeStore.MODEL.TransactionLog> list = null;
            if (dt.Rows.Count > 0)
            {
                list = Table2List(dt);
            }
            return list;
        }
        #endregion

        #region a01.将数据表转换成泛型集合接口+ IList<MODEL.TransactionLog> Table2List(DataTable dt)
        /// <summary>
        /// a01.将数据表转换成泛型集合接口
        /// </summary>
        /// <param name="dt">数据表对象</param>
        /// <returns>泛型集合接口</returns>
        public IList<ShoeStore.MODEL.TransactionLog> Table2List(DataTable dt)
        {
            List<ShoeStore.MODEL.TransactionLog> list = null;
            if (dt.Rows.Count > 0)
            {
                list = new List<ShoeStore.MODEL.TransactionLog>();
                ShoeStore.MODEL.TransactionLog model = null;
                foreach (DataRow dr in dt.Rows)
                {
                    model = new ShoeStore.MODEL.TransactionLog();
                    LoadEntityData(model, dr);
                    list.Add(model);
                }
                return list;
            }
            return null;
        }
        #endregion

        #region a04.加载实体数据(将行数据装入实体对象中)-void LoadEntityData(MODEL.BlogArticleCate model, DataRow dr)
        /// <summary>
        /// 加载实体数据(将行数据装入实体对象中)
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="dr">数据行</param>
        internal void LoadEntityData(ShoeStore.MODEL.TransactionLog model, DataRow dr)
        {
            if (dr.Table.Columns.Contains("Id") && !dr.IsNull("Id"))
            {
                model.Id = int.Parse(dr["Id"].ToString());
            }
            if (dr.Table.Columns.Contains("pId") && !dr.IsNull("pId"))
            {
                model.PId = int.Parse(dr["pId"].ToString());
                
            }
            if (dr.Table.Columns.Contains("userId") && !dr.IsNull("userId"))
            {
                
                model.UserId = int.Parse(dr["userId"].ToString());
            }
            if (dr.Table.Columns.Contains("pCount") && !dr.IsNull("pCount"))
            {
                model.PCount = int.Parse(dr["pCount"].ToString());
            }
            if (dr.Table.Columns.Contains("pTime") && !dr.IsNull("pTime"))
            {
                model.PTime = DateTime.Parse(dr["pTime"].ToString());
            }

        }
        #endregion

        #region 07.新增数据 +int Add(MODEL.BlogArticleCate model)
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model">数据实体对象</param>
        /// <returns>新增成功的ID号</returns>
        public int Add(ShoeStore.MODEL.TransactionLog model)
        {
            int result = 0;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into TransactionLog(");
                strSql.Append("pId,pCount,pTime,UserId)");
                strSql.Append(" values (");
                strSql.Append("@pId,@pCount,@pTime,@UserId)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
                    new SqlParameter("@pId", SqlDbType.Int,4),
                    new SqlParameter("@pCount", SqlDbType.Int,4),
                    new SqlParameter("@pTime", SqlDbType.DateTime,8),
                    new SqlParameter("@UserId", SqlDbType.Int,4)};
                parameters[0].Value = model.PId;
                parameters[1].Value = model.PCount;
                parameters[2].Value = model.PTime;
                parameters[3].Value = model.UserId;
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
        public int Update(ShoeStore.MODEL.TransactionLog model)
        {
            int res = -2;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TransactionLog set ");
            strSql.Append("pId=@pId,");
            strSql.Append("userId=@userId,");
            strSql.Append("pCount=@pCount,");
            strSql.Append("pTime=@pTime");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4),
                    new SqlParameter("@pId", SqlDbType.Int,4),
                    new SqlParameter("@pCount", SqlDbType.Int,4),
                    new SqlParameter("@pTime", SqlDbType.DateTime,8),
                    new SqlParameter("@userId", SqlDbType.Int,4)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.PId;
            parameters[2].Value = model.PCount;
            parameters[3].Value = model.PTime;
            parameters[4].Value = model.UserId;

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



        ////////////////////////////////////////////////////////////
        #region 查询最近x天的购买排名  并返回泛型集合接口

        /// <summary>
        /// 查询最近x天的购买排名  并返回泛型集合接口
        /// </summary>
        /// <returns></returns>
        public IList<MODEL.NewBuy> GetNewBuy(int day,int top)
        {
            DataTable dt = DbHelperSQL.GetBuyDay("GetNewBuy", day,top);
           

            List<ShoeStore.MODEL.NewBuy> list = null;
                if (dt.Rows.Count > 0)
                {
                    list = new List<ShoeStore.MODEL.NewBuy>();
                    ShoeStore.MODEL.NewBuy model = null;
                    foreach (DataRow dr in dt.Rows)
                    {
                        model = new ShoeStore.MODEL.NewBuy ();
                       
                        if (dr.Table.Columns.Contains("PCount") && !dr.IsNull("PCount"))
                        {
                            model.PCount = int.Parse(dr["PCount"].ToString());
                        }
                       DataTable dt1= DbHelperSQL.GetDataTable("select * from Product where pName= '"+dr[1].ToString()+"'");
                         DAL.Product prd = new Product();
                        model.PIdMODEL=new MODEL.Product ();
                        foreach(DataRow dr1 in dt1.Rows)
                        {

                            prd.LoadEntityData(model.PIdMODEL, dr1);
                        }
                        list.Add(model);
                    }
                   
                }
            
            return list;

        } 
        #endregion


        #region 获得分页的购物车数据 包括商品和用户的信息
        /// <summary>
        /// 获得分页的购物车数据 包括商品和用户的信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCount"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public IList<MODEL.TransactionLog> PageTLog(int pageIndex, int pageSize, out int pageCount, out int num)
        {
            DataTable dt = DbHelperSQL.Getproc("pageTLog", pageIndex, pageSize, out pageCount, out num);
            IList<MODEL.TransactionLog> list = new List<MODEL.TransactionLog>();
            if (dt.Rows.Count > 0)
            {
                SetUserAndProduct(dt, list);
            }
            return list;
        } 
        #endregion


        /// <summary>
        /// 购物记录 创建商品 创建用户
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="list"></param>
        internal void SetUserAndProduct(DataTable dt, IList<MODEL.TransactionLog> list)
        {
             
            MODEL.TransactionLog tlog = null;
            DAL.Product product = null;
            DAL.Users user = null;
            foreach (DataRow dr in dt.Rows)
            {
                tlog = new MODEL.TransactionLog();
                LoadEntityData(tlog, dr);
                DataTable dt1 = DbHelperSQL.GetDataTable("select * from Product where pId=" + dr["pId"]);
                product = new Product();
                tlog.PIdMODEL = new MODEL.Product();
                product.LoadEntityData(tlog.PIdMODEL, dt1.Rows[0]);
                DataTable dt2 = DbHelperSQL.GetDataTable("select * from Users where uId=" + dr["UserId"]);
                user = new Users();
                tlog.UserMODEL = new MODEL.Users();
                user.LoadEntityData(tlog.UserMODEL, dt2.Rows[0]);
                list.Add(tlog);
            }
        }
    }
}

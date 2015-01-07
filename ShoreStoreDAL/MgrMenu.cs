using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ShoeStore.DAL
{
    /// <summary>
    /// Author: SunCoder
    /// Description: 数据层 -- MgrMenu的实体类.
    /// 创建时间:2014-06-18 0:45:30
    /// </summary>
    public class MgrMenu
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
            strSql.Append("exec('delete MgrMenu where mgrId in ('+@ids+')')");
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
            string strSql = "delete MgrMenu where mgrId = @id";
            SqlParameter para = new SqlParameter("@id", idInt);
            return DbHelperSQL.ExcuteNonQuery(strSql, para);
        }
        #endregion

        #region 03.根据ID获得实体对象 +MODEL.MgrMenu GetModel(int intId)
        /// <summary>
        /// 根据ID获得实体对象
        /// </summary>
        /// <param name="intId">id值</param>
        /// <returns>实体对象</returns>
        public ShoeStore.MODEL.MgrMenu GetModel(int intId)
        {
            StringBuilder strSql = new StringBuilder("select mgrId,mgrPId,mgrName,mgrLinkUrl,mgrSort,mgrAddtime from MgrMenu ");
            strSql.Append(" where mgrId=@intId ");
            ShoeStore.MODEL.MgrMenu model = new ShoeStore.MODEL.MgrMenu();
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

        #region 04.查询数据集合 +IList<ShoeStore.MODEL.MgrMenu> GetList()
        /// <summary>
        /// 查询数据集合
        /// </summary>
        public IList<ShoeStore.MODEL.MgrMenu> GetList()
        {
            return GetListByWhere("");
        }
        #endregion

        #region 根据where条件查询数据集合 -IList<ShoeStore.MODEL.MgrMenu> GetListByWhere(string strWhere)
        /// <summary>
        /// 根据where条件查询数据集合
        /// </summary>
        internal IList<ShoeStore.MODEL.MgrMenu> GetListByWhere(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select mgrId,mgrPId,mgrName,mgrLinkUrl,mgrSort,mgrAddtime ");
            strSql.Append(" FROM MgrMenu ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataTable dt = DbHelperSQL.GetDataTable(strSql.ToString() + " order by mgrSort asc");
          
            IList<ShoeStore.MODEL.MgrMenu> list = null;
            if (dt.Rows.Count > 0)
            {
                list = Table2List(dt);
            }
            return list;
        }
        #endregion

        #region a01.将数据表转换成泛型集合接口+ IList<MODEL.MgrMenu> Table2List(DataTable dt)
        /// <summary>
        /// a01.将数据表转换成泛型集合接口
        /// </summary>
        /// <param name="dt">数据表对象</param>
        /// <returns>泛型集合接口</returns>
        public IList<ShoeStore.MODEL.MgrMenu> Table2List(DataTable dt)
        {
            List<ShoeStore.MODEL.MgrMenu> list = null;
            if (dt.Rows.Count > 0)
            {
                list = new List<ShoeStore.MODEL.MgrMenu>();
                ShoeStore.MODEL.MgrMenu model = null;
                foreach (DataRow dr in dt.Rows)
                {
                    model = new ShoeStore.MODEL.MgrMenu();
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
        internal void LoadEntityData(ShoeStore.MODEL.MgrMenu model, DataRow dr)
        {
            if (dr.Table.Columns.Contains("mgrId") && !dr.IsNull("mgrId"))
            {
                model.MgrId = int.Parse(dr["mgrId"].ToString());
            }
            if (dr.Table.Columns.Contains("mgrPId") && !dr.IsNull("mgrPId"))
            {
                model.MgrPId = int.Parse(dr["mgrPId"].ToString());
            }
            model.MgrName = dr["mgrName"].ToString();
            model.MgrLinkUrl = dr["mgrLinkUrl"].ToString();
            if (dr.Table.Columns.Contains("mgrSort") && !dr.IsNull("mgrSort"))
            {
                model.MgrSort = int.Parse(dr["mgrSort"].ToString());
            }
         
            if (dr.Table.Columns.Contains("mgrAddtime") && !dr.IsNull("mgrAddtime"))
            {
                model.MgrAddtime = DateTime.Parse(dr["mgrAddtime"].ToString());
            }

        }
        #endregion

        #region 07.新增数据 +int Add(MODEL.BlogArticleCate model)
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model">数据实体对象</param>
        /// <returns>新增成功的ID号</returns>
        public int Add(ShoeStore.MODEL.MgrMenu model)
        {
            int result = 0;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into MgrMenu(");
                strSql.Append("mgrPId,mgrName,mgrLinkUrl,mgrSort,mgrAddtime)");
                strSql.Append(" values (");
                strSql.Append("@mgrPId,@mgrName,@mgrLinkUrl,@mgrSort,@mgrAddtime)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
                    new SqlParameter("@mgrPId", SqlDbType.Int,4),
                    new SqlParameter("@mgrName", SqlDbType.VarChar,50),
                    new SqlParameter("@mgrLinkUrl", SqlDbType.VarChar,150),
                    new SqlParameter("@mgrSort", SqlDbType.Int,4),
             
                    new SqlParameter("@mgrAddtime", SqlDbType.DateTime,8)};
                parameters[0].Value = model.MgrPId;
                parameters[1].Value = model.MgrName;
                parameters[2].Value = model.MgrLinkUrl;
                parameters[3].Value = model.MgrSort;
                
                parameters[4].Value = model.MgrAddtime;
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
        public int Update(ShoeStore.MODEL.MgrMenu model)
        {
            int res = -2;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MgrMenu set ");
            strSql.Append("mgrPId=@mgrPId,");
            strSql.Append("mgrName=@mgrName,");
            strSql.Append("mgrLinkUrl=@mgrLinkUrl,");
            strSql.Append("mgrSort=@mgrSort,");
         
            strSql.Append("mgrAddtime=@mgrAddtime");
            strSql.Append(" where mgrId=@mgrId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@mgrId", SqlDbType.Int,4),
                    new SqlParameter("@mgrPId", SqlDbType.Int,4),
                    new SqlParameter("@mgrName", SqlDbType.VarChar,50),
                    new SqlParameter("@mgrLinkUrl", SqlDbType.VarChar,150),
                    new SqlParameter("@mgrSort", SqlDbType.Int,4),
                   
                    new SqlParameter("@mgrAddtime", SqlDbType.DateTime,8)};
            parameters[0].Value = model.MgrId;
            parameters[1].Value = model.MgrPId;
            parameters[2].Value = model.MgrName;
            parameters[3].Value = model.MgrLinkUrl;
            parameters[4].Value = model.MgrSort;
         
            parameters[5].Value = model.MgrAddtime;

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
    }
}

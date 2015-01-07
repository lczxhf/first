using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ShoeStore.DAL
{
    /// <summary>
    /// Author: SunCoder
    /// Description: 数据层 -- T_Settings的实体类.
    /// 创建时间:2014-06-20 4:57:19
    /// </summary>
    public class T_Settings
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
            strSql.Append("exec('delete T_Settings where Id in ('+@ids+')')");
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
            string strSql = "delete T_Settings where Id = @id";
            SqlParameter para = new SqlParameter("@id", idInt);
            return DbHelperSQL.ExcuteNonQuery(strSql, para);
        }
        #endregion

        #region 03.根据ID获得实体对象 +MODEL.T_Settings GetModel(int intId)
        /// <summary>
        /// 根据ID获得实体对象
        /// </summary>
        /// <param name="intId">id值</param>
        /// <returns>实体对象</returns>
        public ShoeStore.MODEL.T_Settings GetModel(int intId)
        {
            StringBuilder strSql = new StringBuilder("select Id,Name,Value from T_Settings ");
            strSql.Append(" where Id=@intId ");
            ShoeStore.MODEL.T_Settings model = new ShoeStore.MODEL.T_Settings();
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

        #region 04.查询数据集合 +IList<ShoeStore.MODEL.T_Settings> GetList()
        /// <summary>
        /// 查询数据集合
        /// </summary>
        public IList<ShoeStore.MODEL.T_Settings> GetList()
        {
            return GetListByWhere("");
        }
        #endregion

        #region 根据where条件查询数据集合 -IList<ShoeStore.MODEL.T_Settings> GetListByWhere(string strWhere)
        /// <summary>
        /// 根据where条件查询数据集合
        /// </summary>
        internal IList<ShoeStore.MODEL.T_Settings> GetListByWhere(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,Name,Value ");
            strSql.Append(" FROM T_Settings ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataTable dt = DbHelperSQL.GetDataTable(strSql.ToString());
            IList<ShoeStore.MODEL.T_Settings> list = null;
            if (dt.Rows.Count > 0)
            {
                list = Table2List(dt);
            }
            return list;
        }
        #endregion

        #region a01.将数据表转换成泛型集合接口+ IList<MODEL.T_Settings> Table2List(DataTable dt)
        /// <summary>
        /// a01.将数据表转换成泛型集合接口
        /// </summary>
        /// <param name="dt">数据表对象</param>
        /// <returns>泛型集合接口</returns>
        public IList<ShoeStore.MODEL.T_Settings> Table2List(DataTable dt)
        {
            List<ShoeStore.MODEL.T_Settings> list = null;
            if (dt.Rows.Count > 0)
            {
                list = new List<ShoeStore.MODEL.T_Settings>();
                ShoeStore.MODEL.T_Settings model = null;
                foreach (DataRow dr in dt.Rows)
                {
                    model = new ShoeStore.MODEL.T_Settings();
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
        internal void LoadEntityData(ShoeStore.MODEL.T_Settings model, DataRow dr)
        {
            if (dr.Table.Columns.Contains("Id") && !dr.IsNull("Id"))
            {
                model.Id = long.Parse(dr["Id"].ToString());
            }
            model.Name = dr["Name"].ToString();
            model.Value = dr["Value"].ToString();

        }
        #endregion

        #region 07.新增数据 +int Add(MODEL.BlogArticleCate model)
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model">数据实体对象</param>
        /// <returns>新增成功的ID号</returns>
        public int Add(ShoeStore.MODEL.T_Settings model)
        {
            int result = 0;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into T_Settings(");
                strSql.Append("Name,Value)");
                strSql.Append(" values (");
                strSql.Append("@Name,@Value)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
                    new SqlParameter("@Name", SqlDbType.VarChar,250),
                    new SqlParameter("@Value", SqlDbType.VarChar,-1)};
                parameters[0].Value = model.Name;
                parameters[1].Value = model.Value;
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
        public int Update(ShoeStore.MODEL.T_Settings model)
        {
            int res = -2;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Settings set ");
            strSql.Append("Name=@Name,");
            strSql.Append("Value=@Value");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Float,8),
                    new SqlParameter("@Name", SqlDbType.VarChar,250),
                    new SqlParameter("@Value", SqlDbType.VarChar,-1)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Value;

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

        public Dictionary<string, string> GetSetting()
        {
            DataTable dt = DbHelperSQL.GetDataTable("select * from T_Settings");
            if (dt.Rows.Count > 0)
            {
                Dictionary<string, string> setting = new Dictionary<string, string>();
                foreach (DataRow dr in dt.Rows)
                {
                    setting.Add(dr[1].ToString (), dr[2].ToString ());
                }
                return setting;
            }
            return null;
        }
    }
}

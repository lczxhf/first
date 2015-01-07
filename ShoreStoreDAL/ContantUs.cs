using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ShoeStore.DAL
{
    /// <summary>
    /// Author: SunCoder
    /// Description: 数据层 -- ContantUs的实体类.
    /// 创建时间:2014-06-20 18:29:53
    /// </summary>
    public class ContantUs
    {
        #region 01.修改软删除标志
        /// <summary>
        /// 修改软删除标志
        /// </summary>
        /// <param name="ids">要修改软删除标志的id号们(1,2,5)</param>
        /// <param name="isDel">要将删除标志 改成 true/false</param>
        /// <returns>受影响行数</returns>
        public int UpdateDel(string ids, bool isDel)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("exec('update ContantUs set noDelKey=''" + isDel.ToString() + "'' where Id in ('+@ids+')')");
            SqlParameter para = new SqlParameter("@ids", ids);
            return DbHelperSQL.ExcuteNonQuery(strSql.ToString(), para);
        }
        #endregion

        #region 01.2单个修改软删除标志
        /// <summary>
        /// 单个修改软删除标志
        /// </summary>
        /// <param name="idInt">要修改软删除标志的id号</param>
        /// <param name="isDel">要将删除标志 改成 true/false</param>
        /// <returns>受影响行数</returns>
        public int UpdateDel(int idInt, bool isDel)
        {
            string strSql = "update ContantUs set noDelKey='" + isDel.ToString() + "' where Id = @id";
            SqlParameter para = new SqlParameter("@id", idInt);
            return DbHelperSQL.ExcuteNonQuery(strSql, para);
        }
        #endregion

        #region 02.执行物理删除 +int Del(string ids)
        /// <summary>
        /// 执行物理删除
        /// </summary>
        /// <param name="ids">要删除的id号们(1,2,5)</param>
        /// <returns>受影响行数</returns>
        public int Del(string ids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("exec('delete ContantUs where Id in ('+@ids+')')");
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
            string strSql = "delete ContantUs where Id = @id";
            SqlParameter para = new SqlParameter("@id", idInt);
            return DbHelperSQL.ExcuteNonQuery(strSql, para);
        }
        #endregion

        #region 03.根据ID获得实体对象 +MODEL.ContantUs GetModel(int intId)
        /// <summary>
        /// 根据ID获得实体对象
        /// </summary>
        /// <param name="intId">id值</param>
        /// <returns>实体对象</returns>
        public ShoeStore.MODEL.ContantUs GetModel(int intId)
        {
            StringBuilder strSql = new StringBuilder("select Id,UserId,Name,Email,Phone,Message,mTime from ContantUs ");
            strSql.Append(" where Id=@intId ");
            ShoeStore.MODEL.ContantUs model = new ShoeStore.MODEL.ContantUs();
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

        #region 04.查询数据集合 +IList<ShoeStore.MODEL.ContantUs> GetList()
        /// <summary>
        /// 查询数据集合
        /// </summary>
        public IList<ShoeStore.MODEL.ContantUs> GetList()
        {
            return GetListByWhere("");
        }
        #endregion

        #region 根据where条件查询数据集合 -IList<ShoeStore.MODEL.ContantUs> GetListByWhere(string strWhere)
        /// <summary>
        /// 根据where条件查询数据集合
        /// </summary>
        internal IList<ShoeStore.MODEL.ContantUs> GetListByWhere(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,UserId,Name,Email,Phone,Message,mTime ");
            strSql.Append(" FROM ContantUs ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataTable dt = DbHelperSQL.GetDataTable(strSql.ToString());
            IList<ShoeStore.MODEL.ContantUs> list = null;
            if (dt.Rows.Count > 0)
            {
                list = Table2List(dt);
            }
            return list;
        }
        #endregion

        #region a01.将数据表转换成泛型集合接口+ IList<MODEL.ContantUs> Table2List(DataTable dt)
        /// <summary>
        /// a01.将数据表转换成泛型集合接口
        /// </summary>
        /// <param name="dt">数据表对象</param>
        /// <returns>泛型集合接口</returns>
        public IList<ShoeStore.MODEL.ContantUs> Table2List(DataTable dt)
        {
            List<ShoeStore.MODEL.ContantUs> list = null;
            if (dt.Rows.Count > 0)
            {
                list = new List<ShoeStore.MODEL.ContantUs>();
                ShoeStore.MODEL.ContantUs model = null;
                foreach (DataRow dr in dt.Rows)
                {
                    model = new ShoeStore.MODEL.ContantUs();
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
        internal void LoadEntityData(ShoeStore.MODEL.ContantUs model, DataRow dr)
        {
            if (dr.Table.Columns.Contains("Id") && !dr.IsNull("Id"))
            {
                model.Id = int.Parse(dr["Id"].ToString());
            }
            if (dr.Table.Columns.Contains("UserId") && !dr.IsNull("UserId"))
            {
                model.UserId = int.Parse(dr["UserId"].ToString());
            }
            model.Name = dr["Name"].ToString();
            model.Email = dr["Email"].ToString();
            if (dr.Table.Columns.Contains("Phone") && !dr.IsNull("Phone"))
            {
                model.Phone = int.Parse(dr["Phone"].ToString());
            }
            model.Message = dr["Message"].ToString();
            if (dr.Table.Columns.Contains("mTime") && !dr.IsNull("mTime"))
            {
                model.MTime = DateTime.Parse(dr["mTime"].ToString());
            }

        }
        #endregion

        #region 07.新增数据 +int Add(MODEL.BlogArticleCate model)
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model">数据实体对象</param>
        /// <returns>新增成功的ID号</returns>
        public int Add(ShoeStore.MODEL.ContantUs model)
        {
            int result = 0;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into ContantUs(");
                strSql.Append("UserId,Name,Email,Phone,Message,mTime)");
                strSql.Append(" values (");
                strSql.Append("@UserId,@Name,@Email,@Phone,@Message,@mTime)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
                    new SqlParameter("@UserId", SqlDbType.Int,4),
                    new SqlParameter("@Name", SqlDbType.VarChar,50),
                    new SqlParameter("@Email", SqlDbType.VarChar,50),
                    new SqlParameter("@Phone", SqlDbType.Int,4),
                    new SqlParameter("@Message", SqlDbType.VarChar,-1),
                    new SqlParameter("@mTime", SqlDbType.DateTime,8)};
                parameters[0].Value = model.UserId;
                parameters[1].Value = model.Name;
                parameters[2].Value = model.Email;
                parameters[3].Value = model.Phone;
                parameters[4].Value = model.Message;
                parameters[5].Value = model.MTime;
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
        public int Update(ShoeStore.MODEL.ContantUs model)
        {
            int res = -2;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ContantUs set ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("Name=@Name,");
            strSql.Append("Email=@Email,");
            strSql.Append("Phone=@Phone,");
            strSql.Append("Message=@Message,");
            strSql.Append("mTime=@mTime");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4),
                    new SqlParameter("@UserId", SqlDbType.Int,4),
                    new SqlParameter("@Name", SqlDbType.VarChar,50),
                    new SqlParameter("@Email", SqlDbType.VarChar,50),
                    new SqlParameter("@Phone", SqlDbType.Int,4),
                    new SqlParameter("@Message", SqlDbType.VarChar,-1),
                    new SqlParameter("@mTime", SqlDbType.DateTime,8)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.Name;
            parameters[3].Value = model.Email;
            parameters[4].Value = model.Phone;
            parameters[5].Value = model.Message;
            parameters[6].Value = model.MTime;

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

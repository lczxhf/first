using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ShoeStore.DAL
{
    /// <summary>
    /// Author: SunCoder
    /// Description: 数据层 -- Users的实体类.
    /// 创建时间:2014-06-18 0:30:48
    /// </summary>
    public class Users
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
            strSql.Append("exec('delete Users where uId in ('+@ids+')')");
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
            string strSql = "delete Users where uId = @id";
            SqlParameter para = new SqlParameter("@id", idInt);
            return DbHelperSQL.ExcuteNonQuery(strSql, para);
        }
        #endregion

        #region 03.根据ID获得实体对象 +MODEL.Users GetModel(int intId)
        /// <summary>
        /// 根据ID获得实体对象
        /// </summary>
        /// <param name="intId">id值</param>
        /// <returns>实体对象</returns>
        public ShoeStore.MODEL.Users GetModel(int intId)
        {
            StringBuilder strSql = new StringBuilder("select uId,uLoginName,uPwd from Users ");
            strSql.Append(" where uId=@intId ");
            ShoeStore.MODEL.Users model = new ShoeStore.MODEL.Users();
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

        #region 04.查询数据集合 +IList<ShoeStore.MODEL.Users> GetList()
        /// <summary>
        /// 查询数据集合
        /// </summary>
        public IList<ShoeStore.MODEL.Users> GetList()
        {
            return GetListByWhere("");
        }
        #endregion

        #region 根据where条件查询数据集合 -IList<ShoeStore.MODEL.Users> GetListByWhere(string strWhere)
        /// <summary>
        /// 根据where条件查询数据集合
        /// </summary>
        internal IList<ShoeStore.MODEL.Users> GetListByWhere(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select uId,uLoginName,uPwd ");
            strSql.Append(" FROM Users ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataTable dt = DbHelperSQL.GetDataTable(strSql.ToString());
            IList<ShoeStore.MODEL.Users> list = null;
            if (dt.Rows.Count > 0)
            {
                list = Table2List(dt);
            }
            return list;
        }
        #endregion

        #region a01.将数据表转换成泛型集合接口+ IList<MODEL.Users> Table2List(DataTable dt)
        /// <summary>
        /// a01.将数据表转换成泛型集合接口
        /// </summary>
        /// <param name="dt">数据表对象</param>
        /// <returns>泛型集合接口</returns>
        public IList<ShoeStore.MODEL.Users> Table2List(DataTable dt)
        {
            List<ShoeStore.MODEL.Users> list = null;
            if (dt.Rows.Count > 0)
            {
                list = new List<ShoeStore.MODEL.Users>();
                ShoeStore.MODEL.Users model = null;
                foreach (DataRow dr in dt.Rows)
                {
                    model = new ShoeStore.MODEL.Users();
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
        internal void LoadEntityData(ShoeStore.MODEL.Users model, DataRow dr)
        {
            if (dr.Table.Columns.Contains("uId") && !dr.IsNull("uId"))
            {
                model.UId = int.Parse(dr["uId"].ToString());
            }
            if (dr.Table.Columns.Contains("uLoginName") && !dr.IsNull("uLoginName"))
            {
                model.ULoginName = dr["uLoginName"].ToString();
            } if (dr.Table.Columns.Contains("uPwd") && !dr.IsNull("uPwd"))
            {
                model.UPwd = dr["uPwd"].ToString();
            }
            
            

        }
        #endregion

        #region 07.新增数据 +int Add(MODEL.BlogArticleCate model)
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model">数据实体对象</param>
        /// <returns>新增成功的ID号</returns>
        public int Add(ShoeStore.MODEL.Users model)
        {
            int result = 0;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into Users(");
                strSql.Append("uLoginName,uPwd)");
                strSql.Append(" values (");
                strSql.Append("@uLoginName,@uPwd)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
                    new SqlParameter("@uLoginName", SqlDbType.VarChar,50),
                    new SqlParameter("@uPwd", SqlDbType.VarChar,32)};
                parameters[0].Value = model.ULoginName;
                parameters[1].Value = model.UPwd;
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
        public int Update(ShoeStore.MODEL.Users model)
        {
            int res = -2;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Users set ");
            strSql.Append("uLoginName=@uLoginName,");
            strSql.Append("uPwd=@uPwd");
            strSql.Append(" where uId=@uId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@uId", SqlDbType.Int,4),
                    new SqlParameter("@uLoginName", SqlDbType.VarChar,50),
                    new SqlParameter("@uPwd", SqlDbType.VarChar,32)};
            parameters[0].Value = model.UId;
            parameters[1].Value = model.ULoginName;
            parameters[2].Value = model.UPwd;

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

        /// <summary>
        /// 根据用户名查询用户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public MODEL.Users GetUser(string name)
        {
            DataTable dt = DbHelperSQL.GetDataTable("select * from Users where uLoginName=@uLoginName", new SqlParameter("@uLoginName", name));
            if (dt != null && dt.Rows.Count > 0)
            {
                MODEL.Users userModel = new MODEL.Users();
                LoadEntityData(userModel, dt.Rows[0]);
                return userModel;
            }
            return null;
        }

        public bool DelAllAboutUser(int userId)
        {
            if(DbHelperSQL.ExcuteNonQueryWithProc("DelUserAll",new SqlParameter(@"userId",userId))>0)
            {
                return true;

            }
            return false;
        }

        public int GetUserId(string userName)
        {
            int i = 0;
            try
            {
               i=Convert.ToInt32( DbHelperSQL.ExcuteScalar("select uId from Users where uLoginName='" + userName+"'"));
            }
            catch 
            {
                return 0;
            }
            return i;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ShoeStore.DAL
{
    /// <summary>
    /// Author: SunCoder
    /// Description: 数据层 -- UserInfo的实体类.
    /// 创建时间:2014-06-22 1:40:52
    /// </summary>
    public class UserInfo
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
            strSql.Append("exec('update UserInfo set noDelKey=''" + isDel.ToString() + "'' where iId in ('+@ids+')')");
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
            string strSql = "update UserInfo set noDelKey='" + isDel.ToString() + "' where iId = @id";
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
            strSql.Append("exec('delete UserInfo where iId in ('+@ids+')')");
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
            string strSql = "delete UserInfo where iId = @id";
            SqlParameter para = new SqlParameter("@id", idInt);
            return DbHelperSQL.ExcuteNonQuery(strSql, para);
        }
        #endregion

        #region 03.根据ID获得实体对象 +MODEL.UserInfo GetModel(int intId)
        /// <summary>
        /// 根据ID获得实体对象
        /// </summary>
        /// <param name="intId">id值</param>
        /// <returns>实体对象</returns>
        public ShoeStore.MODEL.UserInfo GetModel(int intId)
        {
            StringBuilder strSql = new StringBuilder("select * from UserInfo a join Users b on a.userId=b.uId ");
            strSql.Append(" where iId=@intId ");
            ShoeStore.MODEL.UserInfo model = new ShoeStore.MODEL.UserInfo();
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

        #region 04.查询数据集合 +IList<ShoeStore.MODEL.UserInfo> GetList()
        /// <summary>
        /// 查询数据集合
        /// </summary>
        public IList<ShoeStore.MODEL.UserInfo> GetList()
        {
            return GetListByWhere("");
        }
        #endregion

        #region 根据where条件查询数据集合 -IList<ShoeStore.MODEL.UserInfo> GetListByWhere(string strWhere)
        /// <summary>
        /// 根据where条件查询数据集合
        /// </summary>
        internal IList<ShoeStore.MODEL.UserInfo> GetListByWhere(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select iId,userId,iName,iAge,iAddress,iMoney,Phone,iPostCode,iEmail ");
            strSql.Append(" FROM UserInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataTable dt = DbHelperSQL.GetDataTable(strSql.ToString());
            IList<ShoeStore.MODEL.UserInfo> list = null;
            if (dt.Rows.Count > 0)
            {
                list = Table2List(dt);
            }
            return list;
        }
        #endregion

        #region a01.将数据表转换成泛型集合接口+ IList<MODEL.UserInfo> Table2List(DataTable dt)
        /// <summary>
        /// a01.将数据表转换成泛型集合接口
        /// </summary>
        /// <param name="dt">数据表对象</param>
        /// <returns>泛型集合接口</returns>
        public IList<ShoeStore.MODEL.UserInfo> Table2List(DataTable dt)
        {
            List<ShoeStore.MODEL.UserInfo> list = null;
            if (dt.Rows.Count > 0)
            {
                list = new List<ShoeStore.MODEL.UserInfo>();
                ShoeStore.MODEL.UserInfo model = null;
                foreach (DataRow dr in dt.Rows)
                {
                    model = new ShoeStore.MODEL.UserInfo();
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
        internal void LoadEntityData(ShoeStore.MODEL.UserInfo model, DataRow dr)
        {
            if (dr.Table.Columns.Contains("iId") && !dr.IsNull("iId"))
            {
                model.IId = int.Parse(dr["iId"].ToString());
            }
            if (dr.Table.Columns.Contains("userId") && !dr.IsNull("userId"))
            {
                model.UserId = int.Parse(dr["userId"].ToString());
                model.UserMODEL = new MODEL.Users();
                DAL.Users user = new Users();
                user.LoadEntityData(model.UserMODEL, dr);
            }
            if (dr.Table.Columns.Contains("iName") && !dr.IsNull("iName"))
            {
                model.IName = dr["iName"].ToString();
            }
            if (dr.Table.Columns.Contains("iAge") && !dr.IsNull("iAge"))
            {
                model.IAge = int.Parse(dr["iAge"].ToString());
            }
            if (dr.Table.Columns.Contains("iAddress") && !dr.IsNull("iAddress"))
            {
                model.IAddress = dr["iAddress"].ToString();
            }
            if (dr.Table.Columns.Contains("iMoney") && !dr.IsNull("iMoney"))
            {
                model.IMoney =Math.Round(decimal.Parse( dr["iMoney"].ToString()),2);
            }
            if (dr.Table.Columns.Contains("Phone") && !dr.IsNull("Phone"))
            {
                model.Phone =dr["Phone"].ToString();
            }
            if (dr.Table.Columns.Contains("iPostCode") && !dr.IsNull("iPostCode"))
            {
                model.IPostCode = int.Parse(dr["iPostCode"].ToString());
            }
            if (dr.Table.Columns.Contains("iEmail") && !dr.IsNull("iEmail"))
            {
                model.IEmail = dr["iEmail"].ToString();
            }
            

        }
        #endregion

        #region 07.新增数据 +int Add(MODEL.BlogArticleCate model)
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model">数据实体对象</param>
        /// <returns>新增成功的ID号</returns>
        public int Add(ShoeStore.MODEL.UserInfo model)
        {
            int result = 0;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into UserInfo(");
                strSql.Append("userId,iName,iAge,iAddress,iMoney,Phone,iPostCode,iEmail)");
                strSql.Append(" values (");
                strSql.Append("@userId,@iName,@iAge,@iAddress,@iMoney,@Phone,@iPostCode,@iEmail)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
                    new SqlParameter("@userId", SqlDbType.Int,4),
                    new SqlParameter("@iName", SqlDbType.VarChar,50),
                    new SqlParameter("@iAge", SqlDbType.Int,4),
                    new SqlParameter("@iAddress", SqlDbType.VarChar,-1),
                    new SqlParameter("@iMoney", SqlDbType.Money,8),
                    new SqlParameter("@Phone", SqlDbType.VarChar,11),
                    new SqlParameter("@iPostCode", SqlDbType.Int,4),
                    new SqlParameter("@iEmail", SqlDbType.VarChar,150)};
                parameters[0].Value = model.UserId;
                parameters[1].Value = model.IName;
                parameters[2].Value = model.IAge;
                parameters[3].Value = model.IAddress;
                parameters[4].Value = model.IMoney;
                parameters[5].Value = model.Phone;
                parameters[6].Value = model.IPostCode;
                parameters[7].Value = model.IEmail;
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
        public int Update(ShoeStore.MODEL.UserInfo model)
        {
            int res = -2;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UserInfo set ");
            strSql.Append("userId=@userId,");
            strSql.Append("iName=@iName,");
            strSql.Append("iAge=@iAge,");
            strSql.Append("iAddress=@iAddress,");
            strSql.Append("iMoney=@iMoney,");
            strSql.Append("Phone=@Phone,");
            strSql.Append("iPostCode=@iPostCode,");
            strSql.Append("iEmail=@iEmail");
            strSql.Append(" where iId=@iId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@iId", SqlDbType.Int,4),
                    new SqlParameter("@userId", SqlDbType.Int,4),
                    new SqlParameter("@iName", SqlDbType.VarChar,50),
                    new SqlParameter("@iAge", SqlDbType.Int,4),
                    new SqlParameter("@iAddress", SqlDbType.VarChar,-1),
                    new SqlParameter("@iMoney", SqlDbType.Money,8),
                    new SqlParameter("@Phone", SqlDbType.VarChar,11),
                    new SqlParameter("@iPostCode", SqlDbType.Int,4),
                    new SqlParameter("@iEmail", SqlDbType.VarChar,150)};
            parameters[0].Value = model.IId;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.IName;
            parameters[3].Value = model.IAge;
            parameters[4].Value = model.IAddress;
            parameters[5].Value = model.IMoney;
            parameters[6].Value = model.Phone;
            parameters[7].Value = model.IPostCode;
            parameters[8].Value = model.IEmail;

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

        public MODEL.UserInfo GetInfoByUserId(int userId)
        {
            DataTable dt = DbHelperSQL.GetDataTable("select * from UserInfo where userId="+userId);
            if (dt.Rows.Count > 0)
            {
                MODEL.UserInfo userInfo = new MODEL.UserInfo();
                LoadEntityData(userInfo, dt.Rows[0]);
                return userInfo;
            }
            return null;
        }

        public int UpdateSome(MODEL.UserInfo userInfo)
        {
            int i = DbHelperSQL.ExcuteNonQuery("update userInfo set iAddress=@Address,iAge=@Age,iEmmail=@Email,iName=@Name,iPostCode=@PostCode,Phone=@Phone where iId=" + userInfo.IId, new SqlParameter("@iAddress", userInfo.IAddress), new SqlParameter("@Phone", userInfo.Phone), new SqlParameter("@PostCode", userInfo.IPostCode), new SqlParameter("@Age", userInfo.IAge), new SqlParameter("@Name", userInfo.IName), new SqlParameter("@Email", userInfo.IEmail));
            if (i > 0) 
            {
                return i;
            }
            return 0;
        }

    }
}

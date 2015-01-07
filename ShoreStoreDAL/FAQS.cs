using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ShoeStore.DAL
{
    /// <summary>
    /// Author: SunCoder
    /// Description: 数据层 -- FAQS的实体类.
    /// 创建时间:2014-06-20 3:25:02
    /// </summary>
    public class FAQS
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
            strSql.Append("exec('delete FAQS where pId in ('+@ids+')')");
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
            string strSql = "delete FAQS where pId = @id";
            SqlParameter para = new SqlParameter("@id", idInt);
            return DbHelperSQL.ExcuteNonQuery(strSql, para);
        }
        #endregion

        #region 03.根据ID获得实体对象 +MODEL.FAQS GetModel(int intId)
        /// <summary>
        /// 根据ID获得实体对象
        /// </summary>
        /// <param name="intId">id值</param>
        /// <returns>实体对象</returns>
        public ShoeStore.MODEL.FAQS GetModel(int intId)
        {
            StringBuilder strSql = new StringBuilder("select pId,Question,Answer,fSort from FAQS ");
            strSql.Append(" where pId=@intId ");
            ShoeStore.MODEL.FAQS model = new ShoeStore.MODEL.FAQS();
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

        #region 04.查询数据集合 +IList<ShoeStore.MODEL.FAQS> GetList()
        /// <summary>
        /// 查询数据集合
        /// </summary>
        public IList<ShoeStore.MODEL.FAQS> GetList()
        {
            return GetListByWhere("");
        }
        #endregion

        #region 根据where条件查询数据集合 -IList<ShoeStore.MODEL.FAQS> GetListByWhere(string strWhere)
        /// <summary>
        /// 根据where条件查询数据集合
        /// </summary>
        internal IList<ShoeStore.MODEL.FAQS> GetListByWhere(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,Question,Answer,fSort ");
            strSql.Append(" FROM FAQS ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataTable dt = DbHelperSQL.GetDataTable(strSql.ToString());
            IList<ShoeStore.MODEL.FAQS> list = null;
            if (dt.Rows.Count > 0)
            {
                list = Table2List(dt);
            }
            return list;
        }
        #endregion

        #region a01.将数据表转换成泛型集合接口+ IList<MODEL.FAQS> Table2List(DataTable dt)
        /// <summary>
        /// a01.将数据表转换成泛型集合接口
        /// </summary>
        /// <param name="dt">数据表对象</param>
        /// <returns>泛型集合接口</returns>
        public IList<ShoeStore.MODEL.FAQS> Table2List(DataTable dt)
        {
            List<ShoeStore.MODEL.FAQS> list = null;
            if (dt.Rows.Count > 0)
            {
                list = new List<ShoeStore.MODEL.FAQS>();
                ShoeStore.MODEL.FAQS model = null;
                foreach (DataRow dr in dt.Rows)
                {
                    model = new ShoeStore.MODEL.FAQS();
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
        internal void LoadEntityData(ShoeStore.MODEL.FAQS model, DataRow dr)
        {
            if (dr.Table.Columns.Contains("pId") && !dr.IsNull("pId"))
            {
                model.PId = int.Parse(dr["pId"].ToString());
            }
            model.Question = dr["Question"].ToString();
            model.Answer = dr["Answer"].ToString();
            if (dr.Table.Columns.Contains("fSort") && !dr.IsNull("fSort"))
            {
                model.FSort = int.Parse(dr["fSort"].ToString());
            }

        }
        #endregion

        #region 07.新增数据 +int Add(MODEL.BlogArticleCate model)
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model">数据实体对象</param>
        /// <returns>新增成功的ID号</returns>
        public int Add(ShoeStore.MODEL.FAQS model)
        {
            int result = 0;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into FAQS(");
                strSql.Append("Question,Answer,fSort)");
                strSql.Append(" values (");
                strSql.Append("@Question,@Answer,@fSort)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
                    new SqlParameter("@Question", SqlDbType.VarChar,-1),
                    new SqlParameter("@Answer", SqlDbType.VarChar,-1),
                    new SqlParameter("@fSort", SqlDbType.Int,4)};
                parameters[0].Value = model.Question;
                parameters[1].Value = model.Answer;
                parameters[2].Value = model.FSort;
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
        public int Update(ShoeStore.MODEL.FAQS model)
        {
            int res = -2;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FAQS set ");
            strSql.Append("Question=@Question,");
            strSql.Append("Answer=@Answer,");
            strSql.Append("fSort=@fSort");
            strSql.Append(" where pId=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4),
                    new SqlParameter("@Question", SqlDbType.VarChar,-1),
                    new SqlParameter("@Answer", SqlDbType.VarChar,-1),
                    new SqlParameter("@fSort", SqlDbType.Int,4)};
            parameters[0].Value = model.PId;
            parameters[1].Value = model.Question;
            parameters[2].Value = model.Answer;
            parameters[3].Value = model.FSort;

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

      public IList<MODEL.FAQS> GetPageProduct( int pageIndex, int pageSize, out int pageCount, out int num)
        {
            DataTable dt= DbHelperSQL.GetPageListByProc("PageFAQS",pageIndex,pageSize,out pageCount, out num);
            IList<MODEL.FAQS> list = null;
            if (dt.Rows.Count > 0)
            {
                list=Table2List(dt);

            }
            return list;
        }
    }
}


using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ShoeStore.DAL
{
    /// <summary>
    /// Author: SunCoder
    /// Description: 数据层 -- ProductCate的实体类.
    /// 创建时间:2014-06-17 21:10:29
    /// </summary>
    public class ProductCate
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
            strSql.Append("exec('delete ProductCate where Id in ('+@ids+')')");
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
            string strSql = "delete ProductCate where Id = @id";
            SqlParameter para = new SqlParameter("@id", idInt);
            return DbHelperSQL.ExcuteNonQuery(strSql, para);
        }
        #endregion

        #region 03.根据ID获得实体对象 +MODEL.ProductCate GetModel(int intId)
        /// <summary>
        /// 根据ID获得实体对象
        /// </summary>
        /// <param name="intId">id值</param>
        /// <returns>实体对象</returns>
        public ShoeStore.MODEL.ProductCate GetModel(int intId)
        {
            StringBuilder strSql = new StringBuilder("select Id,pName,pcSort,PId from ProductCate ");
            strSql.Append(" where Id=@intId ");
            ShoeStore.MODEL.ProductCate model = new ShoeStore.MODEL.ProductCate();
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

        #region 04.查询数据集合 +IList<ShoeStore.MODEL.ProductCate> GetList()
        /// <summary>
        /// 查询数据集合
        /// </summary>
        public IList<ShoeStore.MODEL.ProductCate> GetList()
        {
            return GetListByWhere("");
        }
        #endregion

        #region 根据where条件查询数据集合 -IList<ShoeStore.MODEL.ProductCate> GetListByWhere(string strWhere)
        /// <summary>
        /// 根据where条件查询数据集合
        /// </summary>
        internal IList<ShoeStore.MODEL.ProductCate> GetListByWhere(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,pName,pcSort,PId ");
            strSql.Append(" FROM ProductCate ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataTable dt = DbHelperSQL.GetDataTable(strSql.ToString()+"order by pcSort asc");
            IList<ShoeStore.MODEL.ProductCate> list = null;
            if (dt.Rows.Count > 0)
            {
                list = Table2List(dt);
            }
            return list;
        }
        #endregion

        #region a01.将数据表转换成泛型集合接口+ IList<MODEL.ProductCate> Table2List(DataTable dt)
        /// <summary>
        /// a01.将数据表转换成泛型集合接口
        /// </summary>
        /// <param name="dt">数据表对象</param>
        /// <returns>泛型集合接口</returns>
        public IList<ShoeStore.MODEL.ProductCate> Table2List(DataTable dt)
        {
            List<ShoeStore.MODEL.ProductCate> list = null;
            if (dt.Rows.Count > 0)
            {
                list = new List<ShoeStore.MODEL.ProductCate>();
                ShoeStore.MODEL.ProductCate model = null;
                foreach (DataRow dr in dt.Rows)
                {
                    model = new ShoeStore.MODEL.ProductCate();
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
        internal void LoadEntityData(ShoeStore.MODEL.ProductCate model, DataRow dr)
        {
            if (dr.Table.Columns.Contains("Id") && !dr.IsNull("Id"))
            {
                model.Id = int.Parse(dr["Id"].ToString());
            }
            if (dr.Table.Columns.Contains("pName") && !dr.IsNull("pName"))
            {
                model.PName = dr["pName"].ToString();
            }
            if (dr.Table.Columns.Contains("pcSort") && !dr.IsNull("pcSort"))
            {
                model.PcSort = int.Parse(dr["pcSort"].ToString());
            }
            if (dr.Table.Columns.Contains("PId") && !dr.IsNull("PId"))
            {
                model.PId = int.Parse(dr["PId"].ToString());
            }
           
        }
        #endregion

        #region 07.新增数据 +int Add(MODEL.BlogArticleCate model)
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model">数据实体对象</param>
        /// <returns>新增成功的ID号</returns>
        public int Add(ShoeStore.MODEL.ProductCate model)
        {
            int result = 0;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into ProductCate(");
                strSql.Append("pName,pcSort,PId)");
                strSql.Append(" values (");
                strSql.Append("@pName,@pcSort,@PId)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
                    new SqlParameter("@pName", SqlDbType.VarChar,100),
                    new SqlParameter("@pcSort", SqlDbType.Int,4),
                    new SqlParameter("@PId", SqlDbType.Int,4)};
                parameters[0].Value = model.PName;
                parameters[1].Value = model.PcSort;
                parameters[2].Value = model.PId;
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
        public int Update(ShoeStore.MODEL.ProductCate model)
        {
            int res = -2;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ProductCate set ");
            strSql.Append("pName=@pName,");
            strSql.Append("pcSort=@pcSort,");
            strSql.Append("PId=@PId");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4),
                    new SqlParameter("@pName", SqlDbType.VarChar,100),
                    new SqlParameter("@pcSort", SqlDbType.Int,4),
                    new SqlParameter("@PId", SqlDbType.Int,4)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.PName;
            parameters[2].Value = model.PcSort;
            parameters[3].Value = model.PId;

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
        /////////////////////////////////////////////////////

        //internal List<ShoeStore.MODEL.ProductCate> GetList()
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select Id,pName,pcSort ");
        //    strSql.Append(" FROM ProductCate ");
            
        //    DataTable dt = DbHelperSQL.GetDataTable(strSql.ToString() + "order by pcSort asc");
        //    List<ShoeStore.MODEL.ProductCate> list = null;
        //    if (dt.Rows.Count > 0)
        //    {
        //        list = TabletoList(dt);
        //    }
        //    return list;
        //}
        //public List<ShoeStore.MODEL.ProductCate> TabletoList(DataTable dt)
        //{
        //    List<ShoeStore.MODEL.ProductCate> list = null;
        //    if (dt.Rows.Count > 0)
        //    {
        //        list = new List<ShoeStore.MODEL.ProductCate>();
        //        ShoeStore.MODEL.ProductCate model = null;
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            model = new ShoeStore.MODEL.ProductCate();
        //            LoadEntityData(model, dr);
        //            list.Add(model);
        //        }
        //        return list;
        //    }
        //    return null;
        //}

        public IList<MODEL.ProductCate> GetListByParent()
        {
           return GetListByWhere(" PId=0");
        }

        internal void NewLoadData(ShoeStore.MODEL.ProductCate model, DataRow dr)
        {
            if (dr.Table.Columns.Contains("Id") && !dr.IsNull("Id"))
            {
                model.Id = int.Parse(dr["Id"].ToString());
            }
            if (dr.Table.Columns.Contains("Name") && !dr.IsNull("Name"))
            {
                model.PName = dr["Name"].ToString();
            }
            if (dr.Table.Columns.Contains("pcSort") && !dr.IsNull("pcSort"))
            {
                model.PcSort = int.Parse(dr["pcSort"].ToString());
            }
            if (dr.Table.Columns.Contains("cPId") && !dr.IsNull("cPId"))
            {
                model.PId = int.Parse(dr["cPId"].ToString());
            }

        }

        public string GetCateName(int cateId)
        {
            DataTable dt = DbHelperSQL.GetDataTable("select pName from ProductCate where Id=" + cateId);
            string a = dt.Rows[0]["pName"].ToString();
            return a;
        }


    }
}

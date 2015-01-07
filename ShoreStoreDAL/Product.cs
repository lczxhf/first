using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ShoeStore.DAL
{
    /// <summary>
    /// Author: SunCoder
    /// Description: 数据层 -- Product的实体类.
    /// 创建时间:2014-06-17 21:11:52
    /// </summary>
    public class Product
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
            strSql.Append("exec('delete Product where pId in ('+@ids+')')");
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
            string strSql = "delete Product where pId = @id";
            SqlParameter para = new SqlParameter("@id", idInt);
            return DbHelperSQL.ExcuteNonQuery(strSql, para);
        }
        #endregion

        #region 03.根据ID获得实体对象 +MODEL.Product GetModel(int intId)
        /// <summary>
        /// 根据ID获得实体对象
        /// </summary>
        /// <param name="intId">id值</param>
        /// <returns>实体对象</returns>
        public ShoeStore.MODEL.Product GetModel(int intId)
        {
            StringBuilder strSql = new StringBuilder("select pId,pName,pPrice,pRemark,pSrc,pNum,pIsrec,pCateid,pSort from Product ");
            strSql.Append(" where pId=@intId ");
            ShoeStore.MODEL.Product model = new ShoeStore.MODEL.Product();
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

        #region 04.查询数据集合 +IList<ShoeStore.MODEL.Product> GetList()
        /// <summary>
        /// 查询数据集合
        /// </summary>
        public IList<ShoeStore.MODEL.Product> GetList()
        {
            return GetListByWhere("");
        }
        #endregion

        #region 根据where条件查询数据集合 -IList<ShoeStore.MODEL.Product> GetListByWhere(string strWhere)
        /// <summary>
        /// 根据where条件查询数据集合
        /// </summary>
        internal IList<ShoeStore.MODEL.Product> GetListByWhere(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select pId,pName,pPrice,pRemark,pSrc,pNum,pIsrec,pCateid,pSort ");
            strSql.Append(" FROM Product ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            DataTable dt = DbHelperSQL.GetDataTable(strSql.ToString());
            IList<ShoeStore.MODEL.Product> list = null;
            if (dt.Rows.Count > 0)
            {
                list = Table2List(dt);
            }
            return list;
        }
        #endregion

        #region a01.将数据表转换成泛型集合接口+ IList<MODEL.Product> Table2List(DataTable dt)
        /// <summary>
        /// a01.将数据表转换成泛型集合接口
        /// </summary>
        /// <param name="dt">数据表对象</param>
        /// <returns>泛型集合接口</returns>
        public IList<ShoeStore.MODEL.Product> Table2List(DataTable dt)
        {
            List<ShoeStore.MODEL.Product> list = null;
            if (dt.Rows.Count > 0)
            {
                list = new List<ShoeStore.MODEL.Product>();
                ShoeStore.MODEL.Product model = null;
                foreach (DataRow dr in dt.Rows)
                {
                    model = new ShoeStore.MODEL.Product();
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
        internal void LoadEntityData(ShoeStore.MODEL.Product model, DataRow dr)
        {
            if (dr.Table.Columns.Contains("pId") && !dr.IsNull("pId"))
            {
                model.PId = int.Parse(dr["pId"].ToString());
            }
            if (dr.Table.Columns.Contains("PName") && !dr.IsNull("PName"))
            {
                model.PName = dr["pName"].ToString();
            }
            if (dr.Table.Columns.Contains("PPrice") && !dr.IsNull("PPrice"))
            {
                model.PPrice = Math.Round( decimal.Parse(dr["PPrice"].ToString()),2);
            }

            if (dr.Table.Columns.Contains("pRemark") && !dr.IsNull("PPrice"))
            {
                model.PRemark = dr["pRemark"].ToString();
            }
            if (dr.Table.Columns.Contains("pSrc") && !dr.IsNull("pSrc"))
            {
                model.PSrc = dr["pSrc"].ToString();
            }
            if (dr.Table.Columns.Contains("pNum") && !dr.IsNull("pNum"))
            {
                model.PNum = int.Parse(dr["pNum"].ToString());
            }
            if (dr.Table.Columns.Contains("pIsrec") && !dr.IsNull("pIsrec"))
            {
                model.PIsrec = bool.Parse(dr["pIsrec"].ToString());
            }
            if (dr.Table.Columns.Contains("pCateid") && !dr.IsNull("pCateid"))
            {
                model.PCateid = int.Parse(dr["pCateid"].ToString());
                model.PCateidMODEL = new MODEL.ProductCate();
                DAL.ProductCate pc = new ProductCate();
                pc.NewLoadData(model.PCateidMODEL, dr);
                
            }
            if (dr.Table.Columns.Contains("pSort") && !dr.IsNull("pSort"))
            {
                model.PSort = int.Parse(dr["pSort"].ToString());
            }

        }
        #endregion

        #region 07.新增数据 +int Add(MODEL.BlogArticleCate model)
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model">数据实体对象</param>
        /// <returns>新增成功的ID号</returns>
        public int Add(ShoeStore.MODEL.Product model)
        {
            int result = 0;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into Product(");
                strSql.Append("pName,pPrice,pRemark,pSrc,pNum,pIsrec,pCateid,pSort)");
                strSql.Append(" values (");
                strSql.Append("@pName,@pPrice,@pRemark,@pSrc,@pNum,@pIsrec,@pCateid,@pSort)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
                    new SqlParameter("@pName", SqlDbType.VarChar,50),
                    new SqlParameter("@pPrice", SqlDbType.Money,8),
                    new SqlParameter("@pRemark", SqlDbType.VarChar,-1),
                    new SqlParameter("@pSrc", SqlDbType.VarChar,550),
                    new SqlParameter("@pNum", SqlDbType.Int,4),
                    new SqlParameter("@pIsrec", SqlDbType.Bit,1),
                    new SqlParameter("@pCateid", SqlDbType.Int,4),
                    new SqlParameter("@pSort", SqlDbType.Int,4)};
                parameters[0].Value = model.PName;
                parameters[1].Value = model.PPrice;
                parameters[2].Value = model.PRemark;
                parameters[3].Value = model.PSrc;
                parameters[4].Value = model.PNum;
                parameters[5].Value = model.PIsrec;
                parameters[6].Value = model.PCateid;
                parameters[7].Value = model.PSort;
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
        public int Update(ShoeStore.MODEL.Product model)
        {
            int res = -2;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Product set ");
            strSql.Append("pName=@pName,");
            strSql.Append("pPrice=@pPrice,");
            strSql.Append("pRemark=@pRemark,");
            strSql.Append("pSrc=@pSrc,");
            strSql.Append("pNum=@pNum,");
            strSql.Append("pIsrec=@pIsrec,");
            strSql.Append("pCateid=@pCateid,");
            strSql.Append("pSort=@pSort");
            strSql.Append(" where pId=@pId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@pId", SqlDbType.Int,4),
                    new SqlParameter("@pName", SqlDbType.VarChar,50),
                    new SqlParameter("@pPrice", SqlDbType.Money,8),
                    new SqlParameter("@pRemark", SqlDbType.VarChar,-1),
                    new SqlParameter("@pSrc", SqlDbType.VarChar,550),
                    new SqlParameter("@pNum", SqlDbType.Int,4),
                    new SqlParameter("@pIsrec", SqlDbType.Bit,1),
                    new SqlParameter("@pCateid", SqlDbType.Int,4),
                    new SqlParameter("@pSort", SqlDbType.Int,4)};
            parameters[0].Value = model.PId;
            parameters[1].Value = model.PName;
            parameters[2].Value = model.PPrice;
            parameters[3].Value = model.PRemark;
            parameters[4].Value = model.PSrc;
            parameters[5].Value = model.PNum;
            parameters[6].Value = model.PIsrec;
            parameters[7].Value = model.PCateid;
            parameters[8].Value = model.PSort;

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

        #region 获取商品推荐列表
        /// <summary>
        /// 获取商品推荐列表
        /// </summary>
        /// <returns></returns>
        public IList<MODEL.Product> GetListByIsrec()
        {
            return this.GetListByWhere("pIsrec =1");

        } 
        #endregion



        public IList<MODEL.Product> GetPageProduct( int pageIndex, int pageSize, out int pageCount, out int num)
        {
            DataTable dt= DbHelperSQL.GetPageListByProc("PageProduct",pageIndex,pageSize,out pageCount, out num);
            IList<MODEL.Product> list = null;
            if (dt.Rows.Count > 0)
            {
                list=Table2List(dt);
              
            }
            return list;
        }


        public IList<MODEL.Product> GetSearch( string word, int pageIndex, int pageSize, out int pageCount, out int num)
        {
            word = "%" + word + "%";
           word = word.Trim().Replace(' ','%');
            DataTable dt = DbHelperSQL.GetSearch("SearchProduct", word, pageIndex, pageSize, out  pageCount, out  num);
            IList<MODEL.Product> list = null;
            if (dt.Rows.Count > 0)
            {
                list = Table2List(dt);

            }
            return list;
        }

        public IList<MODEL.Product> GetProByCate(int id, int pageIndex, int pageSize, out int pageCount, out int num)
        {

            DataTable dt = DbHelperSQL.GetProByCate("CateProduct", id, pageIndex, pageSize, out  pageCount, out  num);
            IList<MODEL.Product> list = null;
            if (dt.Rows.Count > 0)
            {
                list = Table2List(dt);

            }
            return list;
        }

        public IList<MODEL.Product> GetRelated(int Cateid)
        {
            DataTable dt = DbHelperSQL.GetDataTable("select a.*,b.pName from Product a  join ProductCate b on a.pCateid=b.Id and a.pCateid=" + Cateid);
            IList<ShoeStore.MODEL.Product> list = null;
            if (dt.Rows.Count > 0)
            {
                list = Table2List(dt);
            }
            return list;
        }

        /// <summary>
        /// 根据商品名获取商品id
        /// </summary>
        /// <param name="pName"></param>
        /// <returns></returns>
        public int GetProductId(string pName)
        {
            int i = 0;
            try
            {
                i = Convert.ToInt32(DbHelperSQL.ExcuteScalar("select pId from Product where pName='" + pName+"'"));
            }
            catch 
            {
                return 0;
            }
            return i;
        }

        /// <summary>
        /// 用户结账的时候 更改信息
        /// </summary>
        /// <param name="num"></param>
        /// <param name="userId"></param>
        /// <param name="pName"></param>
        /// <param name="itemId"></param>
        /// <param name="almost"></param>
        /// <returns></returns>
        public int UpdateCount(int num, int userId, string pName, int itemId, int almost)
        {
            try
            {
                return DbHelperSQL.ExcuteNonQueryWithProc("CheckOut", new SqlParameter("@userid", userId), new SqlParameter("@Num", num), new SqlParameter("@itemId", itemId), new SqlParameter("@pName", pName), new SqlParameter("@almost", almost));
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据商品名查询某商品的库存
        /// </summary>
        /// <param name="pName"></param>
        /// <returns></returns>
        public int GetProductCount(string pName)
        {
            return (int)DbHelperSQL.ExcuteScalar("select pNum from Product where pName='" + pName + "'");
        }

        /// <summary>
        /// 根据用户id查询某用户的余额
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int UserMoney(int userId)
        {
            return (int)DbHelperSQL.ExcuteScalar("select iMoney from UserInfo where userId=" + userId);
        }


        /// <summary>
        /// 判断根据商品名判断商品是否存在
        /// </summary>
        /// <param name="pName"></param>
        /// <returns></returns>
        public bool ProductNameIsExist(string pName)
        {
            if ((int)DbHelperSQL.ExcuteScalar("select count(*) from Product where pName'" + pName + "'") == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

}

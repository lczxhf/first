using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// 分页数据实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedAjaxData<T>
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount;

        /// <summary>
        /// 总行数
        /// </summary>
        public int RowCount;

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex;

        /// <summary>
        /// 当前页码数据
        /// </summary>
        public IList<T> PagedList;
    }
}

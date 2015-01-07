using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoeStore.Web.manage.View
{
    public partial class Products : BasePage
    {
       protected System.Text.StringBuilder sbhtml = new System.Text.StringBuilder();
       protected System.Text.StringBuilder page = new System.Text.StringBuilder(400);
        BLL.Product bllproduct = new BLL.Product();
        BLL.ProductCate bllCate = new BLL.ProductCate ();
        protected int PageCount;
        protected int num;
        protected int pageIndex = 1;
        public override void SonLoad()
        {
            if(!string.IsNullOrEmpty(Context.Request["pageIndex"]))
            {
                pageIndex=Convert.ToInt32(Context.Request["pageIndex"]);
            }
            IList<MODEL.Product> product = bllproduct.GetPageProduct(pageIndex, 6, out PageCount, out num);
            this.rptList.DataSource = product;
            this.rptList.DataBind();

            IList<MODEL.ProductCate> list = new BLL.ProductCate().GetList();
            MakeCateHtml(list, 0);
            PageBar();
        }

        void MakeCateHtml(IList<MODEL.ProductCate> icate, int pId)
        {
            foreach (MODEL.ProductCate cate in icate)
            {
                if (pId == 0 && cate.PId == 0)
                {
                    sbhtml.Append("<optgroup label='"+cate.PName+"'> ");
                    MakeCateHtml(icate, cate.Id);
                    sbhtml.Append("</optgroup>");
                    continue;
                }
                if (cate.PId == pId)
                {
                    sbhtml.Append("<option value='"+cate.Id+"'>"+cate.PName+"</option>");
                }
            }
          
        }

        protected void PageBar()
        {
            if (pageIndex == 1)
            {
                page.Append("<span class='disabled'>&nbsp;&lt;&lt;&nbsp;</span>");
                page.Append("<span class='disabled'>&nbsp;&lt;&nbsp;</span>");
            }
            else
            {
                page.Append("<a href='Products.aspx?pageIndex=" + 1 + "'>&nbsp;&lt;&lt;&nbsp;</a>");
                page.Append("<a href='Products.aspx?pageIndex=" + (pageIndex - 1).ToString() + "'>&nbsp;&lt;&nbsp;</a>");
            }
            for (int i = 1; i <= PageCount; i++)
            {
                if (pageIndex == i)
                {
                    page.Append("<span class='current'>" + i + "</span>");
                }
                else
                {
                    page.Append("<a href='Products.aspx?pageIndex=" + i + "'>" + i + "</a>");
                }

            }
            if (pageIndex == PageCount)
            {
                page.Append("<span class='disabled'>&nbsp;&gt;&nbsp;</span>");
                page.Append("<span class='disabled'>&nbsp;&gt;&gt;&nbsp;</span>");
            }
            else
            {
                page.Append("<a href='Products.aspx?pageIndex=" + (pageIndex + 1).ToString() +  "'>&nbsp;&gt;&nbsp;</a>");
                page.Append("<a href='Products.aspx?pageIndex=" + PageCount + "'>&nbsp;&gt;&gt;&nbsp;</a>");
            }
        }
    }
}
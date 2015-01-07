using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoeStore.Web.foreground.View
{
    public partial class Products : BasePage
    {
        BLL.Product bllproduct = new BLL.Product();
        protected System.Text.StringBuilder page = new System.Text.StringBuilder(400);
        protected IList<MODEL.Product> product = null;
        protected int PageCount;
        protected int num;
        protected string title;
        protected int pageIndex=1;
        protected string type;

        public override void SonLoad()
        {
            
            if (!string.IsNullOrEmpty(Context.Request["pageIndex"]))
            {
                pageIndex = Convert.ToInt32(Context.Request["pageIndex"]);
            }
            if ((Context.Request["type"]=="search"))
            {
                type = "search";
                product = bllproduct.GetSearch(Context.Request["keyword"], pageIndex,9, out PageCount, out num);
                if (!string.IsNullOrEmpty(Context.Request["keyword"]))
                {
                    HttpCookie cookie = new HttpCookie("keyword",Context.Request["keyword"]);
                    Context.Response.Cookies.Add(cookie);
                }
                title = "关于\""+Context.Response.Cookies["keyword"].Value + "\"的商品搜索列表";

                
                
            }
            else if (Context.Request["type"] == "cate")
            {
                type = "cate";
                product = bllproduct.GetProByCate(Convert.ToInt32(Context.Request["cateId"]), pageIndex,9, out PageCount, out num);
                title = Context.Request["name"];
            }
            else
            {
                type = "a";
                product = bllproduct.GetPageProduct(pageIndex,9, out PageCount, out num);
                title = "所有商品";
            }
            this.rptProduct.DataSource = product;
            this.rptProduct.DataBind();
            PageBar();
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
                page.Append("<a href='Products.aspx?pageIndex=" + 1 + "&type="+type+"'>&nbsp;&lt;&lt;&nbsp;</a>");
                page.Append("<a href='Products.aspx?pageIndex=" + (pageIndex - 1).ToString() + "&type=" + type + "'>&nbsp;&lt;&nbsp;</a>");
            }
            for (int i = 1; i <= PageCount; i++)
            {
                if (pageIndex == i)
                {
                    page.Append("<span class='current'>" + i + "</span>");
                }
                else
                {
                    page.Append("<a href='Products.aspx?pageIndex=" + i + "&type=" + type + "'>" + i + "</a>");
                }

            }
            if (pageIndex == PageCount)
            {
                page.Append("<span class='disabled'>&nbsp;&gt;&nbsp;</span>");
                page.Append("<span class='disabled'>&nbsp;&gt;&gt;&nbsp;</span>");
            }
            else
            {
                page.Append("<a href='Products.aspx?pageIndex=" + (pageIndex + 1).ToString() + "&type=" + type + "'>&nbsp;&gt;&nbsp;</a>");
                page.Append("<a href='Products.aspx?pageIndex=" + PageCount + "&type=" + type + "'>&nbsp;&gt;&gt;&nbsp;</a>");
            }
        }
    }
}
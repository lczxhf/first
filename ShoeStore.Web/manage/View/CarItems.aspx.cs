using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoeStore.Web.manage.View
{
    public partial class CarItems : BasePage
    {
        protected System.Text.StringBuilder page = new System.Text.StringBuilder(400);
        int pageIndex = 1;
        protected int PageCount;
        protected int num;
        BLL.CarItems Citem = null;
        public override void SonLoad()
        {
            if (!string.IsNullOrEmpty(Context.Request["pageIndex"]))
            {
                pageIndex = Convert.ToInt32(Context.Request["pageIndex"]);
            }
            Citem = new BLL.CarItems();
            IList<MODEL.CarItems> list = Citem.PageItems(pageIndex, 10, out PageCount, out num);
            this.rptList.DataSource = list;
            this.rptList.DataBind();
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
                page.Append("<a href='CarItems.aspx?pageIndex=" + 1 + "'>&nbsp;&lt;&lt;&nbsp;</a>");
                page.Append("<a href='CarItems.aspx?pageIndex=" + (pageIndex - 1).ToString() + "'>&nbsp;&lt;&nbsp;</a>");
            }
            for (int i = 1; i <= PageCount; i++)
            {
                if (pageIndex == i)
                {
                    page.Append("<span class='current'>" + i + "</span>");
                }
                else
                {
                    page.Append("<a href='CarItems.aspx?pageIndex=" + i + "'>" + i + "</a>");
                }

            }
            if (pageIndex == PageCount)
            {
                page.Append("<span class='disabled'>&nbsp;&gt;&nbsp;</span>");
                page.Append("<span class='disabled'>&nbsp;&gt;&gt;&nbsp;</span>");
            }
            else
            {
                page.Append("<a href='CarItems.aspx?pageIndex=" + (pageIndex + 1).ToString() + "'>&nbsp;&gt;&nbsp;</a>");
                page.Append("<a href='CarItems.aspx?pageIndex=" + PageCount + "'>&nbsp;&gt;&gt;&nbsp;</a>");
            }
        }
    }
}
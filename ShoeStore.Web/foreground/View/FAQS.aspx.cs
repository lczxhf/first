using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoeStore.Web.foreground.View
{
    public partial class FAQS : BasePage
    {
        BLL.FAQS bllquestion = new BLL.FAQS();
        protected System.Text.StringBuilder page = new System.Text.StringBuilder(400);
        protected int PageCount;
        protected int num;
        protected int PageIndex=1;
        public override void SonLoad()
        {
            if (!string.IsNullOrEmpty(Context.Request["PageIndex"]))
            {
                PageIndex =Convert.ToInt32( Context.Request["PageIndex"]);
            }
            IList<MODEL.FAQS> question = bllquestion.GetPageFAQS(PageIndex, 5, out PageCount, out num);
            this.rptQuestion.DataSource = question;
            this.rptQuestion.DataBind();
            PageFAQS();
        }
        protected void PageFAQS()
        {
            if (PageIndex == 1)
            {
                page.Append("<span class='disabled'>&nbsp;&lt;&lt;&nbsp;</span>");
                page.Append("<span class='disabled'>&nbsp;&lt;&nbsp;</span>");
            }
            else
            {
                page.Append("<a href='FAQS.aspx?PageIndex=" + 1 + "'>&nbsp;&lt;&lt;&nbsp;</a>");
                page.Append("<a href='FAQS.aspx?PageIndex=" + (PageIndex - 1).ToString() + "'>&nbsp;&lt;&nbsp;</a>");
            }
            for (int i = 1; i <= PageCount; i++)
            {
                if (PageIndex == i)
                {
                    page.Append("<span class='current'>" + i + "</span>");
                }
                else
                {
                    page.Append("<a href='FAQS.aspx?PageIndex=" + i + "'>" + i + "</a>");
                }

            }
            if (PageIndex == PageCount)
            {
                page.Append("<span class='disabled'>&nbsp;&gt;&nbsp;</span>");
                page.Append("<span class='disabled'>&nbsp;&gt;&gt;&nbsp;</span>");
            }
            else
            {
                page.Append("<a href='FAQS.aspx?PageIndex=" + (PageIndex + 1).ToString() + "'>&nbsp;&gt;&nbsp;</a>");
                page.Append("<a href='FAQS.aspx?PageIndex=" + PageCount + "'>&nbsp;&gt;&gt;&nbsp;</a>");
            }
        }
    }
}
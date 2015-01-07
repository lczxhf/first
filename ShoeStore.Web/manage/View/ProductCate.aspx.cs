using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoeStore.Web.manage.View
{
    public partial class ProductCate : BasePage
    {
        protected System.Text.StringBuilder sbHtml = new System.Text.StringBuilder();
        BLL.ProductCate bllcate = new BLL.ProductCate();
        public override void SonLoad()
        {
            IList<MODEL.ProductCate> parent = bllcate.GetListByParent();
            this.rptlist.DataSource = parent;
            this.rptlist.DataBind();
          IList<MODEL.ProductCate> productCate=  bllcate.GetList();
          MakeMenuHtml(productCate, 0);
        }

        void MakeMenuHtml(IList<MODEL.ProductCate> list, int pId)
        {
            //循环生成 父菜单
            foreach (MODEL.ProductCate cate in list)
            {
                if (cate.PId == 0 && pId == 0)
                {
                   
                    sbHtml.AppendLine("<table>");
                    sbHtml.AppendLine("<tr class='claTitle'><th>" + cate.PName + "</th><th>排序号</th><th>操作</th>");
                    
                    sbHtml.AppendLine("</tr>");
                    MakeMenuHtml(list, cate.Id);
                    sbHtml.AppendLine("</table>");
                    continue;
                }
                if (cate.PId == pId)
                {

                    sbHtml.AppendLine("<tr class='tr'><td >" + cate.PName + "</td><td>" + cate.PcSort + "</td><td><a href='javascript:void(0)' onclick='doDel(" + cate.Id + ",this)' class='del'>删</a><a href='javascript:void(0)' onclick='showEditPanel("+cate.Id+",this)' class='edit'>改</a></td></tr>");
                   
                }
            }
        } 
    

    }
}
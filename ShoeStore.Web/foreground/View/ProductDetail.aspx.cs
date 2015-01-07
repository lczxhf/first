using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoeStore.Web.foreground.View
{
    public partial class ProductDetail : BasePage
    {
        BLL.Product bllproduct=new BLL.Product();
        protected MODEL.Product product;
        IList<MODEL.Product> related;
        public override void SonLoad()
        {
            product = bllproduct.GetModel(Convert.ToInt32( Context.Request["Id"]));
            related = bllproduct.GetRelated(product.PId, product.PCateid);
            this.rptRelated.DataSource = related;
            this.rptRelated.DataBind();
        }
    }
}
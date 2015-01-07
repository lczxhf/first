using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoeStore.Web.foreground.View
{
    public partial class Home : BasePage
    {
        BLL.Product bllProduct = null;

        public override void SonLoad()
        {
            bllProduct = new BLL.Product();
            IList<MODEL.Product> proList = bllProduct.GetListByIsrec();
            this.rptRec.DataSource = proList;
            this.rptRec.DataBind();
           

        }
    }
}
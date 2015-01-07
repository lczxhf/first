using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoeStore.Web.manage.View
{
    public partial class SuperUser : BasePage
    {
        BLL.SuperUsers bllsuper = null;
        public override void SonLoad()
        {
            bllsuper = new BLL.SuperUsers();
            IList<MODEL.SuperUsers> SuperUser = bllsuper.GetList();
            this.rptList.DataSource = SuperUser;
            this.rptList.DataBind();
        }
    }
}
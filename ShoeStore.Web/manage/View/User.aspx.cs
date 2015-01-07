using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoeStore.Web.manage.View
{
    public partial class User : BasePage
    {
        BLL.Users blluser = null;
      
        public override void SonLoad()
        {
            blluser = new BLL.Users();
            IList<MODEL.Users> user = blluser.GetList();
            this.rptList.DataSource = user;
            this.rptList.DataBind();

        }

    }
}
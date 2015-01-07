using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoeStore.Web.manage.View
{
    public partial class Setting : BasePage
    {
        BLL.T_Settings bllset = null;
        public override void SonLoad()
        {
            bllset = new BLL.T_Settings();
           IList< MODEL.T_Settings> setting = bllset.GetList();
           this.rptlist.DataSource = setting;
           this.rptlist.DataBind();
        }
        
    }
}
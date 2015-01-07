using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoeStore.Web.manage.View
{
    public partial class Info : BasePage
    {
        BLL.UserInfo bllinfo = null;
        protected MODEL.UserInfo userInfo = null;
        protected string userName;
        protected int userId;
        public override void SonLoad()
        {
            if (!string.IsNullOrEmpty(Context.Request["null"]))
            {
                userName = Context.Request["ULoginName"];
                userInfo = new MODEL.UserInfo()
                {
                    IAddress = "null",
                    IAge = 1,
                    IEmail = "xxx@xx.com",
                    IMoney = 0,
                    IName = "xxx",
                    IPostCode = 000000,
                    Phone = "xxxxxxxxxxx"
                };

            }
            else
            {
                userName = Context.Request["userName"];
                userId = Convert.ToInt32(Context.Request["userId"]);
                bllinfo = new BLL.UserInfo();
                userInfo = bllinfo.GetInfoByUserId(userId);
            }
        }
    }
}
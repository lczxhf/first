using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoeStore.Web.foreground.View
{
    public partial class Info : BasePage,IRequiresSessionState
    {
        BLL.UserInfo blluinfo = null;
       protected MODEL.UserInfo userInfo = null;
       protected MODEL.Users user = null;
       public override void SonLoad()
       {
           if (Context.Session["info"] != null)
           {
               userInfo = Context.Session["info"] as MODEL.UserInfo;
           }
           else
           {
               blluinfo = new BLL.UserInfo();
               userInfo = new MODEL.UserInfo();
               user = new MODEL.Users();
               user = Context.Session["user"] as MODEL.Users;
               userInfo = blluinfo.GetInfoByUserId(user.UId);
           }
          
           
       }
    }
}
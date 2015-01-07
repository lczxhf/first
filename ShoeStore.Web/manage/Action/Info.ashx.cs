using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeStore.Web.manage.Action
{
    /// <summary>
    /// Info 的摘要说明
    /// </summary>
    public class Info : BaseHandler
    {
        BLL.UserInfo bllinfo = null;
        MODEL.UserInfo userInfo = null;
        public override void SonLoad()
        {
            userInfo = new MODEL.UserInfo();
            userInfo.IAddress = context.Request["Address"];
            userInfo.IAge = Convert.ToInt32(context.Request["Age"]);
            userInfo.IEmail = context.Request["Email"];
            userInfo.IName = context.Request["RealName"];
            userInfo.IPostCode = Convert.ToInt32(context.Request["PostCode"]);
            userInfo.Phone = context.Request["Phone"];
            userInfo.IId = Convert.ToInt32(context.Request["iId"]);
            userInfo.UserId = Convert.ToInt32(context.Request["iuserId"]);
            if (bllinfo.Update(userInfo) == true)
            {

                Common.AjaxMsgHelper.AjaxMsg("ok", "保存成功");
                context.Session["info"] = userInfo;
            }
            else
            {
                Common.AjaxMsgHelper.AjaxMsg("err", "保存失败");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace ShoeStore.Web.foreground.Action
{
    /// <summary>
    /// Registered 的摘要说明
    /// </summary>
    public class Registered : IHttpHandler, IRequiresSessionState
    {
        BLL.UserInfo bllUI = null;
        BLL.Users blluser = null;
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request["PassWord"] != context.Request["AgainPW"])
            {
                //两次输入的密码不一致
                Common.AjaxMsgHelper.AjaxMsg("err", "输入的两次密码不一致,请重新输入");
                context.Response.End();
            }
            else if (context.Request["ValidCode"] != context.Session["code"].ToString ())
            {
                Common.AjaxMsgHelper.AjaxMsg("Codeerr", "验证码错误");
                context.Response.End();
            }
            else
            {
                blluser = new BLL.Users();

                MODEL.Users user = new MODEL.Users();
                user.ULoginName = context.Request["Name"];
                user.UPwd = Common.DataHelper.MD5(context.Request["PassWord"]);
                int userId = blluser.Add(user);
                if (userId > 0)
                {
                    user.UId = userId;
                    context.Session["user"] = user;
                    bllUI = new BLL.UserInfo();
                    MODEL.UserInfo userinfo = new MODEL.UserInfo();
                    userinfo.IAddress = context.Request["Address"];
                    userinfo.IAge = Convert.ToInt32(context.Request["age"]);
                    userinfo.IEmail = (context.Request["Email"]);

                    userinfo.IName = context.Request["RealName"];
                    userinfo.IPostCode = Convert.ToInt32(context.Request["PostCode"]);
                    userinfo.Phone = context.Request["Phone"];
                    userinfo.UserId = user.UId;

                    if (bllUI.Add(userinfo) > 0)
                    {
                        Common.AjaxMsgHelper.AjaxMsg("ok", "注册成功");
                    }


                }

            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
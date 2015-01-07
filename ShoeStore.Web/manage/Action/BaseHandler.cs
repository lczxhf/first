using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace ShoeStore.Web.manage.Action
{
    public abstract class BaseHandler : IHttpHandler, IRequiresSessionState
    {
        protected HttpContext context = null;
       
        BLL.SuperUsers bllsuser = null;
     public void ProcessRequest(HttpContext context)
     {
         this.context = context;
         if (context.Session["user"] == null)
         {
             if (context.Request.Cookies["user"] == null || string.IsNullOrEmpty(context.Request.Cookies["user"].Value))
             {
                 Common.AjaxMsgHelper.AjaxMsg("none","您尚未登录!请登录后再进入后台管理",null,"/foreground/View/Home.aspx");
                 context.Response.End();
             }
             else
             {
                 if (context.Request.Cookies["isSuper"] == null || string.IsNullOrEmpty(context.Request.Cookies["isSuper"].Value))
                 {
                     //普通用户 
                     Common.AjaxMsgHelper.AjaxMsg("nolevel", "您是普通用户!没有权限访问后台管理", null, "/foreground/View/Home.aspx");
                     context.Response.End();
                    
                 }
                 else
                 {
                     //管理员 存在cookie
                     bllsuser = new BLL.SuperUsers();
                     MODEL.SuperUsers Suser = bllsuser.GetModel(Convert.ToInt32(context.Request.Cookies["user"].Value));
                     context.Session["user"] = Suser;
                    context. Session["isSuper"] = "Super";

                 }
             }
         }
         //2.调用子类重写的方法 执行 子类的 业务
         SonLoad();

     }

     public abstract void SonLoad();

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
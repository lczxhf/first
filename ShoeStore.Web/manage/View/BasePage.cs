using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeStore.Web.manage.View
{
    public abstract class BasePage : System.Web.UI.Page
    {
        
        BLL.SuperUsers bllsuser = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {

                if (Request.Cookies["user"] == null || string.IsNullOrEmpty(Request.Cookies["user"].Value))
                {
                    Common.PageHelper.WriteJsMsg("您尚未登录!请登录后再访问后台管理", "/foreground/View/Home.aspx");
                    Response.End();
                }
                else
                {
                    if (Request.Cookies["isSuper"] == null || string.IsNullOrEmpty(Request.Cookies["isSuper"].Value))
                    {
                        //普通用户 
                        Common.PageHelper.WriteJsMsg("您是普通用户!没有权限进入后台管理", "/foreground/View/Home.aspx");
                        Response.End();
                      
                    }
                    else
                    {
                        //管理员 存在cookie
                        bllsuser = new BLL.SuperUsers();
                        MODEL.SuperUsers Suser = bllsuser.GetModel(Convert.ToInt32(Request.Cookies["user"].Value));
                        Session["user"] = Suser;
                        Session["isSuper"] = "Super";

                    }
                }
            }

            SonLoad();
        }

        public abstract void SonLoad();
    }
}
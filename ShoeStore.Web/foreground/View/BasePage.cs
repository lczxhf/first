using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace  ShoeStore.Web.foreground.View
{
    public abstract class BasePage:System.Web.UI.Page
    {
        BLL.SuperUsers bllsuser = new BLL.SuperUsers();
        BLL.Users blluser = new BLL.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {

                if (Request.Cookies["user"] == null || string.IsNullOrEmpty(Request.Cookies["user"].Value))
                {
                    
                }
                else
                {
                    if (Request.Cookies["isSuper"] == null || string.IsNullOrEmpty(Request.Cookies["isSuper"].Value))
                    {
                        //普通用户 存在cookie
                        blluser = new BLL.Users();
                        MODEL.Users user = blluser.GetModel(Convert.ToInt32(Request.Cookies["user"].Value));
                        Session["user"] = user;


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



            //2.调用子类的 重写方法
            SonLoad();
        }
        public abstract void SonLoad();
    }
}
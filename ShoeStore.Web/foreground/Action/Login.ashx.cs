using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace ShoeStore.Web.foreground.Action
{
    /// <summary>
    /// Login 的摘要说明
    /// </summary>
    public class Login : IHttpHandler, IRequiresSessionState
    {
        BLL.Users blluser = new BLL.Users();
        BLL.SuperUsers bllsuser = new BLL.SuperUsers();
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request["type"] == "login")
            {
                if (context.Request["ValidCode"] != context.Session["code"].ToString())
                {
                    Common.AjaxMsgHelper.AjaxMsg("Codeerr", "验证码错误");
                    context.Response.End();
                }
                string name = context.Request["MName"];
                string passWord = Common.DataHelper.MD5(context.Request["MpassWord"]);
                string isSuper = context.Request["users"];
                bool isExist = false;
                ShoeStore.MODEL.Users user = new MODEL.Users();

                if (isSuper == "user")
                {
                    #region MyRegion
                    //用户是普通用户
                    user = blluser.GetUser(name, passWord, out isExist);
                    if (user == null)
                    {
                        //数据库找不到匹配数据
                        if (isExist == true)
                        {
                            //账号存在 但是密码错误
                            Common.AjaxMsgHelper.AjaxMsg("err", "密码错误");
                        }
                        else
                        {
                            //账号不存在
                            Common.AjaxMsgHelper.AjaxMsg("err", "账号不存在");
                        }
                    }
                    else
                    {
                        //用户名和密码正确
                        context.Session["user"] = user;
                        if (context.Request["checkAlawy"] == "on")
                        {
                            //用户勾上了自动登录
                            HttpCookie cookie = new HttpCookie("user", user.UId.ToString());
                            cookie.Expires = DateTime.Now.AddDays(1);
                            context.Response.Cookies.Add(cookie);
                        }
                        else
                        {
                            HttpCookie cookie = new HttpCookie("user", user.UId.ToString());
                            cookie.Expires = DateTime.Now.AddHours(1);
                            context.Response.Cookies.Add(cookie);
                        }
                        Common.AjaxMsgHelper.AjaxMsg("ok", "登陆成功", null, "../View/Home.aspx");
                    } 
                    #endregion
                }
                else
                {
                    #region MyRegion
                    //用户是管理员
                    MODEL.SuperUsers suser = bllsuser.GetUser(name, passWord, out isExist);
                    if (suser == null)
                    {
                        //数据库找不到匹配数据
                        if (isExist == true)
                        {
                            //账号存在 但是密码错误
                            Common.AjaxMsgHelper.AjaxMsg("err", "密码错误");
                        }
                        else
                        {
                            //账号不存在
                            Common.AjaxMsgHelper.AjaxMsg("err", "账号不存在");
                        }
                    }
                    else
                    {
                        //用户名和密码正确
                        context.Session["user"] = suser;
                        context.Session["isSuper"] = "Super";
                        if (context.Request["checkAlawy"] == "on")
                        {
                            //用户勾上了自动登录
                            HttpCookie cookie = new HttpCookie("user", suser.Id.ToString());
                            cookie.Expires = DateTime.Now.AddHours(12);
                            context.Response.Cookies.Add(cookie);
                            HttpCookie cookie1 = new HttpCookie("isSuper", "Super");
                            cookie1.Expires = DateTime.Now.AddDays(1);
                            context.Response.Cookies.Add(cookie1);
                        }
                        else
                        {
                            HttpCookie cookie = new HttpCookie("user", suser.Id.ToString());
                            cookie.Expires = DateTime.Now.AddHours(1);
                            context.Response.Cookies.Add(cookie);
                            HttpCookie cookie1 = new HttpCookie("isSuper", "Super");
                            cookie1.Expires = DateTime.Now.AddHours(1);
                            context.Response.Cookies.Add(cookie1);
                        }
                        Common.AjaxMsgHelper.AjaxMsg("ok", "登陆成功", null, "../View/Home.aspx"); 
                    #endregion
                    }
                }
            }
            else if (context.Request["type"] == "back")
            {
               
                context.Session["user"] = null;
                context.Session["isSuper"] = null;
                
                context.Response.Cookies["user"].Value = "";
                context.Response.Cookies["isSuper"].Value = "";
                Common.AjaxMsgHelper.AjaxMsg("ok", "退出成功", null, "../View/Home.aspx");

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
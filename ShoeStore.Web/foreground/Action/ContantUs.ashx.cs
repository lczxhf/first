using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace ShoeStore.Web.foreground.Action
{
    /// <summary>
    /// ContantUs 的摘要说明
    /// </summary>
    public class ContantUs : IHttpHandler, IRequiresSessionState
    {
        BLL.ContantUs bllCU = new BLL.ContantUs();
        
        public void ProcessRequest(HttpContext context)
        {
            
                MODEL.ContantUs contant = new MODEL.ContantUs();
                try
                {
                    MODEL.Users user = context.Session["user"] as MODEL.Users;
                    contant.UserId = user.UId;
                }
                catch
                {
                    contant.UserId = 0;
                }


                contant.Name = context.Request["author"];
                contant.Email = context.Request["email"];
                contant.Phone = Convert.ToInt32(context.Request["phone"]);
                contant.Message = context.Request["text"];
                contant.MTime = DateTime.Now;
                bllCU.Add(contant);

                ShoeStore.Common.AjaxMsgHelper.AjaxMsg("ok", "留言成功");

           
            
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
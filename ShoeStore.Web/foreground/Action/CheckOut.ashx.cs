using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace ShoeStore.Web.foreground.Action
{
    /// <summary>
    /// CheckOut 的摘要说明
    /// </summary>
    public class CheckOut : IHttpHandler, IRequiresSessionState
    {
        
        BLL.Product bllproduct = new BLL.Product();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            MODEL.Users user = context.Session["user"] as MODEL.Users;
            string itemid=context.Request["itemsid"];
            string num = context.Request["num"];
            string product = context.Request["product"];
            int almost =Convert.ToInt32( context.Request["almost"]);
            string[] itemidArr = itemid.Split(',');
            string[] numArr=num.Split(',');
            string[] productArr = product.Split(',');
            
            
                for (int i = 1; i < itemidArr.Length; i++)
                {
                    int count=bllproduct.GetProductCount(productArr[i]);
                    if (count < Convert.ToInt32(numArr[i]))
                    {
                        Common.AjaxMsgHelper.AjaxMsg("err", productArr[i] + "商品数量不足!只剩下:" + count + "双");
                        context.Response.End();
                    }
                    int money=bllproduct.UserMoney(user.UId);
                    if (money < almost)
                    {
                        Common.AjaxMsgHelper.AjaxMsg("err", "您的余额为" + money + "元!不足够支付");
                        context.Response.End();
                    }
                    bllproduct.UpdateCount(Convert.ToInt32(numArr[i]), user.UId, productArr[i], Convert.ToInt32(itemidArr[i]), almost);

                }
                Common.AjaxMsgHelper.AjaxMsg("ok", "支付成功");
            
           

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
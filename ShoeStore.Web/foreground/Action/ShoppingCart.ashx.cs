using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace ShoeStore.Web.foreground.Action
{
    /// <summary>
    /// ShoppingCart 的摘要说明
    /// </summary>
    public class ShoppingCart : IHttpHandler, IRequiresSessionState
    {
        BLL.Car bllCar = new BLL.Car();
        BLL.CarItems bllCI = new BLL.CarItems();
        public void ProcessRequest(HttpContext context)
        {

            MODEL.Users user = context.Session["user"] as MODEL.Users;
            if (user != null)
            {
                string type = context.Request["type"];
                if (type == "clearAll")
                {
                    if (bllCar.ClearCar(user.UId) == true)
                    {
                        Common.AjaxMsgHelper.AjaxMsg("ok", "清空购物车成功");
                    }
                    else
                    {
                        Common.AjaxMsgHelper.AjaxMsg("err", "清空购物车失败");
                    }
                }
                else if (type == "clearOne")
                {
                    if (bllCar.ClearOne(user.UId, Convert.ToInt32(context.Request["itemId"])) == true)
                    {

                        Common.AjaxMsgHelper.AjaxMsg("ok", "删除数据成功");
                    }
                    else
                    {
                        Common.AjaxMsgHelper.AjaxMsg("err", "删除失败");
                    }
                }
                else if (type == "add")
                {
                    int count = 1;
                    if (!string.IsNullOrEmpty(context.Request["count"]))
                    {
                        if (Common.ValidateHelper.IsNum(context.Request["count"]))
                        {
                            count = Convert.ToInt32(context.Request["count"]);
                        }
                        else
                        {
                            Common.AjaxMsgHelper.AjaxMsg("err", "请输入数字");
                            context.Response.End();
                            
                        }
                    }
                    MODEL.Car car = bllCar.GetUserCar(user.UId);
                    MODEL.CarItems cItems = new MODEL.CarItems();
                    cItems.CTime = DateTime.Now;
                    cItems.CCarId = car.CarId;
                    cItems.CPId = Convert.ToInt32(context.Request["Id"]);
                    cItems.CCount = count;
                    if (bllCI.IsExistProduct(cItems) == true)
                    {
                        Common.AjaxMsgHelper.AjaxMsg("ok", "添加成功");

                    }
                    else
                    {
                        Common.AjaxMsgHelper.AjaxMsg("err", "添加失败");
                    }


                }
                else if (type == "update")
                {
                    if (Convert.ToInt32( context.Request["count"]) == 0)
                    {
                        Common.AjaxMsgHelper.AjaxMsg("err","数量不能为0");
                        context.Response.End();
                    }
                    if (bllCI.UpdateItemById(Convert.ToInt32(context.Request["itemId"]), Convert.ToInt32(context.Request["count"]), DateTime.Now) > 0)
                    {
                        Common.AjaxMsgHelper.AjaxMsg("ok", "更新数量成功");
                    }
                }

            }
            else
            {
                Common.AjaxMsgHelper.AjaxMsg("err", "登陆才能加入购物车");
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
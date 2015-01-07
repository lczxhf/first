using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeStore.Web.manage.Action
{
    /// <summary>
    /// TransactionLog 的摘要说明
    /// </summary>
    public class TransactionLog : BaseHandler
    {
        BLL.TransactionLog Tlog = new BLL.TransactionLog();
        string pName;
        string userName;
        int userId;
        int productId;
        public override void SonLoad()
        {
              string strType = context.Request["type"];
            switch (strType)
            {
                case "get":
                    GetMenu();
                    break;
                case "a":
                    DoAdd();
                    break;
                case "m":
                    DoModify();
                    break;
                case "del":
                    DoDel();
                    break;
            }
        }
        #region 1.0 新增菜单 -void DoAdd()
        /// <summary>
        /// 新增菜单
        /// </summary>
        private void DoAdd()
        {
            check();
            
            MODEL.TransactionLog menuModel = new MODEL.TransactionLog()
            { 
                PCount=Convert.ToInt32(context.Request["Count"]),
                 PId=productId,
                  PTime=DateTime.Parse(context.Request["Time"]),
                   UserId=userId
              
            };
            try
            {
                //2.调用业务层对象 提交
                Tlog.Add(menuModel);
                Common.AjaxMsgHelper.AjaxMsg("ok", "增加数据成功！");
            }
            catch (Exception ex)
            {
                Common.AjaxMsgHelper.AjaxMsg("err", "异常：" + ex.Message);
            }
        }
        #endregion

        #region 2.0 根据id获取 菜单 -void GetMenu()
        /// <summary>
        /// 2.0 根据id获取 菜单
        /// </summary>
        private void GetMenu()
        {
            string strId = context.Request["mid"];
            if (!Common.ValidateHelper.IsNum(strId))
            {
                Common.AjaxMsgHelper.AjaxMsg("err", "参数异常~~");
                context.Response.End();
            }

            try
            {
                MODEL.TransactionLog modelTlog = Tlog.GetModel(int.Parse(strId));
                //在服务端 将查询到的对象 转成 json字符串
                string strJson = Common.DataHelper.Obj2Json(modelTlog);
                //组成 规定格式 json数据 返回浏览器
                Common.AjaxMsgHelper.AjaxMsg("ok", "加载成功~", strJson);
            }
            catch (Exception ex)
            {
                Common.AjaxMsgHelper.AjaxMsg("err", "异常：" + ex.Message);
            }
        }
        #endregion

        private void DoModify()
        {
            check();
            //更新到数据库
            MODEL.Product p = new MODEL.Product() { PName = pName };
            MODEL.Users u = new MODEL.Users() { ULoginName = userName };
            MODEL.TransactionLog modelTlog= new MODEL.TransactionLog()
            {
                PCount=Convert.ToInt32(context.Request["Count"]),
                PId=productId,
                 PTime=DateTime.Parse(context.Request["Time"]),
                  UserId=userId,
                   Id=Convert.ToInt32(context.Request["mid"]),
                    PIdMODEL=p,
                     UserMODEL=u
            };
            try
            {
                Tlog.Update(modelTlog);
                string strJson = Common.DataHelper.Obj2Json(modelTlog);
                Common.AjaxMsgHelper.AjaxMsg("ok", "修改成功~", strJson);
            }
            catch (Exception ex)
            {
                Common.AjaxMsgHelper.AjaxMsg("err", "异常：" + ex.Message);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        private void DoDel()
        {
            if (Tlog.Del(context.Request["mid"]) == true)
            {
                Common.AjaxMsgHelper.AjaxMsg("ok", "删除成功~");
            }
        }

        private void check()
        {
            userName = context.Request["UserName"];
            BLL.Users user = new BLL.Users();
             userId = user.GetUserId(userName);
            if (userId < 1)
            {
                Common.AjaxMsgHelper.AjaxMsg("err", "请输入正确的用户名");
                context.Response.End();
            }
            pName = context.Request["pName"];
            BLL.Product product = new BLL.Product();
             productId = product.GetProductId(pName);
            if (productId < 1)
            {
                Common.AjaxMsgHelper.AjaxMsg("err", "请输入正确的商品名");
                context.Response.End();
            }
            if (!Common.ValidateHelper.IsNum(context.Request["Count"]))
            {
                Common.AjaxMsgHelper.AjaxMsg("err", "数量必须是数字");
                context.Response.End();
            }
            try { DateTime.Parse(context.Request["Time"]); }
            catch
            {
                Common.AjaxMsgHelper.AjaxMsg("err", "请输入正确的时间格式");
                context.Response.End();
            }
        }
       
    }
}
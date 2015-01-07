using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ShoeStore.Web.manage.Action
{
    /// <summary>
    /// Menu 的摘要说明
    /// </summary>
    public class Menu : BaseHandler
    {
        BLL.Menu bllmenu = new BLL.Menu();
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
            //1.接收并验证数据
            if (!Common.ValidateHelper.IsNum(context.Request["MSort"]))
            {
                Common.AjaxMsgHelper.AjaxMsg("err", "序号必须是数字~");
                context.Response.End();
            }
            MODEL.Menu menuModel = new MODEL.Menu()
            {
                MName = context.Request["MName"],
                MUrl = context.Request["MUrl"],
                MSort = int.Parse(context.Request["MSort"]),
                MAddtime=DateTime.Now
            };
            try
            {
                //2.调用业务层对象 提交
                bllmenu.Add(menuModel);
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
                MODEL.Menu modelMenu = bllmenu.GetModel(int.Parse(strId));
                //在服务端 将查询到的对象 转成 json字符串
                string strJson = Common.DataHelper.Obj2Json(modelMenu);
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
            if (!Common.ValidateHelper.IsNum(context.Request["MSort"]))
            {
                Common.AjaxMsgHelper.AjaxMsg("err", "序号必须是数字~");
                context.Response.End();
            }
            //更新到数据库
            MODEL.Menu modelMenu = new MODEL.Menu()
            {
                MId = int.Parse(context.Request["MId"]),
                MName = context.Request["MName"],
                MUrl = context.Request["MUrl"],
                MSort = int.Parse(context.Request["MSort"]),
                MAddtime=DateTime.Now
            };
            try
            {
                bllmenu.Update(modelMenu);
                string strJson = Common.DataHelper.Obj2Json(modelMenu);
                Common.AjaxMsgHelper.AjaxMsg("ok", "修改成功~", strJson);
            }
            catch (Exception ex)
            {
                Common.AjaxMsgHelper.AjaxMsg("err", "异常：" + ex.Message);
            }
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        private void DoDel()
        {
            if (bllmenu.Del(context.Request["mid"]) == true)
            {
                Common.AjaxMsgHelper.AjaxMsg("ok", "删除成功~");
            }
        }
    }
}
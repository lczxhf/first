using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeStore.Web.manage.Action
{
    /// <summary>
    /// SuperUser 的摘要说明
    /// </summary>
    public class SuperUser : BaseHandler
    {
          BLL.SuperUsers bllsuser = new BLL.SuperUsers ();     
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
            if (string.IsNullOrEmpty(context.Request["Name"]) || string.IsNullOrEmpty(context.Request["PassWord"]))
            {
                Common.AjaxMsgHelper.AjaxMsg("err", "用户名/密码不能为空");
                context.Response.End();
            }
          
            MODEL.SuperUsers suser = new MODEL.SuperUsers()
            {
                 Name = context.Request["Name"],
                  PassWord=Common.DataHelper.MD5(context.Request["PassWord"])
            };
            try
            {
                //2.调用业务层对象 提交
                bllsuser.Add(suser);
                string data=Common.DataHelper.Obj2Json(suser);
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
                MODEL.SuperUsers suser = bllsuser.GetModel(Convert.ToInt32(strId));
                //在服务端 将查询到的对象 转成 json字符串
                string strJson = Common.DataHelper.Obj2Json(suser);
                //组成 规定格式 json数据 返回浏览器
                Common.AjaxMsgHelper.AjaxMsg("ok", "加载成功~", strJson);
            }
            catch (Exception ex)
            {
                Common.AjaxMsgHelper.AjaxMsg("err", "异常：" + ex.Message);
            }
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        private void DoModify()
        {
            if (string.IsNullOrEmpty(context.Request["Name"]) || string.IsNullOrEmpty(context.Request["PassWord"]))
            {
                Common.AjaxMsgHelper.AjaxMsg("err", "用户名/密码不能为空");
                context.Response.End();
            }
          
            MODEL.SuperUsers suser = new MODEL.SuperUsers()
            {
                 Name = context.Request["Name"],
                  PassWord=Common.DataHelper.MD5(context.Request["PassWord"])
                
            };

            try
            {
                bllsuser.Update(suser);
                string strJson = Common.DataHelper.Obj2Json(suser);
                Common.AjaxMsgHelper.AjaxMsg("ok", "修改成功~", strJson);
            }
            catch (Exception ex)
            {
                Common.AjaxMsgHelper.AjaxMsg("err", "异常：" + ex.Message);
            }
        } 
        #endregion


        #region 删除菜单
        /// <summary>
        /// 删除菜单
        /// </summary>
        private void DoDel()
        {
            if (bllsuser.Del(context.Request["mid"]) == true)
            {
                Common.AjaxMsgHelper.AjaxMsg("ok", "删除成功");
            }
        } 
        #endregion
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeStore.Web.manage.Action
{
    /// <summary>
    /// FAQS 的摘要说明
    /// </summary>
    public class FAQS : BaseHandler
    {
        BLL.FAQS bllquestion = new BLL.FAQS();
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
            if (string.IsNullOrEmpty(context.Request["ULoginName"]) || string.IsNullOrEmpty(context.Request["PassWord"]))
            {
                Common.AjaxMsgHelper.AjaxMsg("err", "用户名/密码不能为空");
                context.Response.End();
            }
            if (Common.ValidateHelper.IsNum(context.Request["Sort"]))
            {
                Common.AjaxMsgHelper.AjaxMsg("err", "排序必须为数字");
                context.Response.End();
            }
            MODEL.FAQS qustion = new MODEL.FAQS()
            {
                PId=Convert.ToInt32(context.Request["mid"]),
                Answer = context.Request["Answer"],
                FSort = Convert.ToInt32(context.Request["Sort"]),
                Question = context.Request["Question"] 
            };
            try
            {
                //2.调用业务层对象 提交
                bllquestion.Add(qustion);
                string data = Common.DataHelper.Obj2Json(qustion);
                Common.AjaxMsgHelper.AjaxMsg("ok", "增加问题成功！");
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
                MODEL.FAQS question = bllquestion.GetModel(Convert.ToInt32(strId));
                //在服务端 将查询到的对象 转成 json字符串
                string strJson = Common.DataHelper.Obj2Json(question);
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
            if (string.IsNullOrEmpty(context.Request["Question"]) || string.IsNullOrEmpty(context.Request["Answer"]))
            {
                Common.AjaxMsgHelper.AjaxMsg("err", "问题/回答不能为空");
                context.Response.End();
            }
            if (Common.ValidateHelper.IsNum(context.Request["Sort"]))
            {
                Common.AjaxMsgHelper.AjaxMsg("err", "排序必须为数字");
                context.Response.End();
            }

            MODEL.FAQS question = new MODEL.FAQS()
            {
                Answer=context.Request["Answer"],
                 FSort=Convert.ToInt32(context.Request["Sort"]),
                  Question=context.Request["Question"] 

            };

            try
            {
                bllquestion.Update(question);
                string strJson = Common.DataHelper.Obj2Json(question);
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
            if (bllquestion.Del(context.Request["mid"]) == true)
            {
                Common.AjaxMsgHelper.AjaxMsg("ok", "删除成功~");
            }
        }
        #endregion

    }
}
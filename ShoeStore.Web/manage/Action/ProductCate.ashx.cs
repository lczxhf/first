using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeStore.Web.manage.Action
{
    /// <summary>
    /// ProductCate 的摘要说明
    /// </summary>
    public class ProductCate : BaseHandler
    {
        BLL.ProductCate bllcate = new BLL.ProductCate();
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
            if (string.IsNullOrEmpty(context.Request["Cate"]))
            {
                Common.AjaxMsgHelper.AjaxMsg("err", "类别不能为空");
                context.Response.End();
            }
            if (!Common.ValidateHelper.IsNum(context.Request["Sort"]))
            {
                Common.AjaxMsgHelper.AjaxMsg("err", "序列号只能为数字");
                context.Response.End();
            }
            MODEL.ProductCate cate = new MODEL.ProductCate()
            {
                
                 PId=Convert.ToInt32(context.Request["Parent"]),
                  PName=context.Request["Cate"],
                   PcSort=Convert.ToInt32( context.Request["Sort"])
            };
            try
            {
                //2.调用业务层对象 提交
                bllcate.Add(cate);
                string data = Common.DataHelper.Obj2Json(cate);
                Common.AjaxMsgHelper.AjaxMsg("ok", "增加类别成功！", data);
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
                MODEL.ProductCate user = bllcate.GetModel(Convert.ToInt32(strId));
                //在服务端 将查询到的对象 转成 json字符串
                string strJson = Common.DataHelper.Obj2Json(user);
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
            if (string.IsNullOrEmpty(context.Request["Cate"]))
            {
                Common.AjaxMsgHelper.AjaxMsg("err", "类别不能为空");
                context.Response.End();
            }
            if (!Common.ValidateHelper.IsNum(context.Request["Sort"]))
            {
                Common.AjaxMsgHelper.AjaxMsg("err", "序列号只能为数字");
                context.Response.End();
            }

            MODEL.ProductCate cate = new MODEL.ProductCate()
            {
                 Id=Convert.ToInt32(context.Request["mid"]),
                  PcSort=Convert.ToInt32(context.Request["Sort"]),
                   PName=context.Request["Cate"],
                    PId=Convert.ToInt32( context.Request["Parent"])

            };

            try
            {
                bllcate. Update(cate);
                string strJson = Common.DataHelper.Obj2Json(cate);
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
            if ( bllcate.Del(context.Request["mid"]) == true)
            {
                Common.AjaxMsgHelper.AjaxMsg("ok", "删除成功~");
            }
        }
        #endregion
    }
}
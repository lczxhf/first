using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeStore.Web.manage.Action
{
    /// <summary>
    /// Setting 的摘要说明
    /// </summary>
    public class Setting : BaseHandler
    {
        BLL.T_Settings bllset = new BLL.T_Settings();
        public override void SonLoad()
        {
            string strType = context.Request["type"];
            switch (strType)
            {

                case "a":
                    DoAdd();
                    break;
                case "save":
                    Dosave();
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
            if (string.IsNullOrEmpty(context.Request["Name"]) || string.IsNullOrEmpty(context.Request["Value"]))
            {
                Common.AjaxMsgHelper.AjaxMsg("err", "系统信息名/系统信息值不能为空");
                context.Response.End();
            }

            MODEL.T_Settings set = new MODEL.T_Settings()
            {
                Name = context.Request["Name"],
                 Value = context.Request["Value"]
            };
            try
            {
                //2.调用业务层对象 提交
                int id=bllset.Add(set);
                set.Id = id;
                string data = Common.DataHelper.Obj2Json(set);
                Common.AjaxMsgHelper.AjaxMsg("ok", "增加数据成功！");
            }
            catch (Exception ex)
            {
                Common.AjaxMsgHelper.AjaxMsg("err", "异常：" + ex.Message);
            }
        }
        #endregion

        private void Dosave()
        {
           
            MODEL.T_Settings set = new MODEL.T_Settings()
            { Id= Convert.ToInt32(context.Request["id"]),
                 Name=context.Request["name"],
                  Value=context.Request["value"]
            };
            if (bllset.Update(set) == true)
            {
                Common.AjaxMsgHelper.AjaxMsg("ok", "修改成功");
            }
            else
            {
                Common.AjaxMsgHelper.AjaxMsg("err", "PostCode/TelPhone/Fax/Mobile 必须是数字");
            }
            
        }

        #region 删除菜单
        /// <summary>
        /// 删除菜单
        /// </summary>
        private void DoDel()
        {
            if (bllset.Del(context.Request["mid"]) == true)
            {
                Common.AjaxMsgHelper.AjaxMsg("ok", "删除成功");
            }
        }
        #endregion
        
    }
}
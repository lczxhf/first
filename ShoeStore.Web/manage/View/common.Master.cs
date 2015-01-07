using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoeStore.Web.manage.View
{
    public partial class common : System.Web.UI.MasterPage
    {
         protected System.Text.StringBuilder sbHtml = new System.Text.StringBuilder(1000);
        protected void Page_Load(object sender, EventArgs e)
        {

              //1.调用业务层对象 读取 后台菜单 数据
            BLL.MgrMenu bllMgrMenu = new BLL.MgrMenu();
            //2.获取排序后的数据
            IList<MODEL.MgrMenu> list = bllMgrMenu.GetList();
            //3.生成菜单
            MakeMenuHtml(list,0);
        }

        #region 1.0 生成父菜单 -void MakeMenuHtml(IList<MODEL.MgrMenu> list)
        /// <summary>
        /// 生成父菜单
        /// </summary>
        /// <param name="list"></param>
        void MakeMenuHtml(IList<MODEL.MgrMenu> list,int pId)
        {
            //循环生成 父菜单
            foreach (MODEL.MgrMenu menu in list)
            {
                if (menu.MgrPId == 0&&pId==0)
                {
                    sbHtml.AppendLine("<ul>");
                    sbHtml.AppendLine("<li class=\"claTitle\">" + menu.MgrName + "</li>");
                    sbHtml.AppendLine("<li>");
                    //生成 子菜单
                    MakeMenuHtml(list, menu.MgrId);

                    sbHtml.AppendLine("</li>");
                    sbHtml.AppendLine("</ul>");
                    continue;
                }
                if (menu.MgrPId == pId)
                {
                    sbHtml.AppendLine("<ul>");
                    sbHtml.AppendLine("<li><a href=\"" + menu.MgrLinkUrl + "\">" + menu.MgrName + "</a></li>");
                    sbHtml.AppendLine("</ul>");
                }
            }
        } 
        #endregion

        //#region 2.0 生成子菜单 -void MakeSonMenuHtml(IList<MODEL.MgrMenu> list, int pId)
        ///// <summary>
        ///// 2.0 生成子菜单
        ///// </summary>
        ///// <param name="list"></param>
        ///// <param name="pId"></param>
        //void MakeSonMenuHtml(IList<MODEL.MgrMenu> list, int pId)
        //{
        //    foreach (MODEL.MgrMenu menuSon in list)
        //    {
        //        //生成子菜单
        //        if (menuSon.MgrPId == pId)
        //        {
        //            sbHtml.AppendLine("<ul>");
        //            sbHtml.AppendLine("<li><a href=\"" + menuSon.MgrLinkUrl + "\">" + menuSon.MgrName + "</a></li>");
        //            sbHtml.AppendLine("</ul>");
        //        }
        //    }
        //}
        //#endregion
        }
    }

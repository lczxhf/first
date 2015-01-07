using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoeStore.Web.foreground.View
{
    public partial class Common : System.Web.UI.MasterPage, IRequiresSessionState
    {
        public System.Text.StringBuilder state = new System.Text.StringBuilder(500);
        protected System.Text.StringBuilder sbHtml = new System.Text.StringBuilder(500);
        protected ShoeStore.BLL.Menu bllmenu=null;
        protected ShoeStore.BLL.ProductCate bllcate = null;
        protected ShoeStore.BLL.TransactionLog bllbuy = null;
        IList<MODEL.ProductCate> cateList = null;
        protected BLL.T_Settings bllSet = null;
        protected Dictionary<string,string> setting = null;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                state.Append("<div id='header_right' style='width:80px'><br /><br /><a href='#' id='login' style='width:80px'>登 陆</a></div>");

            }
            else if (Session["isSuper"]==null)
            {
                MODEL.Users user = Session["user"] as MODEL.Users;
                state.Append("<div id='header_right'><p>" + user.ULoginName + "&nbsp;&nbsp;&nbsp;<a href='#' id='info'>我的账号</a> | <a href='ShoppingCart.aspx'>我的购物车</a> | <a href='#' id='back'>退 出</a></p></div>");

            }
            else
            {
                MODEL.SuperUsers suser = Session["user"] as MODEL.SuperUsers;
                state.Append("<div id='header_right' style='width:200px'><br /><br />"+suser.Name+"&nbsp;&nbsp;<a href='/manage/View/Home.aspx' >进入后台</a> | <a href='#'  id='back' >退出</a></div>");

            }
            //获取菜单条的数据
            bllmenu = new BLL.Menu();
            IList<MODEL.Menu> menuList= bllmenu.GetList();
            this.rptlist.DataSource = menuList;
            this.rptlist.DataBind();
            //this.rptFoot.DataSource = menuList;
            //this.rptFoot.DataBind();
            //获取分类的数据
            bllcate = new BLL.ProductCate();
             cateList= bllcate.GetList();
            //this.rptCate.DataSource = cateList;
            //this.rptCate.DataBind();
             bllSet = new BLL.T_Settings();
             setting = bllSet.GetSeting();
            MakeMenuHtml(cateList);
            //获取最近购买的数据
            bllbuy = new BLL.TransactionLog();
            IList<MODEL.NewBuy> newBuy = bllbuy.GetNewBuy(7,5);
            this.Top.DataSource = newBuy;
            this.Top.DataBind();
            

        }

               #region 1.0 生成父菜单 -void MakeMenuHtml(IList<MODEL.MgrMenu> list)
        /// <summary>
        /// 生成父菜单
        /// </summary>
        /// <param name="list"></param>
        void MakeMenuHtml(IList<MODEL.ProductCate> list)
        {
            //循环生成 父菜单
            foreach (MODEL.ProductCate menu in list)
            {
                if (menu.PId == 0)
                {
                    sbHtml.AppendLine("<ul>");
                    sbHtml.AppendLine("<li class=\"claTitle\">" + menu.PName + "</li>");
                    sbHtml.AppendLine("<li>");
                    //生成 子菜单
                    MakeSonMenuHtml(list, menu.Id);

                    sbHtml.AppendLine("</li>");
                    sbHtml.AppendLine("</ul>");
                }
            }
        } 
        #endregion

        #region 2.0 生成子菜单 -void MakeSonMenuHtml(IList<MODEL.MgrMenu> list, int pId)
        /// <summary>
        /// 2.0 生成子菜单
        /// </summary>
        /// <param name="list"></param>
        /// <param name="pId"></param>
        void MakeSonMenuHtml(IList<MODEL.ProductCate> list, int pId)
        {
            foreach (MODEL.ProductCate menuSon in list)
            {
                //生成子菜单
                if (menuSon.PId == pId)
                {
                    sbHtml.AppendLine("<ul>");
                    sbHtml.AppendLine("<li><a href='/foreground/View/Products.aspx?type=cate&cateId=" + menuSon.Id + "&name=" + menuSon.PName + "'>" + menuSon.PName + "</a></li>");
                    sbHtml.AppendLine("</ul>");
                }
            }
        } 
        #endregion
        
    }
}
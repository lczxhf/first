using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoeStore.Web.manage.View
{
    public partial class Menu :BasePage
    {
        BLL.Menu bllMenu = new BLL.Menu();
       public override void SonLoad()
        {
            //1.通过业务层对象 用户菜单 列表
            IList<MODEL.Menu> list = bllMenu.GetList();
            //2.将数据绑定到 前台 控件
            this.rptList.DataSource = list;
            this.rptList.DataBind();
        }    
    }
}
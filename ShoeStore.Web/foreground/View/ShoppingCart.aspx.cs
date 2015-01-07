using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoeStore.Web.foreground.View
{
    public partial class ShoppingCart : BasePage
    {
        
        BLL.Car bllcar = new BLL.Car();
        public override void SonLoad()
        {
            MODEL.Users user= Context.Session["user"] as MODEL.Users;
            MODEL.Car car= bllcar.GetUserCar(user.UId);
            this.rptCar.DataSource = car.ItemList;
            this.rptCar.DataBind();
        }
    }
}
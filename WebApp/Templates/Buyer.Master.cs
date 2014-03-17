using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MODEL = com.portal.db.Model;
namespace WebApp
{
    public partial class Buyer : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 判断用户是否已登录。
            if (HttpContext.Current.User.Identity.Name != "")
            {
                MODEL.User user = (MODEL.User)Session["user"];
                
                //if (HttpContext.Current.User.Identity.Name == "" || user == null)
                //{
                //    Session.Add("unLogn", "No Login , Can not operation!!");
                //    Response.Redirect("~/Account/Login.aspx");
                //}
                if (user.UserType != "Buyer")
                {
                    Session.Add("unLogn", "Illegal operation!!");
                    Response.Redirect("~/Account/Login.aspx");
                }
                else
                {
                    string rawUrl = Request.RawUrl;
                    string[] url = rawUrl.Split('/');

                    this.UserName.Value = user.UserName;

                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MODEL = com.portal.db.Model;


namespace WebApp
{
    public partial class Supplier : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            MODEL.User user = (MODEL.User)Session["user"];
            ////判断用户是否已登录。
            //if (HttpContext.Current.User.Identity.Name == "" || user == null)
            //{
            //    Session.Add("unLogn", "No Login , Can not operation!!");
            //    Response.Redirect(ResolveUrl("~/Account/Login.aspx"));
            //}
            if (user.UserType != "Supplier")
            {
                Session.Add("unLogn", "Illegal operation!!");
                Response.Redirect(ResolveUrl("~/Account/Login.aspx"));
            }
            else
            {
                string rawUrl = Request.RawUrl;
                string[] url = rawUrl.Split('/');

                this.UserName.Value = user.UserName;

            }
        }

        protected void select_site(object sender, EventArgs e) {

            Response.Redirect(ResolveUrl("~/WebContent/Supplier/SelectSite.aspx"));
        }


        protected void Change(object sender, EventArgs e)
        {

            Response.Redirect(ResolveUrl("~/WebContent/Supplier/ChangePassword.aspx"));
        }

        protected void DifSite(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl("~/WebContent/Supplier/SelectSite.aspx"));
        }

        protected void OutSystem(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect(ResolveUrl("~/Account/Login.aspx"));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MODEL = com.portal.db.Model;

namespace WebApp
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MODEL.User user = (MODEL.User)Session["User"];
            String regist = Request["regist"];
            // 判断用户是否已登录。

            if (!"regist".Equals(regist) && regist != "regist")
            {

                if (user == null || "".Equals(user))
                {
                    Session.Add("unLogn", "No Login , Can not operation!!");
                    Response.Redirect("~/Account/Login.aspx");
                }
                else
                {
                    //string rawUrl = Request.RawUrl;
                    //string[] url = rawUrl.Split('/');

                    //if (url.Length < 2)
                    //{
                    //    Response.Redirect("~/Account/Login.aspx");
                    //}
                    //else
                    //{
                    this.UserName.Value = user.UserName;
                    //}
                }
            }
        }

        protected void Change(object sender, EventArgs e)
        {

            Response.Redirect(ResolveUrl("~/WebContent/Supplier/ChangePassword.aspx"));
        }

        protected void DifUser(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect(ResolveUrl("~/Account/Login.aspx"));
        }

        protected void OutSystem(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect(ResolveUrl("~/Account/Login.aspx"));
        }
    }
}

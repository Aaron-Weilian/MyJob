using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL = com.portal.db.BLL;
using Model = com.portal.db.Model;
using System.Web.Security;
using Log;
using Tool;


namespace WebApp.Account
{
    public partial class Login : System.Web.UI.Page
    {
        private BLL.User userServer;

        protected void Page_Load(object sender, EventArgs e)
        {
            string text = (string)Session["unLogn"];

            this.mes.Text = text;
            this.mes.Style.Add(HtmlTextWriterStyle.Color, "red");
            this.mes.Style.Add(HtmlTextWriterStyle.FontSize, "1.2em");


            userServer = new BLL.User();
            Session.Clear();

            
        }

        protected void LoginSystem(object sender, EventArgs e)
        {
            string username = this.UserName.Text.Trim();
            string password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(this.Password.Text.Trim(), "MD5"); 

            Model.User user = userServer.Login(username, password);

            if (user != null)
            {
                Log.LogHelper.Info("Login", "Login",
                    HttpContext.Current.Request.UserHostAddress,
                    user.UserID,
                    "User  " + username + "  Login System!");


                if (user.Status == "Active" || user.Status == "SuperMan")
                {
                    FormsAuthentication.SetAuthCookie(username, true);
                    Session.Add("user", user);
                    if (user.UserType == "Admin")
                    {
                        if (user.Role == "0X1000BF")
                        {
                            Response.Redirect(ResolveUrl("~/WebContent/Management/UserManagement.aspx"));
                        }
                        if (user.Role == "0X1000CF")
                        {
                            Response.Redirect(ResolveUrl("~/WebContent/Management/AplicationUserList.aspx"));
                        }
                    }
                    if (user.UserType == "Buyer")
                    {
                        Response.Redirect(ResolveUrl("~/WebContent/Buyer/Inbox.aspx"));
                    }
                    if (user.UserType == "Supplier")
                    {
                        if ("no".Equals(user.isFirstLogin))
                        {
                            Response.Redirect(ResolveUrl("~/WebContent/Supplier/SelectSite.aspx"));
                        }
                        else
                        {
                            Response.Redirect(ResolveUrl("~/Account/ChangePassword.aspx"));
                        }
                    }
                }
                if (user.Status == "Pending") {

                    this.mes.Text = "The account is pending……!!";
                    this.mes.Style.Add(HtmlTextWriterStyle.Color, "red");
                    this.mes.Style.Add(HtmlTextWriterStyle.FontSize, "1.2em");
                    return;
                }
                if (user.Status == "Cancel")
                {
                    this.mes.Text = "The account examination not through!!";
                    this.mes.Style.Add(HtmlTextWriterStyle.Color, "red");
                    this.mes.Style.Add(HtmlTextWriterStyle.FontSize, "1.2em");
                    return;
                }
                else
                {

                    this.mes.Text = "The account is Inactive";
                    this.mes.Style.Add(HtmlTextWriterStyle.Color, "red");
                    this.mes.Style.Add(HtmlTextWriterStyle.FontSize, "1.2em");

                    Log.LogHelper.Warn("Login", "Login",
                    HttpContext.Current.Request.UserHostAddress,
                    "",
                    "User" + username + "Login System Fail!");

                }
            }
            else {
                this.mes.Text = "Input has error,please again.";
                this.mes.Style.Add(HtmlTextWriterStyle.Color, "red");
                this.mes.Style.Add(HtmlTextWriterStyle.FontSize, "1.2em");

                Log.LogHelper.Warn("Login", "Login",
                    HttpContext.Current.Request.UserHostAddress,
                    "",
                    "User" + username + "Login System Fail!");
            }
        }

        protected void RegisterUser(object sender, EventArgs e) {

            Response.Redirect("Register.aspx?regist=regist",false);
        }
    
    }
}

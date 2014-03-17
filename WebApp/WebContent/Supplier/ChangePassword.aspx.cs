using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Log;
using Tool;
using BLL = com.portal.db.BLL;
using Model = com.portal.db.Model;
using System.Text.RegularExpressions;

namespace WebApp.WebContent.Supplier
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        //private static readonly ILog logObject = LogHelper.GetInstance();
        private Model.User user;

        protected void Page_Load(object sender, EventArgs e)
        {
            user = (Model.User)Session["User"];

            if ("no".Equals(user.isFirstLogin))
            {
                mes.InnerText = "";
            }
            else
            {
                //mes.InnerText = "First Login System Must Change Password!!";
            }
        }

        public void ChangeUserPassword(object sender, EventArgs e ) {

            if (Page.IsValid)
            {

                BLL.User userServer = new BLL.User();

                //String oldpassword = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(this.CurrentPassword.Text, "MD5");

                String newpassword = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(this.ConfirmNewPassword.Text, "MD5");

                user.UserPassword = newpassword;

                try
                {
                    if (userServer.Update(user))
                    {
                        mes.InnerText = "password update success";
                        mes.Style.Add(HtmlTextWriterStyle.Color, "green");

                        Log.LogHelper.Info("ChangePassword", "Change Password", 
                            HttpContext.Current.Request.UserHostAddress,
                            user.UserID,
                            "password update success , user:" + user.UserName);
                    }
                    else
                    {
                        Log.LogHelper.Error("ChangePassword", "Change Password",
                                    HttpContext.Current.Request.UserHostAddress,
                                    user.UserID,
                                    "password update Fail , user:" + user.UserName);
                    }
                }
                catch (Exception err)
                {

                    Log.LogHelper.Error("ChangePassword", "Change Password",
                                    HttpContext.Current.Request.UserHostAddress,
                                    user.UserID,
                                    "password update Fail , user:" + user.UserName, err);

                }


                //logObject.Info(user.UserName + " had update password");
            }
           
        }

        protected void CustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            String oldpassword = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(this.CurrentPassword.Text, "MD5");

            if (user.UserPassword != oldpassword)
            {
                args.IsValid = false; 
            }

        }

        protected void ValidationPasswordLength(object str, ServerValidateEventArgs arguments)
        {
            if (arguments.Value.Length < 8)
            {
                arguments.IsValid = false;
            }

        }
        protected void ValidationPasswordValid(object str, ServerValidateEventArgs arguments)
        {

            var reg = new Regex("(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])[a-zA-Z0-9]{8,15}");

            if (!reg.IsMatch(arguments.Value))
            {
                arguments.IsValid = false;
            }


        }   

        
    }
}

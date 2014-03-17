using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Log;
using Tool;
using Model = com.portal.db.Model;
using BLL = com.portal.db.BLL;
using System.Text.RegularExpressions;

namespace WebApp.WebContent.Management
{
    public partial class EditUser : System.Web.UI.Page
    {
        private BLL.Supplier supplierServer;
        private BLL.User userServer;
        private Model.User user;
        
        //public string supplierName = "--";
        //public string supplierNum = "";
        public string userType = "--";
        public string isFirstLogin = "yes";

        protected void Page_Load(object sender, EventArgs e)
        {
            
            userServer = new BLL.User();
            supplierServer = new BLL.Supplier();
            user = (Model.User)Session["User"];
            mes.InnerText = "";
            if (!Page.IsPostBack)
            {

                init_user();
                init_drop();
            }

            
            
        }

        protected void init_drop() {

           
            if ("Admin".Equals(userType)) {
                this.UsersType.Items.FindByValue("Admin").Selected = true;
            }
            if ("Buyer".Equals(userType))
            {
                this.UsersType.Items.FindByValue("Buyer").Selected = true;
            }
            if ("Supplier".Equals(userType)) {
                this.UsersType.Items.FindByValue("Supplier").Selected = true;
            }

            this.UsersType.Attributes.Add("onchange", "change('"+this.UsersType.ClientID+"')");
        }

        protected void init_user() {

            string UserId = Request["userID"];
            Model.User u = userServer.GetModel(UserId);
            this.UserName.Value = u.UserName;
            userType = u.UserType;
            //this.susupplierName = u.Supplier;
            //supplierNum = u.SupplierNum;
            this.supplierName.Value = u.Supplier;
            this.supplierNum.Value = u.SupplierNum;
            this.Discription.Value = u.Discription;
            this.Phone.Value = u.Phone;
            this.Email.Value = u.Email;
            this.UserID.Value = u.UserID;
            isFirstLogin = u.isFirstLogin;
        }
    

        protected void save_Click(object sender, EventArgs e)
        {

            Model.User model = userServer.GetModel(this.UserID.Value);
            string email = this.Email.Value;
            string usertype = this.UsersType.SelectedValue;

            if (this.UserName.Value == "")
            {

                mes.InnerText = "UserName must input";
                mes.Style.Add(HtmlTextWriterStyle.Color, "RED");
                return;

            }
            if (email == "" || email==null)
            {
                mes.InnerText = "Email must input";
                mes.Style.Add(HtmlTextWriterStyle.Color, "RED");
                return;
            }
            else
            {
                var reg = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");

                if (!reg.IsMatch(email))
                {
                    mes.InnerText = "Sorry! The email format is Error.";
                    mes.Style.Add(HtmlTextWriterStyle.Color, "RED");
                    return;
                }

            }
            if (usertype == "Admin" || usertype == null)
            {
                model.Supplier = "--";
            }
            if (usertype == "Buyer" || usertype==null)
            {
                model.Supplier = "--";
            }
            if (usertype == "Supplier" || usertype == null)
            {
                if (Request["supplierName"] == "--" || Request["supplierName"] == "")
                {
                    mes.InnerText = "Supplier Must Input SupplierName !!!";
                    mes.Style.Add(HtmlTextWriterStyle.Color, "RED");
                    return;
                }
                else
                {
                    model.Supplier = Request["supplierName"];
                    model.SupplierNum = Request["supplierNum"];
                    model.Status = "Pending";
                }

            }
            model.UserName = this.UserName.Value;
            model.UserType = usertype;
            model.Email = email;
            model.Phone = this.Phone.Value;
            model.Updated = System.DateTime.Now.ToShortDateString();
            model.UpdateBy = user.UserName;
            model.isFirstLogin = isFirstLogin;

            try
            {
                if (userServer.Update(model))
                {
                    Log.LogHelper.Info("WebApp.WebContent.Management.EditUser", "Add User",
                        HttpContext.Current.Request.UserHostAddress,
                        user.UserID,
                        "Add User：" + user.UserName + " Success");

                    mes.InnerText = "User " + user.UserName + " update success!";
                    mes.Style.Add(HtmlTextWriterStyle.Color, "green");
                }
                else
                {

                    Log.LogHelper.Error("WebApp.WebContent.Management.EditUser", "Add User",
                        HttpContext.Current.Request.UserHostAddress,
                        user.UserID,
                        "Add User：" + user.UserName + " Fail");
                }
            }
            catch(Exception err){
                Log.LogHelper.Fatal("WebApp.WebContent.Management.EditUser", "Add User",
                            HttpContext.Current.Request.UserHostAddress,
                            user.UserID,
                            "Add User：" + user.UserName + " Fail" ,
                            err);

            }


            init_user();
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserManagement.aspx");
        }

        protected void init_Click(object sender, EventArgs e)
        {
            Model.User model = userServer.GetModel(this.UserID.Value);

            string password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile("Somc12345", "MD5");

            model.UserPassword = password;

            model.isFirstLogin = "yes";

            try
            {
                if (userServer.Update(model))
                {
                    Log.LogHelper.Info("WebApp.WebContent.Management.EditUser", "Init Password",
                        HttpContext.Current.Request.UserHostAddress,
                        user.UserID,
                        "Init user" + user.UserName + " Password Success");

                    mes.InnerText = "User " + user.UserName + " update success!";
                    mes.Style.Add(HtmlTextWriterStyle.Color, "green");

                    ClientScriptManager cs = this.ClientScript;
                    cs.RegisterStartupScript(this.GetType(), "alert", "<script>alert('Password:Somc12345,Please Remember!!')</script>");  

                }
                else
                {

                    Log.LogHelper.Error("WebApp.WebContent.Management.EditUser", "Init Password",
                        HttpContext.Current.Request.UserHostAddress,
                        user.UserID,
                        "Init " + user.UserName + " Password  Fail");
                }
            }
            catch (Exception err)
            {
                Log.LogHelper.Fatal("WebApp.WebContent.Management.EditUser", "Change Password",
                            HttpContext.Current.Request.UserHostAddress,
                            user.UserID,
                            "Init " + user.UserName + " Password  Fail",
                            err);

            }


            
            init_user();
        }

        

        
    }
}
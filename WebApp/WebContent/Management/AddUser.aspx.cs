using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Log;
using Tool;
using BLL = com.portal.db.BLL;
using MODEL = com.portal.db.Model;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WebApp.WebContent.Management
{
    public partial class AddUser : System.Web.UI.Page
    {
        BLL.User userServer;
        BLL.Supplier supplierServer;
        private MODEL.User user;

        //private static readonly ILog logObject = LogHelper.GetInstance();

        protected void Page_Load(object sender, EventArgs e)
        {
            userServer = new BLL.User();
            supplierServer = new BLL.Supplier();
            user = (MODEL.User)Session["User"];
            mes.InnerText = "";
            init_drop();
           
        }

        protected void init_drop()
        {
            this.UsersType.Attributes.Add("onchange", "change('" + this.UsersType.ClientID + "')");
        }

        protected void save_Click(object sender, EventArgs e)
        {
            
            string username = Request["UserName"];
            string password = "Somc12345";
            string email = Request["Email"];
            if (username == "")
            {
                mes.InnerText = "UserName must input";
                mes.Style.Add(HtmlTextWriterStyle.Color, "RED");
                return;
            }
            else
            {
                String sql = " UserName ='" + username + "' ";

                List<MODEL.User> list =  userServer.GetModelList(sql);

                if (list.Count > 0) {
                    mes.InnerText = "Sorry! This user name already exists,Please Input Again.";
                    mes.Style.Add(HtmlTextWriterStyle.Color, "RED");
                    return;
                }
            }
            if (email == "" || email == null)
            {
                mes.InnerText = "Email must input";
                mes.Style.Add(HtmlTextWriterStyle.Color, "RED");
                return;
            }
            else {
                var reg = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");

                if (!reg.IsMatch(email))
                {
                    mes.InnerText = "Sorry! The email format is Error.";
                    mes.Style.Add(HtmlTextWriterStyle.Color, "RED");
                    return;
                }
            
            }
            String pd = System.Guid.NewGuid().ToString("N");

            MODEL.User model = new MODEL.User();
            model.UserName = username;
            model.UserType = this.UsersType.SelectedValue;
            model.Email = Request["Email"];
            model.Phone = Request["Phone"];
            model.Discription = Request["Discription"];
            model.Status = "Active";



            if (this.UsersType.SelectedValue == "--")
            {
                mes.InnerText = "Please Select User Type !!!";
                mes.Style.Add(HtmlTextWriterStyle.Color, "RED");
                return;
            }
            if (this.UsersType.SelectedValue == "Admin") {
                model.Supplier = "--";
                model.Role = "0X1000BF";
                model.RoleName = "Administrator";
            }
            if (this.UsersType.SelectedValue == "Buyer")
            {
                model.Supplier = "--";
                model.Role = "0A10000A";
                model.RoleName = "General User";
            }
            if (this.UsersType.SelectedValue == "Supplier")
            {
                if (Request["supplierName"] == "--" || Request["supplierName"] == "")
                {
                    mes.InnerText = "Supplier Must Input SupplierName !!!";
                    mes.Style.Add(HtmlTextWriterStyle.Color, "RED");
                    return;
                }
                else {
                    model.Supplier = Request["supplierName"];
                    model.SupplierNum = Request["supplierNum"];
                    model.Role = "0A10000A";
                    model.RoleName = "General User";
                    model.Status = "Pending";
                }
                
            }



            model.Updated = System.DateTime.Now.ToShortDateString();
            model.Created = System.DateTime.Now.ToShortDateString();
            model.CeateBy = user.UserName;
            model.UpdateBy = user.UserName;
            
            model.isFirstLogin = "yes";
            
            if (this.G.Checked == true)
            {
                password = getPassword();
                
            }
            string MD5Password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
            model.UserPassword = MD5Password;

            try
            {
                if (userServer.Add(model))
                {
                    mes.InnerText = "User " + username + " save success!!\r\n Password : " + password + ", Please Remember!!!";
                    mes.Style.Add(HtmlTextWriterStyle.Color, "Blue");

                    Log.LogHelper.Info("WebApp.WebContent.Management.AddUser", "Add User",
                        HttpContext.Current.Request.UserHostAddress,
                        user.UserID,
                        "Add User Success , name:" + username);
                }
                else
                {

                    Log.LogHelper.Error("WebApp.WebContent.Management.AddUser", "Add User",
                            HttpContext.Current.Request.UserHostAddress,
                            user.UserID,
                            "Add User Success , name:" + username);
                }
            }catch(Exception err){

                Log.LogHelper.Error("WebApp.WebContent.Management.AddUser", "Add User",
                            HttpContext.Current.Request.UserHostAddress,
                            user.UserID,
                            "Add User Success , name:" + username, err);
            
            }


            
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserManagement.aspx");
        }

        private string getPassword()
        {
            string[] s1 = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k",
                          "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
                          "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O",
                          "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };


            string password = "";
            long tick = DateTime.Now.Ticks;
            Random rand = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            for (int i = 0; i < 8; i++)
            {
                
                int num = rand.Next(3);

                if (num == 0) { password += (rand.Next(9)).ToString(); }
                else if (num == 1) { password += s1[rand.Next(0, 27)]; }
                else if (num == 2) { password += s1[rand.Next(27, 52)]; }
            }
            return password;
        }
    }
}
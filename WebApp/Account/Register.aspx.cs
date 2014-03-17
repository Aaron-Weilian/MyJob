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

namespace WebApp.Account
{
    public partial class Register : System.Web.UI.Page
    {
        BLL.User userServer;
        BLL.Supplier supplierServer;
        private MODEL.User user;

        //private static readonly ILog logObject = LogHelper.;

        protected void Page_Load(object sender, EventArgs e)
        {
            userServer = new BLL.User();
            supplierServer = new BLL.Supplier();
            this.UsersType.Attributes.Add("onchange", "change('" + this.UsersType.ClientID + "')");
            ClientScript.RegisterClientScriptInclude(this.GetType(), "Include", ResolveUrl("~/Scripts/web.js"));
        }

        protected void save_Click(object sender, EventArgs e)
        {

            string username = Request["UserName"];
            string email = Request["Email"];
            string usertype = this.UsersType.SelectedValue;
            string password = "Somc12345";
            MODEL.User model = new MODEL.User();

            if (username == "")
            {

                mes.InnerText = "UserName must input";
                mes.Style.Add(HtmlTextWriterStyle.Color, "RED");
                return;

            }
            else
            {
                String sql = " UserName ='" + username + "' ";

                List<MODEL.User> list = userServer.GetModelList(sql);

                if (list.Count > 0)
                {
                    mes.InnerText = "Sorry! This user name already exists,Please Input Again.";
                    mes.Style.Add(HtmlTextWriterStyle.Color, "RED");
                    return;
                }
            }
            if (email == "") {
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
            if (usertype == "--")
            {
                mes.InnerText = "Please Select User Type !!!";
                mes.Style.Add(HtmlTextWriterStyle.Color, "RED");
                return;
            }
            else
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
                }

            }
            model.Role = "0A10000A";
            model.RoleName = "General User";
            model.UserName = username;
            model.UserType = usertype;
            model.Email = email;
            model.Phone = Request["Phone"];
            model.Discription = Request["Discription"];


            model.Updated = System.DateTime.Now.ToShortDateString();
            model.Created = System.DateTime.Now.ToShortDateString();
            
            model.Status = "Pending";
            model.isFirstLogin = "yes";

            if (this.G.Checked == true)
            {
                password = getPassword();

            }
            string MD5Password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
            model.UserPassword = MD5Password;

            userServer.Add(model);
            mes.InnerText = "The User " + username + " application has been submitted, please remember the password:\r\n" + password;
            mes.Style.Add(HtmlTextWriterStyle.Color, "Blue");


            //logObject.Info("The User " + username + " application has been submitted!!");


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

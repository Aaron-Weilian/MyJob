using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MODEL = com.portal.db.Model;
namespace WebApp
{
    public partial class Management : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MODEL.User user = (MODEL.User)Session["user"];
          
            if (user.UserType != "Admin")
            {
                Session.Add("unLogn", "Illegal operation!!");
                Response.Redirect("~/Account/Login.aspx");
            }
            else
            {
                string rawUrl = Request.RawUrl;
                string[] url = rawUrl.Split('/');

                this.UserName.Value = user.UserName;
                if (user.Role == "0X1000BF") {
                    NavigationMenu.Items.Add(new MenuItem
                        {
                            Text = "User Management",
                            NavigateUrl = "~/WebContent/Management/UserManagement.aspx"
                        });
                    NavigationMenu.Items.Add(new MenuItem
                    {
                        Text = "Supplier Management",
                        NavigateUrl = "~/WebContent/Management/SupplierManagement.aspx"
                    });
                    NavigationMenu.Items.Add(new MenuItem
                    {
                        Text = "Log Trace",
                        NavigateUrl = "~/WebContent/Management/LogTrace.aspx"
                    });
                    NavigationMenu.Items.Add(new MenuItem
                    {
                        Text = "Monitor File",
                        NavigateUrl = "~/WebContent/Management/MonitorFile.aspx"
                    });
                    NavigationMenu.Items.Add(new MenuItem
                    {
                        Text = "Schedule Service",
                        NavigateUrl = "~/CrystalQuartzPanel.axd"
                    });
                }

                if (user.Role == "0X1000CF")
                {
                    MenuItem node = new MenuItem();
                    node.Text = "Audit User";
                    node.NavigateUrl = "~/WebContent/Management/AplicationUserList.aspx";

                    MenuItem node2 = new MenuItem();
                    node2.Text = "Audit Supplier";
                    node2.NavigateUrl = "~/WebContent/Management/AplicationSupplierList.aspx";
                    this.NavigationMenu.Items.Add(node);
                    this.NavigationMenu.Items.Add(node2);
                }
                //}
                //}

            }
        }

        protected void Change(object sender, EventArgs e)
        {

            Response.Redirect(ResolveUrl("~/WebContent/Management/ChangePassword.aspx"));
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL = com.portal.db.BLL;
using Model = com.portal.db.Model;


namespace WebContent.Supplier
{
    public partial class SelectSite : System.Web.UI.Page
    {
        private Model.User user;
        private BLL.Supplier supplierServer;
        private BLL.Site siteServer;

        protected void Page_Load(object sender, EventArgs e)
        {
            supplierServer = new BLL.Supplier();
            siteServer = new BLL.Site();
            BLL.User userServer = new BLL.User();
            user = (Model.User)Session["user"];

            if (!Page.IsPostBack)
            {
                initSiteList();
            }
        }

        void initSiteList() {

            List<Model.Supplier> list = supplierServer.GetModelList("SupplierNum = '" + user.SupplierNum + "' and Status = 'Active'");
            if (list.Count > 0)
            {
                Model.Supplier supplier = list[0];
                this.siteList.DataSource = siteServer.GetModelList(" SupplierID ='" + supplier.SupplierID + "'");
                this.siteList.DataMember = "SiteName";
                this.siteList.DataTextField = "SiteName";
                this.siteList.DataValueField = "SiteNum";
                this.siteList.DataBind();
                ListItem all = new ListItem("all", "");
                this.siteList.Items.Insert(0, all);
            }
            else {
                this.lab.Visible = true;
                this.lab.Style.Add(HtmlTextWriterStyle.Color, "red");
                this.lab.Text = "User Supplier is Disable!";
                this.login.Visible = false;
            
            }
        }

        protected void login_Click(object sender, EventArgs e)
        {
            Session.Add("SiteNum", this.siteList.SelectedValue);
            Response.Redirect("~/WebContent/Supplier/Inbox.aspx");
        }

    }
}
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

namespace WebApp.WebContent.Management
{
    public partial class AddSupplier : System.Web.UI.Page
    {
        private BLL.Supplier supplierServer;
        private Model.User user;
        //private static readonly ILog logObject = LogHelper.GetInstance();

        protected void Page_Load(object sender, EventArgs e)
        {
            user = (Model.User)Session["user"];
            supplierServer = new BLL.Supplier();
            mes1.InnerText = "";
            mes2.InnerText = "";
        }

       
        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("SupplierManagement.aspx");
        }

        protected void save_Click(object sender, EventArgs e)
        {
            System.DateTime currentTime = new DateTime();
            currentTime = System.DateTime.Now;

            if (this.SupplierName.Value == "")
            {

                mes1.InnerText = "SupplierName must input,Please Input SupplierName";
                mes1.Style.Add(HtmlTextWriterStyle.Color, "RED");
                return;

            }
            if (this.SupplierNUM.Value == "")
            {

                mes1.InnerText = "SupplierNUM must input,Pleaer Input SupplierNUM";
                mes1.Style.Add(HtmlTextWriterStyle.Color, "RED");
                return;

            }

            List<Model.Supplier> l = supplierServer.GetModelList("SupplierNUM='" + this.SupplierNUM.Value + "'");

            if (l.Count > 0) {

                mes2.InnerText = "Supplier NUM Already Exists";
                mes2.Style.Add(HtmlTextWriterStyle.Color, "RED");
                return;
            }


            if (this.SiteList.Items.Count == 0)
            {

                mes2.InnerText = "Supplier Site Must Add";
                mes2.Style.Add(HtmlTextWriterStyle.Color, "RED");
                return;

            }

            if (this.Email.Value == "" || this.Email.Value == null)
            {
                mes.InnerText = "Email must input";
                mes.Style.Add(HtmlTextWriterStyle.Color, "RED");
                return;
            }
            else
            {
                var reg = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");

                if (!reg.IsMatch(this.Email.Value))
                {
                    mes.InnerText = "Sorry! The email format is Error.";
                    mes.Style.Add(HtmlTextWriterStyle.Color, "RED");
                    return;
                }

            }


            Model.Supplier model = new Model.Supplier();

            model.SupplierName = this.SupplierName.Value;
            model.SupplierNUM = this.SupplierNUM.Value;
            model.ContactName = this.ContactName.Value;
            model.CountryCode = this.CountryCode.Value;
            model.SupplierPhone = this.Phone.Value;
            model.SupplierEmail = this.Email.Value;
            model.SupplierAddress = this.Address1.Value;
            model.DUNS = this.Duns.Value;
            model.Status = "Pending";
            model.Created = currentTime.ToShortDateString();
            model.Updated = currentTime.ToShortDateString();
            model.CreateBy = user.UserName;
            model.UpdateBy = user.UserName;

            try
            {
                if (supplierServer.Add(model))
                {
                    mes1.InnerText = this.SupplierName.Value + " save success";
                    mes1.Style.Add(HtmlTextWriterStyle.Color, "green");

                    Log.LogHelper.Info("WebApp.WebContent.Management.AddSupplier","Add Supplier",
                        HttpContext.Current.Request.UserHostAddress,
                        user.UserID,
                        "Add Supplier Success , name:" + this.SupplierName.Value);

                }
                else
                {
                    Log.LogHelper.Error("WebApp.WebContent.Management.AddSupplier", "Add Supplier",
                            HttpContext.Current.Request.UserHostAddress,
                            user.UserID,
                            "Add Supplier Fail , name:" + this.SupplierName.Value);

                }
            }
            catch(Exception err){

                Log.LogHelper.Error("WebApp.WebContent.Management.AddSupplier", "Add Supplier",
                            HttpContext.Current.Request.UserHostAddress,
                            user.UserID,
                            "Add Supplier Fail , name:" + this.SupplierName.Value,err);
            
            }


            Model.Supplier supplier = supplierServer.DataRowToModel((supplierServer.GetList("SupplierNUM='" + this.SupplierNUM.Value + "'")).Tables[0].Rows[0]);
            BLL.Site siteServer = new BLL.Site();
            if (this.SiteList.Items.Count > 0)
            {

                foreach (ListItem item in this.SiteList.Items)
                {
                    Model.Site site = new Model.Site();
                    string[] value = item.Text.Split('/');

                    site.SiteName = value[0];
                    site.SiteNUM = value[1];
                    site.SupplierID = supplier.SupplierID;

                    siteServer.Add(site);

                }

            }


        }

        protected void Addsite_Click(object sender, EventArgs e)
        {
            ListItem item = new ListItem();
            if (!"".Equals(this.SiteName.Value) && !"".Equals(this.SiteNUM.Value))
            {
                item.Text = this.SiteName.Value + "/" + this.SiteNUM.Value;
                this.SiteList.Items.Add(item);
            }
            else {
                mes1.InnerText = "This SiteName is Null,Please Input!!! ";
                mes1.Style.Add(HtmlTextWriterStyle.Color, "RED");
            }
          
        }

        protected void Remocesite_Click(object sender, EventArgs e)
        {
           
            this.SiteList.Items.Remove(this.SiteList.SelectedItem);
          
        }
    }
}
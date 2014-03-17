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
    public partial class EditSupplier : System.Web.UI.Page
    {
        private BLL.Supplier supplierServer;
        private BLL.Site siteServer;
        private Model.User user;
        //private static readonly ILog logObject = LogHelper.GetInstance();

        protected void Page_Load(object sender, EventArgs e)
        {
            supplierServer = new BLL.Supplier();
            siteServer = new BLL.Site();
            user = (Model.User)Session["User"];
            mes1.InnerText = "";
            mes2.InnerText = "";
            if (!Page.IsPostBack)
            {
                string SupplierID = Request["SupplierID"];
                Model.Supplier supplier = supplierServer.GetModel(SupplierID);
                this.SupplierName.Value = supplier.SupplierName;
                this.SupplierNUM.Value = supplier.SupplierNUM;
                this.Duns.Value = supplier.DUNS;
                this.Phone.Value = supplier.SupplierPhone;
                this.Email.Value = supplier.SupplierEmail;
                this.ContactName.Value = supplier.ContactName;
                this.CountryCode.Value = supplier.CountryCode;
                this.Address1.Value = supplier.SupplierAddress;
                this.SupplierID.Value = supplier.SupplierID;

                this.SiteList.DataSource = siteServer.GetModelList("SupplierID='" + SupplierID + "'");
                this.SiteList.DataMember = "NameNUM";
                this.SiteList.DataTextField = "NameNUM";
                //this.SiteList.
                this.SiteList.DataBind();

            }

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
            if (this.SiteList.Items.Count == 0 )
            {

                mes2.InnerText = "Supplier Site must input";
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

            
            Model.Supplier Model = supplierServer.GetModel(this.SupplierID.Value);

            Model.SupplierName = this.SupplierName.Value;
            Model.ContactName = this.ContactName.Value;
            Model.CountryCode = this.CountryCode.Value;
            Model.SupplierPhone = this.Phone.Value;
            Model.SupplierEmail = this.Email.Value;
            Model.SupplierAddress = this.Address1.Value;
            Model.DUNS = this.Duns.Value;
            Model.Status = "Pending";
            Model.Updated = currentTime.ToShortDateString();;
            Model.UpdateBy = user.UserName;

            try
            {
                if (supplierServer.Update(Model))
                {
                     mes1.InnerText = this.SupplierName.Value + " update success";
                     mes1.Style.Add(HtmlTextWriterStyle.Color, "green");

                     Log.LogHelper.Info("WebApp.WebContent.Management.EditSupplier", "Edit Supplier",
                        HttpContext.Current.Request.UserHostAddress,
                        user.UserID,
                        "Edit Supplier Success , name:" + this.SupplierName.Value);


                }
                else
                {

                    Log.LogHelper.Error("WebApp.WebContent.Management.EditSupplier", "Edit Supplier",
                        HttpContext.Current.Request.UserHostAddress,
                        user.UserID,
                        "Edit Supplier Success , name:" + this.SupplierName.Value);
                }
            }
            catch(Exception err){
                Log.LogHelper.Error("WebApp.WebContent.Management.EditSupplier", "Edit Supplier",
                           HttpContext.Current.Request.UserHostAddress,
                           user.UserID,
                           "Edit Supplier Success , name:" + this.SupplierName.Value, err);
            }
           

            //logObject.Info(user.UserName + " had update the" + this.SupplierName.Value + " supplier ");


            siteServer.DeleteBySupplierID(this.SupplierID.Value);

            if (this.SiteList.Items.Count > 0)
            {

                foreach (ListItem item in this.SiteList.Items)
                {
                    Model.Site site = new Model.Site();

                    string[] value = item.Text.Split('/');

                    site.SiteName = value[0];
                    site.SiteNUM = value[1];
                    site.SupplierID = this.SupplierID.Value;

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
            else
            {
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
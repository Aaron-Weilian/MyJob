using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL = com.portal.db.BLL;
using Model = com.portal.db.Model;

namespace WebApp.WebContent.Management
{
    public partial class AplicationSupplierList : System.Web.UI.Page
    {
        private BLL.Supplier supplierServer;
        int pageCount;//总页数
        int currentPage = 1;//第定义当前页
        private Model.User user;

        protected void Page_Load(object sender, EventArgs e)
        {
            supplierServer = new BLL.Supplier();
            user = (Model.User)Session["User"];
            this.cancel.Attributes.Add("onclick", "return (confirm('Are you sure disable the user?'))");


            if (!Page.IsPostBack)
            {
                initSupplierGridList();
            }
        }

        void initSupplierGridList() {

            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = supplierServer.GetSiteList(" [Status] = 'Pending'");

            pds.AllowPaging = true;
            //设置每页显示记录数
            pds.PageSize = 10;
            //获取总页数
            pageCount = pds.PageCount;
            this.total.Text = pageCount.ToString();
            pds.CurrentPageIndex = currentPage - 1;
            //当前页
            this.current.Text = Convert.ToString(currentPage);

            this.supplierList.DataSource = pds;
            this.supplierList.DataBind();
        }

        protected void add_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddSupplier.aspx");
        }

        protected void edit_Click(object sender, EventArgs e)
        {
            String SupplierID = Request["SupplierID"];
            if (SupplierID != null)
            {
                Response.Redirect("EditSupplier.aspx?SupplierID=" + SupplierID);
            }
        }

        protected void disable_Click(object sender, EventArgs e)
        {
            String SupplierID = Request["SupplierID"];
            if (SupplierID != null)
            {

                Model.User u = (Model.User)Session["User"];
                Model.Supplier Model = supplierServer.GetModel(SupplierID);

                Model.Updated = System.DateTime.Now.ToShortDateString();
                Model.UpdateBy = u.UserName;
                Model.Status = "Cancel";

                supplierServer.Update(Model);

                Log.LogHelper.Info("WebApp.WebContent.Management.AplicationSupplierList", "Aduit",
                    HttpContext.Current.Request.UserHostAddress,
                    user.UserID,
                    "Aduit Supplier, Cancel supplier :" + Model.SupplierName);

                initSupplierGridList();
            }
        }

        protected void enable_Click(object sender, EventArgs e)
        {
            String SupplierID = Request["SupplierID"];
            if (SupplierID != null)
            {
                Model.User u = (Model.User)Session["User"];
                Model.Supplier Model = supplierServer.GetModel(SupplierID);

                Model.Updated = System.DateTime.Now.ToShortDateString();
                Model.UpdateBy = u.UserName;
                Model.Status = "Active";

                supplierServer.Update(Model);

                Log.LogHelper.Info("WebApp.WebContent.Management.AplicationSupplierList", "Aduit",
                    HttpContext.Current.Request.UserHostAddress,
                    user.UserID,
                    "Aduit Supplier, Enable supplier :" + Model.SupplierName);

                initSupplierGridList();
            }
        }

        protected void Search_Click(object sender, EventArgs e) {
            string sql = " 1=1 ";

            string suppliername = Request["SupplierName"];
            string duns = Request["DUNS"];
            string suppliersite = Request["SupplierSite"];
            string status = Request["Status"];

            if (suppliername != "") {
                sql += " and SupplierName like '%" + suppliername + "%'";
            }
            if (duns != "") {
                sql += " and DUNS like '%" + duns + "%'";
            }
            if (suppliersite != "") {
                sql += " and SiteName like '%" + suppliersite + "%'";
            }
            sql += " and [Status] = 'Pending'";

            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = supplierServer.GetSiteList(sql);

            pds.AllowPaging = true;
            //设置每页显示记录数
            pds.PageSize = 10;
            //获取总页数
            pageCount = pds.PageCount;
            this.total.Text = pageCount.ToString();
            pds.CurrentPageIndex = currentPage - 1;
            //当前页
            this.current.Text = Convert.ToString(currentPage);


            this.supplierList.DataSource = pds;
            this.supplierList.DataBind();        
        }

        protected void Button1_Click(object sender, EventArgs e)
        {//如果当前不是第一页的时候
            if (this.current.Text == "1")
            {

            }
            else
            {
                currentPage = 1;
                initSupplierGridList();
            }
        }
        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button2_Click(object sender, EventArgs e)
        {//如果当前不是最后页的时候
            if (this.total.Text == this.current.Text)
            {
            }
            else
            {
                currentPage = int.Parse(this.current.Text) + 1;
                this.current.Text = currentPage.ToString();
                initSupplierGridList();
            }
        }
        /// <summary>
        /// 上一页　
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button3_Click(object sender, EventArgs e)
        {//如果当前不是第一页的时候
            if (this.current.Text != "1")
            {
                currentPage = int.Parse(this.current.Text) - 1;
                this.current.Text = currentPage.ToString();
                initSupplierGridList();
            }
        }
        /// <summary>
        /// 最后一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button4_Click(object sender, EventArgs e)
        {//如果当前不是最后一页的时候
            if (this.total.Text != this.current.Text)
            {
                this.current.Text = this.total.Text;
                currentPage = int.Parse(this.current.Text);
                initSupplierGridList();
            }
        }
    }
}
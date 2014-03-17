﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL = com.portal.db.BLL;
using Model = com.portal.db.Model;

namespace WebApp.WebContent.Management
{
    public partial class SupplierManagement : System.Web.UI.Page
    {
        private BLL.Supplier supplierServer;
        int pageCount;//总页数
        int currentPage = 1;//第定义当前页

        protected void Page_Load(object sender, EventArgs e)
        {
            supplierServer = new BLL.Supplier();

            this.disable.Attributes.Add("onclick", "return (confirm('If disable the Supplier, the Supplier User will can not receive message!! Are you sure disable the Supplier?'))");
            this.enable.Style.Add(HtmlTextWriterStyle.Display, "none");


            if (!Page.IsPostBack)
            {
                initSupplierGridList();
            }
        }

        void initSupplierGridList() {

            string sql = " ([Status] = 'Inactive' or [Status] = 'Active')";

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
                Model.Status = "Inactive";

                try
                {
                    if (supplierServer.Update(Model))
                    {

                        Log.LogHelper.Info("WebApp.WebContent.Management.SupplierManagement", "Disable Supplier",
                            HttpContext.Current.Request.UserHostAddress,
                            u.UserID,
                            "Disable Supplier Success , name:" + Model.SupplierName);
                    }
                    else
                    {
                        Log.LogHelper.Error("WebApp.WebContent.Management.SupplierManagement", "Disable Supplier",
                                    HttpContext.Current.Request.UserHostAddress,
                                    u.UserID,
                                    "Disable Supplier Fail , name:" + Model.SupplierName);
                    }
                }
                catch (Exception err)
                {

                    Log.LogHelper.Error("WebApp.WebContent.Management.SupplierManagement", "Disable Supplier",
                                    HttpContext.Current.Request.UserHostAddress,
                                    u.UserID,
                                    "Disable Supplier Fail , name:" + Model.SupplierName, err);

                }

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

                try
                {
                    if (supplierServer.Update(Model))
                    {

                        Log.LogHelper.Info("WebApp.WebContent.Management.SupplierManagement", "Enable Supplier",
                            HttpContext.Current.Request.UserHostAddress,
                            u.UserID,
                            "Enable Supplier Success , name:" + Model.SupplierName);
                    }
                    else
                    {
                        Log.LogHelper.Error("WebApp.WebContent.Management.SupplierManagement", "Enable Supplier",
                                    HttpContext.Current.Request.UserHostAddress,
                                    u.UserID,
                                    "Enable Supplier Fail , name:" + Model.SupplierName);
                    }
                }
                catch (Exception err)
                {

                    Log.LogHelper.Error("WebApp.WebContent.Management.SupplierManagement", "Enable Supplier",
                                    HttpContext.Current.Request.UserHostAddress,
                                    u.UserID,
                                    "Enable Supplier Fail , name:" + Model.SupplierName, err);

                }

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
            if (status != "--") {
                sql += " and [Status] = '" + status + "'";
            }
            if (suppliersite != "") {
                sql += " and SiteName like '%" + suppliersite + "%'";
            }
            if (status == "--")
            {
                sql += " and ([Status] = 'Inactive' or [Status] = 'Active')";
            }
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
                Search_Click(sender,e);
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
                Search_Click(sender, e);
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
                Search_Click(sender, e);
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
                Search_Click(sender, e);
            }
        }
    }
}
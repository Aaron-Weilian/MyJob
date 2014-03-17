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
    public partial class SupplierList : System.Web.UI.Page
    {
        private BLL.Supplier supplierServer;
        int pageCount;//总页数
        int currentPage = 1;//第定义当前页

        protected void Page_Load(object sender, EventArgs e)
        {
            supplierServer = new BLL.Supplier();
            if (!Page.IsPostBack)
            {
                initSupplierGridList();
            }
        }

        void initSupplierGridList()
        {

            PagedDataSource pds = new PagedDataSource();
            string sql = "[Status] = 'Active'";
            pds.DataSource = supplierServer.GetModelList(sql);

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


        protected void Search_Click(object sender, EventArgs e)
        {
            string sql = " 1=1 ";

            string suppliername = Request["SupplierName"];
           

            if (suppliername != "")
            {
                sql += " and SupplierName like '%" + suppliername + "%'";
            }

            sql += "  and [Status] = 'Active'";
            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = supplierServer.GetModelList(sql);

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
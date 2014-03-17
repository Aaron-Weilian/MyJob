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
    public partial class AplicationUserList : System.Web.UI.Page
    {
        private BLL.Supplier supplierServer;
        private BLL.User userServer;
        private Model.User u;
        int pageCount;//总页数
        int currentPage = 1;//第定义当前页

        protected void Page_Load(object sender, EventArgs e)
        {
            supplierServer = new BLL.Supplier();
            userServer = new BLL.User();
            u = (Model.User)Session["User"];

            this.cancel.Attributes.Add("onclick", "return (confirm('Are you sure disable the user?'))");

            if (!IsPostBack)
            {
              
                initUserGridList();
                init_drop();
            }
            
        }

        protected void init_drop()
        {
            this.UsersType.Attributes.Add("onchange", "change('" + this.UsersType.ClientID + "')");
        }

        void initUserGridList()
        {
            string sql = "1=1";

            if (u.UserType == "Supplier") {

                sql += " and SupplierNum = '"+u.SupplierNum+"'";
            }

            sql += " and ([Status] = 'Pending')";
            
            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = userServer.GetModelList(sql);

            pds.AllowPaging = true;
            //设置每页显示记录数
            pds.PageSize = 10;
            //获取总页数
            pageCount = pds.PageCount;
            this.total.Text = pageCount.ToString();
            pds.CurrentPageIndex = currentPage - 1;
            //当前页
            this.current.Text = Convert.ToString(currentPage);

            this.userList.DataSource = pds;
           
            
            this.userList.DataBind();
            
           
        }

      


        protected void disable_Click(object sender, EventArgs e)
        {
           
            String UserID = Request["UserID"];
            if (UserID != null)
            {
                
                Model.User user = userServer.GetModel(UserID);

                user.Status = "Cancel";
                user.Updated = System.DateTime.Now.ToShortDateString();
                user.UpdateBy = u.UserName;

                userServer.Update(user);

                Log.LogHelper.Info("WebApp.WebContent.Management.AplicationUserList", "Aduit",
                   HttpContext.Current.Request.UserHostAddress,
                   u.UserID,
                   "Aduit Supplier, Cancel user :" + user.UserName);

                initUserGridList();
            }
        }

        protected void enable_Click(object sender, EventArgs e)
        {
            String UserID = Request["UserID"];
            if (UserID != null)
            {
                
                Model.User user = userServer.GetModel(UserID);

                user.Status = "Active";
                user.Updated = System.DateTime.Now.ToShortDateString();
                user.UpdateBy = u.UserName;

                userServer.Update(user);

                Log.LogHelper.Info("WebApp.WebContent.Management.AplicationUserList", "Aduit",
                   HttpContext.Current.Request.UserHostAddress,
                   u.UserID,
                   "Aduit Supplier, Enable user :" + user.UserName);

                initUserGridList();
            }
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            string sql = "1=1";
            string username = Request["UserName"];
            string usertype = this.UsersType.SelectedValue;
            string supplierName = Request["supplierName"];
            if (username != "" && username != null)
            {
                sql += " and UserName like '%" + username + "%' ";
            }
            if (usertype != "--" && usertype!=null)
            {
                sql += " and UserType = '" + usertype + "' ";
            }
            if (usertype != "Supplier")
            {
                supplierName = "--";
            }
            if (!"--".Equals(supplierName) && supplierName != null)
            {
                sql += " and Supplier = '" + supplierName + "' ";
            }
           
            sql += " and [Status] = 'Pending'";
           
            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = userServer.GetModelList(sql);

            pds.AllowPaging = true;
            //设置每页显示记录数
            pds.PageSize = 10;
            //获取总页数
            pageCount = pds.PageCount;
            this.total.Text = pageCount.ToString();
            pds.CurrentPageIndex = currentPage - 1;
            //当前页
            this.current.Text = Convert.ToString(currentPage);


            this.userList.DataSource = pds;
            this.userList.DataBind();        
        }


        protected void Button1_Click(object sender, EventArgs e)
        {//如果当前不是第一页的时候
            if (this.current.Text == "1")
            {

            }
            else
            {
                currentPage = 1;
                initUserGridList();
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
                initUserGridList();
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
                initUserGridList();
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
                initUserGridList();
            }
        }
    }
}
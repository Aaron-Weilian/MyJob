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
    public partial class AssignRole : System.Web.UI.Page
    {
        private BLL.Role roleServer;
        private BLL.User userServer;
        private Model.User user;
        int pageCount;//总页数
        int currentPage = 1;//第定义当前页

        protected void Page_Load(object sender, EventArgs e)
        {
            roleServer = new BLL.Role();
            userServer = new BLL.User();
            string UserId = Request["userID"];
            user = userServer.GetModel(UserId);
            initRoleGridList();
        }

        void initRoleGridList()
        {
            string sql = "1=1";
            //sql += " and [Status] <> 'SuperMan'";

            if (user.UserType == "Supplier" || user.UserType == "Buyer")
            {
                sql += " and RoleNum = '0A10000A'";
            }
            else {
                sql += " and RoleNum <> '0A10000A'";
            }

            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = roleServer.GetModelList(sql);

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



        protected void role_Click(object sender, EventArgs e)
        {
            String RoleNum = Request["RoleNum"];

            string sql = "RoleNum = '" + RoleNum+"'";
            List<Model.Role> l = roleServer.GetModelList(sql);
            if (l.Count > 0)
            {
                if (user != null)
                {
                    user.Role = RoleNum;
                    user.RoleName = l[0].RoleName;

                    try
                    {
                        if (userServer.Update(user))
                        {
                            Log.LogHelper.Info("WebApp.WebContent.Management.AssignRole","Assign Role",
                                HttpContext.Current.Request.UserHostAddress,
                                user.UserID,
                                "Assign Role ,username：" + user.UserName + " Success");

                            mes.InnerText = "The role is assigned to user!";
                            mes.Style.Add(HtmlTextWriterStyle.Color, "green");
                        }
                        else
                        {

                            Log.LogHelper.Error("WebApp.WebContent.Management.AssignRole", "Assign Role",
                                HttpContext.Current.Request.UserHostAddress,
                                user.UserID,
                                "Assign Role ,username:" + user.UserName + " Fail");
                        }
                    }
                    catch (Exception err)
                    {
                        Log.LogHelper.Fatal("WebApp.WebContent.Management.AssignRole", "Assign Role",
                                    HttpContext.Current.Request.UserHostAddress,
                                    user.UserID,
                                    "Assign Role ,username:" + user.UserName + " Fail",
                                    err);

                    }


                    
                }
            }

        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserManagement.aspx");
        }


        protected void Button1_Click(object sender, EventArgs e)
        {//如果当前不是第一页的时候
            if (this.current.Text == "1")
            {

            }
            else
            {
                currentPage = 1;
                initRoleGridList();
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
                initRoleGridList();
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
                initRoleGridList();
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
                initRoleGridList();
            }
        }
    }
}
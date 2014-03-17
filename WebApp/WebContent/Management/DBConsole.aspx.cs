using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL = com.portal.db.BLL;
using com.portal.db.DAL;
using System.Data;

namespace WebApp.WebContent.Management
{
    public partial class DBConsole : System.Web.UI.Page
    {

        private BLL.ServerImpl server;
        int pageCount;//总页数
        int currentPage = 1;//第定义当前页

        protected void Page_Load(object sender, EventArgs e)
        {
            server = new BLL.ServerImpl();
            this.m.InnerText = "";
            if (!IsPostBack)
            {

            }
        }

        void bindData(string sql)
        {
            com.portal.db.DAL.CommomDAL commomDal = new CommomDAL();
            DataSet dataSet = commomDal.QuerySQL(sql);
            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = dataSet.Tables[0].DefaultView;

            pds.AllowPaging = true;
            //设置每页显示记录数
            pds.PageSize = 10;
            //获取总页数
            pageCount = pds.PageCount;
            this.total.Text = pageCount.ToString();
            pds.CurrentPageIndex = currentPage - 1;
            //当前页
            this.current.Text = Convert.ToString(currentPage);


            this.grdSQL.DataSource = pds;
            this.grdSQL.DataBind();
        }


        protected void btnExeSql_Click(object sender, EventArgs e)
        {
            this.m.InnerText = "";
            string sql = this.txtSQL.Text.ToString();

            if (sql.IndexOf("select") < 0)
            {
                int n = 0;
                try
                {

                   n = server.Execute(this.txtSQL.Text.ToString());
                }
                catch { 
                
                }
                if (n == 0)
                {
                    this.m.InnerText = "Fail";
                }
                else { this.m.InnerText = "OK"; }
            }
            else {

                whereSQL.Value = sql;
                com.portal.db.DAL.CommomDAL commomDal = new CommomDAL();
                DataSet dataSet = commomDal.QuerySQL(sql);

                PagedDataSource pds = new PagedDataSource();
                pds.DataSource = dataSet.Tables[0].DefaultView;

                pds.AllowPaging = true;
                //设置每页显示记录数
                pds.PageSize = 10;
                //获取总页数
                pageCount = pds.PageCount;
                this.total.Text = pageCount.ToString();
                pds.CurrentPageIndex = currentPage - 1;
                //当前页
                this.current.Text = Convert.ToString(currentPage);

                this.grdSQL.DataSource = pds;
                this.grdSQL.DataBind();
            }

            
        }



        protected void Button1_Click(object sender, EventArgs e)
        {//如果当前不是第一页的时候
            if (this.current.Text == "1")
            {

            }
            else
            {
                currentPage = 1;
                bindData(whereSQL.Value);
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
                bindData(whereSQL.Value);
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
                bindData(whereSQL.Value);
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
                bindData(whereSQL.Value);
            }
        }

    }
}
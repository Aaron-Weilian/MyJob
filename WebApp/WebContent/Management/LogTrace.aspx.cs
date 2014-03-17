using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.portal.db.DAL;

namespace WebApp.WebContent.Management
{
    public partial class LogTrace : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsCrossPagePostBack) {
                Search_Click(sender,e);
            }
        }

        int pageCount;//总页数
        int currentPage = 1;//第定义当前页
        
        void bindData(string sql){

            com.portal.db.DAL.CommomDAL commomDal = new CommomDAL();
            DataSet dataSet = commomDal.QuerySQL("select * from Log where " + sql);
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


            this.logList.DataSource = pds;
            this.logList.DataBind();
        }

        protected void Search_Click(object sender, EventArgs e){
            string sql = "1=1 ";
            string fromdate = Request["FromDate"];
            string todate = Request["ToDate"];
            string level = Request["loglevel"];
            string evenName = Request["EvenName"];
            string Logger = Request["Logger"];
            

            if (!string.IsNullOrEmpty(fromdate)) {
                sql += " and date > '" + fromdate + "' ";
            }
            if (!string.IsNullOrEmpty(todate))
            {
                sql += " and date < '" + todate + "' ";
            }
            if (!string.IsNullOrEmpty(level))
            {
                sql += " and [Level] = '" + level + "' ";
            }
            if (!string.IsNullOrEmpty(evenName))
            {
                sql += " and [EvenName] = '" + evenName + "' ";
            }
            if (!string.IsNullOrEmpty(Logger))
            {
                sql += " and [Logger] = '" + Logger + "' ";
            }
            whereSQL.Value = sql;
            bindData(whereSQL.Value);
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
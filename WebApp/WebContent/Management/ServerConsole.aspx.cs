using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace WebApp.WebContent.Management
{
    public partial class ServerConsole : System.Web.UI.Page
    {
        int pageCount;//总页数
        int currentPage = 1;//第定义当前页

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            com.portal.db.Model.User user = (com.portal.db.Model.User)Session["user"];

            Process p = new Process();
            try
            {
                foreach (Process thisproc in Process.GetProcessesByName("JobService"))
                {
                    if (!thisproc.CloseMainWindow())
                    {
                        thisproc.Kill();

                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogHelper.Error("ServiceMain", "Stop schedule server",
                       HttpContext.Current.Request.UserHostAddress,
                       user.UserID,
                       "Stop Server Fail", ex);
            }
            try
            {

                //p.StartInfo.FileName = @"G:\visual studio 2010\tag\ScheduleService\bin\Debug\ScheduleService.exe";

                p.StartInfo.FileName = @"E:\WWWRoot\JobServer\Prod\JobService.exe";



                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.UseShellExecute = true;
                //p.StartInfo.CreateNoWindow = true;

                p.Start();

                Log.LogHelper.Info("ServiceMain", "Start is ok",
                     HttpContext.Current.Request.UserHostAddress,
                     user.UserID,
                     string.Format("Path:{0}", p.StartInfo.FileName));
            }
            catch (Exception ex)
            {
                Log.LogHelper.Error("ServiceMain", "Start schedule server",
                           HttpContext.Current.Request.UserHostAddress,
                           user.UserID,
                           string.Format("Start Server Fail Path:{0}", p.StartInfo.FileName), ex);

            }
        }


        protected void stop_Click(object sender, EventArgs e)
        {
            com.portal.db.Model.User user = (com.portal.db.Model.User)Session["user"];

            try
            {
                foreach (Process thisproc in Process.GetProcessesByName("JobService"))
                {
                    Log.LogHelper.Info("ServiceMain", "server",
                      HttpContext.Current.Request.UserHostAddress,
                      user.UserID,
                      string.Format("one process name:{0}", thisproc.ProcessName));

                    if (!thisproc.CloseMainWindow())
                    {
                        thisproc.Kill();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogHelper.Error("ServiceMain", "Stop schedule server",
                       HttpContext.Current.Request.UserHostAddress,
                       user.UserID,
                       "Stop Server Fail", ex);
            }
        }
        
        
        protected void btnShow_Click(object sender, EventArgs e)
        {
            PagedDataSource pds = new PagedDataSource();
            Process[] processes = Process.GetProcessesByName("JobService");

            pds.DataSource = processes;

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



        protected void Button1_Click(object sender, EventArgs e)
        {//如果当前不是第一页的时候
            if (this.current.Text == "1")
            {

            }
            else
            {
                currentPage = 1;
                btnShow_Click(sender, e);
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
                btnShow_Click(sender, e);
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
                btnShow_Click(sender, e);
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
                btnShow_Click(sender, e);
            }
        }

    }
}
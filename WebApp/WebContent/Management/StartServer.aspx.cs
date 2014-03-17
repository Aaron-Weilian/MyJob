using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace WebApp.WebContent.Management
{
    public partial class StartServer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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

            Response.Redirect("~/CrystalQuartzPanel.axd");

        }
    }
}
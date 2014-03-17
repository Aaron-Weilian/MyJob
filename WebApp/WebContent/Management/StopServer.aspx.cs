using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace WebApp.WebContent.Management
{
    public partial class StopServer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            com.portal.db.Model.User user = (com.portal.db.Model.User)Session["user"];
            Process p = new Process();
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

            Response.Redirect("~/CrystalQuartzPanel.axd");
        }
    }
}
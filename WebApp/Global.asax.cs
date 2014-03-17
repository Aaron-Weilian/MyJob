using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.IO;
using Log;
using Tool;
using com.portal.db.BLL.NROS;
using com.portal.db.BLL.PO;
using com.portal.db.DAL;
using BLL = com.portal.db.BLL;
using Model = com.portal.db.Model;

namespace WebApp
{
    public class Global : System.Web.HttpApplication
    {
        //Dictionary<string, string> dic = new Dictionary<string, string>();

        void Application_Start(object sender, EventArgs e)
        {
            Log.LogHelper.InitLogger(LogConfiguration.GetConfigFile(), GetAppSetting.GetConnSetting());

            //Log.LogHelper.Info("Global", "PortalSystem",
            //        "Server",
            //        "System",
            //        "Application Start …… !");
            
        }

        void Application_End(object sender, EventArgs e)
        {
            //Log.LogHelper.Info("Global", "PortalSystem",
            //     "Server",
            //     "System",
            //     "Application End …… !");
        }

        void Application_Error(object sender, EventArgs e)
        {
            //Exception ex = Server.GetLastError();

            //Log.LogHelper.Error("Global", "PortalSystem",
            //     "Server",
            //     "System",
            //     "Application Error …… !", ex);
        }

        void Session_Start(object sender, EventArgs e)
        {
            //Log.LogHelper.Info("Global", "PortalSystem",
            //      "Server",
            //      "System",
            //      "Session start …… !");

        }

        void Session_End(object sender, EventArgs e)
        {
            //Log.LogHelper.Info("Global", "PortalSystem",
            //     "Server",
            //     "System",
            //     "Session End …… !");

        }
    }
}

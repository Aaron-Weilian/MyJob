using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;



namespace XMLService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //static void Main(string[] Args)
        static void Main()
        {

            //if(Args.Length==0)//这是服务启动的条件
            if (0 == 0)//这是服务启动的条件
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
			    { 
				    new XmlServer() 
			    };
                ServiceBase.Run(ServicesToRun);
            }
            else
            {
                //ServiceSoapClient webserver = new ServiceSoapClient();
                //string str = webserver.HelloWorld();


                XmlServer server = new XmlServer();
                server.readFiles(server.dic["inRosPath"], "ROS");
                server.readFiles(server.dic["inPOPath"], "PO");
                server.exportFiles();


                ////server.EventLog.WriteEntry("XmlService Start");//在系统事件查看器里的应用程序事件里来源的描述  
                ////server.writestr("XmlService Start");//自定义文本日志  
                //System.Timers.Timer t = new System.Timers.Timer();
                //t.Interval = 1000 * 60;
                //t.Elapsed += new System.Timers.ElapsedEventHandler(server.dowork);//到达时间的时候执行事件；   
                //t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；   
                //t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件； 
                //t.Start();

                //Map map = new Map();

                //bool isImpersonated = false;
                //try
                //{
                //using (new IdentityScope(@"10.155.36.82", @"e10057", @"Emex1234", LogonType.NewCredentials, LogonProvider.WinNT50))
                //{
                //    //File.Copy(@"\\10.155.36.82\1\1.txt", @"c:\tmp\1.txt", true);

                //    server.moveFiles(server.dic["RemoteOutRosPath"], server.dic["inRosPath"], server.dic["inBackupPath"]);
                //    server.readFiles(server.dic["inRosPath"], "ros");
                //}

                    //if (map.impersonateValidUser(@"10.155.36.82\e10057", @"10.155.36.82", @"Emex1234"))
                    //{
                    //    isImpersonated = true;
                    //    do what you want now, as the special user
                    //     ...
                    //    File.Copy(@"\\10.155.36.82\tmp\1\1.txt", "c:\\tmp\\1.txt", true);
                    //    server.moveFiles(server.dic["RemoteMappingOutRosPath"], server.dic["inRosPath"], server.dic["inBackupPath"]);

                    //    server.readFiles(server.dic["inRosPath"], "ros");


                    //}
                //}
                //catch(Exception e){

                //    }
                //finally
                //{
                //    if (isImpersonated)
                //        map.undoImpersonation();
                //}

                //XmlServer server2 = new XmlServer();

                //server2.exportFiles(server2.dic["outRosPath"]);

                //server2.moveFiles(server2.dic["outRosPath"], server2.dic["RemoteMappingInRosPath"], server2.dic["outBackupPath"]);
            }


            
        }

    }
}

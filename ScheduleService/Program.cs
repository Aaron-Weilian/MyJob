using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using Quartz;
using Quartz.Impl;
using com.portal.db.DAL;
using System.Data.SqlClient;

namespace com.portal.JobService
{
    class QuartzService
    {
        public const string CONFIG_FILE_NAME = "Log4net.xml";


        private static Quartz.Impl.StdSchedulerFactory _schedulerFactory;

        static void Main(string[] args){

            //Log.LogHelper.InitLogger(new FileInfo(getConfigFilePath()), GetAppSetting.GetConnSetting());

            //new UploadFilesJob().Execute(null);
            //return;
            
            Log.LogHelper.InitLogger(new FileInfo(getConfigFilePath()), GetAppSetting.GetConnSetting());

            Log.LogHelper.Info("ServiceMain", "PortalJobScheduler", "Server", "Server", "Starting scheduler...");

            var properties = new NameValueCollection();
            properties["quartz.scheduler.instanceName"] = "PortalJobScheduler";

            // set thread pool info
            properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
            properties["quartz.threadPool.threadCount"] = "20";
            properties["quartz.threadPool.threadPriority"] = "Normal";

            // set remoting expoter
            properties["quartz.scheduler.exporter.type"] = "Quartz.Simpl.RemotingSchedulerExporter, Quartz";
            properties["quartz.scheduler.exporter.port"] = "123";
            properties["quartz.scheduler.exporter.bindName"] = "PortalJobScheduler";
            properties["quartz.scheduler.exporter.channelType"] = "tcp";

            try
            {
                _schedulerFactory = new Quartz.Impl.StdSchedulerFactory(properties);
                var scheduler = _schedulerFactory.GetScheduler();

                foreach (
                    var job in
                        typeof(IJobSetup).Assembly.GetTypes()
                                          .Where(t => t.IsClass && typeof(IJobSetup).IsAssignableFrom(t)))
                {
                    ((IJobSetup)Activator.CreateInstance(job)).Setup(scheduler);
                }

                scheduler.Start();
            }
            catch (Exception ex)
            {

                Log.LogHelper.Error("ServiceMain", "PortalSchedule", "Server", "Server", "Starting scheduler fail", ex);

            }
            Log.LogHelper.Info("ServiceMain", "PortalSchedule", "Server", "System", "Scheduler has been started.");
            Console.ReadKey();
        }

      

        private static string getConfigFilePath()
        {
            string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string configPath = Path.Combine(basePath, CONFIG_FILE_NAME);

            if (!File.Exists(configPath))
            {
                configPath = Path.Combine(basePath, "bin");
                configPath = Path.Combine(configPath, CONFIG_FILE_NAME);

                if (!File.Exists(configPath))
                {
                    configPath = Path.Combine(basePath, @"..\" + CONFIG_FILE_NAME);
                }
            }

            return configPath;
        }
    }
}

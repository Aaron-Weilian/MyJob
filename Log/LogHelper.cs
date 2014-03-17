using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using log4net;
using Log.Extensions;
using System.Reflection;

namespace Log
{
    public class LogHelper
    {
        private static bool isLoggerWatching = false;
        public static void InitLogger(FileInfo configFile,string SqlConnection)
        {
            if (!isLoggerWatching)
            {
                if (configFile != null)
                {
                    log4net.Config.XmlConfigurator.ConfigureAndWatch(configFile);

                    log4net.Repository.Hierarchy.Hierarchy hierarchy = log4net.LogManager.GetRepository() as log4net.Repository.Hierarchy.Hierarchy;
                    if (hierarchy != null && hierarchy.Configured)
                    {
                        foreach (log4net.Appender.IAppender appender in hierarchy.GetAppenders())
                        {
                            if (appender is log4net.Appender.AdoNetAppender)
                            {
                                var adoNetAppender = (log4net.Appender.AdoNetAppender)appender;
                                adoNetAppender.ConnectionString = SqlConnection;
                                adoNetAppender.ActivateOptions();
                            }
                        }
                    }

                    isLoggerWatching = true;

                }
            }
        }


        public static void InitLogger(string SqlConnection)
        {
            string assemblyFilePath = Assembly.GetExecutingAssembly().Location;
            string assemblyDirPath = Path.GetDirectoryName(assemblyFilePath);
            string configFilePath = assemblyDirPath + " \\log4net.xml";
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(configFilePath));


        }


        private static ILog GetLogger(string Type)
        {
            ILog logger = (log4net.ILog)log4net.LogManager.GetLogger(Type);
            return logger;
        }


        /// <summary>
        /// Change the Web.config
        /// &lt;add key="logConfigFile" value="configs\log4net2.xml"/&gt;
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="userIP"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>

        public static void Info(string Type,string eventName, string userIP,string operatorID, string message, Exception ex = null)
        {

       // public static void Info(string eventName, string userIP,string operatorID, string message, Exception ex = null){


            ILog logger = GetLogger(Type); 
            if (logger.IsInfoEnabled)
            {
                CustomPatternMessage cpm = new CustomPatternMessage
                {
                    EventName = eventName,
                    UserIP = userIP,
                    OperatorID = operatorID,
                    Message = message
                };
                if (ex == null)
                    logger.Info(cpm);
                else
                    logger.Info(cpm, ex);
            }
        }

        public static void Warn(string Type, string eventName, string userIP, string operatorID, string message, Exception ex = null)
        {
            ILog logger = GetLogger(Type); 
            if (logger.IsInfoEnabled)
            {
                CustomPatternMessage cpm = new CustomPatternMessage
                {
                    EventName = eventName,
                    UserIP = userIP,
                    OperatorID = operatorID,
                    Message = message
                };
                if (ex == null)
                    logger.Warn(cpm);
                else
                    logger.Warn(cpm, ex);
            }
        }


        public static void Error(string Type, string eventName, string userIP, string operatorID, string message, Exception ex = null)

       // public static void Error(string eventName, string userIP, string operatorID, string message, Exception ex = null)

        {
            ILog logger = GetLogger(Type); 
            if (logger.IsInfoEnabled)
            {
                CustomPatternMessage cpm = new CustomPatternMessage
                {
                    EventName = eventName,
                    UserIP = userIP,
                    OperatorID = operatorID,
                    Message = message
                };
                if (ex == null)
                    logger.Error(cpm);
                else
                    logger.Error(cpm, ex);
            }
        }


        public static void Fatal(string Type, string eventName, string userIP, string operatorID, string message, Exception ex = null)

       // public static void Fatal(string eventName, string userIP, string operatorID, string message, Exception ex = null)

        {
            ILog logger = GetLogger(Type); 
            if (logger.IsInfoEnabled)
            {
                CustomPatternMessage cpm = new CustomPatternMessage
                {
                    EventName = eventName,
                    UserIP = userIP,
                    OperatorID = operatorID,
                    Message = message
                };
                if (ex == null)
                    logger.Fatal(cpm);
                else
                    logger.Fatal(cpm, ex);
            }
        }


        public static void Debug(string Type, string eventName, string userIP, string operatorID, string message, Exception ex = null)

        //public static void Debug(string eventName, string userIP, string operatorID, string message, Exception ex = null)

        {
            ILog logger = GetLogger(Type); 

            if (logger.IsInfoEnabled)
            {
                CustomPatternMessage cpm = new CustomPatternMessage
                {
                    EventName = eventName,
                    UserIP = userIP,
                    OperatorID = operatorID,
                    Message = message
                };
                if (ex == null)
                    logger.Debug(cpm);
                else
                    logger.Debug(cpm, ex);
            }
        }

    
    }
}

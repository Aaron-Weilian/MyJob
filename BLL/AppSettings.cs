using System;
using System.Configuration;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace com.portal
{

    public class AppSettings : ConfigurationSection
    {
        private static FileSystemWatcher _watcher;
        private static FileConfigurationSource _fileCfgSource = new FileConfigurationSource("PortalSchedule.config");

        static AppSettings()
        {
            _watcher = new FileSystemWatcher(AppDomain.CurrentDomain.BaseDirectory);
            _watcher.EnableRaisingEvents = true;
        }

        private static void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            //GIT.Library.Common.Logger.Write(e.Name + " changed.", Global.LogCategories.DEBUG);
        }

        public static AppSettings Settings
        {
            get
            {
                return _fileCfgSource.GetSection("AppSettings") as AppSettings;
            }
        }


        [ConfigurationProperty("InBoundPath")]
        public string InBoundPath
        {
            get
            {
                string retval = (string)this["InBoundPath"];
                if (!retval.EndsWith(@"\"))
                {
                    retval += @"\";
                }
                return retval;
            }
            set { this["InBoundPath"] = value; }
        }

        [ConfigurationProperty("OutBoundPath")]
        public string OutBoundPath
        {
            get
            {
                string retval = (string)this["OutBoundPath"];
                if (!retval.EndsWith(@"\"))
                {
                    retval += @"\";
                }
                return retval;
            }
            set { this["OutBoundPath"] = value; }
        }


        [ConfigurationProperty("UPLOADPath")]
        public string UPLOADPath
        {
            get
            {
                string retval = (string)this["UPLOADPath"];
                if (!retval.EndsWith(@"\"))
                {
                    retval += @"\";
                }
                return retval;
            }
            set { this["UPLOADPath"] = value; }
        }


        [ConfigurationProperty("BackupPath")]
        public string BackupPath
        {
            get
            {
                string retval = (string)this["BackupPath"];
                if (!retval.EndsWith(@"\"))
                {
                    retval += @"\";
                }
                return retval;
            }
            set { this["BackupPath"] = value; }
        }

        [ConfigurationProperty("INPre")]
        public string INPre
        {
            get
            {
                string retval = (string)this["INPre"];
              
                return retval;
            }
            set { this["INPre"] = value; }
        }

        [ConfigurationProperty("OUTPre")]
        public string OUTPre
        {
            get
            {
                string retval = (string)this["OUTPre"];
               
                return retval;
            }
            set { this["OUTPre"] = value; }
        }

    }
}

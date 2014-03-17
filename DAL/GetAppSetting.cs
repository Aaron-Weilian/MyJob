using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Configuration;
using Tool;

namespace com.portal.db.DAL
{
    public static class GetAppSetting
    {
            private static string Connection = null;

            public static string GetConnSetting()
            {
                string connstr = string.Empty;

                if (Connection == null)
                {

                    //Uri uriFile = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase));
                    //string FilePath = uriFile.LocalPath;
                    string FilePath = AppDomain.CurrentDomain.BaseDirectory;
                    ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap
                    {

                       ExeConfigFilename =Path.Combine(FilePath,"Web.config")
                     };
                    Configuration assemblyConfig = ConfigurationManager.OpenMappedExeConfiguration(fileMap,ConfigurationUserLevel.None);
                    if (assemblyConfig.HasFile)
                    {
                        AppSettingsSection section = (assemblyConfig.GetSection("appSettings") as AppSettingsSection);

                        string server = section.Settings["SERVER"].Value;
                        string database = section.Settings["DATABASE"].Value;
                        string uid = section.Settings["UID"].Value;
                        string password = section.Settings["PASSWORD"].Value;

                        //connstr = "Data Source=C:\\Data\\Portal.sqlite;";

                        connstr = "SERVER=" + server + ";" + "DATABASE=" +
                                  database + ";" + "UID=" + Encrypt.DecryptDES(uid, "MRABBITW") + ";" + "PASSWORD=" + Encrypt.DecryptDES(password, "MRABBITW") + ";";

                        //connstr = "UID=" + Encrypt.DecryptDES(uid,"MRABBITW") + ";" + "PASSWORD=" + Encrypt.DecryptDES(password,"MRABBITW") + "; Initial File Name=E:\\portal.mdf;";



                    }

                    Connection = connstr;
                }
                else {
                    return Connection;
                }
                return connstr;
            }

    }
}


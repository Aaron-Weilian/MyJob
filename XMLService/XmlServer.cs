using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Configuration;
using System.Reflection;
using System.Threading;  
using Tool;
using BLL = com.portal.db.BLL;
using Model = com.portal.db.Model;




namespace XMLService
{
    public partial class XmlServer : ServiceBase
    {
        public Dictionary<String, String> dic;

        BLL.Message messageServer;
        BLL.MessageBody rosMessageServer;
        //BLL.RosHeader rosHeaderMessageServer;
        //BLL.RosLines rosLineMessageServer;

        public XmlServer()
        {
            InitializeComponent();
            InitSystem();
        }

        void InitSystem(){

            dic = new Dictionary<string, string>();
            Uri uriFile = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase));
            string FilePath = uriFile.LocalPath;
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = Path.Combine(FilePath, "XMLService.exe.config")
            };
            Configuration assemblyConfig = ConfigurationManager.OpenMappedExeConfiguration(fileMap,ConfigurationUserLevel.None);

            if (assemblyConfig.HasFile)
            {
                AppSettingsSection section = (assemblyConfig.GetSection("appSettings") as AppSettingsSection);

                string domain = section.Settings["Domain"].Value;
                string userName = section.Settings["UserName"].Value;
                string password = section.Settings["Password"].Value;
                //string RemoteMappingOutRosPath = section.Settings["RemoteOutRosPath"].Value;
                //string RemoteMappingOutPOPath = section.Settings["RemoteOutPOPath"].Value;
                //string RemoteMappingInRosPath = section.Settings["RemoteInRosPath"].Value;
                //string RemoteMappingInPOPath = section.Settings["RemoteInPOPath"].Value;
                string Interval = section.Settings["Interval"].Value;

                dic.Add("Domain", domain);
                dic.Add("UserName", userName);
                dic.Add("Password", password);

                //dic.Add("RemoteOutRosPath", RemoteMappingOutRosPath);
                //dic.Add("RemoteOutPOPath", RemoteMappingOutPOPath);
                //dic.Add("RemoteInRosPath", RemoteMappingInRosPath);
                //dic.Add("RemoteInPOPath", RemoteMappingInPOPath);

                dic.Add("Interval", Interval);
            }

            //xmlServer In directory
            string inPath = FilePath + "\\IN";
            if (!Directory.Exists(inPath))
            {
                Directory.CreateDirectory(inPath);
            }
            dic.Add("inPath", inPath);

            //xmlServer In/Ros directory
            string inRosPath = FilePath + "\\IN\\Ros";
            if (!Directory.Exists(inRosPath))
            {
                Directory.CreateDirectory(inRosPath);
            }
            dic.Add("inRosPath", inRosPath);

            //xmlServer In/Po directory
            string inPOPath = FilePath + "\\IN\\PO";
            if (!Directory.Exists(inPOPath))
            {
                Directory.CreateDirectory(inPOPath);
            }
            dic.Add("inPOPath", inPOPath);

            //xmlServer In/Backup directory
            string inBackupPath = FilePath + "\\IN\\Backup";
            if (!Directory.Exists(inBackupPath))
            {
                Directory.CreateDirectory(inBackupPath);
            }
            dic.Add("inBackupPath", inBackupPath);

            //xmlServer Out directory
            string outPath = FilePath + "\\OUT";
            if (!Directory.Exists(outPath))
            {
                Directory.CreateDirectory(outPath);
            }
            dic.Add("outPath", outPath);

            //xmlServer Out/Ros directory
            string outRosPath = FilePath + "\\OUT\\Ros";
            if (!Directory.Exists(outRosPath))
            {
                Directory.CreateDirectory(outRosPath);
            }
            dic.Add("outRosPath", outRosPath);

            //xmlServer Out/PO directory
            string outPOPath = FilePath + "\\OUT\\PO";
            if (!Directory.Exists(outPOPath))
            {
                Directory.CreateDirectory(outPOPath);
            }
            dic.Add("outPOPath", outPOPath);

            //xmlServer Out/Backup directory
            string outBackupPath = FilePath + "\\OUT\\Backup";
            if (!Directory.Exists(outBackupPath))
            {
                Directory.CreateDirectory(outBackupPath);
            }
            dic.Add("outBackupPath", outBackupPath);

           

            

           

        
        }

        protected override void OnStart(string[] args)
        {

            eventLog.WriteEntry("XmlService Start \r\nTime:" + getCurrentTime(), EventLogEntryType.Information);
            
            this.scheduled.Elapsed += new System.Timers.ElapsedEventHandler(dowork);
            this.scheduled.Interval = Double.Parse(dic["Interval"]);
            this.scheduled.Enabled = true;
            this.scheduled.Start();

            

        }

        protected override void OnStop()
        {
            this.scheduled.Enabled = false;
            eventLog.WriteEntry("XmlService Stop \r\nTime:" + getCurrentTime(), EventLogEntryType.Information);
        }

        //public void  moveFiles(string srcpath,string destpath,string backuppath) {

        //    try
        //    {
        //         //DirectoryInfo dir = new DirectoryInfo("C:\\tmp\\1");
        //        DirectoryInfo dir = new DirectoryInfo(srcpath);
        //        FileInfo[] files = dir.GetFiles("*.xml");
        //        foreach (FileInfo file in files)
        //        {
        //            //from IC Client out directory to xmlServer in directory 
        //            eventLog.WriteEntry("Move Files to "+destpath+" \r\nTime:" + getCurrentTime(), EventLogEntryType.Information);
        //            if (File.Exists(destpath + "\\" + file.Name))
        //            {
        //                File.Delete(destpath + "\\" + file.Name);
        //            }

        //            file.MoveTo(destpath + "\\" + file.Name);

        //            //from xmlServer in directory to xmlServer backup directory
        //            eventLog.WriteEntry("Backup Files to "+backuppath+" \r\nTime:" + getCurrentTime(), EventLogEntryType.Information);
               
        //            file.CopyTo(backuppath + "\\" + file.Name, true);
        //        }

        //    }
        //    catch(Exception e){
        //        eventLog.WriteEntry(e.Message + "\r\nTime:" + getCurrentTime(), EventLogEntryType.Error);
        //    }

        //}

        public void readFiles(string path,string messageType) {

            eventLog.WriteEntry("Read Files From "+path+" directory \r\n begin……  \r\nTime:" + getCurrentTime(), EventLogEntryType.Information);

            string[] files = Directory.GetFiles(path, "*.xml");

            foreach (string file in files)
            {
                FileInfo info = new FileInfo(file);

                BLL.Message server = new BLL.Message();
                BLL.MessageBody message = new BLL.MessageBody();

                if (messageType == "ROS")
                {
                    Tool.Message<ROSHeader> xmlModel = XMLUtil.loadRosXML(file);
                    if (message.Exists(xmlModel.sender.key, "ROS"))
                    {
                        server.updateROSData(xmlModel, "ROS",info.Name.Substring(0,info.Name.IndexOf(".")));
                    }
                    else
                    {
                        server.saveROSData(xmlModel, "ROS", info.Name.Substring(0, info.Name.IndexOf(".")));
                    }
                    info.CopyTo(dic["inBackupPath"] + "\\" + info.Name, true);
                }
                if (messageType == "PO")
                {
                    Tool.Message<POHeader> xmlModel = XMLUtil.loadPOXML(file);

                    xmlModel.sender.messageName = info.Name.Substring(0, info.Name.IndexOf("."));

                    if (message.Exists(xmlModel.sender.key, "PO"))
                    {
                        server.updatePOData(xmlModel, "PO", info.Name.Substring(0, info.Name.IndexOf(".")));
                    }
                    else
                    {
                        server.savePOData(xmlModel, "PO", info.Name.Substring(0, info.Name.IndexOf(".")));
                    }
                    info.CopyTo(dic["inBackupPath"] + "\\" + info.Name, true);
                }

                //if (messageType == "ROS")
                //{
                //    Tool.Message<ROSHeader> xmlModel = XMLUtil.loadRosXML(file);
                //    //ServiceSoapClient webserver = new ServiceSoapClient();

                //   // Byte[] b =  ObjectBinaryFormate.ChangeObjectToBytes(xmlModel);
                //    try
                //    {
                //        if (webserver.ReceiveData(b, "ROS"))
                //        {
                //            eventLog.WriteEntry(xmlModel.sender.messageName+" have been sent to supplier \r\nTime:" + getCurrentTime(), EventLogEntryType.Information);
                //        }
                //    }
                //    catch(Exception e){
                //        eventLog.WriteEntry(e.Message+"\r\nTime:" + getCurrentTime(), EventLogEntryType.Error);
                //    }

                //    info.CopyTo(dic["inBackupPath"] + "\\" + info.Name, true);

                //}
                //if (messageType == "PO") {

                //    Tool.Message<POHeader> xmlModel = XMLUtil.loadPOXML(file);
                //    ServiceSoapClient webserver = new ServiceSoapClient();
                //    Byte[] b = ObjectBinaryFormate.ChangeObjectToBytes(xmlModel);
                //    try
                //    {
                //        if (webserver.ReceiveData(b, "PO")) {
                //            eventLog.WriteEntry(xmlModel.sender.messageName + " have been sent to supplier \r\nTime:" + getCurrentTime(), EventLogEntryType.Information);
                //        }
                //    }
                //    catch (Exception e) {
                //        eventLog.WriteEntry(e.Message + "\r\nTime:" + getCurrentTime(), EventLogEntryType.Error);
                //    }
                //    info.CopyTo(dic["inBackupPath"] + "\\" + info.Name, true);
                //}

                info.Delete();

            }
        }

        public void exportFiles() {

            messageServer = new BLL.Message();
            rosMessageServer = new BLL.MessageBody();

            string sql = " 1=1 ";

            sql += " and (MessageType = 'ROSConfirm' or MessageType = 'POConfirm')";

            sql += " and (Notes = 'IN-Process')";

            List<Model.MessageBody> list = rosMessageServer.GetModelList(sql);
            foreach (Model.MessageBody message in list)
            {
                //Model.MessageBody mb = rosMessageServer.GetModelList("ReferenceID = '" + message.segment1 + "'")[0];

                if (message.messageType == "ROSConfirm")
                {
                    //gelerate confirm xml file
                    Tool.Message<ROSHeader> model = messageServer.convert2Ros(message);
                    eventLog.WriteEntry("Gelerate Xml Files \r\nTime:" + getCurrentTime(), EventLogEntryType.Information);
                    string file = dic["outRosPath"] + "\\" + message.segment1 + ".xml";
                    XMLUtil.exportRosXml(model, file);
                    message.notes = "Success";
                    rosMessageServer.Update(message);
                    FileInfo info = new FileInfo(file);
                    info.CopyTo(dic["outBackupPath"] + "\\" + info.Name, true);

                }
                if (message.messageType == "POConfirm") {

                    Tool.Message<POHeader> model = messageServer.convert2PO(message);
                    eventLog.WriteEntry("Gelerate Xml Files \r\nTime:" + getCurrentTime(), EventLogEntryType.Information);
                    string file = dic["outPOPath"] + "\\" + message.segment1 + ".xml";
                    XMLUtil.exportPoXml(model, file);
                    message.notes = "Success";
                    rosMessageServer.Update(message);
                    FileInfo info = new FileInfo(file);
                    info.CopyTo(dic["outBackupPath"] + "\\" + info.Name, true);
                }
                //update message status
                
            
            }
            

        }

        //public void addInbox(Tool.Message<ROSHeader> xmlModel, string messageType, string filename)
        //{
        //    int count = 1;
        //    List<Model.Message> list = messageServer.GetModelList("segment1 = '"+xmlModel.sender.key+"' and messageType = '"+messageType+"'");

        //    if (list.Count > 0)
        //    {
        //        count = list.Count + 1;
        //    }

            

        //    Model.Message rosmsg = new Model.Message();

        //    rosmsg.MessageBody = filename.Split('.')[0];
        //    rosmsg.MessageType = messageType;
        //    rosmsg.MessageName = xmlModel.sender.messageName;
        //   // rosmsg.Sender = xmlModel;
        //    rosmsg.segment2 = xmlModel.partner.vender_name;
        //    rosmsg.Receiver = xmlModel.partner.vender_site;
        //    rosmsg.Status = "unread";
        //    rosmsg.FlowPoint = "INXML";
        //    rosmsg.segment1 = xmlModel.sender.key;
        //    rosmsg.Created = DateTime.Parse(xmlModel.sender.creationDateTime);
        //    rosmsg.Updated = DateTime.Parse(xmlModel.sender.creationDateTime);
        //    rosmsg.Count = count.ToString();
        //    messageServer.Add(rosmsg);
        //}


        public void dowork(object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                eventLog.WriteEntry("XmlService Working…… \r\nTime:" + getCurrentTime(), EventLogEntryType.Information);
                //Thread Thread1 = new Thread(new ThreadStart(send));
                //Thread1.Start();
                
                //Thread Thread3 = new Thread(new ThreadStart(receive));
                //Thread3.Start();
                readFiles(dic["inRosPath"], "ROS");
                readFiles(dic["inPOPath"], "PO");
                exportFiles();

            }
            catch (Exception err)
            {
                eventLog.WriteEntry(err.Message+" \r\nTime:" + getCurrentTime(), EventLogEntryType.Error);
            }
        }

        //public void send() {
        //    try
        //    {
        //        //using (new IdentityScope(dic["Domain"], dic["UserName"], dic["Password"], LogonType.NewCredentials, LogonProvider.WinNT50))
        //        //{
        //            //eventLog.WriteEntry("move files to xmlServer" + " \r\nTime:" + getCurrentTime(), EventLogEntryType.Information);
        //            //moveFiles(dic["RemoteOutRosPath"], dic["inRosPath"], dic["inBackupPath"]);

        //            eventLog.WriteEntry("read files into DB" + " \r\nTime:" + getCurrentTime(), EventLogEntryType.Information);
        //            readFiles(dic["inRosPath"], "ROS");
        //            readFiles(dic["inPOPath"], "PO");
        //        //}

        //    }
        //    catch (Exception e)
        //    {
        //        eventLog.WriteEntry(e.Message + " \r\nTime:" + getCurrentTime(), EventLogEntryType.Error);
        //    }
            
        //}


        //public void receive() {

        //    ServiceSoapClient webserver = new ServiceSoapClient();
        //    if (webserver.IsUpload())
        //    {
        //        List<object> list = (List<object>)ObjectBinaryFormate.ChangeBytesToObject(webserver.SendData());
        //        for (int i = 0; i < list.Count; i++) {
        //            Dictionary<string, object> m = (Dictionary<string, object>)list[i];

        //            foreach (string str in m.Keys)
        //            {
        //                if (str == "ROSConfirm")
        //                {
        //                    object o = m["ROSConfirm"];
        //                    Message<ROSHeader> ROS = (Message<ROSHeader>)o;

        //                    XMLUtil.exportRosXml(ROS, dic["outRosPath"] + "\\" + ROS.sender.messageName + ".xml");
        //                }
        //                if (str == "POConfirm")
        //                {
        //                    object o = m["POConfirm"];
        //                    Message<POHeader> PO = (Message<POHeader>)o;

        //                    XMLUtil.exportPoXml(PO, dic["outRosPath"] + "\\" + PO.sender.messageName + ".xml");
        //                }


        //            }
        //        }
        //    }

        //}

       

        public string getCurrentTime(){
              return System.DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
        }
    }
}

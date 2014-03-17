using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using com.portal.db.DAL;
using Microsoft.ApplicationBlocks.Data;
using Quartz;
using System.Xml;
using System.Xml.XPath;
using Tool;
using com.portal.db.BLL.NROS;
using com.portal.db.BLL.PO;

namespace com.portal
{
    public interface IJobSetup
    {
        void Setup(IScheduler scheduler);
    }

    public class InBoundFilesJob : IJob, IJobSetup
    {
        public void Execute(IJobExecutionContext context)
        {
            string originalFilename = Directory.GetFiles(AppSettings.Settings.InBoundPath, "*.xml").FirstOrDefault();
            if (string.IsNullOrEmpty(originalFilename))
                return;


            Log.LogHelper.Info("InBoundFilesJob", "PortalService", "Server", "System", "inbound file name:" + originalFilename);
            string backupFileName = "";


            string fn = Path.GetFileName(originalFilename);

            string downTemp = AppSettings.Settings.UPLOADPath + "downTemp\\";
            string downTemplate = AppSettings.Settings.UPLOADPath + "downTemplate\\";
            string tempFileName = AppSettings.Settings.UPLOADPath + "temp\\" + fn;
            string[] splitfilename = fn.Split('_');
            string procFilename = "";
            string processFilename = "";


            System.Threading.Mutex fileMutex = null;
            bool checkfile = false;
            FileStream fs = null;
            StreamReader m_streamReader = null;

            try
            {
                bool createNew;
                //byte[] array = new byte[22];
                using (fileMutex = new System.Threading.Mutex(true, fn, out createNew))
                {
                    fileMutex.WaitOne(600, true); //如果这个锁存在，则等待。保证一个线程读一个文件。
                    // 读取文件，判定文件长度不满21个字符串  或者 内熔最后 21 个字符串是不是 </BMC_PORTAL_MESSAGE>  则退出，不读取此文件。
                    if (createNew)
                    {
                        FileInfo fi = new FileInfo(originalFilename);
                        string str = "</BMC_PORTAL_MESSAGE>";
                        using (fs = fi.Open(FileMode.Open, FileAccess.Read))
                        {
                            using (m_streamReader = new StreamReader(fs))
                            {
                                if (m_streamReader.BaseStream.Length < str.Length) return;
                                //m_streamReader.BaseStream.Seek(m_streamReader.BaseStream.Length - str.Length, SeekOrigin.Begin);
                                string strLine = m_streamReader.ReadLine();

                                while (!m_streamReader.EndOfStream)
                                {
                                    strLine += m_streamReader.ReadLine();

                                }

                                if (strLine.Trim().ToUpper().IndexOf(str) == -1) return;
                            }
                        }

                        try
                        {
                            backupFileName = Utils.BackupFile(originalFilename, AppSettings.Settings.BackupPath + @"IN\");
                        }
                        catch (Exception ex)
                        {
                            Log.LogHelper.Info("InBoundFilesJob", "PortalService", "Server", "System", "backup file fail :" + backupFileName, ex);
                        }

                        if (splitfilename.Length < 4)
                        {
                            Log.LogHelper.Error("InBoundFilesJob", "PortalService", "Server", "System", "Error inbound file name:" + originalFilename);
                            return;
                        }

                        try
                        {
                            File.Move(originalFilename, tempFileName);
                        }
                        catch (Exception ex)
                        {
                            Log.LogHelper.Info("InBoundFilesJob", "PortalService", "Server", "System", "move temp file name:" + originalFilename + " fail", ex);
                            return;
                        }

                        procFilename = originalFilename.ToLower().Replace(".xml", ".pro");

                        if (File.Exists(procFilename)) return;

                        try
                        {
                            File.Move(tempFileName, procFilename);
                        }
                        catch
                        {
                            Log.LogHelper.Info("InBoundFilesJob", "PortalService", "Server", "System", "Move file error, inbound file name:" + originalFilename);
                            return;
                        }

                        processFilename = Utils.GetProcessDir() + Guid.NewGuid().ToString("N") + ".pro";
                        try
                        {
                            if (File.Exists(processFilename))
                                File.Delete(processFilename);
                            File.Move(procFilename, processFilename);
                        }
                        catch (Exception ex)
                        {
                            Log.LogHelper.Error("InBoundFilesJob", "PortalService", "Server", "System", "BackupPath file error", ex);
                            return;
                        }

                        checkfile = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogHelper.Error("InBoundFilesJob", "PortalService", "Server", "System", "check file:" + originalFilename, ex);
            }

            if (checkfile)
            {
                string fileType = splitfilename[3].ToUpper();
                try
                {
                    com.portal.db.BLL.MessageBody mbServer = new com.portal.db.BLL.MessageBody();
                    com.portal.db.Model.MessageBody mes = new db.Model.MessageBody();
                    string excelname = Guid.NewGuid().ToString("N");


                    switch (fileType)
                    {
                        case "PO":
                            Message<Tool.POHeader> poModel = null;
                            try
                            {
                                poModel = XMLUtil.loadPOXML(processFilename);
                            }
                            catch (Exception ex)
                            {
                                Log.LogHelper.Error("InBoundFilesJob", "PortalService", "Server", "PO",
                                    string.Format("Read {0} file exception, error formant {1}", fileType, originalFilename), ex);
                                return;
                            }

                            if (poModel != null)
                            {
                                try
                                {
                                    FileUtil.exportExcel2007forPO(poModel, downTemp + "\\" + excelname + ".xlsm", downTemplate + "POFormat.xlsm", fn.Substring(17, fn.Length - 21));
                                }
                                catch (Exception ex)
                                {
                                    Log.LogHelper.Error("InBoundFilesJob", "PortalService", "Server", "PO",
                                        string.Format("Make excel file exception,xml filename:{0}", fn), ex);
                                    return;
                                }
                                try
                                {
                                    var poo = new poServer();
                                    var message = new com.portal.db.BLL.MessageBody();
                                    //if (message.Exists(poModel.sender.key, "PO"))
                                    //{
                                    //    //mes = poo.updatePOData(poModel, "PO", fn.Substring(17, fn.Length - 21));
                                    //    mes = poo.updatePOData(poModel, "PO", excelname);
                                    //    Log.LogHelper.Info("InBoundFilesJob", "PortalService", "Server", "PO", string.Format("Update {0} message {1}, file:{2}", fileType, poModel.sender.messageName, backupFileName));
                                    //}
                                    //else
                                    //{
                                    //mes = poo.savePOData(poModel, "PO", fn.Substring(17, fn.Length - 21));
                                    mes = poo.savePOData(poModel, "PO", fn.Substring(17, fn.Length - 21), excelname);
                                    Log.LogHelper.Info("InBoundFilesJob", "PortalService", "Server", "PO", string.Format("Insert {0} message {1}, file:{2}", fileType, poModel.sender.messageName, backupFileName));
                                    //}
                                }
                                catch (Exception ex)
                                {
                                    Log.LogHelper.Error("InBoundFilesJob", "PortalService", "Server", "PO",
                                        "Update portal db error.", ex);
                                    return;
                                }
                            }
                            break;
                        case "NROS":
                            Message<Tool.ROSHeader> rosModel = null;
                            try
                            {
                                rosModel = XMLUtil.loadRosXML(processFilename);
                            }
                            catch (Exception ex)
                            {
                                Log.LogHelper.Error("InBoundFilesJob", "PortalService", "Server", "NROS",
                                    string.Format("Read {0} file exception, error formant {1}", fileType, originalFilename), ex);
                                return;
                            }

                            if (rosModel != null)
                            {

                                try
                                {
                                    FileUtil.exportExcel2007forRos(rosModel, downTemp + "\\" + excelname + ".xlsx", downTemplate + "ROSFormat.xlsx", fn.Substring(17, fn.Length - 21), "Supplier");
                                }
                                catch (Exception ex)
                                {

                                    Log.LogHelper.Error("InBoundFilesJob", "PortalService", "Server", "ROS",
                                            string.Format("Make excel file exception,xml filename:{0}", fn), ex);
                                    return;
                                }
                                try
                                {
                                    var nros = new nrosServer();
                                    var message = new com.portal.db.BLL.MessageBody();
                                    //if (message.Exists(rosModel.sender.key, "NROS"))
                                    //{

                                    //    //mes = nros.updateROSData(rosModel, "NROS", "IN", fn.Substring(17, fn.Length - 21));
                                    //    mes = nros.updateROSData(rosModel, "NROS", "IN", excelname);
                                    //    Log.LogHelper.Info("InBoundFilesJob", "PortalService", "Server", "NROS", string.Format("Update NROS message {0}, file:{1}", rosModel.sender.messageName, backupFileName));
                                    //}
                                    //else
                                    //{
                                    //mes = nros.saveROSData(rosModel, "NROS", "IN", fn.Substring(17, fn.Length - 21));
                                    mes = nros.saveROSData(rosModel, "NROS", "IN", fn.Substring(17, fn.Length - 21), excelname);
                                    Log.LogHelper.Info("InBoundFilesJob", "PortalService", "Server", "NROS", string.Format("Insert NROS message {0}, file:{1}", rosModel.sender.messageName, originalFilename));
                                    //}
                                }
                                catch (Exception ex)
                                {
                                    Log.LogHelper.Error("InBoundFilesJob", "PortalService", "Server", "NROS",
                                        "Update portal db error.", ex);
                                    return;
                                }
                            }
                            break;
                        default:
                            Log.LogHelper.Warn("InBoundFilesJob", "PortalService", "Server", "System",
                                "Type error, inbound file name:" + originalFilename);
                            return;
                    }



                    mes.segment4 = "T";
                    mbServer.Update(mes);
                }
                catch (Exception ex)
                {
                    Log.LogHelper.Error("InBoundFilesJob", "PortalService", "Server", fileType,
                        string.Format("Process {0} file error.", fileType), ex);
                }
                finally
                {
                    try
                    {
                        if (File.Exists(processFilename))
                            File.Delete(processFilename);
                    }
                    catch (Exception ex)
                    {
                        Log.LogHelper.Error("InBoundFilesJob", "PortalService", "Server", "System",
                            "Delete files IO error.", ex);
                    }
                }
            }
        }

        public void Setup(IScheduler scheduler)
        {
            var job = JobBuilder.Create<InBoundFilesJob>()
                .WithIdentity("InBound", "XML")
                .Build();

            var trigger = TriggerBuilder.Create()
                    .WithIdentity("InBound", "XML")
                    .StartNow()
                    .WithCronSchedule("/1 * * ? * *")
                    .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }

    public class OutBoundFilesJob : IJob, IJobSetup
    {
        public void Execute(IJobExecutionContext context)
        {

            var messageServer = new db.BLL.MessageBody();
            //string sql = "Update Message Set Status = 'Working' output deleted.* Where Status = 'Pending' and Notes = 'IN-Process' and segment2='OUT' and segment4 = 'T' ";
            string sql =
                "Update Message Set Status = 'Working' output deleted.* Where messageID = (select top 1 m.messageID from Message m Where m.Status = 'Pending' and m.Notes = 'IN-Process' and m.segment2='OUT' and m.segment4 = 'T' order by m.confirmDateTime)";
            List<db.Model.MessageBody> messages;



            try
            {
                messages = messageServer.GetOutBoundMessages(sql);
            }
            catch (Exception ex)
            {
                Log.LogHelper.Error("OutBoundFilesJob", "PortalSystem", "Server", "System", "Get messages fail !", ex);
                return;
            }

            db.Model.MessageBody message = messages.FirstOrDefault();


            if (message == null)
                return;

            Log.LogHelper.Info("OutBoundFilesJob", "PortalSystem", "Server", "System", string.Format("Get messages {0}!", message.messageName));

            var messageType = message.messageType.ToUpper();
            string fn = AppSettings.Settings.OUTPre + message.segment1 + ".xml";
            string fnwithoutname = AppSettings.Settings.OUTPre + message.segment1;
            string backupFileName = string.Empty;
            string processFilename = Utils.GetProcessDir() + fn;


            try
            {
                switch (messageType)
                {
                    case "POCONFIRM":
                        var po = new poServer();
                        Message<Tool.POHeader> poModel = null;
                        try
                        {
                            poModel = po.convert2PO(message);
                        }
                        catch (Exception ex)
                        {
                            Log.LogHelper.Error("OutBoundFilesJob", "PortalSystem", "Server", "System",
                                "convert2PO fail !", ex);
                            SqlHelper.ExecuteNonQuery(GetAppSetting.GetConnSetting(), CommandType.Text, string.Format("Update Message set Status = 'Error' Where MessageID='{0}'", message.messageID));
                        }

                        if (poModel != null)
                        {


                            try
                            {
                                XMLUtil.exportPoXml(poModel, processFilename);

                                backupFileName = Utils.BackupFile(processFilename,
                                    AppSettings.Settings.BackupPath + @"OUT\");


                                FileInfo fileInfo = new FileInfo(processFilename);

                                int i = 1;
                                //string[] str = fnwithoutname.Split('_');
                                while (File.Exists(AppSettings.Settings.OutBoundPath + fn))
                                {


                                    //str[str.Length - 1] = (str[str.Length - 1] +"."+ i).ToString();

                                    fn = fileInfo.Name.Replace(fileInfo.Extension,
                                                                               "." + i.ToString() + fileInfo.Extension);
                                    //fn = Utils.getStr(str) + ".xml";
                                    i++;
                                }

                                File.Move(processFilename, AppSettings.Settings.OutBoundPath + fn);
                                message.status = "OK";
                                message.notes = "Success";
                                messageServer.Update(message);
                            }
                            catch (Exception err)
                            {
                                Log.LogHelper.Error("OutBoundFilesJob", "PortalSystem", "Server", "System",
                                    string.Format("export {0} Xml fail !", messageType), err);
                                SqlHelper.ExecuteNonQuery(GetAppSetting.GetConnSetting(), CommandType.Text, string.Format("Update Message set Status = 'Pending' Where MessageID='{0}'", message.messageID));
                            }
                        }
                        break;
                    case "ROSCONFIRM":
                        Tool.Message<Tool.ROSHeader> model = null;
                        var nros = new nrosServer();
                        try
                        {
                            model = nros.convert2Ros(message);
                        }
                        catch (Exception err)
                        {
                            Log.LogHelper.Error("OutBoundFilesJob", "PortalSystem", "Server", "System",
                                "convert2Ros fail !", err);
                            SqlHelper.ExecuteNonQuery(GetAppSetting.GetConnSetting(), CommandType.Text, string.Format("Update Message set Status = 'Error' Where MessageID='{0}'", message.messageID));
                        }
                        if (model != null)
                        {



                            try
                            {
                                bool flag = false;

                                foreach (Tool.ROSHeader header in model.headers)
                                {
                                    if (header.updateflag == "" || header.updateflag == null) header.updateflag = "N";

                                    if (header.updateflag.ToUpper() == "Y")
                                    {
                                        flag = true;
                                    }

                                }

                                if (flag)
                                {
                                    XMLUtil.exportRosXml(model, processFilename);
                                    backupFileName = Utils.BackupFile(processFilename,
                                        AppSettings.Settings.BackupPath + @"OUT\");

                                    FileInfo fileInfo = new FileInfo(processFilename);

                                    //int i = 1;
                                    //while (File.Exists(AppSettings.Settings.OutBoundPath + fn))
                                    //{
                                    //    fn = fileInfo.Name.Replace(fileInfo.Extension,
                                    //                                               "_" + i.ToString() + fileInfo.Extension);
                                    //    i++;
                                    //}

                                    int i = 1;
                                    //string[] str = fnwithoutname.Split('_');
                                    while (File.Exists(AppSettings.Settings.OutBoundPath + fn))
                                    {


                                        //str[str.Length - 1] = (str[str.Length - 1] +"."+ i).ToString();

                                        fn = fileInfo.Name.Replace(fileInfo.Extension,
                                                                                   "." + i.ToString() + fileInfo.Extension);
                                        //fn = Utils.getStr(str) + ".xml";
                                        i++;
                                    }



                                    File.Move(processFilename, AppSettings.Settings.OutBoundPath + fn);
                                }
                                //else
                                //{
                                //    SqlHelper.ExecuteNonQuery(GetAppSetting.GetConnSetting(), CommandType.Text, string.Format("Update Message set Status = 'Pending' Where MessageID='0'", message.messageID));
                                //}
                                message.status = "OK";
                                message.notes = "Success";
                                messageServer.Update(message);

                            }
                            catch (Exception err)
                            {
                                Log.LogHelper.Error("OutBoundFilesJob", "PortalSystem", "Server", "System",
                                    string.Format("export {0} Xml fail !", messageType), err);
                                SqlHelper.ExecuteNonQuery(GetAppSetting.GetConnSetting(), CommandType.Text, string.Format("Update Message set Status = 'Error' Where MessageID='{0}'", message.messageID));
                            }
                        }
                        break;
                    default:
                        Log.LogHelper.Warn("OutBoundFilesJob", "PortalSystem", "Server", "System",
                            string.Format("Unknown {0} message type!", messageType));
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.LogHelper.Error("OutBoundFilesJob", "PortalSystem", "Server", "System",
                               "System fail !", ex);
                SqlHelper.ExecuteNonQuery(GetAppSetting.GetConnSetting(), CommandType.Text, string.Format("Update Message set Status = 'Error' Where MessageID='{0}'", message.messageID));
            }
            finally
            {
                try
                {
                    if (File.Exists(processFilename))
                        File.Delete(processFilename);
                }
                catch (Exception err)
                {
                    Log.LogHelper.Error("OutBoundFilesJob", "PortalService", "Server", "System", "Delete files IO error.", err);
                }
            }

        }

        public void Setup(IScheduler scheduler)
        {
            var job = JobBuilder.Create<OutBoundFilesJob>()
                .WithIdentity("OutBound", "XML")
                .Build();

            var trigger = TriggerBuilder.Create()
                    .WithIdentity("OutBound", "XML")
                    .StartNow()
                    .WithCronSchedule("/1 * * ? * *")
                //.WithCronSchedule("0 50 23 ? * *")
                    .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }

    //public class UploadFilesJob : IJob, IJobSetup
    //{
    //    public void Execute(IJobExecutionContext context)
    //    {

    //        foreach(string dir in Directory.GetFiles(AppSettings.Settings.UPLOADPath, "*.Excel")){

    //            string procFilename = dir.ToLower().Replace(".excel", ".pro");

    //            File.Move(dir, procFilename);

    //            string fn = Path.GetFileName(procFilename);

    //            MessageBody messageServer = new MessageBody();
    //            poServer poo = new poServer();
    //            nrosServer ros = new nrosServer();

    //            string[] splitfilename = fn.Split('_');
    //            if (splitfilename.Length < 4)
    //            {
    //                Log.LogHelper.Warn("InBoundPOFilesJob",
    //                    "PortalService",
    //                    "Server",
    //                    "System",
    //                    "Error inbound file name:" + Path.GetFileName(dir));

    //                if (File.Exists(procFilename))
    //                    File.Delete(procFilename);

    //                return;
    //            }

    //            if (splitfilename[0] == "po")
    //            {
    //                Message<Tool.POHeader> po = FileUtil.readPOExcel2007(procFilename, "PO");

    //                poo.mergePOData(po, "POConfirm", po.filename);

    //                Log.LogHelper.Info("UploadFilesJob", "Upload PO File", "Server", "System", string.Format("Upload PO {0}", Path.GetFileName(dir)));


    //            }
    //            if (splitfilename[0] == "nros") {

    //                Message<Tool.ROSHeader> ro = FileUtil.readROSExcel2007(procFilename, "ROS");

    //                ros.mergeROSData(ro, "ROSConfirm", "OUT", ro.filename);

    //                 Log.LogHelper.Info("UploadFilesJob", "Upload ROS File", "Server", "System", string.Format("Upload ROS {0}", Path.GetFileName(dir)));

    //            }


    //            if (File.Exists(procFilename))
    //                File.Delete(procFilename);


    //        }


    //    }

    //    public void Setup(IScheduler scheduler)
    //    {
    //        var job = JobBuilder.Create<UploadFilesJob>()
    //            .WithIdentity("UploadFile", "UPLOADBound")
    //            .Build();

    //        var trigger = TriggerBuilder.Create()
    //                .WithIdentity("UploadFileTrigger", "UPLOADBound")
    //                .StartNow()
    //                .WithCronSchedule("/30 * * ? * *")
    //                .Build();

    //        scheduler.ScheduleJob(job, trigger);
    //    }
    //}


    public class UploadFilesJob : IJob, IJobSetup
    {
        public void Execute(IJobExecutionContext context)
        {

            string sql = "Update UploadFileList Set Status = 'Working' output deleted.UploadFileListID,deleted.FileName,deleted.FilePath,deleted.FileStream,deleted.MessageType,deleted.Status,deleted.UploadBy,deleted.ConfirmDate where UploadFileListID = (select top 1 UploadFileListID from UploadFileList Where Status = 'IN-Process' order by ConfirmDate)";

            com.portal.db.BLL.UploadFileList mServer = new com.portal.db.BLL.UploadFileList();
            //string sql = "Status = 'IN-Process'" ;

            var model = mServer.GetUploadMessages(sql).FirstOrDefault();
            if (model == null)
                return;

            string excelname = Guid.NewGuid().ToString("N");
            string downTemp = AppSettings.Settings.UPLOADPath + "downTemp\\";
            string downTemplate = AppSettings.Settings.UPLOADPath + "downTemplate\\";

            poServer poo = new poServer();
            nrosServer ros = new nrosServer();
            com.portal.db.BLL.MessageBody mbServer = new com.portal.db.BLL.MessageBody();
            using (MemoryStream ms = new MemoryStream(model.FileStream))
            {
                com.portal.db.Model.MessageBody mes = new db.Model.MessageBody();

                if (model.MessageType == "POConfirm")
                {

                    Message<Tool.POHeader> po = new Message<Tool.POHeader>();

                    try
                    {
                        po = FileUtil.readPOExcel2007(ms, "PO");
                    }
                    catch (Exception ex)
                    {

                        model.Status = "Error";
                        mServer.Update(model);
                        Log.LogHelper.Error("UploadFilesJob", "Upload PO File", "Server", "PO", string.Format("Analysis PO {0} Fail", model.FileName), ex);
                        return;
                    }

                    try
                    {
                        FileUtil.exportExcel2007forPO(po, downTemp + "\\buyer\\" + excelname + ".xlsm", downTemplate + "POFormat.xlsm", po.sender.messageName);
                    }
                    catch (Exception ex)
                    {
                        model.Status = "Error";
                        mServer.Update(model);
                        Log.LogHelper.Error("UploadFilesJob", "PortalService", "Server", "PO",
                            string.Format("Make excel file exception,xml filename:{0}", po.sender.messageName), ex);
                        return;
                    }

                    string sqlCheck = "select count(1) from Message  where segment2='OUT' and [key]='{0}'";
                    object retval = SqlHelper.ExecuteScalar(GetAppSetting.GetConnSetting(), CommandType.Text, string.Format(sqlCheck, po.sender.key));
                    if (Convert.ToInt32(retval) == 0)
                    {
                        try
                        {
                            mes = poo.savePOData(po, "POConfirm", po.filename, excelname);
                        }
                        catch (Exception ex)
                        {

                            model.Status = "Error";
                            mServer.Update(model);
                            Log.LogHelper.Error("UploadFilesJob", "Upload PO File", "Server", "PO", string.Format("Update PO Data {0} Fail", model.FileName), ex);
                            return;
                        }

                    }
                    else
                    {
                        //查询message表中记录是否存在(状态为Pending/Error/OK的都将被处理)
                        sqlCheck = "Update Message set status='Uploading' output deleted.* where status<>'Working' and status<>'Uploading' and segment2='OUT' and [key] = '" + po.sender.key + "'";
                        var list = mbServer.GetOutBoundMessages(sqlCheck);
                        if (list.Count == 0)
                        {
                            model.Status = "IN-Process";
                            mServer.Update(model);
                            return;
                        }

                        try
                        {
                            mes = poo.updatePOData(po, "POConfirm", po.sender.key);
                        }
                        catch (Exception ex)
                        {

                            model.Status = "Error";
                            mServer.Update(model);
                            list[0].status = "Pending";
                            mbServer.Update(list[0]);
                            Log.LogHelper.Error("UploadFilesJob", "Upload PO File", "Server", "PO", string.Format("Update PO Data {0} Fail", model.FileName), ex);
                            return;
                        }

                    }
                    Log.LogHelper.Info("UploadFilesJob", "Upload PO File", "Server", "PO", string.Format("Upload PO {0}", model.FileName));
                }

                if (model.MessageType == "ROSConfirm")
                {
                    Message<Tool.ROSHeader> ro = new Message<ROSHeader>();

                    try
                    {
                        ro = FileUtil.readROSExcel2007(ms, "ROS");
                    }
                    catch (Exception ex)
                    {
                        model.Status = "Error";
                        mServer.Update(model);
                        Log.LogHelper.Error("UploadFilesJob", "Upload ROS File", "Server", "ROS", string.Format("Analysiss ROS {0} Fail", model.FileName), ex);
                        return;
                    }

                    try
                    {
                        FileUtil.exportExcel2007forRos(ro, downTemp + "\\buyer\\" + excelname + ".xlsx", downTemplate + "ROSFormat.xlsx", ro.sender.messageName, "Buyer");
                    }
                    catch (Exception ex)
                    {
                        model.Status = "Error";
                        mServer.Update(model);

                        Log.LogHelper.Error("UploadFilesJob", "PortalService", "Server", "ROS",
                                string.Format("Make excel file exception,xml filename:{0}", ro.sender.messageName), ex);
                        return;
                    }

                    string sqlCheck = "select count(1) from Message where segment2='OUT' and [key]='{0}'";
                    object retval = SqlHelper.ExecuteScalar(GetAppSetting.GetConnSetting(), CommandType.Text, string.Format(sqlCheck, ro.sender.key));
                    if (Convert.ToInt32(retval) == 0)
                    {
                        try
                        {
                            mes = ros.saveROSData(ro, "ROSConfirm", "OUT", ro.filename, excelname);
                        }
                        catch (Exception ex)
                        {

                            model.Status = "Error";
                            mServer.Update(model);
                            Log.LogHelper.Error("UploadFilesJob", "Upload ROS File", "Server", "ROS", string.Format("Update ROS Data {0} Fail", model.FileName), ex);
                            return;
                        }
                    }
                    else
                    {
                        //查询message表中记录是否存在(状态为Pending/Error/OK的都将被处理)
                        sqlCheck = "Update Message set status='Uploading' output deleted.* where status<>'Working' and status<>'Uploading' and segment2='OUT' and [key] = '" + ro.sender.key + "'";
                        var list = mbServer.GetOutBoundMessages(sqlCheck);
                        if (list.Count == 0)
                        {
                            model.Status = "Error";
                            mServer.Update(model);
                            return;
                        }
                        try
                        {
                            mes = ros.updateROSData(ro, "ROSConfirm", "OUT", ro.sender.key);
                        }
                        catch (Exception ex)
                        {

                            model.Status = "Error";
                            mServer.Update(model);
                            list[0].status = "Pending";
                            mbServer.Update(list[0]);
                            Log.LogHelper.Error("UploadFilesJob", "Upload ROS File", "Server", "ROS", string.Format("Update ROS Data {0} Fail", model.FileName), ex);
                            return;
                        }
                    }
                }
                model.Status = "Success";
                mServer.Update(model);

                mes.segment4 = "T";
                mbServer.Update(mes);

                Log.LogHelper.Info("UploadFilesJob", "Upload PO File", "Server", "PO", string.Format("Upload PO {0}", model.FileName));

            }
        }

        public void Setup(IScheduler scheduler)
        {
            var job = JobBuilder.Create<UploadFilesJob>()
                .WithIdentity("UploadFile", "UPLOADBound")
                .Build();

            var trigger = TriggerBuilder.Create()
                    .WithIdentity("UploadFileTrigger", "UPLOADBound")
                    .StartNow()
                    .WithCronSchedule("/1 * * ? * *")
                //.WithCronSchedule("0 50 23 ? * *")
                    .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }

    public class PurgeDataJob : IJob, IJobSetup
    {
        private readonly com.portal.db.DAL.CommomDAL dal = new com.portal.db.DAL.CommomDAL();

        public void Execute(IJobExecutionContext context)
        {

            try
            {
                Log.LogHelper.Info("PurgeDataJob", "PortalSystem", "Server", "System", "Purge database data …… !");

                dal.NonQueryByStoredProcedure("Message_Data_Purge");
            }
            catch (Exception err)
            {
                Log.LogHelper.Error("PurgeDataJob", "PortalService", "Server", "System",
                                                "Purge database data error.",
                                                err);
            }


        }

        public void Setup(IScheduler scheduler)
        {

            DateTime targetTime = DateTime.Now.AddMinutes(2);//year,month,day,hour,minute,second
            //DateTime targetTime = new DateTime(2014, 1, 1, 0, 53, 00);//year,month,day,hour,minute,second
            DateTimeOffset dto = new DateTimeOffset(targetTime,
            TimeZoneInfo.Local.GetUtcOffset(targetTime));


            var job = JobBuilder.Create<PurgeDataJob>()
                .WithIdentity("PurgeData", "Purge")
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("PurgeDataTrigger", "Purge")
                .StartAt(dto)
                .WithCronSchedule("30 1 23 1 1 ?")//second,miunte,hour,day(month),month,day(week),year
                .Build();
            scheduler.ScheduleJob(job, trigger);
        }

    }

    public class PurgeBackupFileJob : IJob, IJobSetup
    {
        public void Execute(IJobExecutionContext context)
        {
            Log.LogHelper.Info("PurgeBackupFileJob", "PortalSystem", "Server", "System", "Purge backup in file …… !");

            int year = DateTime.Now.Year;
            //string forder = (year - 1).ToString();

            string forder = (year).ToString();

            try
            {
                if (Directory.Exists(AppSettings.Settings.BackupPath + @"IN\" + forder))
                {
                    Tool.ObjectBinaryFormate.CompressDirectory(
                                        AppSettings.Settings.BackupPath + @"IN\" + forder,
                                        AppSettings.Settings.BackupPath + @"IN\" + forder + ".zip",
                                        9,
                                        4096);
                }
            }
            catch (Exception err)
            {
                Log.LogHelper.Error("PurgeBackupFileJob", "PortalService", "Server", "System",
                                                "Compress backup files error.",
                                                err);
            }
            try
            {
                Utils.DeleteFile(AppSettings.Settings.BackupPath + @"IN\" + forder);

                //Directory.Delete(AppSettings.Settings.BackupPath + @"IN\" + forder);
            }
            catch (Exception err)
            {
                Log.LogHelper.Error("PurgeBackupFileJob", "PortalService", "Server", "System",
                                            "Delete backup files IO error.",
                                            err);
            }

            Log.LogHelper.Info("PurgeBackupFileJob", "PortalSystem", "Server", "System", "Purge backup out file …… !");

            try
            {
                if (Directory.Exists(AppSettings.Settings.BackupPath + @"OUT\" + forder))
                {
                    Tool.ObjectBinaryFormate.CompressDirectory(
                                        AppSettings.Settings.BackupPath + @"OUT\" + forder,
                                        AppSettings.Settings.BackupPath + @"OUT\" + forder + ".zip",
                                        9,
                                        4096);
                }
            }
            catch (Exception err)
            {
                Log.LogHelper.Error("PurgeBackupFileJob", "PortalService", "Server", "System",
                                                "Compress backup files error.",
                                                err);
            }
            try
            {
                Utils.DeleteFile(AppSettings.Settings.BackupPath + @"OUT\" + forder);

                //Directory.Delete(AppSettings.Settings.BackupPath + @"OUT\" + forder);
            }
            catch (Exception err)
            {
                Log.LogHelper.Error("PurgeBackupFileJob", "PortalService", "Server", "System",
                                            "Delete backup files IO error.",
                                            err);
            }

        }

        public void Setup(IScheduler scheduler)
        {
            DateTime targetTime = new DateTime(2013, 12, 25, 15, 53, 00);//year,month,day,hour,minute,second
            DateTimeOffset dto = new DateTimeOffset(targetTime,
            TimeZoneInfo.Local.GetUtcOffset(targetTime));

            var job = JobBuilder.Create<PurgeBackupFileJob>()
                .WithIdentity("PurgeBackupFile", "Purge")
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("PurgeBackupFileTrigger", "Purge")
                // .StartAt(dto)
                .StartNow()
                .WithCronSchedule("50 0 00 1 1 ?")//second,miunte,hour,day(month),month,day(week),year
                .Build();
            scheduler.ScheduleJob(job, trigger);
        }

    }

    public static class Utils
    {
        public static string BackupFile(string originalFileFullName, string backupFilePath)
        {
            FileInfo fileInfo = new FileInfo(originalFileFullName);
            if (fileInfo.Exists)
            {
                if (!backupFilePath.EndsWith(@"\"))
                {
                    backupFilePath += @"\";
                }
                if (!Directory.Exists(backupFilePath))
                {
                    Directory.CreateDirectory(backupFilePath);
                }
                backupFilePath += DateTime.Now.Year.ToString() + @"\";
                if (!Directory.Exists(backupFilePath))
                {
                    Directory.CreateDirectory(backupFilePath);
                }
                backupFilePath += DateTime.Now.Month.ToString("00") + @"\";
                if (!Directory.Exists(backupFilePath))
                {
                    Directory.CreateDirectory(backupFilePath);
                }
                var backupFileFullName = backupFilePath + fileInfo.Name;
                int i = 1;
                while (File.Exists(backupFileFullName))
                {
                    backupFileFullName = backupFilePath +
                                         fileInfo.Name.Replace(fileInfo.Extension,
                                                               "_" + i.ToString() + fileInfo.Extension);
                    i++;
                }
                File.Copy(fileInfo.FullName, backupFileFullName, true);
                return backupFileFullName;
            }

            //if (fileInfo != null) { 

            //    fileInfo.
            //}

            return string.Empty;
        }

        public static string GetProcessDir()
        {
            string retval = AppDomain.CurrentDomain.BaseDirectory;
            if (!retval.EndsWith(@"\"))
            {
                retval += @"\";
            }
            retval += "Process";
            if (!Directory.Exists(retval))
            {
                Directory.CreateDirectory(retval);
            }
            return retval + @"\";
        }

        public static void DeleteFile(string filePath)
        {

            if (Directory.Exists(filePath))
            {

                foreach (string file in Directory.GetFiles(filePath, ".xml"))
                {
                    File.Delete(file);

                }

                foreach (string file in Directory.GetDirectories(filePath))
                {
                    DeleteFile(file);

                    Directory.Delete(file);

                }
            }

        }

        public static void MakeFile(byte[] fData, string path)
        {
            lock (path)
            {
                using (FileStream fs = new FileStream(path, FileMode.CreateNew))
                {
                    BinaryWriter bw = new BinaryWriter(fs);
                    //byte[]  byWrite = Encoding.Unicode.GetBytes();
                    bw.Write(fData);
                    bw.Flush();
                    bw.Close();
                }
            }
        }

        public static string getStr(string[] str)
        {

            string s = "";
            foreach (string x in str)
            {

                s += x + "_";
            }

            s = s.Remove(s.Length - 1, 1);

            return s;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Reflection;
using BLL = com.portal.db.BLL;
using Model = com.portal.db.Model;
using com.portal.db.BLL.PO;
using com.portal.db.BLL.NROS;
using Tool;
using Log;

namespace WebApp.WebContent.Supplier
{
    public partial class Down : System.Web.UI.Page
    {
        private BLL.MessageBody messageServer;

        private Model.User user;
        private string siteNum;
        int pageCount;//总页数
        int currentPage = 1;//第定义当前页


        protected void Page_Load(object sender, EventArgs e)
        {
            user = (Model.User)Session["user"];

          
            BLL.User userServer = new BLL.User();
            messageServer = new BLL.MessageBody();
            siteNum = (string)Session["SiteNum"];

            if (!Page.IsPostBack) {
                InitMessageList();
            }

            string s = @"C:\Broker\Production\OUT\IC0561_TP02_L050_NROS_20140218_093237_109_7.xml";
            System.IO.Stream S = new System.IO.FileStream(s, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);


            long p = 0;

            long l = new System.IO.FileInfo(s).Length;

            Response.AddHeader("Accept-Ranges", "bytes");

            if (Request.Headers["Range"] != null)
            {
                Response.StatusCode = 206;
                p = long.Parse(Request.Headers["Range"].Replace("bytes=", "").Replace("-", ""));
            }

            Response.AddHeader("Content-Length", ((long)(l - p)).ToString());

            if (p != 0)
            {
                //不是从最开始下载,
                //响应的格式是:
                //Content-Range: bytes [文件块的开始字节]-[文件的总大小 - 1]/[文件的总大小]
                Response.AddHeader("Content-Range", "bytes " + p.ToString() + "-" + ((long)(l - p)).ToString() + "/" + l.ToString());
            }

            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(s));

            S.Position = p;

            int i = 1;
            byte[] b = new Byte[1024];
            while (i > 0)
            {
                i = S.Read(b, 0, b.Length);
                Response.OutputStream.Write(b, 0, i);
                Response.Flush();
            }

            S.Close();
            Response.End();

            
        }

        void InitMessageList()
        {
            string sql = "1=1";

            sql += " and Segment2 = 'IN' and Segment4='T' ";

            if (siteNum != ""&&siteNum!=null)
            {

                sql += " and vender_num = '" + user.SupplierNum + "'";
                sql += " and vender_site_num = '" + siteNum + "'";

            }
            else {
                sql += " and vender_num = '" + user.SupplierNum + "'";
            }
            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = messageServer.GetModelList(sql);

            pds.AllowPaging = true;
            //设置每页显示记录数
            pds.PageSize = 10;
            //获取总页数
            pageCount = pds.PageCount;
            this.total.Text = pageCount.ToString();
            pds.CurrentPageIndex = currentPage - 1;
            //当前页
            this.current.Text = Convert.ToString(currentPage);


            this.MessageList.DataSource = pds;
            this.MessageList.DataBind();        

            
        }

        protected void download_Click(object sender, EventArgs e)
        {
            string id = ((LinkButton)sender).CommandArgument;
            Model.MessageBody message = messageServer.GetModel(id);


            if(message.messageType=="NROS")
            {
                Log.LogHelper.Info("Inbox", "Download", HttpContext.Current.Request.UserHostAddress, user.UserID, "Download NROS File:" + message.segment1);

                try {
                    message.status = "Read";
                    messageServer.Update(message);
                }
                catch(Exception err){
                    Log.LogHelper.Error("Inbox", "Download", HttpContext.Current.Request.UserHostAddress, user.UserID, "Download fial ,the file is invalid", err);
                }

                
                gelerateROS(message);
                
                
            }
            if (message.messageType == "PO")
            {
                Log.LogHelper.Info("Inbox", "Download", HttpContext.Current.Request.UserHostAddress, user.UserID, "Download PO File" + message.segment1);

                try
                {
                    message.status = "Read";
                    messageServer.Update(message);
                }
                catch (Exception err)
                {
                    Log.LogHelper.Error("Inbox", "Download", HttpContext.Current.Request.UserHostAddress, user.UserID, "Download fial ,the file is invalid", err);
                }
                
                geleratePO(message);
                
            }

            
            
        }


        protected void Name_Click(object sender, EventArgs e)
        {
            string id = ((LinkButton)sender).CommandArgument;
            Model.MessageBody message = messageServer.GetModel(id);


            if (message.messageType == "NROS")
            {
                Log.LogHelper.Info("Inbox", "Download", HttpContext.Current.Request.UserHostAddress, user.UserID, "Download NROS File:" + message.segment1);

                try
                {
                    message.status = "Read";
                    messageServer.Update(message);
                }
                catch (Exception err)
                {
                    Log.LogHelper.Error("Inbox", "Download", HttpContext.Current.Request.UserHostAddress, user.UserID, "Download fial ,the file is invalid", err);
                }


                //gelerateROSFile(message);


            }
            if (message.messageType == "PO")
            {
                Log.LogHelper.Info("Inbox", "Download", HttpContext.Current.Request.UserHostAddress, user.UserID, "Download PO File" + message.segment1);

                try
                {
                    message.status = "Read";
                    messageServer.Update(message);
                }
                catch (Exception err)
                {
                    Log.LogHelper.Error("Inbox", "Download", HttpContext.Current.Request.UserHostAddress, user.UserID, "Download fial ,the file is invalid", err);
                }

                //geleratePOFile(message);

            }



        }


        protected void gelerateROS(Model.MessageBody model)
        {
            string trueName = model.messageName + ".xlsx";
            nrosServer nros = new nrosServer();
            //BLL.MessageBody rm = new BLL.MessageBody();

            //Model.MessageBody model = rm.GetModelList("[key] = '" + referenceID + "'")[0];
            Message<ROSHeader> ros = null;
            try
            {
                ros = nros.convert2Ros(model);
            }
            catch (Exception err)
            {

                Log.LogHelper.Error("Inbox", "Download", HttpContext.Current.Request.UserHostAddress, user.UserID, "Convert NROS Fail,file name :" + trueName, err);
                this.mes.InnerText = "Download file fail , please check system log!!";
                this.mes.Style.Add(HtmlTextWriterStyle.Color, "red");
                this.mes.Style.Add(HtmlTextWriterStyle.FontSize, "1.2em");
                return;

            }
            //拷贝模板到临时文件夹
            string FilePath = Server.MapPath("~/") + "Excel\\Model\\ROSFormat.xlsx";
            string TempPath = Server.MapPath("~/") + "Excel\\Model\\Temp";
            string name = System.Guid.NewGuid().ToString("N") + ".xlsx";
            copyFiles(FilePath, TempPath, name);

          
            Byte[] ep = null;

            if (ros != null)
            {
                try
                {
                    ep = FileUtil.exportExcelforRos(ros, TempPath + "\\" + name, model.segment1, "Supplier");

                }
                catch (Exception err)
                {
                    Log.LogHelper.Error("Inbox", "Download", HttpContext.Current.Request.UserHostAddress, user.UserID, "Export Excel2007 Ros file Fail,file name :" + trueName, err);
                    this.mes.InnerText = "Export Excel2007 Ros file fail , please check system log!!";
                    this.mes.Style.Add(HtmlTextWriterStyle.Color, "red");
                    this.mes.Style.Add(HtmlTextWriterStyle.FontSize, "1.2em");
                    return;
                }
            }

            FileInfo tempInfo = new FileInfo(TempPath +"\\"+ name);
            tempInfo.Delete();

            if (ep != null)
            {
                downFile(ep, trueName );
            }
         
           

        }


        //protected void gelerateROSFile(Model.MessageBody model)
        //{
        //    string trueName = model.messageName + ".xlsx";
        //    nrosServer nros = new nrosServer();
        //    //BLL.MessageBody rm = new BLL.MessageBody();

        //    //Model.MessageBody model = rm.GetModelList("[key] = '" + referenceID + "'")[0];
        //    Message<ROSHeader> ros = null;
        //    try
        //    {
        //        ros = nros.convert2Ros(model);
        //    }
        //    catch (Exception err)
        //    {

        //        Log.LogHelper.Error("Inbox", "Download", HttpContext.Current.Request.UserHostAddress, user.UserID, "Convert NROS Fail,file name :" + trueName, err);
        //        this.mes.InnerText = "Download file fail , please check system log!!";
        //        this.mes.Style.Add(HtmlTextWriterStyle.Color, "red");
        //        this.mes.Style.Add(HtmlTextWriterStyle.FontSize, "1.2em");
        //        return;

        //    }
        //    //拷贝模板到临时文件夹
        //    string FilePath = Server.MapPath("~/") + "Excel\\Model\\ROSFormat.xlsx";
        //    string TempPath = Server.MapPath("~/") + "Excel\\Model\\Temp";
        //    string name = System.Guid.NewGuid().ToString("N") + ".xlsx";
        //    copyFiles(FilePath, TempPath, name);


        //    //Byte[] ep = null;

        //    if (ros != null)
        //    {
        //        try
        //        {
        //            string newFilePath = Server.MapPath("~/") + "Excel\\ExportTemp\\" + Session.SessionID+trueName;

        //            FileUtil.exportExcel2007forRos(ros, newFilePath, TempPath + "\\" + name, model.segment1, "Supplier");

        //            //File.Copy(newFilePath,
                    
        //            //Response.Redirect("~/Excel/ExportTemp/" + trueName);


        //            //DownLoad(trueName,newFilePath);
        //            TransmitFile(trueName, newFilePath);
                  




        //        }
        //        catch (Exception err)
        //        {
        //            Log.LogHelper.Error("Inbox", "Download", HttpContext.Current.Request.UserHostAddress, user.UserID, "Export Excel2007 Ros file Fail,file name :" + trueName, err);
        //            this.mes.InnerText = "Export Excel2007 Ros file fail , please check system log!!";
        //            this.mes.Style.Add(HtmlTextWriterStyle.Color, "red");
        //            this.mes.Style.Add(HtmlTextWriterStyle.FontSize, "1.2em");
        //            return;
        //        }
        //    }

        //    //FileInfo tempInfo = new FileInfo(TempPath + "\\" + name);
        //    //tempInfo.Delete();

        //    //if (ep != null)
        //    //{
        //    //    downFile(ep, trueName + ".xlsx");
        //    //}



        //}


        protected void geleratePO(Model.MessageBody model)
        {
            string trueName = model.messageName + ".xlsm";
            poServer poo = new poServer();
            
            //BLL.MessageBody rm = new BLL.MessageBody();

            string fileName = System.Guid.NewGuid().ToString("N") + ".xlsm"; 

            //Model.MessageBody model = rm.GetModelList("[key] = '" + referenceID + "'")[0];

            Message<Tool.POHeader> po = null;

            try
            {
                po = poo.convert2PO(model);
            }
            catch (Exception err)
            {
                Log.LogHelper.Error("Inbox", "Download", HttpContext.Current.Request.UserHostAddress, user.UserID, "Convert PO Fail,file name :" + trueName, err);
                this.mes.InnerText = "Download file fail , please check system log!!";
                this.mes.Style.Add(HtmlTextWriterStyle.Color, "red");
                this.mes.Style.Add(HtmlTextWriterStyle.FontSize, "1.2em");
                return;
            }

            string FilePath = Server.MapPath("~/") + "Excel\\Model\\POFormat.xlsm";
            string TempPath = Server.MapPath("~/") + "Excel\\Model\\Temp";
            string name = System.Guid.NewGuid().ToString("N") + ".xlsm";

            copyFiles(FilePath, TempPath, name);

            Byte[] ep = null;

            //string newFilePath = Server.MapPath("~/") + "Excel\\Temp\\" + trueName;

            if (po != null)
            {
                try
                {
                    ep = FileUtil.exportExcelforPO(po, TempPath + "\\" + name, model.segment1);

                    //Response.Redirect("~/Excel/ExportTemp/" + trueName);
                    //DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/") + "Excel\\Temp");
                    //FileInfo[] files = dirInfo.GetFiles();   // 获取该目录下的所有文件
                    //foreach (FileInfo file in files)
                    //{
                    //    if (!file.Name.Equals(fileName))
                    //    {
                    //        file.Delete();
                    //    }
                    //}
                }
                catch (Exception err)
                {
                    Log.LogHelper.Error("Inbox", "Download", HttpContext.Current.Request.UserHostAddress, user.UserID, "Export Excel2007 PO file Fail,file name :" + trueName, err);
                    this.mes.InnerText = "Export Excel2007 PO file fail , please check system log!!";
                    this.mes.Style.Add(HtmlTextWriterStyle.Color, "red");
                    this.mes.Style.Add(HtmlTextWriterStyle.FontSize, "1.2em");
                    return;
                }
            }
            FileInfo tempInfo = new FileInfo(TempPath + "\\" + name);
            tempInfo.Delete();
            if (ep != null)
            {
                downFile(ep, trueName );
            }


        }


        //protected void geleratePOFile(Model.MessageBody model)
        //{
        //    string trueName = model.messageName + ".xlsm";
        //    poServer poo = new poServer();

        //    //BLL.MessageBody rm = new BLL.MessageBody();

        //    string fileName = System.Guid.NewGuid().ToString("N") + ".xlsm";

        //    //Model.MessageBody model = rm.GetModelList("[key] = '" + referenceID + "'")[0];

        //    Message<Tool.POHeader> po = null;

        //    try
        //    {
        //        po = poo.convert2PO(model);
        //    }
        //    catch (Exception err)
        //    {
        //        Log.LogHelper.Error("Inbox", "Download", HttpContext.Current.Request.UserHostAddress, user.UserID, "Convert PO Fail,file name :" + trueName, err);
        //        this.mes.InnerText = "Download file fail , please check system log!!";
        //        this.mes.Style.Add(HtmlTextWriterStyle.Color, "red");
        //        this.mes.Style.Add(HtmlTextWriterStyle.FontSize, "1.2em");
        //        return;
        //    }

        //    string FilePath = Server.MapPath("~/") + "Excel\\Model\\POFormat.xlsm";
        //    string TempPath = Server.MapPath("~/") + "Excel\\Model\\Temp";
        //    string name = System.Guid.NewGuid().ToString("N") + ".xlsm";

        //    copyFiles(FilePath, TempPath, name);

        //    // Byte[] ep = null;

        //    string newFilePath = Server.MapPath("~/") + "Excel\\ExportTemp\\" + trueName;

        //    if (po != null)
        //    {
        //        try
        //        {
        //            FileUtil.exportExcel2007forPO(po, newFilePath, TempPath + "\\" + name, model.segment1);

        //           // Response.Redirect("~/Excel/ExportTemp/" + trueName);

        //            DownLoad(trueName, newFilePath);
        //            //TransmitFile(trueName, newFilePath);
                    
        //            //DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/") + "Excel\\Temp");
        //            //FileInfo[] files = dirInfo.GetFiles();   // 获取该目录下的所有文件
        //            //foreach (FileInfo file in files)
        //            //{
        //            //    if (!file.Name.Equals(fileName))
        //            //    {
        //            //        file.Delete();
        //            //    }
        //            //}
        //        }
        //        catch (Exception err)
        //        {
        //            Log.LogHelper.Error("Inbox", "Download", HttpContext.Current.Request.UserHostAddress, user.UserID, "Export Excel2007 PO file Fail,file name :" + trueName, err);
        //            this.mes.InnerText = "Export Excel2007 PO file fail , please check system log!!";
        //            this.mes.Style.Add(HtmlTextWriterStyle.Color, "red");
        //            this.mes.Style.Add(HtmlTextWriterStyle.FontSize, "1.2em");
        //            return;
        //        }
        //    }
        //    //FileInfo tempInfo = new FileInfo(TempPath + "\\" + name);
        //    //tempInfo.Delete();
        //    //if (ep != null)
        //    //{
        //    //    downFile(ep, trueName + ".xlsm");
        //    //}


        //}


        protected void search_Click(object sender, EventArgs e)
        {
            string sql = "1=1";

            sql += " and (Segment2 = 'IN')";

            if (siteNum != "" && siteNum != null)
            {
                sql += " and vender_num = '" + user.SupplierNum + "'";
                sql += " and vender_site_num = '" + siteNum + "'";
            }
            else {
                sql += " and vender_num = '" + user.SupplierNum + "'";
            }
            string fromdate = Request["FromDate"];
            string todate = Request["ToDate"];
            string reference = Request["reference"];
            string messagetype = Request["messagetype"];
            string status = Request["status"];

            

            if (fromdate != "")
            {

                sql += " and creationDateTime >='" + fromdate + "'";
            }
            if (todate != "") {
                sql += " and creationDateTime <='" + todate + "'";
            }
            if (reference != "") {
                sql += " and MessageName like '%" + reference + "%'";
            }
            if (messagetype != "--") {
                sql += " and MessageType = '" + messagetype + "'";
            }
            if (status != "--") {
                sql += " and Status = '"+ status + "'";
            }


            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = messageServer.GetModelList(sql);

            pds.AllowPaging = true;
            //设置每页显示记录数
            pds.PageSize = 10;
            //获取总页数
            pageCount = pds.PageCount;
            this.total.Text = pageCount.ToString();
            pds.CurrentPageIndex = currentPage - 1;
            //当前页
            this.current.Text = Convert.ToString(currentPage);


            this.MessageList.DataSource = pds;
            this.MessageList.DataBind();

            Log.LogHelper.Info("Inbox", "Search", HttpContext.Current.Request.UserHostAddress, user.UserID, "Search Message");
                 
          


            //logObject.Info("Inbox Page Search File End");
            //LogUtil.WriteLog("Inbox Page Search File ", "End…… \r\nTime:" + LogUtil.getCurrentTime());
        }



        protected void Button1_Click(object sender, EventArgs e)
        {//如果当前不是第一页的时候
            if (this.current.Text == "1")
            {

            }
            else
            {
                currentPage = 1;
                search_Click(sender, e);
            }
        }
        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button2_Click(object sender, EventArgs e)
        {//如果当前不是最后页的时候
            if (this.total.Text == this.current.Text)
            {
            }
            else
            {
                currentPage = int.Parse(this.current.Text) + 1;
                this.current.Text = currentPage.ToString();
                search_Click(sender, e);
            }
        }
        /// <summary>
        /// 上一页　
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button3_Click(object sender, EventArgs e)
        {//如果当前不是第一页的时候
            if (this.current.Text != "1")
            {
                currentPage = int.Parse(this.current.Text) - 1;
                this.current.Text = currentPage.ToString();
                search_Click(sender, e);
            }
        }
        /// <summary>
        /// 最后一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button4_Click(object sender, EventArgs e)
        {//如果当前不是最后一页的时候
            if (this.total.Text != this.current.Text)
            {
                this.current.Text = this.total.Text;
                currentPage = int.Parse(this.current.Text);
                search_Click(sender, e);
            }
        }

        public void copyFiles(string srcpath, string destpath, string name)
        {

            try
            {
               
                FileInfo fileInfo = new FileInfo(srcpath);

                if (File.Exists(destpath + "\\" + fileInfo.Name))
                {
                   File.Delete(destpath + "\\" + fileInfo.Name);
                }
                    //from xmlServer in directory to xmlServer backup directory
                fileInfo.CopyTo(destpath + "\\" + name, true);
               
                //logObject.Info("Move Files to " + destpath );
            }
            catch (Exception e)
            {
                Log.LogHelper.Error("Inbox", "Download", HttpContext.Current.Request.UserHostAddress, user.UserID, "copy file fail,file name :" + name, new Exception(e.Message));
                   
            }

        }


        public void downFile(Byte[] data, string fileName)
        {

            try
            {
                Response.Clear();

                long dataLengthToRead = data.Length;//获取下载的文件总大小

               
                Response.AddHeader("Content-Disposition", "attachment;filename=" 
                    + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Length", dataLengthToRead.ToString()); 
                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.BinaryWrite(data);            //ep.SaveAs(Response.OutputStream);    第二种方式      
                //Response.Flush();            
                Response.End();
                //HttpContext.Current.ApplicationInstance.CompleteRequest();

            }
            catch (Exception err)
            {
                Log.LogHelper.Error("Inbox", "Download", HttpContext.Current.Request.UserHostAddress, user.UserID, "down file fail !", err);
                
                //Response.Clear();
                Response.End();
                //HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            
        
        }


        private void DownLoad(string fileName, string filePath)
        {
            const long ChunkSize = 100*1024;//100K 每次读取文件，只读取100K，这样可以缓解服务器的压力
            byte[] buffer = new byte[ChunkSize];
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            System.IO.FileStream iStream = System.IO.File.OpenRead(filePath);
            long dataLengthToRead = iStream.Length;//获取下载的文件总大小
            Response.AddHeader("Content-Disposition", "attachment;filename="
                + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");

            while (dataLengthToRead > 0 && Response.IsClientConnected)
            {
                int lengthRead = iStream.Read(buffer, 0, Convert.ToInt32(ChunkSize));//读取的大小
                Response.OutputStream.Write(buffer, 0, lengthRead);
                Response.Flush();
                dataLengthToRead = dataLengthToRead - lengthRead;
            }
            Response.End();
            //HttpContext.Current.ApplicationInstance.CompleteRequest();
        }


        public void TransmitFile(string fileName, string filePath)
        {
            Response.AddHeader("Content-Disposition", "attachment;filename="
                + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
            Response.ContentType = "application/ms-excel";
            Response.TransmitFile(filePath);
        }
    }
}
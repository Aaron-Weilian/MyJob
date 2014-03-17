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
using com.portal.db.BLL.NROS;
using com.portal.db.BLL.PO;
using Tool;
using System.Data;
using com.portal;
using System.Runtime.InteropServices;

namespace WebApp.WebContent.Buyer
{
    public partial class Inbox : System.Web.UI.Page
    {
        private BLL.MessageBody messageServer;
        BLL.Supplier supplierServer;
        private BLL.Site siteServer;

        private Model.User user;
        private string siteName;
        int pageCount;//总页数
        int currentPage = 1;//第定义当前页

        protected void Page_Load(object sender, EventArgs e)
        {
            BLL.User userServer = new BLL.User();
            messageServer = new BLL.MessageBody();
            supplierServer = new BLL.Supplier();
            siteServer = new BLL.Site();

            //this.supplierName.Attributes.Add("onclick", "Select()");
            
            siteName = (string)Session["SiteName"];
            user = (Model.User)Session["user"];

            if (!IsPostBack)
            {
                //site.Items.Insert(0, new ListItem("--", "--"));
                //site.SelectedIndex = 0;
                InitMessageList();
            }
        }


        void InitMessageList()
        {
            string sql = "1=1";

            sql += " and (Segment2 = 'OUT')";

            sql += " and [status] = 'OK' ";

            sql += " and [segment4] = 'T' ";

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
            string fileName = ((LinkButton)sender).CommandArgument;
            string[] segment1andmessagetyp = fileName.Split(':');
            fileName = segment1andmessagetyp[0];
            string[] str = segment1andmessagetyp[1].Split('_');
            string fileType = str[0].ToUpper();
            string extranet = "";
            switch (fileType)
            {
                case "PO":
                    extranet = ".xlsm";
                    break;
                case "NROS":
                    extranet = ".xlsx";
                    break;
            }


            var dirInfo = new DirectoryInfo(AppSettings.Settings.UPLOADPath + "downTemp\\");

            string s = dirInfo.FullName + "\\buyer\\" + fileName;
            s += extranet;

            if (!File.Exists(s))
            {

                this.mes.InnerText = "The file no exist , please resend file to system!! File path:"+s;
                Log.LogHelper.Error("Inbox", "Download", HttpContext.Current.Request.UserHostAddress, user.UserID, "The file no exist,file name :" + fileName + extranet);
                return;
            }

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
            Response.AddHeader("Content-Disposition", "attachment; filename=" + segment1andmessagetyp[1] + extranet);

            S.Position = p;

            int i = 1;
            byte[] b = new Byte[1024];
            while (i > 0)
            {
                i = S.Read(b, 0, b.Length);
                Response.OutputStream.Write(b, 0, i);

            }

            S.Close();
            string sql = " segment5 = '" + fileName + "'";

            var list = messageServer.GetModelList(sql);

            if (list.Count > 0)
            {

                list[0].segment3 = "Read";
                messageServer.Update(list[0]);
            }



            Response.End();


        }

        protected void gelerateROS(Model.MessageBody model)
        {
            nrosServer nros = new nrosServer();
            string trueName = model.segment1 + ".xlsm";
            //BLL.MessageBody rm = new BLL.MessageBody();

            //Model.MessageBody model = rm.GetModelList("[key] = '" + referenceID + "'")[0];
            Message<ROSHeader> ros = null;
            try
            {
                ros = nros.convert2Ros(model);
            }
            catch (Exception err)
            {

                Log.LogHelper.Error("WebApp.WebContent.Buyer.Inbox", "Download", HttpContext.Current.Request.UserHostAddress, user.UserID, "Convert NROSConfirm Fail,file name :" + model.segment1, err);
                this.mes.InnerText = "Download file fail , please check system log!!";
                this.mes.Style.Add(HtmlTextWriterStyle.Color, "red");
                this.mes.Style.Add(HtmlTextWriterStyle.FontSize, "1.2em");
                return;

            }

            string FilePath = Server.MapPath("~/") + "Excel\\Model\\ROSFormat.xlsx";
            string TempPath = Server.MapPath("~/") + "Excel\\Model\\Temp";
            string name = System.Guid.NewGuid().ToString("N") + ".xlsx";
            copyFiles(FilePath, TempPath, name);

            

            //Byte[] ep = null;

            //FileInfo[] files = new DirectoryInfo(Server.MapPath("~/") + "Excel\\ExportTemp").GetFiles(); // 获取该目录下的所有文件
            //foreach (FileInfo file in files)
            //{
            //    IntPtr vHandle = _lopen(file.FullName, OF_READWRITE | OF_SHARE_DENY_NONE);

            //    if (vHandle != HFILE_ERROR)
            //    {
            //        file.Delete();
            //    }

            //}


            if (ros != null)
            {
                try
                {
                    string newFilePath = Server.MapPath("~/") + "Excel\\ExportTemp\\" + trueName;

                    FileUtil.exportExcel2007forRos(ros,newFilePath, TempPath + "\\" + name,  "ROS", "Buyer");

                    Response.Redirect("~/Excel/ExportTemp/" + trueName);

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
                    Log.LogHelper.Error("WebApp.WebContent.Buyer.Inbox", "Download", HttpContext.Current.Request.UserHostAddress, user.UserID, "Export Excel2007 Ros Confirm file Fail,file name :" + model.segment1, err);
                    this.mes.InnerText = "Export Excel2007 Ros Confirm file fail , please check system log!!";
                    this.mes.Style.Add(HtmlTextWriterStyle.Color, "red");
                    this.mes.Style.Add(HtmlTextWriterStyle.FontSize, "1.2em");
                    return;
                }
            }

            //FileInfo tempInfo = new FileInfo(TempPath + "\\" + name);
            //tempInfo.Delete();

            //if (ep != null)
            //{
            //    downFile(ep, model.segment1 + ".xlsx");
            //}



        }


        protected void geleratePO(Model.MessageBody model)
        {
            poServer poo = new poServer();
            string trueName = model.segment1 + ".xlsm";
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
                Log.LogHelper.Error("WebApp.WebContent.Buyer.Inbox", "Download", HttpContext.Current.Request.UserHostAddress, user.UserID, "Convert PO  Fail,file name :" + trueName, err);
                this.mes.InnerText = "Download file fail , please check system log!!";
                this.mes.Style.Add(HtmlTextWriterStyle.Color, "red");
                this.mes.Style.Add(HtmlTextWriterStyle.FontSize, "1.2em");
                return;
            }

            string FilePath = Server.MapPath("~/") + "Excel\\Model\\POFormat.xlsm";
            string TempPath = Server.MapPath("~/") + "Excel\\Model\\Temp";
            string name = System.Guid.NewGuid().ToString("N") + ".xlsm";
            copyFiles(FilePath, TempPath, name);


            

            //Byte[] ep = null;

            //FileInfo[] files = new DirectoryInfo(Server.MapPath("~/") + "Excel\\ExportTemp").GetFiles(); // 获取该目录下的所有文件
            //foreach (FileInfo file in files)
            //{
            //    IntPtr vHandle = _lopen(file.FullName, OF_READWRITE | OF_SHARE_DENY_NONE);

            //    if (vHandle != HFILE_ERROR)
            //    {
            //        file.Delete();
            //    }

            //}


            if (po != null)
            {
                try
                {
                    string newFilePath = Server.MapPath("~/") + "Excel\\ExportTemp\\" + trueName;

                    FileUtil.exportExcel2007forPO(po,newFilePath, TempPath + "\\" + name, "PO");

                    Response.Redirect("~/Excel/ExportTemp/" + trueName);

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
                    Log.LogHelper.Error("WebApp.WebContent.Buyer.Inbox", "Download", HttpContext.Current.Request.UserHostAddress, user.UserID, "Export Excel2007 PO Confirm file Fail,file name :" + trueName, err);
                    this.mes.InnerText = "Export Excel2007 PO Confirm file fail , please check system log!!";
                    this.mes.Style.Add(HtmlTextWriterStyle.Color, "red");
                    this.mes.Style.Add(HtmlTextWriterStyle.FontSize, "1.2em");
                    return;
                }
            }
            //FileInfo tempInfo = new FileInfo(TempPath + "\\" + name);
            //tempInfo.Delete();
            //downFile(ep, trueName + ".xlsm");


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
            catch (Exception err)
            {
                Log.LogHelper.Error("WebApp.WebContent.Buyer.Inbox", "Download", HttpContext.Current.Request.UserHostAddress, user.UserID, "copy PO Confirm file Fail,file name :" + name, err);
                this.mes.InnerText = "Export Excel2007 PO Confirm file fail , please check system log!!";
                this.mes.Style.Add(HtmlTextWriterStyle.Color, "red");
                this.mes.Style.Add(HtmlTextWriterStyle.FontSize, "1.2em");
                return;
            }

        }


        public void downFile(Byte[] data, string fileName)
        {

            try
            {
                //const long ChunkSize = 102400;//100K 每次读取文件，只读取100K，这样可以缓解服务器的压力
                byte[] buffer = data;
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                //System.IO.FileStream iStream = System.IO.File.OpenRead(filePath);
                long dataLengthToRead = data.Length;//获取下载的文件总大小

                ////Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                ////Response.ContentType = "application/ms-excel";
                ////Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");

                //while (dataLengthToRead > 0 && Response.IsClientConnected)
                //{
                //    int lengthRead = iStream.Read(buffer, 0, Convert.ToInt32(ChunkSize));//读取的大小
                //    Response.OutputStream.Write(buffer, 0, lengthRead);
                //    Response.Flush();
                //    dataLengthToRead = dataLengthToRead - lengthRead;
                //}


                //Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                Response.ContentType = "application/ms-excel";
                Response.AddHeader("Content-Length", dataLengthToRead.ToString());
                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.BinaryWrite(data);            //ep.SaveAs(Response.OutputStream);    第二种方式      

                Response.Flush();
                //Response.End();
                HttpContext.Current.ApplicationInstance.CompleteRequest();

            }
            catch (Exception err)
            {
                Log.LogHelper.Error("WebApp.WebContent.Buyer.Inbox", "Download", HttpContext.Current.Request.UserHostAddress, user.UserID, "down file fail !", err);

                Response.Clear();
                //Response.End();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            
        }
        
        
        
        protected void search_Click(object sender, EventArgs e)
        {
            string sql = "1=1";

            sql += " and (Segment2 = 'OUT')";
            sql += " and [status] = 'OK' ";
            sql += " and [segment4] = 'T' ";
            string formdate = Request["FormDate"];
            string todate = Request["ToDate"];
            string MessageName = Request["MessageName"];
            string messagetype = Request["messagetype"];
            string supplierNum = Request["supplierNum"];
            string status = Request["status"];
            //string buyer = Request["buyer"];

            Model.MessageBody model = new Model.MessageBody();
            

            if (formdate != "") {

                sql += " and [ConfirmDateTime] >='" + formdate + "'";
            }
            if (todate != "") {
                sql += " and [ConfirmDateTime] <='" + todate + "'";
            }
            if (MessageName != "")
            {
                if (MessageName.IndexOf('%') != -1)
                {
                    sql += " and [MessageName] like '%" + MessageName + "%'";
                }
                if (MessageName.IndexOf('%') == -1)
                {
                    sql += " and [MessageName] = '" + MessageName + "'";
                }
            }
            //if (buyer != "")
            //{
            //    sql += " and [Vender_Site] like '%" + buyer + "%'";
            //}
            if (messagetype != "--") {
                sql += " and [MessageType] like '%" + messagetype + "%'";
            }
            if (status != "--") {
                sql += " and segment3 like '%" + status + "%'";
            }
            if (supplierNum != "--" && supplierNum != null && supplierNum != "")
            {
                sql += " and Vender_Num = '%" + supplierNum + "%'";
            }
            //if (this.site.SelectedValue != "--")
            //{
            //    sql += " and MESSAGE.Sender like '%" + this.site.SelectedValue + "%'";
            //}



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

            Log.LogHelper.Info("WebApp.WebContent.Buyer.Inbox", "Search Confirm Message", HttpContext.Current.Request.UserHostAddress, user.UserID, "Search Message");
             

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

    }
}
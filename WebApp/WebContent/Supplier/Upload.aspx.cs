using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using BLL = com.portal.db.BLL;
using Model = com.portal.db.Model;
using com.portal.db.BLL.NROS;
using com.portal.db.BLL.PO;
using Tool;
using Log;
using com.portal;

namespace WebApp.WebContent.Supplier
{
    public partial class Upload : System.Web.UI.Page
    {
        //private static readonly ILog logObject = LogHelper.GetInstance(); //LogManager.GetLogger(typeof(TEST));

        private BLL.Supplier supplierServer;
        private Model.User user;
        private BLL.Site siteServer;
        private string siteNum;

        protected void Page_Load(object sender, EventArgs e)
        {
            user = (Model.User)Session["user"];
            siteNum = (string)Session["SiteNum"];
            supplierServer = new BLL.Supplier();
            siteServer = new BLL.Site();

           

            if (user == null || "".Equals(user))
            {
                Response.Redirect("~/Account/Login.aspx");
            }

            if (!Page.IsPostBack)
            {
                initSiteList();
            }
           
        }

        void initSiteList()
        {

            List<Model.Supplier> list = supplierServer.GetModelList("SupplierNum = '" + user.SupplierNum + "' and Status = 'Active'");
            if (list.Count > 0)
            {
                Model.Supplier supplier = list[0];
                this.siteList.DataSource = siteServer.GetModelList(" SupplierID ='" + supplier.SupplierID + "'");
                this.siteList.DataMember = "SiteName";
                this.siteList.DataTextField = "SiteName";
                this.siteList.DataValueField = "SiteNum";
                this.siteList.DataBind();
                if (siteNum != ""&&siteNum!=null)
                {
                    this.siteList.Items.FindByValue(siteNum).Selected = true;
                }
                
            }
            else
            {
                information.Style.Add(HtmlTextWriterStyle.Color, "red");
                information.Style.Add(HtmlTextWriterStyle.FontSize, "1.2em");
                information.InnerText = "User Supplier is Disable!";
            }
        }

        protected void upload_Click(object sender, EventArgs e)
        {
            //logObject.Info("Upload File Begin……");
            //清除提示信息
            information.InnerText = "";
       
            //检查文件是否为空
            if (this.file.HasFile)
            {
                //检查文件扩展名
                if (getExt(this.file.FileName) != "xlsx" && getExt(this.file.FileName) != "xlsm")
                {
                    information.Style.Add(HtmlTextWriterStyle.Color, "red");
                    information.Style.Add(HtmlTextWriterStyle.FontSize, "1.2em");
                    information.InnerText = "Upload file type is analyze,Please upload macrosoft office excel2007 format file";
                    return;
                }
                else
                {
                    //验证文件是否匹配
                    if (this.type.Value == "POConfirm")
                    {
                        if (getExt(this.file.FileName) != "xlsm") {
                            information.Style.Add(HtmlTextWriterStyle.Color, "red");
                            information.Style.Add(HtmlTextWriterStyle.FontSize, "1.2em");
                            information.InnerText = "The file type is no match,Please select file type";
                            return;
                        }
                    }
                    if (this.type.Value == "ROSConfirm")
                    {
                        if (getExt(this.file.FileName) != "xlsx")
                        {
                            information.Style.Add(HtmlTextWriterStyle.Color, "red");
                            information.Style.Add(HtmlTextWriterStyle.FontSize, "1.2em");
                            information.InnerText = "The file type is no match,Please select file type";
                            return;
                        }
                    }

                    BLL.MessageBody rosMessageServer = new BLL.MessageBody();
                    string newFilePath = AppSettings.Settings.UPLOADPath + file.FileName + ".Excel";
                    //上传文件到临时目录
                    //string newPath = Server.MapPath("~/") + "Excel\\Temp";

                   // string[] files = System.IO.Directory.GetFiles(newPath);

                    //string tempName = System.Guid.NewGuid().ToString("N")+".xlsx";

                    if (this.type.Value == "POConfirm")
                    {

                        if (!UploadFile(this.file,newFilePath,"POConfirm"))
                        {

                            information.InnerText = "Upload file failed,Please re-download source file , then upload file.";
                            Log.LogHelper.Error("WebApp.WebContent.Supplier.Upload", "Upload", HttpContext.Current.Request.UserHostAddress, user.UserID, "Upload file failed");
                            return;
                        }
                        
                    }
                    if (this.type.Value == "ROSConfirm")
                    {

                        if (!UploadFile(this.file, newFilePath, "ROSConfirm"))
                        {
                            information.InnerText = "Upload file failed,Please re-download source file , then upload file.";
                            Log.LogHelper.Error("WebApp.WebContent.Supplier.Upload", "Upload", HttpContext.Current.Request.UserHostAddress, user.UserID, "Upload file failed");
                            return;
                        }
                    }


                    

                    //int i = 1;
                    //while (File.Exists(newFilePath))
                    //{
                    //    newFilePath = AppSettings.Settings.UPLOADPath +
                    //                         file.FileName.Replace(file.FileName, file.FileName +
                    //                                               "_" + i.ToString()) + ".Excel";
                    //    i++;
                    //}

                    //FileStream fs = new FileStream(newFilePath, FileMode.Create);
                    //BinaryWriter bw = new BinaryWriter(fs);
                    //bw.Write(this.file.FileBytes);
                    //bw.Close();
                    //fs.Close();
                    //fs.Dispose();



                    Log.LogHelper.Info("WebApp.WebContent.Supplier.Upload", "Upload", HttpContext.Current.Request.UserHostAddress, user.UserID, "Upload file success");
                 
                    information.InnerText = "The File Upload Success";
                    information.Style.Add(HtmlTextWriterStyle.Color, "Green");


                }
            }
            else {
                information.Style.Add(HtmlTextWriterStyle.Color, "red");
                information.Style.Add(HtmlTextWriterStyle.FontSize, "1.2em");
                information.InnerText = "The File is null,Please Select Excel File";
               
            }
        }


        public string getExt(string name) {

            string[] str = name.Split('.');
            return str[str.Length - 1];
        }

        //public bool UploadROSFile(string newFilePath,string filename) {
        //    BLL.MessageBody messageServer = new BLL.MessageBody();
        //    nrosServer nros = new nrosServer();
        //    Message<Tool.ROSHeader> ros = null;

        //    try
        //    {
        //        ros = FileUtil.readROSExcel2007(newFilePath, "ROS");
        //    }
        //    catch (Exception err)
        //    {
        //        Log.LogHelper.Error("WebApp.WebContent.Supplier.Upload", "Upload", HttpContext.Current.Request.UserHostAddress, user.UserID, "Upload File Fail", err);
        //        this.information.Style.Add(HtmlTextWriterStyle.Color, "red");
        //        this.information.Style.Add(HtmlTextWriterStyle.FontSize, "1.2em");

        //        information.InnerText = "The File Formate is Invaild,Please Confirm The File Formate or re-download File Then Upload File./r/n Detail:" + err.Message;

        //        if (File.Exists(newFilePath)) File.Delete(newFilePath);

        //        return false;
        //    }
        //    if (ros != null)
        //    {
        //        //验证供应商是否匹配
        //        //if (ros.partner.vender_num == user.SupplierNum)
        //        //{
        //        //    if (ros.partner.vender_site_num == this.siteList.SelectedValue)
        //        //    {
        //                try
        //                {

        //                    nros.uploadROS(ros, "ROSConfirm", ros.filename);
                           
        //                }
        //                catch (Exception err)
        //                {

        //                    Log.LogHelper.Error("WebApp.WebContent.Supplier.Upload", "Upload", HttpContext.Current.Request.UserHostAddress, user.UserID, "Upload NROS File Data Fail", err);
        //                    if (File.Exists(newFilePath)) File.Delete(newFilePath);
        //                    return false;
        //                }
        //        //    }
        //        //    else {
        //        //        information.Style.Add(HtmlTextWriterStyle.Color, "red");
        //        //        information.Style.Add(HtmlTextWriterStyle.FontSize, "1.2em");
        //        //        information.InnerText = "Upload file supplier site information no macth";
        //        //        FileInfo file = new FileInfo(newFilePath);
        //        //        file.Delete();
        //        //        return false;
        //        //    }
        //        //}
        //        //else {
        //        //    information.Style.Add(HtmlTextWriterStyle.Color, "red");
        //        //    information.Style.Add(HtmlTextWriterStyle.FontSize, "1.2em");
        //        //    information.InnerText = "Upload file supplier information no macth";
        //        //    FileInfo file = new FileInfo(newFilePath);
        //        //    file.Delete();
        //        //    return false;
        //        //}

               
        //        //从临时目录移除文件
        //        //FileInfo fileInfo = new FileInfo(newFilePath);
        //        //fileInfo.Delete();
        //        return true;
        //    }
        //    else
        //    {
        //        information.Style.Add(HtmlTextWriterStyle.Color, "red");
        //        information.Style.Add(HtmlTextWriterStyle.FontSize, "1.2em");
        //        information.InnerText = "Excel file is invalid";

        //        if (File.Exists(newFilePath)) File.Delete(newFilePath);

        //        return false;
        //    }
        
        //}

        //public bool UploadPOFile(string newFilePath,string filename) {
        //    BLL.MessageBody messageServer = new BLL.MessageBody();
        //    poServer poo = new poServer();
        //    Message<Tool.POHeader> po = null;

        //    try
        //    {
        //        po = FileUtil.readPOExcel2007(newFilePath, "PO");
        //    }
        //    catch (Exception err)
        //    {
        //        Log.LogHelper.Error("WebApp.WebContent.Supplier.Upload", "Upload", HttpContext.Current.Request.UserHostAddress, user.UserID, "Upload PO File Fail", err);
        //        this.information.Style.Add(HtmlTextWriterStyle.Color, "red");
        //        this.information.Style.Add(HtmlTextWriterStyle.FontSize, "1.2em");

        //        information.InnerText = "The File Formate is Invaild,Please Confirm The File Formate or re-download File Then Upload File./r/n Detail:" + err.Message;
        //        if (File.Exists(newFilePath)) File.Delete(newFilePath);
        //        return false;
        //    }
        //    if (po != null)
        //    {
        //         //验证供应商是否匹配
        //        //if (po.partner.vender_num == user.SupplierNum)
        //        //{
        //        //    if (po.partner.vender_site_num == this.siteList.SelectedValue)
        //        //    {
        //                try
        //                {
        //                    poo.uploadPO(po, "POConfirm", po.filename);

        //                    //if (messageServer.Exists(po.sender.key, "POConfirm"))
        //                    //{
        //                    //    poo.updatePOData(po, "POConfirm", po.filename);
        //                    //}
        //                    //else
        //                    //{
        //                    //    poo.savePOData(po, "POConfirm", po.filename);
        //                    //}
        //                }
        //                catch (Exception err)
        //                {
        //                    Log.LogHelper.Error("WebApp.WebContent.Supplier.Upload", "Upload", HttpContext.Current.Request.UserHostAddress, user.UserID, "Upload PO File Data Fail", err);
        //                    if (File.Exists(newFilePath)) File.Delete(newFilePath);
        //                    return false;
        //                }
        //            //}
                
        //        //else {
        //        //        information.Style.Add(HtmlTextWriterStyle.Color, "red");
        //        //        information.Style.Add(HtmlTextWriterStyle.FontSize, "1.2em");
        //        //        information.InnerText = "Upload file supplier site information no macth";
        //        //        FileInfo file = new FileInfo(newFilePath);
        //        //        file.Delete();
        //        //        return false;
        //        //    }
        //        //}
        //        //else {
        //        //    information.Style.Add(HtmlTextWriterStyle.Color, "red");
        //        //    information.Style.Add(HtmlTextWriterStyle.FontSize, "1.2em");
        //        //    information.InnerText = "Upload file supplier information no macth";
        //        //    FileInfo file = new FileInfo(newFilePath);
        //        //    file.Delete();
        //        //    return false;
        //        //}
        
        //        //从临时目录移除文件
        //        //FileInfo fil = new FileInfo(newFilePath);
        //        //fil.Delete();
        //        return true;
        //    }
        //    else
        //    {
        //        information.Style.Add(HtmlTextWriterStyle.Color, "red");
        //        information.Style.Add(HtmlTextWriterStyle.FontSize, "1.2em");
        //        information.InnerText = "Excel file is invalid";

        //        if (File.Exists(newFilePath)) File.Delete(newFilePath);
        //        return false;
        //    }
        //}

        public bool UploadFile(FileUpload file,string uploadPath,string messagetype) {

            BLL.UploadFileList messageServer = new BLL.UploadFileList();
            Model.UploadFileList model = new Model.UploadFileList();

            model.FileName = file.FileName;
            model.FilePath = uploadPath;
            model.MessageType = messagetype;
            model.Status = "IN-Process";
            model.UploadBy = user.UserID;
            model.FileStream = file.FileBytes;
            model.ConfirmDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if(messageServer.Add(model))  return true;

            return false;
        
        
        }
    
    }
}
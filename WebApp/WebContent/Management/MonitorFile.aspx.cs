using System;
using System.Web;
using System.IO;
using com.portal;
using System.Web.UI.WebControls;

namespace WebApp.WebContent.Management
{
    public partial class MonitorFile : System.Web.UI.Page
    {
        private com.portal.db.Model.User user;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (com.portal.db.Model.User)Session["user"];
        }

        protected void btnInbound_Click(object sender, EventArgs e){
            var dirInfo = new DirectoryInfo(AppSettings.Settings.InBoundPath);
            listFolder.DataSource = dirInfo.GetDirectories();
            listFolder.DataBind();

            litCurrentPath.Text = AppSettings.Settings.InBoundPath;
            this.FileList.DataSource = dirInfo.GetFiles();
            this.FileList.DataBind();
        }

        protected void btnOutbound_Click(object sender, EventArgs e){
            var dirInfo = new DirectoryInfo(AppSettings.Settings.OutBoundPath);
            listFolder.DataSource = dirInfo.GetDirectories();
            listFolder.DataBind();

            litCurrentPath.Text = AppSettings.Settings.OutBoundPath;
            this.FileList.DataSource = dirInfo.GetFiles();
            this.FileList.DataBind();
        }

        protected void btnBackup_Click(object sender, EventArgs e){
            var dirInfo = new DirectoryInfo(AppSettings.Settings.BackupPath);
            var dirs = dirInfo.GetDirectories();
            listFolder.DataSource = dirs;
            listFolder.DataBind();

            FileInfo[] files = dirInfo.GetFiles();
            litCurrentPath.Text = AppSettings.Settings.BackupPath;
            this.FileList.DataSource = files;
            this.FileList.DataBind();
        }

        protected void Dir_ItemCommand(Object Sender, RepeaterCommandEventArgs e){
            string dir = ((LinkButton) e.CommandSource).Text;
            
            dir = Path.Combine(litCurrentPath.Text,dir);
            var dirInfo = new DirectoryInfo(dir);
            
            var dirs = dirInfo.GetDirectories();
            listFolder.DataSource = dirs;
            listFolder.DataBind();

            FileInfo[] files = dirInfo.GetFiles();
            litCurrentPath.Text = dirInfo.FullName;
            //if (dirInfo.Parent == null) litCurrentPath.Text = dirInfo.Root.FullName;
            //else litCurrentPath.Text = dirInfo.Parent.FullName;
            this.FileList.DataSource = files;
            this.FileList.DataBind();
        }

        protected void btnUpgradeWeb_Click(object sender, EventArgs e){
            string path = @"E:\WWWRoot\WebApp\Test\";
            path = @"d:\";
            try {
                var fullpath = Path.Combine(path, txgPath.Text, fileUpload.FileName);
                //if (File.Exists(fullpath)) BackupFile(fullpath, fullpath);

                fileUpload.SaveAs(fullpath);
                literalMsg.Text = fullpath;
            }
            catch (Exception ex) {
                literalMsg.Text = ex.StackTrace; 
            }

            var dirInfo = new DirectoryInfo(litCurrentPath.Text);

            var dirs = dirInfo.GetDirectories();
            listFolder.DataSource = dirs;
            listFolder.DataBind();

            FileInfo[] files = dirInfo.GetFiles();
            litCurrentPath.Text = dirInfo.FullName;
            this.FileList.DataSource = files;
            this.FileList.DataBind();
            
        }

        protected void Name_Click(object sender, EventArgs e)
        {
            string fileName = ((LinkButton)sender).CommandArgument;

            string s = litCurrentPath.Text+"\\" + fileName;
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
               
            }

            S.Close();
            Response.End();
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            string fileName = ((LinkButton)sender).CommandArgument;

            string s = litCurrentPath.Text + "\\" + fileName;

            try
            {
                if (File.Exists(s)) File.Delete(s);
            }
            catch (Exception ex) {
                Log.LogHelper.Error("MonitorFile", "MonitorFile", HttpContext.Current.Request.UserHostAddress, user.UserID, String.Format("Delete File {0} Fail",fileName), ex);
            }

            var dirInfo = new DirectoryInfo(litCurrentPath.Text);

            var dirs = dirInfo.GetDirectories();
            listFolder.DataSource = dirs;
            listFolder.DataBind();

            FileInfo[] files = dirInfo.GetFiles();
            litCurrentPath.Text = dirInfo.FullName;
            //if (dirInfo.Parent == null) litCurrentPath.Text = dirInfo.Root.FullName;
            //else litCurrentPath.Text = dirInfo.Parent.FullName;
            this.FileList.DataSource = files;
            this.FileList.DataBind();

        }

        protected void createFolder_Click(object sender, EventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(txgPath.Text);
            if(!dir.Exists) dir.Create();

            literalMsg.Text = dir.FullName;
        }

        public string BackupFile(string originalFileFullName, string backupFilePath)
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
    }
}
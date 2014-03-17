using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL = com.portal.db.BLL;
using Model = com.portal.db.Model;
using System.IO;

namespace WebApp.WebContent.Management
{
    public partial class UploadFileList : System.Web.UI.Page
    {
        private BLL.UploadFileList messageServer;

        private Model.User user;
        int pageCount;//总页数
        int currentPage = 1;//第定义当前页

        //private static readonly ILog logObject = LogHelper.GetInstance(); //LogManager.GetLogger(typeof(TEST));


        protected void Page_Load(object sender, EventArgs e)
        {
            user = (Model.User)Session["user"];
            BLL.User userServer = new BLL.User();
            messageServer = new BLL.UploadFileList();
            if (!Page.IsPostBack)
            {
                InitMessageList();
            }
        }


        void InitMessageList()
        {
            string sql = "1=1";

            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = messageServer.GetModelFileList(sql);

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

        protected void search_Click(object sender, EventArgs e)
        {
            // logObject.Info("Outbox Page Search File Begin……");
            // LogUtil.WriteLog("Outbox Page Search File ", "Begin…… \r\nTime:" + LogUtil.getCurrentTime());

            string sql = "1=1";

            string fromdate = Request["FromDate"];
            string todate = Request["ToDate"];
            string reference = Request["reference"];
            string messagetype = Request["messagetype"];
            string status = Request["Notes"];

            if (fromdate != "")
            {

                sql += " and confirmDate >='" + fromdate + "'";
            }
            if (todate != "")
            {
                sql += " and confirmDate <='" + todate + "'";
            }
            if (reference != "")
            {
                sql += " and FileName like '%" + reference + "%'";
            }
            if (messagetype != "--")
            {
                sql += " and MessageType = '" + messagetype + "'";
            }
            if (status != "--")
            {
                sql += " and Status = '" + status + "'";
            }

            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = messageServer.GetModelFileList(sql);

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

            // logObject.Info("Outbox Page Search File Begin……");
            //LogUtil.WriteLog("Outbox Page Search File ", "End…… \r\nTime:" + LogUtil.getCurrentTime());


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

       

        protected void download_Click(object sender, EventArgs e) {

            string fileName = ((LinkButton)sender).CommandArgument;
            string[] IDandFileName = fileName.Split(':');
            fileName = IDandFileName[1];
            string ID = IDandFileName[0];

            string sql = "uploadfilelistid='"+ID+"'  ";
            List<Model.UploadFileList> list =  messageServer.GetModelList(sql);

            if (list.Count > 0) {
                downFile(list[0].FileStream, fileName);
            
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


         public void downFile(Byte[] data, string fileName)
         {
             Log.LogHelper.Info("Test", "down", HttpContext.Current.Request.UserHostAddress, user.UserID,
                     string.Format("write file startTime:{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
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
    }
}
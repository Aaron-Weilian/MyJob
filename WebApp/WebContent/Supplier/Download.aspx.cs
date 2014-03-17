using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tool;
using System.IO;

namespace WebApp.WebContent.Supplier
{
    public partial class Download : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsCallback)
            {

                string filePath = Request["filePath"];
                string fileName = Request["fileName"];


                //Response.Clear();                             //清除缓冲区流中的所有内容输出

                //Response.ClearHeaders();                      //清除缓冲区流中的所有头，不知道为什么，不写这句会显示错误页面

                //Response.ContentType = "application/ms-excel";     //设置输出类型，应用于所有文件
                //Response.Buffer = true; 
                ////将 HTTP 头添加到输出流

                //Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName); //一定要写，不写出错
                //Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                ////Response.AppendHeader("Content-Length", DownloadFile.Length.ToString());   //可以不写，程序自动检测长度

                ////Response.TransmitFile(filePath);//以上将指定的文件直接写入 HTTP 内容输出流。

                //Response.WriteFile(filePath); HttpContext.Current.Response.Flush(); 
                ////Response.End();

                //HttpContext.Current.ApplicationInstance.CompleteRequest();

                //LogUtil.WriteLog("Download File", "File " + fileName + " was download \r\nTime:" + LogUtil.getCurrentTime());




                const long ChunkSize = 1024;//100K 每次读取文件，只读取100K，这样可以缓解服务器的压力
                byte[] buffer = new byte[ChunkSize];
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                System.IO.FileStream iStream = System.IO.File.OpenRead(filePath);
                long dataLengthToRead = iStream.Length;//获取下载的文件总大小

                Response.AddHeader("Content-Disposition", "attachment;filename=" 
                    + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                Response.ContentType = "application/ms-excel";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");

                while (dataLengthToRead > 0 && Response.IsClientConnected)
                {
                    int lengthRead = iStream.Read(buffer, 0, Convert.ToInt32(ChunkSize));//读取的大小
                    Response.OutputStream.Write(buffer, 0, lengthRead);
                    Response.Flush();
                    dataLengthToRead = dataLengthToRead - lengthRead;
                }
                Response.Flush();
                Response.Close();

                FileInfo tempInfo = new FileInfo(filePath);
                tempInfo.Delete();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }

        }
    }
}
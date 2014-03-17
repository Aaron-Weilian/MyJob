using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL = com.portal.db.BLL;
using Model = com.portal.db.Model;
using Tool;
using Log;
using com.portal.db.DAL;

namespace WebApp.WebContent.Supplier
{
    public partial class Outbox : System.Web.UI.Page
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


            if (user != null)
            {
                sql += " and uploadby = '" + user.UserID + "'";
            }

            //SqlHelper.ExecuteNonQuery(GetAppSetting.GetConnSetting(), CommandType.Text, string.Format("Update Message set Status = 'Error' Where MessageID='0'", message.messageID));
                            


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


            if (user != null)
            {
                sql += " and uploadby = '" + user.UserID + "'";
            }

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
    }
}
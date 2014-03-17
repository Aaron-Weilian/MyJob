using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL = com.portal.db.BLL;
using Model = com.portal.db.Model;
using Tool;


namespace WebApp.WebContent.Buyer
{
    public partial class Outbox : System.Web.UI.Page
    {
        private BLL.MessageBody messageServer;

        private Model.User user;
        private string siteName;
        int pageCount;//总页数
        int currentPage = 1;//第定义当前页

        protected void Page_Load(object sender, EventArgs e)
        {
            BLL.User userServer = new BLL.User();
            messageServer = new BLL.MessageBody();
            siteName = (string)Session["SiteName"];
            user = (Model.User)Session["user"];

            if (!Page.IsPostBack)
            {
                InitMessageList();
            }
        }


        void InitMessageList()
        {
            string sql = " 1=1 ";

            sql += " and (Segment2 = 'IN')";

            //if (siteName != "")
            //{

            //    sql += " and Sender = '" + siteName + "'";
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


        }

        protected void search_Click(object sender, EventArgs e)
        {
            string sql = " 1=1 ";

            sql += " and (Segment2 = 'IN')";

            string formdate = Request["FromDate"];
            string todate = Request["ToDate"];
            string reference = Request["reference"];
            string messagetype = Request["messagetype"];

            if (formdate != "")
            {

                sql += " and MESSAGE.creationDateTime >='" + formdate + "'";
            }
            if (todate != "")
            {
                sql += " and MESSAGE.creationDateTime <='" + todate + "'";
            }
            if (reference != "")
            {
                sql += " and MESSAGE.MessageName like '%" + reference + "%'";
            }
            if (messagetype != "--" && messagetype != null && messagetype!="")
            {
                sql += " and MESSAGE.MessageType like '%" + messagetype + "%'";
            }

            sql += " and MESSAGE.Notes = 'Creation'";


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
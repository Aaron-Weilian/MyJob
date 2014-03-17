using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{

    public class AuthenticModule : IHttpModule
    {

        public void Dispose() { }

        public void Init(HttpApplication context)
        {

            context.PreRequestHandlerExecute += new EventHandler(context_PreRequestHandlerExecute);

        }

        void context_PreRequestHandlerExecute(object sender, EventArgs e)
        {

            HttpApplication ha = (HttpApplication)sender;

            string path = ha.Context.Request.Url.ToString();

            string[] str = path.Split('/');

            string requestPage = str[str.Length - 1];

            if (requestPage.IndexOf('.') < 0)

                ha.Context.Response.Redirect("~/Account/Login.aspx");

            string[] page = requestPage.ToLower().Split('.');

            bool flag = false;

            flag = page[page.Length - 1].ToLower().Equals("aspx");

            if (flag)
            {

                flag = requestPage.ToLower().Equals("login.aspx");

                if (!flag) //是否是登录页面，不是登录页面的话则进入{}
                {
                    if (ha.Context.Session == null) {

                        ha.Context.Response.Redirect("~/Account/Login.aspx");
                    }

                    if (ha.Context.Session["user"] == null) //是否Session中有用户名，若是空的话，转向登录页。
                    {

                        ha.Context.Response.Redirect("~/Account/Login.aspx");

                    }

                }
            }

        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebTrashCheck.MasterPages
{
    public partial class BootstrapMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MembershipUser currentUser = Membership.GetUser();

            if (currentUser != null)
            {
                lblIP.Visible = true;
                lblUserName.Visible = true;
                lblUserName.Text = "Вы авторизованы как: " + currentUser.UserName;
            }
            string ip = "Ваш IP: " + GetIPAddress();
            lblIP.Text = ip;
        }

        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Others/LoginPage.aspx");
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}
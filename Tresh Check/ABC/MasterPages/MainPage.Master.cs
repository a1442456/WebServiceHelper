using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Net;
using System.Net.Sockets;

namespace WebTrashCheck.WebTrashCheck
{
    public partial class MainPage : System.Web.UI.MasterPage
    {
        MembershipUser currentUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            string greetingsTxt = "Вы авторизированны как:";

            currentUser = Membership.GetUser();

            if (currentUser != null)
            {
                lblGreeting.Text = greetingsTxt;
                lblUserName.Text = " " + currentUser.UserName;
            }
            string ip = GetIPAddress();
            lblIP.Text = ip;
            lblIP2.Text = ip;
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
            if (Membership.ValidateUser(txtbxLogin.Text, txtbxPassword.Text))
            {
                FormsAuthentication.RedirectFromLoginPage(txtbxLogin.Text, false);
            }            
        }

        protected void btnProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Users/Profile.aspx");
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        protected void btnRegistration_Click(object sender, EventArgs e)
        {
            Response.Write("<script>alert('Ты не зарегистрируешься :)');</script>");
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Users/ChangePassword.aspx");
        }
    }
}
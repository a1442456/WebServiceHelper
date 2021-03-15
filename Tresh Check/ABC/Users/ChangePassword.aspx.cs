using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebTrashCheck.Users
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        LogWorker _loger = new LogWorker();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MembershipUser currentUser = Membership.GetUser();
                lblInfo.Text = "Дата последней смены пароля: ";
                lblInfo.Text += currentUser.LastPasswordChangedDate.ToString() + "</br>";
            }
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {   
            MembershipUser currentUser = Membership.GetUser();            

            if (txtbxNewPassword.Text == textbxNewPasswordRepeat.Text)
            {
                currentUser.ChangePassword(txtbxOldPassword.Text, txtbxNewPassword.Text);
                _loger.TypeInLogFile("Попытка смены пароля пользователем. Старый пароль: " + txtbxOldPassword.Text + " Новый пароль: " + txtbxNewPassword.Text, LogStatus.INFO, "WebSRV пользователь: " + currentUser.UserName);
                Response.Write("<script>alert('Произведена попытка смены пароля');</script>");
                lblInfo.Text = "Дата последней смены пароля: ";
                lblInfo.Text += currentUser.LastPasswordChangedDate.ToString() + "</br>";
            }
            else
            {
                Response.Write("<script>alert('Поля нового пароля не совпадают ');</script>");
                _loger.TypeInLogFile("Попытка смены пароля пользователем. Поля нового пароля не совпадают", LogStatus.ERROR, "WebSRV пользователь: " + currentUser.UserName);
            }
       }
    }
}
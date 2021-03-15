using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTrashCheck.DAL;

namespace WebTrashCheck.Users
{
    public partial class SSH : System.Web.UI.Page
    {
        MembershipUser currentUser;
        LogWorker loger = new LogWorker();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {   
                rbtnRebootCash.Checked = true;
                rbtnRebootCash_CheckedChanged(this, e);
                FillShopList();
            }
            if (Roles.IsUserInRole("Shop"))
            {
                HideElementsFromShopUsers();
            }
        }

        private void HideElementsFromShopUsers()
        {
            rbtnChangeDateToCurrent.Visible = false;
            rbtnRebootUKM.Visible = false;
            rbtnShowEquipment.Visible = false;
        }

        private void FillShopList()
        {
            MembershipUser currentUser = Membership.GetUser();
            if (!Roles.IsUserInRole("Shop"))
            {
                foreach (RetailClient client in RetailClient.GetAllCurrentClients().OrderBy(o => int.Parse(o.MagNumber)))
                    ddlShopID.Items.Add(client.MagNumber);
            }
            else
            {
                ddlShopID.Items.Add(currentUser.UserName.Trim('m','a','g'));
            }
            
            ddlShopID_SelectedIndexChanged(this, new EventArgs());
            ddlCash_SelectedIndexChanged(this, new EventArgs());
        }

        protected void rbtnShowEquipment_CheckedChanged(object sender, EventArgs e)
        {   
            txtbxCommand.Text = "config-serial";
        }

        protected void rbtnShowDate_CheckedChanged(object sender, EventArgs e)
        {
            txtbxCommand.Text = "date";
        }

        protected void rbtnChangeDateToCurrent_CheckedChanged(object sender, EventArgs e)
        {
            string minute = ConfigureDate(DateTime.Now.Minute);
            string hour = ConfigureDate(DateTime.Now.Hour);
            string day = ConfigureDate(DateTime.Now.Day);
            string month = ConfigureDate(DateTime.Now.Month);
            string year = ConfigureDate(DateTime.Now.Year);

            string date = month + day + hour + minute + year;

            txtbxCommand.Text = "date " + date;
        }

        protected void rbtnRebootCash_CheckedChanged(object sender, EventArgs e)
        {
            txtbxCommand.Text = "reboot ";
        }

        protected void rbtnRebootUKM_CheckedChanged(object sender, EventArgs e)
        {
            txtbxCommand.Text = "service ukmclient restart";
        }

        protected void rbtnShutDown_CheckedChanged(object sender, EventArgs e)
        {
            txtbxCommand.Text = "sudo shutdown -h now ";
        }

        private string ConfigureDate(int day)
        {
            if (day < 10)
            {
                return string.Concat("0", day);
            }
            else
            {
                return day.ToString();
            }
        }

        protected void ddlShopID_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCashList(ddlShopID.SelectedItem.ToString());
            txtbxCashIP.Text = "172.20." + ddlShopID.SelectedItem + "." + ddlCash.SelectedItem;
        }

        protected void ddlCash_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtbxCashIP.Text = "172.20." + ddlShopID.SelectedItem + "." + ddlCash.SelectedItem;
        }

        private void FillCashList(string retailClientName)
        {
            ddlCash.Items.Clear();
            RetailClient cl = RetailClient.GetRetailClientpByClientName(retailClientName);
            for (int i = 0; i < cl.CashCount; i++)
                ddlCash.Items.Add((i + 11).ToString());
            ddlCash.SelectedIndex = 0;

        }

        protected void btnRun_Click(object sender, EventArgs e)
        {
            PasswordConnectionInfo connectionInfo = new PasswordConnectionInfo(txtbxCashIP.Text, 22, "root", "xxxxxx");
            connectionInfo.Timeout = TimeSpan.FromSeconds(12);

            if (rbtnChangeDateToCurrent.Checked)
            {
                rbtnChangeDateToCurrent_CheckedChanged(sender, e); // change date after each runs of data chengings
            }

            using (var client = new SshClient(connectionInfo))
            {
                try
                {
                    client.Connect();
                    if (client.IsConnected)
                    {
                        var result = client.RunCommand(txtbxCommand.Text);
                        lblConsole.Text += "<br />" + DateTime.Now.ToString() + "   Команда к   " + txtbxCashIP.Text + "   " + txtbxCommand.Text + "<br />";
                        if (result.Result != string.Empty)
                            lblConsole.Text += "*****************Результат команды*******************<br />"  + result.Result + "<br />";
                        else
                            lblConsole.Text += "***************Готово***************<br />";
                        currentUser = Membership.GetUser();
                        loger.TypeInLogFile("Команда к:" + txtbxCashIP.Text + " комманда: " + txtbxCommand.Text, LogStatus.INFO ,"WebSRV пользователь: " + currentUser.UserName);
                        loger.TypeInLogFile("Команда к:" + txtbxCashIP.Text + " результат: " + result.Result, LogStatus.INFO, "WebSRV пользователь: " + currentUser.UserName);
                    }
                }
                catch (Exception)
                {
                    lblConsole.Text += "<br />" + DateTime.Now.ToString() + "   Команда к   " + txtbxCashIP.Text + "   " + txtbxCommand.Text +  "<br />***Нет связи с оборудованием***<br />";
                }
            }

            if (rbtnRebootCash.Checked || rbtnShutDown.Checked || rbtnChangeDateToCurrent.Checked)
            {
                SendRequest();
            }
        }

        private void SendRequest()
        {
            RetailClient rc = new RetailClient();

            currentUser = Membership.GetUser();
            string userName = currentUser.UserName;

            const string mailToAddress = "help-it@bmk.by";
            string subject = "Default subject";
            string mailToName = "GLPI";
            string whoDidAction = rc.PCtoUsers.ContainsKey(userName) ? rc.PCtoUsers[userName] : userName;
            string body = "Default text";
            string selectedShop = ddlShopID.SelectedItem.ToString();
            if (rbtnRebootCash.Checked || rbtnShutDown.Checked)
            {
                subject = "Перезагрузка кассы " + txtbxCashIP.Text[txtbxCashIP.Text.Length - 1] + " магазина " + selectedShop;
                body = "Касса была перезагружена специалистом " + whoDidAction;
            }
            else if (rbtnChangeDateToCurrent.Checked)
            {
                subject = "Изменение даты на кассе " + txtbxCashIP.Text[txtbxCashIP.Text.Length - 1] + " магазина " + selectedShop;
                body = "Дата на кассе была изменена специалистом " + whoDidAction;
            }

            body += ". Использовался Web Service Helper";

            SendEmail(mailToAddress, subject, mailToName, body);
        }

        private void SendEmail(string mailToAddress, string subject, string mailToName, string body)
        {
            var fromAddress = new MailAddress("servicehelperbmk@gmail.com", "Service Helper БелМаркетКомпани");
            var toAddress = new MailAddress(mailToAddress, mailToName);
            const string fromPassword = "2115147Aa";
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };

            using (MailMessage message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject
            })
            {
                message.IsBodyHtml = true;
                message.Body = body;
                smtp.Send(message);
            }
        }
    }
}
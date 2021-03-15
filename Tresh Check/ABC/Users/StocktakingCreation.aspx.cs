using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WebTrashCheck.ViewElements;

namespace WebTrashCheck.Users
{
    public partial class StocktakingCreation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblSelectedDate.Text = "Дата не выбрана";
                FillDepartmentDDL();
                FillPersonsDDL();
                FillTimeDDL(ddlHours, ddlMinuts);
            }
            ShowOpenedStocktakings();
            ShowClosedStocktakings();
            
        }

        private void FillPersonsDDL()
        {
            ddlMOL.Items.Clear();
            foreach (DataRow row in GetPersonsByDepartment(ddlDepartment.SelectedValue).Tables[0].Rows)
            {
                ddlMOL.Items.Add(row["name"].ToString());
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            DateTime startDate = clndrStocktakingDate.SelectedDate;
            TimeSpan time = new System.TimeSpan(int.Parse(ddlHours.SelectedItem.ToString()), int.Parse(ddlMinuts.SelectedItem.ToString()), 0);
            startDate = startDate.Add(time);

            if (startDate > DateTime.Now && ddlDepartment.SelectedItem.ToString() != string.Empty && ddlMOL.SelectedItem.ToString() != string.Empty)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["csTreshCheckDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string procName = string.Empty;
                    SqlCommand sqlComm = new SqlCommand("[CreateStocktaking]", conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@PersonName", ddlMOL.SelectedValue.ToString());
                    //Configure date to send it in a Stored proc
                    sqlComm.Parameters.AddWithValue("@StartDate", startDate);
                    conn.Open();
                    sqlComm.ExecuteNonQuery();
                    conn.Close();
                }
                Response.Write("<script>alert('Инвентаризация успешно создана');</script>");
                ShowOpenedStocktakings();
                ShowClosedStocktakings();
                FillTimeDDL(ddlHours, ddlMinuts);
            }
            else
            {
                Response.Write("<script>alert('Введена неверная дата и/или не введено подразделение/МОЛ');</script>");
            }
        }

        protected void clndrStocktakingDate_SelectionChanged(object sender, EventArgs e)
        {
            lblSelectedDate.Text = string.Concat("Выбранная дата: ", clndrStocktakingDate.SelectedDate.ToShortDateString(), " ", ddlHours.SelectedItem.ToString(), ":", ddlMinuts.SelectedItem.ToString());
        }

        private DataSet GetPersonsByDepartment(string department)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["csTreshCheckDB"].ConnectionString;
            DataSet ds = new DataSet("Persons");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string procName = string.Empty;
                SqlCommand sqlComm = new SqlCommand("[GetPersonsByDepartment]", conn);
                sqlComm.Parameters.AddWithValue("@DepartmentName", department);                
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                da.Fill(ds);
            }
            return ds;
        }

        private DataSet GetAllDepartments()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["csTreshCheckDB"].ConnectionString;
            DataSet ds = new DataSet("EquipData");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string procName = string.Empty;
                SqlCommand sqlComm = new SqlCommand("[GetAllDepartments]", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                da.Fill(ds);
            }
            return ds;
        }

        private void FillDepartmentDDL()
        {
            ddlDepartment.Items.Clear();
            foreach (DataRow row in GetAllDepartments().Tables[0].Rows)
            {
                ddlDepartment.Items.Add(row["name"].ToString());
            }
        }

        private void ShowOpenedStocktakings()
        {
            DataSet ds = new DataSet();
            string connectionString = ConfigurationManager.ConnectionStrings["csTreshCheckDB"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("SELECT * FROM [TrashCheckDB].[dbo].[OpenedStocktakings] order by Дата_начала desc", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(ds);

            //Configuring Panel for OpenedStocktaking
            TableView tblOpened = new TableView(pnlOpened, ds.Tables[0], "Открытые");
            tblOpened.SortDirect = GetGridViewSortDirection(tblOpened.Caption);
            pnlOpened = tblOpened.PanelTableView;
        }


        //Method For Sorting From VeiwState
        private SortDirection GetGridViewSortDirection(string tblCaption)
        {
            if (ViewState[string.Concat("sortDirection", tblCaption)] == null || (SortDirection)ViewState[string.Concat("sortDirection", tblCaption)] == SortDirection.Descending)
            {
                ViewState[string.Concat("sortDirection", tblCaption)] = SortDirection.Ascending;
                return (SortDirection)ViewState[string.Concat("sortDirection", tblCaption)];
            }
            else
            {
                ViewState[string.Concat("sortDirection", tblCaption)] = SortDirection.Descending;
                return (SortDirection)ViewState[string.Concat("sortDirection", tblCaption)];
            }
        }

        private void ShowClosedStocktakings()
        {
            DataSet ds = new DataSet();
            string connectionString = ConfigurationManager.ConnectionStrings["csTreshCheckDB"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("select * from ClosedStocktakings order by Дата_начала desc", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(ds);

            TableView tblClosed = new TableView(pnlClosed, ds.Tables[0], "Закрытые");
            tblClosed.SortDirect = GetGridViewSortDirection(tblClosed.Caption);
            pnlClosed = tblClosed.PanelTableView;
        }

        private void FillTimeDDL(DropDownList ddlHours, DropDownList ddlMinutes)
        {
            for (int i = 0; i <= 23; i++)
            {
                if (i < 10)
                    ddlHours.Items.Add("0" + i.ToString());
                else
                    ddlHours.Items.Add(i.ToString());
            }

            ddlMinuts.Items.Add("00");
            ddlMinuts.Items.Add("10");
            ddlMinuts.Items.Add("20");
            ddlMinuts.Items.Add("30");
            ddlMinuts.Items.Add("40");
            ddlMinuts.Items.Add("50");
        }

        protected void ddlHours_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSelectedDate.Text = string.Concat("Выбранная дата: ", clndrStocktakingDate.SelectedDate.ToShortDateString(), " ", ddlHours.SelectedItem.ToString(), ":", ddlMinuts.SelectedItem.ToString());
        }

        protected void ddlMinuts_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSelectedDate.Text = string.Concat("Выбранная дата: ", clndrStocktakingDate.SelectedDate.ToShortDateString(), " ", ddlHours.SelectedItem.ToString(), ":", ddlMinuts.SelectedItem.ToString());
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                FillPersonsDDL();
            }
        }
    }
}
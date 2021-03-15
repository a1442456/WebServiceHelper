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
    public partial class ChooseStocktaking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                lblSelectedDate.Text = "Дата не выбрана";
            ShowOpenedStocktakings();
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
            TableView tblOpened = new TableView(pnlStocktakings, ds.Tables[0], "Открытые:");
            tblOpened.SortDirect = GetGridViewSortDirection(tblOpened.Caption);
            pnlStocktakings = tblOpened.PanelTableView;
        }

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

        protected void clndrStocktakingDate_SelectionChanged(object sender, EventArgs e)
        {
            ddlStocktakingDepartment.Items.Clear();
            lblSelectedDate.Text = string.Concat("Выбранная дата: ", clndrStocktakingDate.SelectedDate.ToShortDateString());
            FillDDLFromList(GetAvailableStocktackingsByDate(clndrStocktakingDate.SelectedDate), ddlStocktakingDepartment);
        }

        private List<StockTakingDetails> GetAvailableStocktackingsByDate(DateTime date)
        {
            List<StockTakingDetails> departments = new List<StockTakingDetails>();

            string connectionString = ConfigurationManager.ConnectionStrings["csTreshCheckDB"].ConnectionString;
            DataSet ds = new DataSet("EquipData");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string procName = string.Empty;
                SqlCommand sqlComm = new SqlCommand("GetDepartmentByStocktakingDate", conn);
                sqlComm.Parameters.AddWithValue("@Date", date);
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                da.Fill(ds);
            }

            StockTakingDetails detail = new StockTakingDetails();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                detail.Date = (DateTime)row["start_datetime"];
                detail.Department = (string)row["department_name"];
                detail.Person = (string)row["person_name"];
                departments.Add(detail);
                detail = new StockTakingDetails();
            }

            return departments;
        }

        private void FillDDLFromList(List<StockTakingDetails> list, DropDownList ddl)
        {
            foreach (StockTakingDetails str in list)
            {
                ddl.Items.Add(string.Concat(str.Person, " | ", str.Department, " | ", str.Date.ToString()));
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (clndrStocktakingDate.SelectedDate != null && ddlStocktakingDepartment.SelectedValue != string.Empty)
            {
                StockTakingDetails details = GetStockTakingDetailsFromString(ddlStocktakingDepartment.SelectedItem.ToString());
                Session["StockTakingDetails"] = details;
                Response.Redirect("..\\Users\\StocktakingDetails.aspx");
            }
            else
            {
                Response.Write("<script>alert('Не выбрана дата и/или подразделение');</script>");
            }
        }

        private StockTakingDetails GetStockTakingDetailsFromString(string str)
        {
            StockTakingDetails detail = new StockTakingDetails();
            bool isPersonFilled = false;
            bool isDepartmentFilled = false;
            string dateStr = string.Empty;
            int i = 0;
            while (!isPersonFilled)
            {
                detail.Person += str[i];
                i++;
                if (str[i + 1] == '|')
                {
                    i = i + 3;
                    isPersonFilled = true;
                }
            }

            while (!isDepartmentFilled)
            {
                detail.Department += str[i];
                i++;
                if (str[i + 1] == '|')
                {
                    i = i + 3;
                    isDepartmentFilled = true;
                }
            }
            while (i != str.Length)
            {
                dateStr += str[i];
                i++;
            }

            detail.Date = DateTime.Parse(dateStr);
            return detail;
        }
    }
}
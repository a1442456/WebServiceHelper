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
    public partial class StocktakingDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StockTakingDetails details = new StockTakingDetails();
            details = (StockTakingDetails)Session["StockTakingDetails"];
            lblStockTakingDetails.Text = string.Concat("Детали инвентаризации по</br>", details.Date.ToShortDateString(), ' ', details.Department, ' ', details.Person);
            ShowFoundedEquipment();
            ShowUnFoundedEquipment();
        }

        private void ShowFoundedEquipment()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["csTreshCheckDB"].ConnectionString;

            StockTakingDetails details = new StockTakingDetails();
            details = (StockTakingDetails)Session["StockTakingDetails"];

            DataSet ds = new DataSet("EquipData");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string procName = string.Empty;
                SqlCommand sqlComm = new SqlCommand("GetStocktakingDetailsByPersonAndDate", conn);
                sqlComm.Parameters.AddWithValue("@Date", details.Date);
                sqlComm.Parameters.AddWithValue("@Person", details.Person);
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                da.Fill(ds);
                //grdvw.DataSource = ds.Tables[0];
                //grdvw.DataBind();

                TableView tblFounded = new TableView(pnlFounded, ds.Tables[0], "Найденное Оборудование");
                tblFounded.SortDirect = GetGridViewSortDirection(tblFounded.Caption);
                pnlFounded = tblFounded.PanelTableView;
            }
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

        private void ShowUnFoundedEquipment()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["csTreshCheckDB"].ConnectionString;

            StockTakingDetails details = new StockTakingDetails();
            details = (StockTakingDetails)Session["StockTakingDetails"];

            DataSet ds = new DataSet("EquipData");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string procName = string.Empty;
                SqlCommand sqlComm = new SqlCommand("ShowEquipDoesntExistInStocktaking", conn);
                sqlComm.Parameters.AddWithValue("@Date", details.Date);
                sqlComm.Parameters.AddWithValue("@Person", details.Person);
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                da.Fill(ds);
                //grdvw2.DataSource = ds.Tables[0];
                //grdvw2.DataBind();

                TableView tblUnFounded = new TableView(pnlUnFounded, ds.Tables[0], "Не Найденное Оборудование");
                tblUnFounded.SortDirect = GetGridViewSortDirection(tblUnFounded.Caption);
                pnlUnFounded = tblUnFounded.PanelTableView;
            }
        }

        protected void btnColmplete_Click(object sender, EventArgs e)
        {
            StockTakingDetails details = new StockTakingDetails();
            details = (StockTakingDetails)Session["StockTakingDetails"];

            if (txtbxConfirmationWord.Text == "Завершить")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["csTreshCheckDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string procName = string.Empty;
                    SqlCommand sqlComm = new SqlCommand("[CompleteStocktaking]", conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@Person", details.Person);
                    sqlComm.Parameters.AddWithValue("@StartDate", details.Date);
                    conn.Open();
                    sqlComm.ExecuteNonQuery();
                    conn.Close();
                }
                Response.Write("<script>alert('Инвентаризация завершена');</script>");
                Response.Redirect("..\\Users\\ChooseStocktaking.aspx");
            }
            else
                Response.Write("<script>alert('Не введено завершающее слово');</script>");
        }
    }
}
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

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace WebTrashCheck.Users
{
    public partial class ClosedStocktakingDetails : System.Web.UI.Page
    {
        private string _fontCode39Path = @"D:\FTP\fonts\Code39.ttf";
        private string _pathToFTP = @"D:\FTP\Reports\";

        protected void Page_Load(object sender, EventArgs e)
        {
            StockTakingDetails details = new StockTakingDetails();
            details = (StockTakingDetails)Session["ClosedStockTakingDetails"];
            lblStockTakingDetails.Text = string.Concat("Детали инвентаризации по</br>", details.Date.ToShortDateString(), ' ', details.Department, ' ', details.Person);
            ShowFoundedEquipment();
            ShowUnFoundedEquipment();
        }

        private void ShowFoundedEquipment()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["csTreshCheckDB"].ConnectionString;

            StockTakingDetails details = new StockTakingDetails();
            details = (StockTakingDetails)Session["ClosedStockTakingDetails"];

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
            details = (StockTakingDetails)Session["ClosedStockTakingDetails"];

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

        protected void btnReportUnFounded_Click(object sender, EventArgs e)
        {

        }

        protected void btnReportFounded_Click(object sender, EventArgs e)
        {

        }

        private Document GetReport(DataTable tbl, string reportName)
        {
            BaseFont baseFont = BaseFont.CreateFont(_fontCode39Path, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 15, iTextSharp.text.Font.NORMAL);
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 0, 0, 0, 0);

            Guid guidLink = new Guid();
            guidLink = Guid.NewGuid();
            //PdfWriter writer = PdfWriter.GetInstance(doc, this._pathToFTP + reportName + guidLink.ToString() + ".pdf", FileMode.Create);

            return doc;
        }
    }
}
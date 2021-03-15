using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;

namespace WebTrashCheck.ViewElements
{
    public class TableView
    {
        private const string _ascending = " ASC";
        private const string _descending = " DESC";

        private GridView _grdview = new GridView();
        private Panel _panelTableVew;
        private DataTable _tbl;
        private string _caption = string.Empty;

        public TableView(Panel pnl, DataTable tbl, string caption)
        {
            //new System.Windows.Forms.KeyEventHandler(this.frmCashiers_KeyDown);

            this._grdview.Sorting += new GridViewSortEventHandler(grdvw_Sorting);
            this._panelTableVew = pnl;
            this._tbl = tbl;
            this._caption = caption;
            CreatePanel();
        }

        private void CreatePanel()
        {
            this._panelTableVew.Controls.Clear();
            ConfigureGridView();
            this._panelTableVew.Controls.Add(this._grdview);
        }

        private void ConfigureGridView()
        {
            this._grdview.Caption = this._caption;
            this._grdview.CssClass = "tblEquipments";
            this._grdview.AllowSorting = true;
            this._grdview.DataSource = this._tbl;
            this._grdview.DataBind();

            if (this._grdview.HeaderRow != null) //is header exist? If stored proc returns null(means nothing found), then filter ellements adding is unavailable.
                AddFilters();
            else 
            {
                DataTable tbl = GetEmptyTableState();
                this._grdview.DataSource = tbl;
                this._grdview.DataBind();
                this._grdview.AllowSorting = false;
            }
        }

        private DataTable GetEmptyTableState()
        {
            DataTable dt = new DataTable();            
            dt.Columns.Add("Статус");
            DataRow row = dt.NewRow();
            row["Статус"] = string.Concat("-----------------------Пусто-----------------------");
            dt.Rows.Add(row);
            return dt;
        }

        private void AddFilters()
        {
            TextBox txtbxFilter = new TextBox();
            Label newRow = new Label();
            Button btnFilter = new Button();

            foreach (DataControlFieldHeaderCell cell in this._grdview.HeaderRow.Cells)
            {
                newRow.Text = "<br/>";
                cell.Controls.Add(newRow);
                newRow = new Label();
                newRow.Text = "<br/>";
                txtbxFilter.Width = 85;
                txtbxFilter.Text = "фильтр:";
                txtbxFilter.CssClass = "TableFilter";
                txtbxFilter.Enabled = false;
                cell.Controls.Add(txtbxFilter);
                btnFilter.Text = "▼";
                btnFilter.CssClass = "TableFilter";
                btnFilter.Enabled = false;
                cell.Controls.Add(btnFilter);
                newRow = new Label();
                txtbxFilter = new TextBox();
                btnFilter = new Button();
            }

        }

        private void CreateFilterFields()
        {
 
        }

        //Sorting Property
        public SortDirection SortDirect
        {
            get;
            set;
        }

        //Sortings Methods
        private void SortGridView(string sortExpression, string direction)
        {
            DataView dv = new DataView(this._tbl);
            dv.Sort = string.Concat(sortExpression, direction);

            this._grdview.DataSource = dv;
            this._grdview.DataBind();
            AddFilters();
        }

        //Sortings Event
        protected void grdvw_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            if (SortDirect == SortDirection.Descending)
                SortGridView(sortExpression, _descending);
            else
                SortGridView(sortExpression, _ascending);
        }

        public Panel PanelTableView
        {
            get { return this._panelTableVew; }
        }

        public string Caption
        {
            get { return this._caption; }
        }
    }
}
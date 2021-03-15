using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebTrashCheck.Others
{
    public partial class WebFormTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=IT3m\IT3mdb;Initial Catalog=TrashCheckDB;Integrated Security=True";
            DataSet ds = new DataSet("EquipData");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string procName = string.Empty;
                if (true)
                    procName = "GetdataByEquip";
                SqlCommand sqlComm = new SqlCommand(procName, conn);
                sqlComm.Parameters.AddWithValue("@Name", "%");

                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);

                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }
        }
    }
}
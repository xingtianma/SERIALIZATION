using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SERIALIZATION
{
    public partial class AddJobForm : Form   
    {
        DataGridView table;
        string partName;
        public AddJobForm(DataGridView table)
        {
            this.table = table;
            InitializeComponent();
            partName = "AH";
        }
        public AddJobForm(DataGridView table, string partName)
        {
            this.table = table;
            this.partName = partName;
            InitializeComponent();
            partText.Text = partName;
        }
        String connString = @"Data Source=SQLSERVER;Initial Catalog=SERIALIZATION;Persist Security Info=True;User ID=sa;Password=AATech#101";
        SqlConnection conn = new SqlConnection(@"Data Source=SQLSERVER;Initial Catalog=SERIALIZATION;Persist Security Info=True;User ID=sa;Password=AATech#101");
        private void button1_Click(object sender, EventArgs e)
        { 
            //submits job into table
            string query = "INSERT INTO jobs ([JOB #], [PO #], [PART #], [REV], [QTY BUILD], [DATE CODE])";
            query += " VALUES (@JobNumber, @PONumber, @PartNumber, @Rev, @QtyBuild, @DateCode)";

            string query2 = "INSERT INTO parts([PART #])";
            query2 += "VALUES(@PartNumber)";

            conn.Open();

           if (CheckIfValueExists(connString, partText.Text) == false)
            {
                SqlCommand secCommand = new SqlCommand(query2, conn);
                secCommand.Parameters.AddWithValue("@PartNumber", partText.Text);

                secCommand.ExecuteNonQuery();
            }

            SqlCommand myCommand = new SqlCommand(query, conn);
            myCommand.Parameters.AddWithValue("@JobNumber", jobText.Text);
            myCommand.Parameters.AddWithValue("@PONumber", poText.Text);
            myCommand.Parameters.AddWithValue("@PartNumber", partText.Text);
            myCommand.Parameters.AddWithValue("@Rev", revText.Text);
            myCommand.Parameters.AddWithValue("@QtyBuild", qtyText.Text);
            myCommand.Parameters.AddWithValue("@DateCode", dateText.Text);
            myCommand.ExecuteNonQuery();
            
            conn.Close();

            jobText.Clear();
            poText.Clear();
            partText.Clear();
            revText.Clear();
            qtyText.Clear();
            dateText.Clear();

            table.DataSource = null;
            table.Rows.Clear();

            conn.Open();
            string query3 = "";
            if (partName.Equals("AH"))
            {
                query3 = "SELECT * FROM parts";
            }
            else
            {
                query3 = "SELECT * FROM jobs WHERE [PART #] = @Part";
            }
            SqlCommand command = new SqlCommand(query3, conn);
            command.Parameters.AddWithValue("@Part", partName);

            SqlDataReader reader = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            table.DataSource = dt;
            table.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            table.Columns[7].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            table.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Change this line
            table.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells); // Adjust columns sizing
            table.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells; // Set row auto resizing mode
            table.Refresh();
            reader.Close();
            conn.Close();


        }
        static bool CheckIfValueExists(string connectionString, string searchValue)
        {
            //checks for value so doesn't make multiple part in part table which is a unique key and connected to job table
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM parts WHERE [PART #] = @SearchValue";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SearchValue", searchValue);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }
        }
    }
}

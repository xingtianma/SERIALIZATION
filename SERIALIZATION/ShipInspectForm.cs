﻿using System;
using System.Collections;
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
    public partial class ShipInspectForm : Form
    {
        DataGridView table2;
        string partNumber;
        string poNumber;
        int start;
        int end;
        public ShipInspectForm(string partNumber, string poNumber, int start, int end, DataGridView table2)
        {
            this.table2 = table2;
            this.FormClosed += MyClosedHandler;
            //constructor to show only the serial numbers from the start to end ex( 1- 5)
            InitializeComponent();
            var topLeftHeaderCell = table.TopLeftHeaderCell;
            //designer.cs
            this.partNumber = partNumber;
            this.poNumber = poNumber;
            this.start = start;
            this.end = end;
            loadTable(start, end);
        }
        public ShipInspectForm(string partNumber, string poNumber)
        {
            start = -1;
            this.FormClosed += MyClosedHandler;
            //constructor that shows EVERY serial number for that part #
            //both constructors take in a part and po #
            InitializeComponent();
            this.partNumber = partNumber;
            this.poNumber = poNumber;
            loadTable();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=SQLSERVER;Initial Catalog=SERIALIZATION;Persist Security Info=True;User ID=sa;Password=AATech#101");
        protected override void OnHandleCreated(EventArgs e)
        {
            // Touching the TopLeftHeaderCell here prevents
            // System.InvalidOperationException:
            // This operation cannot be performed while
            // an auto-filled column is being resized.

            var topLeftHeaderCell = table.TopLeftHeaderCell;

            base.OnHandleCreated(e);
        }
        private void loadTable(int start, int end)
        {
            //method to load table for certain numberrs
            conn.Open();

            string query = "SELECT [INSPECTED], [SHIPPED], [SERIAL #] FROM shipping WHERE ([PART #] = @partNumber AND [PO #] = @poNumber)" +
                " AND (";
            for (int x = start; x <= end; x++)
            {
                if (x != end)
                {
                    query += "[COUNT] = " + x.ToString() + " OR ";
                }
                else
                {
                    query += "[COUNT] = " + x.ToString();
                }
            }
            query += ")";

            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@partNumber", partNumber);
            command.Parameters.AddWithValue("@poNumber", poNumber);
            SqlDataReader reader = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            table.DataSource = dt;
            table.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            table.Refresh();
            reader.Close();
            conn.Close();

        }
        private void loadTable()
        {
            //method to load table for every single serial number under that part
            start = -1;
            conn.Open();
            string query = "SELECT [INSPECTED], [SHIPPED], [SERIAL #] FROM shipping WHERE [PART #] = @partNumber AND [PO #] = @poNumber";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@partNumber", partNumber);
            command.Parameters.AddWithValue("@poNumber", poNumber);
            SqlDataReader reader = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            table.DataSource = dt;
            table.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            table.Refresh();
            reader.Close();
            conn.Close();

        }
        private void table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ArrayList badList = new ArrayList();

            if (table.SelectedCells.Count > 1)
            {
                foreach (DataGridViewRow row in table.Rows)
                {
                    var inspectBox = row.Cells[0];
                    var shipBox = row.Cells[1];
                    var serial = row.Cells[2].Value.ToString();

                    if (inspectBox.Selected)
                    {
                        inspectBox.Value = !(bool)inspectBox.Value;
                        if ((bool)inspectBox.Value == false)
                        {
                            if ((bool)shipBox.Value == true)
                            {
                                addNoDupe(serial, badList);
                                shipBox.Value = false;
                            }
                        }
                    }
                    if (shipBox.Selected)
                    {
                        shipBox.Value = !(bool)shipBox.Value;
                        if ((bool)shipBox.Value == true)
                        {
                            if ((bool)inspectBox.Value == false)
                            {
                                shipBox.Value = false;
                                addNoDupe(serial, badList);
                            }
                        }
                    }
                }
            }
            else if (table.SelectedCells.Count > 0)
            {

            }

            if (table.CurrentCell.ColumnIndex.Equals(1) && e.RowIndex != -1)
            {
                //if column = 1 (shipped col)
                var senderGrid = (DataGridView)sender;
                senderGrid.EndEdit();

                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex >= 0)
                {
                    conn.Open();
                    var shipBox = (DataGridViewCheckBoxCell)senderGrid.Rows[e.RowIndex].Cells["SHIPPED"];
                    var inspectBox = (DataGridViewCheckBoxCell)senderGrid.Rows[e.RowIndex].Cells["INSPECTED"];
                    var serial = table[2, e.RowIndex].Value.ToString();

                    if ((bool)shipBox.Value == true)
                    {
                        if (inspectBox.Selected == true)
                        {
                            inspectBox.Value = true;
                        }
                        else if ((bool)inspectBox.Value == false)
                        {
                            shipBox.Value = false;
                            addNoDupe(serial, badList);
                        }
                    }
                    else
                    {
                        if (inspectBox.Selected)
                        {
                            Console.WriteLine(serial);
                            inspectBox.Value = false;
                        }
                    }
                    conn.Close();
                }
            }
            else if (table.CurrentCell.ColumnIndex.Equals(0) && e.RowIndex != -1)
            {
                var senderGrid = (DataGridView)sender;
                senderGrid.EndEdit();

                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex >= 0)
                {
                    conn.Open();
                    var shipBox = (DataGridViewCheckBoxCell)senderGrid.Rows[e.RowIndex].Cells["SHIPPED"];
                    var inspectBox = (DataGridViewCheckBoxCell)senderGrid.Rows[e.RowIndex].Cells["INSPECTED"];
                    var serial = table[2, e.RowIndex].Value.ToString();

                    if ((bool)inspectBox.Value == false)
                    {
                        if ((bool)shipBox.Value == true)
                        {
                            shipBox.Value = false;
                        }
                    }
                    else
                    {
                        if (shipBox.Selected == true)
                        {
                            shipBox.Value = true;
                            badList.Remove(serial);
                        }
                    }
                    conn.Close();
                }
            }

            if (badList.Count > 0)
            {
                string output = "THE FOLLOWING WERE UNTICKED : ";

                for (int x = 0; x < badList.Count; x++)
                {
                    output += "\n" + badList[x];
                }

                MessageBox.Show(output);
            }

            foreach (DataGridViewRow row in table.Rows)
            {
                conn.Open();
                var inspectBox = (DataGridViewCheckBoxCell)row.Cells[0];
                var shipBox = (DataGridViewCheckBoxCell)row.Cells[1];

                bool inspected = (bool)row.Cells[0].Value;
                bool shipped = (bool)row.Cells[1].Value;

                string query = "UPDATE shipping SET SHIPPED = @shipped, INSPECTED = @inspected WHERE [SERIAL #] = @serialNumber AND [PO #] = @poNumber AND [PART #] = @partNumber";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@shipped", shipped);
                command.Parameters.AddWithValue("@inspected", inspected);
                command.Parameters.AddWithValue("@serialNumber", row.Cells[2].Value.ToString());
                command.Parameters.AddWithValue("@poNumber", poNumber);
                command.Parameters.AddWithValue("@partNumber", partNumber);
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
        protected void MyClosedHandler(object sender, EventArgs e)
        {
            Console.WriteLine("PDJKMWKMD");
            // Handle the Event here.
            foreach (DataGridViewRow row in table.Rows)
            {
                conn.Open();
                var inspectBox = (DataGridViewCheckBoxCell)row.Cells[0];
                var shipBox = (DataGridViewCheckBoxCell)row.Cells[1];

                bool inspected = (bool)row.Cells[0].Value;
                bool shipped = (bool)row.Cells[1].Value;

                string query = "UPDATE shipping SET SHIPPED = @shipped, INSPECTED = @inspected WHERE [SERIAL #] = @serialNumber AND [PO #] = @poNumber AND [PART #] = @partNumber";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@shipped", shipped);
                command.Parameters.AddWithValue("@inspected", inspected);
                command.Parameters.AddWithValue("@serialNumber", row.Cells[2].Value.ToString());
                command.Parameters.AddWithValue("@poNumber", poNumber);
                command.Parameters.AddWithValue("@partNumber", partNumber);
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
        private void addNoDupe(string str, ArrayList list)
        {
            bool check = false;
            foreach (string aString in list)
            {
                if (list.Contains(str))
                {
                    check = true;
                }
            }
            if (check == false)
            {
                list.Add(str);
            }
        }
        private void addNoDupe(int str, ArrayList list)
        {
            bool check = false;
            foreach (int aString in list)
            {
                if (list.Contains(str))
                {
                    check = true;
                }
            }
            if (check == false)
            {
                list.Add(str);
            }
        }
        private void createTextButton_Click(object sender, EventArgs e)
        {
            ArrayList textList = new ArrayList();
            ArrayList numberList = new ArrayList();
            conn.Open();
            foreach (DataGridViewRow row in table.Rows)
            {
                var inspectBox = (DataGridViewCheckBoxCell)row.Cells[0];
                var shipBox = (DataGridViewCheckBoxCell)row.Cells[1];
                var serial = row.Cells[2].Value.ToString();

                bool inspected = (bool)row.Cells[0].Value;
                bool shipped = (bool)row.Cells[1].Value;

                if (inspected == true && shipped == false)
                {
                    textList.Add(serial);
                }
                string query = "UPDATE shipping SET SHIPPED = @shipped, INSPECTED = @inspected WHERE [SERIAL #] = @serialNumber AND [PO #] = @poNumber AND [PART #] = @partNumber";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@shipped", shipped);
                command.Parameters.AddWithValue("@inspected", inspected);
                command.Parameters.AddWithValue("@serialNumber", row.Cells[2].Value.ToString());
                command.Parameters.AddWithValue("@poNumber", poNumber);
                command.Parameters.AddWithValue("@partNumber", partNumber);
                command.ExecuteNonQuery();
            }

            for (int x = 0; x < textList.Count; x++)
            {
                string query2 = "SELECT [COUNT] FROM shipping WHERE [SERIAL #] = @serialNumber";
                SqlCommand command2 = new SqlCommand(query2, conn);
                command2.Parameters.AddWithValue("@serialNumber", textList[x].ToString());

                using (SqlDataReader reader = command2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        addNoDupe(reader.GetInt32(0), numberList);
                    }
                }
            }
            conn.Close();

            for(int x = 0; x < textList.Count; x++)
            {
                Console.WriteLine(textList[x].ToString());
                Console.WriteLine(numberList[x].ToString());
            }
            string output = "S/N ";
            if (textList.Count == 0)
            {
                MessageBox.Show("NOTHING INSPECTED");
            }
            else if (textList.Count == 1)
            {
                output += textList[0];
            }
            else
            {
                int count = 0;
                output += textList[0] + ", " + textList[1];
                bool test = false;
                for (int x = 1; x < textList.Count; x++)
                {
                    if (x == textList.Count - 1)
                    {
                        if (test == true && count == 1)
                        {
                            output += ", " + textList[textList.Count - 1];
                        }
                        else if (test == true)
                        {
                            output += " TO " + textList[textList.Count - 1];
                        }
                        else
                        {
                            if (output.IndexOf((string)textList[textList.Count - 1]) == -1)
                            {
                                output += ", " + textList[textList.Count - 1];
                            }
                        }
                    }
                    else if ((int)numberList[x + 1] == (int)numberList[x] + 1)
                    {
                        test = true;
                        count++;
                    }
                    else
                    {
                        if(test == true && count == 1)
                        {
                            output += ", " + textList[x] + ", " + textList[x + 1];
                        }
                        else if (test == true)
                        {
                            output += " TO " + textList[x] + ", " + textList[x + 1];
                        }
                        else
                        {
                            output += ", " + textList[x + 1];
                        }
                        test = false;
                        count = 0;
                    }
                }
            }

            Console.WriteLine(output);

            conn.Open();
            string query3 = "UPDATE jobs SET [TEXT FORMAT] = @textFormat WHERE [PART #] = @partNumber AND [PO #] = @poNumber AND [SERIAL RANGE] = @serialRange";
            SqlCommand command3 = new SqlCommand(query3, conn);

            command3.Parameters.AddWithValue("@textFormat", output);
            command3.Parameters.AddWithValue("@partNumber", partNumber);
            command3.Parameters.AddWithValue("@poNumber", poNumber);
            command3.Parameters.AddWithValue("@serialRange", start.ToString() + " - " +  end.ToString());
            command3.ExecuteNonQuery();
            conn.Close();

            Console.WriteLine(start.ToString() + " - " + end.ToString());

            conn.Open();
            string currentPart = table[2, 0].Value.ToString();
            table2.DataSource = null;
            table2.Rows.Clear();

            string query4 = "SELECT * FROM jobs WHERE [PART #] = @partNumber";
            SqlCommand command4 = new SqlCommand(query4, conn);
            command4.Parameters.AddWithValue("@partNumber", partNumber);
            SqlDataReader reader4 = command4.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader4);

            table2.DataSource = dt;
            table2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            table2.Columns[7].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            table2.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            table2.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            table2.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells);
            table2.Refresh();
            conn.Close();
        }
    }
}

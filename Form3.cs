using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihan_layout
{
    public partial class Form3 : Form
    {
        public string DataForm2 { get; set; }
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // Load the data from the Excel sheet into the DataGridView
            LoadTable();
            // Set the Timer interval to 5 seconds (5000 milliseconds)
            timer1.Interval = 1000;
            // Start the Timer
            timer1.Start();

            textBox1.Text = (DataForm2 + Environment.NewLine);
        }

        private void LoadTable()
        {
            OleDbConnection myconnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source = 'C:\Users\Theo\Desktop\Test.xlsx';Extended Properties='Excel 12.0;HDR=YES';");
            OleDbDataAdapter oda = new OleDbDataAdapter("Select * from [sheet1$]", myconnection);
            DataSet ds = new DataSet();
            oda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            myconnection.Close();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            int firstDisplayedRowIndex = dataGridView1.FirstDisplayedScrollingRowIndex;
            int firstDisplayedColumnIndex = dataGridView1.FirstDisplayedScrollingColumnIndex;
            LoadTable();
            if (firstDisplayedRowIndex > 0 && firstDisplayedRowIndex < dataGridView1.Rows.Count)
                dataGridView1.FirstDisplayedScrollingRowIndex = firstDisplayedRowIndex;
            if (firstDisplayedColumnIndex > 0 && firstDisplayedColumnIndex < dataGridView1.Columns.Count)
                dataGridView1.FirstDisplayedScrollingColumnIndex = firstDisplayedColumnIndex;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using DevExpress.Utils.MVVM;
using DevExpress.XtraGauges.Win.Gauges.Circular;
using DevExpress.XtraGauges.Win.Gauges.Digital;
using DevExpress.XtraGauges.Win.Base;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

//Tambah komen

namespace latihan_layout
{
    
    public partial class Form1 : Form
    {
        private SerialPort myport;

        private string mydata;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] portnames = SerialPort.GetPortNames();
            port_cb.Items.AddRange(portnames);
            int[] baudRates = new int[] { 300, 600, 1200, 2400, 4800, 9600, 14400, 19200, 38400, 57600, 115200 };
            foreach (int baudRate in baudRates)
            {
                baudrate_cb.Items.Add(baudRate);
            }
            data_tb.AcceptsReturn = true;
        }

        private void start_btn_Click(object sender, EventArgs e)
        {
            myport = new SerialPort();
            myport.BaudRate = Convert.ToInt32(baudrate_cb.Text);
            myport.PortName = port_cb.Text;
            myport.Parity = Parity.None;
            myport.DataBits = 8;
            myport.StopBits = StopBits.One;
            myport.DataReceived += myport_DataReceived;
            try
            {
                myport.Open();
                data_tb.Text = "";
                indikator.BackColor = Color.Green;
                indikator_lb.Text = "Connected";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Gagal Terhubung");
            }
        }
        void myport_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            this.Invoke(new EventHandler(displaydata_event));
            mydata = myport.ReadLine();
            //Split Data
            string[] datasplit = mydata.Split(',');
            //jumlah length datasplit sama dengan jumlah ada berapa parameter yang digunakan

            //if (datasplit.Length >= 3)
            //{
            //    for (int i = 0; i < datasplit.Length; i++)
            //    {
            //        tb1.Invoke((MethodInvoker)(() => tb1.AppendText(Environment.NewLine + datasplit[0])));
            //        if (i == 1)
            //        {
            //            tb2.Invoke((MethodInvoker)(() => tb2.AppendText(Environment.NewLine + datasplit[1])));
            //        }
            //        if (i == 2)
            //        {
            //            tb3.Invoke((MethodInvoker)(() => tb3.AppendText(Environment.NewLine + datasplit[2])));
            //        }
            //    }
            //}

       
            //di sini arcScaleNeedle butuh format Value yang mana adalah float
            arcScaleNeedleComponent1.Value = float.Parse(datasplit[0]);
            arcScaleNeedleComponent4.Value = float.Parse(datasplit[1]);
            arcScaleNeedleComponent3.Value = float.Parse(datasplit[2]);


            //Di Invoke Karena Error = 'Cross-thread operation not valid: Control 'tb1' accessed from a thread other than the thread it was created on.'
            //tb1.Invoke((MethodInvoker)(() => tb1.AppendText(Environment.NewLine + datasplit[0])));
            //tb2.Invoke((MethodInvoker)(() => tb2.AppendText(Environment.NewLine + datasplit[1])));
        }

        private void displaydata_event(object sender, EventArgs e)
        {
            //Environment.NewLine untuk membuat baris baru saat mengeprint
            data_tb.AppendText(mydata + Environment.NewLine);
        }

        private void stop_btn_Click(object sender, EventArgs e)
        {
            myport.Close();
            data_tb.Text = "";
            port_cb.Text = "";
            baudrate_cb.Text = "";
            arcScaleNeedleComponent1.Value = 0;
            arcScaleNeedleComponent4.Value = 0;
            arcScaleNeedleComponent3.Value = 0;
            indikator.BackColor= Color.Red;
            indikator_lb.Text = "Disconnected";
        }
    }
}

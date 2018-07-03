using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDF
{
    public partial class Home : Form
    {
        public Home()
        {            
            InitializeComponent();

            System.Windows.Forms.Timer MyTimer = new System.Windows.Forms.Timer();
            MyTimer.Interval = (1 * 60 * 1000); // 1 mins
            MyTimer.Tick += new EventHandler(button1_Click);
            MyTimer.Start();
        }
        private DataTable GetData(string query)
        {
            string conString = ConfigurationManager.ConnectionStrings["PDFEntiti"].ConnectionString;
            SqlCommand cmd = new SqlCommand(query);
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;

                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }        
        private void button1_Click(object sender, EventArgs e)
        {          
            if (RB_EDII.Checked == true)
            {
                if(RB_SSP.Checked == true)
                {
                    string query = $"Select * From SSP Where TanggalBayar between '{dateFrom.Value}' and '{dateTo.Value}'";
                    DataTable da = GetData(query);
                    for (int q = 0; q < da.Rows.Count; q++)
                    {
                        var pdfnew = new SSP();
                        pdfnew.pdfBPNSSP(da.Rows[q]);
                        pdfnew.pdfSSP(da.Rows[q]);
                    }
                }
                else
                {
                    string query = $"Select * From SSCP Where StartContract between '{dateFrom.Value}' and '{dateTo.Value}'";
                    DataTable da = GetData(query);
                    for (int q = 0; q < da.Rows.Count; q++)
                    {
                        var pdfnew = new SSCP();
                        pdfnew.pdfBPN(da.Rows[q]);
                        pdfnew.pdfSSCP(da.Rows[q]);
                    }
                }
            }
            else
            {}

        }
    }
}

using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDF
{
    public partial class FormSSCP : Form
    {
        public FormSSCP()
        {
            InitializeComponent();
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

        private void btnBPN_Click(object sender, EventArgs e)
        {
            string query = "Select * From SSPCP Where Id = 1";
            DataTable da = GetData(query);
            for (int q = 0; q < da.Rows.Count; q++)
            {
                var pdfnew = new SSCP();
                pdfnew.pdfBPN(da.Rows[q]);
            }
        }

        private void btnSSCP1_Click(object sender, EventArgs e)
        {
            string query = "Select * From SSPCP Where Id = 1";
            DataTable da = GetData(query);
            for (int q = 0; q < da.Rows.Count; q++)
            {
                var pdfnew = new SSCP();
                pdfnew.pdfSSCP(da.Rows[q]);
            }
        }
    }
}

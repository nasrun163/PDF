using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;

namespace PDF
{
    public partial class FormPrint : Form
    {
        public FormPrint()
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
        private void btnPrint_Click(object sender, EventArgs e)
        {
            string query = "Select * From SSP Where Id = 1";            
            DataTable da = GetData(query);
            for (int q = 0; q < da.Rows.Count; q++)
            {                
                var pdfnew = new SSP();
                pdfnew.pdfBPNSSP(da.Rows[q]);             
            }
        }      
        private void btnPrintSSP_Click(object sender, EventArgs e)
        {
            string query = "Select * From SSP Where Id = 1";
            DataTable da = GetData(query);
            for (int q = 0; q < da.Rows.Count; q++)
            {
                var pdfnew = new SSP();
                pdfnew.pdfSSP(da.Rows[q]);
            }                       
        }


        //private void button1_Click(object sender, EventArgs e)
        //{
        //    string query = "Select * From BPNSSP Where MasaPajak = '2015-10-10'";
        //    string conString = ConfigurationManager.ConnectionStrings["PDFEntiti"].ConnectionString;
        //    SqlConnection conDatabase = new SqlConnection(conString);
        //    conDatabase.Open();
        //    SqlCommand com = new SqlCommand("Select COUNT(*) From BPNSSP Where MasaPajak = '2015-10-10'", conDatabase);
        //    object count = com.ExecuteScalar();
        //    for (int q = 0; q < Convert.ToInt32(count); q++)
        //    {
        //        int a = q;
        //        DataRow dr = GetData(query).Rows[q];
        //        try
        //        {
        //            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        //            iTextSharp.text.Font fontnormal = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
        //            iTextSharp.text.Font fontkecil = new iTextSharp.text.Font(bf, 8, iTextSharp.text.Font.NORMAL);
        //            iTextSharp.text.Font fontbold = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD);
        //            iTextSharp.text.Font fontItalic = new iTextSharp.text.Font(bf, 8, iTextSharp.text.Font.ITALIC);
        //            Document document = new Document(PageSize.A4, 10, 10, 5, 5);
        //            PdfWriter.GetInstance(document, new FileStream("D:/Sinau/HasilPrint/Output" + dr["TransaksiBank"] + ".pdf", FileMode.Create));
        //            document.Open();

        //            #region HEADER
        //            //=======================================HEADER========================================================                      
        //            Paragraph a1 = new Paragraph("Standard Chartered Bank", fontkecil);
        //            a1.SpacingAfter = 0;
        //            Paragraph a2 = new Paragraph("Menara Standard Chartered Bank", fontkecil);
        //            a2.SpacingAfter = 0;
        //            Paragraph a3 = new Paragraph("Jl.Prof.DR Satrio No.164,", fontkecil);
        //            a3.SpacingAfter = 0;
        //            Paragraph a4 = new Paragraph("Jakarta 12930, Indonesia", fontkecil);
        //            a4.SpacingAfter = 0;
        //            document.Add(a1);
        //            document.Add(a2);
        //            document.Add(a3);
        //            document.Add(a4);

        //            Paragraph b = new Paragraph("BUKTI PENERIMAAN NEGARA", fontbold);
        //            b.Alignment = Element.ALIGN_CENTER;
        //            document.Add(b);
        //            Paragraph c = new Paragraph("Penerimaan Pajak - 200000 \n\n\n", fontbold);
        //            c.Alignment = Element.ALIGN_CENTER;
        //            document.Add(c);
        //            #endregion //HEADER

        //            #region BODY
        //            //=======================================BODY========================================================
        //            PdfPTable table = new PdfPTable(2);
        //            table.TotalWidth = 580f;
        //            table.DefaultCell.BorderColor = BaseColor.WHITE;
        //            table.LockedWidth = true;
        //            PdfPCell cellalmt = new PdfPCell(new Phrase("Standard Chartered Bank \nJAKARTA \n0050 / 500306", fontnormal));
        //            cellalmt.BorderColor = BaseColor.WHITE;
        //            table.AddCell(cellalmt);
        //            //table.AddCell("Standard Chartered Bank \nJAKARTA \n0050 / 500306");
        //            var data = Convert.ToDateTime(dr["TanggalBayar"].ToString()).ToString("dd-MM-yyyy");
        //            PdfPCell cellhal = new PdfPCell(new Phrase("" + dr["Id"], fontbold));
        //            cellhal.BorderColor = BaseColor.WHITE;
        //            cellhal.HorizontalAlignment = Element.ALIGN_RIGHT;
        //            table.AddCell(cellhal);
        //            document.Add(table);

        //            Paragraph spasi = new Paragraph("\n");
        //            document.Add(spasi);


        //            PdfPTable tabletgl = new PdfPTable(4);
        //            tabletgl.TotalWidth = 580f;
        //            tabletgl.DefaultCell.BorderColor = BaseColor.WHITE;
        //            tabletgl.LockedWidth = true;
        //            tabletgl.AddCell(new Phrase("Tanggal dan Jam Bayar", fontnormal));
        //            tabletgl.AddCell(new Phrase(": " + data, fontnormal));
        //            tabletgl.AddCell(new Phrase("Transaksi Bank #", fontnormal));
        //            tabletgl.AddCell(new Phrase(":", fontnormal));
        //            tabletgl.AddCell(new Phrase("Tanggal dan Jam Online", fontnormal));
        //            tabletgl.AddCell(new Phrase(":", fontnormal));
        //            tabletgl.AddCell(new Phrase("NTPN", fontnormal));
        //            tabletgl.AddCell(new Phrase(":", fontnormal));
        //            tabletgl.AddCell(new Phrase("Tanggal Lapor", fontnormal));
        //            tabletgl.AddCell(new Phrase(":", fontnormal));
        //            tabletgl.AddCell(new Phrase("Jenis Pelayanan", fontnormal));
        //            tabletgl.AddCell(new Phrase(":", fontnormal));
        //            document.Add(tabletgl);

        //            document.Add(spasi);
        //            Paragraph g = new Paragraph("Identitas Pelaku Transaksi", fontItalic);
        //            g.Alignment = Element.ALIGN_RIGHT;
        //            document.Add(g);
        //            document.Add(spasi);

        //            PdfPTable tableIdentitas = new PdfPTable(4);
        //            tableIdentitas.TotalWidth = 580f;
        //            tableIdentitas.DefaultCell.BorderColor = BaseColor.WHITE;
        //            tableIdentitas.LockedWidth = true;
        //            tableIdentitas.AddCell(new Phrase("Identitas #", fontnormal));
        //            PdfPCell EmployeeId = new PdfPCell(new Phrase(": 000" + dr["Id"], fontnormal));
        //            EmployeeId.Colspan = 3;
        //            EmployeeId.BorderColor = BaseColor.WHITE;
        //            tableIdentitas.AddCell(EmployeeId);
        //            tableIdentitas.AddCell(new Phrase("Nama", fontnormal));
        //            tableIdentitas.AddCell(new Phrase(":", fontnormal));
        //            tableIdentitas.AddCell("");
        //            tableIdentitas.AddCell("");
        //            tableIdentitas.AddCell(new Phrase("Alamat", fontnormal));
        //            tableIdentitas.AddCell(new Phrase(":", fontnormal));
        //            tableIdentitas.AddCell("");
        //            tableIdentitas.AddCell("");
        //            tableIdentitas.AddCell(new Phrase("Kota", fontnormal));
        //            tableIdentitas.AddCell(new Phrase(":", fontnormal));
        //            tableIdentitas.AddCell("");
        //            tableIdentitas.AddCell("");
        //            document.Add(tableIdentitas);

        //            document.Add(spasi);
        //            Paragraph h = new Paragraph("Rincian pembayaran untuk disetorkan ke rekening kas negara", fontItalic);
        //            h.Alignment = Element.ALIGN_RIGHT;
        //            document.Add(h);
        //            document.Add(spasi);

        //            PdfPTable tableRincian = new PdfPTable(4);
        //            tableRincian.TotalWidth = 580f;
        //            tableRincian.DefaultCell.BorderColor = BaseColor.WHITE;
        //            tableRincian.LockedWidth = true;
        //            tableRincian.AddCell(new Phrase("Mata Anggaran dan Jenis Setor", fontnormal));
        //            tableRincian.AddCell(new Phrase(": 411126 - 100", fontnormal));
        //            tableRincian.AddCell(new Phrase("PPH", fontnormal));
        //            tableRincian.AddCell("");
        //            tableRincian.AddCell(new Phrase("Masa Pajak", fontnormal));
        //            tableRincian.AddCell(new Phrase(": 10 - 10 - 2015", fontnormal));
        //            tableRincian.AddCell("");
        //            tableRincian.AddCell("");
        //            tableRincian.AddCell(new Phrase("Jumlah Setoran", fontnormal));
        //            tableRincian.AddCell(new Phrase(": 938.512.200", fontnormal));
        //            tableRincian.AddCell(new Phrase("Mata Uang : 360", fontnormal));
        //            tableRincian.AddCell("");
        //            tableRincian.AddCell(new Phrase("Terbilang", fontnormal));
        //            //tableRincian.AddCell(new Phrase(": sembilan ratus tiga puluh delapan juta lima ratus dua belas ribu dua ratus", fontnormal));
        //            PdfPCell terbilang = new PdfPCell(new Phrase(": sembilan ratus tiga puluh delapan juta lima ratus dua belas ribu dua ratus", fontnormal));
        //            terbilang.Colspan = 3;
        //            terbilang.BorderColor = BaseColor.WHITE;
        //            tableRincian.AddCell(terbilang);
        //            tableRincian.AddCell(new Phrase("Nomor Referensi", fontnormal));
        //            tableRincian.AddCell(new Phrase(": 00000", fontnormal));
        //            tableRincian.AddCell("");
        //            tableRincian.AddCell("");
        //            document.Add(tableRincian);

        //            document.Add(spasi);
        //            Paragraph i = new Paragraph("Validasi dan Pengesahan Bank", fontItalic);
        //            i.Alignment = Element.ALIGN_RIGHT;
        //            document.Add(i);

        //            document.Add(spasi);
        //            Paragraph j = new Paragraph("<153060000069><1107071512151207>", fontItalic);
        //            j.Alignment = Element.ALIGN_LEFT;
        //            document.Add(j);
        //            #endregion //BODY

        //            document.Close();
        //        }
        //        catch (Exception ex)
        //        { }
        //    }
        //}

        //public static List<EmployeeDetail> GetAllEmployees()
        //{
        //    using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["BankEntiti"].ConnectionString))
        //    {
        //        if (db.State == ConnectionState.Closed)
        //            db.Open();
        //        string query = $"select EmployeeID, FirstName, LastName, EmailID, City, Country, StartContract, EndContract from Employee where EmployeeID = '1'";
        //        List<EmployeeDetail> list = db.Query<EmployeeDetail>(query, commandType: CommandType.Text).ToList();
        //        return list;
        //    }
        //}
        
    }
}

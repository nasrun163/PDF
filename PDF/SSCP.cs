using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDF
{
    class SSCP
    {
        public void pdfBPN(DataRow dr)
        {
            try
            {
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font Font6 = new iTextSharp.text.Font(bf, 6, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font Font7 = new iTextSharp.text.Font(bf, 7, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font Font7Italic = new iTextSharp.text.Font(bf, 7, iTextSharp.text.Font.ITALIC);
                iTextSharp.text.Font Font8 = new iTextSharp.text.Font(bf, 8, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font Font8Italic = new iTextSharp.text.Font(bf, 8, iTextSharp.text.Font.ITALIC);
                iTextSharp.text.Font Font9 = new iTextSharp.text.Font(bf, 9, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font Font9Italic = new iTextSharp.text.Font(bf, 9, iTextSharp.text.Font.ITALIC);
                iTextSharp.text.Font Font10 = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font Font10Italic = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.ITALIC);
                iTextSharp.text.Font Font10BOLD = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font Font12 = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font Font12BOLD = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font Font18BOLD = new iTextSharp.text.Font(bf, 18, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font Font27BOLD = new iTextSharp.text.Font(bf, 27, iTextSharp.text.Font.BOLD);

                Document document = new Document(PageSize.A4, 30, 55, 5, 5);   //left, rigt, top, botton
                PdfWriter.GetInstance(document, new FileStream("D:/Sinau/HasilPrint/OutputBPNSSPCP"+dr["KodeKPPN"]+".pdf", FileMode.Create));
                document.Open();
                ;
                PdfPCell spasi = new PdfPCell(new Phrase("\n"));
                spasi.BorderWidth = 0f;
                Paragraph Spasi = new Paragraph("\n");

                #region HEADER
                //==================HEADER======================
                string namabank =dr["NamaBank"].ToString().ToUpper();
                Paragraph A = new Paragraph(namabank + "\n"+dr["UnitKKPPN"]+ "\n\n", Font8);
                document.Add(A);
                //==================JUDULL======================                                                 

                PdfPTable header = new PdfPTable(8);
                header.TotalWidth = 515f;
                header.LockedWidth = true;
                header.DefaultCell.BorderColor = BaseColor.WHITE;
                PdfPCell judul = new PdfPCell(new Phrase("BUKTI PENERIMAAN NEGARA IMPOR\nKPBC : "+dr["KodeKantor"]+"- "+dr["NamaKantor"], Font12));
                judul.Colspan = 7;
                judul.HorizontalAlignment = Element.ALIGN_CENTER;
                judul.BorderColor = BaseColor.WHITE;
                header.AddCell(judul);

                PdfPTable halnomor = new PdfPTable(1);
                halnomor.DefaultCell.BorderColor = BaseColor.WHITE;
                PdfPCell nomor = new PdfPCell(new Phrase(""+dr["KodeKPPN"], Font10));
                nomor.HorizontalAlignment = Element.ALIGN_CENTER;
                nomor.BorderColor = BaseColor.WHITE;
                halnomor.AddCell(nomor);

                PdfPCell kodekkpn = new PdfPCell(new Phrase("Kode KKPN", Font6));
                kodekkpn.HorizontalAlignment = Element.ALIGN_CENTER;
                kodekkpn.BorderColor = BaseColor.WHITE;
                halnomor.AddCell(kodekkpn);

                PdfPCell gabung = new PdfPCell(halnomor);
                gabung.BorderColor = BaseColor.WHITE;
                gabung.Padding = 0f;
                header.AddCell(gabung);

                document.Add(header);
                #endregion //HEADER

                #region IDENTITAS
                //==================IDENTITAS======================
                PdfPTable Identitas = new PdfPTable(4);
                Identitas.TotalWidth = 515f;
                Identitas.LockedWidth = true;
                Identitas.DefaultCell.BorderColor = BaseColor.WHITE;
                PdfPCell ket = new PdfPCell(new Phrase("Identitas Transaksi", Font7Italic));
                ket.Colspan = 4;
                ket.BorderWidth = 0f;
                Identitas.AddCell(ket);
                Identitas.AddCell(new Phrase("Tanggal Setor", Font9));
                Identitas.AddCell(new Phrase(": " + Convert.ToDateTime(dr["TanggalSetor"].ToString()).ToString("dd-MM-yyyy"), Font9));
                Identitas.AddCell(new Phrase("Transaksi Bank #", Font9));
                Identitas.AddCell(new Phrase(": "+dr["TransaksiBank"], Font9));

                Identitas.AddCell(new Phrase("Tanggal Buku", Font9));
                Identitas.AddCell(new Phrase(": " + Convert.ToDateTime(dr["TaggalBuku"].ToString()).ToString("dd-MM-yyyy"), Font9));
                Identitas.AddCell(new Phrase("Transaksi MPN #", Font9));
                Identitas.AddCell(new Phrase(": " + dr["TransaksiBank"], Font9));

                Identitas.AddCell(new Phrase("Rekening #", Font9));
                Identitas.AddCell(new Phrase(": " + dr["Rekening"], Font9));
                Identitas.AddCell(new Phrase("Bukti Pengesahan", Font9));
                Identitas.AddCell(new Phrase(": " + dr["BuktiPengesahan"], Font9));

                PdfPCell alert = new PdfPCell(new Phrase("Identitas Pelaku Transaksi Pelunasan Bea & Cukai", Font7Italic));
                alert.BorderColor = BaseColor.WHITE;
                alert.Colspan = 4;
                Identitas.AddCell(alert);

                Identitas.AddCell(new Phrase("Identitas", Font9));
                PdfPCell identitas = new PdfPCell(new Phrase(": " + dr["NomorIdentitas"], Font9));
                identitas.BorderColor = BaseColor.WHITE;
                identitas.Colspan = 3;
                Identitas.AddCell(identitas);
                Identitas.AddCell(new Phrase("Nama", Font9));
                PdfPCell nama = new PdfPCell(new Phrase(": " + dr["NamaIdentitas"], Font9));
                nama.BorderColor = BaseColor.WHITE;
                nama.Colspan = 3;
                Identitas.AddCell(nama);
                Identitas.AddCell(new Phrase("Alamat", Font9));
                PdfPCell alamat = new PdfPCell(new Phrase(": " + dr["AlamatIdentitas"], Font9));
                alamat.BorderColor = BaseColor.WHITE;
                alamat.Colspan = 3;
                Identitas.AddCell(alamat);
                Identitas.AddCell(new Phrase("Kota", Font9));
                PdfPCell kota = new PdfPCell(new Phrase(": "+dr["KotaIdentitas"], Font9));
                kota.BorderColor = BaseColor.WHITE;
                kota.Colspan = 3;
                Identitas.AddCell(kota);

                PdfPCell alertjenis = new PdfPCell(new Phrase("Jenis dan Nomor Dokumen", Font7Italic));
                alertjenis.BorderColor = BaseColor.WHITE;
                alertjenis.Colspan = 4;
                Identitas.AddCell(alertjenis);
                Identitas.AddCell(new Phrase("Jenis Dokument", Font9));
                PdfPCell jenis = new PdfPCell(new Phrase(": 01. "+dr["JenisDocukemnt"], Font9));
                jenis.BorderColor = BaseColor.WHITE;
                jenis.Colspan = 3;
                Identitas.AddCell(jenis);
                Identitas.AddCell(new Phrase("Nomor Dokument", Font9));
                PdfPCell nomordoc = new PdfPCell(new Phrase(": "+dr["NomorDokument"], Font9));
                nomordoc.BorderColor = BaseColor.WHITE;
                nomordoc.Colspan = 2;
                Identitas.AddCell(nomordoc);
                PdfPCell tnggal = new PdfPCell(new Phrase("Tanggal " + Convert.ToDateTime(dr["TanggalDokument"].ToString()).ToString("dd-MM-yyyy"), Font9));
                tnggal.BorderColor = BaseColor.WHITE;
                tnggal.HorizontalAlignment = Element.ALIGN_RIGHT;
                Identitas.AddCell(tnggal);

                document.Add(Identitas);
                document.Add(Spasi);
                #endregion //IDENTITAS                

                #region FOOTER
                //==================FOOTER======================
                Paragraph ket2 = new Paragraph("\n\nRekening Kas Negara dan Pengesahan Bank ", Font7Italic);
                document.Add(ket2);
                Paragraph ket1 = new Paragraph("ID > 018244319058000 > NAMA > METSO MINERALS INDONESIA > NTPN > 0901101214020010 > P11 >101244> P37 >" +
                    "\n152580000159 > KPPN > 031 > BANK > 0050500021 > P7 >0915165522 > P12 > 165522 > P15 > 0916 > KPBC >120800", Font9);
                document.Add(ket1);
                #endregion //FOOTER

                document.Close();
            }
            catch (Exception ex)
            { }
        }
        public void pdfSSCP(DataRow dr)
        {
            try
            {
                BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                BaseFont f = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font FontX = new iTextSharp.text.Font(bf, 16, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font Font6Italic = new iTextSharp.text.Font(bf, 6, iTextSharp.text.Font.ITALIC);
                iTextSharp.text.Font Font7 = new iTextSharp.text.Font(bf, 7, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font Font8 = new iTextSharp.text.Font(bf, 8, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font Font8Italic = new iTextSharp.text.Font(bf, 8, iTextSharp.text.Font.ITALIC);
                iTextSharp.text.Font Font8BOLD = new iTextSharp.text.Font(bf, 8, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font Font9 = new iTextSharp.text.Font(bf, 9, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font Font9BOLD = new iTextSharp.text.Font(bf, 9, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font Font10 = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font Font10Italic = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.ITALIC);
                iTextSharp.text.Font Font10BOLD = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font Font11 = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font Font11BOLD = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font Font12 = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font Font12BOLD = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font Font13 = new iTextSharp.text.Font(bf, 13, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font Font18BOLD = new iTextSharp.text.Font(bf, 18, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font Font27BOLD = new iTextSharp.text.Font(bf, 27, iTextSharp.text.Font.BOLD);
                Document document = new Document(PageSize.A4, 10, 10, 20, 10);   //left, rigt, top, botton
                var writer = PdfWriter.GetInstance(document, new FileStream("D:/Sinau/HasilPrint/OutputSSPCP"+dr["KodeKPPN"]+".pdf", FileMode.Create));
                document.Open();

                PdfPCell spasi = new PdfPCell(new Phrase("\n"));
                spasi.BorderWidth = 0f;
                Paragraph Spasi = new Paragraph("\n");

                #region HEADER
                //========================HEADER=======================
                PdfPTable tableheader = new PdfPTable(3);
                tableheader.TotalWidth = 570f;
                tableheader.LockedWidth = true;
                float[] widths = new float[] { 200f, 170f, 200f };
                tableheader.SetWidths(widths);
                PdfPTable headerkiri = new PdfPTable(1);
                headerkiri.DefaultCell.BorderWidth = 0f;
                headerkiri.AddCell(new Phrase("KEMENTERIAN KEUANGAN R.I.", Font9BOLD));
                headerkiri.AddCell(new Phrase("DIREKTORAT JENDRAL BEA DAN CUKAI", Font9BOLD));
                headerkiri.AddCell(new Phrase("Kantor : "+dr["NamaKantor"], Font8));
                PdfPTable kodekantor = new PdfPTable(1);
                kodekantor.TotalWidth = 150f;
                kodekantor.LockedWidth = true;
                PdfPCell kode = new PdfPCell(new Phrase("Kode Kantor : "+dr["KodeKantor"], Font8));
                kodekantor.AddCell(kode);
                headerkiri.AddCell(kodekantor);
                tableheader.AddCell(headerkiri);

                PdfPCell headertengah = new PdfPCell(new Phrase("SURAT SETORAN\nPABEAN, CUKAI, DAN\nPAJAK (SSPCP)", Font11BOLD));
                headertengah.HorizontalAlignment = Element.ALIGN_CENTER;
                tableheader.AddCell(headertengah);

                PdfPTable headerkanan = new PdfPTable(1);
                headerkanan.DefaultCell.BorderWidth = 0f;
                headerkanan.AddCell(new Phrase("Lembar ke-1  :  Wajib Bayar", Font8));
                headerkanan.AddCell(new Phrase("Lembar ke-2  :  KPPN", Font8));
                headerkanan.AddCell(new Phrase("Lembar ke-3  :  Kantor Bea dan Cukai", Font8));
                headerkanan.AddCell(new Phrase("Lembar ke-4  :  Bank Devisa Persepsi / Bank", Font8));
                headerkanan.AddCell(new Phrase("                          Persepsi / Pos Persepsi", Font8));
                tableheader.AddCell(headerkanan); ;

                document.Add(tableheader);
                #endregion //HEADER

                #region  JENIS PENERIMAAN NEGARA
                //============== JENIS PENERIMAAN NEGARA===============
                PdfPTable tablejenispenerima = new PdfPTable(9);
                tablejenispenerima.TotalWidth = 570f;
                tablejenispenerima.LockedWidth = true;
                tablejenispenerima.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                float[] widthsjenispenerima = new float[] { 200f, 25f, 55f, 25f, 55f, 25f, 55f, 25f, 105f };
                tablejenispenerima.SetWidths(widthsjenispenerima);
                PdfPCell A = new PdfPCell(new Phrase("A. JENIS PENERIMAAN NEGARA", Font9BOLD));
                A.HorizontalAlignment = Element.ALIGN_LEFT;
                tablejenispenerima.AddCell(A);
                #region fungsi Jenis Penerima
                string im = "";
                string eks = "";
                string cuk = "";
                string tertentu = "";
                if(dr["JenisPenerima"].ToString() == "IMPOR")
                {
                    im = "X";
                }
                else if (dr["JenisPenerima"].ToString() == "EKSPOR")
                {
                    eks = "X";
                }
                else if (dr["JenisPenerima"].ToString() == "CUKAI")
                {
                    cuk = "X";
                }
                else
                {
                    tertentu = "X";
                }
                #endregion
                tablejenispenerima.AddCell(new Phrase("" + im, Font13));
                tablejenispenerima.AddCell(new Phrase("IMPOR", Font9BOLD));
                tablejenispenerima.AddCell(new Phrase(""+eks, Font13));
                tablejenispenerima.AddCell(new Phrase("EKSPOR", Font9BOLD));
                tablejenispenerima.AddCell(new Phrase(""+cuk, Font13));
                tablejenispenerima.AddCell(new Phrase("CUKAI", Font9BOLD));
                tablejenispenerima.AddCell(new Phrase(""+tertentu, Font13));
                tablejenispenerima.AddCell(new Phrase("BARANG TERTENTU", Font9BOLD));

                document.Add(tablejenispenerima);
                #endregion // JENIS PENERIMAAN NEGARA

                #region  JENIS IDENTITAS
                //============== JENIS IDENTITAS===============
                PdfPTable tablejenisidentitas = new PdfPTable(7);
                tablejenisidentitas.TotalWidth = 570f;
                tablejenisidentitas.LockedWidth = true;
                tablejenisidentitas.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                float[] widthsjenisidentitas = new float[] { 200f, 25f, 98f, 25f, 99f, 25f, 98f };
                tablejenisidentitas.SetWidths(widthsjenisidentitas);
                PdfPCell B = new PdfPCell(new Phrase("B. JENIS IDENTITAS", Font9BOLD));
                B.HorizontalAlignment = Element.ALIGN_LEFT;
                tablejenisidentitas.AddCell(B);
                #region Fungis JenisIDentitas
                string jenis1 = "";
                string jenis2 = "";
                string jenis3 = "";
                if(dr["JenisIdentitas"].ToString() == "NPWP")
                {
                    jenis1 = "X";
                }
                else if(dr["JenisIdentitas"].ToString() == "PASPOR")
                {
                    jenis2 = "X";
                }
                else
                {
                    jenis3 = "X";
                }
                #endregion
                tablejenisidentitas.AddCell(new Phrase(""+jenis1, Font13));
                tablejenisidentitas.AddCell(new Phrase("NPWP", Font9BOLD));
                tablejenisidentitas.AddCell(new Phrase("" + jenis2));
                tablejenisidentitas.AddCell(new Phrase("PASPOR", Font9BOLD));
                tablejenisidentitas.AddCell(new Phrase("" + jenis3));
                tablejenisidentitas.AddCell(new Phrase("KTP", Font9BOLD));

                document.Add(tablejenisidentitas);
                #endregion // JENIS IDENTITAS

                #region IDENTITAS
                //==============IDENTITAS===============
                PdfPTable tableIndentitasborder = new PdfPTable(1);
                tableIndentitasborder.TotalWidth = 570f;
                tableIndentitasborder.DefaultCell.PaddingTop = 0f;
                tableIndentitasborder.LockedWidth = true;

                PdfPTable tableidentitas = new PdfPTable(3);
                tableidentitas.TotalWidth = 570f;
                tableidentitas.LockedWidth = true;
                tableidentitas.DefaultCell.PaddingTop = 0f;
                tableidentitas.DefaultCell.BorderWidth = 0f;
                float[] widthsidentitas = new float[] { 80f, 10f, 480f };
                tableidentitas.SetWidths(widthsidentitas);

                tableidentitas.AddCell(new Phrase("     NOMOR", Font9));
                tableidentitas.AddCell(new Phrase(":"));

                char[] charnomor = new char[24];
                if (dr["NomorIdentitas"].ToString() != "")
                {                                        
                    charnomor = Convert.ToString(dr["NomorIdentitas"].ToString().PadRight(24, ' ')).ToCharArray();
                }
                PdfPTable kotaknomor = new PdfPTable(24);                
                for (int i = 0; i < 23; i++)
                {                    
                    kotaknomor.AddCell(new Phrase("" + charnomor[i], Font9));                    
                }
                kotaknomor.AddCell(new Phrase("", Font9));
                tableidentitas.AddCell(kotaknomor);

                tableidentitas.AddCell(new Phrase("     NAMA", Font9));
                tableidentitas.AddCell(new Phrase(":", Font9));
                tableidentitas.AddCell(new Phrase(""+dr["NamaIdentitas"], Font9));
                PdfPCell alamat = new PdfPCell(new Phrase("     ALAMAT", Font9));
                alamat.BorderWidth = 0f;
                alamat.PaddingBottom = 7f;
                tableidentitas.AddCell(alamat);
                PdfPCell alamat1 = new PdfPCell(new Phrase(":", Font9));
                alamat1.BorderWidth = 0f;
                alamat1.PaddingBottom = 7f;
                tableidentitas.AddCell(alamat1);
                PdfPCell alamat2 = new PdfPCell(new Phrase(""+dr["AlamatIdentitas"] +"\n"+dr["KotaIdentitas"], Font9));
                alamat2.BorderWidth = 0f;
                alamat2.PaddingBottom = 7f;
                tableidentitas.AddCell(alamat2);
                tableIndentitasborder.AddCell(tableidentitas);
                document.Add(tableIndentitasborder);
                #endregion //IDENTITAS

                #region DOKUMEN DASAR PEMBAYARAN
                //=======================DOKUMEN DASAR PEMBAYARAN=====================
                PdfPTable tableDasarLayout = new PdfPTable(1);
                tableDasarLayout.TotalWidth = 570f;
                tableDasarLayout.LockedWidth = true;
                PdfPTable tableDasar = new PdfPTable(1);
                tableDasar.TotalWidth = 570f;
                tableDasar.LockedWidth = true;
                tableDasar.DefaultCell.BorderWidth = 0f;

                PdfPTable dasar = new PdfPTable(2);
                dasar.TotalWidth = 570f;
                dasar.LockedWidth = true;
                dasar.DefaultCell.PaddingBottom = 10f;
                dasar.DefaultCell.BorderWidth = 0f;
                float[] widthsdasar = new float[] { 180f, 390f };
                dasar.SetWidths(widthsdasar);
                dasar.AddCell(new Phrase("C. DOKUMEN DASAR PEMBAYARAN  : ", Font9BOLD));
                dasar.AddCell(new Phrase("1  - "+dr["JenisDocukemnt"], Font9));
                tableDasar.AddCell(dasar);

                PdfPTable nomordasar = new PdfPTable(2);
                nomordasar.TotalWidth = 570f;
                nomordasar.LockedWidth = true;
                nomordasar.DefaultCell.BorderWidth = 0f;
                float[] widthsnomordasar = new float[] { 390f, 180f };
                nomordasar.SetWidths(widthsnomordasar);
                nomordasar.AddCell(new Phrase("     NOMOR  : "+dr["NomorDokument"], Font9));
                nomordasar.AddCell(new Phrase(" TANGGAL  : " + Convert.ToDateTime(dr["TanggalDokument"].ToString()).ToString("dd-MM-yyyy"), Font9));
                tableDasar.AddCell(nomordasar);

                tableDasarLayout.AddCell(tableDasar);
                document.Add(tableDasarLayout);
                #endregion //DOKUMEN DASAR PEMBAYARAN

                #region PEMBAYARAN PENERIMAAN NEGARA
                //================PEMBAYARAN PENERIMAAN NEGARA==================
                PdfPTable tablePEMBAYARAN = new PdfPTable(3);
                tablePEMBAYARAN.TotalWidth = 570f;
                tablePEMBAYARAN.LockedWidth = true;
                float[] widthsPEMBAYARAN = new float[] { 280f, 80f, 210f };
                tablePEMBAYARAN.SetWidths(widthsPEMBAYARAN);
                PdfPCell judulPembayaran = new PdfPCell(new Phrase("D. PEMBAYARAN PENERIMAAN NEGARA", Font9BOLD));
                judulPembayaran.Colspan = 3;
                tablePEMBAYARAN.AddCell(judulPembayaran);

                PdfPCell AKun = new PdfPCell(new Phrase("AKUN", Font9BOLD));
                AKun.HorizontalAlignment = Element.ALIGN_CENTER;
                tablePEMBAYARAN.AddCell(AKun);
                PdfPCell KODE = new PdfPCell(new Phrase("KODE AKUN", Font9BOLD));
                KODE.HorizontalAlignment = Element.ALIGN_CENTER;
                tablePEMBAYARAN.AddCell(KODE);
                PdfPCell JUMLAH = new PdfPCell(new Phrase("JUMLAH PEMBAYARAN", Font9BOLD));
                JUMLAH.HorizontalAlignment = Element.ALIGN_CENTER;
                tablePEMBAYARAN.AddCell(JUMLAH);

                PdfPTable jenisakun = new PdfPTable(1);
                jenisakun.DefaultCell.BorderWidth = 0f;
                jenisakun.DefaultCell.PaddingLeft = 10f;
                jenisakun.AddCell(new Phrase("Bea Masuk", Font9));
                jenisakun.AddCell(new Phrase("Bea Masuk Ditanggung Pemerintah atas Hibah (SPM) Nihil", Font9));
                jenisakun.AddCell(new Phrase("Bea Masuk Dalam Rangka Kemudahan Impor Tujuan Ekspor (KITE)", Font9));
                jenisakun.AddCell(new Phrase("Bea Masuk Anti Dumping (BMAD)", Font9));
                jenisakun.AddCell(new Phrase("Bea Masuk Imbalan (BMI)", Font9));
                jenisakun.AddCell(new Phrase("Bea Masuk Tindakan Pengamanan (BMTP)", Font9));
                jenisakun.AddCell(new Phrase("Denda Administrasi Pabean", Font9));
                jenisakun.AddCell(new Phrase("Denda Administrasi Atas Pengangkutan Barang Tertentu", Font9));
                jenisakun.AddCell(new Phrase("Pendapatan Pabean lainnya", Font9));
                jenisakun.AddCell(new Phrase("Bea Keluar", Font9));
                jenisakun.AddCell(new Phrase("Denda Administrasi Bea Keluar", Font9));
                jenisakun.AddCell(new Phrase("Bunga Bea Keluar", Font9));
                jenisakun.AddCell(new Phrase("Cukai Hasil Tembakau", Font9));
                jenisakun.AddCell(new Phrase("Cukai Etil Alkohol", Font9));
                jenisakun.AddCell(new Phrase("Cukai Minuman Mengandung Etil Alkohol", Font9));
                jenisakun.AddCell(new Phrase("Pendapatan Cukai lainnya", Font9));
                jenisakun.AddCell(new Phrase("Denda Administrasi Cukai", Font9));
                jenisakun.AddCell(new Phrase("PNBP/Pendapatan DJBC", Font9));
                jenisakun.AddCell(new Phrase("PPN Impor                          NPWP :"+dr["NomorIdentitas"], Font9));
                jenisakun.AddCell(new Phrase("PPN Hasil Tembakau/PPN Dalam Negeri", Font9));
                jenisakun.AddCell(new Phrase("PPnBM Impor                    NPWP :", Font9));
                jenisakun.AddCell(new Phrase("PPh Pasal 22 Impor            NPWP :" + dr["NomorIdentitas"], Font9));
                jenisakun.AddCell(new Phrase("Bunga Penagihan PPN", Font9));
                tablePEMBAYARAN.AddCell(jenisakun);

                PdfPTable kodeakun = new PdfPTable(1);
                kodeakun.DefaultCell.BorderWidth = 0f;
                kodeakun.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                kodeakun.AddCell(new Phrase("412115", Font9));
                kodeakun.AddCell(new Phrase("412115", Font9));
                kodeakun.AddCell(new Phrase("412115", Font9));
                kodeakun.AddCell(new Phrase("412115", Font9));
                kodeakun.AddCell(new Phrase("412115", Font9));
                kodeakun.AddCell(new Phrase("412115", Font9));
                kodeakun.AddCell(new Phrase("412115", Font9));
                kodeakun.AddCell(new Phrase("412115", Font9));
                kodeakun.AddCell(new Phrase("412115", Font9));
                kodeakun.AddCell(new Phrase("412115", Font9));
                kodeakun.AddCell(new Phrase("412115", Font9));
                kodeakun.AddCell(new Phrase("412115", Font9));
                kodeakun.AddCell(new Phrase("412115", Font9));
                kodeakun.AddCell(new Phrase("412115", Font9));
                kodeakun.AddCell(new Phrase("412115", Font9));
                kodeakun.AddCell(new Phrase("412115", Font9));
                kodeakun.AddCell(new Phrase("412115", Font9));
                kodeakun.AddCell(new Phrase("412115", Font9));
                kodeakun.AddCell(new Phrase("412115", Font9));
                kodeakun.AddCell(new Phrase("412115", Font9));
                kodeakun.AddCell(new Phrase("412115", Font9));
                kodeakun.AddCell(new Phrase("412115", Font9));
                kodeakun.AddCell(new Phrase("412115", Font9));
                tablePEMBAYARAN.AddCell(kodeakun);

                PdfPTable jumlahpembayaran = new PdfPTable(2);
                jumlahpembayaran.DefaultCell.BorderWidth = 0f;
                PdfPTable rp = new PdfPTable(1);
                rp.DefaultCell.BorderWidth = 0f;
                rp.AddCell(new Phrase("Rp.", Font9));
                rp.AddCell(new Phrase("Rp.", Font9));
                rp.AddCell(new Phrase("Rp.", Font9));
                rp.AddCell(new Phrase("Rp.", Font9));
                rp.AddCell(new Phrase("Rp.", Font9));
                rp.AddCell(new Phrase("Rp.", Font9));
                rp.AddCell(new Phrase("Rp.", Font9));
                rp.AddCell(new Phrase("Rp.", Font9));
                rp.AddCell(new Phrase("Rp.", Font9));
                rp.AddCell(new Phrase("Rp.", Font9));
                rp.AddCell(new Phrase("Rp.", Font9));
                rp.AddCell(new Phrase("Rp.", Font9));
                rp.AddCell(new Phrase("Rp.", Font9));
                rp.AddCell(new Phrase("Rp.", Font9));
                rp.AddCell(new Phrase("Rp.", Font9));
                rp.AddCell(new Phrase("Rp.", Font9));
                rp.AddCell(new Phrase("Rp.", Font9));
                rp.AddCell(new Phrase("Rp.", Font9));
                rp.AddCell(new Phrase("Rp.", Font9));
                rp.AddCell(new Phrase("Rp.", Font9));
                rp.AddCell(new Phrase("Rp.", Font9));
                rp.AddCell(new Phrase("Rp.", Font9));
                rp.AddCell(new Phrase("Rp.", Font9));
                jumlahpembayaran.AddCell(rp);

                PdfPTable jumlah = new PdfPTable(1);
                jumlah.DefaultCell.BorderWidth = 0f;
                jumlah.DefaultCell.PaddingRight = 20f;
                jumlah.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                jumlah.AddCell(new Phrase("0", Font9));
                jumlah.AddCell(new Phrase("0", Font9));
                jumlah.AddCell(new Phrase("0", Font9));
                jumlah.AddCell(new Phrase("0", Font9));
                jumlah.AddCell(new Phrase("0", Font9));
                jumlah.AddCell(new Phrase("0", Font9));
                jumlah.AddCell(new Phrase("0", Font9));
                jumlah.AddCell(new Phrase("0", Font9));
                jumlah.AddCell(new Phrase("0", Font9));
                jumlah.AddCell(new Phrase("0", Font9));
                jumlah.AddCell(new Phrase("0", Font9));
                jumlah.AddCell(new Phrase("0", Font9));
                jumlah.AddCell(new Phrase("0", Font9));
                jumlah.AddCell(new Phrase("0", Font9));
                jumlah.AddCell(new Phrase("0", Font9));
                jumlah.AddCell(new Phrase("0", Font9));
                jumlah.AddCell(new Phrase("0", Font9));
                jumlah.AddCell(new Phrase("0", Font9));
                jumlah.AddCell(new Phrase("0", Font9));
                jumlah.AddCell(new Phrase("0", Font9));
                jumlah.AddCell(new Phrase("0", Font9));
                jumlah.AddCell(new Phrase("0", Font9));
                jumlah.AddCell(new Phrase("0", Font9));
                jumlahpembayaran.AddCell(jumlah);
                jumlahpembayaran.AddCell("");
                jumlahpembayaran.AddCell("");
                jumlahpembayaran.AddCell("");
                tablePEMBAYARAN.AddCell(jumlahpembayaran);

                document.Add(tablePEMBAYARAN);
                #endregion //PEMBAYARAN PENERIMAAN NEGARA

                #region Masa Pajak
                //================Masa Pajak====================
                PdfPTable tableMasaPajak = new PdfPTable(2);
                tableMasaPajak.DefaultCell.PaddingBottom = 0f;
                tableMasaPajak.TotalWidth = 570f;
                tableMasaPajak.LockedWidth = true;
                tableMasaPajak.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                float[] widthsMasaPajak = new float[] { 360f, 210f };
                tableMasaPajak.SetWidths(widthsMasaPajak);
                
                PdfPTable masa = new PdfPTable(12);
                masa.TotalWidth = 360f;
                masa.LockedWidth = true;
                masa.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                masa.DefaultCell.BorderWidthBottom = 0f;
                PdfPCell msa = new PdfPCell(new Phrase("Masa Pajak", Font9));
                msa.Colspan = 12;
                msa.BorderWidthTop = 0f;
                msa.HorizontalAlignment = Element.ALIGN_CENTER;
                msa.PaddingBottom = 8f;
                masa.AddCell(msa);                                
                masa.AddCell(new Phrase("Jan", Font9));
                masa.AddCell(new Phrase("Feb", Font9));
                masa.AddCell(new Phrase("Mar", Font9));
                masa.AddCell(new Phrase("Apr", Font9));
                masa.AddCell(new Phrase("Mei", Font9));
                masa.AddCell(new Phrase("Jun", Font9));
                masa.AddCell(new Phrase("Jul", Font9));
                masa.AddCell(new Phrase("Ags", Font9));
                masa.AddCell(new Phrase("Sep", Font9));
                masa.AddCell(new Phrase("Okt", Font9));
                masa.AddCell(new Phrase("Nop", Font9));
                masa.AddCell(new Phrase("Des", Font9));                                       
                tableMasaPajak.AddCell(masa);                

                PdfPTable Tahun = new PdfPTable(6);
                Tahun.TotalWidth = 210;
                Tahun.LockedWidth = true;
                Tahun.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                Tahun.DefaultCell.BorderWidthBottom = 0f;
                PdfPCell tahun = new PdfPCell(new Phrase("Tahun", Font9));
                tahun.Colspan = 6;
                tahun.HorizontalAlignment = Element.ALIGN_CENTER;
                tahun.PaddingBottom = 8f;
                tahun.BorderWidth = 0f;
                Tahun.AddCell(tahun);
                Tahun.AddCell(spasi);
                char[] charTahunPajak = { ' ', ' ', ' ', ' ' };
                if (Convert.ToString(dr["TanggalDokument"]) != "")
                {
                    DateTime thun = Convert.ToDateTime(dr["TanggalDokument"]);
                    charTahunPajak = (thun.Year.ToString()).ToCharArray();
                }
                Tahun.AddCell(new Phrase("" + charTahunPajak[0], Font9));
                Tahun.AddCell(new Phrase("" + charTahunPajak[1], Font9));
                Tahun.AddCell(new Phrase("" + charTahunPajak[2], Font9));
                Tahun.AddCell(new Phrase("" + charTahunPajak[3], Font9));
                Tahun.AddCell(spasi);
                tableMasaPajak.AddCell(Tahun);

                document.Add(tableMasaPajak);
                                
                int Bulan = (Convert.ToInt32((Convert.ToDateTime(dr["TanggalDokument"])).Month));                               
                Chunk X1 = new Chunk("" +MasaPajak(Bulan), FontX);                
                document.Add(X1);                          
                #endregion //Masa Pajak

                #region Jumlah Pembayaran
                //===============Jumlah Pembayaran===============
                PdfPTable tablejumlah = new PdfPTable(1);
                tablejumlah.TotalWidth = 570f;
                tablejumlah.LockedWidth = true;
                PdfPTable tablejumlah1 = new PdfPTable(1);
                tablejumlah1.DefaultCell.BorderWidth = 0f;
                int Total = 21815000;
                tablejumlah1.AddCell(new Phrase("E. Jumlah Pembayaran Penerimaan Negara :              Rp.              21,815,000", Font9BOLD));
                tablejumlah1.AddCell(new Phrase("    Dengan huruf :       dua puluh satu juta delapan ratus lima belas  ribu rupiah", Font9BOLD));
                tablejumlah.AddCell(tablejumlah1);

                document.Add(tablejumlah);
                #endregion //Jumlah Pembayaran

                #region Tanda Tangan
                //=================Tanda Tangan===============
                PdfPTable tableTandaTangan = new PdfPTable(2);
                tableTandaTangan.TotalWidth = 570f;
                tableTandaTangan.LockedWidth = true;
                //tableTandaTangan.DefaultCell.PaddingLeft = 10f;

                PdfPTable terima = new PdfPTable(1);
                terima.TotalWidth = 285f;
                terima.DefaultCell.BorderWidth = 0f;
                terima.LockedWidth = true;

                PdfPTable cekterima = new PdfPTable(5);
                cekterima.TotalWidth = 285f;
                cekterima.DefaultCell.BorderWidth = 0f;
                cekterima.LockedWidth = true;
                float[] widthscekterima = new float[] { 50f, 15f, 95f, 15f, 110f };
                cekterima.SetWidths(widthscekterima);
                cekterima.AddCell(new Phrase("Diterima Oleh : ", Font7));
                PdfPCell kotakterima = new PdfPCell(new Phrase("", Font7));
                cekterima.AddCell(kotakterima);
                cekterima.AddCell(new Phrase("Kantor Bea dan Cukai", Font7));
                PdfPCell kotakterima1 = new PdfPCell(new Phrase("", Font7));
                cekterima.AddCell(kotakterima1);
                cekterima.AddCell(new Phrase("Kantor Pos", Font7));
                terima.AddCell(cekterima);

                PdfPTable detailterima = new PdfPTable(2);
                detailterima.TotalWidth = 285f;
                detailterima.DefaultCell.BorderWidth = 0f;
                detailterima.LockedWidth = true;
                float[] widthsdetailsterima = new float[] { 70f, 215f };
                detailterima.SetWidths(widthsdetailsterima);
                detailterima.AddCell(new Phrase("NPWP", Font8));
                detailterima.AddCell(new Phrase(":", Font8));
                detailterima.AddCell(new Phrase("Nama Kantor", Font8));
                detailterima.AddCell(new Phrase(":", Font8));
                detailterima.AddCell(new Phrase("Kode Kantor", Font8));
                detailterima.AddCell(new Phrase(":", Font8));
                detailterima.AddCell(new Phrase("Nomor SSPCP", Font8));
                detailterima.AddCell(new Phrase(":", Font8));
                detailterima.AddCell(new Phrase("Tanggal", Font8));
                detailterima.AddCell(new Phrase(":", Font8));
                terima.AddCell(detailterima);

                PdfPTable ttdpenerima = new PdfPTable(1);
                ttdpenerima.DefaultCell.BorderWidth = 0f;
                ttdpenerima.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                ttdpenerima.AddCell(new Phrase("Cap dan tanda tangan", Font6Italic));
                ttdpenerima.AddCell("\n\n");
                PdfPCell namaterima = new PdfPCell(new Phrase("Nama : ", Font7));
                namaterima.HorizontalAlignment = Element.ALIGN_LEFT;
                namaterima.PaddingLeft = 100f;
                namaterima.BorderWidth = 0f;
                ttdpenerima.AddCell(namaterima);
                terima.AddCell(ttdpenerima);

                tableTandaTangan.AddCell(terima);



                PdfPTable Persepsi = new PdfPTable(1);
                Persepsi.TotalWidth = 285f;
                Persepsi.DefaultCell.BorderWidth = 0f;
                Persepsi.LockedWidth = true;
                Persepsi.DefaultCell.PaddingLeft = 10f;

                PdfPTable cekPersepsi = new PdfPTable(6);
                cekPersepsi.TotalWidth = 285f;
                cekPersepsi.DefaultCell.BorderWidth = 0f;
                cekPersepsi.LockedWidth = true;
                float[] widthsPersepsi = new float[] { 15f, 100f, 15f, 70f, 15f, 70f };
                cekPersepsi.SetWidths(widthsPersepsi);
                PdfPCell kotakpersepsi = new PdfPCell(new Phrase("", Font7));
                cekPersepsi.AddCell(kotakpersepsi);
                cekPersepsi.AddCell(new Phrase("Bank Devisa Persepsi", Font7));
                PdfPCell kotakpersepsi1 = new PdfPCell(new Phrase("", Font7));
                cekPersepsi.AddCell(kotakpersepsi1);
                cekPersepsi.AddCell(new Phrase("Bank Persepsi", Font7));
                PdfPCell kotakpersepsi2 = new PdfPCell(new Phrase("", Font7));
                cekPersepsi.AddCell(kotakpersepsi2);
                cekPersepsi.AddCell(new Phrase("Pos Persepsi", Font7));
                Persepsi.AddCell(cekPersepsi);

                PdfPTable detailPersepsi = new PdfPTable(2);
                detailPersepsi.DefaultCell.BorderWidth = 0f;
                detailPersepsi.AddCell(new Phrase("Nama Bank/Pos", Font8));
                detailPersepsi.AddCell(new Phrase(":", Font8BOLD));
                detailPersepsi.AddCell(new Phrase("Kode Bank/Pos", Font8));
                detailPersepsi.AddCell(new Phrase(":", Font8BOLD));
                detailPersepsi.AddCell(new Phrase("Nomor SSPCP", Font8));
                detailPersepsi.AddCell(new Phrase(":", Font8BOLD));
                detailPersepsi.AddCell(new Phrase("Unit KPPN", Font8));
                detailPersepsi.AddCell(new Phrase(":", Font8BOLD));
                detailPersepsi.AddCell(new Phrase("Tanggal", Font8));
                detailPersepsi.AddCell(new Phrase(":", Font8BOLD));
                Persepsi.AddCell(detailPersepsi);

                PdfPTable ttdPersepsi = new PdfPTable(1);
                ttdPersepsi.DefaultCell.BorderWidth = 0f;
                ttdPersepsi.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                ttdPersepsi.AddCell(new Phrase("Cap dan tanda tangan", Font6Italic));
                ttdPersepsi.AddCell("\n\n");
                PdfPCell namaPersepsi = new PdfPCell(new Phrase("Nama : ", Font7));
                namaPersepsi.HorizontalAlignment = Element.ALIGN_LEFT;
                namaPersepsi.PaddingLeft = 100f;
                namaPersepsi.BorderWidth = 0f;
                ttdPersepsi.AddCell(namaPersepsi);
                Persepsi.AddCell(ttdPersepsi);

                tableTandaTangan.AddCell(Persepsi);

                document.Add(tableTandaTangan);
                #endregion //Tanda Tangan

                #region Footer
                //=============Footer===========
                PdfPTable tableFooter = new PdfPTable(1);
                tableFooter.TotalWidth = 570f;
                tableFooter.LockedWidth = true;

                PdfPTable ketfooter = new PdfPTable(2);
                ketfooter.DefaultCell.BorderWidth = 0f;
                ketfooter.TotalWidth = 570f;
                ketfooter.LockedWidth = true;
                float[] widthsfooter = new float[] { 250f, 320f };
                ketfooter.SetWidths(widthsfooter);
                ketfooter.AddCell(new Phrase("NTB/NTP	:	152580000159", Font9BOLD));
                ketfooter.AddCell(new Phrase("NTPN	:	0901101214020010", Font9BOLD));
                tableFooter.AddCell(ketfooter);

                document.Add(tableFooter);
                #endregion //Footer
                                
                document.Close();
            }
            catch (Exception ex)
            { }
        }
        public static string MasaPajak(int x)
        {
            string X = "";
            string jan = "   X";
            string feb = "           X";
            string mar = "                  X";
            string apr = "                         X";                
            string mei = "                                X";
            string jun = "                                         X";
            string jul = "                                                X";
            string agu = "                                                       X";
            string sep = "                                                               X";
            string okt = "                                                                      X";
            string nov = "                                                                             X";
            string des = "                                                                                     X";
            if(x == 1)
            {
                X = jan;
            }
            else if(x == 2)
            {
                X = feb;
            }
            else if (x == 3)
            {
                X = mar;
            }
            else if (x == 4)
            {
                X = apr;
            }
            else if (x == 5)
            {
                X = mei;
            }
            else if (x == 6)
            {
                X = jun;
            }
            else if (x == 7)
            {
                X = jul;
            }
            else if (x == 8)
            {
                X = agu;
            }
            else if (x == 9)
            {
                X = sep;
            }
            else if (x == 10)
            {
                X = okt;
            }
            else if (x == 11)
            {
                X = nov;
            }
            else if (x == 12)
            {
                X = des;
            }
            else
            {
                X = "";
            }
            return X;
        }
        public static string Terbilang(int x)
        {
            string[] bilangan = { "", "satu", "dua", "tiga", "empat", "lima", "enam", "tujuh", "delapan", "sembilan", "sepuluh", "sebelas" };
            string temp = "";

            if (x < 12)
            {
                temp = " " + bilangan[x];
            }
            else if (x < 20)
            {
                temp = Terbilang(x - 10).ToString() + " belas";
            }
            else if (x < 100)
            {
                temp = Terbilang(x / 10) + " puluh" + Terbilang(x % 10);
            }
            else if (x < 200)
            {
                temp = " seratus" + Terbilang(x - 100);
            }
            else if (x < 1000)
            {
                temp = Terbilang(x / 100) + " ratus" + Terbilang(x % 100);
            }
            else if (x < 2000)
            {
                temp = " seribu" + Terbilang(x - 1000);
            }
            else if (x < 1000000)
            {
                temp = Terbilang(x / 1000) + " ribu" + Terbilang(x % 1000);
            }
            else if (x < 1000000000)
            {
                temp = Terbilang(x / 1000000) + " juta" + Terbilang(x % 1000000);
            }
            return temp;
        }
    }
}

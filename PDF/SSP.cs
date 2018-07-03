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
using System.Globalization;

namespace PDF
{
    public class SSP
    {
        public void pdfBPNSSP(DataRow dr)
        {
            try
            {
                BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font fontnormal = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font fontnormalItalic = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.ITALIC);
                iTextSharp.text.Font fontkecil = new iTextSharp.text.Font(bf, 8, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font fontbold = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font fontItalic = new iTextSharp.text.Font(bf, 8, iTextSharp.text.Font.ITALIC);
                Document document = new Document(PageSize.A4, 10, 10, 5, 5);
                PdfWriter.GetInstance(document, new FileStream("D:/Sinau/HasilPrint/OutputBPNSSP " + dr["TransaksiBank"] + ".pdf", FileMode.Create));
                document.Open();

                #region HEADER
                //=======================================HEADER========================================================                      
                Paragraph a1 = new Paragraph("Standard Chartered Bank", fontkecil);
                a1.SpacingAfter = 0;
                Paragraph a2 = new Paragraph("Menara Standard Chartered Bank", fontkecil);
                a2.SpacingAfter = 0;
                Paragraph a3 = new Paragraph("Jl.Prof.DR Satrio No.164,", fontkecil);
                a3.SpacingAfter = 0;
                Paragraph a4 = new Paragraph("Jakarta 12930, Indonesia", fontkecil);
                a4.SpacingAfter = 0;
                document.Add(a1);
                document.Add(a2);
                document.Add(a3);
                document.Add(a4);

                Paragraph b = new Paragraph("BUKTI PENERIMAAN NEGARA", fontbold);
                b.Alignment = Element.ALIGN_CENTER;
                document.Add(b);
                Paragraph c = new Paragraph("Penerimaan Pajak - 200000 \n\n\n", fontbold);
                c.Alignment = Element.ALIGN_CENTER;
                document.Add(c);
                #endregion //HEADER

                #region BODY
                //=======================================BODY========================================================
                PdfPTable table = new PdfPTable(2);
                table.TotalWidth = 580f;
                table.DefaultCell.BorderColor = BaseColor.WHITE;
                table.LockedWidth = true;
                PdfPCell cellalmt = new PdfPCell(new Phrase("Standard Chartered Bank \nJAKARTA \n0050 / 500306", fontnormal));
                cellalmt.BorderColor = BaseColor.WHITE;
                table.AddCell(cellalmt);
                //table.AddCell("Standard Chartered Bank \nJAKARTA \n0050 / 500306");
                var data = Convert.ToDateTime(dr["TanggalBayar"].ToString()).ToString("dd-MM-yyyy");
                //DateTime cc = Convert.ToDateTime(dr["TanggalBayar"]);
                //or
                //Convert.ToDateTime(dr["TanggalBayar"].ToString()).ToString("MMMM")

                PdfPCell cellhal = new PdfPCell(new Phrase("" + dr["Id"], fontbold));
                cellhal.BorderColor = BaseColor.WHITE;
                cellhal.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.AddCell(cellhal);
                document.Add(table);

                Paragraph spasi = new Paragraph("\n");
                document.Add(spasi);

                #region Waktu Bayar
                PdfPTable tabletgl = new PdfPTable(4);
                tabletgl.TotalWidth = 580f;
                tabletgl.DefaultCell.BorderColor = BaseColor.WHITE;
                tabletgl.LockedWidth = true;
                tabletgl.AddCell(new Phrase("Tanggal dan Jam Bayar", fontnormal));
                tabletgl.AddCell(new Phrase(": " + Convert.ToDateTime(dr["TanggalBayar"].ToString()).ToString("dd-MM-yyyy HH:mm:ss"), fontnormal));
                tabletgl.AddCell(new Phrase("Transaksi Bank #", fontnormal));
                tabletgl.AddCell(new Phrase(": " + dr["TransaksiBank"], fontnormal));
                tabletgl.AddCell(new Phrase("Tanggal dan Jam Online", fontnormal));
                tabletgl.AddCell(new Phrase(": " + Convert.ToDateTime(dr["TanggalOnline"].ToString()).ToString("dd-MM-yyyy HH:mm:ss"), fontnormal));
                tabletgl.AddCell(new Phrase("NTPN", fontnormal));                
                tabletgl.AddCell(new Phrase(": " + dataNTPN(Convert.ToString(dr["NTPN"])), fontnormal));
                tabletgl.AddCell(new Phrase("Tanggal Lapor", fontnormal));
                tabletgl.AddCell(new Phrase(": " + Convert.ToDateTime(dr["TanggalLapor"].ToString()).ToString("dd-MM-yyyy"), fontnormal));
                tabletgl.AddCell(new Phrase("Jenis Pelayanan", fontnormal));
                tabletgl.AddCell(new Phrase(": " + dr["JenisPelayanan"], fontnormal));
                document.Add(tabletgl);
                #endregion // Waktu Bayar

                #region Identitas
                document.Add(spasi);
                Paragraph g = new Paragraph("Identitas Pelaku Transaksi", fontItalic);
                g.Alignment = Element.ALIGN_RIGHT;
                document.Add(g);
                document.Add(spasi);

                PdfPTable tableIdentitas = new PdfPTable(4);
                tableIdentitas.TotalWidth = 580f;
                tableIdentitas.DefaultCell.BorderColor = BaseColor.WHITE;
                tableIdentitas.LockedWidth = true;
                tableIdentitas.AddCell(new Phrase("Identitas #", fontnormal));                
                PdfPCell EmployeeId = new PdfPCell(new Phrase(": " + dataNPWP(Convert.ToString(dr["NPWP"])), fontnormal));
                EmployeeId.Colspan = 3;
                EmployeeId.BorderColor = BaseColor.WHITE;
                tableIdentitas.AddCell(EmployeeId);
                tableIdentitas.AddCell(new Phrase("Nama", fontnormal));                
                //char[] charnama = (dr["Nama"].ToString()).ToCharArray();

                PdfPCell NamaWP = new PdfPCell(new Phrase(": " + dr["NamaWP"], fontnormal));
                NamaWP.BorderWidth = 0f;
                NamaWP.Colspan = 3;
                tableIdentitas.AddCell(NamaWP);                
                tableIdentitas.AddCell(new Phrase("Alamat", fontnormal));
                PdfPCell AlamatWP = new PdfPCell(new Phrase(": " + dr["AlamatWP"], fontnormal));
                AlamatWP.BorderWidth = 0f;
                AlamatWP.Colspan = 3;
                tableIdentitas.AddCell(AlamatWP);
                tableIdentitas.AddCell(new Phrase("Kota", fontnormal));
                PdfPCell KotaWP = new PdfPCell(new Phrase(": " + dr["KotaWP"], fontnormal));
                KotaWP.BorderWidth = 0f;
                KotaWP.Colspan = 3;
                tableIdentitas.AddCell(KotaWP);
                document.Add(tableIdentitas);
                #endregion //Identitas

                #region Rincian Pemabayaran
                document.Add(spasi);
                Paragraph h = new Paragraph("Rincian pembayaran untuk disetorkan ke rekening kas negara", fontItalic);
                h.Alignment = Element.ALIGN_RIGHT;
                document.Add(h);
                document.Add(spasi);

                PdfPTable tableRincian = new PdfPTable(4);
                tableRincian.TotalWidth = 580f;
                tableRincian.DefaultCell.BorderColor = BaseColor.WHITE;
                tableRincian.LockedWidth = true;
                tableRincian.AddCell(new Phrase("Mata Anggaran dan Jenis Setor", fontnormal));
                tableRincian.AddCell(new Phrase(": " + dr["KodeAkunPajak"] + " - " + dr["KodeJenisSetor"], fontnormal));
                PdfPCell UraianPembayaranSSP = new PdfPCell(new Phrase("" + dr["UraianPembayaranSSP"], fontnormal));
                UraianPembayaranSSP.BorderWidth = 0f;
                UraianPembayaranSSP.Colspan = 2;
                tableRincian.AddCell(UraianPembayaranSSP);
                tableRincian.AddCell(new Phrase("Masa Pajak", fontnormal));
                PdfPCell MasaPajak = new PdfPCell(new Phrase(": " + Convert.ToDateTime(dr["MasaPajak"].ToString()).ToString("dd-MM-yyyy"), fontnormal));
                MasaPajak.BorderWidth = 0f;
                MasaPajak.Colspan = 3;
                tableRincian.AddCell(MasaPajak);
                tableRincian.AddCell(new Phrase("Jumlah Setoran", fontnormal));
                tableRincian.AddCell(new Phrase(": " + String.Format(CultureInfo.CreateSpecificCulture("id-id"), "{0:N0}",dr["JumlahSetoran"]), fontnormal));
                PdfPCell MataUang = new PdfPCell(new Phrase("Mata Uang : " + dr["MataUang"], fontnormal));
                MataUang.Colspan = 2;
                MataUang.BorderWidth = 0f;
                tableRincian.AddCell(MataUang);
                tableRincian.AddCell(new Phrase("Terbilang", fontnormal));
                PdfPCell terbilang = new PdfPCell(new Phrase(":" + Terbilang(Convert.ToInt32(dr["JumlahSetoran"])), fontnormalItalic));
                terbilang.Colspan = 3;
                terbilang.BorderColor = BaseColor.WHITE;
                tableRincian.AddCell(terbilang);
                tableRincian.AddCell(new Phrase("Nomor Referensi", fontnormal));
                PdfPCell NomorRefrensi = new PdfPCell(new Phrase(": " + dr["NomorRefrensi"], fontnormal));
                NomorRefrensi.BorderWidth = 0f;
                NomorRefrensi.Colspan = 3;
                tableRincian.AddCell(NomorRefrensi);
                document.Add(tableRincian);
                #endregion //Rincian Pemabayaran

                document.Add(spasi);
                Paragraph i = new Paragraph("Validasi dan Pengesahan Bank", fontItalic);
                i.Alignment = Element.ALIGN_RIGHT;
                document.Add(i);

                document.Add(spasi);
                Paragraph j = new Paragraph("<153060000069><1107071512151207>", fontItalic);
                j.Alignment = Element.ALIGN_LEFT;
                document.Add(j);
                #endregion //BODY

                document.Close();
            }
            catch (Exception ex)
            { }
        }
        public void pdfSSP(DataRow dr)
        {
            try
            {
                BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font Font8 = new iTextSharp.text.Font(bf, 8, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font Font8Italic = new iTextSharp.text.Font(bf, 8, iTextSharp.text.Font.ITALIC);
                iTextSharp.text.Font Font9 = new iTextSharp.text.Font(bf, 9, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font Font10 = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font Font10Italic = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.ITALIC);
                iTextSharp.text.Font Font10BOLD = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font Font11 = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font Font12 = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font Font12BOLD = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font Font18BOLD = new iTextSharp.text.Font(bf, 18, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font Font27BOLD = new iTextSharp.text.Font(bf, 27, iTextSharp.text.Font.BOLD);
                Document document = new Document(PageSize.A4, 10, 10, 80, 5);   //left, rigt, top, botton
                PdfWriter.GetInstance(document, new FileStream("D:/Sinau/HasilPrint/OutputSSP " + dr["TransaksiBank"] + ".pdf", FileMode.Create));
                document.Open();

                string[] KeteranganArsip = { "UNTUK ARSIP WP","UNTUK ARSIP KPP", "UNTUK DILAPORKAN WP KE KPP", "UNTUK BANK PERSEPSI / KANTOR POS & GIRO"};
                for (int i = 1; i < 5; i++)
                {
                    #region HeaderRegion
                    //........................Coloum Header...............
                    PdfPTable tablelayout = new PdfPTable(3);
                    tablelayout.TotalWidth = 550f;
                    tablelayout.LockedWidth = true;
                    float[] widths = new float[] { 230f, 160f, 160f };
                    tablelayout.SetWidths(widths);
                    PdfPTable logo = new PdfPTable(4);
                    logo.DefaultCell.BorderColor = BaseColor.WHITE;
                    string url = "D:/Sinau/PDF/PDF/Image/p.jpg";
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(new Uri(url));
                    jpg.ScaleToFit(200f, 200f);
                    iTextSharp.text.pdf.PdfPCell imgCell1 = new iTextSharp.text.pdf.PdfPCell();
                    imgCell1.AddElement(new Chunk(jpg, 0, 0));
                    imgCell1.PaddingTop = 25f;
                    imgCell1.PaddingRight = 0f;
                    imgCell1.BorderColor = BaseColor.WHITE;
                    logo.AddCell(imgCell1);
                    PdfPCell Depart = new PdfPCell(new Phrase("\nDEPARTEMEN KEUANGAN RI \nDIREKTORAT JENDRAL PAJAK \nKANTOR PELAYANAN PAJAK", Font10));
                    Depart.BorderColor = BaseColor.WHITE;
                    Depart.Colspan = 3;
                    logo.AddCell(Depart);
                    tablelayout.AddCell(logo);

                    PdfPTable ssp = new PdfPTable(1);
                    ssp.DefaultCell.BorderColor = BaseColor.WHITE;
                    PdfPCell surat = new PdfPCell(new Phrase("\nSURAT SETORAN PAJAK", Font12BOLD));
                    surat.BorderColor = BaseColor.WHITE;
                    surat.HorizontalAlignment = Element.ALIGN_CENTER;
                    ssp.AddCell(surat);
                    PdfPCell SSP = new PdfPCell(new Phrase("(SSP)\n\n", Font18BOLD));
                    SSP.BorderColor = BaseColor.WHITE;
                    SSP.HorizontalAlignment = Element.ALIGN_CENTER;
                    ssp.AddCell(SSP);
                    tablelayout.AddCell(ssp);

                    PdfPTable hal = new PdfPTable(2);
                    hal.DefaultCell.BorderColor = BaseColor.WHITE;                    
                    PdfPCell lembar1 = new PdfPCell(new Phrase("\n\nLEMBAR", Font12));
                    lembar1.BorderColor = BaseColor.WHITE;                                                                            
                    hal.AddCell(lembar1);

                    PdfPTable no = new PdfPTable(1);
                    no.TotalWidth = 20f;
                    PdfPCell nohal = new PdfPCell(new Phrase("" + i.ToString(), Font27BOLD));
                    nohal.BorderColor = BaseColor.WHITE;
                    no.AddCell(nohal);
                    hal.AddCell(no);

                    PdfPCell keterangan = new PdfPCell(new Phrase(KeteranganArsip[i - 1] + "\n", Font8));
                    keterangan.BorderColor = BaseColor.WHITE;
                    keterangan.Colspan = 2;
                    hal.AddCell(keterangan);
                    tablelayout.AddCell(hal);                    
                    document.Add(tablelayout);

                    #endregion // end of Header

                    #region ColoumIdentitas
                    //================ColoumIdentitas=================
                    PdfPTable TIden = new PdfPTable(1);
                    TIden.TotalWidth = 550f;
                    TIden.LockedWidth = true;
                    PdfPTable TNWPWP = new PdfPTable(3);
                    float[] widthsIdentitas = new float[] { 110f, 10f, 420f };
                    TNWPWP.SetWidths(widthsIdentitas);
                    TNWPWP.DefaultCell.BorderColor = BaseColor.WHITE;
                    TNWPWP.SpacingBefore = 8f;
                    TNWPWP.SpacingAfter = 10f;
                    TNWPWP.AddCell(new Phrase("   NPWP", Font10BOLD));

                    TNWPWP.AddCell(new Phrase(":", Font10BOLD));

                    PdfPTable kotakNPWP = new PdfPTable(27);
                    kotakNPWP.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    kotakNPWP.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;

                    PdfPCell spasikotak = new PdfPCell(new Phrase(""));
                    spasikotak.BorderColor = BaseColor.WHITE;
                    spasikotak.BorderWidthLeft = 0f;
                    spasikotak.BorderWidthRight = 0f;

                    char[] charNPWP = { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
                    if (Convert.ToString(dr["NPWP"]) != "")
                    {
                        charNPWP = (dr["NPWP"].ToString()).ToCharArray();
                    }
                    kotakNPWP.AddCell(new Phrase("" + charNPWP[0], Font10BOLD));
                    kotakNPWP.AddCell(new Phrase("" + charNPWP[1], Font10BOLD));
                    kotakNPWP.AddCell(spasikotak);
                    kotakNPWP.AddCell(new Phrase("" + charNPWP[2], Font10BOLD));
                    kotakNPWP.AddCell(new Phrase("" + charNPWP[3], Font10BOLD));
                    kotakNPWP.AddCell(new Phrase("" + charNPWP[4], Font10BOLD));
                    kotakNPWP.AddCell(spasikotak);
                    kotakNPWP.AddCell(new Phrase("" + charNPWP[5], Font10BOLD));
                    kotakNPWP.AddCell(new Phrase("" + charNPWP[6], Font10BOLD));
                    kotakNPWP.AddCell(new Phrase("" + charNPWP[7], Font10BOLD));
                    kotakNPWP.AddCell(spasikotak);
                    kotakNPWP.AddCell(new Phrase("" + charNPWP[8], Font10BOLD));
                    kotakNPWP.AddCell(spasikotak);
                    kotakNPWP.AddCell(new Phrase("" + charNPWP[9], Font10BOLD));
                    kotakNPWP.AddCell(new Phrase("" + charNPWP[10], Font10BOLD));
                    kotakNPWP.AddCell(new Phrase("" + charNPWP[11], Font10BOLD));
                    kotakNPWP.AddCell(spasikotak);
                    kotakNPWP.AddCell(new Phrase("" + charNPWP[12], Font10BOLD));
                    kotakNPWP.AddCell(new Phrase("" + charNPWP[13], Font10BOLD));
                    kotakNPWP.AddCell(new Phrase("" + charNPWP[14], Font10BOLD));
                    PdfPCell batas = new PdfPCell(new Phrase(""));
                    batas.Colspan = 7;
                    batas.BorderColor = BaseColor.WHITE;
                    batas.BorderWidthLeft = 0f;
                    kotakNPWP.AddCell(batas);

                    TNWPWP.AddCell(kotakNPWP);
                    //TNWPWP.AddCell(new Phrase(":", Font10));
                    PdfPCell alert = new PdfPCell(new Phrase("Diisi sesuai dengan Nomor Pokok Wajib Pajak yang dimiliki", Font8Italic));
                    alert.Colspan = 3;
                    alert.BorderColor = BaseColor.WHITE;
                    TNWPWP.AddCell(alert);
                    TNWPWP.AddCell(new Phrase("   NAMA WP", Font10BOLD));
                    TNWPWP.AddCell(new Phrase(":", Font10BOLD));
                    TNWPWP.AddCell(new Phrase("" + dr["NamaWP"], Font10));
                    TNWPWP.AddCell(new Phrase("   ALAMAT WP", Font10BOLD));
                    TNWPWP.AddCell(new Phrase(":", Font10BOLD));
                    TNWPWP.AddCell(new Phrase("" + dr["AlamatWP"] + "\n" + dr["KotaWP"], Font10));

                    TIden.AddCell(TNWPWP);
                    document.Add(TIden);

                    #endregion // ColoumIdentitas

                    #region ColoumNOP
                    //================ColoumNOP================= 
                    PdfPTable TNop = new PdfPTable(1);
                    TNop.TotalWidth = 550f;
                    TNop.LockedWidth = true;
                    PdfPTable NOP = new PdfPTable(3);
                    NOP.SpacingAfter = 10f;
                    float[] widthsNOP = new float[] { 110f, 10f, 420f };
                    NOP.SetWidths(widthsNOP);
                    NOP.DefaultCell.BorderColor = BaseColor.WHITE;
                    NOP.AddCell(new Phrase("  NOP", Font10BOLD));
                    NOP.AddCell(new Phrase(":", Font10BOLD));
                    PdfPTable kotakNOP = new PdfPTable(27);
                    kotakNOP.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    kotakNOP.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;

                    char[] charNOP = { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
                    if (Convert.ToString(dr["NOP"]) != "")
                    {
                        charNOP = (dr["NOP"].ToString()).ToCharArray();
                    }
                    kotakNOP.AddCell(new Phrase("" + charNOP[0], Font10BOLD));
                    kotakNOP.AddCell(new Phrase("" + charNOP[1], Font10BOLD));
                    kotakNOP.AddCell(spasikotak);
                    kotakNOP.AddCell(new Phrase("" + charNOP[2], Font10BOLD));
                    kotakNOP.AddCell(new Phrase("" + charNOP[3], Font10BOLD));
                    kotakNOP.AddCell(spasikotak);
                    kotakNOP.AddCell(new Phrase("" + charNOP[4], Font10BOLD));
                    kotakNOP.AddCell(new Phrase("" + charNOP[5], Font10BOLD));
                    kotakNOP.AddCell(new Phrase("" + charNOP[6], Font10BOLD));
                    kotakNOP.AddCell(spasikotak);
                    kotakNOP.AddCell(new Phrase("" + charNOP[7], Font10BOLD));
                    kotakNOP.AddCell(new Phrase("" + charNOP[8], Font10BOLD));
                    kotakNOP.AddCell(new Phrase("" + charNOP[9], Font10BOLD));
                    kotakNOP.AddCell(spasikotak);
                    kotakNOP.AddCell(new Phrase("" + charNOP[10], Font10BOLD));
                    kotakNOP.AddCell(new Phrase("" + charNOP[11], Font10BOLD));
                    kotakNOP.AddCell(new Phrase("" + charNOP[12], Font10BOLD));
                    kotakNOP.AddCell(spasikotak);
                    kotakNOP.AddCell(new Phrase("" + charNOP[13], Font10BOLD));
                    kotakNOP.AddCell(new Phrase("" + charNOP[14], Font10BOLD));
                    kotakNOP.AddCell(new Phrase("" + charNOP[15], Font10BOLD));
                    kotakNOP.AddCell(new Phrase("" + charNOP[16], Font10BOLD));
                    kotakNOP.AddCell(spasikotak);
                    kotakNOP.AddCell(new Phrase("" + charNOP[17], Font10BOLD));
                    PdfPCell batasNOP = new PdfPCell(new Phrase(""));
                    batasNOP.Colspan = 3;
                    batasNOP.BorderColor = BaseColor.WHITE;
                    batasNOP.BorderWidthLeft = 0f;
                    kotakNOP.AddCell(batasNOP);
                    NOP.AddCell(kotakNOP);

                    PdfPCell AlertNop = new PdfPCell(new Phrase("Diisi sesuai dengan Nomor Objek Pajak", Font8Italic));
                    AlertNop.Colspan = 3;
                    AlertNop.BorderColor = BaseColor.WHITE;
                    NOP.AddCell(AlertNop);
                    NOP.AddCell(new Phrase("  ALAMAT NOP", Font10BOLD));
                    NOP.AddCell(new Phrase(":", Font10BOLD));
                    NOP.AddCell(new Phrase("" + dr["AlamatNOP"], Font10));
                    TNop.AddCell(NOP);
                    document.Add(TNop);

                    #endregion //endColoumNOP

                    #region ColoumKode
                    //================ColoumKode================= 
                    PdfPTable TKode = new PdfPTable(2);
                    TKode.TotalWidth = 550f;
                    float[] widthsTKode = new float[] { 250f, 300f };
                    TKode.SetWidths(widthsTKode);
                    TKode.LockedWidth = true;
                    TKode.DefaultCell.PaddingTop = 10f;
                    TKode.DefaultCell.PaddingBottom = 20f;
                    PdfPTable kode = new PdfPTable(2);
                    kode.DefaultCell.BorderColor = BaseColor.WHITE;
                    PdfPCell akun = new PdfPCell(new Phrase("Kode Akun Pajak", Font10BOLD));
                    akun.HorizontalAlignment = Element.ALIGN_CENTER;
                    akun.BorderColor = BaseColor.WHITE;
                    kode.AddCell(akun);
                    PdfPCell jenis = new PdfPCell(new Phrase("Kode Jenis Setor", Font10BOLD));
                    jenis.HorizontalAlignment = Element.ALIGN_CENTER;
                    jenis.BorderColor = BaseColor.WHITE;
                    kode.AddCell(jenis);
                    PdfPTable kotakKodeAkun = new PdfPTable(8);
                    kotakKodeAkun.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    kotakKodeAkun.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;

                    char[] charKodeAkun = { ' ', ' ', ' ', ' ', ' ', ' ' };
                    if (Convert.ToString(dr["KodeAkunPajak"]) != "")
                    {
                        charKodeAkun = (dr["KodeAkunPajak"].ToString()).ToCharArray();
                    }
                    kotakKodeAkun.AddCell(spasikotak);
                    kotakKodeAkun.AddCell(new Phrase("" + charKodeAkun[0], Font10BOLD));
                    kotakKodeAkun.AddCell(new Phrase("" + charKodeAkun[1], Font10BOLD));
                    kotakKodeAkun.AddCell(new Phrase("" + charKodeAkun[2], Font10BOLD));
                    kotakKodeAkun.AddCell(new Phrase("" + charKodeAkun[3], Font10BOLD));
                    kotakKodeAkun.AddCell(new Phrase("" + charKodeAkun[4], Font10BOLD));
                    kotakKodeAkun.AddCell(new Phrase("" + charKodeAkun[5], Font10BOLD));
                    kotakKodeAkun.AddCell(batas);
                    kode.AddCell(kotakKodeAkun);

                    PdfPTable kotakKodeJenis = new PdfPTable(8);
                    kotakKodeJenis.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    kotakKodeJenis.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;

                    kotakKodeJenis.AddCell(spasikotak);
                    kotakKodeJenis.AddCell(spasikotak);
                    kotakKodeJenis.AddCell(spasikotak);
                    char[] charKodeSetor = { ' ', ' ', ' ' };
                    if (Convert.ToString(dr["KodeJenisSetor"]) != "")
                    {
                        charKodeSetor = (dr["KodeJenisSetor"].ToString()).ToCharArray();
                    }
                    kotakKodeJenis.AddCell(new Phrase("" + charKodeSetor[0], Font10BOLD));
                    kotakKodeJenis.AddCell(new Phrase("" + charKodeSetor[1], Font10BOLD));
                    kotakKodeJenis.AddCell(new Phrase("" + charKodeSetor[2], Font10BOLD));
                    kotakKodeJenis.AddCell(batas);
                    kode.AddCell(kotakKodeJenis);

                    TKode.AddCell(kode);
                    TKode.AddCell(new Phrase("   Uraian Pembayaran SSP \n" + dr["UraianPembayaranSSP"], Font10BOLD));
                    document.Add(TKode);
                    #endregion //ColoumKode

                    #region ColoumMasaPajak
                    //================ColoumMasaPajak=================
                    PdfPTable TMasa = new PdfPTable(2);
                    TMasa.TotalWidth = 550f;
                    float[] widthsMasa = new float[] { 390f, 160f };
                    TMasa.SetWidths(widthsMasa);
                    TMasa.LockedWidth = true;
                    PdfPTable TBulan = new PdfPTable(12);
                    TBulan.TotalWidth = 390f;
                    TBulan.LockedWidth = true;
                    PdfPCell masa = new PdfPCell(new Phrase("Masa Pajak", Font10BOLD));
                    masa.Colspan = 12;
                    masa.BorderWidthTop = 0f;
                    masa.BorderWidthLeft = 0f;
                    masa.HorizontalAlignment = Element.ALIGN_CENTER;
                    masa.PaddingBottom = 10f;
                    TBulan.AddCell(masa);
                    PdfPCell Jan = new PdfPCell(new Phrase("Jan", Font10));
                    TBulan.AddCell(Jan);
                    PdfPCell Feb = new PdfPCell(new Phrase("Feb", Font10));
                    TBulan.AddCell(Feb);
                    PdfPCell Mar = new PdfPCell(new Phrase("Mar", Font10));
                    TBulan.AddCell(Mar);
                    PdfPCell Apr = new PdfPCell(new Phrase("Apr", Font10));
                    TBulan.AddCell(Apr);
                    PdfPCell Mei = new PdfPCell(new Phrase("Mei", Font10));
                    TBulan.AddCell(Mei);
                    PdfPCell Jun = new PdfPCell(new Phrase("Jun", Font10));
                    TBulan.AddCell(Jun);
                    PdfPCell Jul = new PdfPCell(new Phrase("Jul", Font10));
                    TBulan.AddCell(Jul);
                    PdfPCell Ags = new PdfPCell(new Phrase("Ags", Font10));
                    TBulan.AddCell(Ags);
                    PdfPCell Sep = new PdfPCell(new Phrase("Sep", Font10));
                    TBulan.AddCell(Sep);
                    PdfPCell Okt = new PdfPCell(new Phrase("Okt", Font10));
                    TBulan.AddCell(Okt);
                    PdfPCell Nov = new PdfPCell(new Phrase("Nov", Font10));
                    TBulan.AddCell(Nov);
                    PdfPCell Des = new PdfPCell(new Phrase("Des", Font10));
                    TBulan.AddCell(Des);

                    int Bulan = (Convert.ToInt32((Convert.ToDateTime(dr["MasaPajak"])).Month)) - 1;
                    string x = "X";
                    for (int j = 0; j < 12; j++)
                    {

                        if (j == Bulan)
                        {
                            PdfPCell bln = new PdfPCell(new Phrase(x));
                            bln.BorderWidth = 0f;
                            bln.HorizontalAlignment = Element.ALIGN_CENTER;
                            TBulan.AddCell(bln);
                        }
                        else
                        {
                            TBulan.AddCell("");
                        }
                    }

                    PdfPCell alertmasa = new PdfPCell(new Phrase("Beri tanda silang (X) pada kolom bulan, sesuai dengan pembayaran untuk masa yang berkenaan", Font8));
                    alertmasa.Colspan = 12;
                    alertmasa.HorizontalAlignment = Element.ALIGN_CENTER;
                    alertmasa.BorderWidthBottom = 0f;
                    alertmasa.BorderWidthLeft = 0f;
                    TBulan.AddCell(alertmasa);
                    TMasa.AddCell(TBulan);

                    PdfPTable Ttahun = new PdfPTable(1);
                    Ttahun.DefaultCell.BorderColor = BaseColor.WHITE;
                    PdfPCell tahun = new PdfPCell(new Phrase("          Tahun Pajak", Font10BOLD));
                    tahun.BorderColor = BaseColor.WHITE;
                    tahun.PaddingBottom = 10f;
                    Ttahun.AddCell(tahun);
                    PdfPTable kotakTahunPajak = new PdfPTable(8);
                    kotakTahunPajak.TotalWidth = 410f;
                    kotakTahunPajak.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    kotakTahunPajak.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
                    kotakTahunPajak.AddCell(spasikotak);
                    char[] charTahunPajak = { ' ', ' ', ' ', ' ' };
                    if (Convert.ToString(dr["MasaPajak"]) != "")
                    {
                        DateTime thun = Convert.ToDateTime(dr["MasaPajak"]);
                        charTahunPajak = (thun.Year.ToString()).ToCharArray();
                    }
                    kotakTahunPajak.AddCell(new Phrase("" + charTahunPajak[0], Font10));
                    kotakTahunPajak.AddCell(new Phrase("" + charTahunPajak[1], Font10));
                    kotakTahunPajak.AddCell(new Phrase("" + charTahunPajak[2], Font10));
                    kotakTahunPajak.AddCell(new Phrase("" + charTahunPajak[3], Font10));
                    kotakTahunPajak.AddCell(batas);

                    Ttahun.AddCell(kotakTahunPajak);
                    Ttahun.AddCell(new Phrase("     Diisi tahun terutangnya pajak", Font8Italic));
                    TMasa.AddCell(Ttahun);

                    document.Add(TMasa);
                    #endregion //ColoumMasaPajak

                    #region ColoumKetetapan
                    //================ColoumKetetapan=================
                    PdfPTable TableTetap = new PdfPTable(1);
                    TableTetap.TotalWidth = 550f;
                    TableTetap.LockedWidth = true;
                    PdfPTable TTetap = new PdfPTable(3);
                    float[] widthsTetap = new float[] { 100f, 10f, 440f };
                    TTetap.SetWidths(widthsTetap);
                    TTetap.TotalWidth = 550f;
                    TTetap.LockedWidth = true;
                    TTetap.DefaultCell.PaddingTop = 8f;
                    TTetap.DefaultCell.BorderWidth = 0f;
                    TTetap.AddCell(new Phrase("Nomor Ketetapan", Font10));
                    TTetap.AddCell(new Phrase(":", Font9));

                    PdfPTable kotakKetetapan = new PdfPTable(26);
                    kotakKetetapan.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    kotakKetetapan.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;

                    char[] charNoRefrensi = { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
                    if (Convert.ToString(dr["NomorRefrensi"]) != "")
                    {
                        charNoRefrensi = (dr["NomorRefrensi"].ToString()).ToCharArray();
                    }
                    kotakKetetapan.AddCell(new Phrase("" + charNoRefrensi[0], Font10BOLD));
                    kotakKetetapan.AddCell(new Phrase("" + charNoRefrensi[1], Font10BOLD));
                    kotakKetetapan.AddCell(new Phrase("" + charNoRefrensi[2], Font10BOLD));
                    kotakKetetapan.AddCell(new Phrase("" + charNoRefrensi[3], Font10BOLD));
                    kotakKetetapan.AddCell(new Phrase("" + charNoRefrensi[4], Font10BOLD));
                    kotakKetetapan.AddCell(spasikotak);
                    kotakKetetapan.AddCell(new Phrase("" + charNoRefrensi[5], Font10BOLD));
                    kotakKetetapan.AddCell(new Phrase("" + charNoRefrensi[6], Font10BOLD));
                    kotakKetetapan.AddCell(new Phrase("" + charNoRefrensi[7], Font10BOLD));
                    kotakKetetapan.AddCell(spasikotak);
                    kotakKetetapan.AddCell(new Phrase("" + charNoRefrensi[8], Font10BOLD));
                    kotakKetetapan.AddCell(new Phrase("" + charNoRefrensi[9], Font10BOLD));
                    kotakKetetapan.AddCell(spasikotak);
                    kotakKetetapan.AddCell(new Phrase("" + charNoRefrensi[10], Font10BOLD));
                    kotakKetetapan.AddCell(new Phrase("" + charNoRefrensi[11], Font10BOLD));
                    kotakKetetapan.AddCell(new Phrase("" + charNoRefrensi[12], Font10BOLD));
                    kotakKetetapan.AddCell(spasikotak);
                    kotakKetetapan.AddCell(new Phrase("" + charNoRefrensi[13], Font10BOLD));
                    kotakKetetapan.AddCell(new Phrase("" + charNoRefrensi[14], Font10BOLD));
                    kotakKetetapan.AddCell(batas);
                    TTetap.AddCell(kotakKetetapan);

                    PdfPCell AlertTetap = new PdfPCell(new Phrase("Diisi sesuai Nomor Ketetapan : STP, SKPKB, SKPKBT", Font8Italic));
                    AlertTetap.BorderWidth = 0f;
                    AlertTetap.PaddingBottom = 10f;
                    AlertTetap.Colspan = 3;
                    AlertTetap.PaddingLeft = 10f;
                    TTetap.AddCell(AlertTetap);

                    TableTetap.AddCell(TTetap);
                    document.Add(TableTetap);
                    #endregion //ColoumKetetapan

                    #region ColoumPemabayaran
                    //================ColoumPemabayaran=================
                    PdfPTable TBayar = new PdfPTable(1);
                    TBayar.TotalWidth = 550f;
                    TBayar.LockedWidth = true;
                    TBayar.DefaultCell.PaddingBottom = 30f;
                    TBayar.DefaultCell.PaddingLeft = 15f;
                    PdfPTable TBayar1 = new PdfPTable(4);
                    TBayar1.TotalWidth = 550f;
                    TBayar1.LockedWidth = true;
                    float[] widthsBayar = new float[] { 110f, 10f, 300f, 130f };
                    TBayar1.SetWidths(widthsBayar);
                    TBayar1.DefaultCell.BorderWidth = 0f;
                    TBayar1.AddCell(new Phrase("Jumlah Pembayaran", Font10BOLD));
                    TBayar1.AddCell(new Phrase(":", Font10BOLD));
                    TBayar1.AddCell(new Phrase("" + String.Format(CultureInfo.CreateSpecificCulture("id-id"), "Rp. {0:N0}", dr["JumlahSetoran"]), Font10BOLD));
                    TBayar1.AddCell(new Phrase("Diisi dengan rupiah penuh", Font8));
                    TBayar1.AddCell(new Phrase("Terbilang", Font10BOLD));
                    TBayar1.AddCell(new Phrase(":", Font10BOLD));
                    PdfPCell terbilang = new PdfPCell(new Phrase("" + Terbilang(Convert.ToInt32(dr["JumlahSetoran"])), Font10Italic));
                    terbilang.Colspan = 2;
                    terbilang.BorderWidth = 0f;
                    TBayar1.AddCell(terbilang);
                    TBayar.AddCell(TBayar1);
                    document.Add(TBayar);
                    #endregion //ColoumPemabayaran

                    #region ColoumTerima
                    //===============ColoumTerima===========================
                    PdfPTable Tterima = new PdfPTable(2);
                    Tterima.TotalWidth = 550f;
                    Tterima.LockedWidth = true;
                    PdfPTable Pkantor = new PdfPTable(1);
                    Pkantor.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    Pkantor.DefaultCell.BorderColor = BaseColor.WHITE;
                    Pkantor.AddCell(new Phrase("Diterima oleh Kantor Penerimaan Pembayaran", Font10BOLD));
                    Pkantor.AddCell(new Phrase("Tanggal " + Convert.ToDateTime(dr["TanggalBayar"].ToString()).ToString("dd-MM-yyyy"), Font10));
                    Pkantor.AddCell(new Phrase("Cap dan tanda tangan", Font8Italic));
                    Pkantor.AddCell("\n\n\n\n");
                    Pkantor.AddCell(new Phrase("Nama Jelas : Standard Chartered Bank\n\n", Font11));
                    Tterima.AddCell(Pkantor);

                    PdfPTable Penyetor = new PdfPTable(1);
                    Penyetor.DefaultCell.BorderColor = BaseColor.WHITE;
                    Penyetor.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    Penyetor.AddCell(new Phrase("Wajib pajak/Penyetor", Font10BOLD));
                    Penyetor.AddCell(new Phrase("" + (dr["KotaWP"]).ToString().Trim() + ", Tanggal " + Convert.ToDateTime(dr["TanggalBayar"].ToString()).ToString("dd-MM-yyyy"), Font10));
                    Penyetor.AddCell(new Phrase("Cap dan tanda tangan", Font8Italic));
                    Penyetor.AddCell("\n\n\n\n");
                    Penyetor.AddCell(new Phrase("Nama Jelas " + dr["NamaWP"] + "\n\n", Font11));
                    Tterima.AddCell(Penyetor);

                    document.Add(Tterima);
                    #endregion //ColoumTerima

                    #region ColoumTerimakasih
                    //=======================ColoumTerimakasih=========================
                    PdfPTable Tterimakasih = new PdfPTable(1);
                    Tterimakasih.TotalWidth = 550f;
                    Tterimakasih.LockedWidth = true;
                    PdfPTable Terimakasih = new PdfPTable(3);
                    float[] widthsTerimakasih = new float[] { 70f, 10f, 470f };
                    Terimakasih.SetWidths(widthsTerimakasih);
                    Terimakasih.DefaultCell.BorderColor = BaseColor.WHITE;
                    PdfPCell terima = new PdfPCell(new Phrase("Terima Kasih Telah Membayar Pajak - Pajak Untuk Pembangunan Bangsa", Font9));
                    terima.Colspan = 3;
                    terima.HorizontalAlignment = Element.ALIGN_CENTER;
                    terima.BorderColor = BaseColor.WHITE;
                    Terimakasih.AddCell(terima);
                    PdfPCell ruang = new PdfPCell(new Phrase("Ruang Validasi Kantor Penerimaan Pembayaran", Font9));
                    ruang.Colspan = 3;
                    ruang.HorizontalAlignment = Element.ALIGN_CENTER;
                    ruang.BorderColor = BaseColor.WHITE;
                    Terimakasih.AddCell(ruang);
                    Terimakasih.AddCell(new Phrase("  Tanggal", Font9));
                    Terimakasih.AddCell(new Phrase(":", Font9));
                    Terimakasih.AddCell(new Phrase("" + Convert.ToDateTime(dr["TanggalBayar"].ToString()).ToString("dd-MM-yyyy HH:mm"), Font9));
                    Terimakasih.AddCell(new Phrase("  Cabang", Font9));
                    Terimakasih.AddCell(new Phrase(":", Font9));
                    Terimakasih.AddCell(new Phrase("", Font9));
                    Terimakasih.AddCell(new Phrase("  Terminal", Font9));
                    Terimakasih.AddCell(new Phrase(":", Font9));
                    Terimakasih.AddCell(new Phrase("", Font9));
                    Terimakasih.AddCell(new Phrase("  Tx No.", Font9));
                    Terimakasih.AddCell(new Phrase(":", Font9));
                    Terimakasih.AddCell(new Phrase("", Font9));
                    Terimakasih.AddCell(new Phrase("  NTPN", Font9));
                    Terimakasih.AddCell(new Phrase(":", Font9));
                    Terimakasih.AddCell(new Phrase("", Font9));
                    PdfPCell spasibwah = new PdfPCell(new Phrase("\n\n\n"));
                    spasibwah.Colspan = 3;
                    spasibwah.BorderWidth = 0f;
                    Terimakasih.AddCell(spasibwah);
                    Tterimakasih.AddCell(Terimakasih);
                    document.Add(Tterimakasih);
                    #endregion //ColoumTerimakasih               
                }
                document.Close();
            }
            catch (Exception ex)
            {
            }
        }
        public static string Terbilang(int x)
        {
            string [] bilangan = {"", "satu", "dua", "tiga", "empat", "lima", "enam", "tujuh", "delapan", "sembilan", "sepuluh", "sebelas" };
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
        public string dataNPWP(string np)
        {                        
            //string detailNPWP = np[0] + "" + np[1] + "-" + np[2] + "" + np[3] + "" + np[4]
            //    + "" + np[5] + "" + np[6] + "" + np[7] + "-" + np[8] + "-" + np[9] +
            //    "" + np[10] + "" + np[11] + "-" + np[12] + "" + np[13] + "" + np[14];
            string npwp0 = np.Insert(12, "-");
            string npwp1 = npwp0.Insert(9, "-");
            string npwp2 = npwp1.Insert(8, "-");
            string npwp = npwp2.Insert(2, "-");
            return npwp;
        }
        public string dataNTPN(string np)
        {
            string NTPN0 = np.Insert(12, " ");
            string NTPN1 = NTPN0.Insert(8, " ");
            string NTPN = NTPN1.Insert(4, " ");            
            return NTPN;
        }
    }
}

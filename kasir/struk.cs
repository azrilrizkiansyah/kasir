using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace kasir
{
    public partial class PrintPreviewControl : Form
    {
<<<<<<< HEAD

        string konfigurasi = "server=localhost;username=root;password=;database=toko_buku";
        public PrintPreviewControl(long idTrans)
        {
            InitializeComponent();

        }
        public static void TampilkanPreviewStruk()
        {
            PrintDocument printDoc = new PrintDocument();

            // Menentukan ukuran kertas struk (lebar: 300, tinggi auto/sesuai kebutuhan)
            printDoc.DefaultPageSettings.PaperSize = new PaperSize("Receipt", 300, 500);
            printDoc.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

            // Menampilkan dialog preview struk
            PrintPreviewDialog previewDialog = new PrintPreviewDialog();
            previewDialog.Document = printDoc;
            previewDialog.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public struk()
=======
        string konfigurasi = "server=localhost;username=root;password=;database=toko_buku";
        private long idTransaksi;
        
        public struk(long idTrans)
>>>>>>> parent of 0e318d8 (membuat struk)
        {
            InitializeComponent();
            this.idTransaksi = idTrans;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void struk_Load(object sender, EventArgs e)
        {
            MySqlConnection koneksi = new MySqlConnection(konfigurasi);

<<<<<<< HEAD

=======
            try
            {
                koneksi.Open();
                string queryHeader = @"SELECT id_transaksi, tanggal, total_harga, id_user 
                               FROM transactions 
                               WHERE id_transaksi = @idTrans";

                MySqlCommand cmdHeader = new MySqlCommand(queryHeader, koneksi);
                cmdHeader.Parameters.AddWithValue("@idTrans", idTransaksi);

                using (MySqlDataReader reader = cmdHeader.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        lblNoTrans.Text = "TRX-" + reader["id_transaksi"].ToString().PadLeft(3, '0');
                        lblTanggal.Text = Convert.ToDateTime(reader["tanggal"]).ToString("dd/MM/yyyy HH:mm");
                        lblKasir.Text = reader["id_user"].ToString();
                        decimal total = Convert.ToDecimal(reader["total_harga"]);
                        lblTotal.Text = "Rp " + total.ToString("N0");
                    }
                }
                string queryDetail = @"SELECT b.judul, b.harga, td.jumlah, td.subtotal 
                               FROM transaction_details td 
                               JOIN books b ON td.id_buku = b.id_buku 
                               WHERE td.id_transaksi = @idTrans";

                MySqlCommand cmdDetail = new MySqlCommand(queryDetail, koneksi);
                cmdDetail.Parameters.AddWithValue("@idTrans", idTransaksi);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmdDetail);
                DataTable dtDetail = new DataTable();
                adapter.Fill(dtDetail);
                string textRincian = "";
                int maxKarakterPerBaris = 40; 

                foreach (DataRow row in dtDetail.Rows)
                {
                    string judul = row["judul"].ToString();
                    int jumlah = Convert.ToInt32(row["jumlah"]);
                    decimal harga = Convert.ToDecimal(row["harga"]);
                    decimal subtotal = Convert.ToDecimal(row["subtotal"]);
                    string teksKiri = $"{jumlah} X  {harga:N0}";  
                    string teksKanan = $"{subtotal:N0}";           
                    int jumlahSpasi = maxKarakterPerBaris - (teksKiri.Length + teksKanan.Length);
                    if (jumlahSpasi < 1) jumlahSpasi = 1; 
                    string spasi = new string(' ', jumlahSpasi);
                    textRincian += $"{judul}\n";
                    textRincian += $"{teksKiri}{spasi}{teksKanan}\n\n";
                }

                lblDaftarItem.Text = textRincian;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan struk: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (koneksi.State == ConnectionState.Open) koneksi.Close();
            }
>>>>>>> parent of 0e318d8 (membuat struk)
        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            // Font yang digunakan
            Font fontTitle = new Font("Segoe UI", 10, FontStyle.Bold);
            Font fontRegular = new Font("Segoe UI", 9, FontStyle.Regular);
            Font fontBold = new Font("Segoe UI", 9, FontStyle.Bold);

            // Alignment format
            StringFormat formatCenter = new StringFormat { Alignment = StringAlignment.Center };
            StringFormat formatRight = new StringFormat { Alignment = StringAlignment.Far };

            // Koordinat & dimensi dasar
            float startX = 10;
            float startY = 10;
            float width = 280; // Lebar printable area
            float y = startY;
            float lineSpacing = 18;

            string lineSeparator = "--------------------------------------------------";

            // 1. HEADER (Nama & Alamat Toko)
            RectangleF rectHeaderTitle = new RectangleF(startX, y, width, 20);
            g.DrawString("TOKO BUKU ONLINE", fontTitle, Brushes.Black, rectHeaderTitle, formatCenter);
            y += lineSpacing;

            RectangleF rectHeaderAddress = new RectangleF(startX, y, width, 20);
            g.DrawString("Jl. Sudirman No. 123, Jakarta", fontRegular, Brushes.Black, rectHeaderAddress, formatCenter);
            y += lineSpacing + 5;

            // Garis Pemisah
            g.DrawString(lineSeparator, fontRegular, Brushes.Black, startX, y);
            y += lineSpacing - 3;

            // 2. INFORMASI TRANSAKSI
            g.DrawString("No. Trans : #TRX-10024", fontRegular, Brushes.Black, startX, y);
            y += lineSpacing;
            g.DrawString("Tanggal : 14/09/2026 19:38", fontRegular, Brushes.Black, startX, y);
            y += lineSpacing;
            g.DrawString("Kasir : Budi Santoso", fontRegular, Brushes.Black, startX, y);
            y += lineSpacing + 5;

            // Garis Pemisah
            g.DrawString(lineSeparator, fontRegular, Brushes.Black, startX, y);
            y += lineSpacing - 3;

            // 3. DAFTAR BARANG
            // Item 1
            g.DrawString("Pemrograman C# WinForms", fontRegular, Brushes.Black, startX, y);
            y += lineSpacing;
            g.DrawString("1 x 85.000", fontRegular, Brushes.Black, startX, y);
            g.DrawString("85.000", fontRegular, Brushes.Black, startX + width, y, formatRight);
            y += lineSpacing + 2;

            // Item 2
            g.DrawString("Panduan Basis Data SQL", fontRegular, Brushes.Black, startX, y);
            y += lineSpacing;
            g.DrawString("2 x 95.000", fontRegular, Brushes.Black, startX, y);
            g.DrawString("190.000", fontRegular, Brushes.Black, startX + width, y, formatRight);
            y += lineSpacing + 5;

            // Garis Pemisah
            g.DrawString(lineSeparator, fontRegular, Brushes.Black, startX, y);
            y += lineSpacing - 3;

            // 4. TOTAL, BAYAR, KEMBALI
            // TOTAL
            g.DrawString("TOTAL", fontBold, Brushes.Black, startX, y);
            g.DrawString("Rp 275.000", fontBold, Brushes.Black, startX + width, y, formatRight);
            y += lineSpacing;

            // BAYAR
            g.DrawString("BAYAR", fontRegular, Brushes.Black, startX, y);
            g.DrawString("Rp 300.000", fontRegular, Brushes.Black, startX + width, y, formatRight);
            y += lineSpacing;

            // KEMBALI
            g.DrawString("KEMBALI", fontRegular, Brushes.Black, startX, y);
            g.DrawString("Rp 25.000", fontRegular, Brushes.Black, startX + width, y, formatRight);
            y += lineSpacing + 5;

            // Garis Pemisah
            g.DrawString(lineSeparator, fontRegular, Brushes.Black, startX, y);
            y += lineSpacing + 5;

            // 5. FOOTER
            RectangleF rectFooter = new RectangleF(startX, y, width, 20);
            g.DrawString("Terima Kasih Atas Kunjungan Anda", fontRegular, Brushes.Black, rectFooter, formatCenter);
        }

        private void btnCetakStruk_Click(object sender, EventArgs e)
        {

        }

        private void btnTutup_Click(object sender, EventArgs e)
        {

        }
    }
}

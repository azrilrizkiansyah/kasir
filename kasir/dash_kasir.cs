using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MySql.Data.MySqlClient;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace kasir
{
    public partial class dash_kasir : Form
    {
        string konfigurasi = "server=localhost;username=root;password=;database=toko_buku;";
        public dash_kasir()
        {
            InitializeComponent();
        }

        private void dash_kasir_Load(object sender, EventArgs e)
        {
            btn_laporaPenjual.Enabled = false;
            btn_manajemenStok.Enabled = true;
            btn_Restore_Data.Enabled = false;
            btn_tambah_buku.Enabled = false;
            btn_transaksi.Enabled = true;
            Btn_BackupData.Enabled = false;
            TampilkanChartStokBuku();


        }
        private void TampilkanChartStokBuku()
        {
            MySqlConnection koneksi = new MySqlConnection(konfigurasi);
            MySqlDataReader hasil = null;

            // Query untuk mengambil judul buku dan stok buku
            string sql = "SELECT judul, stok FROM books ORDER BY judul ASC";

            try
            {
                koneksi.Open();
                MySqlCommand komando = new MySqlCommand(sql, koneksi);
                hasil = komando.ExecuteReader();

                // 1. Bersihkan elemen lama pada Chart
                chart1.Series.Clear();
                chart1.Legends.Clear();
                chart1.Titles.Clear();

                // 2. Buat dan Konfigurasi Legenda (Keterangan di Sebelah Kanan Atas)
                Legend legend = new Legend("LegendUtama");
                legend.Docking = Docking.Right;              // Posisi di sebelah kanan
                legend.Alignment = StringAlignment.Far;      // Rata atas (Top Right)
                legend.LegendStyle = LegendStyle.Column;     // Menurun secara vertikal
                legend.IsTextAutoFit = false;
                legend.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
                chart1.Legends.Add(legend);

                // 3. Buat Series baru dengan Tipe Doughnut
                Series seriesStok = new Series("StokBuku");
                seriesStok.ChartType = SeriesChartType.Doughnut;

                // Menghilangkan label angka di dalam chart agar bersih seperti gambar
                seriesStok.IsValueShownAsLabel = false;

                // Mengatur ukuran lubang tengah (Doughnut Radius)
                seriesStok["DoughnutRadius"] = "45";

                // 4. Custom Warna Setiap Sektor (Sesuai Skema Warna pada Gambar)
                Color[] daftarWarna = new Color[]
                {
                    Color.FromArgb(66, 133, 244),  // Biru Muda (seperti '2')
                    Color.FromArgb(251, 188, 66),  // Kuning / Oranye (seperti '3')
                    Color.FromArgb(227, 60, 18),   // Merah Oranye (seperti '4')
                    Color.FromArgb(6, 102, 148),    // Biru Toska Gelap (seperti '5')
                    Color.FromArgb(189, 189, 189),  // Abu-abu (seperti '6')
                    Color.FromArgb(27, 54, 93)     // Biru Dongker / Navy (seperti '7')
                };

                int indexWarna = 0;

                // 5. Tambahkan Data dari MySQL ke Chart
                while (hasil.Read())
                {
                    string namaBuku = hasil["judul"].ToString();
                    int stokBuku = Convert.ToInt32(hasil["stok"]);

                    // Tambahkan titik data (DataPoint)
                    int pIndex = seriesStok.Points.AddXY(namaBuku, stokBuku);
                    DataPoint point = seriesStok.Points[pIndex];

                    // Tampilkan nama buku/label di Legend
                    point.LegendText = namaBuku;

                    // Terapkan warna sesuai urutan
                    if (indexWarna < daftarWarna.Length)
                    {
                        point.Color = daftarWarna[indexWarna];
                        indexWarna++;
                    }
                }

                // 6. Masukkan Series ke dalam Chart Control
                chart1.Series.Add(seriesStok);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat chart: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Tutup koneksi database secara manual
                if (hasil != null && !hasil.IsClosed)
                {
                    hasil.Close();
                }
                if (koneksi != null && koneksi.State == ConnectionState.Open)
                {
                    koneksi.Close();
                }
            }
        }

        private void btn_sig_out_Click(object sender, EventArgs e)
        {
            login kembali = new login();
            kembali.FormClosed += Kembali_FormClosed;
            kembali.Show();
            this.Hide();
        }

        private void Kembali_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void btn_transaksi_Click(object sender, EventArgs e)
        {
            tran_penjulan pindah = new tran_penjulan();
            pindah.FormClosed += Pindah_FormClosed;
            pindah.Show();
            this.Hide();
        }

        private void Pindah_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void Kembali1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void btn_manajemenStok_Click(object sender, EventArgs e)
        {
            kasir_stok pindah = new kasir_stok();
            pindah.FormClosed += Pindah_FormClosed1;
            pindah.Show();
            this.Hide();
        }

        private void Pindah_FormClosed1(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
    }
}

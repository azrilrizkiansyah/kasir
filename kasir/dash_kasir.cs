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
        private Form currentChild = null; 
        public dash_kasir()
        {
            InitializeComponent();
        }

        private void dash_kasir_Load(object sender, EventArgs e)
        {
           
            btn_manajemenStok.Enabled = true;
            btn_Restore_Data.Enabled = false;
            btn_tambah_buku.Enabled = false;
            btn_transaksi.Enabled = true;
            Btn_BackupData.Enabled = false;
            AktifkanMenu(btn_dashboard);

            tampilkanStok();
            tampilkanunitbuku();
            TampilkanTotalSemuaTransaksi();
            UpdateDashboardPenjualan();

            MySqlConnection koneksi = new MySqlConnection(konfigurasi);
            try
            {
                koneksi.Open();

                // Query dengan JOIN agar otomatis mengambil nama buku dari tabel books
                string query = @"SELECT 
                        td.id_detail AS 'Detail ID', 
                        td.id_transaksi AS 'ID Transaksi', 
                        td.id_buku AS 'ID Buku', 
                        b.judul AS 'Nama Buku', 
                        td.jumlah AS 'Jumlah', 
                        td.subtotal AS 'Subtotal' 
                     FROM transaction_details td 
                     JOIN books b ON td.id_buku = b.id_buku";

                MySqlCommand cmd = new MySqlCommand(query, koneksi);

                // Gunakan DataAdapter dan DataTable untuk mengisi data secara otomatis
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Masukkan DataTable ke DataGridView secara otomatis
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = dt;

                // Supaya tampilannya rapi memenuhi lebar tabel
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Format mata uang untuk kolom Subtotal
                if (dataGridView1.Columns["Subtotal"] != null)
                {
                    dataGridView1.Columns["Subtotal"].DefaultCellStyle.Format = "N0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (koneksi.State == ConnectionState.Open)
                {
                    koneksi.Close();
                }
            }


        }
        private void tampilkanStok()
        {

            MySqlConnection koneksi = new MySqlConnection(konfigurasi);
            try
            {
                koneksi.Open();
                string query = "SELECT SUM(stok) FROM books";
                MySqlCommand cmd = new MySqlCommand(query, koneksi);
                int totalStok = System.Convert.ToInt32(cmd.ExecuteScalar());
                label3.Text = totalStok > 0 ? totalStok + " " : "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void tampilkanunitbuku()
        {
            MySqlConnection koneksi = new MySqlConnection(konfigurasi);
            try
            {
                koneksi.Open();
                string query = "SELECT SUM(jumlah) FROM transaction_details";
                MySqlCommand cmd = new MySqlCommand(query, koneksi);
                int totalUnit = System.Convert.ToInt32(cmd.ExecuteScalar());
                label14.Text = totalUnit > 0 ? totalUnit + " " : "";

                if (totalUnit > 0)
                {
                    label13.Text = totalUnit.ToString() + " unit buku terjual";
                    label13.ForeColor = Color.ForestGreen;
                }
                else
                {
                    label13.Text = "Belum ada buku terjual";
                    label13.ForeColor = Color.Gray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void TampilkanTotalSemuaTransaksi()
        {
            MySqlConnection koneksi = new MySqlConnection(konfigurasi);
            try
            {
                koneksi.Open();
                string query = "SELECT COUNT(id_transaksi) FROM transactions";
                MySqlCommand cmd = new MySqlCommand(query, koneksi);

                int total = System.Convert.ToInt32(cmd.ExecuteScalar());
                label11.Text = total > 0 ? total + " " : "";

                if (total > 0)
                {
                    label10.Text = total.ToString() + " transaksi tercatat";
                    label10.ForeColor = Color.ForestGreen;
                }
                else
                {
                    label10.Text = "Belum ada transaksi";
                    label10.ForeColor = Color.Gray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                koneksi.Close();
            }
        }
        private void UpdateDashboardPenjualan()
        {
            // Deklarasi variabel di luar try agar bisa diakses di blok finally
            MySqlConnection conn = null;
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;
            MySqlConnection koneksi = new MySqlConnection(konfigurasi);
            try
            {

                koneksi.Open();
                // Query untuk mengambil total nominal dan total jumlah transaksi
                string query = @"SELECT COALESCE(SUM(total_harga), 0) AS total_nominal,COUNT(id_transaksi) AS total_jumlah FROM transactions WHERE DATE(tanggal) = CURDATE()";
                MySqlCommand kemando = new MySqlCommand(query, koneksi);
                MySqlDataReader pembaca = kemando.ExecuteReader();

                if (pembaca.Read())
                {
                    decimal totalNominal = pembaca.GetDecimal("total_nominal");
                    int totalJumlah = pembaca.GetInt32("total_jumlah");


                    label8.Text = totalNominal.ToString("C0", new System.Globalization.CultureInfo("id-ID"));


                    if (totalJumlah > 0)
                    {
                        label9.Text = totalJumlah.ToString() + " Transaksi hari ini";
                        label9.ForeColor = System.Drawing.Color.ForestGreen;
                    }
                    else
                    {
                        label9.Text = "Belum ada transaksi hari ini";
                        label9.ForeColor = System.Drawing.Color.Gray;
                    }
                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Gagal memuat data: " + ex.Message);
            }
            finally
            {

                if (reader != null) reader.Close();
                if (cmd != null) cmd.Dispose();
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }
        public void BukaFormAnak(Form formAnak)
        {
            panelUtama.Controls.Clear();

            formAnak.TopLevel = false;
            formAnak.FormBorderStyle = FormBorderStyle.None;
            formAnak.Dock = DockStyle.Fill;

            panelUtama.Controls.Add(formAnak);
            formAnak.Show();
        }

        

        private void Kembali_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void Pindah_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

       
        // Variabel untuk menyimpan tombol yang sedang aktif saat ini
        private Button tombolAktif = null;

        private void AktifkanMenu(Button tombolPilihan)
        {
            // Jika ada tombol yang sebelumnya aktif, kembalikan warnanya ke normal
            if (tombolAktif != null)
            {
                tombolAktif.BackColor = Color.FromArgb(15, 23, 42); // Warna normal sidebar
            }

            // Set tombol yang baru dipilih menjadi tombol aktif
            tombolAktif = tombolPilihan;

            // Berikan warna khusus untuk menandakan menu sedang aktif (misalnya warna biru terang / sedikit lebih terang)
            tombolAktif.BackColor = Color.FromArgb(37, 99, 235);
        }

       

        

        private void Pindah_FormClosed2(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void btn_transaksi_Click(object sender, EventArgs e)
        {
            BukaFormAnak(new tran_penjulan());
            AktifkanMenu(btn_transaksi);
        }

        private void btn_manajemenStok_Click(object sender, EventArgs e)
        {
            BukaFormAnak(new kasir_stok());
            AktifkanMenu(btn_manajemenStok);
        }

        private void btn_riwayat_Click(object sender, EventArgs e)
        {

            BukaFormAnak(new riwayar_tran());
            AktifkanMenu(btn_riwayat);
        }

        private void btn_dashboard_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Bersihkan form anak yang sedang aktif jika ada
                if (currentChild != null)
                {
                    currentChild.Close();
                    currentChild.Dispose();
                    currentChild = null;
                }

                // 2. Kosongkan panel utama lalu muat ulang komponen/data dashboard
                panelUtama.Controls.Clear();

                // Panggil kembali fungsi data dashboard agar angka dan tabelnya muncul lagi
                tampilkanStok();
                tampilkanunitbuku();
                TampilkanTotalSemuaTransaksi();
                UpdateDashboardPenjualan();

                // Aktifkan indikator warna tombol sidebar
                AktifkanMenu(btn_dashboard);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat membuka dashboard: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

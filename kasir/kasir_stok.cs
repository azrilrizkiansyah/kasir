using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace kasir
{
    public partial class kasir_stok : Form
    {
        string konfigurasi = "server=localhost;username=root;password=;database=toko_buku";
        public kasir_stok()
        {
            InitializeComponent();
        }

        private void kasir_stok_Load(object sender, EventArgs e)
        {
            btn_laporaPenjual.Enabled = false;
            btn_tambah_buku.Enabled = false;
            Btn_BackupData.Enabled = false;
            btn_Restore_Data.Enabled = false;
            AktifkanMenu(btn_manajemenStok);
            
            MySqlConnection conn = new MySqlConnection(konfigurasi);
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT kode_buku, judul, stok FROM books", conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);          
                dataGridView1.DataSource = dt;           
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Columns["kode_buku"].FillWeight = 25;
                dataGridView1.Columns["judul"].FillWeight = 55;
                dataGridView1.Columns["stok"].FillWeight = 20;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
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

        private void button4_Click(object sender, EventArgs e)
        {
            dash_kasir kembali = new dash_kasir();
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
            tran_penjulan kembali = new tran_penjulan();
            kembali.FormClosed += Kembali_FormClosed1;
            kembali.Show();
            this.Hide();
        }

        private void Kembali_FormClosed1(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void btn_log_out_Click(object sender, EventArgs e)
        {
            login kembali2 = new login();
            kembali2.FormClosed += Kembali2_FormClosed;
            kembali2.Show();
            this.Hide();
        }

        private void Kembali2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
    }
}

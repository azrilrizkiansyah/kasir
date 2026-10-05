using MySql.Data.MySqlClient;
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

namespace kasir
{
    public partial class Stok_buku : Form
    {
        string koneksi = "server=localhost;database=toko_buku;username=root;password=;";
        public Stok_buku()
        {
            InitializeComponent();
        }
        private Button tombolAktif = null;

        private void AktifkanMenu(Button tombolPilihan)
        {

            if (tombolAktif != null)
            {
                tombolAktif.BackColor = Color.FromArgb(15, 23, 42);
            }
            tombolAktif = tombolPilihan;
            tombolAktif.BackColor = Color.FromArgb(37, 99, 235);
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void Kembali_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void Stok_buku_Load(object sender, EventArgs e)
        {
            btn_transaksi.Enabled = false;
            AktifkanMenu(btn_manajemenStok);

            MySqlConnection conn = new MySqlConnection(koneksi);

            conn.Open();

            MySqlDataAdapter da = new MySqlDataAdapter(
                "SELECT * FROM books", conn);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;

            conn.Close();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Dash_admin kembali = new Dash_admin();
            kembali.FormClosed += Kembali_FormClosed;
            kembali.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_manajemenBuku_Click(object sender, EventArgs e)
        {
            dafta_buku pindah = new dafta_buku();
            pindah.FormClosed += Pindah_FormClosed;
            pindah.Show();
            this.Hide();
        }

        private void Pindah_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void btn_laporaPenjual_Click(object sender, EventArgs e)
        {
            Laporan_penjualan pindah = new Laporan_penjualan();
            pindah.FormClosed += Pindah_FormClosed1;
            pindah.Show();
            this.Hide();
        }

        private void Pindah_FormClosed1(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Pilih buku terlebih dahulu!");
                return;
            }

            int jumlah;

            if (!int.TryParse(textBox2.Text, out jumlah))
            {
                MessageBox.Show("Masukkan jumlah stok berupa angka!");
                return;
            }

            int id = Convert.ToInt32(
                dataGridView1.CurrentRow.Cells["id_buku"].Value
            );

            MySqlConnection conn = new MySqlConnection(koneksi);

            conn.Open();

            string sql = "UPDATE books SET stok = stok + "
                       + jumlah + " WHERE id_buku = " + id;

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

            conn.Close();

            MessageBox.Show("Stok berhasil ditambahkan!");

            Stok_buku_Load(null, null);

            textBox2.Clear();
        }

        private void btn_cari_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(koneksi);

            conn.Open();

            string sql = "SELECT * FROM books WHERE judul LIKE '%" + txt_cari.Text + "%'";

            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();

            da.Fill(dt);

            dataGridView1.DataSource = dt;

            conn.Close();
        }

        private void btn_batal_Click(object sender, EventArgs e)
        {
            txt_cari.Clear();
            textBox2.Clear();
        }
    }
}

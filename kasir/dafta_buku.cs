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
    public partial class dafta_buku : Form
    {
        MySqlConnection conn = new MySqlConnection(
           "server=localhost;database=toko_buku;username=root;password=;"
            ); 
        public dafta_buku()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            conn.Open();

            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM books", conn);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;

            conn.Close();

            btn_transaksi.Enabled = false;
            txt_id.ReadOnly = true;
            AktifkanMenu(btn_manajemenBuku);
            btn_update.Enabled = false;
            btn_hapus.Enabled = false;
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

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void Kembali_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void btn_log_out_Click(object sender, EventArgs e)
        {
            
        }

        private void Kembali2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void btn_laporaPenjual_Click(object sender, EventArgs e)
        {
            
        }

        private void Pindah_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void btn_manajemenStok_Click(object sender, EventArgs e)
        {
            
        }

        private void Pindah_FormClosed1(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void btn_tambah_Click(object sender, EventArgs e)
        {
            conn.Open();

            string sql = "INSERT INTO books (kode_buku, judul, pengarang, penerbit, harga, stok) VALUES (@kode, @judul, @pengarang, @penerbit, @harga, @stok)";

            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@kode", txt_kode.Text);
            cmd.Parameters.AddWithValue("@judul", txt_judul.Text);
            cmd.Parameters.AddWithValue("@pengarang", txt_pengarang.Text);
            cmd.Parameters.AddWithValue("@penerbit", txt_penerbit.Text);
            cmd.Parameters.AddWithValue("@harga", txt_harga.Text);
            cmd.Parameters.AddWithValue("@stok", txt_hargabuku.Text);

            cmd.ExecuteNonQuery();

            conn.Close();

            MessageBox.Show("Data berhasil ditambahkan");

            Form3_Load(sender, e);
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            conn.Open();

            string sql = "UPDATE books SET kode_buku=@kode, judul=@judul, pengarang=@pengarang, penerbit=@penerbit, harga=@harga, stok=@stok WHERE id_buku=@id";

            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@id", txt_id.Text);
            cmd.Parameters.AddWithValue("@kode", txt_kode.Text);
            cmd.Parameters.AddWithValue("@judul", txt_judul.Text);
            cmd.Parameters.AddWithValue("@pengarang", txt_pengarang.Text);
            cmd.Parameters.AddWithValue("@penerbit", txt_penerbit.Text);
            cmd.Parameters.AddWithValue("@harga", txt_harga.Text);
            cmd.Parameters.AddWithValue("@stok", txt_hargabuku.Text);

            cmd.ExecuteNonQuery();

            conn.Close();

            MessageBox.Show("Data berhasil diubah");

            Form3_Load(sender, e);
        }

        private void btn_hapus_Click(object sender, EventArgs e)
        {
            conn.Open();

            string sql = "DELETE FROM books WHERE id_buku=@id";

            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@id", txt_id.Text);

            cmd.ExecuteNonQuery();

            conn.Close();

            MessageBox.Show("Data berhasil dihapus");

            Form3_Load(sender, e);
        }

        private void btn_batal_Click(object sender, EventArgs e)
        {
            txt_id.Clear();
            txt_kode.Clear();
            txt_judul.Clear();
            txt_pengarang.Clear();
            txt_penerbit.Clear();
            txt_harga.Clear();
            txt_hargabuku.Clear();

            btn_tambah.Enabled = true;
            btn_update.Enabled = false;
            btn_hapus.Enabled = false;
        }

        private void btn_cari_Click(object sender, EventArgs e)
        {
            conn.Open();

            string sql = "SELECT * FROM books WHERE judul LIKE @cari";

            MySqlCommand cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue(
                "@cari",
                "%" + textBox8.Text + "%"
            );

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            dataGridView1.DataSource = dt;

            conn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txt_id.Text =
                    dataGridView1.Rows[e.RowIndex].Cells["id_buku"].Value.ToString();

                txt_kode.Text =
                    dataGridView1.Rows[e.RowIndex].Cells["kode_buku"].Value.ToString();

                txt_judul.Text =
                    dataGridView1.Rows[e.RowIndex].Cells["judul"].Value.ToString();

                txt_pengarang.Text =
                    dataGridView1.Rows[e.RowIndex].Cells["pengarang"].Value.ToString();

                txt_penerbit.Text =
                    dataGridView1.Rows[e.RowIndex].Cells["penerbit"].Value.ToString();

                txt_harga.Text =
                    dataGridView1.Rows[e.RowIndex].Cells["harga"].Value.ToString();

                txt_hargabuku.Text =
                    dataGridView1.Rows[e.RowIndex].Cells["stok"].Value.ToString();

                btn_tambah.Enabled = false;
                btn_update.Enabled = true;
                btn_hapus.Enabled = true;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Dash_admin kembali = new Dash_admin();
            kembali.FormClosed += Kembali_FormClosed1;
            kembali.Show();
            this.Hide();
        }

        private void Kembali_FormClosed1(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void btn_manajemenBuku_Click(object sender, EventArgs e)
        {

        }

        private void btn_laporaPenjual_Click_1(object sender, EventArgs e)
        {
            Laporan_penjualan pindah = new Laporan_penjualan();
            pindah.FormClosed += Pindah_FormClosed;
            pindah.Show();
            this.Hide();
        }

        private void btn_manajemenStok_Click_1(object sender, EventArgs e)
        {
            Stok_buku pindah = new Stok_buku();
            pindah.FormClosed += Pindah_FormClosed1;
            pindah.Show();
            this.Hide();
        }

        private void btn_log_out_Click_1(object sender, EventArgs e)
        {
            Dash_admin kembali2 = new Dash_admin();
            kembali2.FormClosed += Kembali2_FormClosed;
            kembali2.Show();
            this.Hide();
        }
    }
}

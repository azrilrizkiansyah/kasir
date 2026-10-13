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
        string conn = "server=localhost;database=toko_buku;username=root;password=;";
        public dafta_buku()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            MySqlConnection koneksi = new MySqlConnection(conn);
            koneksi.Open();

            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM books", koneksi);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataSource = dt;

            koneksi.Close();

            
            txt_id.ReadOnly = true;
            
            btn_update.Enabled = false;
            btn_hapus.Enabled = false;
        }
        private Button tombolAktif = null; 

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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

        private void btn_tambah_Click(object sender, EventArgs e)
        {
            MySqlConnection koneksi = new MySqlConnection(conn);
            koneksi.Open();

            string sql = "INSERT INTO books (kode_buku, judul, pengarang, penerbit, harga, stok) VALUES (@kode, @judul, @pengarang, @penerbit, @harga, @stok)";

            MySqlCommand cmd = new MySqlCommand(sql, koneksi);

            cmd.Parameters.AddWithValue("@kode", txt_kode.Text);
            cmd.Parameters.AddWithValue("@judul", txt_judul.Text);
            cmd.Parameters.AddWithValue("@pengarang", txt_pengarang.Text);
            cmd.Parameters.AddWithValue("@penerbit", txt_penerbit.Text);
            cmd.Parameters.AddWithValue("@harga", txt_harga.Text);
            cmd.Parameters.AddWithValue("@stok", txt_hargabuku.Text);

            cmd.ExecuteNonQuery();

            koneksi.Close();

            MessageBox.Show("Data berhasil ditambahkan");

            Form3_Load(sender, e);
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            MySqlConnection koneksi = new MySqlConnection(conn);
            koneksi.Open();

            string sql = "UPDATE books SET kode_buku=@kode, judul=@judul, pengarang=@pengarang, penerbit=@penerbit, harga=@harga, stok=@stok WHERE id_buku=@id";

            MySqlCommand cmd = new MySqlCommand(sql, koneksi);

            cmd.Parameters.AddWithValue("@id", txt_id.Text);
            cmd.Parameters.AddWithValue("@kode", txt_kode.Text);
            cmd.Parameters.AddWithValue("@judul", txt_judul.Text);
            cmd.Parameters.AddWithValue("@pengarang", txt_pengarang.Text);
            cmd.Parameters.AddWithValue("@penerbit", txt_penerbit.Text);
            cmd.Parameters.AddWithValue("@harga", txt_harga.Text);
            cmd.Parameters.AddWithValue("@stok", txt_hargabuku.Text);

            cmd.ExecuteNonQuery();

            koneksi.Close();

            MessageBox.Show("Data berhasil diubah");

            Form3_Load(sender, e);
        }

        private void btn_hapus_Click(object sender, EventArgs e)
        {
            MySqlConnection koneksi = new MySqlConnection(conn);
            koneksi.Open();

            string sql = "DELETE FROM books WHERE id_buku=@id";

            MySqlCommand cmd = new MySqlCommand(sql, koneksi);

            cmd.Parameters.AddWithValue("@id", txt_id.Text);

            cmd.ExecuteNonQuery();

            koneksi.Close();

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

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            MySqlConnection koneksi = new MySqlConnection(conn); 
            try
            {
                koneksi.Open();
                string sql = @"SELECT 
                        kode_buku AS 'Kode Buku', 
                        judul AS 'Judul', 
                        pengarang AS 'Pengarang', 
                        penerbit AS 'Penerbit', 
                        harga AS 'Harga', 
                        stok AS 'Stok' 
                       FROM books 
                       WHERE judul LIKE @cari OR pengarang LIKE @cari";

                MySqlCommand komando = new MySqlCommand(sql, koneksi);
                komando.Parameters.AddWithValue("@cari", "%" + textBox8.Text + "%");
                MySqlDataAdapter adapter = new MySqlDataAdapter(komando);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = dt;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat buku: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (koneksi.State == ConnectionState.Open)
                {
                    koneksi.Close();
                }
            }
        }
    }
}

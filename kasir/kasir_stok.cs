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
            // Menampilkan data kodebuku, judulbuku, dan stok dari database ke datagridview
            MySqlConnection conn = new MySqlConnection(konfigurasi);
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT kode_buku, judul, stok FROM books", conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // 1. Tampilkan data dari database
                dataGridView1.DataSource = dt;

                // 2. Hilangkan space abu-abu di sebelah kanan (kolom otomatis melebar)
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // 3. Atur proporsi lebar kolom agar rapi (opsional)
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
    }
}

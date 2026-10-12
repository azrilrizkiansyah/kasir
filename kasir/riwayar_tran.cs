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
    public partial class riwayar_tran : Form
    {
        public riwayar_tran()
        {
            InitializeComponent();
        }

        private void riwayar_tran_Load(object sender, EventArgs e)
        {
            MySqlConnection koneksi = new MySqlConnection(
               "server=localhost;database=toko_buku;uid=root;pwd=;");

            MySqlDataAdapter adapter = new MySqlDataAdapter(
                "SELECT * FROM transactions", koneksi);

            DataTable tabel = new DataTable();

            adapter.Fill(tabel);

            dataGridView1.DataSource = tabel;
        }
    }
}
